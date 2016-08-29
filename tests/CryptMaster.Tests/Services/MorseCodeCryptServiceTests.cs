using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptMaster.Services;
using Xunit;

namespace CryptMaster.Tests
{
    public class MorseCodeCryptServiceTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData(".- .- .-   .- .- .-", "aaa aaa")]
        [InlineData(".- .- .-   \n\t.- .- .-", "aaa aaa")]
        [InlineData(".- .- .-     .- .- .-", "aaa aaa")]
        [InlineData("-- -.--  -.-. --- -.. .  .. ...  ... ---  -.-. --- --- .-..", "my code is so cool")]
        [InlineData(".- -... -.-. -.. . ..-. --. .... .. .--- -.- .-.. -- -. --- .--. --.- .-. ... - ..- ...- .-- -..- -.-- --.. ----- .---- ..--- ...-- ....- ..... -.... --... ---.. ----.", "abcdefghijklmnopqrstuvwxyz0123456789")]
        public void Decrypt_Morse_DecodeToCorrectText(string morse, string expectedText)
        {
            var sut = new MorseCodeCryptService();

            Assert.Equal(expectedText, sut.Decrypt(morse));
        }

        [Fact]
        public void Decrypt_InvalidMorseCode_ThrowsInvalidOperationException()
        {
            var sut = new MorseCodeCryptService();

            Assert.Throws<InvalidOperationException>(() => sut.Decrypt("...----"));
        }

        [Fact]
        public void Decrypt_InvalidCharacter_ThrowsInvalidOperationException()
        {
            var sut = new MorseCodeCryptService();

            Assert.Throws<InvalidOperationException>(() => sut.Decrypt("a"));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("aaa aaa", ".- .- .-  .- .- .-")]
        [InlineData("aaa\naaa", ".- .- .-  .- .- .-")]
        [InlineData("aaa\taaa", ".- .- .-  .- .- .-")]
        [InlineData("aaa    aaa", ".- .- .-  .- .- .-")]
        [InlineData("my code is so cool", "-- -.--  -.-. --- -.. .  .. ...  ... ---  -.-. --- --- .-..")]
        [InlineData("My Code Is sO coOl", "-- -.--  -.-. --- -.. .  .. ...  ... ---  -.-. --- --- .-..")]
        [InlineData("abcdefghijklmnopqrstuvwxyz0123456789", ".- -... -.-. -.. . ..-. --. .... .. .--- -.- .-.. -- -. --- .--. --.- .-. ... - ..- ...- .-- -..- -.-- --.. ----- .---- ..--- ...-- ....- ..... -.... --... ---.. ----.")]
        public void Encrypt_Morse_DecodeToCorretText(string text, string expectedMorse)
        {
            var sut = new MorseCodeCryptService();

            Assert.Equal(expectedMorse, sut.Encrypt(text));
        }
    }
}
