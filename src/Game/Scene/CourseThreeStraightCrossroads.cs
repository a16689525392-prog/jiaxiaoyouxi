using SoundAndTextShow;

namespace Scene
{
	public class CourseThreeStraightCrossroads : CourseThreeItemBase
	{
		private bool isdoFootbrakeHalf;

		private void Update()
		{
			UpdateJudgeStraightCrossroads();
		}

		private void UpdateJudgeStraightCrossroads()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("路口直行项目开始");
				itemStep++;
			}
			else if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > 36f && CourseThree.Instance.dirveLength - startLength < 66f)
			{
				if (CarActionJudge.Instance.JudgeActionDone(22))
				{
					isdoFootbrakeHalf = true;
				}
			}
			else if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > 66f)
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
			else if (itemStep == 3 && CourseThree.Instance.dirveLength - startLength > 116f)
			{
				if (ColliderDetection.instance.GetCurRoadIndex() == 0)
				{
					SoundPlayCtl.Instance.PlaySound("路口直行项目结束", 0f, 0, 2.5f);
					itemStep = 99;
					NetworkManagement.Instance.TrainingIsPass = true;
				}
				if (ColliderDetection.instance.GetCurRoadIndex() == 1)
				{
					SoundPlayCtl.Instance.PlaySound("未靠右行驶", 10f);
					itemStep++;
				}
			}
			else if (itemStep == 4)
			{
				SoundPlayCtl.Instance.PlaySound("路口左转项目结束", 0f, 0, 2.5f);
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
