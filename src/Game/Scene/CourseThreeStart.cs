using Car;
using SoundAndTextShow;
using View;

namespace Scene
{
	public class CourseThreeStart : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateStartJudge();
			BanStopping();
		}

		private void UpdateStartJudge()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("起步项目开始");
				itemStep++;
			}
			if (itemStep == 1 && CarState.Instance.CurSpeed > 0.1f && CourseThree.Instance.dirveLength > 10f)
			{
				if (ViewJumpCtl.Instance.modelType != 0)
				{
					CarActionJudge.Instance.JudgeTurnLight3s(109, 4);
				}
				itemStep = 2;
			}
			if (itemStep == 2 && CarState.Instance.CurSpeed > 0.5f && CourseThree.Instance.dirveLength > 200f)
			{
				if (CarState.Instance.CarSignal.gear < 3)
				{
					SoundPlayCtl.Instance.PlaySound("未将档位加至三挡", 100f);
				}
				SoundPlayCtl.Instance.PlaySound("起步项目结束", 0f, 0, 2.5f);
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
