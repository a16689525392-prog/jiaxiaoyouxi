using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class VipCourseThreeTurnLeft : CourseThreeItemBase
	{
		private int EnterErrorRoad;

		private bool PlayItemSound;

		private void Awake()
		{
			EnterErrorRoad = 0;
			PlayItemSound = true;
		}

		private void Update()
		{
			UpdateJudgeTurnLeft();
		}

		private void UpdateJudgeTurnLeft()
		{
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "TurnLeftItmeSound" && PlayItemSound)
			{
				SoundPlayCtl.Instance.PlaySound("前方路口左转弯");
				PlayItemSound = false;
			}
			if (other.name == "TurnLeftStart" && itemStep == 0)
			{
				if (ViewJumpCtl.Instance.modelType != 0)
				{
					CarActionJudge.Instance.JudgeTurnLight3s(109, 1);
					if (!CarActionJudge.Instance.JudgeActionDone(107))
					{
						SoundPlayCtl.Instance.PlaySound("转向前,未将车速降低至30km/h以下", 10f);
					}
				}
				itemStep++;
			}
			if (other.name == "TurnLeftRoadError1" && EnterErrorRoad == 0)
			{
				EnterErrorRoad++;
			}
			if (other.name == "TurnLeftRoadError2" && EnterErrorRoad == 1)
			{
				SoundPlayCtl.Instance.PlaySound("不按导向车道行驶", 100f);
				EnterErrorRoad++;
				itemStep = 99;
			}
			if (other.name == "TurnLeftNiXing1" && itemStep != 99)
			{
				SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
				itemStep = 99;
			}
		}
	}
}
