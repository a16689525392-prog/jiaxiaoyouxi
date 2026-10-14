using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scene
{
	public class HighRoadSceneBase : MonoBehaviour
	{
		public UnityAction ItemEndEvent;

		public int itemStep;

		protected virtual void Start()
		{
			ColliderDetection instance = ColliderDetection.instance;
			instance.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Combine(instance.roadIndexChangeCallBack, new UnityAction<int, int>(RoadIndexChange));
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
