using Car;
using SoundAndTextShow;

namespace Scene
{
	public class CourseThreeAddAndSubGear : CourseThreeItemBase
	{
		private void Update()
		{
			UpdateJudgeAddAndSubGear();
			BanStopping();
		}

		private void UpdateJudgeAddAndSubGear()
		{
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("加减挡项目开始");
				itemStep++;
			}
			if (itemStep == 1 && CarState.Instance.CarSignal.gear == 4)
			{
				itemStep++;
			}
			if (itemStep == 2 && CarState.Instance.CarSignal.gear == 3)
			{
				itemStep++;
			}
			if (CourseThree.Instance.dirveLength - startLength > 600f && itemStep < 3)
			{
				if (itemStep == 2 && CarState.Instance.CarSignal.gear == 4)
				{
					SoundPlayCtl.Instance.PlaySound("未进行减挡操作", 100f);
				}
				else if (itemStep == 1)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行加减挡操作", 100f);
				}
				SoundPlayCtl.Instance.PlaySound("加减挡项目结束", 0f, 0, 2.5f);
				itemStep = 99;
			}
			if (itemStep == 3)
			{
				SoundPlayCtl.Instance.PlaySound("加减挡项目结束", 0f, 0, 2.5f);
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
