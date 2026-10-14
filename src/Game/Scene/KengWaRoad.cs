using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class KengWaRoad : YingJiXunLianCtlBase
	{
		private static KengWaRoad instance;

		private RaycastHit kengWaHitInfo;

		private bool isLowSpeed;

		public static KengWaRoad Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (!isLowSpeed && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out kengWaHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out kengWaHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out kengWaHitInfo, 8)) && kengWaHitInfo.collider.name == "KengWaLu")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.footBrake < 0.2f && CarState.Instance.CurSpeed > 15f)
				{
					SoundPlayCtl.Instance.PlaySound("通过坑洼路面时,未轻踩脚刹低速平稳通过", 100f);
					isLowSpeed = true;
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake < 0.2f && DriveHistoryPlay.Instance.curCalculateSpeedH > 15f)
				{
					SoundPlayCtl.Instance.PlaySound("通过坑洼路面时,未轻踩脚刹低速平稳通过", 100f);
					isLowSpeed = true;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType == 606)
			{
				if (itemStep == 0 && other.name == "Start_Position5")
				{
					SoundPlayCtl.Instance.PlaySound("坑洼路面项目开始");
					itemStep = 1;
				}
				else if (itemStep != 99 && other.name == "KengWaEnd")
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("坑洼路面项目结束");
					itemStep = 99;
				}
			}
		}
	}
}
