using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class TiredDrive : YingJiXunLianCtlBase
	{
		private static TiredDrive instance;

		public bool JudgePlaySound;

		public bool notStopCar;

		public bool startMohu;

		public float stopTime;

		public static TiredDrive Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (!JudgePlaySound && itemStep == 2)
			{
				stopTime += Time.deltaTime;
				if (stopTime > 5f && ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed > 10f && CarState.Instance.CarSignal.footBrake < 0.1f)
				{
					SoundPlayCtl.Instance.PlaySound("疲劳驾驶时,未减速行驶", 100f);
					JudgePlaySound = true;
				}
				else if (stopTime > 5f && (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH > 10f && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake < 0.1f)
				{
					SoundPlayCtl.Instance.PlaySound("疲劳驾驶时,未减速行驶", 100f);
					JudgePlaySound = true;
				}
			}
			if (!notStopCar && itemStep == 2)
			{
				stopTime += Time.deltaTime;
				if (stopTime > 20f && ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed > 0.5f)
				{
					SoundPlayCtl.Instance.PlaySound("疲劳驾驶时,未将车辆停靠在最近可以停车处", 100f);
					notStopCar = true;
				}
				else if (stopTime > 20f && (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH > 0.5f)
				{
					SoundPlayCtl.Instance.PlaySound("疲劳驾驶时,未将车辆停靠在最近可以停车处", 100f);
					notStopCar = true;
				}
			}
			if (notStopCar)
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("疲劳驾驶项目结束");
				itemStep = 99;
				notStopCar = false;
			}
			if (itemStep == 2 && ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed < 0.1f)
			{
				stopTime += Time.deltaTime;
				if (stopTime > 20f)
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("疲劳驾驶项目结束");
					itemStep = 99;
				}
			}
			else if (itemStep == 2 && (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH < 0.1f)
			{
				stopTime += Time.deltaTime;
				if (stopTime > 20f)
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("疲劳驾驶项目结束");
					itemStep = 99;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType == 607)
			{
				if (itemStep == 0 && other.name == "Start_Position6")
				{
					GameObject.Find("DriveCamera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
					SoundPlayCtl.Instance.PlaySound("疲劳驾驶项目开始");
					itemStep = 1;
				}
				else if (itemStep != 2 && other.name == "StartMohu")
				{
					GameObject.Find("DriveCamera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = true;
					startMohu = true;
					itemStep = 2;
				}
			}
		}
	}
}
