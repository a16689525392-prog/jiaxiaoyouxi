using System;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class CourseThreeItemBase : MonoBehaviour
	{
		public int itemType;

		public int itemStep;

		public UnityAction ItemEndEvent;

		public float startLength;

		private float timeSum;

		private bool isStart;

		private bool isJudgeStopEnd;

		private bool isAiRoad;

		private RaycastHit courseThreeHitInfo;

		private bool isStopping;

		protected virtual void Start()
		{
			startLength = CourseThree.Instance.dirveLength;
			ColliderDetection instance = ColliderDetection.instance;
			instance.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Combine(instance.roadIndexChangeCallBack, new UnityAction<int, int>(RoadIndexChange));
		}

		public virtual void RoadIndexChange(int preindex, int curIndex)
		{
		}

		public virtual void TriggerEvent(Collider other)
		{
		}

		protected void ItemEnd()
		{
			if (ItemEndEvent != null)
			{
				ItemEndEvent();
			}
		}

		protected void BanStopping()
		{
			if (CarState.Instance.CurSpeed > 0.56f && !SceneMenuView.isReset && CarState.Instance.FireOn)
			{
				isStart = true;
			}
			if (isStart && !isJudgeStopEnd && itemStep != 99 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 213)
			{
				if (CarState.Instance.CurSpeed < 0.1f && ViewJumpCtl.Instance.modelType == 2)
				{
					timeSum += Time.deltaTime;
					if (timeSum > 2f)
					{
						SoundPlayCtl.Instance.PlaySound("中途停车", 5f);
						isJudgeStopEnd = true;
					}
				}
				else
				{
					timeSum = 0f;
				}
			}
			if (CarState.Instance.CurSpeed > 3f)
			{
				if (isJudgeStopEnd)
				{
					isJudgeStopEnd = false;
					timeSum = 0f;
				}
				SceneMenuView.isReset = false;
			}
			if (!isStopping && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out courseThreeHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out courseThreeHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out courseThreeHitInfo, 8)) && (courseThreeHitInfo.collider.name == "Wang0" || courseThreeHitInfo.collider.name == "Wang1" || courseThreeHitInfo.collider.name == "Wang2" || courseThreeHitInfo.collider.name == "Wang3" || courseThreeHitInfo.collider.name == "Wang4" || courseThreeHitInfo.collider.name == "Wang5"))
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed < 2f)
				{
					SoundPlayCtl.Instance.PlaySound("违法停车", 10f);
					isStopping = true;
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH < 2f)
				{
					SoundPlayCtl.Instance.PlaySound("违法停车", 10f);
					isStopping = true;
				}
			}
			if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed > 10f)
			{
				isStopping = false;
			}
			else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH > 10f)
			{
				isStopping = false;
			}
			if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out courseThreeHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out courseThreeHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out courseThreeHitInfo, 8))
			{
				if (courseThreeHitInfo.collider.name == "0" && !isAiRoad)
				{
					SoundPlayCtl.Instance.PlaySound("行驶中,占用应急车道", 100f);
					isAiRoad = true;
				}
				else if (courseThreeHitInfo.collider.name == "Road0")
				{
					isAiRoad = false;
				}
			}
		}

		protected virtual void OnDestroy()
		{
			ColliderDetection instance = ColliderDetection.instance;
			instance.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Remove(instance.roadIndexChangeCallBack, new UnityAction<int, int>(RoadIndexChange));
		}
	}
}
