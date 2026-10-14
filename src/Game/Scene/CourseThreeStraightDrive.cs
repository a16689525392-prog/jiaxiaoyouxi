using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class CourseThreeStraightDrive : CourseThreeItemBase
	{
		private bool isJudgeSpeed;

		private bool isJudgeSteerwheel;

		private void Update()
		{
			UpdateJudgeStraightDrive();
			BanStopping();
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
				SoundPlayCtl.Instance.PlaySound("直线行驶项目开始");
				itemStep++;
			}
			else if (itemStep >= 1 && itemStep != 99)
			{
				if (!isJudgeSteerwheel && Mathf.Abs(CarState.Instance.CarSignal.steerWheel) > 0.1f)
				{
					if (ViewJumpCtl.Instance.modelType != 0)
					{
						SoundPlayCtl.Instance.PlaySound("方向控制不稳,不能保持车辆直线行驶", 10f);
					}
					itemStep++;
					isJudgeSteerwheel = true;
				}
				if (!isJudgeSpeed && !CarActionJudge.Instance.JudgeActionDone(108) && CourseThree.Instance.dirveLength - startLength > 200f)
				{
					if (ViewJumpCtl.Instance.modelType != 0)
					{
						SoundPlayCtl.Instance.PlaySound("直线行驶时,车速未达到30km/h以上", 10f);
					}
					itemStep++;
					isJudgeSpeed = true;
				}
			}
			if (itemStep != 99 && CourseThree.Instance.dirveLength - startLength > 600f)
			{
				itemStep = 99;
				SoundPlayCtl.Instance.PlaySound("直线行驶项目结束", 0f, 0, 2.5f);
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
