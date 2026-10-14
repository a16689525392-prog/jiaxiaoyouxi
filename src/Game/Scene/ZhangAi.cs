using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class ZhangAi : CourseTwoItemCtlBase
	{
		private void Update()
		{
			UpdateZhangAi();
			UpdateCarStop();
		}

		private void UpdateZhangAi()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				if (CarState.Instance.CarSignal.turnLight == 2)
				{
					SoundPlayCtl.Instance.PlaySound("未正确开启转向灯", 10f);
				}
				else if (CarState.Instance.CarSignal.turnLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("未开启左转向灯", 10f);
				}
				itemStep++;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "Start_ZhangAi" && itemStep != 1)
			{
				SoundPlayCtl.Instance.PlaySound("通过连续障碍考试开始");
				itemStep = 1;
			}
			else if (other.name == "Bing1" && itemStep == 2)
			{
				itemStep = 3;
			}
			else if (other.name == "Bing2" && itemStep == 3)
			{
				itemStep = 4;
			}
			else if (other.name == "Bing3" && itemStep == 4)
			{
				itemStep = 5;
			}
			else if (other.name == "Bing4" && itemStep == 5)
			{
				itemStep = 6;
			}
			else if (other.name == "Bing5" && itemStep == 6)
			{
				itemStep = 7;
			}
			else if (other.name == "Bing6" && itemStep == 7)
			{
				itemStep = 8;
			}
			else if (other.name == "End_ZhangAi" && itemStep != 99)
			{
				if (CarState.Instance.CarSignal.turnLight != 0)
				{
					SoundPlayCtl.Instance.PlaySound("未及时关闭转向灯", 10f);
				}
				if (itemStep < 8)
				{
					SoundPlayCtl.Instance.PlaySound("不按规定路线顺序行驶", 100f);
				}
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("通过连续障碍考试结束");
				itemStep = 99;
			}
		}
	}
}
