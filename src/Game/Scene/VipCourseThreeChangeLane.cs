using SoundAndTextShow;

namespace Scene
{
	public class VipCourseThreeChangeLane : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateChangeLane();
		}

		private void UpdateChangeLane()
		{
			if (CourseThree.Instance.dirveLength - startLength < 450f)
			{
				if (itemStep == 0)
				{
					SoundPlayCtl.Instance.PlaySound("前方变更车道");
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
