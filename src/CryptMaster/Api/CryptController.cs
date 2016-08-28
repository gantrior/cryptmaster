namespace CryptMaster.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CryptMaster.Api.Exceptions;
    using CryptMaster.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Crypt controller which handles all routes "api/v1/crypt*"
    /// New instance of this class is created for each request
    /// </summary>
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class CryptController : Controller
    {
        private readonly Dictionary<string, ICryptService> cryptServices;

        /// <summary>
        /// Creates new instance of the <see cref="CryptController"/> class.
        /// </summary>
        /// <param name="cryptServices">list of services which application supports for encryption/decryption.
        /// This list is filled automatically by DI container, for adding new service register it in <see cref="DefaultModule"/></param>
        public CryptController(IEnumerable<ICryptService> cryptServices)
        {
            this.cryptServices = cryptServices.ToDictionary(x => x.Type, x => x);
        }

        /// <summary>
        /// Gets available algorithms
        /// [Get] api/v1/crypt/types
        /// </summary>
        /// <returns>array of string with type names</returns>
        [HttpGet("types")]
        [Produces(typeof(string[]))]
        public IActionResult GetTypes()
        {
            return this.Ok(this.cryptServices.Keys.ToArray());
        }

        /// <summary>
        /// Encrypts given text with given algorithm
        /// </summary>
        /// <param name="config">config containing text and algorithm type</param>
        /// <returns><see cref="CryptResponse"/> with encrypted text</returns>
        [HttpPost("encrypt")]
        [Produces(typeof(CryptResponse))]
        public IActionResult Encrypt([FromBody] CryptConfig config)
        {
            return this.HandleOperation(config, (cryptService, text) => cryptService.Encrypt(text));
        }

        /// <summary>
        /// Decrypts given text with given algorithm
        /// </summary>
        /// <param name="config">config containing text and algorithm type</param>
        /// <returns><see cref="CryptResponse"/> with decrypted text</returns>
        [HttpPost("decrypt")]
        [Produces(typeof(CryptResponse))]
        public IActionResult Decrypt([FromBody] CryptConfig config)
        {
            return this.HandleOperation(config, (cryptService, text) => cryptService.Decrypt(text));
        }

        private IActionResult HandleOperation(CryptConfig config, Func<ICryptService, string, string> action)
        {
            try
            {
                if (this.cryptServices.ContainsKey(config.Type))
                {
                    ICryptService cryptService = this.cryptServices[config.Type];
                    return this.Ok(new CryptResponse()
                    {
                        Result = action(cryptService, config.Text)
                    });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new HttpException(500, ex.Message, ex);
            }
        }

        public class CryptConfig
        {
            public string Text { get; set; }

            public string Type { get; set; }
        }

        public class CryptResponse
        {
            public string Result { get; set; }
        }
    }
}
