using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class ShanQuWanDao : ShanQuSceneBase
	{
		private RaycastHit wanDaoHitInfo;

		private bool isLowSpeed;

		private bool isOpenHorn;

		private void Update()
		{
			if (itemStep == 1 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out wanDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out wanDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out wanDaoHitInfo, 8)))
			{
				if (wanDaoHitInfo.collider.name == "Wan_Horn")
				{
					if (ViewJumpCtl.Instance.modelType == 2)
					{
						if (!isOpenHorn && CarState.Instance.CarSignal.horn == 1)
						{
							isOpenHorn = true;
						}
					}
					else if (ViewJumpCtl.Instance.modelType == 5 && !isOpenHorn && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn == 1)
					{
						isOpenHorn = true;
					}
				}
				else if (wanDaoHitInfo.collider.name == "Wan_BeforeWan")
				{
					if (!isOpenHorn)
					{
						SoundPlayCtl.Instance.PlaySound("进入弯道前,未鸣喇叭", 10f);
						isOpenHorn = true;
					}
					if (ViewJumpCtl.Instance.modelType == 2)
					{
						if (!isLowSpeed && CarState.Instance.CurSpeed > 30f)
						{
							SoundPlayCtl.Instance.PlaySound("进入弯道前,未减速至通过弯道所需的速度", 100f);
							isLowSpeed = true;
						}
					}
					else if (ViewJumpCtl.Instance.modelType == 5 && !isLowSpeed && DriveHistoryPlay.Instance.curCalculateSpeedH > 30f)
					{
						SoundPlayCtl.Instance.PlaySound("进入弯道前,未减速至通过弯道所需的速度", 100f);
						isLowSpeed = true;
					}
					itemStep++;
				}
			}
			if (itemStep == 2 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out wanDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[0].position, out wanDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[0].position, out wanDaoHitInfo, 8)) && wanDaoHitInfo.collider.name == "Wan_Road1")
			{
				SoundPlayCtl.Instance.PlaySound("弯道内占用对方车道", 100f);
				itemStep++;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Wan_Start")
			{
				SoundPlayCtl.Instance.PlaySound("前方连续急弯");
				itemStep = 1;
			}
			else if (itemStep != 99 && other.name == "Wan_End")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("连续急弯结束");
				itemStep = 99;
			}
		}
	}
}
