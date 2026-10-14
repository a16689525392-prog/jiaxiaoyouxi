using System;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;

namespace Scene
{
	public class TaiZhouChangTanSceneCtl : TrainSceneBase
	{
		public int itemStep;

		private RaycastHit shenZhenHitInfo;

		private bool isAidRoad;

		private bool isRightRoad;

		private bool isErrorRoad;

		protected override void Start()
		{
			base.Start();
			ColliderDetection instance = ColliderDetection.instance;
			instance.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Combine(instance.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			base.ResetAction = (UnityAction)Delegate.Combine(base.ResetAction, new UnityAction(TrainSceneReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			AiCarManager aiCarManager = base.gameObject.AddComponent<AiCarManager>();
			TaiZhouChangTanSceneModel taiZhouChangTanSceneModel = (TaiZhouChangTanSceneModel)SceneModelBase.Instance;
			aiCarManager.Init(taiZhouChangTanSceneModel.wayPointsGroup.transform);
		}

		private void FixedUpdate()
		{
		}

		private void TriggerAreaEvent(Collider other)
		{
		}

		private void ResetTimePlay()
		{
			isAidRoad = false;
			isRightRoad = false;
			isErrorRoad = false;
			itemStep = 0;
		}

		private void TrainSceneReset()
		{
			isAidRoad = false;
			isRightRoad = false;
			isErrorRoad = false;
			itemStep = 0;
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			ColliderDetection instance = ColliderDetection.instance;
			instance.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Remove(instance.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainSceneReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			SoundPlayCtl.Instance.ClearClip();
		}
	}
}
