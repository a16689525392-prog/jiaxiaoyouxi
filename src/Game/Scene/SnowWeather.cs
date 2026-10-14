using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class SnowWeather : XueJingSceneBase
	{
		private float SnowTime;

		private bool isLowGear;

		private bool isBrake;

		private bool isAddSpeed;

		private bool isFogLightOpen;

		private bool isFrontLightNear;

		private bool isWidthLightOpen;

		private RaycastHit xueJingHitInfo;

		protected override void Start()
		{
			SoundPlayCtl.Instance.PlaySound("雪天请保持车辆低速档平稳行驶");
		}

		private void Update()
		{
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.FireOn && CarState.Instance.CurSpeed > 3f)
				{
					SnowTime += Time.deltaTime;
					if (CarState.Instance.CarSignal.fogLight == 0 && SettingInfoCtl.Instance.isJudgeFogLight && !isFogLightOpen && SnowTime > 5f)
					{
						SoundPlayCtl.Instance.PlaySound("雪天未开启雾灯", 10f);
						isFogLightOpen = true;
					}
					if (!isFrontLightNear && CarState.Instance.CarSignal.frontLight != 1 && SnowTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("雪天未开启近光灯", 10f);
						isFrontLightNear = true;
					}
					if (!isWidthLightOpen && CarState.Instance.CarSignal.widthLight != 1 && SnowTime > 15f)
					{
						SoundPlayCtl.Instance.PlaySound("雪天未开启示宽灯", 10f);
						isWidthLightOpen = true;
					}
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && CarState.Instance.FireOn && DriveHistoryPlay.Instance.curCalculateSpeedH > 3f)
			{
				SnowTime += Time.deltaTime;
				if (!isFogLightOpen && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fogLight == 0 && SettingInfoCtl.Instance.isJudgeFogLight && SnowTime > 5f)
				{
					SoundPlayCtl.Instance.PlaySound("雪天未开启雾灯", 10f);
					isFogLightOpen = true;
				}
				if (!isFrontLightNear && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight != 1 && SnowTime > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("雪天未开启近光灯", 10f);
					isFrontLightNear = true;
				}
				if (!isWidthLightOpen && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.widthLight != 1 && SnowTime > 15f)
				{
					SoundPlayCtl.Instance.PlaySound("雪天未开启示宽灯", 10f);
					isWidthLightOpen = true;
				}
			}
			if (itemStep != 1 || (!ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out xueJingHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out xueJingHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out xueJingHitInfo, 8)) || !(xueJingHitInfo.collider.name == "SnowTrigger"))
			{
				return;
			}
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.CarSignal.gear != 1 && CarState.Instance.CarSignal.gear != 6 && CarState.Instance.CarSignal.gear != 0 && !isLowGear)
				{
					SoundPlayCtl.Instance.PlaySound("没有使用低速档平稳行驶", 100f);
					isLowGear = true;
				}
				if (CarState.Instance.CarSignal.footBrake > 0.6f && !isBrake)
				{
					SoundPlayCtl.Instance.PlaySound("急刹车", 100f);
					isBrake = true;
				}
				if (CarState.Instance.CarSignal.oil > 0.6f && !isAddSpeed)
				{
					SoundPlayCtl.Instance.PlaySound("急加速", 100f);
					isAddSpeed = true;
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 5)
			{
				if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear != 1 && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear != 6 && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear != 0 && !isLowGear)
				{
					SoundPlayCtl.Instance.PlaySound("没有使用低速档平稳行驶", 100f);
					isLowGear = true;
				}
				if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.6f && !isBrake)
				{
					SoundPlayCtl.Instance.PlaySound("急刹车", 100f);
					isBrake = true;
				}
				if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.oil > 0.6f && !isAddSpeed)
				{
					SoundPlayCtl.Instance.PlaySound("急加速", 100f);
					isAddSpeed = true;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "StartJudge")
			{
				itemStep = 1;
			}
		}
	}
}
