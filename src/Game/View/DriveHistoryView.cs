using System.Collections;
using System.Collections.Generic;
using System.IO;
using Car;
using Global;
using Scene;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
	public class DriveHistoryView : ViewBase
	{
		public static DriveHistoryView instance;

		private int trainIndex;

		public List<Text> btnTime = new List<Text>();

		public List<Text> btnItem = new List<Text>();

		public List<GameObject> gameObjectsList = new List<GameObject>();

		public GameObject tips;

		public GameObject Title;

		public GameObject ClearButton;

		public int Pindex;

		private List<RecordItemData> recordList = new List<RecordItemData>();

		public override void Awake()
		{
			base.Awake();
			instance = this;
		}

		public override void Start()
		{
			base.Start();
			string text = "";
			text = ((Application.platform == RuntimePlatform.WindowsPlayer) ? (Application.streamingAssetsPath + "/RecordFiles/historymap.map") : ((Application.platform != RuntimePlatform.Android) ? (Application.streamingAssetsPath + "/RecordFiles/historymap.map") : (Application.persistentDataPath + "/RecordFiles/historymap.map")));
			if (File.Exists(text))
			{
				StartCoroutine(LoadHistoryDriveItemFiles(text));
			}
			for (int i = 0; i < recordList.Count; i++)
			{
				gameObjectsList[i].SetActive(value: true);
				btnTime[i].text = recordList[i].time;
				btnItem[i].text = GetItem(recordList[i].item) + GetItem(recordList[i].other);
			}
			for (int j = recordList.Count; j < gameObjectsList.Count; j++)
			{
				gameObjectsList[j].SetActive(value: false);
			}
			if (btnTime[0].text == "" || recordList.Count == 0)
			{
				tips.SetActive(value: true);
				Title.SetActive(value: false);
				ClearButton.SetActive(value: false);
			}
			else
			{
				tips.SetActive(value: false);
				Title.SetActive(value: true);
				ClearButton.SetActive(value: true);
			}
			if (Application.platform == RuntimePlatform.WindowsPlayer)
			{
				if (!File.Exists(Application.streamingAssetsPath + "/RecordFiles/historymap.map"))
				{
					tips.SetActive(value: true);
					Title.SetActive(value: false);
					ClearButton.SetActive(value: false);
				}
			}
			else if (Application.platform == RuntimePlatform.Android)
			{
				if (!File.Exists(Application.persistentDataPath + "/RecordFiles/historymap.map"))
				{
					tips.SetActive(value: true);
					Title.SetActive(value: false);
					ClearButton.SetActive(value: false);
				}
			}
			else if (!File.Exists(Application.streamingAssetsPath + "/RecordFiles/historymap.map"))
			{
				tips.SetActive(value: true);
				Title.SetActive(value: false);
				ClearButton.SetActive(value: false);
			}
		}

		private void Update()
		{
			if (Remove.refresh)
			{
				recordList.Clear();
				Start();
				Remove.refresh = false;
			}
		}

		public void OnClick()
		{
			switch (EventSystem.current.currentSelectedGameObject.name)
			{
			case "delete0":
				DeleteList(0);
				Remove.refresh = true;
				break;
			case "delete1":
				DeleteList(1);
				Remove.refresh = true;
				break;
			case "delete2":
				DeleteList(2);
				Remove.refresh = true;
				break;
			case "delete3":
				DeleteList(3);
				Remove.refresh = true;
				break;
			case "delete4":
				DeleteList(4);
				Remove.refresh = true;
				break;
			case "delete5":
				DeleteList(5);
				Remove.refresh = true;
				break;
			case "delete6":
				DeleteList(6);
				Remove.refresh = true;
				break;
			case "delete7":
				DeleteList(7);
				Remove.refresh = true;
				break;
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				Pindex = recordList[0].pathindex;
				GetItemData(0);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				Pindex = recordList[1].pathindex;
				GetItemData(1);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				Pindex = recordList[2].pathindex;
				GetItemData(2);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				Pindex = recordList[3].pathindex;
				GetItemData(3);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(4))
			{
				Pindex = recordList[4].pathindex;
				GetItemData(4);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(5))
			{
				Pindex = recordList[5].pathindex;
				GetItemData(5);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(6))
			{
				Pindex = recordList[6].pathindex;
				GetItemData(6);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(7))
			{
				Pindex = recordList[7].pathindex;
				GetItemData(7);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(8))
			{
				ViewJumpCtl.Instance.ReturnPreView();
				ViewJumpCtl.Instance.modelType = 2;
				ViewJumpCtl.Instance.trainType = TrainType.None;
			}
			if (go == GetBtn(8))
			{
				PlayerPrefs.SetInt("isrecordorplay", 1);
			}
			else
			{
				PlayerPrefs.SetInt("isrecordorplay", 2);
			}
		}

		private void GetItemData(int i)
		{
			if (i < recordList.Count)
			{
				ViewJumpCtl.Instance.modelType = 5;
				ViewJumpCtl.Instance.trainType = (TrainType)recordList[i].trainType;
				SettingInfoCtl.Instance.isManualGear = recordList[i].isManual;
				ViewJumpCtl.Instance.otherType = recordList[i].other;
				ViewJumpCtl.Instance.itemType = recordList[i].item;
				SettingInfoCtl.Instance.carType = (CarType)recordList[i].carType;
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
				{
					SceneJumpCtl.Instance.CoursetwoName = (CoursetwoScenesName)recordList[i].scenesname;
				}
				else if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
				{
					SceneJumpCtl.Instance.CoursethreeName = (CoursethreeScenesName)recordList[i].scenesname;
				}
			}
		}

		private string GetItem(int index)
		{
			string result = "";
			switch (index)
			{
			case 0:
				result = "科目二-倒车入库";
				break;
			case 1:
				if (SettingInfoCtl.Instance.carType != CarType.DongFengDuoLiKa)
				{
					result = "科目二-侧方停车";
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
				{
					result = "科目二-侧方停车(大车)";
				}
				break;
			case 2:
				if (SettingInfoCtl.Instance.carType != CarType.DongFengDuoLiKa)
				{
					result = "科目二-坡道定点";
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
				{
					result = "科目二-坡道定点(大车)";
				}
				break;
			case 3:
				if (SettingInfoCtl.Instance.carType != CarType.DongFengDuoLiKa)
				{
					result = "科目二-曲线行驶";
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
				{
					result = "科目二-曲线行驶(大车)";
				}
				break;
			case 4:
				if (SettingInfoCtl.Instance.carType != CarType.DongFengDuoLiKa)
				{
					result = "科目二-直角转弯";
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
				{
					result = "科目二-直角转弯(大车)";
				}
				break;
			case 1200:
				result = "科目二-考试模式";
				break;
			case -11:
				result = "定制科目二-倒车入库";
				break;
			case 111:
				result = "定制科目二-侧方停车";
				break;
			case 222:
				result = "定制科目二-坡道定点";
				break;
			case 333:
				result = "定制科目二-曲线行驶";
				break;
			case 444:
				result = "定制科目二-直角转弯";
				break;
			case 5:
				result = "科目二-桩考(大车)";
				break;
			case 6:
				result = "科目二-连续障碍(大车)";
				break;
			case 7:
				result = "科目二-单边桥(大车)";
				break;
			case 8:
				result = "科目二-起伏路(大车)";
				break;
			case 9:
				result = "科目二-限宽门(大车)";
				break;
			case 10:
				result = "科目二-窄路掉头(大车)";
				break;
			case 11:
				result = "科目二-湿滑路面(大车)";
				break;
			case 12:
				result = "科目二-模拟雨天(大车)";
				break;
			case 13:
				result = "科目二-模拟雾天(大车)";
				break;
			case 14:
				result = "科目二-模拟隧道(大车)";
				break;
			case 15:
				result = "科目二-模拟转弯(大车)";
				break;
			case 16:
				result = "科目二-模拟高速(大车)";
				break;
			case 100:
				result = "灯光考试-蓝天灯光";
				break;
			case 101:
				result = "灯光考试-白云灯光";
				break;
			case 200:
				result = "科目三-起步";
				break;
			case 201:
				result = "科目三-超车";
				break;
			case 202:
				result = "科目三-通过学校";
				break;
			case 203:
				result = "科目三-变更车道";
				break;
			case 204:
				result = "科目三-通过人形横道";
				break;
			case 205:
				result = "科目三-路口右转";
				break;
			case 206:
				result = "科目三-掉头";
				break;
			case 207:
				result = "通过公交站台";
				break;
			case 208:
				result = "科目三-路口左转";
				break;
			case 209:
				result = "科目三-会车";
				break;
			case 210:
				result = "科目三-加减档";
				break;
			case 211:
				result = "科目三-直线行驶";
				break;
			case 212:
				result = "科目三-靠边停车";
				break;
			case 213:
				result = "科目三-倒车";
				break;
			case 214:
				result = "科目三-路口直行";
				break;
			case 400:
				result = "自由驾驶-海景公路";
				break;
			case 401:
				result = "自由驾驶-乡村公路";
				break;
			case 402:
				result = "自由驾驶-山区道路";
				break;
			case 403:
				result = "自由驾驶-城市道路";
				break;
			case 404:
				result = "自由驾驶-高速公路";
				break;
			case 405:
				result = "自由驾驶-深圳实景";
				break;
			case 407:
				result = "自由驾驶-台州长潭水库";
				break;
			case 406:
				result = "自由驾驶-雪景";
				break;
			case 512:
				result = "驾驶技能-恶劣天气";
				break;
			case 511:
				result = "驾驶技能-城市红绿灯";
				break;
			case 5110:
				result = "驾驶技能-城市让超车";
				break;
			case 5111:
				result = "驾驶技能-城市跟车";
				break;
			case 510:
				result = "驾驶技能-山路跟车/超车";
				break;
			case 5100:
				result = "驾驶技能-山路会车";
				break;
			case 509:
				result = "驾驶技能-山路坡道";
				break;
			case 5090:
				result = "驾驶技能-山路弯道";
				break;
			case 507:
				result = "驾驶技能-高速公路停车";
				break;
			case 506:
				result = "驾驶技能-高速公路隧道";
				break;
			case 500:
				result = "驾驶技能-恶劣天气-雨天";
				break;
			case 501:
				result = "驾驶技能-恶劣天气-雾天";
				break;
			case 502:
				result = "驾驶技能-恶劣天气-雪天";
				break;
			case 503:
				result = "驾驶技能-恶劣天气-泥泞路";
				break;
			case 504:
				result = "";
				break;
			case 505:
				result = "-夜间驾驶";
				break;
			case 5050:
				result = "驾驶技能";
				break;
			case 601:
				result = "应急训练-出现行人";
				break;
			case 602:
				result = "应急训练-突然爆胎";
				break;
			case 603:
				result = "应急训练-酒驾(毒驾)";
				break;
			case 604:
				result = "应急训练-车身起火";
				break;
			case 605:
				result = "应急训练-方向盘失灵";
				break;
			case 606:
				result = "应急训练-坑洼路面";
				break;
			case 607:
				result = "应急训练-疲劳驾驶";
				break;
			case 608:
				result = "应急训练-前车双闪(事故)";
				break;
			case 609:
				result = "应急训练-前方落石";
				break;
			case 610:
				result = "应急训练-中途没油";
				break;
			case 611:
				result = "应急训练-刹车失灵";
				break;
			case 612:
				result = "应急训练-山体滑坡";
				break;
			case 613:
				result = "应急训练-公交盲区";
				break;
			case 614:
				result = "应急训练-非机动车抢道";
				break;
			case 615:
				result = "应急综合训练";
				break;
			}
			return result;
		}

		public IEnumerator LoadHistoryDriveItemFiles(string path)
		{
			BinaryReader binaryReader = new BinaryReader(new FileInfo(path).OpenRead());
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
				recordList.Add(recordItemData);
			}
			binaryReader.Close();
			yield return 0;
		}

		public void DeleteList(int a)
		{
			FileStream fileStream = new FileInfo(GetPath() + "historymap.map").Create();
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			recordList.RemoveAt(a);
			binaryWriter.Write(recordList.Count);
			foreach (RecordItemData record in recordList)
			{
				binaryWriter.Write(record.trainType);
				binaryWriter.Write(record.item);
				binaryWriter.Write(record.other);
				binaryWriter.Write(record.time);
				binaryWriter.Write(record.isManual);
				binaryWriter.Write(record.carType);
				binaryWriter.Write(record.pathindex);
				binaryWriter.Write(record.scenesname);
			}
			binaryWriter.Flush();
			binaryWriter.Close();
			fileStream.Close();
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
	}
}
