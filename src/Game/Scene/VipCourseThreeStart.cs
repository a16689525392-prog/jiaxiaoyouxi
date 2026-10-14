using Car;
using SoundAndTextShow;
using View;

namespace Scene
{
	public class VipCourseThreeStart : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateStartJudge();
		}

		private void UpdateStartJudge()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				if (!CourseThree.Instance.IsCourseThreeKaoShi)
				{
					SoundPlayCtl.Instance.PlaySound("起步项目开始");
				}
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
			if (itemStep == 2 && CarState.Instance.CurSpeed > 0.5f && CourseThree.Instance.dirveLength > 100f)
			{
				if (!CarActionJudge.Instance.JudgeActionDone(12))
				{
					SoundPlayCtl.Instance.PlaySound("未关闭转向灯", 10f);
				}
				itemStep = 99;
			}
		}
	}
}
