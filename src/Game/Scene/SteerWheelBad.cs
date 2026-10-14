using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class SteerWheelBad : YingJiXunLianCtlBase
	{
		private static SteerWheelBad instance;

		private RaycastHit steerWheelBadHitInfo;

		private bool isBrake;

		private bool isSlowBrake;

		private bool isHorn;

		private bool isLowSpeed = true;

		private float brakeBadTime;

		public static SteerWheelBad Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out steerWheelBadHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out steerWheelBadHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out steerWheelBadHitInfo, 8))
			{
				if (steerWheelBadHitInfo.collider.name == "Brake")
				{
					if (ViewJumpCtl.Instance.modelType == 2)
					{
						if (CarState.Instance.CarSignal.footBrake > 0.6f && CarState.Instance.CurSpeed > 10f && !isBrake)
						{
							SoundPlayCtl.Instance.PlaySound("转向装置失控时,脚刹踩的太急", 100f);
							isBrake = true;
						}
						if (CarState.Instance.CarSignal.footBrake < 0.05f && !isSlowBrake)
						{
							SoundPlayCtl.Instance.PlaySound("转向装置失控时,未轻踩脚刹", 100f);
							isSlowBrake = true;
						}
					}
					else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
					{
						if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.6f && DriveHistoryPlay.Instance.curCalculateSpeedH > 10f && !isBrake)
						{
							SoundPlayCtl.Instance.PlaySound("转向装置失控时,脚刹踩的太急", 100f);
							isBrake = true;
						}
						if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake < 0.05f && !isSlowBrake)
						{
							SoundPlayCtl.Instance.PlaySound("转向装置失控时,未轻踩脚刹", 100f);
							isSlowBrake = true;
						}
					}
				}
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (steerWheelBadHitInfo.collider.name == "OnHorn" && !isHorn && CarState.Instance.CarSignal.horn == 1)
					{
						isHorn = true;
					}
					if (steerWheelBadHitInfo.collider.name == "NotHorn" && !isHorn)
					{
						SoundPlayCtl.Instance.PlaySound("转向装置失控时,未鸣喇叭提醒其他车辆,行人", 100f);
						isHorn = true;
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
				{
					if (steerWheelBadHitInfo.collider.name == "OnHorn" && !isHorn && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn == 1)
					{
						isHorn = true;
					}
					if (steerWheelBadHitInfo.collider.name == "NotHorn" && !isHorn)
					{
						SoundPlayCtl.Instance.PlaySound("转向装置失控时,未鸣喇叭提醒其他车辆,行人", 100f);
						isHorn = true;
					}
				}
			}
			if (isLowSpeed)
			{
				return;
			}
			if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed > 10f)
			{
				brakeBadTime += Time.deltaTime;
				if (brakeBadTime > 20f)
				{
					SoundPlayCtl.Instance.PlaySound("转向装置失控时,未及时停车", 100f);
					isLowSpeed = true;
				}
			}
			else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH > 10f)
			{
				brakeBadTime += Time.deltaTime;
				Debug.Log(brakeBadTime);
				if (brakeBadTime > 20f)
				{
					SoundPlayCtl.Instance.PlaySound("转向装置失控时,未及时停车", 100f);
					isLowSpeed = true;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType != 605)
			{
				return;
			}
			if (itemStep == 0 && other.name == "Start_Position4")
			{
				SoundPlayCtl.Instance.PlaySound("方向盘失灵项目开始");
				isLowSpeed = false;
				itemStep = 1;
			}
			else if (itemStep == 1 && other.name == "SteerWheelBad")
			{
				SoundPlayCtl.Instance.PlaySound("车辆突发故障,方向盘转动失效");
				itemStep = 2;
			}
			else if (itemStep == 2 && other.name == "Judge")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.turnLight != 3)
				{
					SoundPlayCtl.Instance.PlaySound("转向装置失控时,未开危险警示灯提醒其他车辆,行人", 100f);
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 3)
				{
					SoundPlayCtl.Instance.PlaySound("转向装置失控时,未开危险警示灯提醒其他车辆,行人", 100f);
				}
				itemStep = 3;
			}
			else if (itemStep != 5 && other.name == "LowSpeedStop")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed < 10f)
				{
					SoundPlayCtl.Instance.PlaySound("降低车速后未及时停车", 100f);
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH < 10f)
				{
					SoundPlayCtl.Instance.PlaySound("降低车速后未及时停车", 100f);
				}
				itemStep = 5;
			}
			else if (itemStep != 99 && other.name == "SteerWheelBadEnd")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("方向盘失灵项目结束");
				itemStep = 99;
			}
		}
	}
}
