namespace CryptMaster.Api.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Extension for <see cref="IApplicationBuilder"/> adding middlewares supporting current project
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// The problem with default MVC behavior is that it does not send any information in case of error
        /// We are defining new Exception <see cref="HttpException"/> where we quarantee that if this is thrown it contains human-readable
        /// message, and therefore we rethrow this message to the clients
        /// This methods apply middleware which catch <see cref="HttpException"/> and adjust status code from its message
        /// </summary>
        /// <param name="application">startup application</param>
        /// <returns>startup application self</returns>
        public static IApplicationBuilder UseHttpException(this IApplicationBuilder application)
        {
            return application.UseMiddleware<HttpExceptionMiddleware>();
        }
    }
}
