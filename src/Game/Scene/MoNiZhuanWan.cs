using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class MoNiZhuanWan : CourseTwoItemCtlBase
	{
		private RaycastHit zhuanWanHitInfo;

		private bool isOtherRoad;

		private void Update()
		{
			UpdateZhuanWan();
			UpdateCarStop();
		}

		private void UpdateZhuanWan()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				if (CarState.Instance.CarSignal.turnLight != 2)
				{
					_ = CarState.Instance.CarSignal.turnLight;
				}
				itemStep++;
			}
			if (itemStep == 2 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out zhuanWanHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out zhuanWanHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out zhuanWanHitInfo, 8)))
			{
				if (zhuanWanHitInfo.collider.name == "OpenHorn")
				{
					if (CarState.Instance.CarSignal.horn == 1)
					{
						itemStep++;
					}
				}
				else if (zhuanWanHitInfo.collider.name == "Wan_BeforeWan")
				{
					SoundPlayCtl.Instance.PlaySound("进入弯道前,未鸣喇叭", 10f);
					itemStep++;
				}
			}
			if (!isOtherRoad && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out zhuanWanHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[0].position, out zhuanWanHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[0].position, out zhuanWanHitInfo, 8)) && zhuanWanHitInfo.collider.name == "Wan_Road1")
			{
				SoundPlayCtl.Instance.PlaySound("弯道内占用对方车道", 100f);
				isOtherRoad = true;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_ZhuanWan")
			{
				SoundPlayCtl.Instance.PlaySound("模拟连续急转弯山区路行驶考试开始");
				itemStep = 1;
			}
			if (itemStep != 4 && other.name == "Speed_Judge")
			{
				if (CarState.Instance.CurSpeed > 20f)
				{
					SoundPlayCtl.Instance.PlaySound("进入弯道前,未减速至通过弯道所需的速度", 10f);
				}
				itemStep = 4;
			}
			else if (itemStep != 99 && other.name == "End_ZhuanWan")
			{
				_ = CarState.Instance.CarSignal.turnLight;
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("模拟连续急转弯山区路行驶考试结束");
				itemStep = 99;
			}
		}
	}
}
