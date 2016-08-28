namespace CryptMaster.Services
{
    /// <summary>
    /// Service which defines service for encryption/decryption of some algorithm
    /// </summary>
    public interface ICryptService
    {
        /// <summary>
        /// Type of the service. This can be string like "Morse code"
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Encrypts text by given algorithm
        /// </summary>
        /// <param name="text">text to encrypt</param>
        /// <returns>encrypted text</returns>
        string Encrypt(string text);

        /// <summary>
        /// Decrypts text by given algorithm
        /// </summary>
        /// <param name="text">text to decrypt</param>
        /// <returns>decrypted text</returns>
        string Decrypt(string text);
    }
}
