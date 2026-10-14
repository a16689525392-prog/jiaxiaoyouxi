using System;
using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class BadWeatherTestSceneCtl : TrainSceneBase
	{
		public GameObject InstanceRain;

		public int tempItemType;

		private bool isYuGuaOpen;

		private float RainTime;

		private float JianGeTime = 10f;

		public GameObject BadWeatherTestAICar1;

		public GameObject BadWeatherTestAICar2;

		private GameObject InstanceBadWeatherTestAICar1;

		private GameObject InstanceBadWeatherTestAICar2;

		public int itemStep;

		protected override void Start()
		{
			base.Start();
			InstanceRain = UnityEngine.Object.Instantiate(Resources.Load("RainGlass")) as GameObject;
			ColliderDetection instance = ColliderDetection.instance;
			instance.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Combine(instance.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			ColliderDetection instance2 = ColliderDetection.instance;
			instance2.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Combine(instance2.roadIndexChangeCallBack, new UnityAction<int, int>(JudgeTurnLight));
			tempItemType = -1;
			base.ResetAction = (UnityAction)Delegate.Combine(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			if (SettingInfoCtl.Instance.carType == CarType.JieDaManual || SettingInfoCtl.Instance.carType == CarType.JieDaAuto)
			{
				GameObject gameObject = GameObject.Find("JieDa(Clone)");
				InstanceRain.transform.parent = gameObject.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
			{
				GameObject gameObject2 = GameObject.Find("DongFengDuoLiKa(Clone)");
				InstanceRain.transform.parent = gameObject2.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
			{
				GameObject gameObject3 = GameObject.Find("dabache(Clone)");
				InstanceRain.transform.parent = gameObject3.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.YongShi)
			{
				GameObject gameObject4 = GameObject.Find("YongShi(Clone)");
				InstanceRain.transform.parent = gameObject4.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1121)
			{
				GameObject gameObject5 = GameObject.Find("JieFang1121(Clone)");
				InstanceRain.transform.parent = gameObject5.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QiRuiE3)
			{
				GameObject gameObject6 = GameObject.Find("QiRuiE3(Clone)");
				InstanceRain.transform.parent = gameObject6.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.KaLuoLa)
			{
				GameObject gameObject7 = GameObject.Find("KaLuoLa(Clone)");
				InstanceRain.transform.parent = gameObject7.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.TianJin)
			{
				GameObject gameObject8 = GameObject.Find("TianJin(Clone)");
				InstanceRain.transform.parent = gameObject8.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1125)
			{
				GameObject gameObject9 = GameObject.Find("JieFang1125(Clone)");
				InstanceRain.transform.parent = gameObject9.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
			{
				GameObject gameObject10 = GameObject.Find("DFKangMingSi(Clone)");
				InstanceRain.transform.parent = gameObject10.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.PiKa)
			{
				GameObject gameObject11 = GameObject.Find("PiKa(Clone)");
				InstanceRain.transform.parent = gameObject11.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DFMengShi)
			{
				GameObject gameObject12 = GameObject.Find("DFMengShi(Clone)");
				InstanceRain.transform.parent = gameObject12.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.AiLiSheManual || SettingInfoCtl.Instance.carType == CarType.AiLiSheAuto)
			{
				GameObject gameObject13 = GameObject.Find("AiLiShe(Clone)");
				InstanceRain.transform.parent = gameObject13.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QianYinChe)
			{
				GameObject gameObject14 = GameObject.Find("QianYinChe(Clone)");
				InstanceRain.transform.parent = gameObject14.transform;
				InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
				InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
				InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}

		protected override void Awake()
		{
			base.Awake();
		}

		private void TrainItemReset()
		{
			itemStep = 0;
			isYuGuaOpen = false;
			RainTime = 0f;
			JianGeTime = 10f;
			if (InstanceBadWeatherTestAICar1 != null)
			{
				UnityEngine.Object.Destroy(InstanceBadWeatherTestAICar1);
			}
			if (InstanceBadWeatherTestAICar2 != null)
			{
				UnityEngine.Object.Destroy(InstanceBadWeatherTestAICar2);
			}
			BadWeatherTestAICar1.SetActive(value: true);
			BadWeatherTestAICar2.SetActive(value: true);
		}

		private void ResetTimePlay()
		{
			itemStep = 0;
			isYuGuaOpen = false;
			RainTime = 0f;
			JianGeTime = 10f;
			if (InstanceBadWeatherTestAICar1 != null)
			{
				UnityEngine.Object.Destroy(InstanceBadWeatherTestAICar1);
			}
			if (InstanceBadWeatherTestAICar2 != null)
			{
				UnityEngine.Object.Destroy(InstanceBadWeatherTestAICar2);
			}
			BadWeatherTestAICar1.SetActive(value: true);
			BadWeatherTestAICar2.SetActive(value: true);
		}

		protected override void Update()
		{
			base.Update();
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.FireOn && CarState.Instance.CurSpeed > 3f)
				{
					RainTime += Time.deltaTime;
					if (RainTime > 10f && !isYuGuaOpen)
					{
						if (CarState.Instance.CarSignal.rainBrush == 0)
						{
							SoundPlayCtl.Instance.PlaySound("雨天未开启雨刮器", 10f);
						}
						if (CarState.Instance.CarSignal.fogLight == 0 || CarState.Instance.CarSignal.frontLight != 1 || CarState.Instance.CarSignal.widthLight != 1)
						{
							SoundPlayCtl.Instance.PlaySound("恶劣天气驾驶机动车不按规定使用灯光", 15f);
						}
						isYuGuaOpen = true;
					}
				}
				JianGeTime += Time.deltaTime;
				if (JianGeTime > 10f && CarState.Instance.CurSpeed > 30f)
				{
					SoundPlayCtl.Instance.PlaySound("恶劣天气未减速慢行", 20f);
					JianGeTime = 0f;
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 5)
			{
				if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && CarState.Instance.FireOn && DriveHistoryPlay.Instance.curCalculateSpeedH > 3f)
				{
					RainTime += Time.deltaTime;
					if (RainTime > 10f && !isYuGuaOpen && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush == 0)
					{
						SoundPlayCtl.Instance.PlaySound("雨天未开启雨刮器", 10f);
						isYuGuaOpen = true;
					}
				}
				JianGeTime += Time.deltaTime;
				if (JianGeTime > 10f && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.curCalculateSpeedH > 30f)
				{
					SoundPlayCtl.Instance.PlaySound("恶劣天气未减速慢行", 20f);
					JianGeTime = 0f;
				}
			}
			if (itemStep > 0 && InstanceBadWeatherTestAICar1 != null && InstanceBadWeatherTestAICar1.transform.localPosition.y > -123f)
			{
				InstanceBadWeatherTestAICar1.transform.localPosition -= new Vector3(0f, 0.16f, 0f);
			}
			if (itemStep >= 3 && InstanceBadWeatherTestAICar2 != null && InstanceBadWeatherTestAICar2.transform.localPosition.y < 150f)
			{
				InstanceBadWeatherTestAICar2.transform.localPosition += new Vector3(0f, 0.16f, 0f);
			}
		}

		private void TriggerAreaEvent(Collider other)
		{
			if (other.name == "BadWeatherTestAICarMove1" && itemStep == 0)
			{
				InstanceBadWeatherTestAICar1 = UnityEngine.Object.Instantiate(Resources.Load("AppearPerson/NightTestAICar")) as GameObject;
				GameObject gameObject = GameObject.Find("BadWeatherTestAICarStartPosition1");
				InstanceBadWeatherTestAICar1.transform.parent = gameObject.transform;
				InstanceBadWeatherTestAICar1.transform.localPosition = new Vector3(0f, 0f, 0.72f);
				InstanceBadWeatherTestAICar1.transform.localEulerAngles = new Vector3(90f, 90f, 90f);
				InstanceBadWeatherTestAICar1.transform.localScale = new Vector3(1f, 1f, 1f);
				BadWeatherTestAICar1.SetActive(value: false);
				itemStep = 1;
			}
			if (other.name == "SafeX" && itemStep == 1)
			{
				SoundPlayCtl.Instance.PlaySound("会车时,未与其他车辆保持横向安全距离", 15f);
				itemStep = 2;
			}
			if (other.name == "BadWeatherTestAICarMove2" && itemStep < 3)
			{
				InstanceBadWeatherTestAICar2 = UnityEngine.Object.Instantiate(Resources.Load("AppearPerson/NightTestAICar")) as GameObject;
				GameObject gameObject2 = GameObject.Find("BadWeatherTestAICarStartPosition2");
				InstanceBadWeatherTestAICar2.transform.parent = gameObject2.transform;
				InstanceBadWeatherTestAICar2.transform.localPosition = new Vector3(0f, 0f, 0.72f);
				InstanceBadWeatherTestAICar2.transform.localEulerAngles = new Vector3(-90f, 90f, 90f);
				InstanceBadWeatherTestAICar2.transform.localScale = new Vector3(1f, 1f, 1f);
				BadWeatherTestAICar2.SetActive(value: false);
				itemStep = 3;
			}
			if (other.name == "SafeY" && itemStep == 3)
			{
				SoundPlayCtl.Instance.PlaySound("恶劣天气未与前车保持安全距离", 20f);
				itemStep = 4;
			}
			if (other.name == "BadWeatherTestOver" && itemStep != 99)
			{
				itemStep = 99;
			}
		}

		private void JudgeTurnLight(int preIndex, int curIndex)
		{
			if (curIndex > preIndex)
			{
				CarActionJudge.Instance.JudgeTurnLight3s(109);
			}
			else
			{
				CarActionJudge.Instance.JudgeTurnLight3s(110);
			}
			if (itemStep >= 3 && itemStep != 99)
			{
				SoundPlayCtl.Instance.PlaySound("冰雪道路超车或变更车道", 15f);
			}
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			ColliderDetection instance = ColliderDetection.instance;
			instance.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Remove(instance.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			ColliderDetection instance2 = ColliderDetection.instance;
			instance2.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Remove(instance2.roadIndexChangeCallBack, new UnityAction<int, int>(JudgeTurnLight));
			SoundPlayCtl.Instance.ClearClip();
		}
	}
}
