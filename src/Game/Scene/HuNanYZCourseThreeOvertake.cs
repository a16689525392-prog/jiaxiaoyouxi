using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class HuNanYZCourseThreeOvertake : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeOvertake();
			Debug.Log(CourseThree.Instance.dirveLength - startLength);
		}

		public override void RoadIndexChange(int preIndex, int curIndex)
		{
			if (itemStep != 99)
			{
				if (itemStep == 2)
				{
					itemStep = 99;
					NetworkManagement.Instance.TrainingIsPass = true;
				}
				else
				{
					itemStep++;
				}
			}
		}

		private void UpdateJudgeOvertake()
		{
			if (CourseThree.Instance.dirveLength - startLength < 455f && itemStep != 99)
			{
				if (itemStep == 0)
				{
					SoundPlayCtl.Instance.PlaySound("请超越前方车辆");
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
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
