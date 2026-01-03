using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class AESUtility
{
    public static byte[] Encrypt(string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = AESKeyManager.Key;
        aes.IV = AESKeyManager.IV;

        using var encryptor = aes.CreateEncryptor();
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }
        return ms.ToArray();
    }

    public static string Decrypt(byte[] cipherData)
    {
        using Aes aes = Aes.Create();
        aes.Key = AESKeyManager.Key;
        aes.IV = AESKeyManager.IV;

        using var decryptor = aes.CreateDecryptor();
        using var ms = new MemoryStream(cipherData);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}