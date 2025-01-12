﻿using System;
using System.Runtime.Serialization;

namespace VanakIndustry.Core.Api.Exceptions
{
    public class ApiException: Exception
    {
        public ApiException()
        {
        }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
