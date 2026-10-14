using SoundAndTextShow;
using View;

namespace Scene
{
	public class CourseThreeOvertake : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeOvertake();
			BanStopping();
		}

		public override void RoadIndexChange(int preIndex, int curIndex)
		{
			if (itemStep != 99)
			{
				if (itemStep == 2)
				{
					itemStep = 99;
					SoundPlayCtl.Instance.PlaySound("超车项目结束");
				}
				else
				{
					itemStep++;
				}
			}
		}

		private void UpdateJudgeOvertake()
		{
			if (CourseThree.Instance.dirveLength - startLength < 600f && itemStep != 99)
			{
				if (itemStep == 0)
				{
					SoundPlayCtl.Instance.PlaySound("超车项目开始");
					itemStep++;
				}
			}
			else
			{
				if (itemStep == 99)
				{
					return;
				}
				if (itemStep == 1)
				{
					if (ViewJumpCtl.Instance.modelType != 0)
					{
						SoundPlayCtl.Instance.PlaySound("未按规定进行超车操作", 100f);
					}
				}
				else if (itemStep == 2 && ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("超车后,未变更回原车道", 100f);
				}
				SoundPlayCtl.Instance.PlaySound("超车项目结束", 0f, 0, 2.5f);
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
