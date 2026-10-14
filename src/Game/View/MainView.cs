using System;
using System.Runtime.InteropServices;
using Car;
using Global;
using Scene;
using SoundAndTextShow;
using UnityEngine;

namespace View
{
	public class MainView : ViewBase
	{
		private static int SM_W = 0;

		private static int SM_H = 1;

		private int usbV;

		public static int curClickBtnIndex;

		private string[] contents = new string[1] { "下面将进行模拟夜间行驶灯光的考试，请在5秒内做出相应的灯光操作，请开启前照灯" };

		private bool isPlaySound;

		private int index;

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int GetSystemMetrics(int nIndex);

		public override void Start()
		{
			usbV = DataCenter.Instance.UsbVersion;
			if (SettingInfoCtl.Instance.SetResolution == "自适应")
			{
				Application.runInBackground = true;
				Screen.SetResolution(GetSystemMetrics(0), GetSystemMetrics(1), fullscreen: true);
			}
			else
			{
				string[] array = SettingInfoCtl.Instance.SetResolution.Split('*');
				int width = Convert.ToInt32(array[0]);
				int height = Convert.ToInt32(array[1]);
				Application.runInBackground = true;
				Screen.SetResolution(width, height, fullscreen: true);
			}
			if (DataCenter.Instance.UsbVersion != 64 && SettingInfoCtl.Instance.carType == CarType.YongShi)
			{
				SettingInfoCtl.Instance.carType = CarType.JieDaManual;
				SettingInfoCtl.Instance.isManualGear = true;
				SettingInfoCtl.Instance.SaveSetingInfos();
			}
			base.Start();
			SceneTextShow.Instance.BackToMainScene();
		}

		private void Update()
		{
			if (SignalSourceCtl.Instance.GetCircuitBoardSignalType() == 5 && SignalSourceCtl.Instance.GetReadBuffer(57) == 1)
			{
				SignalSourceCtl.Instance.SetWriteBuffer(4, 97);
			}
			if (usbV != DataCenter.Instance.UsbVersion)
			{
				if (SettingInfoCtl.Instance.SetResolution == "自适应")
				{
					Application.runInBackground = true;
					Screen.SetResolution(GetSystemMetrics(0), GetSystemMetrics(1), fullscreen: true);
				}
				else
				{
					string[] array = SettingInfoCtl.Instance.SetResolution.Split('*');
					int width = Convert.ToInt32(array[0]);
					int height = Convert.ToInt32(array[1]);
					Application.runInBackground = true;
					Screen.SetResolution(width, height, fullscreen: true);
				}
				usbV = DataCenter.Instance.UsbVersion;
			}
			if (SignalSourceCtl.Instance.connectStatus == 1 && !DataCenter.Instance.isShowKeTengBiaoDingView && (SignalSourceCtl.Instance.Pid == "4200" || SignalSourceCtl.Instance.Pid == "4100") && SettingInfoCtl.Instance.isShowBiaoDing)
			{
				DataCenter.Instance.isShowKeTengBiaoDingView = true;
				ViewJumpCtl.Instance.EnterView(ViewType.KeTengBiaoDingView, isDestroyCurView: false);
			}
			else if (SignalSourceCtl.Instance.connectStatus == 2 && !DataCenter.Instance.isShowKeTengBiaoDingView && SettingInfoCtl.Instance.isShowBiaoDing && SignalSourceCtl.Instance.Pid == "4004")
			{
				DataCenter.Instance.isShowKeTengBiaoDingView = true;
				ViewJumpCtl.Instance.EnterView(ViewType.KeTengBiaoDingView, isDestroyCurView: false);
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.TeachingVedioChooseView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.trainType = TrainType.BasicTrain;
				ViewJumpCtl.Instance.EnterView(ViewType.BasicTrainView);
			}
			else if (go == GetBtn(2))
			{
				if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa || SettingInfoCtl.Instance.carType == CarType.DaBaChe || SettingInfoCtl.Instance.carType == CarType.JieFang1121 || SettingInfoCtl.Instance.carType == CarType.TianJin || SettingInfoCtl.Instance.carType == CarType.JieFang1125 || SettingInfoCtl.Instance.carType == CarType.DFKangMingSi || SettingInfoCtl.Instance.carType == CarType.QianYinChe)
				{
					ViewJumpCtl.Instance.trainType = TrainType.CourseTwo;
					ViewJumpCtl.Instance.EnterView(ViewType.CourseTwoDFChooseView);
				}
				else
				{
					ViewJumpCtl.Instance.trainType = TrainType.CourseTwo;
					ViewJumpCtl.Instance.EnterView(ViewType.CoursetwoScenesChooseTwoView);
				}
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeScenesChooseView);
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.trainType = TrainType.DriveSkill;
				ViewJumpCtl.Instance.modelType = 2;
				ViewJumpCtl.Instance.EnterView(ViewType.DriveSkillView);
				SceneJumpCtl.Instance.CoursethreeName = CoursethreeScenesName.NanChang;
				StartCoroutine(new CourseThreeItemPositionCtl().ReadCourseThreeItemPostion(DataCenter.Instance.itemPositionList));
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.trainType = TrainType.FreeDriving;
				ViewJumpCtl.Instance.modelType = 2;
				ViewJumpCtl.Instance.EnterView(ViewType.FreeDrivingView);
			}
			else if (go == GetBtn(6))
			{
				ViewJumpCtl.Instance.trainType = TrainType.YingjiXunlian;
				ViewJumpCtl.Instance.EnterView(ViewType.YingjiXunlianView);
			}
			else if (go == GetBtn(7))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.DriveHistoryView);
			}
			else if (go == GetBtn(8))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.SignalTestView);
			}
			else if (go == GetBtn(9))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.AboutView);
			}
			else if (go == GetBtn(10))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.CarSelect);
			}
			else if (go == GetBtn(11))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.PasswordView, isDestroyCurView: false);
				ViewJumpCtl.Instance.curView.GetComponent<PasswordView>().ShowDefaultTips(active: true);
				ViewJumpCtl.Instance.curView.GetComponent<PasswordView>().EnterAction = PasswordEnter;
				ViewJumpCtl.Instance.curView.GetComponent<PasswordView>().btnClickAction = JudgePassword;
			}
			else if (go == GetBtn(12) && !ExternalInputCtl.Instance.isCircuitBoardSignal)
			{
				Application.Quit();
			}
			curClickBtnIndex = mSelectBtnIndex;
		}

		private void PasswordEnter(string value)
		{
			if (SettingInfoCtl.Instance.password == value)
			{
				ViewJumpCtl.Instance.ReturnPreView();
				ViewJumpCtl.Instance.EnterView(ViewType.SoftSettingView);
			}
			else if (SettingInfoCtl.Instance.password != value)
			{
				ViewJumpCtl.Instance.ShowMsgTips("输入的密码错误,请重新输入");
				ViewJumpCtl.Instance.curView.GetComponent<PasswordView>().ResetPasswordInput();
			}
		}

		private void JudgePassword(string value)
		{
			if (SettingInfoCtl.Instance.password == value)
			{
				ViewJumpCtl.Instance.ReturnPreView();
				ViewJumpCtl.Instance.EnterView(ViewType.SoftSettingView);
			}
		}
	}
}
