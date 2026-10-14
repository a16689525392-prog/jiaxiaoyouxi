using System.Collections;
using System.IO;
using Car;
using UnityEngine;
using UnityEngine.Networking;
using View;

namespace Global
{
	public class SettingInfoCtl : MonoBehaviour
	{
		private string path = "";

		private string BiaoDingpath = "";

		public bool IsFangXiangPan = true;

		public string SetResolution = "自适应";

		public double priceSum;

		public int zhengDongForce = 40;

		public int huiZhengForce = 40;

		public int historyRecordTime = 1;

		public bool isShowTopView = true;

		public string password = "1111";

		public bool isJudgeFogLight;

		public bool isJudgeDoor;

		public bool isJudgeSafetyBelt;

		public bool isReverseSafetyBelt;

		public bool isSaveData;

		public MyBiaoDing BiaoDingModel = MyBiaoDing.Board;

		public CarType carType;

		public bool isManualGear = true;

		public int curViewPoint;

		public bool isCeFangJudgeLight;

		public bool isShowFps = true;

		public MyQualityLevel qualityLevel = MyQualityLevel.Fast;

		public bool isChangeYanShi = true;

		public bool isDelete;

		public bool isCorrect;

		public float Mileage;

		public bool isOpenDongGan;

		public int ManualToAutoGear1;

		public int ManualToAutoGear2;

		public int ManualToAutoGear3;

		public int ManualToAutoGear4;

		public int ManualToAutoGear5;

		public int ManualToAutoGearR;

		public byte[] biaoDingData = new byte[30];

		public string LastCalibratedCircuitBoard = "";

		public bool isShowBiaoDing;

		public int RainIntensity;

		public int SnowIntensity;

		public int FogIntensity;

		public int BigCarlaps = 5;

		public static SettingInfoCtl Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
			if (Application.platform == RuntimePlatform.Android)
			{
				if (!Directory.Exists(Application.persistentDataPath + "/SettingFile"))
				{
					StartCoroutine(LoadTotagfilePath(Application.streamingAssetsPath + "/SettingFile/setting.bytes", Application.persistentDataPath + "/SettingFile/setting.bytes"));
					StartCoroutine(LoadTotagfilePath(Application.streamingAssetsPath + "/SettingFile/MirrorData.xml", Application.persistentDataPath + "/SettingFile/MirrorData.xml"));
				}
				path = Application.persistentDataPath + "/SettingFile/setting.bytes";
			}
			else
			{
				path = Application.streamingAssetsPath + "/SettingFile/setting.bytes";
			}
			if (Application.platform == RuntimePlatform.Android)
			{
				BiaoDingpath = Application.persistentDataPath + "/SettingFile/BiaoDingData.bytes";
			}
			else
			{
				BiaoDingpath = Application.streamingAssetsPath + "/SettingFile/BiaoDingData.bytes";
			}
		}

		private IEnumerator LoadTotagfilePath(string filePath, string tagfilePath)
		{
			if (!File.Exists(tagfilePath))
			{
				UnityWebRequest www = UnityWebRequest.Get(filePath);
				DownloadHandlerFile downloadHandler = new DownloadHandlerFile(tagfilePath);
				www.downloadHandler = downloadHandler;
				yield return www.SendWebRequest();
				while (!www.isDone)
				{
					yield return null;
				}
			}
		}

		public void SaveSetingInfos()
		{
			try
			{
				FileStream fileStream = new FileInfo(path).Create();
				BinaryWriter binaryWriter = new BinaryWriter(fileStream);
				binaryWriter.Write(zhengDongForce);
				binaryWriter.Write(isShowTopView);
				binaryWriter.Write(huiZhengForce);
				binaryWriter.Write(historyRecordTime);
				binaryWriter.Write(password);
				binaryWriter.Write(isJudgeFogLight);
				binaryWriter.Write(isJudgeDoor);
				binaryWriter.Write(isJudgeSafetyBelt);
				binaryWriter.Write(curViewPoint);
				binaryWriter.Write((int)carType);
				binaryWriter.Write(isManualGear);
				binaryWriter.Write(isCeFangJudgeLight);
				binaryWriter.Write(isShowFps);
				binaryWriter.Write(isChangeYanShi);
				binaryWriter.Write((int)qualityLevel);
				binaryWriter.Write(SetResolution);
				binaryWriter.Write(IsFangXiangPan);
				binaryWriter.Write(Mileage);
				binaryWriter.Write(isOpenDongGan);
				binaryWriter.Write(isReverseSafetyBelt);
				binaryWriter.Write(isSaveData);
				binaryWriter.Write(ManualToAutoGear1);
				binaryWriter.Write(ManualToAutoGear2);
				binaryWriter.Write(ManualToAutoGear3);
				binaryWriter.Write(ManualToAutoGear4);
				binaryWriter.Write(ManualToAutoGear5);
				binaryWriter.Write(ManualToAutoGearR);
				binaryWriter.Write((int)BiaoDingModel);
				binaryWriter.Write(isShowBiaoDing);
				binaryWriter.Write(LastCalibratedCircuitBoard);
				binaryWriter.Write(RainIntensity);
				binaryWriter.Write(SnowIntensity);
				binaryWriter.Write(FogIntensity);
				binaryWriter.Write(BigCarlaps);
				binaryWriter.Close();
				fileStream.Close();
			}
			catch
			{
				ViewJumpCtl.Instance.ShowMsgTips("存储文件异常！");
			}
		}

