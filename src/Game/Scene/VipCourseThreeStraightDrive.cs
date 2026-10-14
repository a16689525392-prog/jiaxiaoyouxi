using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class VipCourseThreeStraightDrive : CourseThreeItemBase
	{
		private bool isJudgeSteerwheel;

		private void Update()
		{
			UpdateJudgeStraightDrive();
		}

		public override void RoadIndexChange(int preIndex, int curIndex)
		{
			if (itemStep != 0 && itemStep != 99 && !isJudgeSteerwheel)
			{
				SoundPlayCtl.Instance.PlaySound("方向控制不稳,不能保持车辆直线行驶", 10f);
				itemStep++;
				isJudgeSteerwheel = true;
			}
		}

		private void UpdateJudgeStraightDrive()
		{
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方直线行驶");
				itemStep++;
			}
			else if (itemStep >= 1 && itemStep != 99 && !isJudgeSteerwheel && Mathf.Abs(CarState.Instance.CarSignal.steerWheel) > 0.025f)
			{
				if (ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("方向控制不稳,不能保持车辆直线行驶", 10f);
				}
				itemStep++;
				isJudgeSteerwheel = true;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "StraightDriveItemOver" && itemStep != 99)
			{
				SoundPlayCtl.Instance.PlaySound("直线行驶完成", 0f, 0, 2.5f);
				itemStep = 99;
			}
		}
	}
}
