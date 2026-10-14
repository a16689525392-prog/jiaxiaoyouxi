using System;
using System.Collections.Generic;
using Car;
using Global;
using Scene;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;

namespace View
{
	[Serializable]
	public class ViewJumpCtl : MonoBehaviour
	{
		private static ViewJumpCtl instance;

		public bool IsKaoShi;

		public bool Enter;

		protected Stack<int> selectBtnIndexStack = new Stack<int>();

		public TrainType trainType;

		public int itemType;

		public int otherType = 504;

		public int modelType = 2;

		public Stack<ViewType> viewStack = new Stack<ViewType>();

		[HideInInspector]
		public GameObject curView;

		private Transform mainViewParent;

		private Transform UiCanvas;

		public Transform background;

		private Dictionary<ViewType, GameObject> showViewDirectory = new Dictionary<ViewType, GameObject>();

		public static ViewType curViewType;

		public int curViewSelectIndex;

		public static ViewJumpCtl Instance => instance;

		private void Awake()
		{
			instance = this;
			UiCanvas = base.transform.Find("Canvas");
			mainViewParent = base.transform.Find("Canvas").Find("ViewParent");
			background = base.transform.Find("Canvas").Find("Background");
		}

		private void Start()
		{
			AcceptInputSignal();
		}

		private void Update()
		{
		}

		public void EnterView(ViewType mViewType, bool isDestroyCurView = true, bool isSave = true)
		{
			if (!MyDiagnosis.Instance.JudgeSoftCorrect() && mViewType != ViewType.TiShiView && mViewType != ViewType.AboutView && mViewType != ViewType.LoadingView && mViewType != ViewType.MainView && mViewType != ViewType.MsgView)
			{
				EnterView(ViewType.TiShiView);
				return;
			}
			if (curView != null)
			{
				if (isDestroyCurView)
				{
					UnityEngine.Object.Destroy(curView);
				}
				else
				{
					showViewDirectory.Add(curViewType, curView);
				}
				if (isSave && JudgeViewSave(curViewType))
				{
					viewStack.Push(curViewType);
				}
			}
			if (showViewDirectory.ContainsKey(mViewType))
			{
				curView = showViewDirectory[mViewType];
				showViewDirectory.Remove(mViewType);
			}
			else
			{
				curView = CreateView(mViewType);
			}
			curViewType = mViewType;
		}

		private bool JudgeViewSave(ViewType mViewType)
		{
			switch (mViewType)
			{
			case ViewType.LoadingView:
			case ViewType.BuyChooseView:
			case ViewType.QrCodeView:
			case ViewType.MsgView:
			case ViewType.WayChooseView:
				return false;
			default:
				return true;
			}
		}

		public void ReturnPreView()
		{
			if (viewStack.Count > 0)
			{
				ViewType mViewType = viewStack.Pop();
				EnterView(mViewType, isDestroyCurView: true, isSave: false);
			}
			else
			{
				EnterView(ViewType.MainView);
			}
		}

