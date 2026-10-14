using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class MoNiSuiDao : CourseTwoItemCtlBase
	{
		private RaycastHit suiDaoHitInfo;

		private bool isLowSpeed;

		private bool isOtherRoad;

		private void Update()
		{
			UpdateSuiDao();
			UpdateCarStop();
		}

		private void UpdateSuiDao()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				if (CarState.Instance.CarSignal.turnLight != 2)
				{
					_ = CarState.Instance.CarSignal.turnLight;
				}
				itemStep++;
			}
			if (itemStep == 2 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out suiDaoHitInfo, 8)))
			{
				if (suiDaoHitInfo.collider.name == "Tunnel_InHorn")
				{
					if (CarState.Instance.CarSignal.horn == 1)
					{
						itemStep++;
					}
				}
				else if (suiDaoHitInfo.collider.name == "Tunnel_Front")
				{
					SoundPlayCtl.Instance.PlaySound("驶抵隧道入口时,未鸣喇叭", 10f);
					itemStep++;
				}
			}
			if (!isLowSpeed && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out suiDaoHitInfo, 8)) && suiDaoHitInfo.collider.name == "Tunnel_Front" && CarState.Instance.CurSpeed > 20f)
			{
				SoundPlayCtl.Instance.PlaySound("驶抵隧道时,未减速", 10f);
				isLowSpeed = true;
			}
			if (!isOtherRoad && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[0].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[0].position, out suiDaoHitInfo, 8)) && suiDaoHitInfo.collider.name == "Tunnel_Road_1")
			{
				SoundPlayCtl.Instance.PlaySound("占用对方车道", 100f);
				isOtherRoad = true;
			}
			if (itemStep == 6 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out suiDaoHitInfo, 8)))
			{
				if (suiDaoHitInfo.collider.name == "Tunnel_OutHorn" && CarState.Instance.CarSignal.horn == 1)
				{
					itemStep++;
				}
				if (suiDaoHitInfo.collider.name == "Tunnel_BeforeOut")
				{
					SoundPlayCtl.Instance.PlaySound("驶抵隧道出口时,未鸣喇叭", 10f);
					itemStep++;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_SuiDao")
			{
				SoundPlayCtl.Instance.PlaySound("模拟隧道考试开始");
				itemStep = 1;
			}
			if (itemStep != 4 && other.name == "OpenFrontLight")
			{
				if (CarState.Instance.CarSignal.frontLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("驶抵隧道时,未开启前照灯", 100f);
				}
				itemStep = 4;
			}
			if (itemStep != 6 && other.name == "Out_OpenHorn")
			{
				itemStep = 6;
			}
			else if (itemStep != 99 && other.name == "End_SuiDao")
			{
				_ = CarState.Instance.CarSignal.turnLight;
				if (CarState.Instance.CarSignal.frontLight != 0)
				{
					SoundPlayCtl.Instance.PlaySound("驶出隧道后,未关闭前照灯", 10f);
					itemStep++;
				}
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("模拟隧道考试结束");
				itemStep = 99;
			}
		}
	}
}
