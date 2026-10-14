using System;
using System.Collections.Generic;
using System.IO;
using Car;
using Global;
using UnityEngine;
using View;

namespace Scene
{
	public class DriveHistoryRecord : MonoBehaviour
	{
		public static DriveHistoryRecord Instance;

		private List<DriveHistory> carSignalList = new List<DriveHistory>();

		private List<RecordItemData> recordItemDataList = new List<RecordItemData>();

		public int historyIndex = 8;

		public int index;

		private void Awake()
		{
			Instance = this;
			MonoBehaviour.print(PlayerPrefs.GetInt("i"));
		}

		private void Start()
		{
			string text = GetPath() + "historymap.map";
			FileInfo fileInfo = new FileInfo(text);
			if (File.Exists(text))
			{
				BinaryReader binaryReader = new BinaryReader(fileInfo.OpenRead());
				int num = binaryReader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					RecordItemData recordItemData = new RecordItemData();
					recordItemData.trainType = binaryReader.ReadInt32();
					recordItemData.item = binaryReader.ReadInt32();
					recordItemData.other = binaryReader.ReadInt32();
					recordItemData.time = binaryReader.ReadString();
					recordItemData.isManual = binaryReader.ReadBoolean();
					recordItemData.carType = binaryReader.ReadInt32();
					recordItemData.pathindex = binaryReader.ReadInt32();
					recordItemData.scenesname = binaryReader.ReadInt32();
					recordItemDataList.Add(recordItemData);
				}
				if (num >= historyIndex)
				{
					recordItemDataList.RemoveAt(0);
				}
				binaryReader.Close();
			}
			FileStream fileStream = fileInfo.Create();
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			RecordItemData recordItemData2 = new RecordItemData();
			recordItemData2.trainType = (int)ViewJumpCtl.Instance.trainType;
			recordItemData2.item = ViewJumpCtl.Instance.itemType;
			recordItemData2.other = ViewJumpCtl.Instance.otherType;
			recordItemData2.time = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
			recordItemData2.isManual = SettingInfoCtl.Instance.isManualGear;
			recordItemData2.carType = (int)SettingInfoCtl.Instance.carType;
			recordItemData2.pathindex = PlayerPrefs.GetInt("i") + 1;
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
			{
				recordItemData2.scenesname = (int)SceneJumpCtl.Instance.CoursetwoName;
			}
			else if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
			{
				recordItemData2.scenesname = (int)SceneJumpCtl.Instance.CoursethreeName;
			}
			recordItemDataList.Add(recordItemData2);
			binaryWriter.Write(recordItemDataList.Count);
			foreach (RecordItemData recordItemData3 in recordItemDataList)
			{
				binaryWriter.Write(recordItemData3.trainType);
				binaryWriter.Write(recordItemData3.item);
				binaryWriter.Write(recordItemData3.other);
				binaryWriter.Write(recordItemData3.time);
				binaryWriter.Write(recordItemData3.isManual);
				binaryWriter.Write(recordItemData3.carType);
				binaryWriter.Write(recordItemData3.pathindex);
				binaryWriter.Write(recordItemData3.scenesname);
			}
			binaryWriter.Flush();
			binaryWriter.Close();
			fileStream.Close();
		}

		private void FixedUpdate()
		{
			DriveHistory driveHistory = new DriveHistory();
			driveHistory.pos = CarState.Instance.CurCar.transform.position;
			driveHistory.localEuler = CarState.Instance.CurCar.transform.localEulerAngles;
			driveHistory.speed = CarState.Instance.CurSpeed;
			driveHistory.engineSpeed = CarState.Instance.CurEngineSpeed;
			driveHistory.carSignal = CarState.Instance.CarSignal;
			carSignalList.Add(driveHistory);
		}

		public void SaveInfo()
		{
			FileStream fileStream = new FileInfo(GetPath() + "history" + SettingInfoCtl.Instance.isManualGear.ToString() + (int)SettingInfoCtl.Instance.carType + ViewJumpCtl.Instance.itemType + PlayerPrefs.GetInt("i") + ".bytes").Create();
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(carSignalList.Count);
			foreach (DriveHistory carSignal in carSignalList)
			{
				binaryWriter.Write(carSignal.speed);
				binaryWriter.Write(carSignal.engineSpeed);
				binaryWriter.Write(carSignal.pos.x);
				binaryWriter.Write(carSignal.pos.y);
				binaryWriter.Write(carSignal.pos.z);
				binaryWriter.Write(carSignal.localEuler.x);
				binaryWriter.Write(carSignal.localEuler.y);
				binaryWriter.Write(carSignal.localEuler.z);
				binaryWriter.Write(carSignal.carSignal.oil);
				binaryWriter.Write(carSignal.carSignal.footBrake);
				binaryWriter.Write(carSignal.carSignal.handBrake);
				binaryWriter.Write(carSignal.carSignal.cluth);
				binaryWriter.Write(carSignal.carSignal.steerWheel);
				binaryWriter.Write(carSignal.carSignal.gear);
				binaryWriter.Write(carSignal.carSignal.turnLight);
				binaryWriter.Write(carSignal.carSignal.horn);
				binaryWriter.Write(carSignal.carSignal.fire);
				binaryWriter.Write(carSignal.carSignal.power);
				binaryWriter.Write(carSignal.carSignal.safeBelt);
				binaryWriter.Write(carSignal.carSignal.door);
				binaryWriter.Write(carSignal.carSignal.rainBrush);
				binaryWriter.Write(carSignal.carSignal.frontLight);
				binaryWriter.Write(carSignal.carSignal.widthLight);
				binaryWriter.Write(carSignal.carSignal.fogLight);
				binaryWriter.Write(carSignal.carSignal.sprayWater);
			}
			binaryWriter.Flush();
			binaryWriter.Close();
			fileStream.Close();
		}

