using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Global;
using UnityEngine;
using View;

namespace Car
{
	public class ReadLockSignal : RwSignal
	{
		public bool isGetLock;

		private BinaryWriter writeBufferBw;

		private byte[] writeBuffer = new byte[64];

		private byte[] readBuffer = new byte[64];

		private int size;

		private bool isReadLockFile;

		private BinaryReader readBufferBr;

		private CarSignal tempSignal;

		private float handbreakTime = 1f;

		private float widthLightTime = 1f;

		private float beltTime = 1f;

		private float rainBrushTime = 1f;

		private float doorTime = 1f;

		private float hornTime = 1f;

		private float fireTime = 1f;

		private float powerTime = 1f;

		private float fogLightTime = 1f;

		private float turnLightTime = 1f;

		private float frontLightTime = 1f;

		private List<LockCalibrationData> lockCalList = new List<LockCalibrationData>();

		private byte[] Key = new byte[128]
		{
			175, 4, 250, 163, 204, 157, 219, 123, 87, 15,
			51, 126, 142, 142, 18, 211, 133, 255, 22, 90,
			151, 45, 126, 90, 117, 133, 209, 53, 160, 69,
			241, 126, 200, 198, 205, 93, 227, 16, 99, 244,
			80, 109, 53, 236, 249, 173, 89, 228, 17, 177,
			223, 120, 145, 124, 7, 235, 189, 246, 215, 15,
			208, 110, 168, 182, 52, 136, 237, 147, 6, 166,
			112, 16, 112, 48, 21, 254, 53, 106, 28, 175,
			212, 255, 1, 102, 72, 116, 122, 236, 50, 251,
			221, 252, 99, 10, 26, 17, 134, 67, 178, 186,
			53, 202, 223, 85, 12, 247, 137, 212, 197, 187,
			52, 146, 193, 205, 236, 139, 166, 115, 129, 199,
			111, 166, 188, 151, 184, 57, 224, 68
		};

		public ReadLockSignal()
		{
			readBufferBr = new BinaryReader(new MemoryStream(readBuffer));
		}

		public CarSignal UpdateLockSignal()
		{
			size = Joystick(readBuffer);
			if (size == 56)
			{
				SignalSourceCtl.Instance.joyOriData = GetJoyOriData(readBuffer);
				if (!isReadLockFile && SignalSourceCtl.Instance.Pid != "")
				{
					SettingInfoCtl.Instance.ReadSettingFile();
					ReadLockCalFile();
					if (SignalSourceCtl.Instance.Pid == "4004")
					{
						CarSignalValue.SteerWheeMaxlAngle = 450f;
					}
					isReadLockFile = true;
				}
				if (lockCalList.Count == 30)
				{
					AnalysisGameProtocolSignal();
					GetLockDynamicData();
				}
			}
			else
			{
				isReadLockFile = false;
			}
			return tempSignal;
		}

		private void GetLockDynamicData()
		{
			if (!(CarState.Instance.CurCar != null))
			{
				return;
			}
			DynamicData caculatState = MoNiCarDynamic.Instance.TransfromGeneralFormat(CarState.Instance);
			if (MoNiCarDynamic.Instance.dynamicData.engineIsFired)
			{
				if (MoNiCarDynamic.Instance.dynamicData.gear == 1 || MoNiCarDynamic.Instance.dynamicData.gear == 6 || MoNiCarDynamic.Instance.dynamicData.gear == 0 || Mathf.Abs(MoNiCarDynamic.Instance.dynamicData.carSpeed) < 6f)
				{
					MoNiCarDynamic.Instance.DynamicCaculateLocal();
					return;
				}
				windowDynamic.Instance.windows_DynaicInDog(caculatState, MoNiCarDynamic.Instance.dynamicData);
				MoNiCarDynamic.Instance.DynamicCaculate();
			}
			else
			{
				MoNiCarDynamic.Instance.dynamicData.pullForce = 0f;
				MoNiCarDynamic.Instance.dynamicData.fResist = 0f;
				MoNiCarDynamic.Instance.dynamicData.fBrake = 0f;
				MoNiCarDynamic.Instance.dynamicData.fResist01 = 0f;
				MoNiCarDynamic.Instance.DynamicCaculate();
			}
		}

