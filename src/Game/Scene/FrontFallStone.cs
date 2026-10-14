using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class FrontFallStone : YingJiXunLianCtlBase
	{
		private static FrontFallStone instance;

		private float stopTime;

		private float notstopTime;

		public static FrontFallStone Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (itemStep == 2)
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed < 0.5f && CarState.Instance.CarSignal.footBrake > 0.3f)
				{
					stopTime += Time.deltaTime;
					if (stopTime > 3f)
					{
						ItemEnd();
						SoundPlayCtl.Instance.PlaySound("前方落石项目结束");
						itemStep = 99;
					}
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH < 0.5f && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.3f)
				{
					stopTime += Time.deltaTime;
					if (stopTime > 3f)
					{
						ItemEnd();
						SoundPlayCtl.Instance.PlaySound("前方落石项目结束");
						itemStep = 99;
					}
				}
			}
			else if (itemStep == 3)
			{
				notstopTime += Time.deltaTime;
				if (notstopTime > 2f)
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("前方落石项目结束");
					itemStep = 99;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_Position8")
			{
				SoundPlayCtl.Instance.PlaySound("前方落石项目开始");
				itemStep = 1;
			}
			else if (itemStep == 1 && other.name == "StartStopCar")
			{
				itemStep = 2;
			}
			else if (itemStep != 3 && other.name == "StopCar")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed > 1f)
				{
					SoundPlayCtl.Instance.PlaySound("前方道路遇到落石时,未减速至停车", 100f);
					itemStep = 3;
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH > 1f)
				{
					SoundPlayCtl.Instance.PlaySound("前方道路遇到落石时,未减速至停车", 100f);
					itemStep = 3;
				}
			}
			else if (itemStep != 99 && other.name == "FallStoneEnd")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("前方落石项目结束");
				itemStep = 99;
			}
		}
	}
}
