using System;
using System.Collections.Generic;
using Car;
using Scene;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Global
{
	public class GameGlobalCtl : MonoBehaviour
	{
		public int CircuitBoardNumber;

		public static float PXvalue;

		public static float PYvalue;

		public static float PZvalue;

		public static float RXvalue;

		public static float RYvalue;

		public static float RZvalue;

		public string Signal;

		public Queue<string> SignalQueueDataPool = new Queue<string>();

		public float DisconnectToConnectTime;

		public bool IsStartReadAndWrite;

		private float Checktime1;

		private float Checktime2;

		private int Checkindex;

		private int WriteCount;

		private int GetCircuitBoardSignalType;

		public static GameGlobalCtl Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null)
			{
				Debug.Log("global des");
				UnityEngine.Object.DestroyImmediate(base.gameObject);
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			Instance = this;
			AddGlobalScripts();
			if (Application.platform == RuntimePlatform.Android)
			{
				Application.targetFrameRate = 30;
			}
			else
			{
				Application.targetFrameRate = 300;
			}
			Screen.sleepTimeout = -1;
			Checktime1 = 0f;
			Checktime2 = 0f;
			Checkindex = 0;
			WriteCount = 0;
		}

		private void AddGlobalScripts()
		{
			base.gameObject.AddComponent<ExternalInputCtl>();
			base.gameObject.AddComponent<ViewJumpCtl>();
			base.gameObject.AddComponent<SceneJumpCtl>();
			base.gameObject.AddComponent<DataCenter>();
			base.gameObject.AddComponent<CarActionJudge>();
			base.gameObject.AddComponent<SoundPlayCtl>();
			base.gameObject.AddComponent<CarState>();
			base.gameObject.AddComponent<SignalSourceCtl>();
			base.gameObject.AddComponent<MoNiCarDynamic>();
			base.gameObject.AddComponent<MyDiagnosis>();
		}

		public void FindUsbDevice(string idStr)
		{
			string[] array = idStr.Split(':');
			hidControl._GetMoniDevice(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]), int.Parse(array[3]));
		}

		public void FixedUpdate()
		{
			GetCarSteerWheeMaxlAngle();
			if (SignalSourceCtl.Instance.connectStatus == 1)
			{
				DisconnectToConnectTime += Time.fixedDeltaTime;
			}
			else if (SignalSourceCtl.Instance.connectStatus == 0)
			{
				DisconnectToConnectTime = 0f;
				IsStartReadAndWrite = false;
				LoadingScene.isFirstCheck = false;
				Checktime1 = 0f;
				Checktime2 = 0f;
				Checkindex = 0;
				WriteCount = 0;
			}
			if (DisconnectToConnectTime > 1f)
			{
				IsStartReadAndWrite = true;
			}
			if (DisconnectToConnectTime > 2f && GetComponent<SignalSourceCtl>() != null && !GetComponent<SignalSourceCtl>().cbSignalReader.isDescryptOver)
			{
				UnityEngine.Object.Destroy(GetComponent<SignalSourceCtl>());
				base.gameObject.AddComponent<SignalSourceCtl>();
				DisconnectToConnectTime = 0f;
				Debug.Log("执行了信号脚本重置");
			}
			Signal = "离合" + (CarState.Instance.CarSignal.cluth * 100f).ToString("f0") + "脚刹" + (CarState.Instance.CarSignal.footBrake * 100f).ToString("f0") + "油门" + (CarState.Instance.CarSignal.oil * 100f).ToString("f0") + "手刹" + (CarState.Instance.CarSignal.handBrake * 100f).ToString("f0") + "方向盘" + CarState.Instance.CarSignal.steerWheel.ToString("f2") + "转向灯状态" + CarState.Instance.CarSignal.turnLight + "雨刷状态" + CarState.Instance.CarSignal.rainBrush + "前照灯状态" + CarState.Instance.CarSignal.frontLight + "档位状态" + CarState.Instance.CarSignal.gear + "点火开关状态" + CarState.Instance.CarSignal.fire + "电源开关状态" + CarState.Instance.CarSignal.power + "喇叭状态" + CarState.Instance.CarSignal.horn + "安全带状态" + CarState.Instance.CarSignal.safeBelt + "示宽灯状态" + CarState.Instance.CarSignal.widthLight + "前雾灯状态" + CarState.Instance.CarSignal.fogLight;
			SignalQueueDataPool.Enqueue(Signal);
			if (SignalQueueDataPool.Count > 60)
			{
				SignalQueueDataPool.Dequeue();
			}
			if (DataCenter.Instance.SerialNumber != "" && DataCenter.Instance.SerialNumber == SettingInfoCtl.Instance.LastCalibratedCircuitBoard)
			{
				if (DataCenter.Instance.UsbVersion == 240 || DataCenter.Instance.UsbVersion == 0 || SignalSourceCtl.Instance.connectStatus != 1 || !IsStartReadAndWrite || LoadingScene.isFirstCheck)
				{
					return;
				}
				if (!SettingInfoCtl.Instance.ReadBiaoDingFile())
				{
					LoadingScene.isFirstCheck = true;
				}
				else
				{
					if (LoadingScene.isFirstCheck)
					{
						return;
					}
					Check();
					if (Checkindex == 99)
					{
						LoadingScene.isFirstCheck = true;
						Checkindex = 100;
					}
					else if (Checkindex == 10)
					{
						SignalSourceCtl.Instance.SetCircuitBoardSignalType(3);
						for (int i = 0; i < 30; i++)
						{
							SignalSourceCtl.Instance.SetWriteBuffer(32 + i, SettingInfoCtl.Instance.biaoDingData[i]);
						}
						ViewJumpCtl.Instance.ShowMsgTips("正在使用本地数据进行标定!");
						Checkindex = 20;
						Checktime2 = 0f;
					}
					else if (Checkindex == 20)
					{
						Checktime2 += Time.deltaTime;
						if (Checktime2 > 0.5f)
						{
							Checkindex = 30;
						}
					}
					else
					{
						if (Checkindex != 30)
						{
							return;
						}
						SignalSourceCtl.Instance.SetCircuitBoardSignalType(4);
						if (SignalSourceCtl.Instance.GetCircuitBoardSignalType() != 4)
						{
							return;
						}
						bool flag = false;
						GetCircuitBoardSignalType = Convert.ToInt32(SignalSourceCtl.Instance.GetReadBuffer(62));
						Debug.Log("当前板子是：" + GetCircuitBoardSignalType);
						if (GetCircuitBoardSignalType == 3)
						{
							for (int j = 0; j < 30; j++)
							{
								if (j != 1 && j != 2 && j != 3 && j != 4 && j != 5 && j != 7 && j != 8 && j != 9 && j != 10 && j != 11 && j != 13 && SettingInfoCtl.Instance.biaoDingData[j] != SignalSourceCtl.Instance.GetReadBuffer(32 + j))
								{
									flag = true;
									Debug.Log("i：" + j + "biaoDingData:" + SettingInfoCtl.Instance.biaoDingData[j] + "GetReadBuffer:" + SignalSourceCtl.Instance.GetReadBuffer(32 + j));
								}
							}
						}
						else if (GetCircuitBoardSignalType == 7)
						{
							for (int k = 0; k < 30; k++)
							{
								if (k != 1 && k != 3 && k != 5 && k != 7 && k != 9 && k != 8 && k != 9 && k != 11 && k != 13 && SettingInfoCtl.Instance.biaoDingData[k] != SignalSourceCtl.Instance.GetReadBuffer(32 + k))
								{
									flag = true;
									Debug.Log("i：" + k + "biaoDingData:" + SettingInfoCtl.Instance.biaoDingData[k] + "GetReadBuffer:" + SignalSourceCtl.Instance.GetReadBuffer(32 + k));
								}
							}
						}
						if (flag)
						{
							WriteCount++;
							if (WriteCount < 10)
							{
								Checkindex = 10;
								return;
							}
							ViewJumpCtl.Instance.ShowMsgTips("本地数据标定失败!");
							Checkindex = 99;
						}
						else
						{
							ViewJumpCtl.Instance.ShowMsgTips("本地数据标定成功!");
							Checkindex = 99;
						}
					}
				}
			}
			else if (DataCenter.Instance.SerialNumber != "" && DataCenter.Instance.SerialNumber != SettingInfoCtl.Instance.LastCalibratedCircuitBoard)
			{
				LoadingScene.isFirstCheck = true;
			}
		}

		private void Check()
		{
			if (Checkindex >= 10)
			{
				return;
			}
			try
			{
				Checktime1 += Time.deltaTime;
				SignalSourceCtl.Instance.SetCircuitBoardSignalType(4);
				if (Checktime1 <= 5f)
				{
					if (SignalSourceCtl.Instance.GetCircuitBoardSignalType() != 4)
					{
						return;
					}
					bool flag = false;
					GetCircuitBoardSignalType = Convert.ToInt32(SignalSourceCtl.Instance.GetReadBuffer(62));
					Debug.Log("当前板子是：" + GetCircuitBoardSignalType);
					if (GetCircuitBoardSignalType == 3)
					{
						for (int i = 0; i < 30; i++)
						{
							if (i != 1 && i != 2 && i != 3 && i != 4 && i != 5 && i != 7 && i != 8 && i != 9 && i != 10 && i != 11 && i != 13 && SettingInfoCtl.Instance.biaoDingData[i] != SignalSourceCtl.Instance.GetReadBuffer(32 + i))
							{
								flag = true;
								Debug.Log("i：" + i + "biaoDingData:" + SettingInfoCtl.Instance.biaoDingData[i] + "GetReadBuffer:" + SignalSourceCtl.Instance.GetReadBuffer(32 + i));
							}
						}
					}
					else if (GetCircuitBoardSignalType == 7)
					{
						for (int j = 0; j < 30; j++)
						{
							if (j != 1 && j != 3 && j != 5 && j != 7 && j != 9 && j != 8 && j != 9 && j != 11 && j != 13 && SettingInfoCtl.Instance.biaoDingData[j] != SignalSourceCtl.Instance.GetReadBuffer(32 + j))
							{
								flag = true;
								Debug.Log("i：" + j + "biaoDingData:" + SettingInfoCtl.Instance.biaoDingData[j] + "GetReadBuffer:" + SignalSourceCtl.Instance.GetReadBuffer(32 + j));
							}
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						ViewJumpCtl.Instance.ShowMsgTips("本地数据与主控板原始标定数据核对异常!");
						Checkindex = 10;
					}
					else
					{
						ViewJumpCtl.Instance.ShowMsgTips("本地数据与主控板原始标定数据核对正常!");
						Checkindex = 99;
					}
				}
				else
				{
					ViewJumpCtl.Instance.ShowMsgTips("检验超时!");
					Checkindex = 99;
				}
			}
			catch
			{
				ViewJumpCtl.Instance.ShowMsgTips("核对过程异常!");
				Checkindex = 99;
			}
		}

		private void GetCarSteerWheeMaxlAngle()
		{
			if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual || SettingInfoCtl.Instance.carType == CarType.KaLuoLa || SettingInfoCtl.Instance.carType == CarType.QiRuiE3 || SettingInfoCtl.Instance.carType == CarType.AiLiSheManual || SettingInfoCtl.Instance.carType == CarType.AiLiSheAuto || SettingInfoCtl.Instance.carType == CarType.PiKa || SettingInfoCtl.Instance.carType == CarType.YongShi)
			{
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
			}
			else if (SettingInfoCtl.Instance.BigCarlaps == 4)
			{
				CarSignalValue.SteerWheeMaxlAngle = 720f;
			}
			else
			{
				CarSignalValue.SteerWheeMaxlAngle = 900f;
			}
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
		}
	}
}
