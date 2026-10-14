using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class NoOil : YingJiXunLianCtlBase
	{
		private static NoOil instance;

		private float NoOilTime;

		private RaycastHit NoOilHitInfo;

		public static NoOil Instance => instance;

		private void Awake()
		{
			instance = this;
			CarCtl.IsNoOil = false;
		}

		private void Update()
		{
			if (itemStep > 0 && itemStep != 99)
			{
				CarCtl.IsNoOil = true;
				if (CarState.Instance.FireOn)
				{
					CarState.Instance.FireOn = false;
				}
				if (CarState.Instance.carRigibody.velocity.magnitude * 3.6f > 5f)
				{
					CarState.Instance.carRigibody.velocity -= new Vector3(0.01f, 0.01f, 0.01f);
				}
			}
			if (itemStep == 1)
			{
				NoOilTime += Time.deltaTime;
				if (CarState.Instance.CarSignal.turnLight == 3)
				{
					itemStep++;
					NoOilTime = 0f;
				}
				if (NoOilTime > 5f && CarState.Instance.CarSignal.turnLight != 3)
				{
					SoundPlayCtl.Instance.PlaySound("没油熄火时,未及时打开双闪", 100f);
					itemStep++;
					NoOilTime = 0f;
				}
			}
			if (itemStep == 2)
			{
				NoOilTime += Time.deltaTime;
				if ((!ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out NoOilHitInfo, 8) || !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[1].position, out NoOilHitInfo, 8)) && NoOilTime >= 20f)
				{
					SoundPlayCtl.Instance.PlaySound("未进行靠边停车操作", 100f);
					itemStep = 99;
				}
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out NoOilHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[1].position, out NoOilHitInfo, 8) && NoOilTime < 20f)
				{
					itemStep++;
					NoOilTime = 0f;
				}
			}
			if (itemStep == 3)
			{
				NoOilTime += Time.deltaTime;
				if (NoOilTime > 10f && CarState.Instance.CurSpeed >= 0.1f)
				{
					SoundPlayCtl.Instance.PlaySound("未进行靠边停车操作", 100f);
					itemStep = 99;
				}
				if (CarState.Instance.CurSpeed < 0.1f)
				{
					if (CarState.Instance.CarSignal.handBrake > 0.7f && NoOilTime < 12f)
					{
						itemStep = 99;
					}
					else if (NoOilTime > 12f)
					{
						SoundPlayCtl.Instance.PlaySound("停车后未拉紧手刹", 100f);
						itemStep = 99;
					}
				}
			}
			if (itemStep == 99)
			{
				SoundPlayCtl.Instance.PlaySound("中途没油项目结束");
				itemStep = 100;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "TiShiNoOil")
			{
				SoundPlayCtl.Instance.PlaySound("中途没油");
				itemStep = 1;
			}
		}
	}
}
