using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class XianKuan : CourseTwoItemCtlBase
	{
		private RaycastHit xianKuanHitInfo;

		private void Update()
		{
			UpdateXianKuan();
			UpdateCarStop();
		}

		private void UpdateXianKuan()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				if (CarState.Instance.CarSignal.turnLight != 2)
				{
					_ = CarState.Instance.CarSignal.turnLight;
				}
				itemStep++;
			}
			if (itemStep == 2 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out xianKuanHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out xianKuanHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out xianKuanHitInfo, 8)))
			{
				if (xianKuanHitInfo.collider.name == "Men_First")
				{
					if (CarState.Instance.CurSpeed < 10f)
					{
						SoundPlayCtl.Instance.PlaySound("时速低于10公里/小时通过", 10f);
						itemStep++;
					}
				}
				else if (xianKuanHitInfo.collider.name == "Men_FirstLeft")
				{
					SoundPlayCtl.Instance.PlaySound("不按规定路线顺序行驶", 100f);
					itemStep++;
				}
			}
			if (itemStep == 4 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out xianKuanHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out xianKuanHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out xianKuanHitInfo, 8)))
			{
				if (xianKuanHitInfo.collider.name == "Men_Second")
				{
					if (CarState.Instance.CurSpeed < 10f)
					{
						SoundPlayCtl.Instance.PlaySound("时速低于10公里/小时通过", 10f);
						itemStep++;
					}
				}
				else if (xianKuanHitInfo.collider.name == "Men_SecondRight")
				{
					SoundPlayCtl.Instance.PlaySound("不按规定路线顺序行驶", 100f);
					itemStep++;
				}
			}
			if (itemStep != 6 || (!ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out xianKuanHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out xianKuanHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out xianKuanHitInfo, 8)))
			{
				return;
			}
			if (xianKuanHitInfo.collider.name == "Men_Third")
			{
				if (CarState.Instance.CurSpeed < 10f)
				{
					SoundPlayCtl.Instance.PlaySound("时速低于10公里/小时通过", 10f);
					itemStep++;
				}
			}
			else if (xianKuanHitInfo.collider.name == "Men_ThirdLeft")
			{
				SoundPlayCtl.Instance.PlaySound("不按规定路线顺序行驶", 100f);
				itemStep++;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_XianKuan")
			{
				SoundPlayCtl.Instance.PlaySound("限速通过限宽门考试开始");
				itemStep = 1;
			}
			if (other.name == "XianKuan_Men1")
			{
				itemStep = 4;
			}
			if (other.name == "XianKuan_Men2")
			{
				itemStep = 6;
			}
			else if (itemStep != 99 && other.name == "End_XianKuan")
			{
				_ = CarState.Instance.CarSignal.turnLight;
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("限速通过限宽门考试结束");
				itemStep = 99;
			}
		}
	}
}
