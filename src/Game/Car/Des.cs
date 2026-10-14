using System;
using System.IO;
using System.Security.Cryptography;

namespace Car
{
	public class Des
	{
		private DESCryptoServiceProvider des;

		private byte[] key = new byte[8] { 114, 9, 144, 66, 203, 3, 205, 74 };

		public Des()
		{
			des = new DESCryptoServiceProvider();
			des.Padding = PaddingMode.Zeros;
			des.Mode = CipherMode.ECB;
			byte[] iV = new byte[8];
			des.IV = iV;
			des.Key = key;
		}

		public byte[] Decrypt(byte[] encryptByte, int Positon, int length)
		{
			if (encryptByte == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(encryptByte, Positon, length);
			cryptoStream.FlushFinalBlock();
			cryptoStream.Close();
			byte[] result = memoryStream.ToArray();
			memoryStream.Close();
			return result;
		}

		public byte[] Decrypt(byte[] encryptByte)
		{
			return Decrypt(encryptByte, 0, encryptByte.Length);
		}

		public byte[] Encrypt(byte[] originalByte)
		{
			return Encrypt(originalByte, 0, originalByte.Length);
		}

		public byte[] Encrypt(byte[] originalByte, int iOffset, int Length)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
				if (originalByte.Length < Length)
				{
					Length = originalByte.Length;
				}
				if (originalByte.Length < iOffset)
				{
					iOffset = 0;
				}
				cryptoStream.Write(originalByte, iOffset, Length);
				cryptoStream.FlushFinalBlock();
				cryptoStream.Close();
				byte[] result = memoryStream.ToArray();
				memoryStream.Close();
				return result;
			}
			catch
			{
				return null;
			}
		}

		public void SetKey(byte[] newkey, int potion)
		{
			if (newkey != null && newkey.Length >= 8)
			{
				Buffer.BlockCopy(newkey, potion, key, 0, 8);
			}
		}
	}
}
