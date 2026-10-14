using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Global;
using Scene;
using UnityEngine;
using UnityEngine.Networking;
using View;

namespace Car
{
	public class CarCameraCtl : MonoBehaviour
	{
		public static CarCameraCtl Instance;

		public RenderTexture leftCameraRenderTexture;

		public RenderTexture rightCameraRenderTexture;

		public RenderTexture topCameraRenderTexture;

		public RenderTexture wheelCameraRenderTexture;

		private GameObject topView;

		public bool LeftViewOpen;

		public bool RightViewOpen;

		private Dictionary<string, string> setcameraItem = new Dictionary<string, string>();

		public Transform leftReviewCamera;

		public Transform rightReviewCamera;

		public Transform driveCamera;

		private Transform topCamera;

		private Transform wheelCamera;

		private Transform followCamera;

		private Transform fourViewPoint;

		private Transform leftViewPointCamera;

		private Transform rightViewPointCamera;

		private void Awake()
		{
			LeftViewOpen = false;
			RightViewOpen = false;
			Init();
			Instance = this;
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
			{
				StartCoroutine(LoadRuleFile("SetDriveCamera.xml"));
			}
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
			{
				leftReviewCamera.transform.localPosition = new Vector3(-1.2f, 1.15f, 0.616f);
				leftReviewCamera.transform.localEulerAngles = new Vector3(0f, 176.6f, 0f);
				rightReviewCamera.transform.localPosition = new Vector3(0.92f, 1.1f, 0.628f);
				rightReviewCamera.transform.localEulerAngles = new Vector3(0f, 170f, 0f);
			}
		}

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
			if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
			{
				switch (ViewJumpCtl.Instance.itemType)
				{
				case 0:
				case 34:
					SetCameraPosition("DaoCheRuKu");
					break;
				case 1:
					SetCameraPosition("CeFangTingChe");
					break;
				case 3:
					SetCameraPosition("QuXianXingShi");
					break;
				case 4:
					SetCameraPosition("ZhiJiaoZhuanWan");
					break;
				case 2:
					SetCameraPosition("PoDaoDingDian");
					break;
				}
			}
		}

		private void ReadCameraPosition(XmlNode camera)
		{
			setcameraItem.Clear();
			foreach (XmlElement childNode in camera.ChildNodes)
			{
				string key = "";
				string value = "";
				foreach (XmlElement item in childNode)
				{
					string text = item.Name;
					if (!(text == "ItemID"))
					{
						if (text == "cameraName")
						{
							value = item.InnerText;
						}
					}
					else
					{
						key = item.InnerText;
					}
				}
				setcameraItem.Add(key, value);
			}
		}

		public void SetCameraPosition(string name)
		{
			string[] array = setcameraItem[name].Split(',');
			GetCamera(array[0]).transform.localPosition = new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
			GetCamera(array[0]).transform.localEulerAngles = new Vector3(Convert.ToSingle(array[4]), Convert.ToSingle(array[5]), Convert.ToSingle(array[6]));
			GetCamera(array[0]).GetComponent<Camera>().fieldOfView = Convert.ToSingle(array[7]);
		}

		public Transform GetCamera(string cname)
		{
			switch (cname)
			{
			case "leftReviewCamera":
				return Instance.leftReviewCamera;
			case "rightReviewCamera":
				return Instance.rightReviewCamera;
			case "driveCamera":
				return Instance.driveCamera;
			default:
				return Instance.leftReviewCamera;
			}
		}

		private void Start()
		{
			if (ViewJumpCtl.Instance.trainType == TrainType.BasicTrain && ViewJumpCtl.Instance.itemType == 300)
			{
				topCamera.gameObject.SetActive(value: false);
				topCamera.GetComponent<Camera>().targetTexture = null;
			}
			else if ((ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.BasicTrain) && !JudgeTurnDrive())
			{
				topCamera.gameObject.SetActive(value: true);
				wheelCamera.gameObject.SetActive(value: false);
				if (SettingInfoCtl.Instance.curViewPoint == 5)
				{
					SettingInfoCtl.Instance.curViewPoint = 0;
				}
			}
			else
			{
				topCamera.gameObject.SetActive(value: false);
				wheelCamera.gameObject.SetActive(value: false);
				topCamera.GetComponent<Camera>().targetTexture = null;
			}
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
			{
				topView = ViewJumpCtl.Instance.CreateViewPrefab("TopView", Vector3.zero);
				if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
				{
					topView.transform.localPosition = new Vector3(100f, 0f, 0f);
				}
			}
			else if (ViewJumpCtl.Instance.trainType == TrainType.BasicTrain && !JudgeTurnDrive())
			{
				topView = ViewJumpCtl.Instance.CreateViewPrefab("TopView", Vector3.zero);
				if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
				{
					topView.transform.localPosition = new Vector3(100f, 0f, 0f);
				}
			}
			ShowViewPoint(SettingInfoCtl.Instance.curViewPoint);
		}

		public void ShowNextViewPoint()
		{
			SettingInfoCtl.Instance.curViewPoint = GetNextViewPoint(SettingInfoCtl.Instance.curViewPoint);
			ShowViewPoint(SettingInfoCtl.Instance.curViewPoint);
		}

		private bool JudgeTurnDrive()
		{
			if (ViewJumpCtl.Instance.trainType == TrainType.BasicTrain)
			{
				return true;
			}
			return false;
		}

