using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class RangChaoChe : CourseThreeItemBase
	{
		public static bool isOverSpeed = false;

		public static bool isChangRoad = false;

		public static float errorShoudTime = 10f;

		private RaycastHit rangChaoCheHitInfo;

		private void Update()
		{
			BanStopping();
			errorShoudTime += Time.deltaTime;
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.CurSpeed > 30f && errorShoudTime > 10f && AICarMove.start && !AICarMove.isEndOver)
				{
					SoundPlayCtl.Instance.PlaySound("后方车辆将要超车时,车速过快", 100f);
					errorShoudTime = 0f;
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curCalculateSpeedH > 30f && errorShoudTime > 10f && AICarMove.start && !AICarMove.isEndOver)
			{
				SoundPlayCtl.Instance.PlaySound("后方车辆将要超车时,车速过快", 100f);
				errorShoudTime = 0f;
			}
			if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out rangChaoCheHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out rangChaoCheHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out rangChaoCheHitInfo, 8)) && rangChaoCheHitInfo.collider.name == "IfChangeRoad" && !isChangRoad)
			{
				SoundPlayCtl.Instance.PlaySound("后方车辆将要超车时变道", 100f);
				isChangRoad = true;
			}
		}
	}
}
