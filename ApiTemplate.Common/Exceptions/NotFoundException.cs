using System;
using ApiTemplate.Common.Enums.Exceptions;

namespace ApiTemplate.Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
            : base(CustomStatusCodes.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(CustomStatusCodes.NotFound, message)
        {
        }

        public NotFoundException(object additionalData)
            : base(CustomStatusCodes.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, object additionalData)
            : base(CustomStatusCodes.NotFound, message, additionalData)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(CustomStatusCodes.NotFound, message, exception)
        {
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(CustomStatusCodes.NotFound, message, exception, additionalData)
        {
        }
    }
}