		public void ShowViewPoint(int viewPoint)
		{
			switch (viewPoint)
			{
			case 0:
				LeftViewOpen = false;
				RightViewOpen = false;
				driveCamera.gameObject.SetActive(value: true);
				fourViewPoint.gameObject.SetActive(value: false);
				followCamera.gameObject.SetActive(value: false);
				leftViewPointCamera.gameObject.SetActive(value: false);
				rightViewPointCamera.gameObject.SetActive(value: false);
				leftReviewCamera.gameObject.SetActive(value: true);
				rightReviewCamera.gameObject.SetActive(value: true);
				SetTopViewActvie(topView != null);
				SetMainCameraViewActive(active: true);
				break;
			case 1:
				LeftViewOpen = false;
				RightViewOpen = false;
				driveCamera.gameObject.SetActive(value: false);
				fourViewPoint.gameObject.SetActive(value: false);
				followCamera.gameObject.SetActive(value: false);
				leftViewPointCamera.gameObject.SetActive(value: true);
				rightViewPointCamera.gameObject.SetActive(value: false);
				leftReviewCamera.gameObject.SetActive(value: false);
				rightReviewCamera.gameObject.SetActive(value: false);
				SetMainCameraViewActive(active: false);
				SetTopViewActvie(active: false);
				break;
			case 2:
				LeftViewOpen = false;
				RightViewOpen = false;
				driveCamera.gameObject.SetActive(value: false);
				fourViewPoint.gameObject.SetActive(value: false);
				followCamera.gameObject.SetActive(value: false);
				leftViewPointCamera.gameObject.SetActive(value: false);
				rightViewPointCamera.gameObject.SetActive(value: true);
				leftReviewCamera.gameObject.SetActive(value: false);
				rightReviewCamera.gameObject.SetActive(value: false);
				SetTopViewActvie(active: false);
				SetMainCameraViewActive(active: false);
				break;
			case 3:
				LeftViewOpen = false;
				RightViewOpen = false;
				driveCamera.gameObject.SetActive(value: false);
				fourViewPoint.gameObject.SetActive(value: false);
				followCamera.gameObject.SetActive(value: true);
				leftViewPointCamera.gameObject.SetActive(value: false);
				rightViewPointCamera.gameObject.SetActive(value: false);
				leftReviewCamera.gameObject.SetActive(value: false);
				rightReviewCamera.gameObject.SetActive(value: false);
				SetTopViewActvie(active: false);
				SetMainCameraViewActive(active: false);
				break;
			case 4:
				LeftViewOpen = false;
				RightViewOpen = false;
				topCamera.gameObject.SetActive(value: true);
				driveCamera.gameObject.SetActive(value: false);
				fourViewPoint.gameObject.SetActive(value: true);
				followCamera.gameObject.SetActive(value: false);
				leftViewPointCamera.gameObject.SetActive(value: false);
				rightViewPointCamera.gameObject.SetActive(value: false);
				leftReviewCamera.gameObject.SetActive(value: false);
				rightReviewCamera.gameObject.SetActive(value: false);
				SetTopViewActvie(active: false);
				SetMainCameraViewActive(active: false);
				break;
			case 5:
				LeftViewOpen = false;
				RightViewOpen = false;
				SetTopViewActvie(active: true);
				driveCamera.gameObject.SetActive(value: false);
				fourViewPoint.gameObject.SetActive(value: false);
				followCamera.gameObject.SetActive(value: false);
				leftViewPointCamera.gameObject.SetActive(value: false);
				rightViewPointCamera.gameObject.SetActive(value: false);
				leftReviewCamera.gameObject.SetActive(value: false);
				rightReviewCamera.gameObject.SetActive(value: false);
				SetMainCameraViewActive(active: false);
				break;
			}
		}

		private void SetMainCameraViewActive(bool active)
		{
			if (TrainSceneBase.BaseInstance != null && TrainSceneBase.BaseInstance.mainCamView != null)
			{
				TrainSceneBase.BaseInstance.mainCamView.SetActive(active);
			}
		}

		private int GetNextViewPoint(int curViewPoint)
		{
			int result = 0;
			switch (curViewPoint)
			{
			case 0:
				result = 1;
				break;
			case 1:
				result = 2;
				break;
			case 2:
				result = 3;
				break;
			case 3:
				result = ((!(topView != null)) ? 5 : 0);
				break;
			case 5:
				result = 0;
				break;
			}
			return result;
		}

		public void SetTopViewActvie(bool active)
		{
			if (ViewJumpCtl.Instance.trainType == TrainType.BasicTrain && ViewJumpCtl.Instance.itemType == 300)
			{
				topCamera.gameObject.SetActive(value: false);
			}
			else
			{
				topCamera.gameObject.SetActive(active);
			}
			if (topView != null)
			{
				topView.SetActive(active);
			}
		}

		private void Init()
		{
			leftReviewCamera = base.transform.Find("LeftReviewCamera");
			rightReviewCamera = base.transform.Find("RightReviewCamera");
			driveCamera = base.transform.Find("DriveCamera");
			topCamera = base.transform.Find("TopCamera");
			wheelCamera = base.transform.Find("WheelCamera");
			followCamera = base.transform.Find("FollowCamera");
			fourViewPoint = base.transform.Find("FourViewPoint");
			leftViewPointCamera = base.transform.Find("LeftViewPointCamera");
			rightViewPointCamera = base.transform.Find("RightViewPointCamera");
		}

		private void OnDestroy()
		{
			if (topView != null)
			{
				UnityEngine.Object.Destroy(topView);
			}
			SettingInfoCtl.Instance.SaveSetingInfos();
		}
	}
}
