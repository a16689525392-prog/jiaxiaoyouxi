using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class YuTian : CourseTwoItemCtlBase
	{
		private RaycastHit yuTianHitInfo;

		public Light dirLight;

		public static GameObject rain;

		private void Update()
		{
			UpdateYuTian();
			UpdateCarStop();
		}

		private void UpdateYuTian()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				if (CarState.Instance.CarSignal.turnLight != 2)
				{
					_ = CarState.Instance.CarSignal.turnLight;
				}
				itemStep++;
			}
			if (itemStep == 3 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out yuTianHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out yuTianHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out yuTianHitInfo, 8)) && yuTianHitInfo.collider.name == "Speed_Judge")
			{
				if (CarState.Instance.CurSpeed > 20f)
				{
					SoundPlayCtl.Instance.PlaySound("雨天驾驶速度过快", 100f);
				}
				itemStep++;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_YuTian")
			{
				SoundPlayCtl.Instance.PlaySound("模拟雨天行驶考试开始");
				if (ViewJumpCtl.Instance.itemType == 12)
				{
					rain = Object.Instantiate(Resources.Load("RainGlass")) as GameObject;
					GameObject gameObject = GameObject.Find("DFCourseTwo");
					rain.transform.parent = gameObject.transform;
				}
				itemStep = 1;
			}
			else if (itemStep != 3 && other.name == "YuGua_Judge")
			{
				if (CarState.Instance.CarSignal.rainBrush == 0)
				{
					SoundPlayCtl.Instance.PlaySound("雨天未开启雨刮器", 100f);
				}
				itemStep = 3;
			}
			else if (itemStep != 99 && other.name == "End_YuTian")
			{
				_ = CarState.Instance.CarSignal.turnLight;
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("模拟雨天行驶考试结束");
				Object.Destroy(rain);
				itemStep = 99;
			}
		}
	}
}
