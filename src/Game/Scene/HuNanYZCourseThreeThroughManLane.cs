using Car;
using SoundAndTextShow;
using View;

namespace Scene
{
	public class HuNanYZCourseThreeThroughManLane : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeThrougManLane();
		}

		private void UpdateJudgeThrougManLane()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				itemStep++;
			}
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				itemStep++;
			}
			if (itemStep == 2 && CarState.Instance.CarSignal.footBrake > 0.1f && CourseThree.Instance.dirveLength - startLength > 25f)
			{
				itemStep++;
			}
			if (itemStep < 4 && CourseThree.Instance.dirveLength - startLength > 85f)
			{
				if (itemStep == 2 && ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行减速操作", 10f);
				}
				itemStep = 4;
			}
			if (itemStep == 4 && CourseThree.Instance.dirveLength - startLength > 120f)
			{
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
