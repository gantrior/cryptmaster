namespace CryptMaster.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Morse code encryption algorithm. The class focuses on speed of Encrypt/Decrypt method.
    /// Encryption algorithm explained:
    /// * When service is created it prepares encryptionDictionary with all supported morse characters
    /// * When Encrypt is called, it iterates through the text and simply look for morse representation in dictionary
    /// Decryption algorithm explained:
    /// * When service it prepares binary tree: https://en.wikipedia.org/wiki/Morse_code
    /// * When Decrypt is called it iterates through the text and walk through binary tree to look for character
    /// General rules:
    /// * Characters are separated by space
    /// * Words are separated by 2 spaces
    /// </summary>
    public class MorseCodeCryptService : ICryptService
    {
        private Node decryptionTree;
        private IDictionary<char, string> encryptionDictionary = new Dictionary<char, string>()
        {
            { 'a', ".-" },
            { 'b', "-..." },
            { 'c', "-.-." },
            { 'd', "-.." },
            { 'e', "." },
            { 'f', "..-." },
            { 'g', "--." },
            { 'h', "...." },
            { 'i', ".." },
            { 'j', ".---" },
            { 'k', "-.-" },
            { 'l', ".-.." },
            { 'm', "--" },
            { 'n', "-." },
            { 'o', "---" },
            { 'p', ".--." },
            { 'q', "--.-" },
            { 'r', ".-." },
            { 's', "..." },
            { 't', "-" },
            { 'u', "..-" },
            { 'v', "...-" },
            { 'w', ".--" },
            { 'x', "-..-" },
            { 'y', "-.--" },
            { 'z', "--.." },
            { '1', ".----" },
            { '2', "..---" },
            { '3', "...--" },
            { '4', "....-" },
            { '5', "....." },
            { '6', "-...." },
            { '7', "--..." },
            { '8', "---.." },
            { '9', "----." },
            { '0', "-----" },
            { '=', "-...-" },
            { '/', "-..-." },
            { '?', "..--.." },
            { '_', "..--.-" },
            { '"', ".-..-." },
            { '.', ".-.-.-" },
            { '+', ".-.-." },
            { '@', ".--.-." },
            { '\'', ".----." },
            { '-', "-....-" },
            { ';', "-.-.-." },
            { '!', "-.-.--" },
            { ',', "--..--" },
            { ':', "---..." }
        };

        /// <summary>
        /// Creates new instance of the <see cref="MorseCodeCryptService"/> class.
        /// It creates decryption binary tree here (it is no problem since it is singleton class)
        /// </summary>
        public MorseCodeCryptService()
        {
            this.InitializeDecryption();
        }

        private enum DecryptionState
        {
            Starting,
            Processing,
            WordSeparated,
            FirstDelimiter,
        }

        /// <summary>
        /// Gets morse code type name
        /// </summary>
        public string Type => "Morse code";

        /// <summary>
        /// Decrypts text from morse code to ASCII
        /// for details and algorithm explanation see XML doc of <see cref="MorseCodeCryptService"/> class
        /// </summary>
        /// <param name="text">text to decrypt</param>
        /// <returns>decrypted text</returns>
        public string Decrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            Node currentNode = this.decryptionTree;
            DecryptionState state = DecryptionState.Starting;
            foreach (char character in text)
            {
                switch (character)
                {
                    case ' ':
                    case '\n':
                    case '\r':
                    case '\t':
                        AppendIfPossible(result, currentNode, state);

                        currentNode = this.decryptionTree;
                        if (state == DecryptionState.FirstDelimiter)
                        {
                            result.Append(' ');
                            state = DecryptionState.WordSeparated;
                        }
                        else if (state == DecryptionState.Processing)
                        {
                            state = DecryptionState.FirstDelimiter;
                        }

                        break;
                    case '.':
                        currentNode = currentNode.Dot;
                        state = DecryptionState.Processing;
                        break;
                    case '-':
                        currentNode = currentNode.Dash;
                        state = DecryptionState.Processing;
                        break;
                    default:
                        throw new InvalidOperationException($"Input is invalid.");
                }
            }

            AppendIfPossible(result, currentNode, state);

            return result.ToString();
        }

        /// <summary>
        /// Encrypts text into morse code from ASCII
        /// for details and algorithm explanation see XML doc of <see cref="MorseCodeCryptService"/> class
        /// </summary>
        /// <param name="text">text to encrypt</param>
        /// <returns>encrypted text</returns>
        /// <exception cref="InvalidOperationException">When there is invalid input. 
        /// If it is possible it attaches result in the end of the message</exception>
        public string Encrypt(string text)
        {
            List<string> words = new List<string>();
            List<string> lastWord = new List<string>();
            var errorMessage = string.Empty;
            var hasError = false;

            // for simplification lets replace new line and tab into space
            Regex pattern = new Regex(@"[\t\r\n]");
            foreach (char character in pattern.Replace(text, " ").ToLower())
            {
                if (character == ' ')
                {
                    if (lastWord.Count > 0)
                    {
                        words.Add(string.Join(" ", lastWord));
                    }

                    lastWord.Clear();
                }
                else
                {
                    if (this.encryptionDictionary.ContainsKey(character))
                    {
                        lastWord.Add(this.encryptionDictionary[character]);
                    }
                    else
                    {
                        if (!hasError)
                        {
                            errorMessage = $"Cannot translate some characters into morse code, check the output.";
                        }

                        hasError = true;
                        lastWord.Add(character.ToString());
                    }
                }
            }

            words.Add(string.Join(" ", lastWord));
            var result = string.Join("  ", words);
            if (!hasError)
            {
                return result;
            }
            else
            {
                throw new InvalidOperationException($"{errorMessage}<<{result}>>");
            }
        }

        private static void AppendIfPossible(StringBuilder result, Node currentNode, DecryptionState state)
        {
            if (currentNode.Char.HasValue)
            {
                result.Append(currentNode.Char.Value);
            }
            else if (state == DecryptionState.Processing)
            {
                throw new InvalidOperationException("Invalid morse code input");
            }
        }

        private void InitializeDecryption()
        {
            var decriptionDictionary = this.encryptionDictionary.ToDictionary(x => x.Value, x => x.Key);
            this.decryptionTree = new Node(null);
            this.WalkTree(decriptionDictionary, this.decryptionTree, 6, string.Empty);
        }

        private void WalkTree(Dictionary<string, char> decryptionDictionary, Node node, int depth, string currentText)
        {
            if (decryptionDictionary.ContainsKey(currentText))
            {
                node.Char = decryptionDictionary[currentText];
            }

            if (depth == 0)
            {
                node.Dash = node;
                node.Dot = node;
            }
            else
            {
                var dot = new Node(null);
                var dash = new Node(null);
                node.Dot = dot;
                node.Dash = dash;
                this.WalkTree(decryptionDictionary, dot, depth - 1, $"{currentText}.");
                this.WalkTree(decryptionDictionary, dash, depth - 1, $"{currentText}-");
            }
        }

        private class Node
        {
            public Node(char? character)
            {
                this.Char = character;
            }

            public char? Char { get; set; }

            public Node Dot { get; set; }

            public Node Dash { get; set; }
        }
    }
}
