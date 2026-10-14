using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class WuTian : CourseTwoItemCtlBase
	{
		private RaycastHit wuTianHitInfo;

		private float JianGeTime = 10f;

		private bool isFogLightOpen;

		private bool isFrontLightNear;

		private bool isWidthLightOpen;

		private bool isBlinkLightOpen;

		private void Update()
		{
			UpdateWuTian();
			UpdateCarStop();
		}

		private void UpdateWuTian()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				if (CarState.Instance.CarSignal.turnLight != 2)
				{
					_ = CarState.Instance.CarSignal.turnLight;
				}
				itemStep++;
			}
			if (itemStep == 2 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out wuTianHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out wuTianHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out wuTianHitInfo, 8)) && wuTianHitInfo.collider.name == "Fog_Judge")
			{
				if (CarState.Instance.CarSignal.fogLight == 0 && SettingInfoCtl.Instance.isJudgeFogLight && !isFogLightOpen)
				{
					SoundPlayCtl.Instance.PlaySound("雾天未开启雾灯", 100f);
					isFogLightOpen = true;
				}
				if (CarState.Instance.CarSignal.frontLight != 1 && !isFrontLightNear)
				{
					SoundPlayCtl.Instance.PlaySound("雾天未开启近光灯", 100f);
					isFrontLightNear = true;
				}
				if (CarState.Instance.CarSignal.widthLight != 1 && !isWidthLightOpen)
				{
					SoundPlayCtl.Instance.PlaySound("雾天未开启示宽灯", 100f);
					isWidthLightOpen = true;
				}
				if (CarState.Instance.CarSignal.turnLight != 3 && !isBlinkLightOpen)
				{
					SoundPlayCtl.Instance.PlaySound("雾天未开启危险警报闪光灯", 100f);
					isBlinkLightOpen = true;
				}
				JianGeTime += Time.deltaTime;
				if (JianGeTime > 10f && CarState.Instance.CurSpeed > 20f)
				{
					SoundPlayCtl.Instance.PlaySound("雾天驾驶速度过快", 100f);
					JianGeTime = 0f;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_WuTian")
			{
				SoundPlayCtl.Instance.PlaySound("模拟雾天行驶考试开始");
				itemStep = 1;
			}
			else if (itemStep != 99 && other.name == "End_WuTian")
			{
				_ = CarState.Instance.CarSignal.turnLight;
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("模拟雾天行驶考试结束");
				SkyBoxCtl.Instance.ChangeSkyBox(ViewJumpCtl.Instance.otherType = 504);
				itemStep = 99;
			}
		}
	}
}
