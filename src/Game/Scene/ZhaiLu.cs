using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class ZhaiLu : CourseTwoItemCtlBase
	{
		private void Update()
		{
			UpdateZhuanWan();
		}

		private void UpdateZhuanWan()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f && !CarActionJudge.Instance.JudgeActionDone(114))
			{
				if (CarState.Instance.CarSignal.turnLight != 2)
				{
					_ = CarState.Instance.CarSignal.turnLight;
				}
				itemStep++;
			}
			else if (itemStep == 2)
			{
				if (CarActionJudge.Instance.JudgeActionDone(114))
				{
					itemStep++;
				}
			}
			else if (itemStep == 3)
			{
				if (!CarActionJudge.Instance.JudgeActionDone(114))
				{
					itemStep++;
				}
			}
			else if (itemStep == 4)
			{
				if (CarActionJudge.Instance.JudgeActionDone(114))
				{
					itemStep++;
				}
			}
			else if (itemStep == 5)
			{
				if (!CarActionJudge.Instance.JudgeActionDone(114))
				{
					itemStep++;
				}
			}
			else if (itemStep == 6 && CarActionJudge.Instance.JudgeActionDone(114))
			{
				SoundPlayCtl.Instance.PlaySound("三进两退未完成掉头", 100f);
				itemStep++;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && itemStep != 6 && other.name == "Start_ZhaiLu")
			{
				SoundPlayCtl.Instance.PlaySound("窄路掉头考试开始");
				itemStep = 1;
			}
			else if (other.name == "End_ZhaiLu")
			{
				if (itemStep == 3 || itemStep == 4 || itemStep == 5)
				{
					SoundPlayCtl.Instance.PlaySound("不按考试员指令驾驶", 100f);
				}
				else if (itemStep == 6)
				{
					_ = CarState.Instance.CarSignal.turnLight;
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("窄路掉头考试结束");
					itemStep = 99;
				}
			}
		}
	}
}
