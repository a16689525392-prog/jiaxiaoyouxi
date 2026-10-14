using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class VipCourseThreeThroughMeetting : CourseThreeItemBase
	{
		private bool IsStat;

		private bool IsExceed30;

		private void Update()
		{
			UpdateJudgeThrougManLane();
		}

		private void UpdateJudgeThrougManLane()
		{
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方会车");
				itemStep++;
			}
			if (itemStep == 1 && CarState.Instance.CurSpeed > 1f)
			{
				itemStep++;
			}
			if (itemStep == 2 && CarState.Instance.CarSignal.footBrake > 0.1f)
			{
				itemStep++;
			}
			if (itemStep >= 4 || itemStep == 99 || !IsStat)
			{
				return;
			}
			if (itemStep == 2)
			{
				if (ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("会车时,未按规定进行减速操作", 10f);
				}
				itemStep = 4;
			}
			else if (CarState.Instance.CurSpeed > 30f)
			{
				if (ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("会车时,未将车速降低至30km/h以下", 10f);
				}
				itemStep = 4;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "MeettingItemStart" && itemStep != 99)
			{
				IsStat = true;
			}
			if (other.name == "MeettingItemOver" && itemStep != 99)
			{
				SoundPlayCtl.Instance.PlaySound("会车完成", 0f, 0, 2.5f);
				itemStep = 99;
			}
		}
	}
}
