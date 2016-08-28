namespace CryptMaster.Api.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Middleware;

    /// <summary>
    /// Exception which is thrown when API responds error
    /// <see cref="ApplicationBuilderExtensions.UseHttpException"/> for more details
    /// </summary>
    public class HttpException : Exception
    {
        private readonly int httpStatusCode;

        /// <summary>
        /// Creates new instance of the <see cref="HttpException"/> class
        /// </summary>
        /// <param name="httpStatusCode">Http status code which will be returned to the clients</param>
        public HttpException(int httpStatusCode)
        {
            this.httpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Creates new instance of the <see cref="HttpException"/> class
        /// </summary>
        /// <param name="httpStatusCode">Http status code which will be returned to the clients</param>
        public HttpException(HttpStatusCode httpStatusCode)
        {
            this.httpStatusCode = (int)httpStatusCode;
        }

        /// <summary>
        /// Creates new instance of the <see cref="HttpException"/> class
        /// </summary>
        /// <param name="httpStatusCode">Http status code which will be returned to the clients</param>
        /// <param name="message">Message which will be thrown to the clients</param>
        public HttpException(int httpStatusCode, string message)
            : base(message)
        {
            this.httpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Creates new instance of the <see cref="HttpException"/> class
        /// </summary>
        /// <param name="httpStatusCode">Http status code which will be returned to the clients</param>
        /// <param name="message">Message which will be thrown to the clients</param>
        public HttpException(HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            this.httpStatusCode = (int)httpStatusCode;
        }

        /// <summary>
        /// Creates new instance of the <see cref="HttpException"/> class
        /// </summary>
        /// <param name="httpStatusCode">Http status code which will be returned to the clients</param>
        /// <param name="message">Message which will be thrown to the clients</param>
        /// <param name="inner">inner exception with further details</param>
        public HttpException(int httpStatusCode, string message, Exception inner)
            : base(message, inner)
        {
            this.httpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Creates new instance of the <see cref="HttpException"/> class
        /// </summary>
        /// <param name="httpStatusCode">Http status code which will be returned to the clients</param>
        /// <param name="message">Message which will be thrown to the clients</param>
        /// <param name="inner">inner exception with further details</param>
        public HttpException(HttpStatusCode httpStatusCode, string message, Exception inner)
            : base(message, inner)
        {
            this.httpStatusCode = (int)httpStatusCode;
        }

        /// <summary>
        /// Gets status code which will be returned to the clients
        /// </summary>
        public int StatusCode
        {
            get
            {
                return this.httpStatusCode;
            }
        }
    }
}
