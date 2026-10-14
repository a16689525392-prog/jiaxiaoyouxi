using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class VipCourseThreeOvertake : CourseThreeItemBase
	{
		private bool IsChangeOne;

		private bool IsOvertakeItemOver;

		private void Awake()
		{
			IsChangeOne = false;
			IsOvertakeItemOver = false;
		}

		private void Update()
		{
			UpdateJudgeOvertake();
		}

		public override void RoadIndexChange(int preIndex, int curIndex)
		{
			if (itemStep != 99)
			{
				if (itemStep == 2)
				{
					itemStep = 99;
				}
				else
				{
					itemStep++;
				}
			}
		}

		private void UpdateJudgeOvertake()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方超车");
				itemStep++;
			}
			if (!IsOvertakeItemOver)
			{
				return;
			}
			if (IsChangeOne)
			{
				if (itemStep == 1 && ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行超车操作", 100f);
				}
			}
			else if (itemStep == 1)
			{
				if (ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行超车操作", 100f);
				}
			}
			else if (itemStep == 2 && ViewJumpCtl.Instance.modelType != 0)
			{
				SoundPlayCtl.Instance.PlaySound("超车后,未变更回原车道", 100f);
			}
			itemStep = 99;
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "Overtake_ChangeOne")
			{
				IsChangeOne = true;
			}
			if (other.name == "OvertakeItemOver")
			{
				IsOvertakeItemOver = true;
			}
		}
	}
}
