using System;
using System.Security.Cryptography;
using UnityEngine;

public static class AESKeyManager
{
    public static byte[] Key { get; private set; }
    public static byte[] IV  { get; private set; }

    public static void Init()
    {
        if (PlayerPrefs.HasKey("AES_KEY"))
        {
            Key = Convert.FromBase64String(PlayerPrefs.GetString("AES_KEY"));
            IV  = Convert.FromBase64String(PlayerPrefs.GetString("AES_IV"));
        }
        else
        {
            Key = Generate(32);
            IV  = Generate(16);
            PlayerPrefs.SetString("AES_KEY", Convert.ToBase64String(Key));
            PlayerPrefs.SetString("AES_IV", Convert.ToBase64String(IV));
            PlayerPrefs.Save();
        }
    }

    private static byte[] Generate(int size)
    {
        byte[] data = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(data);
        return data;
    }
}