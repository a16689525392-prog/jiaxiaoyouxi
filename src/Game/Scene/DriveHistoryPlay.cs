using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Car;
using Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;
using View;

namespace Scene
{
	public class DriveHistoryPlay : MonoBehaviour
	{
		public static DriveHistoryPlay Instance;

		public float curCalculateSpeedH;

		public float curCalculateEngineSpeedH;

		public bool isLoadHistoryEnd;

		public List<DriveHistory> carDriveRecordList = new List<DriveHistory>();

		public bool isAuto;

		public int curIndexPlay;

		private float floatIndex;

		private float timeNum = 6f;

		private GameObject countDownPrefabs;

		private bool isCountDown;

		private GameObject showYanShiText;

		private Color isCorrectTextColor = Color.blue;

		private Color isErrorTextColor = Color.red;

		private GameObject timeNumText;

		private GameObject goLine;

		private int curShowPathGear;

		private int curShowPathIndex;

		private List<Vector3> posList = new List<Vector3>();

		private LineRenderer lineRender;

		public bool MySpeed1;

		public bool MySpeed2;

		public int Myspeed;

		private void Awake()
		{
			Instance = this;
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetAction = (UnityAction)Delegate.Combine(baseInstance.ResetAction, new UnityAction(Reset));
		}

		private void Start()
		{
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			showYanShiText = GameObject.Find("GameGlobalCtl(Clone)/Canvas/SceneText/YanShiText");
			timeNumText = GameObject.Find("GameGlobalCtl(Clone)/Canvas/SceneText/CountNum");
			GetItemTime();
		}

