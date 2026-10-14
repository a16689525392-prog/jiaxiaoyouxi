using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Car
{
	public class hidControl
	{
		private static ushort ifHaveFindUsb = 0;

		private static byte[] _buffer = new byte[65];

		private static AndroidJavaObject jo = null;

		public static void _GetMoniDevice(int usbFileDesriptor, int inEndPointAddress, int outEndPointAddress, int pid)
		{
			ifHaveFindUsb = (ushort)pid;
			SignalSourceCtl.Instance.cbSignalReader.ReadKeyFile($"{ifHaveFindUsb:X0000}");
		}

		private static AndroidJavaObject GetAndroidObject()
		{
			if (jo == null)
			{
				jo = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			}
			return jo;
		}

		public static void AndroidStartFindUsb()
		{
			GetAndroidObject().Call("FindUsbDevice");
		}

		private static void Close()
		{
			GetAndroidObject().Call("Close");
		}

		public static bool StartUsb()
		{
			if (ifHaveFindUsb == 0)
			{
				ifHaveFindUsb = _Start();
			}
			return ifHaveFindUsb > 0;
		}

		public static void CloseUsb()
		{
			_End();
		}

		public static bool TranslateWithUSB(byte[] buffer, OperUsb oper = OperUsb.READ, int Flage = 0)
		{
			bool result = false;
			int num = 1;
			int num2 = 0;
			if (buffer == null)
			{
				return result;
			}
			if (oper == OperUsb.READ)
			{
				num2 = read(_buffer, Flage);
				Buffer.BlockCopy(_buffer, num, buffer, 0, 64);
			}
			else
			{
				Buffer.BlockCopy(buffer, 0, _buffer, num, 64);
				num2 = write(_buffer, Flage);
			}
			return num2 > 0;
		}

		public static bool GetUsbFlage()
		{
			return ifHaveFindUsb > 0;
		}

		public static ushort GetPid()
		{
			return ifHaveFindUsb;
		}

		public static void ResetUsb()
		{
			if (ifHaveFindUsb > 0)
			{
				ifHaveFindUsb = 0;
			}
		}

		[DllImport("hidControl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Start")]
		private static extern ushort _Start();

		[DllImport("hidControl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "End")]
		private static extern void _End();

		[DllImport("hidControl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "UsbOut")]
		private static extern int write(byte[] buff, int Flage);

		[DllImport("hidControl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "UsbIn")]
		private static extern int read(byte[] buff, int Flage);
	}
}
