namespace ChatServer.Core.Providers
{
    using System;
    using System.Collections.Generic;
    using ChatServer.Common.Models;
    using ChatServer.Data.Interfaces;
    using Interfaces;

    public class ChatRoomsProvider : IChatRoomsProvider
    {
        private readonly INaxexChatData m_NaxexChatData;

        public ChatRoomsProvider(INaxexChatData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data", "NaxexChatData is null");
            }

            m_NaxexChatData = data;
        }

        public ChatRoom GetById(int chatRoomId)
        {
            ChatRoom chatRoom = m_NaxexChatData.ChatRooms.GetRoomById(chatRoomId);
            return chatRoom;
        }

        public ChatRoom OpenChatRoom(IEnumerable<string> participantNames)
        {
            ChatRoom chatRoom = m_NaxexChatData.ChatRooms.GetChatRoom(participantNames);
            return chatRoom;
        }

        public ChatRoom UpdateStartDate(int chatRoomId)
        {
            ChatRoom chatRoom = m_NaxexChatData.ChatRooms.UpdateStartDate(chatRoomId);
            return chatRoom;
        }

        public ChatRoom UpdateEndDate(int chatRoomId)
        {
            ChatRoom chatRoom = m_NaxexChatData.ChatRooms.UpdateEndDate(chatRoomId);
            return chatRoom;
        }
    }
}
