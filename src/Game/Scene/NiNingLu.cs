using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class NiNingLu : XueJingSceneBase
	{
		private bool isLowGear;

		private bool isBrake;

		private bool isAddSpeed;

		private bool isLowSpeed;

		private RaycastHit xueJingHitInfo;

		protected override void Start()
		{
			SoundPlayCtl.Instance.PlaySound("泥泞道路，请低速平稳通过");
		}

		private void Update()
		{
			if ((!ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out xueJingHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out xueJingHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out xueJingHitInfo, 8)) || !(xueJingHitInfo.collider.name == "NiNingTrigger"))
			{
				return;
			}
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.CarSignal.gear != 1 && CarState.Instance.CarSignal.gear != 6 && CarState.Instance.CarSignal.gear != 0 && !isLowGear)
				{
					SoundPlayCtl.Instance.PlaySound("未能使用低速挡平稳通过", 100f);
					isLowGear = true;
				}
				if (CarState.Instance.CurSpeed > 15f && !isLowSpeed)
				{
					SoundPlayCtl.Instance.PlaySound("湿滑路面未减速", 100f);
					isLowSpeed = true;
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
					SoundPlayCtl.Instance.PlaySound("未能使用低速挡平稳通过", 100f);
					isLowGear = true;
				}
				if (DriveHistoryPlay.Instance.curCalculateSpeedH > 15f && !isLowSpeed)
				{
					SoundPlayCtl.Instance.PlaySound("湿滑路面未减速", 100f);
					isLowSpeed = true;
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
	}
}