		public void ReadSettingFile()
		{
			try
			{
				FileInfo fileInfo = new FileInfo(path);
				if (!fileInfo.Exists)
				{
					SetDefaultSettingData();
					if (SignalSourceCtl.Instance.Pid == "4200" || SignalSourceCtl.Instance.Pid == "4100")
					{
						isJudgeDoor = true;
						isJudgeFogLight = true;
						isJudgeSafetyBelt = true;
					}
					return;
				}
				FileStream fileStream = fileInfo.OpenRead();
				BinaryReader binaryReader = new BinaryReader(fileStream);
				zhengDongForce = binaryReader.ReadInt32();
				isShowTopView = binaryReader.ReadBoolean();
				huiZhengForce = binaryReader.ReadInt32();
				historyRecordTime = binaryReader.ReadInt32();
				password = binaryReader.ReadString();
				isJudgeFogLight = binaryReader.ReadBoolean();
				isJudgeDoor = binaryReader.ReadBoolean();
				isJudgeSafetyBelt = binaryReader.ReadBoolean();
				curViewPoint = binaryReader.ReadInt32();
				carType = (CarType)binaryReader.ReadInt32();
				isManualGear = binaryReader.ReadBoolean();
				isCeFangJudgeLight = binaryReader.ReadBoolean();
				isShowFps = binaryReader.ReadBoolean();
				isChangeYanShi = binaryReader.ReadBoolean();
				qualityLevel = (MyQualityLevel)binaryReader.ReadInt32();
				SetResolution = binaryReader.ReadString();
				IsFangXiangPan = binaryReader.ReadBoolean();
				Mileage = binaryReader.ReadSingle();
				isOpenDongGan = binaryReader.ReadBoolean();
				isReverseSafetyBelt = binaryReader.ReadBoolean();
				isSaveData = binaryReader.ReadBoolean();
				ManualToAutoGear1 = binaryReader.ReadInt32();
				ManualToAutoGear2 = binaryReader.ReadInt32();
				ManualToAutoGear3 = binaryReader.ReadInt32();
				ManualToAutoGear4 = binaryReader.ReadInt32();
				ManualToAutoGear5 = binaryReader.ReadInt32();
				ManualToAutoGearR = binaryReader.ReadInt32();
				BiaoDingModel = (MyBiaoDing)binaryReader.ReadInt32();
				isShowBiaoDing = binaryReader.ReadBoolean();
				LastCalibratedCircuitBoard = binaryReader.ReadString();
				RainIntensity = binaryReader.ReadInt32();
				SnowIntensity = binaryReader.ReadInt32();
				FogIntensity = binaryReader.ReadInt32();
				BigCarlaps = binaryReader.ReadInt32();
				binaryReader.Close();
				fileStream.Close();
				if (carType == CarType.JieDaManual && !isManualGear)
				{
					SetDefaultSettingData();
					SaveSetingInfos();
				}
			}
			catch
			{
				SetDefaultSettingData();
				ViewJumpCtl.Instance.ShowMsgTips("读取文件异常！");
			}
		}

		public void SaveBiaoDingInfos()
		{
			try
			{
				FileStream fileStream = new FileInfo(BiaoDingpath).Create();
				BinaryWriter binaryWriter = new BinaryWriter(fileStream);
				for (int i = 0; i < 30; i++)
				{
					binaryWriter.Write(biaoDingData[i]);
				}
				binaryWriter.Close();
				fileStream.Close();
			}
			catch
			{
				ViewJumpCtl.Instance.ShowMsgTips("存储文件异常！");
			}
		}

		public bool ReadBiaoDingFile()
		{
			bool result = false;
			try
			{
				FileInfo fileInfo = new FileInfo(BiaoDingpath);
				if (fileInfo.Exists)
				{
					FileStream fileStream = fileInfo.OpenRead();
					BinaryReader binaryReader = new BinaryReader(fileStream);
					for (int i = 0; i < 30; i++)
					{
						biaoDingData[i] = binaryReader.ReadByte();
					}
					binaryReader.Close();
					fileStream.Close();
					result = true;
				}
			}
			catch
			{
				ViewJumpCtl.Instance.ShowMsgTips("读取文件异常！");
				return result;
			}
			return result;
		}

		public void SetDefaultSettingData()
		{
			IsFangXiangPan = true;
			SetResolution = "自适应";
			priceSum = 0.0;
			zhengDongForce = 40;
			huiZhengForce = 40;
			historyRecordTime = 1;
			isShowTopView = true;
			password = "1111";
			isJudgeFogLight = false;
			isJudgeDoor = false;
			isJudgeSafetyBelt = false;
			isReverseSafetyBelt = false;
			isSaveData = false;
			BiaoDingModel = MyBiaoDing.Board;
			carType = CarType.JieDaManual;
			isManualGear = true;
			curViewPoint = 0;
			isCeFangJudgeLight = false;
			isShowFps = true;
			qualityLevel = MyQualityLevel.Fast;
			isChangeYanShi = true;
			isDelete = false;
			isCorrect = false;
			Mileage = 0f;
			isOpenDongGan = false;
			ManualToAutoGear1 = 100;
			ManualToAutoGear2 = 100;
			ManualToAutoGear3 = 102;
			ManualToAutoGear4 = 100;
			ManualToAutoGear5 = 101;
			ManualToAutoGearR = 102;
			isShowBiaoDing = false;
			RainIntensity = 0;
			SnowIntensity = 0;
			FogIntensity = 0;
			BigCarlaps = 5;
		}
	}
}