		public int GetIndex()
		{
			if (File.Exists(GetUserPath() + "history" + SettingInfoCtl.Instance.isManualGear.ToString() + (int)SettingInfoCtl.Instance.carType + ViewJumpCtl.Instance.itemType + index + SettingInfoCtl.Instance.isCorrect.ToString() + ".bytes"))
			{
				index++;
				GetIndex();
			}
			return index;
		}

		public void SaveYanShiNum()
		{
			if (GetIndex() > 0 && SettingInfoCtl.Instance.isCorrect)
			{
				ViewJumpCtl.Instance.ShowMsgTips("演示视频已存在");
			}
			else if (SettingInfoCtl.Instance.isCorrect)
			{
				ViewJumpCtl.Instance.ShowMsgTips("合格,已保存到正确演示操作中");
				SaveUserInfo();
			}
			else if (GetIndex() > 0 && !SettingInfoCtl.Instance.isCorrect)
			{
				ViewJumpCtl.Instance.ShowMsgTips("演示视频已存在");
			}
			else if (!SettingInfoCtl.Instance.isCorrect)
			{
				ViewJumpCtl.Instance.ShowMsgTips("不合格,已保存到错误演示操作中");
				SaveUserInfo();
			}
		}

		public void SaveUserInfo()
		{
			FileInfo fileInfo = new FileInfo(GetUserPath() + "history" + SettingInfoCtl.Instance.isManualGear.ToString() + (int)SettingInfoCtl.Instance.carType + ViewJumpCtl.Instance.itemType + GetIndex() + SettingInfoCtl.Instance.isCorrect.ToString() + ".bytes");
			Debug.Log(index);
			Debug.Log(carSignalList.Count);
			FileStream fileStream = fileInfo.Create();
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(carSignalList.Count);
			foreach (DriveHistory carSignal in carSignalList)
			{
				binaryWriter.Write(carSignal.speed);
				binaryWriter.Write(carSignal.engineSpeed);
				binaryWriter.Write(carSignal.pos.x);
				binaryWriter.Write(carSignal.pos.y);
				binaryWriter.Write(carSignal.pos.z);
				binaryWriter.Write(carSignal.localEuler.x);
				binaryWriter.Write(carSignal.localEuler.y);
				binaryWriter.Write(carSignal.localEuler.z);
				binaryWriter.Write(carSignal.carSignal.oil);
				binaryWriter.Write(carSignal.carSignal.footBrake);
				binaryWriter.Write(carSignal.carSignal.handBrake);
				binaryWriter.Write(carSignal.carSignal.cluth);
				binaryWriter.Write(carSignal.carSignal.steerWheel);
				binaryWriter.Write(carSignal.carSignal.gear);
				binaryWriter.Write(carSignal.carSignal.turnLight);
				binaryWriter.Write(carSignal.carSignal.horn);
				binaryWriter.Write(carSignal.carSignal.fire);
				binaryWriter.Write(carSignal.carSignal.power);
				binaryWriter.Write(carSignal.carSignal.safeBelt);
				binaryWriter.Write(carSignal.carSignal.door);
				binaryWriter.Write(carSignal.carSignal.rainBrush);
				binaryWriter.Write(carSignal.carSignal.frontLight);
				binaryWriter.Write(carSignal.carSignal.widthLight);
				binaryWriter.Write(carSignal.carSignal.fogLight);
				binaryWriter.Write(carSignal.carSignal.sprayWater);
			}
			binaryWriter.Flush();
			binaryWriter.Close();
			fileStream.Close();
		}

		private void OnDestroy()
		{
			PlayerPrefs.SetInt("i", PlayerPrefs.GetInt("i") + 1);
			SaveInfo();
		}

		public string GetPath()
		{
			string text = "";
			text = ((Application.platform == RuntimePlatform.WindowsPlayer) ? (Application.streamingAssetsPath + "/RecordFiles/") : ((Application.platform != RuntimePlatform.Android) ? (Application.streamingAssetsPath + "/RecordFiles/") : (Application.persistentDataPath + "/RecordFiles/")));
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		public string GetUserPath()
		{
			string text = "";
			text = ((Application.platform == RuntimePlatform.WindowsPlayer) ? (Application.streamingAssetsPath + "/UserHistoryFile/") : ((Application.platform != RuntimePlatform.Android) ? (Application.streamingAssetsPath + "/UserHistoryFile/") : (Application.persistentDataPath + "/UserHistoryFile/")));
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}
	}
}
