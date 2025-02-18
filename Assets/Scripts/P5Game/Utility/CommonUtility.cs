using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtility : MonoBehaviour
{
    public static byte[] XorByte(byte[] bt,int incode)
    {
        string publicKey = "test";

        byte[] retByte = new byte[bt.Length];
        int publicKeyIndex = 0;

        for (int i = 0; i < bt.Length; i++)
        {
            byte val = bt[i];
            int index = publicKeyIndex++ % publicKey.Length;
            val ^= (byte)publicKey[index];
            retByte[i] = (byte)(val ^ (byte)incode);
        }
        return retByte;
    }
}
