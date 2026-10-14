using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using View;

namespace Scene
{
	public class CourseTwoScene : TrainSceneBase
	{
		private static CourseTwoScene instance;

		public bool IsCourseTwoKaoShi;

		private int tempItemType;

		public GameObject TriggerArea;

		public int directionID;

		public int indexID;

		private Dictionary<string, string> setcameraItem = new Dictionary<string, string>();

		public CourseTwoItemCtlBase curItemCtl;

		public static CourseTwoScene Instance => instance;

		private IEnumerator LoadRuleFile(string path)
		{
			string text = ((!SettingInfoCtl.Instance.isManualGear) ? "/GuideAndRuleXml/CourseTwo/AutoGear/" : "/GuideAndRuleXml/CourseTwo/ManualGear/");
			switch (SceneJumpCtl.Instance.CoursetwoName)
			{
			case CoursetwoScenesName.YiAnDa:
				text += "YiAnDa/";
				break;
			case CoursetwoScenesName.QingHu:
				text += "QingHu/";
				break;
			case CoursetwoScenesName.WenZhou:
				text += "WenZhou/";
				break;
			case CoursetwoScenesName.HeBei:
				text += "HeBei/";
				break;
			case CoursetwoScenesName.HuNanYZ:
				text += "HuNanYZ/";
				break;
			case CoursetwoScenesName.ShiJiaZhuang:
				text += "ShiJiaZhuang/";
				break;
			case CoursetwoScenesName.FoShan:
				text += "FoShan/";
				break;
			case CoursetwoScenesName.NanFeng:
				text += "NanFeng/";
				break;
			}
			UnityWebRequest www = UnityWebRequest.Get(DataCenter.Instance.GetWWWPath(Application.streamingAssetsPath + text + path));
			yield return www.SendWebRequest();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(www.downloadHandler.text);
			XmlNode camera = xmlDocument.SelectSingleNode("rule").SelectSingleNode("camera");
			ReadCameraPosition(camera);
			XmlNode index = xmlDocument.SelectSingleNode("rule").SelectSingleNode("index");
			Readitemindex(index);
			if (path == "ZhiJiaoZhuanWanRule.xml")
			{
				XmlNode direction = xmlDocument.SelectSingleNode("rule").SelectSingleNode("direction");
				Readitemdirection(direction);
			}
			switch (path)
			{
			case "DaoCheRuKuRule.xml":
			case "ZuoDaoCheRuKuRule.xml":
				curItemCtl = base.gameObject.AddComponent<DaoCheRuKu>();
				break;
			case "ZhiJiaoZhuanWanRule.xml":
				curItemCtl = base.gameObject.AddComponent<ZhiJiaoZhuanWan>();
				break;
			case "CeFangTingCheRule.xml":
				curItemCtl = base.gameObject.AddComponent<CeFangTingChe>();
				break;
			case "PoDaoDingDianRule.xml":
				curItemCtl = base.gameObject.AddComponent<PoDaoDingDian>();
				break;
			case "QuXianXingShiRule.xml":
				curItemCtl = base.gameObject.AddComponent<QuXianXingShi>();
				break;
			}
		}

		private void Readitemdirection(XmlNode direction)
		{
			foreach (XmlElement childNode in direction.ChildNodes)
			{
				foreach (XmlElement item in childNode)
				{
					string text = item.Name;
					if (text == "directionID")
					{
						directionID = int.Parse(item.InnerText);
					}
				}
			}
		}

		private void Readitemindex(XmlNode index)
		{
			foreach (XmlElement childNode in index.ChildNodes)
			{
				foreach (XmlElement item in childNode)
				{
					string text = item.Name;
					if (text == "indexID")
					{
						indexID = int.Parse(item.InnerText);
					}
				}
			}
		}

		private void ReadCameraPosition(XmlNode camera)
		{
			setcameraItem.Clear();
			foreach (XmlElement childNode in camera.ChildNodes)
			{
				string key = "";
				string text = "";
				foreach (XmlElement item in childNode)
				{
					switch (item.Name)
					{
					case "cameraID":
						key = item.InnerText;
						break;
					case "cameraName":
						text = item.InnerText;
						break;
					case "isExecute":
						text = text + "," + item.InnerText;
						break;
					}
				}
				setcameraItem.Add(key, text);
			}
		}

