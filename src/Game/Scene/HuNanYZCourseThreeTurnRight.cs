using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class HuNanYZCourseThreeTurnRight : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeTurnRight();
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
			else if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > 200f)
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
			else if (itemStep == 2 && ColliderDetection.instance.GetCurRoadIndex() == -1)
			{
				itemStep++;
			}
			else if (itemStep == 3 && CourseThree.Instance.dirveLength - startLength > 350f && ColliderDetection.instance.GetCurRoadIndex() != -1)
			{
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "TurnRightNiXing1" && itemStep != 99)
			{
				SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
				itemStep = 99;
			}
		}
	}
}
