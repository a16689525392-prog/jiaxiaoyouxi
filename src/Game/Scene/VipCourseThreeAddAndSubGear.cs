using Car;
using Global;
using SoundAndTextShow;

namespace Scene
{
	public class VipCourseThreeAddAndSubGear : CourseThreeItemBase
	{
		private void Update()
		{
			if (SettingInfoCtl.Instance.isManualGear)
			{
				UpdateJudgeAddAndSubGear();
			}
		}

		private void UpdateJudgeAddAndSubGear()
		{
			if (itemStep == 0)
			{
				itemStep++;
			}
			if (itemStep == 1 && CarState.Instance.CarSignal.gear == 4)
			{
				itemStep++;
			}
			if (itemStep == 2 && CarActionJudge.Instance.JudgeActionDone(1080))
			{
				itemStep++;
			}
			if (itemStep == 3 && CarState.Instance.CarSignal.gear == 3)
			{
				itemStep++;
			}
			if (CourseThree.Instance.dirveLength - startLength > 1200f && itemStep < 4)
			{
				if (itemStep == 3 && CarState.Instance.CarSignal.gear == 4)
				{
					SoundPlayCtl.Instance.PlaySound("未进行减挡操作", 100f);
				}
				else if (itemStep == 2)
				{
					SoundPlayCtl.Instance.PlaySound("未保持车速在40km/h以上3秒", 100f);
				}
				else if (itemStep == 1)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行加减挡操作", 100f);
				}
				itemStep = 99;
			}
			if (itemStep == 4)
			{
				itemStep = 99;
			}
		}
	}
}
