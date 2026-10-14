using SoundAndTextShow;

namespace Scene
{
	public class VipCourseThreeStraightCrossroads : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeStraightCrossroads();
		}

		private void UpdateJudgeStraightCrossroads()
		{
			if (itemStep != 99 && itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方直行通过路口");
				itemStep++;
			}
		}
	}
}
