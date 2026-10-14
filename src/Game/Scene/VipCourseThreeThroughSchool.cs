using SoundAndTextShow;
using View;

namespace Scene
{
	public class VipCourseThreeThroughSchool : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeThroughSchool();
		}

		private void UpdateJudgeThroughSchool()
		{
			if (itemStep == 0)
			{
				itemStep++;
			}
			if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > 90f)
			{
				if (!CarActionJudge.Instance.JudgeActionDone(107) && ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("未将车速降低至30km/h以下", 10f);
				}
				itemStep++;
			}
			if (itemStep == 2 && CourseThree.Instance.dirveLength - startLength > 100f)
			{
				itemStep = 99;
			}
		}
	}
}
