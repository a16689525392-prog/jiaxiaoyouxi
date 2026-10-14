using System;
using System.IO;
using System.Runtime.InteropServices;
using Global;
using Scene;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Car
{
	public class ReadCircuitBoardSignal : RwSignal
	{
		private BinaryWriter writeBufferBw;

		private BinaryReader readBufferBr;

		[HideInInspector]
		public byte[] DesEncrystRsaKeyData = new byte[2048];

		[HideInInspector]
		public byte[] ENCRY_XINHAO_DATA = new byte[2432];

		[HideInInspector]
		public byte[] RSAENCRY_DESKEY_DATA = new byte[128];

		private TextAsset textAsset;

		private byte[] ReadBuffer = new byte[64];

		private byte[] WriteBuffer = new byte[64];

		private int preSignalIndex;

		private int sameSignalCount;

		private bool isPrintErrorData;

		private CarSignal readSignal;

		public bool isDisConnectEvent;

		private bool isFirst = true;

		private byte[] otherSignalData = new byte[4];

		private uint othersignalData2;

		private CarSignal carSignal;

		private byte[] PreSignalData16To28 = new byte[13];

		private float widthLightIntervalTime;

		private int preWidthLightState;

		private byte[] UsedWriteBuffer = new byte[64];

		private byte[] RecvDescryData = new byte[112];

		private bool RunFirst = true;

		private byte Flage = 224;

		private StreamWriter sw;

		private byte[] ReadTempBuffer = new byte[64];

		private ushort writeIndex;

		private const int MaxValue = 65535;

		private DynamicData dynamicData = new DynamicData();

		private int specialDataCount;

		private byte[] CurrDescryData = new byte[128];

		private Des des = new Des();

		private byte[] rsaDescryKey = new byte[8];

		private byte[] xinhaoDescryKey = new byte[8];

		private int keyIndex = -1;

		public bool isDescryptOver;

		private bool haveBeginDescryXinHao;

		public bool JudgeCircuitConnect()
		{
			if (hidControl.GetUsbFlage())
			{
				writeBufferBw = new BinaryWriter(new MemoryStream(WriteBuffer));
				readBufferBr = new BinaryReader(new MemoryStream(ReadBuffer));
				return true;
			}
			isDescryptOver = false;
			SignalSourceCtl.Instance.connectStatus = 0;
			return false;
		}

		public void ReadKeyFile(string type)
		{
			SignalSourceCtl.Instance.Pid = type;
			if (SignalSourceCtl.Instance.Pid == "4200")
			{
				CarSignalValue.SteerWheeMaxlAngle = 450f;
			}
			else if (SignalSourceCtl.Instance.Pid == "400A")
			{
				CarSignalValue.SteerWheeMaxlAngle = 540f;
			}
			else
			{
				CarSignalValue.SteerWheeMaxlAngle = 540f;
			}
			SettingInfoCtl.Instance.ReadSettingFile();
			textAsset = Resources.Load<TextAsset>("KeyFile/" + type);
			Buffer.BlockCopy(textAsset.bytes, 0, DesEncrystRsaKeyData, 0, 2048);
			Buffer.BlockCopy(textAsset.bytes, 2048, ENCRY_XINHAO_DATA, 0, 2432);
			Buffer.BlockCopy(textAsset.bytes, 4480, RSAENCRY_DESKEY_DATA, 0, 128);
		}

		public void ReadAndWriteData()
		{
			ReadData(ReadBuffer);
			SceneTextShow.Instance.FrameNumber = GetByteValue(ReadBuffer[10], ReadBuffer[11]);
			if (preSignalIndex != ReadBuffer[10] * 256 + ReadBuffer[11] && isPrintErrorData)
			{
				SceneTextShow.Instance.IsFrameChange = true;
				SceneTextShow.Instance.SavetoDataFile("采集恢复");
				isPrintErrorData = false;
			}
			if (preSignalIndex == ReadBuffer[10] * 256 + ReadBuffer[11])
			{
				sameSignalCount++;
				if (sameSignalCount > 10)
				{
					Reset();
					SceneTextShow.Instance.IsFrameChange = false;
					if (!isPrintErrorData)
					{
						SceneTextShow.Instance.SavetoDataFile("采集错误");
						isPrintErrorData = true;
					}
				}
				readSignal.InitCarOtherSignal();
				isDisConnectEvent = true;
				for (int i = 0; i < ReadBuffer.Length; i++)
				{
					ReadBuffer[i] = 0;
				}
				return;
			}
			sameSignalCount = 0;
			preSignalIndex = ReadBuffer[10] * 256 + ReadBuffer[11];
			if (!isDescryptOver)
			{
				byte[] decryptKey = GetDecryptKey(ReadBuffer);
				if (decryptKey != null && decryptKey[14] != byte.MaxValue)
				{
					Buffer.BlockCopy(decryptKey, 0, WriteBuffer, 0, 64);
					WriteData(WriteBuffer);
				}
			}
			else
			{
				WriteData(WriteBuffer);
			}
		}

		private void Reset()
		{
			sameSignalCount = 0;
			isDescryptOver = false;
			isFirst = true;
			SignalSourceCtl.Instance.connectStatus = 0;
			DataCenter.Instance.SerialNumber = "";
			hidControl.ResetUsb();
			DataCenter.Instance.isShowKeTengBiaoDingView = false;
		}

		private void InitWirteBufferBase()
		{
			WriteBuffer[8] = 168;
			WriteBuffer[9] = 40;
			if (isFirst && isDescryptOver)
			{
				WriteBuffer[18] = 0;
				isFirst = false;
			}
			WriteBuffer[16] = (byte)CarState.Instance.CurSpeed;
			WriteBuffer[17] = (byte)(CarState.Instance.CurEngineSpeed / 100f);
			WriteBuffer[19] = 128;
			WriteBuffer[20] = 0;
			WriteBuffer[21] = (byte)SettingInfoCtl.Instance.huiZhengForce;
			WriteBuffer[22] = 0;
		}

		private byte GetCarID(CarType type)
		{
			byte result = 0;
			switch (type)
			{
			case CarType.JieDaManual:
				result = 1;
				break;
			case CarType.JieDaAuto:
				result = 1;
				break;
			case CarType.DongFengDuoLiKa:
				result = 19;
				break;
			case CarType.DaBaChe:
				result = 19;
				break;
			case CarType.KaLuoLa:
				result = 1;
				break;
			case CarType.QiRuiE3:
				result = 1;
				break;
			case CarType.JieFang1121:
				result = 19;
				break;
			case CarType.TianJin:
				result = 19;
				break;
			case CarType.JieFang1125:
				result = 19;
				break;
			case CarType.DFKangMingSi:
				result = 19;
				break;
			case CarType.PiKa:
				result = 1;
				break;
			case CarType.AiLiSheManual:
			case CarType.AiLiSheAuto:
				result = 1;
				break;
			case CarType.YongShi:
				result = 1;
				break;
			case CarType.DFMengShi:
				result = 1;
				break;
			}
			return result;
		}

		public string Get16(byte Rbuffer)
		{
			string text = Convert.ToString(Rbuffer, 16);
			switch (text.Length)
			{
			case 1:
				text = "0" + text[0];
				break;
			case 2:
				text = text[0].ToString() + text[1];
				break;
			}
			return text;
		}

		private float GetSignalValue(int i)
		{
			int num = Mathf.Abs(SettingInfoCtl.Instance.biaoDingData[i] - SettingInfoCtl.Instance.biaoDingData[i + 1]);
			if (SettingInfoCtl.Instance.biaoDingData[i] - SettingInfoCtl.Instance.biaoDingData[i + 1] > 0)
			{
				return Mathf.InverseLerp(0f, (float)num * 0.94f, GetByteValue(ReadBuffer[i + 16], ReadBuffer[i + 17]) - (float)(int)SettingInfoCtl.Instance.biaoDingData[i + 1] - (float)num * 0.03f);
			}
			return 1f - Mathf.InverseLerp(0f, (float)num * 0.94f, GetByteValue(ReadBuffer[i + 16], ReadBuffer[i + 17]) - (float)(int)SettingInfoCtl.Instance.biaoDingData[i] - (float)num * 0.03f);
		}

		private bool JudgeIsConnect()
		{
			if (isDescryptOver)
			{
				for (int i = 0; i < 13; i++)
				{
					if (ReadBuffer[16 + i] != 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		public CarSignal AnalysisCircuitBoardSignal()
		{
			InitWirteBufferBase();
			if ((ViewJumpCtl.curViewType == ViewType.MainView || ViewJumpCtl.curViewType == ViewType.SoftSettingView || ViewJumpCtl.curViewType == ViewType.AboutView || ViewJumpCtl.curViewType == ViewType.CourseTwoView || ViewJumpCtl.curViewType == ViewType.CoursetwoScenesChooseOneView || ViewJumpCtl.curViewType == ViewType.CoursetwoScenesChooseTwoView || ViewJumpCtl.curViewType == ViewType.CourseThreeScenesChooseView || ViewJumpCtl.curViewType == ViewType.BanZiTiShiView) && GameGlobalCtl.Instance.IsStartReadAndWrite && DataCenter.Instance.SerialNumber != "" && LoadingScene.isFirstCheck)
			{
				SetWriteBuffer(29, 63);
				SetWriteBuffer(14, GetCarID(SettingInfoCtl.Instance.carType));
			}
			if (DataCenter.Instance.UsbVersion == 64)
			{
				if ((ViewJumpCtl.curViewType == ViewType.MainView || ViewJumpCtl.curViewType == ViewType.SoftSettingView || ViewJumpCtl.curViewType == ViewType.AboutView || ViewJumpCtl.curViewType == ViewType.CourseTwoView || ViewJumpCtl.curViewType == ViewType.CoursetwoScenesChooseOneView || ViewJumpCtl.curViewType == ViewType.CoursetwoScenesChooseTwoView || ViewJumpCtl.curViewType == ViewType.CourseThreeScenesChooseView) && Get16(ReadBuffer[56])[0].ToString() == "a")
				{
					AboutView.Number = "定制" + ReadBuffer[53];
				}
			}
			else
			{
				AboutView.Number = "";
			}
			if (ReadBuffer[56] == 160 && ReadBuffer[55] == 0 && ReadBuffer[54] == 0)
			{
				if (ReadBuffer[53] == 1)
				{
					ViewJumpCtl.Instance.Enter = true;
				}
				else
				{
					ViewJumpCtl.Instance.Enter = false;
				}
				GameGlobalCtl.Instance.CircuitBoardNumber = Convert.ToInt32(ReadBuffer[53]);
			}
			if ((ReadBuffer[8] != 166 || !isDescryptOver) && (JudgeIsConnect() || CarState.Instance.CurCar == null))
			{
				readSignal.InitCarOtherSignal();
				return readSignal;
			}
			if (SignalSourceCtl.Instance.Pid == "4200")
			{
				SignalSourceCtl.Instance.joyOriData = SignalSourceCtl.Instance.joyOriData.GetCirJoyOriData(ReadBuffer);
			}
			int num = 15;
			if (CarState.Instance.CurCar != null && !JudgeIsConnect())
			{
				if (!SettingInfoCtl.Instance.isManualGear)
				{
					carSignal.cluth = 0f;
				}
				else
				{
					carSignal.cluth = Mathf.InverseLerp(0f, 65535f, GetByteValue(PreSignalData16To28[0], PreSignalData16To28[1]));
				}
				carSignal.footBrake = Mathf.InverseLerp(0f, 65535f, GetByteValue(PreSignalData16To28[2], PreSignalData16To28[3]));
				carSignal.oil = Mathf.InverseLerp(0f, 65535f, GetByteValue(PreSignalData16To28[4], PreSignalData16To28[5]));
				carSignal.handBrake = Mathf.InverseLerp(0f, 65535f, GetByteValue(PreSignalData16To28[6], PreSignalData16To28[7]));
				if (ViewJumpCtl.Instance.itemType != 605)
				{
					carSignal.steerWheel = (Mathf.InverseLerp(0f, 65535f, GetByteValue(PreSignalData16To28[8], PreSignalData16To28[9])) - 0.5f) * 2f;
				}
				Buffer.BlockCopy(PreSignalData16To28, 10, otherSignalData, 0, 3);
				othersignalData2 = BitConverter.ToUInt32(otherSignalData, 0);
				GetOtherSignal(othersignalData2);
			}
			else
			{
				if (!SettingInfoCtl.Instance.isManualGear)
				{
					carSignal.cluth = 0f;
					num++;
					num++;
				}
				else
				{
					carSignal.cluth = Mathf.InverseLerp(0f, 65535f, GetByteValue(ReadBuffer[++num], ReadBuffer[++num]));
				}
				carSignal.footBrake = Mathf.InverseLerp(0f, 65535f, GetByteValue(ReadBuffer[++num], ReadBuffer[++num]));
				carSignal.oil = Mathf.InverseLerp(0f, 65535f, GetByteValue(ReadBuffer[++num], ReadBuffer[++num]));
				carSignal.handBrake = Mathf.InverseLerp(0f, 65535f, GetByteValue(ReadBuffer[++num], ReadBuffer[++num]));
				if (ViewJumpCtl.Instance.itemType != 605)
				{
					carSignal.steerWheel = (Mathf.InverseLerp(0f, 65535f, GetByteValue(ReadBuffer[24], ReadBuffer[25])) - 0.5f) * 2f;
				}
				Buffer.BlockCopy(ReadBuffer, 26, otherSignalData, 0, 3);
				othersignalData2 = BitConverter.ToUInt32(otherSignalData, 0);
				GetOtherSignal(othersignalData2);
			}
			if (DataCenter.Instance.SerialNumber == "" && ReadBuffer[29] == 0 && isDescryptOver && CarState.Instance.CurCar == null)
			{
				GetSerialNumber(ReadBuffer);
			}
			else if (DataCenter.Instance.SerialNumber == "")
			{
				SetWriteBuffer(29, 0);
			}
			if (CarState.Instance.CurCar != null)
			{
				dynamicData = MoNiCarDynamic.Instance.TransfromGeneralFormat(CarState.Instance);
			}
			if (CarState.Instance.CurCar != null && JudgeIsConnect())
			{
				for (int i = 0; i < 13; i++)
				{
					PreSignalData16To28[i] = ReadBuffer[i + 16];
				}
			}
			GetCircuitBoardDynamicData();
			return carSignal;
		}

		private void GetOtherSignal(uint signal)
		{
			widthLightIntervalTime += Time.deltaTime;
			carSignal.InitCarOtherSignal();
			int num = 1;
			for (int i = 0; i < 24; i++)
			{
				num = 1 << i;
				if ((signal & num) <= 0)
				{
					continue;
				}
				switch (i)
				{
				case 0:
					if (DriveHistoryPlay.Instance != null && (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
					{
						carSignal.door = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.door;
					}
					carSignal.door = 0;
					break;
				case 1:
					if (!SettingInfoCtl.Instance.isManualGear)
					{
						carSignal.gear = SettingInfoCtl.Instance.ManualToAutoGear4;
					}
					else
					{
						carSignal.gear = 4;
					}
					break;
				case 2:
					if (!SettingInfoCtl.Instance.isManualGear)
					{
						carSignal.gear = SettingInfoCtl.Instance.ManualToAutoGear2;
					}
					else
					{
						carSignal.gear = 2;
					}
					break;
				case 3:
					if (DriveHistoryPlay.Instance != null)
					{
						if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
						{
							carSignal.safeBelt = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.safeBelt;
						}
					}
					else if (SettingInfoCtl.Instance.isReverseSafetyBelt)
					{
						carSignal.safeBelt = 1;
					}
					else
					{
						carSignal.safeBelt = 0;
					}
					break;
				case 4:
					if (!SettingInfoCtl.Instance.isManualGear)
					{
						carSignal.gear = SettingInfoCtl.Instance.ManualToAutoGear1;
					}
					else
					{
						carSignal.gear = 1;
					}
					break;
				case 5:
					if (!SettingInfoCtl.Instance.isManualGear)
					{
						carSignal.gear = SettingInfoCtl.Instance.ManualToAutoGear3;
					}
					else
					{
						carSignal.gear = 3;
					}
					break;
				case 6:
					if (!SettingInfoCtl.Instance.isManualGear)
					{
						carSignal.gear = SettingInfoCtl.Instance.ManualToAutoGear5;
					}
					else
					{
						carSignal.gear = 5;
					}
					break;
				case 7:
					if (!SettingInfoCtl.Instance.isManualGear)
					{
						carSignal.gear = SettingInfoCtl.Instance.ManualToAutoGearR;
					}
					else
					{
						carSignal.gear = 6;
					}
					break;
				case 8:
					carSignal.fogLight = 1;
					break;
				case 10:
					carSignal.frontLight = 1;
					break;
				case 11:
					carSignal.horn = 1;
					break;
				case 12:
					carSignal.sprayWater = 1;
					break;
				case 13:
					if (SignalSourceCtl.Instance.Pid != "4200")
					{
						carSignal.widthLight = 1;
					}
					break;
				case 14:
					carSignal.frontLight = 2;
					break;
				case 15:
					if (SignalSourceCtl.Instance.Pid == "4200" && widthLightIntervalTime > 0.5f)
					{
						if (carSignal.widthLight == 1)
						{
							carSignal.widthLight = 0;
						}
						else
						{
							carSignal.widthLight = 1;
						}
						widthLightIntervalTime = 0f;
					}
					break;
				case 17:
					carSignal.rainBrush = 1;
					break;
				case 18:
					carSignal.turnLight = 2;
					break;
				case 19:
					carSignal.fire = 1;
					break;
				case 20:
					carSignal.power = 1;
					break;
				case 21:
					if (carSignal.turnLight == 2)
					{
						carSignal.turnLight = 3;
					}
					else
					{
						carSignal.turnLight = 1;
					}
					break;
				case 22:
					carSignal.rainBrush = 2;
					break;
				case 23:
					carSignal.rainBrush = 3;
					break;
				}
			}
		}

		private void GetCircuitBoardDynamicData()
		{
			if (ReadBuffer[29] != 192)
			{
				return;
			}
			readBufferBr.BaseStream.Position = 32L;
			readBufferBr.ReadInt32();
			DataCenter.Instance.coreNumber = readBufferBr.ReadInt32();
			readBufferBr.ReadInt16();
			if (readBufferBr.ReadUInt16() != writeIndex)
			{
				return;
			}
			if (MoNiCarDynamic.Instance.dynamicData.engineIsFired)
			{
				if (MoNiCarDynamic.Instance.dynamicData.gear == 1 || MoNiCarDynamic.Instance.dynamicData.gear == 6 || MoNiCarDynamic.Instance.dynamicData.gear == 0 || Mathf.Abs(MoNiCarDynamic.Instance.dynamicData.carSpeed) < 6f)
				{
					MoNiCarDynamic.Instance.DynamicCaculateLocal();
				}
				else
				{
					MoNiCarDynamic.Instance.dynamicData.pullForce = readBufferBr.ReadSingle();
					MoNiCarDynamic.Instance.dynamicData.fResist = readBufferBr.ReadSingle();
					MoNiCarDynamic.Instance.dynamicData.fBrake = readBufferBr.ReadSingle();
					MoNiCarDynamic.Instance.dynamicData.fResist01 = readBufferBr.ReadSingle();
					MoNiCarDynamic.Instance.DynamicCaculate();
				}
			}
			else
			{
				MoNiCarDynamic.Instance.dynamicData.pullForce = 0f;
				MoNiCarDynamic.Instance.dynamicData.fResist = 0f;
				MoNiCarDynamic.Instance.dynamicData.fBrake = 0f;
				MoNiCarDynamic.Instance.dynamicData.fResist01 = 0f;
				MoNiCarDynamic.Instance.DynamicCaculate();
			}
			writeIndex++;
		}

		private void GetSerialNumber(byte[] signal)
		{
			byte[] array = new byte[24];
			Buffer.BlockCopy(signal, 42, array, 0, 20);
			string text = "";
			for (int i = 12; i < 20; i++)
			{
				text += Get16(array[i]).ToString();
			}
			DataCenter.Instance.SerialNumber = text.ToUpper();
			Buffer.BlockCopy(signal, 14, array, 20, 2);
			DataCenter.Instance.UsbVersion = (byte)(signal[62] & 0xF0);
			Des des = new Des();
			Debug.Log(ByteToString(des.Encrypt(array)));
		}

		private string ByteToString(byte[] bytes)
		{
			string text = "";
			if (bytes != null)
			{
				for (int i = 0; i < bytes.Length; i++)
				{
					text += $"{bytes[i]:X2}";
				}
			}
			return text;
		}

		private float GetByteValue(byte a, byte b)
		{
			return a * 256 + b;
		}

		public byte GetReadBuffer(int index)
		{
			return ReadBuffer[index];
		}

		public void SetWriteBuffer(int index, byte value)
		{
			if (isDescryptOver)
			{
				WriteBuffer[index] = value;
			}
		}

		private void ShowBuffer(byte[] buffer, int startIndex = 0)
		{
			string text = "";
			for (int i = startIndex; i < buffer.Length; i++)
			{
				text = text + buffer[i] + "  ";
			}
			Debug.Log(text);
		}

		public byte[] GetDecryptKey(byte[] buffer)
		{
			if (keyIndex != buffer[13] && hidControl.GetPid() != 0)
			{
				keyIndex = buffer[13];
				Buffer.BlockCopy(des.Decrypt(DesEncrystRsaKeyData), keyIndex * 8, rsaDescryKey, 0, 8);
				haveBeginDescryXinHao = false;
				isDescryptOver = false;
				RunFirst = true;
			}
			if (buffer[9] == 40 || RunFirst)
			{
				RunFirst = false;
				Flage = 224;
				UsedWriteBuffer[8] = 169;
				UsedWriteBuffer[9] = 41;
				UsedWriteBuffer[14] = Flage;
				SetDataForDescryst(UsedWriteBuffer, 0);
			}
			else if (Flage == buffer[14] || (Flage == 226 && buffer[14] == 210))
			{
				switch (buffer[14])
				{
				case 224:
					Flage = 225;
					SetDataForDescryst(UsedWriteBuffer, 1);
					break;
				case 225:
					Flage = 226;
					SetDataForDescryst(UsedWriteBuffer, 2);
					break;
				case 210:
					haveBeginDescryXinHao = true;
					Buffer.BlockCopy(buffer, 16, RecvDescryData, 80, 32);
					Flage = 209;
					break;
				case 209:
					Buffer.BlockCopy(buffer, 16, RecvDescryData, 32, 48);
					Flage = 208;
					break;
				case 208:
					Buffer.BlockCopy(buffer, 32, RecvDescryData, 0, 32);
					if (haveBeginDescryXinHao)
					{
						buffer = des.Decrypt(RecvDescryData);
						Buffer.BlockCopy(buffer, keyIndex % 14 * 8, xinhaoDescryKey, 0, 8);
						isDescryptOver = true;
					}
					Flage = byte.MaxValue;
					break;
				}
				UsedWriteBuffer[14] = Flage;
			}
			else
			{
				UsedWriteBuffer[14] = byte.MaxValue;
			}
			return UsedWriteBuffer;
		}

		public void ReadData(byte[] buffer)
		{
			if (hidControl.TranslateWithUSB(ReadTempBuffer) && CkData(ReadTempBuffer) && EncryOrDescryUsbData(ReadTempBuffer, 16, 48, ReadTempBuffer[13], 1))
			{
				Buffer.BlockCopy(ReadTempBuffer, 0, buffer, 0, 64);
			}
		}

		public bool WriteData(byte[] buffer)
		{
			if (buffer[29] == 192)
			{
				writeBufferBw.BaseStream.Position = 32L;
				uint coreNumber = (uint)DataCenter.Instance.coreNumber;
				writeBufferBw.Write(coreNumber);
				writeBufferBw.Write(~coreNumber);
				writeBufferBw.Write(writeIndex);
				writeBufferBw.Write((short)dynamicData.engineSpeed);
				writeBufferBw.Write((short)(dynamicData.carSpeed * 100f));
				writeBufferBw.Write(dynamicData.engineIsFired);
				writeBufferBw.Write((short)(DataCenter.Instance.mAdhere * 100f));
				writeBufferBw.Write(0f);
				writeBufferBw.Write(dynamicData.gear);
				writeBufferBw.Write((short)(dynamicData.clutch * 65535f));
				writeBufferBw.Write((short)(dynamicData.footBrake * 65535f));
				writeBufferBw.Write((short)(dynamicData.oil * 65535f));
				writeBufferBw.Write((short)(dynamicData.handBrake * 65535f));
			}
			byte[] array = new byte[64];
			Buffer.BlockCopy(buffer, 0, array, 0, 64);
			array[13] = (byte)keyIndex;
			if (!EncryOrDescryUsbData(array, 16, 48, array[13], 0))
			{
				return false;
			}
			array[12] = 0;
			for (int i = 16; i < 64; i++)
			{
				array[12] += array[i];
			}
			if (buffer[29] != 192 && buffer[29] != 0)
			{
				specialDataCount++;
				if (specialDataCount > 10)
				{
					buffer[29] = 0;
					specialDataCount = 0;
				}
			}
			return hidControl.TranslateWithUSB(array, OperUsb.WRITE);
		}

		private bool CkData(byte[] buffer)
		{
			bool result = false;
			if (buffer != null && (166 == buffer[8] || 167 == buffer[8]) && (40 == buffer[9] || 41 == buffer[9]))
			{
				byte b = 0;
				for (byte b2 = 16; b2 < 64; b2++)
				{
					b += buffer[b2];
				}
				if (buffer[12] == b)
				{
					result = true;
				}
			}
			return result;
		}

		public bool EncryOrDescryUsbData(byte[] data, int postion, int len, int keyindex, int Encry_Flage)
		{
			bool result = false;
			if (data[9] == 40)
			{
				result = EncryOrDescryData(data, postion, len, Encry_Flage, xinhaoDescryKey);
			}
			else if (data[9] == 41)
			{
				result = EncryOrDescryData(data, postion, len, Encry_Flage, rsaDescryKey);
			}
			return result;
		}

		public void SetDataForDescryst(byte[] buffer, int index)
		{
			Buffer.BlockCopy(ENCRY_XINHAO_DATA, keyIndex / 14 * 128, CurrDescryData, 0, 128);
			switch (index)
			{
			case 0:
				Buffer.BlockCopy(CurrDescryData, index * 48, buffer, 16, 48);
				break;
			case 1:
				Buffer.BlockCopy(CurrDescryData, index * 48, buffer, 16, 48);
				break;
			case 2:
				Buffer.BlockCopy(CurrDescryData, index * 48, buffer, 16, 32);
				break;
			}
		}

		public bool JudgeDescryptOver()
		{
			return isDescryptOver;
		}

		private void OnDestroy()
		{
			WriteBuffer[18] = 0;
			WriteData(WriteBuffer);
			textAsset = null;
			Resources.UnloadUnusedAssets();
		}

		[DllImport("MoniDecryst", CallingConvention = CallingConvention.Cdecl)]
		private static extern bool EncryOrDescryData(byte[] data, int postion, int len, int Encry_Flage, byte[] key);
	}
}