		public void SetCameraPosition(string name)
		{
			string[] array = setcameraItem[name].Split(',');
			if (bool.Parse(array[8]))
			{
				GetCamera(array[0]).transform.localPosition = new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
				GetCamera(array[0]).transform.localEulerAngles = new Vector3(Convert.ToSingle(array[4]), Convert.ToSingle(array[5]), Convert.ToSingle(array[6]));
				GetCamera(array[0]).GetComponent<Camera>().fieldOfView = Convert.ToSingle(array[7]);
			}
		}

		public bool GetIsExecute(string name)
		{
			return bool.Parse(setcameraItem[name].Split(',')[8]);
		}

		public Transform GetCamera(string cname)
		{
			switch (cname)
			{
			case "leftReviewCamera":
				return CarCameraCtl.Instance.leftReviewCamera;
			case "rightReviewCamera":
				return CarCameraCtl.Instance.rightReviewCamera;
			case "driveCamera":
				return CarCameraCtl.Instance.driveCamera;
			default:
				return CarCameraCtl.Instance.leftReviewCamera;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			instance = this;
			if (!SettingInfoCtl.Instance.isManualGear)
			{
				TriggerArea.transform.Find("Trigger_PoDao1").gameObject.SetActive(value: false);
			}
		}

		protected override void Start()
		{
			base.Start();
			if (ViewJumpCtl.Instance.modelType == 1)
			{
				base.gameObject.AddComponent<CourseTwoDriveGuide>();
			}
			if (ViewJumpCtl.Instance.itemType == 1200)
			{
				ViewJumpCtl.Instance.IsKaoShi = true;
				base.gameObject.AddComponent<CourseTwoKaoShi>();
			}
			ColliderDetection colliderDetection = ColliderDetection.instance;
			colliderDetection.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Combine(colliderDetection.roadIndexChangeCallBack, new UnityAction<int, int>(JudgeTurnLight));
			ColliderDetection colliderDetection2 = ColliderDetection.instance;
			colliderDetection2.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Combine(colliderDetection2.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			tempItemType = -1;
			base.ResetAction = (UnityAction)Delegate.Combine(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
		}

		protected override void Update()
		{
			base.Update();
		}

		private void TrainItemReset()
		{
			tempItemType = -1;
			if (CourseTwoDriveGuide.Instance != null)
			{
				CourseTwoDriveGuide.Instance.Reset();
			}
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
		}

		private void ResetTimePlay()
		{
			CarState.Instance.FireOn = false;
			tempItemType = -1;
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
		}

		private void TriggerAreaEvent(Collider other)
		{
			if (ViewJumpCtl.Instance.IsKaoShi && other.name == "AddDaoKu" && tempItemType != 0)
			{
				tempItemType = 0;
				if (curItemCtl != null)
				{
					UnityEngine.Object.Destroy(curItemCtl);
				}
				StartCoroutine(LoadRuleFile("DaoCheRuKuRule.xml"));
			}
			if (ViewJumpCtl.Instance.IsKaoShi)
			{
				JudgeItemReset(other.name);
			}
			if (curItemCtl != null)
			{
				curItemCtl.TriggerEvent(other);
			}
		}

		private void JudgeItemReset(int itemType)
		{
			switch (itemType)
			{
			case 34:
				JudgeItemReset("Start_ZuoDaoKu");
				break;
			case 4:
				JudgeItemReset("Start_ZhiJiao");
				break;
			case 0:
				JudgeItemReset("Start_DaoKu");
				break;
			case 1:
				JudgeItemReset("Start_CeFang");
				break;
			case 2:
				JudgeItemReset("Start_PoDao");
				break;
			case 3:
				JudgeItemReset("Start_QuXian");
				break;
			case 5:
				JudgeItemReset("Start_DaoZhuang");
				break;
			case 6:
				JudgeItemReset("Start_ZhangAi");
				break;
			case 7:
				JudgeItemReset("Start_Qiao");
				break;
			case 8:
				JudgeItemReset("Start_QiFu");
				break;
			case 9:
				JudgeItemReset("Start_XianKuan");
				break;
			case 10:
				JudgeItemReset("Start_ZhaiLu");
				break;
			case 11:
				JudgeItemReset("Start_ShiHua");
				break;
			case 12:
				JudgeItemReset("Start_YuTian");
				break;
			case 13:
				JudgeItemReset("Start_WuTian");
				break;
			case 14:
				JudgeItemReset("Start_SuiDao");
				break;
			case 15:
				JudgeItemReset("Start_ZhuanWan");
				break;
			case 16:
				JudgeItemReset("Start_GaoSu");
				break;
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
				break;
			}
		}

		private void JudgeItemReset(string other)
		{
			switch (other)
			{
			case "Start_ZuoDaoKu":
				if (tempItemType != 34)
				{
					tempItemType = 34;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 1)
					{
						CourseTwoDriveGuide.Instance.EnterNewItem(34);
					}
					else if (ViewJumpCtl.Instance.modelType == 0)
					{
						StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("ZuoDaoCheRuKu.bytes"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2)
					{
						StartCoroutine(LoadRuleFile("ZuoDaoCheRuKuRule.xml"));
					}
					ItemStart();
				}
				break;
			case "Start_YouChuKu":
				if (tempItemType != 0)
				{
					tempItemType = 0;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 1)
					{
						CourseTwoDriveGuide.Instance.EnterNewItem(0);
					}
					else if (ViewJumpCtl.Instance.modelType == 0)
					{
						StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("DaoCheRuKu.bytes"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						StartCoroutine(LoadRuleFile("DaoCheRuKuRule.xml"));
					}
					ItemStart();
				}
				break;
			case "Start_ZhiJiao":
				if (tempItemType != 4)
				{
					tempItemType = 4;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 1)
					{
						CourseTwoDriveGuide.Instance.EnterNewItem(4);
					}
					else if (ViewJumpCtl.Instance.modelType == 0)
					{
						StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("ZhiJiaoZhuanWan.bytes"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						StartCoroutine(LoadRuleFile("ZhiJiaoZhuanWanRule.xml"));
					}
					ItemStart();
				}
				break;
			case "Start_DaoKu":
				if (tempItemType != 0)
				{
					tempItemType = 0;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 1)
					{
						CourseTwoDriveGuide.Instance.EnterNewItem(0);
					}
					else if (ViewJumpCtl.Instance.modelType == 0)
					{
						StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("DaoCheRuKu.bytes"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						StartCoroutine(LoadRuleFile("DaoCheRuKuRule.xml"));
					}
					ItemStart();
				}
				break;
			case "Start_CeFang":
				if (tempItemType != 1)
				{
					tempItemType = 1;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 1)
					{
						CourseTwoDriveGuide.Instance.EnterNewItem(1);
					}
					else if (ViewJumpCtl.Instance.modelType == 0)
					{
						StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("CeFangTingChe.bytes"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						StartCoroutine(LoadRuleFile("CeFangTingCheRule.xml"));
					}
					ItemStart();
				}
				break;
			case "Start_PoDao":
				if (tempItemType != 2)
				{
					tempItemType = 2;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 1)
					{
						CourseTwoDriveGuide.Instance.EnterNewItem(2);
					}
					else if (ViewJumpCtl.Instance.modelType == 0)
					{
						StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("PoDaoDingDian.bytes"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						StartCoroutine(LoadRuleFile("PoDaoDingDianRule.xml"));
					}
					ItemStart();
				}
				break;
			case "Start_QuXian":
				if (tempItemType != 3)
				{
					tempItemType = 3;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 1)
					{
						CourseTwoDriveGuide.Instance.EnterNewItem(3);
					}
					else if (ViewJumpCtl.Instance.modelType == 0)
					{
						StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("QuXianXingShi.bytes"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						StartCoroutine(LoadRuleFile("QuXianXingShiRule.xml"));
					}
					ItemStart();
				}
				break;
			}
		}

		private void ItemStart()
		{
		}

		private void JudgeTurnLight(int preIndex, int curIndex)
		{
			if (ViewJumpCtl.Instance.itemType == 16)
			{
				if (curIndex > preIndex)
				{
					CarActionJudge.Instance.JudgeTurnLight3s(109);
				}
				else
				{
					CarActionJudge.Instance.JudgeTurnLight3s(110);
				}
			}
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			SoundPlayCtl.Instance.ClearClip();
			ColliderDetection colliderDetection = ColliderDetection.instance;
			colliderDetection.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Remove(colliderDetection.roadIndexChangeCallBack, new UnityAction<int, int>(JudgeTurnLight));
			ColliderDetection colliderDetection2 = ColliderDetection.instance;
			colliderDetection2.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Remove(colliderDetection2.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainItemReset));
		}
	}
}
