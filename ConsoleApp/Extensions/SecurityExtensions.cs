using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Extensions
{
    public static class SecurityExtensions
    {
        //public static string Encrypt(this string text)
        //{
        //    var fileName = Guid.NewGuid().ToString();
        //    using (FileStream fileStream = new($"{fileName}.ibh", FileMode.OpenOrCreate))
        //    {
        //        using (Aes aes = Aes.Create())
        //        {
        //            byte[] key =
        //            {
        //                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
        //                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        //            };
        //            aes.Key = key;

        //            byte[] iv = aes.IV;
        //            fileStream.Write(iv, 0, iv.Length);

        //            using (CryptoStream cryptoStream = new(
        //                fileStream,
        //                aes.CreateEncryptor(),
        //                CryptoStreamMode.Write))
        //            {
        //                // By default, the StreamWriter uses UTF-8 encoding.
        //                // To change the text encoding, pass the desired encoding as the second parameter.
        //                // For example, new StreamWriter(cryptoStream, Encoding.Unicode).
        //                using (StreamWriter encryptWriter = new(cryptoStream))
        //                {
        //                    encryptWriter.WriteLine(text);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
