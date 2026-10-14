using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class VipCourseThreeTurnRound : CourseThreeItemBase
	{
		private int EnterErrorRoad;

		private void Update()
		{
			UpdateJudgeTurnRound();
		}

		private void UpdateJudgeTurnRound()
		{
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "TurnRoundStart" && itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方掉头");
				EnterErrorRoad = 0;
				itemStep++;
			}
			if (other.name == "TurnRoundStart_NoSound" && itemStep == 0)
			{
				itemStep++;
			}
			if (other.name == "TurnRoundRoadError1" && EnterErrorRoad == 0)
			{
				EnterErrorRoad++;
			}
			if (other.name == "TurnRoundRoadError2" && EnterErrorRoad == 1)
			{
				SoundPlayCtl.Instance.PlaySound("不按导向车道行驶", 100f);
				EnterErrorRoad++;
				itemStep = 99;
			}
			if (!(other.name == "TurnRoundJudge") || itemStep != 1)
			{
				return;
			}
			if (ViewJumpCtl.Instance.modelType != 0)
			{
				CarActionJudge.Instance.JudgeTurnLight3s(109, 3);
				if (!CarActionJudge.Instance.JudgeActionDone(107))
				{
					SoundPlayCtl.Instance.PlaySound("掉头前,未将车速降低至30km/h以下", 10f);
				}
			}
			itemStep++;
		}
	}
}