		public void EnterScene()
		{
			if (!MyDiagnosis.Instance.JudgeSoftCorrect())
			{
				EnterView(ViewType.TiShiView);
				return;
			}
			switch (trainType)
			{
			case TrainType.CourseThree:
			case TrainType.NightLight:
				if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.CourseThree);
				}
				else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.HuNanYZCourseThree);
				}
				else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.RenCaiCourseThree);
				}
				else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ChengDongCourseThree);
				}
				else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.BaiYunCourseThree);
				}
				break;
			case TrainType.CourseTwo:
				if (SettingInfoCtl.Instance.carType != CarType.DongFengDuoLiKa && SettingInfoCtl.Instance.carType != CarType.DaBaChe && SettingInfoCtl.Instance.carType != CarType.JieFang1121 && SettingInfoCtl.Instance.carType != CarType.TianJin && SettingInfoCtl.Instance.carType != CarType.JieFang1125 && SettingInfoCtl.Instance.carType != CarType.DFKangMingSi && SettingInfoCtl.Instance.carType != CarType.QianYinChe)
				{
					switch (SceneJumpCtl.Instance.CoursetwoName)
					{
					case CoursetwoScenesName.YiAnDa:
						SceneJumpCtl.Instance.EnterScene(SceneType.CourseTwo);
						break;
					case CoursetwoScenesName.QingHu:
						SceneJumpCtl.Instance.EnterScene(SceneType.QingHuCourseTwo);
						break;
					case CoursetwoScenesName.WenZhou:
						SceneJumpCtl.Instance.EnterScene(SceneType.WenZhouCourseTwo);
						break;
					case CoursetwoScenesName.HeBei:
						SceneJumpCtl.Instance.EnterScene(SceneType.HeBeiCourseTwo);
						break;
					case CoursetwoScenesName.HuNanYZ:
						SceneJumpCtl.Instance.EnterScene(SceneType.HuNanYZCourseTwo);
						break;
					case CoursetwoScenesName.ShiJiaZhuang:
						SceneJumpCtl.Instance.EnterScene(SceneType.ShiJiaZhuangCourseTwo);
						break;
					case CoursetwoScenesName.FoShan:
						SceneJumpCtl.Instance.EnterScene(SceneType.FoShanCourseTwo);
						break;
					case CoursetwoScenesName.NanFeng:
						SceneJumpCtl.Instance.EnterScene(SceneType.NanFengCourseTwo);
						break;
					}
				}
				if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa || SettingInfoCtl.Instance.carType == CarType.DaBaChe || SettingInfoCtl.Instance.carType == CarType.JieFang1121 || SettingInfoCtl.Instance.carType == CarType.TianJin || SettingInfoCtl.Instance.carType == CarType.JieFang1125 || SettingInfoCtl.Instance.carType == CarType.DFKangMingSi || SettingInfoCtl.Instance.carType == CarType.QianYinChe)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.DFCourseTwo);
				}
				break;
			case TrainType.Test:
				SceneJumpCtl.Instance.EnterScene(SceneType.Test);
				break;
			case TrainType.DrivingAbilityTest:
				if (otherType == 505 && itemType == 701)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.NightTest);
				}
				else if (itemType == 702)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.BadWeatherTest);
				}
				else if (itemType == 704)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.HighRoad);
				}
				else if (itemType == 705)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.HighRoad);
				}
				else if (itemType == 703)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ShanQu);
				}
				else if (itemType == 706)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ShenZhen);
				}
				break;
			case TrainType.DriveSkill:
				if ((otherType == 502 && itemType == 51202) || itemType == 503)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.XueJing);
				}
				else if (otherType == 505 && itemType == 5050)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.HaiJing);
				}
				else if (otherType == 501 && itemType == 51201)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ShanQu);
				}
				else if (otherType == 500 && itemType == 51200)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.XiangCun);
				}
				else if (itemType == 511)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.City);
				}
				else if (itemType == 5111 || itemType == 5110)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.CourseThree);
				}
				else if (itemType == 510 || itemType == 509 || itemType == 5100 || itemType == 5090)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ShanQu);
				}
				else if (itemType == 506 || itemType == 507)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.HighRoad);
				}
				break;
			case TrainType.FreeDriving:
				if (itemType == 400)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.HaiJing);
				}
				else if (itemType == 402)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ShanQu);
				}
				else if (itemType == 401)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.XiangCun);
				}
				else if (itemType == 403)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.City);
				}
				else if (itemType == 404)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.HighRoad);
				}
				else if (itemType == 405)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ShenZhen);
				}
				else if (itemType == 407)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.TaiZhouChangTan);
				}
				break;
			case TrainType.YingjiXunlian:
				if (itemType == 601 || itemType == 602 || itemType == 603 || itemType == 611)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.YingjiXunlian);
				}
				else if (itemType == 604 || itemType == 605 || itemType == 606 || itemType == 607 || itemType == 609 || itemType == 612)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ShanQu);
				}
				else if (itemType == 608)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.HighRoad);
				}
				else if (itemType == 610 || itemType == 613 || itemType == 614 || itemType == 615)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ChengDong);
				}
				break;
			case TrainType.BasicTrain:
				if (instance.itemType == 304 || instance.itemType == 306 || instance.itemType == 305)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.TurnDrive);
				}
				else if (instance.itemType == 308)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.CourseThree);
				}
				else
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.XunLianScene);
				}
				break;
			case TrainType.NanChangKaoChang:
				SceneJumpCtl.Instance.EnterScene(SceneType.YiandaCourseTwo);
				break;
			case TrainType.CourseOne:
			case TrainType.CourseFour:
			case TrainType.DriveHistory:
				break;
			}
		}

		public GameObject CreateView(ViewType mViewType)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(GetViewName(mViewType)));
			if (mViewType == ViewType.AdjustMirrorView)
			{
				gameObject.transform.SetParent(UiCanvas);
				gameObject.transform.SetAsLastSibling();
			}
			else
			{
				gameObject.transform.SetParent(mainViewParent);
			}
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			return gameObject;
		}

		public string GetViewName(ViewType mViewType)
		{
			string text = "";
			switch (mViewType)
			{
			case ViewType.WayChooseView:
				return "WayChooseView";
			case ViewType.DaoKuChooseView:
				return "DaoKuChooseView";
			case ViewType.BindingView:
				return "BindingView";
			case ViewType.RechargeView:
				return "RechargeView";
			case ViewType.LoginView:
				return "LoginView";
			case ViewType.TeachingVedioChooseView:
				return "TeachingVedioChooseView";
			case ViewType.DrivingAbilityTestView:
				return "DrivingAbilityTestView";
			case ViewType.AutoGearSettingView:
				return "AutoGearSettingView";
			case ViewType.BiaoDingDataView:
				return "BiaoDingDataView";
			case ViewType.SetResolutionView:
				return "SetResolutionView";
			case ViewType.SecurityCheckView:
				return "SecurityCheckView";
			case ViewType.CoursetwoScenesChooseOneView:
				return "CoursetwoScenesChooseOneView";
			case ViewType.CoursetwoScenesChooseTwoView:
				return "CoursetwoScenesChooseTwoView";
			case ViewType.CourseThreeScenesChooseView:
				return "CourseThreeScenesChooseView";
			case ViewType.CourseTwoKaoShiView:
				return "CourseTwoKaoShiView";
			case ViewType.TiMuShowView:
				return "TiMuShowView";
			case ViewType.LiLunModelChooseView:
				return "LiLunModelChooseView";
			case ViewType.CourseOneAndFourChooseView:
				return "CourseOneAndFourChooseView";
			case ViewType.CitySafeDriveView:
				return "CitySafeDriveView";
			case ViewType.HighRoadSafeDriveView:
				return "HighRoadSafeDriveView";
			case ViewType.CourseTwoDFChooseView:
				return "CourseTwoDFChooseView";
			case ViewType.BanZiTiShiView:
				return "BanZiTiShiView";
			case ViewType.LoadingView:
				return "LoadingView";
			case ViewType.MainView:
				return "MainView";
			case ViewType.BasicTrainView:
				return "BasicTrainView";
			case ViewType.CourseTwoView:
				return "CourseTwoChooseView";
			case ViewType.DaCheCourseTwoView:
				return "DaCheCourseTwoChooseView";
			case ViewType.CourseThreeView:
				return "KeMuSanView";
			case ViewType.SignalTestView:
				return "SignalTestView";
			case ViewType.SoftSettingView:
				return "SettingView";
			case ViewType.AboutView:
				return "AboutView";
			case ViewType.ModelChooseView:
				return "ModelChooseView";
			case ViewType.DaCheModelChooseView:
				return "ModelChooseView_DaChe";
			case ViewType.YingJiModelChooseView:
				return "YingJiModelChooseView";
			case ViewType.SceneMenuView:
				return "SceneMenuView";
			case ViewType.TiShiView:
				return "TiShiView";
			case ViewType.BuyChooseView:
				return "BuyChooseView";
			case ViewType.QrCodeView:
				return "QrCodeView";
			case ViewType.BiaoDingChooseView:
				return "BiaoDingChooseView";
			case ViewType.BiaoDingView:
				return "BiaoDingView";
			case ViewType.MsgView:
				return "MsgView";
			case ViewType.LightResultView:
				return "LightResultView";
			case ViewType.LightExamChooseView:
				return "LightExamChooseView";
			case ViewType.TopView:
				return "TopView";
			case ViewType.GuideTipsView:
				return "GuideTipsView";
			case ViewType.CanShuSettingView:
				return "CanShuSettingView";
			case ViewType.PasswordView:
				return "PasswordView";
			case ViewType.ChangePassword:
				return "ChangePasswordView";
			case ViewType.BindQrView:
				return "DeviceBindQrView";
			case ViewType.DeviceBindView:
				return "BindSettingView";
			case ViewType.KeTengBiaoDingView:
				return "KeTengBiaoDingView";
			case ViewType.CourseThreeModelChooseView:
				return "CourseThreeModelChooseView";
			case ViewType.CourseThreeChooseView:
				return "CourseThreeChooseView";
			case ViewType.HuNanYZCourseThreeChooseView:
				return "HuNanYZCourseThreeChooseView";
			case ViewType.CourseThreeItemChooseView:
				return "CourseThreeItemChooseView";
			case ViewType.DriveSkillView:
				return "DriveSkillChooseView";
			case ViewType.LockBiaoDingView:
				return "LockBiaoDingView";
			case ViewType.CarSelect:
				return "CarSelectView";
			case ViewType.FreeDrivingView:
				return "FreeDrivingChooseView";
			case ViewType.YingjiXunlianView:
				return "YingjiXunlianChooseView";
			case ViewType.DriveHistoryView:
				return "DriveHistoryPlayView";
			case ViewType.LightGuideView:
				return "LightGuideView";
			case ViewType.BadWeatherView:
				return "BadWeatherView";
			case ViewType.ShanQuSafeDriveView:
				return "ShanQuSafeDrive";
			case ViewType.TianQiQiangDuView:
				return "TianQiQiangDu";
			case ViewType.NanChangKaoChangView:
				return "NanChangKaoChangView";
			case ViewType.YADCourseTwoChooseView:
				return "YADCourseTwoChooseView";
			case ViewType.AdjustMirrorView:
				return "AdjustMirrorView";
			default:
				Debug.Log(string.Concat("没有", mViewType, "的界面"));
				return "MainView";
			}
		}

		public string GetItemTypeName(int itemType = -1)
		{
			if (itemType == -1)
			{
				itemType = instance.itemType;
			}
			string result = "";
			switch (itemType)
			{
			case 1300:
				result = "科二全程训练";
				break;
			case 1200:
				result = "科二考试";
				break;
			case 0:
				result = "倒车入库";
				break;
			case 1:
				result = "侧方停车";
				break;
			case 2:
				result = "坡道定点";
				break;
			case 3:
				result = "曲线行驶";
				break;
			case 4:
				result = "直角转弯";
				break;
			case 5:
				result = "桩考";
				break;
			case 6:
				result = "障碍";
				break;
			case 7:
				result = "单边桥";
				break;
			case 8:
				result = "起伏路";
				break;
			case 9:
				result = "限宽门";
				break;
			case 10:
				result = "窄路掉头";
				break;
			case 11:
				result = "湿滑路面";
				break;
			case 12:
				result = "模拟雨天";
				break;
			case 13:
				result = "模拟雾天";
				break;
			case 14:
				result = "模拟隧道";
				break;
			case 15:
				result = "模拟转弯";
				break;
			case 16:
				result = "模拟高速";
				break;
			case 100:
				result = "蓝天";
				break;
			case 101:
				result = "白云";
				break;
			case 200:
				result = "起步";
				break;
			case 201:
				result = "超车";
				break;
			case 202:
				result = "通过学校区域";
				break;
			case 203:
				result = "变更车道";
				break;
			case 204:
				result = "通过人形横道";
				break;
			case 205:
				result = "路口右转";
				break;
			case 206:
				result = "掉头";
				break;
			case 207:
				result = "通过公交站台";
				break;
			case 208:
				result = "路口左转";
				break;
			case 209:
				result = "会车";
				break;
			case 210:
				result = "加减挡";
				break;
			case 211:
				result = "直线行驶";
				break;
			case 212:
				result = "靠边停车";
				break;
			case 213:
				result = "倒车";
				break;
			case 300:
				result = "方向盘";
				break;
			case 301:
				result = "离合器";
				break;
			case 302:
				result = "换挡";
				break;
			case 303:
				result = "起步";
				break;
			case 307:
				result = "雨刮器";
				break;
			case 308:
				result = "加速操作";
				break;
			case 601:
				result = "出现行人";
				break;
			case 602:
				result = "突然爆胎";
				break;
			case 603:
				result = "酒驾(毒驾)";
				break;
			case 604:
				result = "车身起火";
				break;
			case 605:
				result = "方向盘失灵";
				break;
			case 606:
				result = "坑洼路面";
				break;
			case 608:
				result = "前车双闪(事故)";
				break;
			case 607:
				result = "疲劳驾驶";
				break;
			case 609:
				result = "前方落石";
				break;
			case 611:
				result = "刹车失灵";
				break;
			case 612:
				result = "山体滑坡";
				break;
			case 610:
				result = "中途没油";
				break;
			case 613:
				result = "公交盲区";
				break;
			case 614:
				result = "非机动车抢道";
				break;
			case 615:
				result = "应急综合训练";
				break;
			case 505:
				result = "灯光考试";
				break;
			}
			return result;
		}

		private void AcceptInputSignal()
		{
			ExternalInputCtl externalInputCtl = ExternalInputCtl.Instance;
			externalInputCtl.rightKeyDown = (UnityAction)Delegate.Combine(externalInputCtl.rightKeyDown, (UnityAction)delegate
			{
				curView.GetComponent<ViewBase>().SelectNextBtn();
			});
			ExternalInputCtl externalInputCtl2 = ExternalInputCtl.Instance;
			externalInputCtl2.leftKeyDown = (UnityAction)Delegate.Combine(externalInputCtl2.leftKeyDown, (UnityAction)delegate
			{
				curView.GetComponent<ViewBase>().SelectPreBtn();
			});
			ExternalInputCtl externalInputCtl3 = ExternalInputCtl.Instance;
			externalInputCtl3.enterKeyDown = (UnityAction)Delegate.Combine(externalInputCtl3.enterKeyDown, (UnityAction)delegate
			{
				curView.GetComponent<ViewBase>().CurBtnClick();
			});
			ExternalInputCtl externalInputCtl4 = ExternalInputCtl.Instance;
			externalInputCtl4.backKeyDown = (UnityAction)Delegate.Combine(externalInputCtl4.backKeyDown, (UnityAction)delegate
			{
				curView.GetComponent<ViewBase>().BackBtnClick();
			});
		}

		public void SetBackgroundActive(bool active)
		{
			background.gameObject.SetActive(active);
		}

		public void ShowYanShiTips(string content)
		{
			if (CourseThreeYanShiGuide.Instance != null)
			{
				if (CourseThreeYanShiGuide.Instance.YanShiTipsImage != null)
				{
					UnityEngine.Object.Destroy(CourseThreeYanShiGuide.Instance.YanShiTipsImage);
				}
				CourseThreeYanShiGuide.Instance.YanShiTipsImage = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("BtnAndTips/YanShiTips"));
				CourseThreeYanShiGuide.Instance.YanShiTipsImage.transform.SetParent(mainViewParent);
				CourseThreeYanShiGuide.Instance.YanShiTipsImage.transform.localScale = Vector3.one;
				CourseThreeYanShiGuide.Instance.YanShiTipsImage.transform.localPosition = new Vector3(0f, 150f, 0f);
				CourseThreeYanShiGuide.Instance.YanShiTipsImage.GetComponent<YanShiTips>().SoundTips.text = content;
				SoundPlayCtl.Instance.PlaySound(content, 0f, 4);
				CourseThreeYanShiGuide.Instance.YanShiTipsImage.GetComponent<YanShiTips>().IsSoundStart = true;
			}
		}

		public void ShowMsgTips(string content)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("BtnAndTips/Tips"));
			gameObject.transform.SetParent(mainViewParent);
			gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			gameObject.GetComponent<PayTips>().SetTipsContent(content);
			gameObject.transform.localPosition = new Vector3(0f, 182f, 0f);
			PayTips.payTipsList.Add(gameObject);
			if (PayTips.payTipsList.Count == 2)
			{
				PayTips.payTipsList[0].GetComponent<PayTips>().UpMove();
			}
			else if (PayTips.payTipsList.Count > 2)
			{
				UnityEngine.Object.Destroy(PayTips.payTipsList[0]);
				PayTips.payTipsList.RemoveAt(0);
				PayTips.payTipsList[0].GetComponent<PayTips>().UpMove();
			}
		}

		public GameObject CreateViewPrefab(string name, Vector3 mVector3)
		{
			GameObject obj = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(name));
			obj.transform.SetParent(mainViewParent);
			obj.transform.localScale = Vector3.one;
			obj.transform.localPosition = mVector3;
			return obj;
		}
	}
}
