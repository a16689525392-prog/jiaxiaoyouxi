using System;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class HaiJingSceneCtl : TrainSceneBase
	{
		public Light dirLight;

		public GameObject PaiZi;

		private float DriveTime;

		private bool isLightOpen;

		public int itemStep;

		private float triggertime;

		protected override void Start()
		{
			base.Start();
			if (ViewJumpCtl.Instance.otherType == 505)
			{
				base.gameObject.AddComponent<AiCarManager>();
				HaiJingSceneModel haiJingSceneModel = (HaiJingSceneModel)SceneModelBase.Instance;
				base.gameObject.GetComponent<AiCarManager>().Init(haiJingSceneModel.wayPointsGroup.transform);
				dirLight.gameObject.SetActive(value: false);
				RenderSettings.ambientLight = new Color(8f / 51f, 8f / 51f, 8f / 51f);
				PaiZi.SetActive(value: false);
			}
			ColliderDetection instance = ColliderDetection.instance;
			instance.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Combine(instance.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			base.ResetAction = (UnityAction)Delegate.Combine(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
		}

		private void FixedUpdate()
		{
			base.Update();
			if (ViewJumpCtl.Instance.otherType != 505)
			{
				return;
			}
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.FireOn && CarState.Instance.CurSpeed > 3f)
				{
					DriveTime += Time.deltaTime;
					if (DriveTime > 10f && !isLightOpen && CarState.Instance.CarSignal.frontLight == 0)
					{
						SoundPlayCtl.Instance.PlaySound("夜间未开启前照灯", 100f);
						isLightOpen = true;
					}
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && CarState.Instance.FireOn && DriveHistoryPlay.Instance.curCalculateSpeedH > 3f)
			{
				DriveTime += Time.deltaTime;
				if (DriveTime > 10f && !isLightOpen && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("夜间未开启前照灯", 100f);
					isLightOpen = true;
				}
			}
		}

		private void TrainItemReset()
		{
			itemStep = 0;
			DriveTime = 0f;
			isLightOpen = false;
		}

		private void ResetTimePlay()
		{
			itemStep = 0;
			DriveTime = 0f;
			isLightOpen = false;
		}

		protected override void Update()
		{
			triggertime += Time.deltaTime;
		}

		private void TriggerAreaEvent(Collider other)
		{
			if (triggertime > 1.5f)
			{
				if (other.name == "BanZhiXing")
				{
					SoundPlayCtl.Instance.PlaySound("违禁直行", 100f);
				}
				triggertime = 0f;
			}
			if (ViewJumpCtl.Instance.otherType != 505)
			{
				if (itemStep == 0 && other.name == "IfZuoZhuan")
				{
					itemStep = 1;
				}
				else if (itemStep == 1 && other.name == "BanZuoZhuan")
				{
					SoundPlayCtl.Instance.PlaySound("违禁左转弯", 100f);
					itemStep = 2;
				}
				else if (itemStep != 3 && other.name == "IfYouZhuan")
				{
					itemStep = 3;
				}
				else if (itemStep == 3 && other.name == "BanYouZhuan")
				{
					SoundPlayCtl.Instance.PlaySound("违禁右转弯", 100f);
					itemStep = 0;
				}
			}
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			ColliderDetection instance = ColliderDetection.instance;
			instance.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Remove(instance.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			SoundPlayCtl.Instance.ClearClip();
		}
	}
}
