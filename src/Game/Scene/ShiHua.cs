using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class ShiHua : CourseTwoItemCtlBase
	{
		private RaycastHit shiHuaHitInfo;

		private bool isBrake;

		private bool isAddSpeed;

		private bool isLowGear;

		private void Update()
		{
			UpdateShiHua();
			UpdateCarStop();
		}

		private void UpdateShiHua()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				if (CarState.Instance.CarSignal.turnLight != 2)
				{
					_ = CarState.Instance.CarSignal.turnLight;
				}
				itemStep++;
			}
			if (itemStep == 3 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out shiHuaHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out shiHuaHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out shiHuaHitInfo, 8)) && shiHuaHitInfo.collider.name == "Wet_Judge")
			{
				if (CarState.Instance.CarSignal.footBrake > 0.4f && !isBrake)
				{
					SoundPlayCtl.Instance.PlaySound("通过时,急刹车", 100f);
					isBrake = true;
				}
				if (CarState.Instance.CarSignal.oil > 0.2f && !isAddSpeed)
				{
					SoundPlayCtl.Instance.PlaySound("通过时,急加速", 100f);
					isAddSpeed = true;
				}
				if (!CarActionJudge.Instance.JudgeActionDone(34) && !isLowGear)
				{
					SoundPlayCtl.Instance.PlaySound("未能使用低速挡平稳通过", 100f);
					isLowGear = true;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_ShiHua")
			{
				SoundPlayCtl.Instance.PlaySound("湿滑路面考试开始");
				itemStep = 1;
			}
			else if (itemStep != 3 && other.name == "LowSpeed_Judge")
			{
				if (CarState.Instance.CurSpeed > 20f)
				{
					SoundPlayCtl.Instance.PlaySound("进入湿滑路前,未减速", 100f);
				}
				itemStep = 3;
			}
			else if (itemStep != 99 && other.name == "End_ShiHua")
			{
				_ = CarState.Instance.CarSignal.turnLight;
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("湿滑路面考试结束");
				itemStep = 99;
			}
		}
	}
}