		private void FixedUpdate()
		{
			if (isLoadHistoryEnd && curIndexPlay < carDriveRecordList.Count - 1)
			{
				CalculateSpeed();
				SetCarPosition();
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree && ViewJumpCtl.Instance.modelType == 0)
				{
					if (CourseThreeYanShiGuide.Instance != null && !CourseThreeYanShiGuide.Instance.YanShiPause)
					{
						CarDrive();
					}
				}
				else
				{
					CarDrive();
				}
				ShowDrivePath();
			}
			else
			{
				curCalculateSpeedH = 0f;
				curCalculateEngineSpeedH = 0f;
				if (isLoadHistoryEnd)
				{
					if (isAuto)
					{
						CarState.Instance.CurCar.transform.localPosition = carDriveRecordList[carDriveRecordList.Count - 1].pos;
					}
					else
					{
						CarState.Instance.CurCar.transform.position = carDriveRecordList[carDriveRecordList.Count - 1].pos;
					}
					CarState.Instance.CurCar.transform.localEulerAngles = carDriveRecordList[carDriveRecordList.Count - 1].localEuler;
				}
			}
			if (ViewJumpCtl.Instance.modelType == 0)
			{
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
				{
					if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
					{
						if (curIndexPlay >= 5560 && ViewJumpCtl.Instance.itemType == 1)
						{
							CountDown();
						}
						else if (curIndexPlay >= 12648 && ViewJumpCtl.Instance.itemType == 5)
						{
							CountDown();
						}
						else if (curIndexPlay >= 3885 && ViewJumpCtl.Instance.itemType == 2)
						{
							CountDown();
						}
						else if (curIndexPlay >= 6003 && ViewJumpCtl.Instance.itemType == 3)
						{
							CountDown();
						}
						else if (curIndexPlay >= 2772 && ViewJumpCtl.Instance.itemType == 4)
						{
							CountDown();
						}
						else if (curIndexPlay >= 3361 && ViewJumpCtl.Instance.itemType == 7)
						{
							CountDown();
						}
						else if (curIndexPlay >= 2431 && ViewJumpCtl.Instance.itemType == 8)
						{
							CountDown();
						}
						else if (curIndexPlay >= 5094 && ViewJumpCtl.Instance.itemType == 6)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1863 && ViewJumpCtl.Instance.itemType == 9)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1798 && ViewJumpCtl.Instance.itemType == 13)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1698 && ViewJumpCtl.Instance.itemType == 12)
						{
							CountDown();
						}
						else if (curIndexPlay >= 2566 && ViewJumpCtl.Instance.itemType == 11)
						{
							CountDown();
						}
						else if (curIndexPlay >= 3585 && ViewJumpCtl.Instance.itemType == 14)
						{
							CountDown();
						}
						else if (curIndexPlay >= 4260 && ViewJumpCtl.Instance.itemType == 15)
						{
							CountDown();
						}
						else if (curIndexPlay >= 8335 && ViewJumpCtl.Instance.itemType == 16)
						{
							CountDown();
						}
						else if (curIndexPlay >= 5697 && ViewJumpCtl.Instance.itemType == 10)
						{
							CountDown();
						}
					}
					else if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
					{
						if (curIndexPlay >= 109600 && ViewJumpCtl.Instance.itemType == 0)
						{
							CountDown();
						}
						else if (curIndexPlay >= 77680 && ViewJumpCtl.Instance.itemType == 1)
						{
							CountDown();
						}
						else if (curIndexPlay >= 37130 && ViewJumpCtl.Instance.itemType == 2)
						{
							CountDown();
						}
						else if (curIndexPlay >= 42430 && ViewJumpCtl.Instance.itemType == 3)
						{
							CountDown();
						}
						else if (curIndexPlay >= 20500 && ViewJumpCtl.Instance.itemType == 4)
						{
							CountDown();
						}
					}
				}
				else
				{
					if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree || ViewJumpCtl.Instance.trainType != TrainType.YingjiXunlian)
					{
						return;
					}
					if (SettingInfoCtl.Instance.isCorrect)
					{
						TishiText0();
						if (curIndexPlay >= 2961 && ViewJumpCtl.Instance.itemType == 601)
						{
							CountDown();
						}
						else if (curIndexPlay >= 2484 && ViewJumpCtl.Instance.itemType == 602)
						{
							CountDown();
						}
						else if (curIndexPlay >= 348 && ViewJumpCtl.Instance.itemType == 603)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1290 && ViewJumpCtl.Instance.itemType == 604)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1114 && ViewJumpCtl.Instance.itemType == 605)
						{
							CountDown();
						}
						else if (curIndexPlay >= 4758 && ViewJumpCtl.Instance.itemType == 606)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1266 && ViewJumpCtl.Instance.itemType == 607)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1917 && ViewJumpCtl.Instance.itemType == 608)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1314 && ViewJumpCtl.Instance.itemType == 609)
						{
							CountDown();
						}
						else if (curIndexPlay >= 2265 && ViewJumpCtl.Instance.itemType == 611)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1683 && ViewJumpCtl.Instance.itemType == 612)
						{
							CountDown();
						}
					}
					else if (!SettingInfoCtl.Instance.isCorrect)
					{
						TishiText1();
						if (curIndexPlay >= 1807 && ViewJumpCtl.Instance.itemType == 601)
						{
							CountDown();
						}
						else if (curIndexPlay >= 2328 && ViewJumpCtl.Instance.itemType == 602)
						{
							CountDown();
						}
						else if (curIndexPlay >= 736 && ViewJumpCtl.Instance.itemType == 603)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1306 && ViewJumpCtl.Instance.itemType == 604)
						{
							CountDown();
						}
						else if (curIndexPlay >= 2421 && ViewJumpCtl.Instance.itemType == 605)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1440 && ViewJumpCtl.Instance.itemType == 606)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1647 && ViewJumpCtl.Instance.itemType == 607)
						{
							CountDown();
						}
						else if (curIndexPlay >= 2115 && ViewJumpCtl.Instance.itemType == 608)
						{
							CountDown();
						}
						else if (curIndexPlay >= 1453 && ViewJumpCtl.Instance.itemType == 609)
						{
							CountDown();
						}
						else if (curIndexPlay >= 3688 && ViewJumpCtl.Instance.itemType == 611)
						{
							CountDown();
						}
						else if (curIndexPlay >= 2587 && ViewJumpCtl.Instance.itemType == 612)
						{
							CountDown();
						}
					}
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 5 && curIndexPlay >= carDriveRecordList.Count - 1)
			{
				CountDown();
			}
		}

		private void CountDown()
		{
			if (timeNum >= 0f)
			{
				timeNum -= Time.deltaTime;
				timeNumText.GetComponent<Text>().text = "播放结束，5秒后将重新播放!";
				if (!isCountDown)
				{
					countDownPrefabs = ViewJumpCtl.Instance.CreateViewPrefab("YanShiCountDown", Vector3.zero);
					isCountDown = true;
				}
			}
			else
			{
				TrainSceneBase.BaseInstance.ResetTime();
			}
		}

		private void TishiText0()
		{
			showYanShiText.GetComponent<Text>().text = "正确操作演示";
			showYanShiText.GetComponent<Text>().color = isCorrectTextColor;
		}

		private void TishiText1()
		{
			showYanShiText.GetComponent<Text>().text = "错误操作演示";
			showYanShiText.GetComponent<Text>().color = isErrorTextColor;
		}

		private void GetItemTime()
		{
		}

		private void ShowDrivePath()
		{
			if (ViewJumpCtl.Instance.modelType == 0)
			{
				if (ViewJumpCtl.Instance.trainType == TrainType.YingjiXunlian || ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
				{
					if (lineRender == null)
					{
						lineRender = CreateLineRenderer();
					}
					if (curIndexPlay >= curShowPathIndex)
					{
						ShowCourseThreePath();
					}
				}
				else if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang)
				{
					if (lineRender == null)
					{
						lineRender = CreateLineRenderer();
					}
					if (curIndexPlay >= curShowPathIndex)
					{
						ShowCourseTwoDirvePath();
					}
				}
			}
			else
			{
				if (ViewJumpCtl.Instance.modelType != 5)
				{
					return;
				}
				if (ViewJumpCtl.Instance.itemType == 0 || ViewJumpCtl.Instance.itemType == 1)
				{
					if (lineRender == null)
					{
						lineRender = CreateLineRenderer();
					}
					if (curIndexPlay >= curShowPathIndex)
					{
						ShowCourseTwoDirvePath();
					}
				}
				else
				{
					if (lineRender == null)
					{
						lineRender = CreateLineRenderer();
					}
					if (curIndexPlay >= curShowPathIndex)
					{
						ShowCourseThreePath();
					}
				}
			}
		}

		private void ShowCourseThreePath()
		{
			if (curIndexPlay <= curShowPathIndex)
			{
				return;
			}
			posList.Add(carDriveRecordList[curShowPathIndex].pos);
			while (curShowPathIndex < carDriveRecordList.Count)
			{
				curShowPathIndex++;
				if (curShowPathIndex % 10 == 0 && Vector3.Distance(carDriveRecordList[curShowPathIndex - 1].pos, posList[posList.Count - 1]) > 0.3f)
				{
					posList.Add(carDriveRecordList[curShowPathIndex - 1].pos);
				}
			}
			SetLineRenderPositions(posList);
		}

		private void ShowCourseTwoDirvePath()
		{
			if (curIndexPlay >= carDriveRecordList.Count)
			{
				if (lineRender != null)
				{
					lineRender.positionCount = 0;
				}
				return;
			}
			if (ViewJumpCtl.Instance.trainType != TrainType.NanChangKaoChang)
			{
				while (curShowPathIndex < carDriveRecordList.Count && (carDriveRecordList[curShowPathIndex].carSignal.gear == curShowPathGear || carDriveRecordList[curShowPathIndex].carSignal.gear == 0))
				{
					curShowPathIndex++;
				}
			}
			else
			{
				while (curShowPathIndex < carDriveRecordList.Count && carDriveRecordList[curShowPathIndex].carSignal.gear == curShowPathGear)
				{
					curShowPathIndex++;
				}
			}
			if (curShowPathIndex < carDriveRecordList.Count)
			{
				curShowPathGear = carDriveRecordList[curShowPathIndex].carSignal.gear;
			}
			posList.Clear();
			posList.Add(carDriveRecordList[curIndexPlay].pos);
			while (curShowPathIndex < carDriveRecordList.Count && carDriveRecordList[curShowPathIndex].carSignal.gear == curShowPathGear)
			{
				curShowPathIndex++;
				if (curShowPathIndex % 50 == 0 && Vector3.Distance(carDriveRecordList[curShowPathIndex].pos, posList[posList.Count - 1]) > 0.2f)
				{
					posList.Add(carDriveRecordList[curShowPathIndex].pos);
				}
			}
			SetLineRenderPositions(posList);
		}

		private void SetLineRenderPositions(List<Vector3> positions)
		{
			lineRender.positionCount = positions.Count;
			for (int i = 0; i < positions.Count; i++)
			{
				if (isAuto)
				{
					switch (ViewJumpCtl.Instance.itemType)
					{
					case 0:
						positions[i] = GameObject.Find("DaoKuGuideModeArea").transform.TransformPoint(positions[i]) + new Vector3(0f, 0.2f, 0f);
						break;
					case 1:
						positions[i] = GameObject.Find("CeFangGuideModeArea").transform.TransformPoint(positions[i]) + new Vector3(0f, 0.2f, 0f);
						break;
					case 2:
						positions[i] = GameObject.Find("PoDaoGuideModeArea").transform.TransformPoint(positions[i]) + new Vector3(0f, 0.2f, 0f);
						break;
					case 3:
						positions[i] = GameObject.Find("QuXianGuideModeArea").transform.TransformPoint(positions[i]) + new Vector3(0f, 0.2f, 0f);
						break;
					case 4:
						positions[i] = GameObject.Find("ZhiJiaoGuideModeArea").transform.TransformPoint(positions[i]) + new Vector3(0f, 0.2f, 0f);
						break;
					}
				}
				else
				{
					positions[i] += new Vector3(0f, 0.2f, 0f);
				}
			}
			lineRender.SetPositions(positions.ToArray());
			lineRender.material.mainTextureScale = new Vector2(positions.Count / 3, 1f);
		}

		private LineRenderer CreateLineRenderer()
		{
			goLine = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("DriveLineRenderer"));
			goLine.transform.SetParent(TrainSceneBase.BaseInstance.transform);
			return goLine.GetComponent<LineRenderer>();
		}

		private void SetCarPosition()
		{
			if (isAuto)
			{
				CarState.Instance.CurCar.transform.localPosition = carDriveRecordList[curIndexPlay].pos;
			}
			else
			{
				CarState.Instance.CurCar.transform.position = carDriveRecordList[curIndexPlay].pos;
			}
			CarState.Instance.CurCar.transform.localEulerAngles = carDriveRecordList[curIndexPlay].localEuler;
		}

		private void CalculateSpeed()
		{
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang)
			{
				curCalculateSpeedH = carDriveRecordList[curIndexPlay].speed;
				if (CarState.Instance.FireOn && carDriveRecordList[curIndexPlay].carSignal.gear != 0 && carDriveRecordList[curIndexPlay].carSignal.gear != 104)
				{
					if (curCalculateEngineSpeedH < 840f)
					{
						curCalculateEngineSpeedH = 800f + 3f * carDriveRecordList[curIndexPlay].speed + 1f * carDriveRecordList[curIndexPlay].carSignal.cluth - 1f * carDriveRecordList[curIndexPlay].carSignal.footBrake;
					}
					else
					{
						curCalculateEngineSpeedH = 840f + 3f * carDriveRecordList[curIndexPlay].speed + 5f * carDriveRecordList[curIndexPlay].carSignal.cluth - 15f * carDriveRecordList[curIndexPlay].carSignal.footBrake;
					}
				}
				else
				{
					curCalculateEngineSpeedH = 835f + 3f * carDriveRecordList[curIndexPlay].speed + 15f * carDriveRecordList[curIndexPlay].carSignal.cluth - 25f * carDriveRecordList[curIndexPlay].carSignal.footBrake;
				}
			}
			else
			{
				curCalculateSpeedH = carDriveRecordList[curIndexPlay].speed;
				curCalculateEngineSpeedH = carDriveRecordList[curIndexPlay].engineSpeed;
			}
		}

		private void Update()
		{
			if (MySpeed1 && MySpeed2)
			{
				Myspeed = 6;
			}
			else if (MySpeed1)
			{
				Myspeed = 3;
			}
			else
			{
				Myspeed = 1;
			}
		}

		private void CarDrive()
		{
			if (ViewJumpCtl.Instance.modelType == 0)
			{
				if (Application.platform == RuntimePlatform.WindowsPlayer)
				{
					floatIndex += 1f;
				}
				else if (Application.platform == RuntimePlatform.Android)
				{
					floatIndex += 1f;
				}
				else
				{
					floatIndex += 1f * (float)Myspeed;
				}
			}
			if (ViewJumpCtl.Instance.modelType == 5)
			{
				if (Application.platform == RuntimePlatform.WindowsPlayer)
				{
					floatIndex += 1f;
				}
				else if (Application.platform == RuntimePlatform.Android)
				{
					floatIndex += 1f;
				}
				else
				{
					floatIndex += 1f;
				}
			}
			curIndexPlay = Mathf.FloorToInt(floatIndex);
		}

		private void ResetTimePlay()
		{
			posList.Clear();
			UnityEngine.Object.Destroy(goLine);
			if (ViewJumpCtl.Instance.trainType != TrainType.CourseTwo && ViewJumpCtl.Instance.trainType != TrainType.NanChangKaoChang)
			{
				lineRender = null;
			}
			else if (lineRender != null && (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang))
			{
				lineRender.positionCount = 0;
				curShowPathGear = 0;
			}
			curIndexPlay = 0;
			floatIndex = 0f;
			curShowPathIndex = 0;
			curCalculateSpeedH = 0f;
			curCalculateEngineSpeedH = 0f;
			if (carDriveRecordList != null && carDriveRecordList.Count > 0)
			{
				CarState.Instance.CurCar.transform.position = carDriveRecordList[0].pos;
				CarState.Instance.CurCar.transform.localEulerAngles = carDriveRecordList[0].localEuler;
			}
			if (lineRender != null && (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang))
			{
				lineRender.positionCount = 0;
				curShowPathIndex = 0;
				curShowPathGear = 0;
				posList.Clear();
			}
			timeNum = 6f;
			timeNumText.GetComponent<Text>().text = "";
			UnityEngine.Object.Destroy(countDownPrefabs);
			isCountDown = false;
			GetItemTime();
		}

		private void Reset()
		{
			posList.Clear();
			UnityEngine.Object.Destroy(goLine);
			if (ViewJumpCtl.Instance.trainType != TrainType.CourseTwo && ViewJumpCtl.Instance.trainType != TrainType.NanChangKaoChang)
			{
				lineRender = null;
			}
			else if (lineRender != null && (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang))
			{
				lineRender.positionCount = 0;
				curShowPathGear = 0;
			}
			curIndexPlay = 0;
			floatIndex = 0f;
			curShowPathIndex = 0;
			curCalculateSpeedH = 0f;
			curCalculateEngineSpeedH = 0f;
			if (carDriveRecordList != null && carDriveRecordList.Count > 0)
			{
				CarState.Instance.CurCar.transform.position = carDriveRecordList[0].pos;
				CarState.Instance.CurCar.transform.localEulerAngles = carDriveRecordList[0].localEuler;
			}
			timeNum = 6f;
			timeNumText.GetComponent<Text>().text = "";
			UnityEngine.Object.Destroy(countDownPrefabs);
			isCountDown = false;
			GetItemTime();
		}

		public IEnumerator LoadHistoryDriveFiles(string path)
		{
			carDriveRecordList.Clear();
			FileStream input = new FileInfo(path).OpenRead();
			new BinaryFormatter();
			BinaryReader binaryReader = new BinaryReader(input);
			int num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				DriveHistory driveHistory = new DriveHistory();
				driveHistory.speed = binaryReader.ReadSingle();
				driveHistory.engineSpeed = binaryReader.ReadSingle();
				driveHistory.pos.x = binaryReader.ReadSingle();
				driveHistory.pos.y = binaryReader.ReadSingle();
				driveHistory.pos.z = binaryReader.ReadSingle();
				driveHistory.localEuler.x = binaryReader.ReadSingle();
				driveHistory.localEuler.y = binaryReader.ReadSingle();
				driveHistory.localEuler.z = binaryReader.ReadSingle();
				driveHistory.carSignal.oil = binaryReader.ReadSingle();
				driveHistory.carSignal.footBrake = binaryReader.ReadSingle();
				driveHistory.carSignal.handBrake = binaryReader.ReadSingle();
				driveHistory.carSignal.cluth = binaryReader.ReadSingle();
				driveHistory.carSignal.steerWheel = binaryReader.ReadSingle();
				driveHistory.carSignal.gear = binaryReader.ReadInt32();
				driveHistory.carSignal.turnLight = binaryReader.ReadInt32();
				driveHistory.carSignal.horn = binaryReader.ReadInt32();
				driveHistory.carSignal.fire = binaryReader.ReadInt32();
				driveHistory.carSignal.power = binaryReader.ReadInt32();
				driveHistory.carSignal.safeBelt = binaryReader.ReadInt32();
				driveHistory.carSignal.door = binaryReader.ReadInt32();
				driveHistory.carSignal.rainBrush = binaryReader.ReadInt32();
				driveHistory.carSignal.frontLight = binaryReader.ReadInt32();
				driveHistory.carSignal.widthLight = binaryReader.ReadInt32();
				driveHistory.carSignal.fogLight = binaryReader.ReadInt32();
				driveHistory.carSignal.sprayWater = binaryReader.ReadInt32();
				carDriveRecordList.Add(driveHistory);
			}
			binaryReader.Close();
			isLoadHistoryEnd = true;
			yield return 0;
		}

		public IEnumerator LoadYanShiDriveFiles(string path)
		{
			carDriveRecordList.Clear();
			string text = "";
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
			{
				switch (SceneJumpCtl.Instance.CoursetwoName)
				{
				case CoursetwoScenesName.YiAnDa:
					text = "/HistoryData/CourseTwo/YiAnDa/";
					break;
				case CoursetwoScenesName.QingHu:
					text = "/HistoryData/CourseTwo/QingHu/";
					break;
				case CoursetwoScenesName.WenZhou:
					text = "/HistoryData/CourseTwo/WenZhou/";
					break;
				case CoursetwoScenesName.HeBei:
					text = "/HistoryData/CourseTwo/HeBei/";
					break;
				case CoursetwoScenesName.HuNanYZ:
					text = "/HistoryData/CourseTwo/HuNanYZ/";
					break;
				case CoursetwoScenesName.ShiJiaZhuang:
					text = "/HistoryData/CourseTwo/ShiJiaZhuang/";
					break;
				case CoursetwoScenesName.FoShan:
					text = "/HistoryData/CourseTwo/FoShan/";
					break;
				case CoursetwoScenesName.NanFeng:
					text = "/HistoryData/CourseTwo/NanFeng/";
					break;
				}
			}
			else if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
			{
				switch (SceneJumpCtl.Instance.CoursethreeName)
				{
				case CoursethreeScenesName.NanChang:
					text = "/HistoryData/CourseThree/NanChang/";
					break;
				case CoursethreeScenesName.HuNanYZ:
					text = "/HistoryData/CourseThree/HuNanYZ/";
					break;
				case CoursethreeScenesName.RenCai:
					text = "/HistoryData/CourseThree/RenCai/";
					break;
				case CoursethreeScenesName.ChengDong:
					text = "/HistoryData/CourseThree/ChengDong/";
					break;
				case CoursethreeScenesName.BaiYun:
					text = "/HistoryData/CourseThree/BaiYun/";
					break;
				}
			}
			UnityWebRequest www = UnityWebRequest.Get(DataCenter.Instance.GetWWWPath(Application.streamingAssetsPath + text + path));
			yield return www.SendWebRequest();
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(www.downloadHandler.data));
			int num = binaryReader.ReadInt32();
			int num2;
			if (num == -1)
			{
				isAuto = true;
				num2 = binaryReader.ReadInt32();
				switch (ViewJumpCtl.Instance.itemType)
				{
				case 0:
					if ((bool)GameObject.Find("DaoKuGuideModeArea"))
					{
						CarState.Instance.CurCar.transform.parent = GameObject.Find("DaoKuGuideModeArea").transform;
					}
					break;
				case 1:
					if ((bool)GameObject.Find("CeFangGuideModeArea"))
					{
						CarState.Instance.CurCar.transform.parent = GameObject.Find("CeFangGuideModeArea").transform;
					}
					break;
				case 2:
					if ((bool)GameObject.Find("PoDaoGuideModeArea"))
					{
						CarState.Instance.CurCar.transform.parent = GameObject.Find("PoDaoGuideModeArea").transform;
					}
					break;
				case 3:
					if ((bool)GameObject.Find("QuXianGuideModeArea"))
					{
						CarState.Instance.CurCar.transform.parent = GameObject.Find("QuXianGuideModeArea").transform;
					}
					break;
				case 4:
					if ((bool)GameObject.Find("ZhiJiaoGuideModeArea"))
					{
						CarState.Instance.CurCar.transform.parent = GameObject.Find("ZhiJiaoGuideModeArea").transform;
					}
					break;
				}
			}
			else
			{
				isAuto = false;
				num2 = num;
			}
			for (int i = 0; i < num2; i++)
			{
				DriveHistory driveHistory = new DriveHistory();
				driveHistory.speed = binaryReader.ReadSingle();
				driveHistory.engineSpeed = binaryReader.ReadSingle();
				driveHistory.pos.x = binaryReader.ReadSingle();
				driveHistory.pos.y = binaryReader.ReadSingle();
				driveHistory.pos.z = binaryReader.ReadSingle();
				driveHistory.localEuler.x = binaryReader.ReadSingle();
				driveHistory.localEuler.y = binaryReader.ReadSingle();
				driveHistory.localEuler.z = binaryReader.ReadSingle();
				driveHistory.carSignal.oil = binaryReader.ReadSingle();
				driveHistory.carSignal.footBrake = binaryReader.ReadSingle();
				driveHistory.carSignal.handBrake = binaryReader.ReadSingle();
				driveHistory.carSignal.cluth = binaryReader.ReadSingle();
				driveHistory.carSignal.steerWheel = binaryReader.ReadSingle();
				driveHistory.carSignal.gear = binaryReader.ReadInt32();
				driveHistory.carSignal.turnLight = binaryReader.ReadInt32();
				driveHistory.carSignal.horn = binaryReader.ReadInt32();
				driveHistory.carSignal.fire = binaryReader.ReadInt32();
				driveHistory.carSignal.power = binaryReader.ReadInt32();
				driveHistory.carSignal.safeBelt = binaryReader.ReadInt32();
				driveHistory.carSignal.door = binaryReader.ReadInt32();
				driveHistory.carSignal.rainBrush = binaryReader.ReadInt32();
				driveHistory.carSignal.frontLight = binaryReader.ReadInt32();
				driveHistory.carSignal.widthLight = binaryReader.ReadInt32();
				driveHistory.carSignal.fogLight = binaryReader.ReadInt32();
				driveHistory.carSignal.sprayWater = binaryReader.ReadInt32();
				carDriveRecordList.Add(driveHistory);
			}
			binaryReader.Close();
			isLoadHistoryEnd = true;
			yield return 0;
		}

		public IEnumerator LoadHistoryRecordFiles(string path)
		{
			carDriveRecordList.Clear();
			ResourceRequest request = Resources.LoadAsync<TextAsset>(path);
			yield return request;
			BinaryReader binaryReader = new BinaryReader(new MemoryStream((request.asset as TextAsset).bytes));
			int num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				DriveHistory driveHistory = new DriveHistory();
				driveHistory.speed = binaryReader.ReadSingle();
				driveHistory.engineSpeed = binaryReader.ReadSingle();
				driveHistory.pos.x = binaryReader.ReadSingle();
				driveHistory.pos.y = binaryReader.ReadSingle();
				driveHistory.pos.z = binaryReader.ReadSingle();
				driveHistory.localEuler.x = binaryReader.ReadSingle();
				driveHistory.localEuler.y = binaryReader.ReadSingle();
				driveHistory.localEuler.z = binaryReader.ReadSingle();
				driveHistory.carSignal.oil = binaryReader.ReadSingle();
				driveHistory.carSignal.footBrake = binaryReader.ReadSingle();
				driveHistory.carSignal.handBrake = binaryReader.ReadSingle();
				driveHistory.carSignal.cluth = binaryReader.ReadSingle();
				driveHistory.carSignal.steerWheel = binaryReader.ReadSingle();
				driveHistory.carSignal.gear = binaryReader.ReadInt32();
				driveHistory.carSignal.turnLight = binaryReader.ReadInt32();
				driveHistory.carSignal.horn = binaryReader.ReadInt32();
				driveHistory.carSignal.fire = binaryReader.ReadInt32();
				driveHistory.carSignal.power = binaryReader.ReadInt32();
				driveHistory.carSignal.safeBelt = binaryReader.ReadInt32();
				driveHistory.carSignal.door = binaryReader.ReadInt32();
				driveHistory.carSignal.rainBrush = binaryReader.ReadInt32();
				driveHistory.carSignal.frontLight = binaryReader.ReadInt32();
				driveHistory.carSignal.widthLight = binaryReader.ReadInt32();
				driveHistory.carSignal.fogLight = binaryReader.ReadInt32();
				driveHistory.carSignal.sprayWater = binaryReader.ReadInt32();
				carDriveRecordList.Add(driveHistory);
			}
			binaryReader.Close();
			isLoadHistoryEnd = true;
			yield return 0;
		}

		private void OnDestroy()
		{
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetAction = (UnityAction)Delegate.Remove(baseInstance.ResetAction, new UnityAction(Reset));
			TrainSceneBase baseInstance2 = TrainSceneBase.BaseInstance;
			baseInstance2.ResetTime = (UnityAction)Delegate.Remove(baseInstance2.ResetTime, new UnityAction(ResetTimePlay));
			if (ViewJumpCtl.Instance.trainType == TrainType.YingjiXunlian && showYanShiText != null)
			{
				showYanShiText.GetComponent<Text>().text = "";
			}
			if (timeNumText != null)
			{
				timeNumText.GetComponent<Text>().text = "";
			}
			UnityEngine.Object.Destroy(countDownPrefabs);
		}
	}
}
