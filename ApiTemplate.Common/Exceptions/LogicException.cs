using System;
using ApiTemplate.Common.Enums.Exceptions;

namespace ApiTemplate.Common.Exceptions
{
    public class LogicException : AppException
    {
        public LogicException() 
            : base(CustomStatusCodes.LogicError)
        {
            
        }

        public LogicException(string message) 
            : base(CustomStatusCodes.LogicError, message)
        {
        }

        public LogicException(object additionalData) 
            : base(CustomStatusCodes.LogicError, additionalData)
        {
        }

        public LogicException(string message, object additionalData) 
            : base(CustomStatusCodes.LogicError, message, additionalData)
        {
        }

        public LogicException(string message, Exception exception)
            : base(CustomStatusCodes.LogicError, message, exception)
        {
        }

        public LogicException(string message, Exception exception, object additionalData)
            : base(CustomStatusCodes.LogicError, message, exception, additionalData)
        {
        }
    }
}
