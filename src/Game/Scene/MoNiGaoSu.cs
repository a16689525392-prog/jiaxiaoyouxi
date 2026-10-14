using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class MoNiGaoSu : CourseTwoItemCtlBase
	{
		private RaycastHit gaoSuHitInfo;

		private bool DriveRoad0;

		private bool DriveRoad1;

		private bool isAidRoad;

		public static bool kaoShiJieShu;

		private float driveTime;

		private float addSpeedTime;

		private void FixedUpdate()
		{
			UpdateDriveRoad();
			UpdateGaoSu();
			UpdateCarStop();
			if (DriveRoad0 && !DriveRoad1 && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out gaoSuHitInfo, 11) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[0].position, out gaoSuHitInfo, 11) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[0].position, out gaoSuHitInfo, 11) && gaoSuHitInfo.collider.name == "High_Road1")
			{
				driveTime += Time.deltaTime;
				Debug.Log(driveTime);
				if (driveTime > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("行驶中,占用两条车道", 100f);
					driveTime = 0f;
				}
			}
			if (DriveRoad1 && !DriveRoad0 && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out gaoSuHitInfo, 11) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[2].position, out gaoSuHitInfo, 11) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[2].position, out gaoSuHitInfo, 11) && gaoSuHitInfo.collider.name == "High_Road0")
			{
				driveTime += Time.deltaTime;
				Debug.Log(driveTime);
				if (driveTime > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("行驶中,占用两条车道", 100f);
					driveTime = 0f;
				}
			}
		}

		private void UpdateDriveRoad()
		{
			if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out gaoSuHitInfo, 11) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[2].position, out gaoSuHitInfo, 11) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[2].position, out gaoSuHitInfo, 11) && gaoSuHitInfo.collider.name == "High_Road0")
			{
				DriveRoad0 = true;
				DriveRoad1 = false;
			}
			if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out gaoSuHitInfo, 11) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[0].position, out gaoSuHitInfo, 11) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[0].position, out gaoSuHitInfo, 11) && gaoSuHitInfo.collider.name == "High_Road1")
			{
				DriveRoad1 = true;
				DriveRoad0 = false;
			}
		}

		private void UpdateGaoSu()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				if (CarState.Instance.CarSignal.turnLight != 2)
				{
					_ = CarState.Instance.CarSignal.turnLight;
				}
				itemStep++;
			}
			if (itemStep == 2 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out gaoSuHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out gaoSuHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out gaoSuHitInfo, 8)) && gaoSuHitInfo.collider.name == "High_In")
			{
				if (CarState.Instance.CurSpeed < 20f)
				{
					SoundPlayCtl.Instance.PlaySound("驶入高速公路时,未提速至规定车速", 100f);
				}
				itemStep++;
			}
			if (itemStep == 3)
			{
				addSpeedTime += Time.deltaTime;
				if (CarState.Instance.CurSpeed < 60f && addSpeedTime > 20f)
				{
					SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 100f);
					itemStep++;
				}
			}
			if (itemStep == 5 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out gaoSuHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out gaoSuHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out gaoSuHitInfo, 8)) && gaoSuHitInfo.collider.name == "High_OutSlowArea" && CarState.Instance.CurSpeed > 60f)
			{
				SoundPlayCtl.Instance.PlaySound("驶出高速公路时,未按照出口预告标志提前调整车速", 100f);
				itemStep++;
			}
			if (!isAidRoad && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out gaoSuHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out gaoSuHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out gaoSuHitInfo, 8) && gaoSuHitInfo.collider.name == "High_AidLane")
			{
				SoundPlayCtl.Instance.PlaySound("行驶中,占用应急车道", 100f);
				isAidRoad = true;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_GaoSu")
			{
				SoundPlayCtl.Instance.PlaySound("模拟高速公路考试开始");
				itemStep = 1;
			}
			else if (itemStep == 3 && other.name == "LowSpeed")
			{
				itemStep = 4;
			}
			else if (itemStep != 5 && other.name == "OpenRightLight")
			{
				if (CarState.Instance.CarSignal.turnLight == 1)
				{
					SoundPlayCtl.Instance.PlaySound("未正确开启转向灯", 10f);
				}
				else if (CarState.Instance.CarSignal.turnLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("未开启右转向灯", 100f);
				}
				itemStep = 5;
			}
			else if (itemStep != 99 && other.name == "OutErrorArea")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("驶出高速公路时,未按照出口预告标志提前调整车道", 100f);
				itemStep = 99;
			}
			else if (itemStep != 99 && other.name == "End_GaoSu")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("模拟高速公路考试结束");
				itemStep = 99;
				kaoShiJieShu = true;
			}
		}
	}
}
