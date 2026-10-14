using SoundAndTextShow;

namespace Scene
{
	public class CourseThreeChangeLane : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateChangeLane();
			BanStopping();
		}

		private void UpdateChangeLane()
		{
			if (CourseThree.Instance.dirveLength - startLength < 350f)
			{
				if (itemStep == 0)
				{
					SoundPlayCtl.Instance.PlaySound("变更车道项目开始");
					itemStep++;
				}
			}
			else if (itemStep != 99)
			{
				if (itemStep == 1)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行变更车道操作", 100f);
				}
				SoundPlayCtl.Instance.PlaySound("变更车道项目结束", 0f, 0, 2.5f);
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
