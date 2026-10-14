using Car;
using SoundAndTextShow;
using View;

namespace Scene
{
	public class HuNanYZCourseThreeThroughMeetting : CourseThreeItemBase
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
				SoundPlayCtl.Instance.PlaySound("会车");
				itemStep++;
			}
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				itemStep++;
			}
			if (itemStep == 2 && CarState.Instance.CarSignal.footBrake > 0.1f)
			{
				itemStep++;
			}
			if (itemStep < 4 && CourseThree.Instance.dirveLength - startLength > 120f)
			{
				if (itemStep == 2)
				{
					if (ViewJumpCtl.Instance.modelType != 0)
					{
						SoundPlayCtl.Instance.PlaySound("会车时,未按规定进行减速操作", 10f);
					}
				}
				else if (CarState.Instance.CurSpeed > 30f && ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("会车时,未将车速降低至30km/h以下", 10f);
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
