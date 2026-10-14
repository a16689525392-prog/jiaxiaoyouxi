using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class HuNanYZCourseThreeDaoCar : CourseThreeItemBase
	{
		private RaycastHit daoCarHitInfo;

		private float StopTime;

		private void Update()
		{
			UpdateJudgeDaoCar();
			BanStopping();
			MonoBehaviour.print(CourseThree.Instance.dirveLength - startLength);
			if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out daoCarHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out daoCarHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out daoCarHitInfo, 8))
			{
				if (daoCarHitInfo.collider.name == "DaoCheRoad" && itemStep != 99)
				{
					SoundPlayCtl.Instance.PlaySound("倒车项目结束", 0f, 0, 2.5f);
					itemStep = 99;
				}
				else if (daoCarHitInfo.collider.name == "ErrorRoad" && itemStep == 1)
				{
					itemStep = 2;
				}
			}
		}

		private void UpdateJudgeDaoCar()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("请倒车,将车辆开到正确车道上");
				itemStep++;
			}
			if (CourseThree.Instance.dirveLength - startLength > 20f && itemStep < 3)
			{
				if (itemStep == 1 && CarState.Instance.CarSignal.gear != 6)
				{
					SoundPlayCtl.Instance.PlaySound("未进行倒车操作", 100f);
					itemStep = 3;
				}
				else if (itemStep == 2)
				{
					StopTime += Time.deltaTime;
					if (StopTime > 5f)
					{
						SoundPlayCtl.Instance.PlaySound("未按规定进行倒车操作", 100f);
						itemStep = 3;
					}
				}
			}
			if (itemStep == 3)
			{
				SoundPlayCtl.Instance.PlaySound("倒车项目结束", 0f, 0, 2.5f);
				itemStep = 99;
			}
		}
	}
}
