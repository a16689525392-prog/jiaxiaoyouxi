using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class HuNanYZCourseThreeTurnRound : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeTurnRound();
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
			else if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > 100f)
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
			else if (itemStep == 2 && (float)ColliderDetection.instance.GetCurRoadIndex() == 1f)
			{
				itemStep++;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "TurnRound" && itemStep == 3)
			{
				SoundPlayCtl.Instance.PlaySound("掉头项目结束", 0f, 0, 2.5f);
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
