namespace Car
{
	public class JoyOriginalData
	{
		public uint dwSize;

		public uint dwFlags;

		public uint dwXpos;

		public uint dwYpos;

		public uint dwZpos;

		public uint dwRpos;

		public uint dwUpos;

		public uint dwVpos;

		public uint dwButtons;

		public uint dwButtonNumber;

		public uint dwPOV;

		public uint dwReserved1;

		public uint dwReserved2;

		public bool[] btnStateArray = new bool[32];

		public JoyOriginalData GetCirJoyOriData(byte[] readBuffer)
		{
			JoyOriginalData joyOriginalData = new JoyOriginalData();
			joyOriginalData.dwSize = 64u;
			joyOriginalData.dwFlags = 0u;
			joyOriginalData.dwXpos = (uint)(readBuffer[0] * 65535) / 255u;
			joyOriginalData.dwYpos = (uint)(readBuffer[1] * 65535) / 255u;
			joyOriginalData.dwZpos = (uint)(readBuffer[2] * 65535) / 255u;
			joyOriginalData.dwRpos = 0u;
			joyOriginalData.dwUpos = 0u;
			joyOriginalData.dwButtons = (uint)(readBuffer[4] + readBuffer[5] * 256);
			switch (readBuffer[6])
			{
			case 0:
				joyOriginalData.dwPOV = 0u;
				break;
			case 64:
				joyOriginalData.dwPOV = 18000u;
				break;
			case 96:
				joyOriginalData.dwPOV = 27000u;
				break;
			case 32:
				joyOriginalData.dwPOV = 9000u;
				break;
			case 129:
				joyOriginalData.dwButtons += 65536u;
				break;
			default:
				joyOriginalData.dwPOV = 65535u;
				break;
			}
			uint num = 1u;
			for (int i = 0; i < 32; i++)
			{
				if ((joyOriginalData.dwButtons & num) != 0)
				{
					joyOriginalData.btnStateArray[i] = true;
				}
				else
				{
					joyOriginalData.btnStateArray[i] = false;
				}
				num <<= 1;
			}
			return joyOriginalData;
		}
	}
}
