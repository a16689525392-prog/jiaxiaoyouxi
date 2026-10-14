using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class ShanQuPoDao : ShanQuSceneBase
	{
		private RaycastHit poDaoHitInfo;

		private bool isLowSpeed;

		private void Update()
		{
			if (itemStep != 3 || (!ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out poDaoHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out poDaoHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out poDaoHitInfo, 8)) || !(poDaoHitInfo.collider.name == "XiaPoLuDuan"))
			{
				return;
			}
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (!isLowSpeed && CarState.Instance.CurSpeed > 30f)
				{
					SoundPlayCtl.Instance.PlaySound("下坡路段,未减速行驶", 100f);
					isLowSpeed = true;
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 5 && !isLowSpeed && DriveHistoryPlay.Instance.curCalculateSpeedH > 30f)
			{
				SoundPlayCtl.Instance.PlaySound("下坡路段,未减速行驶", 100f);
				isLowSpeed = true;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "PoDaoStart")
			{
				SoundPlayCtl.Instance.PlaySound("前方有坡道");
				itemStep = 1;
			}
			else if (itemStep == 1 && other.name == "PoTop")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.oil > 0.4f)
				{
					SoundPlayCtl.Instance.PlaySound("驶近坡顶时,未松开油门", 10f);
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.oil > 0.4f)
				{
					SoundPlayCtl.Instance.PlaySound("驶近坡顶时,未松开油门", 10f);
				}
				itemStep = 2;
			}
			else if (itemStep == 2 && other.name == "XiaPo")
			{
				SoundPlayCtl.Instance.PlaySound("下坡,请注意车速");
				itemStep++;
			}
			else if (itemStep == 3 && other.name == "XiaPoJudge")
			{
				itemStep++;
			}
		}
	}
}
