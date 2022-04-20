using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KR_Cryptography
{
    public class Program
    {
        static string Encryption(string plainText)
        {
            try
            {
                string encryptedText = "";
                string publicKey = "12345678";
                string secretKey = "87654321";
                byte[] publicKeyByte = Array.Empty<byte>();
                publicKeyByte = Encoding.UTF8.GetBytes(publicKey);
                byte[] secretKeyByte = Array.Empty<byte>();
                secretKeyByte = Encoding.UTF8.GetBytes(secretKey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);
                using (DESCryptoServiceProvider des = new())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publicKeyByte, secretKeyByte), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    encryptedText = Convert.ToBase64String(ms.ToArray());
                }
                return encryptedText;
            }
            catch (Exception ex)
            {
                throw new Exception("Error!", ex.InnerException);
            }
        }
		
		static string Decryption(string cipherText)
        {
            try
            {
                string plainText = "";
                string publicKey = "12345678";
                string secretKey = "87654321";
                byte[] publicKeyByte = Array.Empty<byte>();
                publicKeyByte = Encoding.UTF8.GetBytes(publicKey);
                byte[] secretKeyByte = Array.Empty<byte>();
                secretKeyByte = Encoding.UTF8.GetBytes(secretKey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputByteArray = new byte[cipherText.Replace(" ", "+").Length];
                inputByteArray = Convert.FromBase64String(cipherText.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publicKeyByte, secretKeyByte), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    plainText = encoding.GetString(ms.ToArray());
                }
                return plainText;
            }
            catch (Exception ex)
            {
                throw new Exception("Error!", ex.InnerException);
            }
        }
        public static void Main(string[] args)
        {
			string plainText = "";
			
			while(true) {
			Console.WriteLine("Make a choice:");
			Console.WriteLine("1 - Type a plain text to encrypt");
			Console.WriteLine("2 - Decrypt the encrypted text");
			Console.WriteLine("3 - Exit");
			Console.WriteLine();
			int choice = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine();
				
				switch(choice)
				{
					case 1:
						Console.WriteLine("Type a plain text here");
						plainText = Console.ReadLine();
						Console.WriteLine();
						Console.WriteLine(plainText + " -> " + Encryption(plainText));
						Console.WriteLine();
						break;
					case 2:
						Console.WriteLine("Decrypting the cipher text...");
						Console.WriteLine();
						Console.WriteLine(Encryption(plainText) + " -> " + Decryption(Encryption(plainText)));
						Console.WriteLine();
						break;															  
					case 3:
						return;
					default:
						Console.WriteLine("Error, wrong number!");
						Console.WriteLine();
						break;
				}
			}
        }
    }
}