using SoundAndTextShow;
using View;

namespace Scene
{
	public class CourseThreeTurnRight : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeTurnRight();
			BanStopping();
		}

		private void UpdateJudgeTurnRight()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("路口右转项目开始");
				itemStep++;
			}
			else if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > 300f)
			{
				if (ViewJumpCtl.Instance.modelType != 0)
				{
					CarActionJudge.Instance.JudgeTurnLight3s(110, 1);
					if (!CarActionJudge.Instance.JudgeActionDone(107))
					{
						SoundPlayCtl.Instance.PlaySound("转向前,未将车速降低至30km/h以下", 10f);
					}
				}
				itemStep++;
			}
			else if (itemStep == 2 && ColliderDetection.instance.GetCurRoadIndex() == -1 && CourseThree.Instance.dirveLength - startLength > 500f)
			{
				itemStep++;
			}
			else if (itemStep == 3 && ColliderDetection.instance.GetCurRoadIndex() != -1)
			{
				SoundPlayCtl.Instance.PlaySound("路口右转项目结束", 0f, 0, 2.5f);
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
