﻿using System.ServiceModel.Channels;

namespace ChatServer.Common.DispatchFormatters
{
    public class RawWebContentTypeMapper : WebContentTypeMapper
    {
        public override WebContentFormat GetMessageFormatForContentType(string contentType)
        {
            return WebContentFormat.Raw;
        }
    }
}