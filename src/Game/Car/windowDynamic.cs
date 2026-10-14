using System;
using System.IO;
using System.Runtime.InteropServices;
using Global;

namespace Car
{
	public class windowDynamic
	{
		private DynamicsDogInData_1_2 dogDataInput;

		private static windowDynamic _instance;

		private int inputSize;

		private IntPtr orgPosion;

		private byte[] buffIn;

		public static windowDynamic Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new windowDynamic();
				}
				return _instance;
			}
		}

		private windowDynamic()
		{
			inputSize = Marshal.SizeOf(dogDataInput);
			orgPosion = Marshal.AllocHGlobal(inputSize);
			buffIn = new byte[inputSize];
		}

		public void windows_DynaicInDog(DynamicData caculatState, DynamicData data)
		{
			dogDataInput.engineSpeed = caculatState.engineSpeed;
			dogDataInput.carSpeed = caculatState.carSpeed;
			dogDataInput.oil = caculatState.oil;
			dogDataInput.clutch = caculatState.clutch;
			dogDataInput.gear = caculatState.gear;
			if (caculatState.engineIsFired)
			{
				dogDataInput.engineIsFired = 1;
			}
			else
			{
				dogDataInput.engineIsFired = 0;
			}
			dogDataInput.footBrake = caculatState.footBrake;
			dogDataInput.handBrake = caculatState.handBrake;
			dogDataInput.maxFootBrake = 15000f;
			dogDataInput.maxHandBrake = 7500f;
			dogDataInput.curIg = caculatState.curIg;
			dogDataInput.carAA = -1E-06f;
			dogDataInput.carBB = 0.005729f;
			dogDataInput.carCC = 129.973f;
			dogDataInput.carNp = 5600f;
			dogDataInput.carTtqMax = 140f;
			dogDataInput.carRt = 0.311651f;
			dogDataInput.carIO = 3.83649f;
			dogDataInput.carEtaT = 0.92f;
			dogDataInput.carAdhere = DataCenter.Instance.mAdhere;
			dogDataInput.carMM = 1504f;
			dogDataInput.carCd = 0.35f;
			dogDataInput.carAirA = 2.0134f;
			dogDataInput.carGrade = 0f;
			dogDataInput.carRollF1 = 0.02f;
			dogDataInput.carRollF2 = 5.6E-05f;
			dogDataInput.carRollF3 = 0f;
			Marshal.StructureToPtr(dogDataInput, orgPosion, fDeleteOld: true);
			Marshal.Copy(orgPosion, buffIn, 0, inputSize);
			DynamicInDog(buffIn, inputSize);
			data.pullForce = 0f;
			data.fResist = 0f;
			data.fBrake = 0f;
			data.fResist01 = 0f;
			if (buffIn != null && buffIn.Length != 0)
			{
				BinaryReader binaryReader = new BinaryReader(new MemoryStream(buffIn));
				if (binaryReader.ReadInt32() == 2 && binaryReader.BaseStream.Length >= 20)
				{
					data.pullForce = binaryReader.ReadSingle();
					data.fResist = binaryReader.ReadSingle();
					data.fBrake = binaryReader.ReadSingle();
					data.fResist01 = binaryReader.ReadSingle();
				}
				binaryReader.Close();
			}
		}

		public void windows_Close()
		{
			Close();
		}

		[DllImport("Win_DynamicDll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void DynamicInDog(byte[] datain, int len);

		[DllImport("Win_DynamicDll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void Close();
	}
}
