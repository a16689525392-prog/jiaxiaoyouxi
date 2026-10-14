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
	public class CourseTwoDriveGuide : MonoBehaviour
	{
		public static CourseTwoDriveGuide Instance;

		private CourseTwoSceneModel sceneModel;

		private GameObject Duidian;

		private GameObject TiShiLine;

		private GameObject Frame;

		private GameObject CarCamera;

		private GuideSignalShow guideSignalShow;

		private GameObject carFlashGroup;

		private List<GuideStepInfo> guideStepInfoList = new List<GuideStepInfo>();

		private bool isGuide;

		private List<FlashItemInfo> flashItemInfoList = new List<FlashItemInfo>();

		private List<CameraItemInfo> cameraItemInfoList = new List<CameraItemInfo>();

		private List<CameraViewItemInfo> cameraviewItemInfoList = new List<CameraViewItemInfo>();

		private float intervalTime;

		private int curGuideOperateStep;

		private int curFlashOperateStep;

		private int curCameraShowStep;

		private int curCameraViewShowStep;

		private GuideTipsViewCtl guideTipsView;

		private List<ActionInfo> curActionList = new List<ActionInfo>();

		private int guideActionIndex;

		private int curHistotyIndex;

		private bool isShowTipsView;

		private float curSteerWheelAngle;

		private List<GameObject> flashGoList = new List<GameObject>();

		private List<GameObject> noflashGoList = new List<GameObject>();

		private List<GameObject> cameraGoList = new List<GameObject>();

		private List<GameObject> cameraviewGoList = new List<GameObject>();

		private float flashTime;

		public float CurSteerWheelAngle
		{
			get
			{
				return curSteerWheelAngle;
			}
			set
			{
				curSteerWheelAngle = value;
			}
		}

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			sceneModel = (CourseTwoSceneModel)SceneModelBase.Instance;
			Duidian = GameObject.Find("DuiDian");
			TiShiLine = GameObject.Find("TiShiLine");
			Frame = GameObject.Find("Frame");
			CarCamera = GameObject.Find("CarCamera");
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetAction = (UnityAction)Delegate.Combine(baseInstance.ResetAction, new UnityAction(Reset));
		}

		private void Update()
		{
			UpdateShowGuideLine();
		}

		private IEnumerator LoadGuideFile(string path)
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
			XmlNode guideOperate = xmlDocument.SelectSingleNode("guide").SelectSingleNode("guideOperate");
			ReadGuideOperate(guideOperate);
			XmlNode flash = xmlDocument.SelectSingleNode("guide").SelectSingleNode("flash");
			ReadFlashOperate(flash);
			XmlNode camera = xmlDocument.SelectSingleNode("guide").SelectSingleNode("camera");
			ReadCameraShow(camera);
			XmlNode cameraview = xmlDocument.SelectSingleNode("guide").SelectSingleNode("cameraview");
			ReadCameraViewShow(cameraview);
		}

		public void EnterNewItem(int itemType)
		{
			if (isGuide)
			{
				Reset();
				isGuide = true;
			}
			else
			{
				Reset();
			}
			if (!isGuide)
			{
				SoundPlayCtl.Instance.PlaySound(ViewJumpCtl.Instance.GetItemTypeName(itemType) + "项目开始");
				switch (itemType)
				{
				case 34:
					StartCoroutine(LoadGuideFile("ZuoDaoCheRuKuGuide.xml"));
					StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("ZuoDaoCheRuKu.bytes"));
					break;
				case 0:
					StartCoroutine(LoadGuideFile("DaoCheRuKuGuide.xml"));
					StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("DaoCheRuKu.bytes"));
					break;
				case 1:
					StartCoroutine(LoadGuideFile("CeFangTingCheGuide.xml"));
					StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("CeFangTingChe.bytes"));
					break;
				case 2:
					StartCoroutine(LoadGuideFile("PoDaoDingDianGuide.xml"));
					StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("PoDaoDingDian.bytes"));
					break;
				case 3:
					StartCoroutine(LoadGuideFile("QuXianXingShiGuide.xml"));
					StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("QuXianXingShi.bytes"));
					break;
				case 4:
					StartCoroutine(LoadGuideFile("ZhiJiaoZhuanWanGuide.xml"));
					StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("ZhiJiaoZhuanWan.bytes"));
					break;
				}
				isGuide = true;
			}
		}

		private void ReadGuideOperate(XmlNode guideOperate)
		{
			guideStepInfoList.Clear();
			foreach (XmlElement childNode in guideOperate.ChildNodes)
			{
				GuideStepInfo guideStepInfo = new GuideStepInfo();
				foreach (XmlElement item in childNode)
				{
					switch (item.Name)
					{
					case "index":
						guideStepInfo.index = int.Parse(item.InnerText);
						break;
					case "judgeIndex":
						guideStepInfo.judgeIndex = int.Parse(item.InnerText);
						break;
					case "titleText":
						guideStepInfo.titleText = item.InnerText;
						break;
					case "action":
					{
						ActionInfo actionInfo = new ActionInfo();
						foreach (XmlElement childNode2 in item.ChildNodes)
						{
							string text = childNode2.Name;
							if (!(text == "content"))
							{
								if (text == "isAction")
								{
									actionInfo.isAction = bool.Parse(childNode2.InnerText);
								}
							}
							else
							{
								actionInfo.content = childNode2.InnerText;
							}
						}
						actionInfo.endIndex = 99999;
						if (SettingInfoCtl.Instance.isCeFangJudgeLight || ViewJumpCtl.Instance.itemType != 1)
						{
							guideStepInfo.actionList.Add(actionInfo);
						}
						else if (!SettingInfoCtl.Instance.isCeFangJudgeLight && actionInfo.content != "开启左转向灯" && actionInfo.content != "开启右转向灯" && actionInfo.content != "关闭转向灯")
						{
							guideStepInfo.actionList.Add(actionInfo);
						}
						break;
					}
					}
				}
				guideStepInfoList.Add(guideStepInfo);
			}
		}

		private void ReadFlashOperate(XmlNode flash)
		{
			foreach (XmlElement childNode in flash.ChildNodes)
			{
				FlashItemInfo flashItemInfo = new FlashItemInfo();
				foreach (XmlElement item in childNode)
				{
					switch (item.Name)
					{
					case "name":
						flashItemInfo.name = item.InnerText;
						break;
					case "index":
						flashItemInfo.index = int.Parse(item.InnerText);
						break;
					case "isFlash":
						flashItemInfo.isFlash = bool.Parse(item.InnerText);
						break;
					}
				}
				flashItemInfoList.Add(flashItemInfo);
			}
		}

		private void ReadCameraShow(XmlNode camera)
		{
			foreach (XmlElement childNode in camera.ChildNodes)
			{
				CameraItemInfo cameraItemInfo = new CameraItemInfo();
				foreach (XmlElement item in childNode)
				{
					switch (item.Name)
					{
					case "cameraName":
						cameraItemInfo.cameraName = item.InnerText;
						break;
					case "index":
						cameraItemInfo.index = int.Parse(item.InnerText);
						break;
					case "isShow":
						cameraItemInfo.isShow = bool.Parse(item.InnerText);
						break;
					}
				}
				cameraItemInfoList.Add(cameraItemInfo);
			}
		}

		private void ReadCameraViewShow(XmlNode cameraview)
		{
			foreach (XmlElement childNode in cameraview.ChildNodes)
			{
				CameraViewItemInfo cameraViewItemInfo = new CameraViewItemInfo();
				foreach (XmlElement item in childNode)
				{
					switch (item.Name)
					{
					case "cameraviewName":
						cameraViewItemInfo.cameraviewName = item.InnerText;
						break;
					case "index":
						cameraViewItemInfo.index = int.Parse(item.InnerText);
						break;
					case "isShow":
						cameraViewItemInfo.isShow = bool.Parse(item.InnerText);
						break;
					}
				}
				cameraviewItemInfoList.Add(cameraViewItemInfo);
			}
		}

		private void UpdateJudgeGuideSignalShow()
		{
			if (!CarDriveHistoryPlay.Instance.IsPlayEnd())
			{
				if (guideTipsView == null)
				{
					intervalTime += Time.deltaTime;
				}
				else
				{
					intervalTime = 0f;
					if (guideSignalShow.gameObject.activeSelf)
					{
						guideSignalShow.gameObject.SetActive(value: false);
					}
				}
				if (intervalTime > 1f && !guideSignalShow.gameObject.activeSelf)
				{
					guideSignalShow.gameObject.SetActive(value: true);
				}
			}
			else if (guideSignalShow.gameObject.activeSelf)
			{
				guideSignalShow.gameObject.SetActive(value: false);
			}
		}

		public void JudgeGuide(int index)
		{
			curHistotyIndex = index;
			if (curGuideOperateStep < guideStepInfoList.Count && CarDriveHistoryPlay.Instance.IsPlaying())
			{
				if (index >= guideStepInfoList[curGuideOperateStep].judgeIndex && guideTipsView != null)
				{
					guideTipsView.StartCurActionJudge();
					CarDriveHistoryPlay.Instance.Pause();
					curGuideOperateStep++;
					isShowTipsView = false;
				}
				else if (index >= guideStepInfoList[curGuideOperateStep].index && !isShowTipsView)
				{
					curActionList = guideStepInfoList[curGuideOperateStep].actionList;
					guideActionIndex = 0;
					if (guideTipsView != null)
					{
						guideTipsView.DestroyThis();
					}
					guideTipsView = ViewJumpCtl.Instance.CreateView(ViewType.GuideTipsView).GetComponent<GuideTipsViewCtl>();
					guideTipsView.InitGuideTipsView(guideStepInfoList[curGuideOperateStep]);
					isShowTipsView = true;
				}
			}
			if (curFlashOperateStep < flashItemInfoList.Count && index > flashItemInfoList[curFlashOperateStep].index)
			{
				ShowGuideLine(flashItemInfoList[curFlashOperateStep].name, flashItemInfoList[curFlashOperateStep].isFlash);
				curFlashOperateStep++;
			}
			if (curCameraShowStep < cameraItemInfoList.Count && index > cameraItemInfoList[curCameraShowStep].index && cameraItemInfoList[curCameraShowStep].cameraName != null)
			{
				string[] array = cameraItemInfoList[curCameraShowStep].cameraName.Split(',');
				CarCamera.transform.Find(array[0]).transform.localPosition = new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
				CarCamera.transform.Find(array[0]).localEulerAngles = new Vector3(Convert.ToSingle(array[4]), Convert.ToSingle(array[5]), Convert.ToSingle(array[6]));
				CarCamera.transform.Find(array[0]).GetComponent<Camera>().fieldOfView = Convert.ToSingle(array[7]);
				ShowCamera(array[0], cameraItemInfoList[curCameraShowStep].isShow);
				curCameraShowStep++;
			}
			if (curCameraViewShowStep < cameraviewItemInfoList.Count && index > cameraviewItemInfoList[curCameraViewShowStep].index)
			{
				ShowCameraView(cameraviewItemInfoList[curCameraViewShowStep].cameraviewName, cameraviewItemInfoList[curCameraViewShowStep].isShow);
				curCameraViewShowStep++;
			}
		}

		public void JudgeNextAction()
		{
			guideActionIndex++;
			if (guideActionIndex < curActionList.Count)
			{
				guideTipsView.StartCurActionJudge();
				return;
			}
			if (curGuideOperateStep == guideStepInfoList.Count)
			{
				SoundPlayCtl.Instance.PlaySound(ViewJumpCtl.Instance.GetItemTypeName() + "项目结束");
				curGuideOperateStep++;
			}
			CarDriveHistoryPlay.Instance.Play();
		}

		private void ShowGuideLine(string name, bool isFlash)
		{
			if (isFlash && Duidian != null)
			{
				if (Duidian.transform.Find(name) != null)
				{
					flashGoList.Add(Duidian.transform.Find(name).gameObject);
				}
				else if (TiShiLine != null && TiShiLine.transform.Find(name) != null)
				{
					noflashGoList.Add(TiShiLine.transform.Find(name).gameObject);
				}
				else if (Frame != null && Frame.transform.Find(name) != null)
				{
					flashGoList.Add(Frame.transform.Find(name).gameObject);
				}
				else
				{
					Debug.Log("未找到 " + name + " 物体");
				}
				return;
			}
			foreach (GameObject flashGo in flashGoList)
			{
				if (flashGo.name == name)
				{
					flashGoList.Remove(flashGo);
					flashGo.SetActive(value: false);
					break;
				}
			}
			foreach (GameObject noflashGo in noflashGoList)
			{
				if (noflashGo.name == name)
				{
					noflashGoList.Remove(noflashGo);
					noflashGo.SetActive(value: false);
					break;
				}
			}
		}

		private void ShowCamera(string name, bool isFlash)
		{
			if (isFlash && CarCamera != null)
			{
				if (CarCamera.transform.Find(name) != null)
				{
					cameraGoList.Add(CarCamera.transform.Find(name).gameObject);
				}
				else
				{
					Debug.Log("未找到 " + name + " 物体");
				}
				return;
			}
			CarCamera.transform.Find(name).gameObject.SetActive(value: false);
			foreach (GameObject cameraGo in cameraGoList)
			{
				if (cameraGo.name == name)
				{
					cameraGoList.Remove(cameraGo);
					cameraGo.SetActive(value: false);
					break;
				}
			}
		}

		private void ShowCameraView(string name, bool isFlash)
		{
			if (isFlash && TiShiLine != null)
			{
				if (TiShiLine.transform.Find(name) != null)
				{
					cameraviewGoList.Add(TiShiLine.transform.Find(name).gameObject);
				}
				else
				{
					Debug.Log("未找到 " + name + " 物体");
				}
				return;
			}
			foreach (GameObject cameraviewGo in cameraviewGoList)
			{
				if (cameraviewGo.name == name)
				{
					cameraviewGoList.Remove(cameraviewGo);
					cameraviewGo.SetActive(value: false);
					break;
				}
			}
		}

		public float GetSteerWheelAngle(string str)
		{
			float result = 99f;
			switch (str)
			{
			case "方向盘左转90度":
				result = -90f / CarSignalValue.SteerWheeMaxlAngle;
				break;
			case "方向盘左转半圈":
				result = -180f / CarSignalValue.SteerWheeMaxlAngle;
				break;
			case "方向盘左转一圈":
				result = -360f / CarSignalValue.SteerWheeMaxlAngle;
				break;
			case "方向盘左打满":
				result = -1f;
				break;
			case "方向盘右转90度":
				result = 90f / CarSignalValue.SteerWheeMaxlAngle;
				break;
			case "方向盘右转半圈":
				result = 180f / CarSignalValue.SteerWheeMaxlAngle;
				break;
			case "方向盘右转一圈":
				result = 360f / CarSignalValue.SteerWheeMaxlAngle;
				break;
			case "方向盘右打满":
				result = 1f;
				break;
			case "回正方向盘":
				result = 0f;
				break;
			}
			return result;
		}

		private void UpdateShowGuideLine()
		{
			flashTime += Time.deltaTime;
			if (flashTime < 0.6f)
			{
				foreach (GameObject flashGo in flashGoList)
				{
					flashGo.SetActive(value: true);
				}
			}
			else if (flashTime > 0.6f && flashTime < 1f)
			{
				foreach (GameObject flashGo2 in flashGoList)
				{
					flashGo2.SetActive(value: false);
				}
			}
			else if (flashTime >= 1f)
			{
				flashTime = 0f;
			}
			foreach (GameObject noflashGo in noflashGoList)
			{
				noflashGo.SetActive(value: true);
			}
			foreach (GameObject cameraGo in cameraGoList)
			{
				cameraGo.SetActive(value: true);
			}
			foreach (GameObject cameraviewGo in cameraviewGoList)
			{
				cameraviewGo.SetActive(value: true);
			}
		}

		public void Reset()
		{
			curGuideOperateStep = 0;
			curActionList = null;
			curFlashOperateStep = 0;
			curCameraShowStep = 0;
			curCameraViewShowStep = 0;
			if (noflashGoList.Count > 0)
			{
				foreach (GameObject noflashGo in noflashGoList)
				{
					noflashGo.SetActive(value: false);
				}
				noflashGoList.Clear();
				noflashGoList = new List<GameObject>();
			}
			if (flashGoList.Count > 0)
			{
				foreach (GameObject flashGo in flashGoList)
				{
					flashGo.SetActive(value: false);
				}
				flashGoList.Clear();
				flashGoList = new List<GameObject>();
			}
			if (cameraGoList.Count > 0)
			{
				foreach (GameObject cameraGo in cameraGoList)
				{
					cameraGo.SetActive(value: false);
				}
				cameraGoList.Clear();
				cameraGoList = new List<GameObject>();
			}
			if (cameraviewGoList.Count > 0)
			{
				foreach (GameObject cameraviewGo in cameraviewGoList)
				{
					cameraviewGo.SetActive(value: false);
				}
				cameraviewGoList.Clear();
				cameraviewGoList = new List<GameObject>();
			}
			if (guideTipsView != null && guideTipsView.gameObject != null)
			{
				guideTipsView.DestroyThis();
			}
			CarDriveHistoryPlay.Instance.Play();
			curSteerWheelAngle = 0f;
			isShowTipsView = false;
			isGuide = false;
		}

		private void OnDestroy()
		{
			if (guideTipsView != null && guideTipsView.gameObject != null)
			{
				guideTipsView.DestroyThis();
			}
			if (guideSignalShow != null)
			{
				UnityEngine.Object.Destroy(guideSignalShow.gameObject);
			}
		}
	}
}
