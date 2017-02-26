using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using ChatServer.Common.Models;
using ChatServer.Core.Interfaces;

namespace ChatServer.Core.Managers
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ChatRoomsManager : IChatRoomsManager
    {
        private readonly IChatRoomsProvider m_ChatRoomProvider;
        private readonly IMessagesManager m_MessageManager;
        private readonly ConcurrentDictionary<int, ChatRoom> m_OpenChatRooms;
        private readonly ConcurrentDictionary<string, Dictionary<int, ChatRoom>> m_OpenChatRoomsByParticipant;

        public ChatRoomsManager(IChatRoomsProvider chatRoomProvider, IMessagesManager messageManager)
        {
            if (chatRoomProvider == null)
            {
                throw new ArgumentNullException("chatRoomProvider", "ChatRoomProvider is null");
            }

            if (messageManager == null)
            {
                throw new ArgumentNullException("messageManager", "MessageManager is null");
            }

            m_ChatRoomProvider = chatRoomProvider;
            m_MessageManager = messageManager;
            m_OpenChatRooms = new ConcurrentDictionary<int, ChatRoom>();
            m_OpenChatRoomsByParticipant = new ConcurrentDictionary<string, Dictionary<int, ChatRoom>>();
        }

        public ChatRoom OpenChatRoom(IEnumerable<string> participantNames)
        {
            if (participantNames.Count() < 2)
            {
                throw new ArgumentException("At least two Chat room participants are required");
            }

            ChatRoom chatRoom = m_ChatRoomProvider.OpenChatRoom(participantNames);

            m_OpenChatRooms.AddOrUpdate(chatRoom.Id,
                                       (id) =>
                                       {
                                           ChatRoom updatedChatRoom = m_ChatRoomProvider.UpdateStartDate(id);
                                           m_MessageManager.AddSystemMessage(updatedChatRoom, SystemMessageType.Start);

                                           foreach (var username in updatedChatRoom.ParticipantNames)
                                           {
                                               m_OpenChatRoomsByParticipant.AddOrUpdate(username,
                                                   (user) =>
                                                   {
                                                       var openChatRooms = new Dictionary<int, ChatRoom>();
                                                       openChatRooms.Add(updatedChatRoom.Id, updatedChatRoom);
                                                       return openChatRooms;
                                                   },
                                                   (user, existingOpenChatRooms) =>
                                                   {
                                                       existingOpenChatRooms.Add(updatedChatRoom.Id, updatedChatRoom);
                                                       return existingOpenChatRooms;
                                                   });
                                           }

                                           return updatedChatRoom;
                                       },
                                       (id, exinstingChatRoom) => chatRoom);
            return chatRoom;
        }

        //public void AddParticipantToChatRoom(int chatRoomId, string username)
        //{
        //    ChatRoom chatRoom;
        //    if (m_OpenChatRooms.TryGetValue(chatRoomId, out chatRoom))
        //    {
        //        var users = chatRoom.ParticipantNames.ToList();
        //        users.Add(username);
        //        chatRoom.ParticipantNames = users;
        //    }
        //}

        public ChatRoom OpenChatRoomById(int chatRoomId)
        {
            if (chatRoomId < 1)
            {
                throw new ArgumentException("Chat room Id must be greater than zero");
            }

            ChatRoom chatRoom = m_OpenChatRooms.GetOrAdd(chatRoomId, (id) =>
                                                                     {
                                                                         ChatRoom chatRoomInDb = m_ChatRoomProvider.GetById(id);
                                                                         return chatRoomInDb;
                                                                     });
            return chatRoom;
        }

        public IEnumerable<ChatRoom> GetAllOpenChatRooms(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("username");
            }

            Dictionary<int, ChatRoom> chatRooms;
            if (m_OpenChatRoomsByParticipant.TryGetValue(username, out chatRooms))
            {

                var openChatRooms = chatRooms.Values.ToList();
                foreach (var openChatRoom in openChatRooms)
                {
                    openChatRoom.ParticipantNames = openChatRoom.ParticipantNames.OrderByDescending(c => c == username);
                }

                return openChatRooms;
            }
            else
            {
                return new ChatRoom[] { };
            }
        }

        public void CloseChatRoomById(int chatRoomId)
        {
            ChatRoom chatRoomInCache;
            if (m_OpenChatRooms.TryGetValue(chatRoomId, out chatRoomInCache))
            {
                ChatRoom updatedChatRoom = m_ChatRoomProvider.UpdateEndDate(chatRoomId);
                m_MessageManager.AddSystemMessage(updatedChatRoom, SystemMessageType.End);

                ChatRoom closedRoom;
                m_OpenChatRooms.TryRemove(chatRoomId, out closedRoom);

                if (closedRoom != null)
                {
                    foreach (var username in closedRoom.ParticipantNames)
                    {
                        m_OpenChatRoomsByParticipant.AddOrUpdate(username, new Dictionary<int, ChatRoom>(), (user, existingChatRooms) =>
                        {
                            existingChatRooms.Remove(closedRoom.Id);
                            return existingChatRooms;
                        });
                    }
                }
            }
        }

        public void CloseChatRoomsByUserName(string username)
        {
            Dictionary<int, ChatRoom> openChatRooms;
            m_OpenChatRoomsByParticipant.TryGetValue(username, out openChatRooms);
            if (openChatRooms != null && openChatRooms.Any())
            {
                foreach (var chatRoomId in openChatRooms.Keys.ToArray())
                {
                    CloseChatRoomById(chatRoomId);
                }
            }
        }
    }
}
