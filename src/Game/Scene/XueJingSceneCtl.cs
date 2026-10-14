using System;
using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class XueJingSceneCtl : TrainSceneBase
	{
		public GameObject InstanceSnow;

		public int tempItemType;

		private XueJingSceneBase curItemCtl;

		protected override void Start()
		{
			base.Start();
			if (ViewJumpCtl.Instance.otherType == 502)
			{
				if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject = GameObject.Find("JieDa(Clone)");
					InstanceSnow.transform.parent = gameObject.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject2 = GameObject.Find("DongFengDuoLiKa(Clone)");
					InstanceSnow.transform.parent = gameObject2.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject3 = GameObject.Find("dabache(Clone)");
					InstanceSnow.transform.parent = gameObject3.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.KaLuoLa)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject4 = GameObject.Find("KaLuoLa(Clone)");
					InstanceSnow.transform.parent = gameObject4.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.QiRuiE3)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject5 = GameObject.Find("QiRuiE3(Clone)");
					InstanceSnow.transform.parent = gameObject5.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.JieFang1121)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject6 = GameObject.Find("JieFang1121(Clone)");
					InstanceSnow.transform.parent = gameObject6.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.JieFang1125)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject7 = GameObject.Find("JieFang1125(Clone)");
					InstanceSnow.transform.parent = gameObject7.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.TianJin)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject8 = GameObject.Find("TianJin(Clone)");
					InstanceSnow.transform.parent = gameObject8.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject9 = GameObject.Find("DFKangMingSi(Clone)");
					InstanceSnow.transform.parent = gameObject9.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.PiKa)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject10 = GameObject.Find("PiKa(Clone)");
					InstanceSnow.transform.parent = gameObject10.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.AiLiSheManual || SettingInfoCtl.Instance.carType == CarType.AiLiSheAuto)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject11 = GameObject.Find("AiLiShe(Clone)");
					InstanceSnow.transform.parent = gameObject11.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (SettingInfoCtl.Instance.carType == CarType.QianYinChe)
				{
					InstanceSnow = UnityEngine.Object.Instantiate(Resources.Load("Snow")) as GameObject;
					GameObject gameObject12 = GameObject.Find("QianYinChe(Clone)");
					InstanceSnow.transform.parent = gameObject12.transform;
					InstanceSnow.transform.localPosition = new Vector3(20f, 10f, 0f);
					InstanceSnow.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceSnow.transform.localScale = new Vector3(1f, 1f, 1f);
				}
			}
			ColliderDetection instance = ColliderDetection.instance;
			instance.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Combine(instance.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			tempItemType = -1;
			base.ResetAction = (UnityAction)Delegate.Combine(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
		}

		protected override void Awake()
		{
			base.Awake();
		}

		private void TrainItemReset()
		{
			tempItemType = -1;
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
		}

		private void ResetTimePlay()
		{
			tempItemType = -1;
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
		}

		private void TriggerAreaEvent(Collider other)
		{
			JudgeItemReset(other.name);
			if (curItemCtl != null)
			{
				curItemCtl.TriggerEvent(other);
			}
		}

		private void JudgeItemReset(int itemType)
		{
			switch (itemType)
			{
			case 51202:
				JudgeItemReset("CommonStart");
				break;
			case 503:
				JudgeItemReset("NiNingLu");
				break;
			}
		}

		private void JudgeItemReset(string other)
		{
			if (!(other == "CommonStart"))
			{
				if (other == "NiNingLu" && ViewJumpCtl.Instance.itemType == 503 && tempItemType != 503)
				{
					tempItemType = 503;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<NiNingLu>();
					ItemStart();
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 51202 && tempItemType != 51202)
			{
				tempItemType = 51202;
				if (curItemCtl != null)
				{
					UnityEngine.Object.Destroy(curItemCtl);
				}
				curItemCtl = base.gameObject.AddComponent<SnowWeather>();
				ItemStart();
			}
		}

		private void ItemStart()
		{
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			ColliderDetection instance = ColliderDetection.instance;
			instance.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Remove(instance.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			SoundPlayCtl.Instance.ClearClip();
		}
	}
}
