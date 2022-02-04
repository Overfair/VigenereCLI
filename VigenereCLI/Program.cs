using System;

namespace VigenereCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] alphabet = new char[27, 27];
            SeedAlphabet(alphabet);
            bool continueStatus = true;
            while (continueStatus)
            {
                Console.Write("Encrypt - 1\n" +
                    "Decrypt - 2\n" +
                    "Exit - 0\n" +
                    "Your Input: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.Write("\nMessage: ");
                        string messageToEncrypt = Console.ReadLine().ToUpper();
                        Console.Write("Key: ");
                        string keyForEncryption = Console.ReadLine().ToUpper();
                        keyForEncryption = KeyGen(messageToEncrypt.Length, keyForEncryption);
                        string encryptedMessage = Encrypt(messageToEncrypt, keyForEncryption, alphabet);
                        Console.WriteLine("Encrypted message: " + encryptedMessage);
                        break;
                    case "2":
                        Console.Write("\nMessage: ");
                        string messageToDecrypt = Console.ReadLine().ToUpper();
                        Console.Write("Key: ");
                        string keyForDecryption = Console.ReadLine().ToUpper();
                        keyForDecryption = KeyGen(messageToDecrypt.Length, keyForDecryption);
                        string decryptedMessage = Decrypt(messageToDecrypt, keyForDecryption, alphabet);
                        Console.WriteLine("Decrypted message: " + decryptedMessage);
                        break;
                    case "0":
                        continueStatus = false;
                        break;
                }
                Console.WriteLine();

            }
        }

        static void SeedAlphabet(char[,] table)
        {
            for (int i = 1; i <= 26; i++)
                table[i, 1] = table[1, i] = Convert.ToChar('A' + i - 1);

            for (int i = 2; i <= 26; i++)
                for (int j = 2; j <= 26; j++)
                    if (table[i - 1, j].Equals('Z'))
                        table[i, j] = 'A';
                    else
                        table[i, j] = table[j, i] = Convert.ToChar(table[i - 1, j] + 1);
        }

        static string KeyGen(int messageLength, string key)
        {
            while (key.Length < messageLength)
            {
                if (key.Length < messageLength - key.Length)
                    key += key;
                else key += key.Substring(0, messageLength - key.Length);
            }

            return key;
        }

        static int[] GetPos(string str)
        {
            char[] strChar = str.ToCharArray();
            int[] pos = new int[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                pos[i] = strChar[i] - 'A' + 1;
            }

            return pos;
        }

        static string Encrypt(string message, string key, char[,] table)
        {
            string cipher = null;

            int[] messagePos = GetPos(message);
            int[] keyPos = GetPos(key);

            for (int i = 0; i < keyPos.Length; i++)
            {
                cipher += table[keyPos[i], messagePos[i]];
            }
            return cipher;
        }

        static string Decrypt(string message, string key, char[,] table)
        {
            string cipher = null;

            char[] messageChar = message.ToCharArray();
            int[] keyPos = GetPos(key);

            for (int i = 0; i < keyPos.Length; i++)
            {
                for (int j = 1; j <= 26; j++)
                {
                    if (table[keyPos[i], j].Equals(messageChar[i]))
                    {
                        cipher += table[1, j];
                        break;
                    }
                }
            }
            return cipher;
        }
    }
}
