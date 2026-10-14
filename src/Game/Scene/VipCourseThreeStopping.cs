using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class VipCourseThreeStopping : CourseThreeItemBase
	{
		private float RightLighttime;

		private float time;

		private void Update()
		{
			UpdateJudgeStopping();
		}

		private void UpdateJudgeStopping()
		{
			if (itemStep == 1 && CarState.Instance.CarSignal.turnLight == 2)
			{
				RightLighttime += Time.deltaTime;
			}
			if (itemStep == 1 && CarState.Instance.CarSignal.handBrake > 0.5f)
			{
				itemStep++;
			}
			if (itemStep == 2)
			{
				if (RightLighttime < 3f)
				{
					SoundPlayCtl.Instance.PlaySound("靠边停车时,未正确开启右转向灯3秒", 10f);
				}
				itemStep++;
			}
			if (itemStep == 3)
			{
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[2].position, out var hitinfo, 8))
				{
					if (ColliderDetection.instance.GetTriggerRaycastInfoName(ColliderDetection.instance.carMidPoint[2].position, out hitinfo, 8) == "30between50")
					{
						SoundPlayCtl.Instance.PlaySound("停车后,车辆距离道路边缘线超过规定距离", 10f);
					}
					else if (ColliderDetection.instance.GetTriggerRaycastInfoName(ColliderDetection.instance.carMidPoint[2].position, out hitinfo, 8) != "Less30")
					{
						SoundPlayCtl.Instance.PlaySound("停车后,车辆距离道路边缘线超过规定距离", 100f);
					}
				}
				else
				{
					SoundPlayCtl.Instance.PlaySound("停车后,车辆距离道路边缘线超过规定距离", 100f);
				}
				itemStep = 4;
			}
			if (itemStep < 4 || itemStep == 99)
			{
				return;
			}
			time += Time.deltaTime;
			if (time <= 8f && !CarState.Instance.FireOn && CarState.Instance.CarSignal.handBrake > 0.5f)
			{
				itemStep = 99;
				if (CourseThree.Instance.IsCourseThreeKaoShi)
				{
					VipCourseThreeKaoShi.Instance.isLastItem = true;
				}
			}
			if (!(time > 8f))
			{
				return;
			}
			if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1)
			{
				if (CarState.Instance.CarSignal.handBrake < 0.7f)
				{
					SoundPlayCtl.Instance.PlaySound("停车后,未及时拉紧手刹", 10f);
				}
				if (CarState.Instance.FireOn)
				{
					SoundPlayCtl.Instance.PlaySound("停车后,未及时熄火", 10f);
				}
			}
			itemStep = 99;
			if (CourseThree.Instance.IsCourseThreeKaoShi)
			{
				VipCourseThreeKaoShi.Instance.isLastItem = true;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "StoppingStart" && itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方靠边停车");
				RightLighttime = 0f;
				time = 0f;
				itemStep++;
			}
			if (other.name == "StoppingOver" && itemStep != 99)
			{
				SoundPlayCtl.Instance.PlaySound("未在规定距离内完成靠边停车操作", 100f);
				itemStep = 99;
			}
		}
	}
}