		public JoyOriginalData GetJoyOriData(byte[] ReadBuff)
		{
			readBuffer = ReadBuff;
			JoyOriginalData joyOriginalData = new JoyOriginalData();
			readBufferBr.BaseStream.Position = 0L;
			SignalSourceCtl.Instance.Pid = readBufferBr.ReadUInt16().ToString("X2");
			SignalSourceCtl.Instance.Vid = readBufferBr.ReadUInt16().ToString("X2");
			joyOriginalData.dwSize = readBufferBr.ReadUInt32();
			joyOriginalData.dwFlags = readBufferBr.ReadUInt32();
			joyOriginalData.dwXpos = readBufferBr.ReadUInt32();
			joyOriginalData.dwYpos = readBufferBr.ReadUInt32();
			joyOriginalData.dwZpos = readBufferBr.ReadUInt32();
			joyOriginalData.dwRpos = readBufferBr.ReadUInt32();
			joyOriginalData.dwUpos = readBufferBr.ReadUInt32();
			joyOriginalData.dwVpos = readBufferBr.ReadUInt32();
			joyOriginalData.dwButtons = readBufferBr.ReadUInt32();
			joyOriginalData.dwButtonNumber = readBufferBr.ReadUInt32();
			joyOriginalData.dwPOV = readBufferBr.ReadUInt32();
			joyOriginalData.dwReserved1 = readBufferBr.ReadUInt32();
			joyOriginalData.dwReserved2 = readBufferBr.ReadUInt32();
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

		public void AnalysisGameProtocolSignal()
		{
			tempSignal.InitCarOtherSignal();
			tempSignal.steerWheel = 0f - GetJoySignalStatus(0);
			if (tempSignal.steerWheel > -0.005f)
			{
				tempSignal.steerWheel = GetJoySignalStatus(1);
			}
			tempSignal.cluth = GetJoySignalStatus(2);
			tempSignal.footBrake = GetJoySignalStatus(3);
			tempSignal.oil = GetJoySignalStatus(4);
			tempSignal.handBrake = GetHandbreak();
			tempSignal.gear = GetGear();
			tempSignal.frontLight = GetFrontLight();
			tempSignal.turnLight = GetTurnLight();
			tempSignal.fogLight = GetFogLight();
			tempSignal.power = GetPower();
			tempSignal.fire = GetFire();
			tempSignal.horn = GetHorn();
			tempSignal.rainBrush = GetRainBrush();
			tempSignal.door = GetDoor();
			tempSignal.safeBelt = GetSeatBelt();
			tempSignal.widthLight = GetWidthLight();
		}

		private float GetHandbreak()
		{
			float result = 0f;
			if (lockCalList[5].type == 1 || lockCalList[5].type == 10)
			{
				handbreakTime += Time.deltaTime;
				if (GetJoySignalStatus(5, CarState.Instance.CarSignal.handBrake == 1f, ref handbreakTime))
				{
					result = 1f;
				}
			}
			else
			{
				result = GetJoySignalStatus(5);
			}
			return result;
		}

		private int GetWidthLight()
		{
			int result = 0;
			widthLightTime += Time.deltaTime;
			if (GetJoySignalStatus(16, CarState.Instance.CarSignal.widthLight == 1, ref widthLightTime))
			{
				result = 1;
			}
			return result;
		}

		private int GetSeatBelt()
		{
			int result = 0;
			beltTime += Time.deltaTime;
			if (GetJoySignalStatus(29, CarState.Instance.CarSignal.safeBelt == 1, ref beltTime))
			{
				result = 1;
			}
			return result;
		}

		private int GetRainBrush()
		{
			int result = 0;
			rainBrushTime += Time.deltaTime;
			if (GetJoySignalStatus(27, CarState.Instance.CarSignal.rainBrush == 2, ref rainBrushTime))
			{
				result = 2;
				if (CarState.Instance.CarSignal.rainBrush != 2)
				{
					return result;
				}
			}
			if (GetJoySignalStatus(26, CarState.Instance.CarSignal.rainBrush == 1, ref rainBrushTime))
			{
				result = 1;
				_ = CarState.Instance.CarSignal.rainBrush;
				_ = 1;
				return result;
			}
			return result;
		}

		private int GetDoor()
		{
			int result = 0;
			doorTime += Time.deltaTime;
			if (GetJoySignalStatus(28, CarState.Instance.CarSignal.door == 1, ref doorTime))
			{
				result = 1;
			}
			return result;
		}

		private int GetHorn()
		{
			int result = 0;
			hornTime += Time.deltaTime;
			if (GetJoySignalStatus(23, CarState.Instance.CarSignal.horn == 1, ref hornTime))
			{
				result = 1;
			}
			return result;
		}

		private int GetFire()
		{
			int result = 0;
			fireTime += Time.deltaTime;
			if (GetJoySignalStatus(22, CarState.Instance.CarSignal.fire == 1, ref fireTime))
			{
				result = 1;
			}
			return result;
		}

		private int GetPower()
		{
			int result = 0;
			powerTime += Time.deltaTime;
			if (GetJoySignalStatus(21, CarState.Instance.CarSignal.power == 1, ref powerTime))
			{
				result = 1;
			}
			return result;
		}

		private int GetFogLight()
		{
			int result = 0;
			fogLightTime += Time.deltaTime;
			if (GetJoySignalStatus(25, CarState.Instance.CarSignal.fogLight == 1, ref fogLightTime))
			{
				result = 1;
			}
			return result;
		}

		private int GetTurnLight()
		{
			int result = 0;
			turnLightTime += Time.deltaTime;
			if (GetJoySignalStatus(24, CarState.Instance.CarSignal.turnLight == 3, ref turnLightTime))
			{
				result = 3;
				if (CarState.Instance.CarSignal.turnLight != 3)
				{
					return result;
				}
			}
			if (GetJoySignalStatus(19, CarState.Instance.CarSignal.turnLight == 1, ref turnLightTime))
			{
				result = 1;
				if (CarState.Instance.CarSignal.turnLight != 1)
				{
					return result;
				}
			}
			if (GetJoySignalStatus(20, CarState.Instance.CarSignal.turnLight == 2, ref turnLightTime))
			{
				result = 2;
				_ = CarState.Instance.CarSignal.turnLight;
				_ = 2;
				return result;
			}
			return result;
		}

		private int GetFrontLight()
		{
			int result = 0;
			frontLightTime += Time.deltaTime;
			if (GetJoySignalStatus(18, CarState.Instance.CarSignal.frontLight == 2, ref frontLightTime))
			{
				result = 2;
				if (CarState.Instance.CarSignal.frontLight != 2)
				{
					return result;
				}
			}
			if (GetJoySignalStatus(17, CarState.Instance.CarSignal.frontLight == 1, ref frontLightTime))
			{
				result = 1;
				_ = CarState.Instance.CarSignal.frontLight;
				_ = 1;
				return result;
			}
			return result;
		}

		private int GetGear()
		{
			int num = 0;
			if (SettingInfoCtl.Instance.isManualGear)
			{
				if (GetJoySignalStatus(6) == 1f)
				{
					return 1;
				}
				if (GetJoySignalStatus(7) == 1f)
				{
					return 2;
				}
				if (GetJoySignalStatus(8) == 1f)
				{
					return 3;
				}
				if (GetJoySignalStatus(9) == 1f)
				{
					return 4;
				}
				if (GetJoySignalStatus(10) == 1f)
				{
					return 5;
				}
				if (GetJoySignalStatus(11) == 1f)
				{
					return 6;
				}
				return 0;
			}
			if (GetJoySignalStatus(12) == 1f)
			{
				return 100;
			}
			if (GetJoySignalStatus(13) == 1f)
			{
				return 102;
			}
			if (GetJoySignalStatus(14) == 1f)
			{
				return 101;
			}
			if (GetJoySignalStatus(15) == 1f)
			{
				return 103;
			}
			return 104;
		}

		private float GetJoySignalStatus(int signalId)
		{
			JoyOriginalData joyOriData = SignalSourceCtl.Instance.joyOriData;
			LockCalibrationData lockCalibrationData = lockCalList[signalId];
			float num = 0f;
			switch (lockCalibrationData.type)
			{
			case 1:
				if (joyOriData.btnStateArray[lockCalibrationData.btnIndex])
				{
					return 1f;
				}
				return 0f;
			case 2:
				return Mathf.InverseLerp(lockCalibrationData.preValue, lockCalibrationData.curValue, joyOriData.dwXpos);
			case 3:
				return Mathf.InverseLerp(lockCalibrationData.preValue, lockCalibrationData.curValue, joyOriData.dwYpos);
			case 4:
				return Mathf.InverseLerp(lockCalibrationData.preValue, lockCalibrationData.curValue, joyOriData.dwZpos);
			case 6:
				return Mathf.InverseLerp(lockCalibrationData.preValue, lockCalibrationData.curValue, joyOriData.dwUpos);
			case 7:
				return Mathf.InverseLerp(lockCalibrationData.preValue, lockCalibrationData.curValue, joyOriData.dwVpos);
			case 8:
				return Mathf.InverseLerp(lockCalibrationData.preValue, lockCalibrationData.curValue, joyOriData.dwPOV);
			case 5:
				return Mathf.InverseLerp(lockCalibrationData.preValue, lockCalibrationData.curValue, joyOriData.dwRpos);
			case 9:
				return 0f;
			default:
				return 0f;
			}
		}

		private bool GetJoySignalStatus(int signalId, bool isOpen, ref float intervalTime)
		{
			JoyOriginalData joyOriData = SignalSourceCtl.Instance.joyOriData;
			LockCalibrationData lockCalibrationData = lockCalList[signalId];
			switch (lockCalibrationData.type)
			{
			case 1:
				if (joyOriData.btnStateArray[lockCalibrationData.btnIndex])
				{
					if (lockCalibrationData.isPersistentSignal)
					{
						isOpen = true;
					}
					else if (!lockCalibrationData.isPersistentSignal && intervalTime > 0.5f)
					{
						isOpen = !isOpen;
						intervalTime = 0f;
					}
				}
				else if (lockCalibrationData.isPersistentSignal)
				{
					isOpen = false;
				}
				break;
			case 10:
				if (joyOriData.dwPOV == lockCalibrationData.btnIndex)
				{
					if (lockCalibrationData.isPersistentSignal)
					{
						isOpen = true;
					}
					else if (!lockCalibrationData.isPersistentSignal && intervalTime > 0.5f)
					{
						isOpen = !isOpen;
						intervalTime = 0f;
					}
				}
				else if (lockCalibrationData.isPersistentSignal)
				{
					isOpen = false;
				}
				break;
			}
			return isOpen;
		}

		public bool JudgeLockConnect()
		{
			return JudgeLockConnect(Key);
		}

		public bool JudgeLockConnect(byte[] keyData)
		{
			byte[] array = null;
			byte[] array2 = new byte[128];
			int dataOutLen = 0;
			if (!isGetLock)
			{
				DogOpen();
			}
			if (DogRSADecrypt(keyData, array2, keyData.Length, ref dataOutLen) && dataOutLen > 0)
			{
				array = new byte[dataOutLen];
				Buffer.BlockCopy(array2, 0, array, 0, dataOutLen);
				DataCenter.Instance.UsbVersion = 224;
				isGetLock = true;
				if (DataCenter.Instance.SerialNumber == "")
				{
					byte[] array3 = new byte[8];
					DogGetID(array3, 8);
					if (array3 != null)
					{
						for (int i = 0; i < array3.Length; i++)
						{
							DataCenter.Instance.SerialNumber += $"{array3[i]:X2}";
						}
					}
				}
				Debug.Log("检测到加密锁");
			}
			else
			{
				isGetLock = false;
				DataCenter.Instance.SerialNumber = "";
				SignalSourceCtl.Instance.connectStatus = 0;
			}
			return isGetLock;
		}

		private void Init()
		{
			writeBufferBw = new BinaryWriter(new MemoryStream(writeBuffer));
		}

		public void ReadLockCalFile()
		{
			lockCalList.Clear();
			string text = Application.streamingAssetsPath + "/SettingFile/" + SignalSourceCtl.Instance.Pid + SignalSourceCtl.Instance.Vid + SettingInfoCtl.Instance.isManualGear + ".dat";
			if (File.Exists(text))
			{
				BinaryReader binaryReader = new BinaryReader(new FileInfo(text).OpenRead());
				for (int i = 0; i < 30; i++)
				{
					LockCalibrationData lockCalibrationData = new LockCalibrationData();
					lockCalibrationData.type = binaryReader.ReadInt32();
					lockCalibrationData.isPersistentSignal = binaryReader.ReadBoolean();
					lockCalibrationData.btnIndex = binaryReader.ReadInt32();
					lockCalibrationData.preValue = binaryReader.ReadInt32();
					lockCalibrationData.curValue = binaryReader.ReadInt32();
					lockCalList.Add(lockCalibrationData);
				}
				binaryReader.Close();
			}
			else
			{
				Debug.Log("没有找到加密锁标定数据");
				SignalSourceCtl.Instance.StartCoroutine(EnterMsgView());
			}
		}

		public void SaveLockCalFile(List<LockCalibrationData> lockCalInfoList)
		{
			BinaryWriter binaryWriter = new BinaryWriter(new FileInfo(Application.streamingAssetsPath + "/SettingFile/" + SignalSourceCtl.Instance.Pid + SignalSourceCtl.Instance.Vid + SettingInfoCtl.Instance.isManualGear + ".dat").OpenWrite());
			foreach (LockCalibrationData lockCalInfo in lockCalInfoList)
			{
				binaryWriter.Write(lockCalInfo.type);
				binaryWriter.Write(lockCalInfo.isPersistentSignal);
				binaryWriter.Write(lockCalInfo.btnIndex);
				binaryWriter.Write(lockCalInfo.preValue);
				binaryWriter.Write(lockCalInfo.curValue);
			}
			binaryWriter.Close();
		}

		private IEnumerator EnterMsgView()
		{
			yield return new WaitForSeconds(0.5f);
			ViewJumpCtl.Instance.EnterView(ViewType.MsgView, isDestroyCurView: false);
			ViewJumpCtl.Instance.curView.GetComponent<MsgView>().InitMsgView("当前方向盘没有标定\n是否跳转到标定界面?", "是", delegate
			{
				ViewJumpCtl.Instance.EnterView(ViewType.CarSelect);
				CarSelect.isGoBiaoDing = true;
			}, "否");
			yield return 0;
		}

		private void OnDestroy()
		{
			DogClose();
		}

		[DllImport("Win_DynamicDll")]
		private static extern bool DogRSADecrypt(byte[] dataIn, byte[] dataOut, int dataInLen, ref int dataOutLen);

		[DllImport("Win_DynamicDll")]
		public static extern bool DogGetID(byte[] dataInOut, int dataLen);

		[DllImport("Win_DynamicDll")]
		private static extern bool DogOpen();

		[DllImport("Win_DynamicDll")]
		private static extern void DogClose();

		[DllImport("Win_Joystick", CallingConvention = CallingConvention.Cdecl)]
		private static extern int Joystick(byte[] buff);

		[DllImport("Win_Joystick", CallingConvention = CallingConvention.Cdecl)]
		public static extern void JoyClose();
	}
}
