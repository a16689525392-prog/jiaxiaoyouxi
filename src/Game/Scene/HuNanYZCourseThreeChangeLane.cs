using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class HuNanYZCourseThreeChangeLane : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateChangeLane();
			Debug.Log(CourseThree.Instance.dirveLength - startLength);
		}

		private void UpdateChangeLane()
		{
			if (CourseThree.Instance.dirveLength - startLength < 320f)
			{
				if (itemStep == 0)
				{
					SoundPlayCtl.Instance.PlaySound("请变更车道");
					itemStep++;
				}
			}
			else if (itemStep != 99)
			{
				if (itemStep == 1)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行变更车道操作", 100f);
				}
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}

		public override void RoadIndexChange(int preindex, int curindex)
		{
			if (itemStep == 1)
			{
				itemStep = 2;
			}
		}
	}
}
