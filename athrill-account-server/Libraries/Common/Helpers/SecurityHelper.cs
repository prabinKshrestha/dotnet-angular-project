using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AT.Common.Helpers
{
    public static class SecurityHelper
    {
        public static string GetHashPassword(string password, string saltkey)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] bytePassword = uEncode.GetBytes(password + saltkey);
            SHA512Managed sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(bytePassword);
            return Convert.ToBase64String(hash);
        }
        public static string Encrypt(string source, string key)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(source);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        public static string Decrypt(string encrypted, string key)
        {
            encrypted = encrypted.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(encrypted);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    return Encoding.Unicode.GetString(ms.ToArray());
                }
            }
        }
        //public static string Encrypt(string source, string key)
        //{
        //    if(key.Length != 32)
        //    {
        //        throw new ApplicationException("Encryption or Decryption Key should have exacclty 32 characters.");
        //    }
        //    byte[] _key = Encoding.UTF8.GetBytes(key);

        //    using (var aes = Aes.Create())
        //    {
        //        using (var encryptor = aes.CreateEncryptor(_key, aes.IV))
        //        {
        //            using (var ms = new MemoryStream())
        //            {
        //                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        //                {
        //                    using (var sw = new StreamWriter(cs))
        //                    {
        //                        sw.Write(source);
        //                    }
        //                }

        //                byte[] iv = aes.IV;
        //                byte[] encrypted = ms.ToArray();
        //                byte[] result = new byte[iv.Length + encrypted.Length];

        //                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        //                Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);

        //                return Convert.ToBase64String(result);
        //            }
        //        }
        //    }
        //}

        //public static string Decrypt(string encrypted, string key)
        //{
        //    if (key.Length != 32)
        //    {
        //        throw new ApplicationException("Encryption or Decryption Key should have exacclty 32 characters.");
        //    }
        //    var _key = Encoding.UTF8.GetBytes(key);

        //    byte[] base64 = Convert.FromBase64String(encrypted);

        //    byte[] iv = new byte[16];
        //    byte[] cipher = new byte[16];

        //    Buffer.BlockCopy(base64, 0, iv, 0, iv.Length);
        //    Buffer.BlockCopy(base64, iv.Length, cipher, 0, cipher.Length);

        //    using (var aes = Aes.Create())
        //    {
        //        using (var decryptor = aes.CreateDecryptor(_key, iv))
        //        {
        //            var result = string.Empty;
        //            using (var ms = new MemoryStream(cipher))
        //            {
        //                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
        //                {
        //                    using (var sr = new StreamReader(cs))
        //                    {
        //                        result = sr.ReadToEnd();
        //                    }
        //                }
        //            } 
        //            return result;
        //        }
        //    }
        //}
    }
}
