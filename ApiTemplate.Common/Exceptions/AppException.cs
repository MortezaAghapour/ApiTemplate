using System;
using System.Net;
using ApiTemplate.Common.Enums.Exceptions;

namespace ApiTemplate.Common.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public CustomStatusCodes ApiStatusCode { get; set; }
        public object AdditionalData { get; set; }

        public AppException()
            : this(CustomStatusCodes.ServerError)
        {
        }

        public AppException(CustomStatusCodes statusCode)
            : this(statusCode, null)
        {
        }

        public AppException(string message)
            : this(CustomStatusCodes.ServerError, message)
        {
        }

        public AppException(CustomStatusCodes statusCode, string message)
            : this(statusCode, message, HttpStatusCode.InternalServerError)
        {
        }

        public AppException(string message, object additionalData)
            : this(CustomStatusCodes.ServerError, message, additionalData)
        {
        }

        public AppException(CustomStatusCodes statusCode, object additionalData)
            : this(statusCode, null, additionalData)
        {
        }

        public AppException(CustomStatusCodes statusCode, string message, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
        {
        }

        public AppException(CustomStatusCodes statusCode, string message, HttpStatusCode httpStatusCode)
            : this(statusCode, message, httpStatusCode, null)
        {
        }

        public AppException(CustomStatusCodes statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
            : this(statusCode, message, httpStatusCode, null, additionalData)
        {
        }

        public AppException(string message, Exception exception)
            : this(CustomStatusCodes.ServerError, message, exception)
        {
        }

        public AppException(string message, Exception exception, object additionalData)
            : this(CustomStatusCodes.ServerError, message, exception, additionalData)
        {
        }

        public AppException(CustomStatusCodes statusCode, string message, Exception exception)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception)
        {
        }

        public AppException(CustomStatusCodes statusCode, string message, Exception exception, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
        {
        }

        public AppException(CustomStatusCodes statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
            : this(statusCode, message, httpStatusCode, exception, null)
        {
        }

        public AppException(CustomStatusCodes statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
            : base(message, exception)
        {
            ApiStatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
            AdditionalData = additionalData;
        }
    }
}
