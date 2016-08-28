namespace CryptMaster.Api.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CryptMaster.Api.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;

    /// <summary>
    /// Middleware which attach exception message into the error response
    /// more details are explained at <see cref="ApplicationBuilderExtensions.UseHttpException"/>
    /// </summary>
    public class HttpExceptionMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Creates new instance of the <see cref="HttpExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">middleware request</param>
        public HttpExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Action done for each request
        /// </summary>
        /// <param name="context">request context</param>
        /// <returns>response value</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (HttpException httpException)
            {
                context.Response.StatusCode = httpException.StatusCode;
                var responseFeature = context.Features.Get<IHttpResponseFeature>();
                responseFeature.ReasonPhrase = httpException.Message;
            }
        }
    }
}
