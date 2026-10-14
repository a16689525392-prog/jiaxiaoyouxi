using Car;
using SoundAndTextShow;
using View;

namespace Scene
{
	public class CourseThreeThroughMeetting : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeThrougManLane();
			BanStopping();
		}

		private void UpdateJudgeThrougManLane()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("会车项目开始");
				itemStep++;
			}
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				itemStep++;
			}
			if (itemStep == 2 && (CarState.Instance.CarSignal.cluth > 0.3f || CarState.Instance.CarSignal.footBrake > 0.1f))
			{
				itemStep++;
			}
			if (itemStep < 4 && CourseThree.Instance.dirveLength - startLength > 250f)
			{
				if (itemStep == 2)
				{
					if (ViewJumpCtl.Instance.modelType != 0)
					{
						SoundPlayCtl.Instance.PlaySound("会车时,未按规定进行减速操作", 10f);
					}
				}
				else if (CarState.Instance.CurSpeed > 31f && ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("会车时,未将车速降低至30km/h以下", 10f);
				}
				itemStep = 4;
			}
			if (itemStep == 4 && CourseThree.Instance.dirveLength - startLength > 50f)
			{
				SoundPlayCtl.Instance.PlaySound("会车项目结束", 0f, 0, 2.5f);
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
