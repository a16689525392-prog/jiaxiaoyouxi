using Car;
using SoundAndTextShow;
using View;

namespace Scene
{
	public class VipCourseThreeThroughBusStation : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeThrougBusStation();
		}

		private void UpdateJudgeThrougBusStation()
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
			if (itemStep == 2 && CarState.Instance.CarSignal.footBrake > 0.1f)
			{
				itemStep++;
			}
			if (itemStep < 4 && CourseThree.Instance.dirveLength - startLength > 270f)
			{
				if (itemStep == 2)
				{
					if (ViewJumpCtl.Instance.modelType != 0)
					{
						SoundPlayCtl.Instance.PlaySound("未按规定进行减速操作", 10f);
					}
				}
				else if (!CarActionJudge.Instance.JudgeActionDone(107))
				{
					_ = ViewJumpCtl.Instance.modelType;
				}
				itemStep = 4;
			}
			if (itemStep == 4 && CourseThree.Instance.dirveLength - startLength > 220f)
			{
				itemStep = 99;
			}
		}
	}
}
