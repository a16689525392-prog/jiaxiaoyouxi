using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class HuNanYZCourseThreeStraightCrossroads : CourseThreeItemBase
	{
		private bool isdoFootbrakeHalf;

		private void Update()
		{
			UpdateJudgeStraightCrossroads();
			Debug.Log(CourseThree.Instance.dirveLength - startLength);
		}

		private void UpdateJudgeStraightCrossroads()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方路口请直行");
				itemStep++;
			}
			else if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > 140f && CourseThree.Instance.dirveLength - startLength < 210f)
			{
				if (CarActionJudge.Instance.JudgeActionDone(22))
				{
					isdoFootbrakeHalf = true;
				}
			}
			else if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > 270f)
			{
				if (!isdoFootbrakeHalf)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行减速操作", 10f);
				}
				itemStep++;
			}
			else if (itemStep == 2 && ColliderDetection.instance.GetCurRoadIndex() == -1)
			{
				itemStep++;
			}
			else if (itemStep == 3 && CourseThree.Instance.dirveLength - startLength > 116f && (ColliderDetection.instance.GetCurRoadIndex() == 0 || ColliderDetection.instance.GetCurRoadIndex() == 1))
			{
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
