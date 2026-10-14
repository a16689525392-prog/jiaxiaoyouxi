using SoundAndTextShow;
using View;

namespace Scene
{
	public class CourseThreeTurnRound : CourseThreeItemBase
	{
		private bool isJudgeSteerwheel;

		private void Update()
		{
			UpdateJudgeTurnRound();
			BanStopping();
		}

		private void UpdateJudgeTurnRound()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("掉头项目开始");
				itemStep++;
			}
			else if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > 300f)
			{
				if (ViewJumpCtl.Instance.modelType != 0)
				{
					CarActionJudge.Instance.JudgeTurnLight3s(109, 3);
					if (!CarActionJudge.Instance.JudgeActionDone(107))
					{
						SoundPlayCtl.Instance.PlaySound("掉头前,未将车速降低至30km/h以下", 10f);
					}
				}
				itemStep++;
			}
			else if (itemStep == 2 && (float)ColliderDetection.instance.GetCurRoadIndex() == -1f)
			{
				itemStep++;
			}
			else if (itemStep == 3 && ColliderDetection.instance.GetCurRoadIndex() != -1)
			{
				SoundPlayCtl.Instance.PlaySound("掉头项目结束", 0f, 0, 2.5f);
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}

		public override void RoadIndexChange(int preIndex, int curIndex)
		{
			if (itemStep != 0 && itemStep != 99 && !isJudgeSteerwheel)
			{
				if (ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("方向控制不稳,不能保持车辆直线行驶", 10f);
				}
				itemStep++;
				isJudgeSteerwheel = true;
			}
			base.RoadIndexChange(preIndex, curIndex);
		}
	}
}
