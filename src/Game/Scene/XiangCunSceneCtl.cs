using System;
using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class XiangCunSceneCtl : TrainSceneBase
	{
		public Light dirLight;

		public Material shuMat;

		public Material lianasMat;

		public Material Disophyros_barkMat;

		public Material Disophyros_leafs2Mat;

		public Material b3be97fMat;

		public Material Hua1Mat;

		public Material cao_hebingMat;

		public Material dapeng1Mat;

		public GameObject InstanceRain;

		public int tempItemType;

		private float triggertime;

		private XiangCunSceneBase curItemCtl;

		protected override void Start()
		{
			base.Start();
			if (ViewJumpCtl.Instance.trainType == TrainType.FreeDriving && ViewJumpCtl.Instance.itemType == 401)
			{
				shuMat.shader = Shader.Find("Sprites/Default");
				lianasMat.shader = Shader.Find("Sprites/Default");
				Disophyros_barkMat.shader = Shader.Find("Legacy Shaders/Diffuse");
				Disophyros_leafs2Mat.shader = Shader.Find("Sprites/Default");
				b3be97fMat.shader = Shader.Find("Sprites/Default");
				Hua1Mat.shader = Shader.Find("Sprites/Default");
				cao_hebingMat.shader = Shader.Find("Sprites/Default");
				dapeng1Mat.shader = Shader.Find("Legacy Shaders/Diffuse");
				base.gameObject.AddComponent<AiCarManager>();
				XiangCunSceneModel xiangCunSceneModel = (XiangCunSceneModel)SceneModelBase.Instance;
				base.gameObject.GetComponent<AiCarManager>().Init(xiangCunSceneModel.wayPointsGroup.transform);
			}
			if (ViewJumpCtl.Instance.otherType == 500)
			{
				shuMat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				lianasMat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				Disophyros_barkMat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				Disophyros_leafs2Mat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				b3be97fMat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				Hua1Mat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				cao_hebingMat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				dapeng1Mat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				InstanceRain = UnityEngine.Object.Instantiate(Resources.Load("RainGlass")) as GameObject;
				if (SettingInfoCtl.Instance.carType == CarType.JieDaManual || SettingInfoCtl.Instance.carType == CarType.JieDaAuto)
				{
					GameObject gameObject = GameObject.Find("JieDa(Clone)");
					InstanceRain.transform.parent = gameObject.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
				{
					GameObject gameObject2 = GameObject.Find("DongFengDuoLiKa(Clone)");
					InstanceRain.transform.parent = gameObject2.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
				{
					GameObject gameObject3 = GameObject.Find("dabache(Clone)");
					InstanceRain.transform.parent = gameObject3.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.YongShi)
				{
					GameObject gameObject4 = GameObject.Find("YongShi(Clone)");
					InstanceRain.transform.parent = gameObject4.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.JieFang1121)
				{
					GameObject gameObject5 = GameObject.Find("JieFang1121(Clone)");
					InstanceRain.transform.parent = gameObject5.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.QiRuiE3)
				{
					GameObject gameObject6 = GameObject.Find("QiRuiE3(Clone)");
					InstanceRain.transform.parent = gameObject6.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.KaLuoLa)
				{
					GameObject gameObject7 = GameObject.Find("KaLuoLa(Clone)");
					InstanceRain.transform.parent = gameObject7.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.TianJin)
				{
					GameObject gameObject8 = GameObject.Find("TianJin(Clone)");
					InstanceRain.transform.parent = gameObject8.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.JieFang1125)
				{
					GameObject gameObject9 = GameObject.Find("JieFang1125(Clone)");
					InstanceRain.transform.parent = gameObject9.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
				{
					GameObject gameObject10 = GameObject.Find("DFKangMingSi(Clone)");
					InstanceRain.transform.parent = gameObject10.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.PiKa)
				{
					GameObject gameObject11 = GameObject.Find("PiKa(Clone)");
					InstanceRain.transform.parent = gameObject11.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.DFMengShi)
				{
					GameObject gameObject12 = GameObject.Find("DFMengShi(Clone)");
					InstanceRain.transform.parent = gameObject12.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.AiLiSheManual || SettingInfoCtl.Instance.carType == CarType.AiLiSheAuto)
				{
					GameObject gameObject13 = GameObject.Find("AiLiShe(Clone)");
					InstanceRain.transform.parent = gameObject13.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
				}
				else if (SettingInfoCtl.Instance.carType == CarType.QianYinChe)
				{
					GameObject gameObject14 = GameObject.Find("QianYinChe(Clone)");
					InstanceRain.transform.parent = gameObject14.transform;
					InstanceRain.transform.localPosition = new Vector3(-0.75f, 10f, 0f);
					InstanceRain.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
					InstanceRain.transform.localScale = new Vector3(1f, 1f, 1f);
					dirLight.intensity = 0.2f;
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

		protected override void Update()
		{
			base.Update();
			triggertime += Time.deltaTime;
		}

		private void TriggerAreaEvent(Collider other)
		{
			if (triggertime > 2f)
			{
				if (other.name == "Bantiedaolu")
				{
					SoundPlayCtl.Instance.PlaySound("违禁驶入铁路道口", 100f);
				}
				triggertime = 0f;
			}
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
			case 51200:
				JudgeItemReset("Position_RainStart");
				break;
			case 401:
				JudgeItemReset("Position_CommonStart");
				break;
			}
		}

		private void JudgeItemReset(string other)
		{
			if (!(other == "Position_RainStart"))
			{
				if (other == "Position_CommonStart" && ViewJumpCtl.Instance.itemType == 401 && tempItemType != 401)
				{
					tempItemType = 401;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<WeiJinZhiXing>();
					ItemStart();
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 51200 && tempItemType != 51200)
			{
				tempItemType = 51200;
				if (curItemCtl != null)
				{
					UnityEngine.Object.Destroy(curItemCtl);
				}
				curItemCtl = base.gameObject.AddComponent<RainWeather>();
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
