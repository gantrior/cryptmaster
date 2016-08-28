using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptMaster.Api;
using CryptMaster.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static CryptMaster.Api.CryptController;

namespace CryptMaster.Tests.Api
{
    public class CryptControllerTests
    {
        [Fact]
        public void GetTypes_SimpleService_ReturnsCorrectServiceType()
        {
            CryptController sut = CreateSut();

            var result = sut.GetTypes();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<string[]>(okResult.Value);
            Assert.Equal(new string[] { "testservice" }, value);
        }

        [Fact]
        public void Encrypt_SimpleService_ReturnsEncryptedDada()
        {
            CryptController sut = CreateSut();

            var result = sut.Encrypt(new CryptController.CryptConfig()
            {
                Type = "testservice",
                Text = "aaa"
            });

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<CryptResponse>(okResult.Value);
            Assert.Equal("AAA", value.Result);
        }

        [Fact]
        public void Decrypt_SimpleService_ReturnsDecryptedDada()
        {
            CryptController sut = CreateSut();

            var result = sut.Decrypt(new CryptController.CryptConfig()
            {
                Type = "testservice",
                Text = "AAA"
            });

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<CryptResponse>(okResult.Value);
            Assert.Equal("aaa", value.Result);
        }

        private static CryptController CreateSut()
        {
            var service = new Mock<ICryptService>();
            service.SetupGet(x => x.Type).Returns("testservice");
            service.Setup(x => x.Encrypt(It.IsAny<string>())).Returns<string>((value) => value.ToUpper());
            service.Setup(x => x.Decrypt(It.IsAny<string>())).Returns<string>((value) => value.ToLower());
            var sut = new CryptController(new[] { service.Object });
            return sut;
        }
    }
}
