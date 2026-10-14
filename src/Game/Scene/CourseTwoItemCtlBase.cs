using System;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class CourseTwoItemCtlBase : MonoBehaviour
	{
		public UnityAction ItemEndEvent;

		public int itemStep;

		private float timeSum;

		private bool isStart;

		private bool isJudgeStopEnd;

		private RaycastHit hitInfo;

		protected virtual void Start()
		{
			ColliderDetection instance = ColliderDetection.instance;
			instance.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Combine(instance.roadIndexChangeCallBack, new UnityAction<int, int>(RoadIndexChange));
		}

		public virtual void TriggerEvent(Collider other)
		{
		}

		protected void UpdateCarStop()
		{
			if (CarState.Instance.CurSpeed > 2f && !SceneMenuView.isReset && CarState.Instance.FireOn)
			{
				isStart = true;
			}
			if (isStart && !isJudgeStopEnd && itemStep != 99)
			{
				if (CarState.Instance.CurSpeed < 0.1f)
				{
					timeSum += Time.deltaTime;
					if (timeSum > 2f)
					{
						if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out hitInfo, 11))
						{
							if (!(hitInfo.collider.name == "stopcar"))
							{
								SoundPlayCtl.Instance.PlaySound("中途停车", 5f);
								isJudgeStopEnd = true;
							}
						}
						else
						{
							SoundPlayCtl.Instance.PlaySound("中途停车", 5f);
							isJudgeStopEnd = true;
						}
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
		}

		protected void ItemEnd()
		{
			if (ItemEndEvent != null)
			{
				ItemEndEvent();
			}
		}

		public virtual void RoadIndexChange(int preindex, int curIndex)
		{
		}

		protected virtual void OnDestroy()
		{
			ColliderDetection instance = ColliderDetection.instance;
			instance.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Remove(instance.roadIndexChangeCallBack, new UnityAction<int, int>(RoadIndexChange));
		}
	}
}
