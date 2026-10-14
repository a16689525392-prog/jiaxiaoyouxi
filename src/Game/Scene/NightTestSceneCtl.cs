using System;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class NightTestSceneCtl : TrainSceneBase
	{
		public static NightTestSceneCtl instance;

		private float DriveTime;

		private bool isLightOpen;

		public int itemStep;

		public GameObject NightTestAICar;

		private float triggertime;

		private bool JudgeFarAndNearAction;

		private bool NightTestAICarMove;

		public GameObject InstanceNightTestAICar;

		public GameObject InstanceElectricCarandMan;

		public bool isGenCheFalse;

		public bool isElectricCarHuiCheFalse;

		public float carfaulttime;

		protected override void Start()
		{
			base.Start();
			instance = this;
			if (ViewJumpCtl.Instance.otherType == 505)
			{
				base.gameObject.AddComponent<AiCarManager>();
				NightTestSceneModel nightTestSceneModel = (NightTestSceneModel)SceneModelBase.Instance;
				base.gameObject.GetComponent<AiCarManager>().Init(nightTestSceneModel.wayPointsGroup.transform);
			}
			ColliderDetection colliderDetection = ColliderDetection.instance;
			colliderDetection.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Combine(colliderDetection.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
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
			if (CarState.Instance.CarSignal.frontLight == 2 || NightTestAICarMove)
			{
				DriveTime = 0f;
			}
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.FireOn && CarState.Instance.CurSpeed > 3f)
				{
					DriveTime += Time.deltaTime;
					if (DriveTime > 6f && !isLightOpen && CarState.Instance.CarSignal.frontLight != 2)
					{
						SoundPlayCtl.Instance.PlaySound("夜间在没有路灯，照明不良条件下行驶，未开启远光灯", 20f);
						isLightOpen = true;
					}
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && CarState.Instance.FireOn && DriveHistoryPlay.Instance.curCalculateSpeedH > 3f)
			{
				DriveTime += Time.deltaTime;
				if (DriveTime > 6f && !isLightOpen && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight != 2)
				{
					SoundPlayCtl.Instance.PlaySound("夜间在没有路灯，照明不良条件下行驶，未开启远光灯", 20f);
					isLightOpen = true;
				}
			}
		}

		private void TrainItemReset()
		{
			itemStep = 0;
			DriveTime = 0f;
			isLightOpen = false;
			JudgeFarAndNearAction = false;
			NightTestAICarMove = false;
			if (InstanceNightTestAICar != null)
			{
				UnityEngine.Object.Destroy(InstanceNightTestAICar);
			}
			if (InstanceElectricCarandMan != null)
			{
				UnityEngine.Object.Destroy(InstanceElectricCarandMan);
			}
			NightTestAICar.SetActive(value: true);
			isGenCheFalse = false;
			isElectricCarHuiCheFalse = false;
		}

		private void ResetTimePlay()
		{
			itemStep = 0;
			DriveTime = 0f;
			isLightOpen = false;
		}

		protected override void Update()
		{
			if (JudgeFarAndNearAction)
			{
				triggertime += Time.deltaTime;
				if (triggertime > 3f)
				{
					switch (CarActionJudge.Instance.GetFarAndNearActionDone())
					{
					case 0:
						SoundPlayCtl.Instance.PlaySound("未交替使用远近光灯", 10f);
						break;
					}
					JudgeFarAndNearAction = false;
				}
			}
			if (NightTestAICarMove && InstanceNightTestAICar != null)
			{
				if (InstanceNightTestAICar.transform.localPosition.y < 203f)
				{
					InstanceNightTestAICar.transform.localPosition += new Vector3(0f, 0.25f, 0f);
				}
				else
				{
					NightTestAICarMove = false;
				}
			}
			if (itemStep != 4)
			{
				return;
			}
			carfaulttime += Time.deltaTime;
			if (carfaulttime > 5f)
			{
				if (CarState.Instance.CarSignal.turnLight != 3 || CarState.Instance.CarSignal.widthLight != 1)
				{
					SoundPlayCtl.Instance.PlaySound("道路上发生故障妨碍交通难移动时,不开启示宽灯和危险报警灯", 10f);
				}
				itemStep = 99;
			}
		}

		private void TriggerAreaEvent(Collider other)
		{
			if (other.name == "JudgeFarAndNearActionDone" && !JudgeFarAndNearAction)
			{
				JudgeFarAndNearAction = true;
				triggertime = 0f;
				CarActionJudge.Instance.JudgeActionDone(6);
			}
			if (itemStep == 0 && other.name == "NightTestAICarMove")
			{
				InstanceNightTestAICar = UnityEngine.Object.Instantiate(Resources.Load("AppearPerson/NightTestAICar")) as GameObject;
				GameObject gameObject = GameObject.Find("NightTestAICarStartPosition");
				InstanceNightTestAICar.transform.parent = gameObject.transform;
				InstanceNightTestAICar.transform.localPosition = new Vector3(0f, 0f, 0.7f);
				InstanceNightTestAICar.transform.localEulerAngles = new Vector3(-90f, 180f, 0f);
				InstanceNightTestAICar.transform.localScale = new Vector3(2f, 2f, 2f);
				NightTestAICar.SetActive(value: false);
				NightTestAICarMove = true;
				itemStep++;
			}
			if (itemStep == 1 && other.name == "JudgeFarAndNearActionDone")
			{
				NightTestAICarMove = false;
				itemStep++;
			}
			if (itemStep == 2 && other.name == "NightTestElectricCarandManMove")
			{
				InstanceElectricCarandMan = UnityEngine.Object.Instantiate(Resources.Load("AppearPerson/ElectricCarandMan")) as GameObject;
				GameObject gameObject2 = GameObject.Find("NightTestElectricCarandManPosition");
				InstanceElectricCarandMan.transform.parent = gameObject2.transform;
				InstanceElectricCarandMan.transform.localPosition = new Vector3(0.2f, 0.14f, 1.1f);
				InstanceElectricCarandMan.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
				InstanceElectricCarandMan.transform.localScale = new Vector3(2f, 2f, 2f);
				itemStep++;
			}
			if (itemStep == 3 && other.name == "CarFault")
			{
				if (CarState.Instance.FireOn)
				{
					CarState.Instance.FireOn = false;
				}
				CarState.Instance.carRigibody.velocity = Vector3.zero;
				SoundPlayCtl.Instance.PlaySound("车辆故障");
				carfaulttime = 0f;
				itemStep++;
			}
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			ColliderDetection colliderDetection = ColliderDetection.instance;
			colliderDetection.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Remove(colliderDetection.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			SoundPlayCtl.Instance.ClearClip();
		}
	}
}
