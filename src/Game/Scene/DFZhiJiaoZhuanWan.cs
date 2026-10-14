using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class DFZhiJiaoZhuanWan : CourseTwoItemCtlBase
	{
		private void Update()
		{
			UpdateCarStop();
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			switch (itemStep)
			{
			case 0:
				if (other.name == "Start_ZhiJiao")
				{
					SoundPlayCtl.Instance.PlaySound("直角转弯项目开始");
					itemStep++;
				}
				break;
			case 1:
				if (other.name == "OpenLight")
				{
					if (CarState.Instance.CarSignal.turnLight == 2)
					{
						SoundPlayCtl.Instance.PlaySound("未正确开启转向灯", 100f);
					}
					else if (CarState.Instance.CarSignal.turnLight == 0)
					{
						SoundPlayCtl.Instance.PlaySound("未开启左转向灯", 100f);
					}
					itemStep++;
				}
				break;
			case 2:
				if (other.name == "End_ZhiJiao")
				{
					if (CarState.Instance.CarSignal.turnLight != 0)
					{
						SoundPlayCtl.Instance.PlaySound("转向结束后未及时关闭转向灯", 100f);
					}
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("直角转弯项目结束");
					itemStep = 99;
				}
				break;
			}
		}
	}
}
