using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class FrontAccident : YingJiXunLianCtlBase
	{
		private static FrontAccident instance;

		private bool YingJi;

		private RaycastHit frontAcctidentHitInfo;

		public static FrontAccident Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (!YingJi && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out frontAcctidentHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out frontAcctidentHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out frontAcctidentHitInfo, 8)) && frontAcctidentHitInfo.collider.name == "YingJiRoad")
			{
				SoundPlayCtl.Instance.PlaySound("行驶中,占用应急车道", 100f);
				YingJi = true;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_Position9")
			{
				SoundPlayCtl.Instance.PlaySound("前方事故项目开始");
				itemStep = 1;
			}
			else if (itemStep != 2 && other.name == "OpenLight")
			{
				if (CarState.Instance.CarSignal.turnLight != 3)
				{
					SoundPlayCtl.Instance.PlaySound("道路上发生故障妨碍交通难移动时,不开启示宽灯和危险报警灯", 100f);
				}
				itemStep = 2;
			}
			else if (itemStep != 99 && other.name == "StopCar")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("高速公路车辆发生故障,未及时平稳靠边停车", 100f);
				itemStep = 99;
			}
		}
	}
}
