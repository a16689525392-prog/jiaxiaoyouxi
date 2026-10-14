using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class HuNanYZCourseThreeTurnLeft : CourseThreeItemBase
	{
		private bool isdoFootbrakeHalf;

		private float L1;

		private float L2;

		private float L3;

		private float L4;

		private int EnterErrorRoad;

		private void Awake()
		{
			if (CourseThree.Instance.ItemStartChild0 == 10)
			{
				L1 = 365f;
				L2 = 400f;
				L3 = 405f;
				L4 = 540f;
			}
			else
			{
				L1 = 60f;
				L2 = 130f;
				L3 = 135f;
				L4 = 270f;
			}
			EnterErrorRoad = 0;
		}

		private void Update()
		{
			UpdateJudgeTurnRight();
			Debug.Log(CourseThree.Instance.dirveLength - startLength);
		}

		private void UpdateJudgeTurnRight()
		{
			if (itemStep == 99)
			{
				return;
			}
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方路口请左转");
				itemStep++;
			}
			else if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > L1 && CourseThree.Instance.dirveLength - startLength < L2)
			{
				if (CarActionJudge.Instance.JudgeActionDone(22))
				{
					isdoFootbrakeHalf = true;
				}
			}
			else if (itemStep == 1 && CourseThree.Instance.dirveLength - startLength > L2)
			{
				if (ViewJumpCtl.Instance.modelType != 0)
				{
					CarActionJudge.Instance.JudgeTurnLight3s(109, 1);
				}
				if (!isdoFootbrakeHalf)
				{
					SoundPlayCtl.Instance.PlaySound("转向前,未踩刹车", 10f);
				}
				itemStep++;
			}
			else if (itemStep == 2 && ColliderDetection.instance.GetCurRoadIndex() == -1 && CourseThree.Instance.dirveLength - startLength > L3)
			{
				itemStep++;
			}
			else if (itemStep == 3 && CourseThree.Instance.dirveLength - startLength > L4)
			{
				if (ColliderDetection.instance.GetCurRoadIndex() == 0)
				{
					itemStep = 99;
				}
				if (ColliderDetection.instance.GetCurRoadIndex() == 1)
				{
					itemStep++;
				}
			}
			else if (itemStep == 4 && ColliderDetection.instance.GetCurRoadIndex() == 0)
			{
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "TurnLeftNiXing1" && itemStep != 99)
			{
				SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
				itemStep = 99;
			}
			if (other.name == "TurnLeftRoadError1" && EnterErrorRoad == 0)
			{
				EnterErrorRoad++;
			}
			if (other.name == "TurnLeftRoadError2" && EnterErrorRoad == 1)
			{
				SoundPlayCtl.Instance.PlaySound("在右转车道左转", 100f);
				EnterErrorRoad++;
			}
		}
	}
}
