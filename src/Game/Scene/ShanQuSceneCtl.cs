using System;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class ShanQuSceneCtl : TrainSceneBase
	{
		public Material shuMat;

		private GameObject PrefabTiShiText;

		public GameObject aiCarReset;

		private GameObject aiCar;

		private static ShanQuSceneCtl instance;

		public int tempItemType;

		public static bool isReset;

		public bool yanShiMoHu;

		private YingJiXunLianCtlBase curItemCtl;

		private ShanQuSceneBase curItemCtlAdd;

		public static ShanQuSceneCtl Instance => instance;

		protected override void Start()
		{
			base.Start();
			if (ViewJumpCtl.Instance.otherType == 501)
			{
				shuMat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
			}
			else
			{
				shuMat.shader = Shader.Find("Sprites/Default");
			}
			if (ViewJumpCtl.Instance.modelType == 0 && ViewJumpCtl.Instance.trainType == TrainType.YingjiXunlian)
			{
				PrefabTiShiText = UnityEngine.Object.Instantiate(Resources.Load("TiShi/YingJiTiShiText")) as GameObject;
				GameObject gameObject = GameObject.Find("SceneText");
				PrefabTiShiText.transform.parent = gameObject.transform;
				PrefabTiShiText.transform.localPosition = new Vector3(0f, 130f, 0f);
				PrefabTiShiText.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
				PrefabTiShiText.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			ColliderDetection colliderDetection = ColliderDetection.instance;
			colliderDetection.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Combine(colliderDetection.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			tempItemType = -1;
			base.ResetAction = (UnityAction)Delegate.Combine(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
			AiCarManager aiCarManager = base.gameObject.AddComponent<AiCarManager>();
			ShanQuSceneModel shanQuSceneModel = (ShanQuSceneModel)SceneModelBase.Instance;
			aiCarManager.OtherInit(shanQuSceneModel.OtherAiCarwayPointsGroup.transform);
			aiCar = GameObject.Find("Road_ShanQuDaoLu/AICar");
			if (ViewJumpCtl.Instance.itemType != 510 && ViewJumpCtl.Instance.itemType != 703)
			{
				UnityEngine.Object.Destroy(aiCar);
			}
		}

		protected override void Awake()
		{
			base.Awake();
			instance = this;
		}

		private void ResetTimePlay()
		{
			CarState.Instance.FireOn = false;
			tempItemType = -1;
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
			if (!isReset)
			{
				isReset = true;
			}
			ShanLuGenChe.isInitCar = false;
			if (ViewJumpCtl.Instance.itemType == 510 || ViewJumpCtl.Instance.itemType == 703)
			{
				aiCarReset.SetActive(value: true);
			}
			if (ViewJumpCtl.Instance.itemType == 703)
			{
				if (curItemCtlAdd != null)
				{
					UnityEngine.Object.Destroy(curItemCtlAdd);
				}
				if (curItemCtl != null)
				{
					UnityEngine.Object.Destroy(curItemCtl);
				}
			}
		}

		private void TrainItemReset()
		{
			tempItemType = -1;
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
			if (!isReset)
			{
				isReset = true;
			}
			ShanLuGenChe.isInitCar = false;
			if (ViewJumpCtl.Instance.itemType == 510 || ViewJumpCtl.Instance.itemType == 703)
			{
				aiCarReset.SetActive(value: true);
			}
			if (ViewJumpCtl.Instance.itemType == 703)
			{
				if (curItemCtlAdd != null)
				{
					UnityEngine.Object.Destroy(curItemCtlAdd);
				}
				if (curItemCtl != null)
				{
					UnityEngine.Object.Destroy(curItemCtl);
				}
			}
		}

		private void TriggerAreaEvent(Collider other)
		{
			JudgeItemReset(other.name);
			if (curItemCtl != null)
			{
				curItemCtl.TriggerEvent(other);
			}
			if (curItemCtlAdd != null)
			{
				curItemCtlAdd.TriggerEvent(other);
			}
		}

		private void JudgeItemReset(int itemType)
		{
			switch (itemType)
			{
			case 402:
				JudgeItemReset("Position_ShanQu");
				break;
			case 51201:
				JudgeItemReset("Position_CommonStart");
				break;
			case 604:
				JudgeItemReset("Start_Position3");
				break;
			case 605:
				JudgeItemReset("Start_Position4");
				break;
			case 607:
				JudgeItemReset("Start_Position6");
				break;
			case 606:
				JudgeItemReset("Start_Position5");
				break;
			case 609:
				JudgeItemReset("Start_Position8");
				break;
			case 612:
				JudgeItemReset("Start_Position11");
				break;
			case 510:
				JudgeItemReset("Position_GenChe");
				break;
			case 5100:
				JudgeItemReset("Position_HuiChe");
				break;
			case 509:
				JudgeItemReset("Position_PoStart");
				break;
			case 5090:
				JudgeItemReset("Position_WanStart");
				break;
			}
		}

		private void JudgeItemReset(string other)
		{
			switch (other)
			{
			case "Position_ShanQu":
				if (ViewJumpCtl.Instance.itemType == 402 && tempItemType != 402)
				{
					tempItemType = 402;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<JinXing>();
					ItemStart();
				}
				break;
			case "Position_CommonStart":
				if (ViewJumpCtl.Instance.itemType == 51201 && tempItemType != 51201)
				{
					tempItemType = 51201;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<FogWeather>();
					ItemStart();
				}
				break;
			case "Position_PoStart":
				if (ViewJumpCtl.Instance.itemType == 509 && tempItemType != 509)
				{
					tempItemType = 509;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<ShanQuPoDao>();
					ItemStart();
				}
				break;
			case "Position_WanStart":
				if ((ViewJumpCtl.Instance.itemType == 5090 || ViewJumpCtl.Instance.itemType == 703) && tempItemType != 5090)
				{
					tempItemType = 5090;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<ShanQuWanDao>();
					ItemStart();
				}
				break;
			case "Position_GenChe":
				if ((ViewJumpCtl.Instance.itemType == 510 || ViewJumpCtl.Instance.itemType == 703) && tempItemType != 510)
				{
					tempItemType = 510;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<ShanLuGenChe>();
					ItemStart();
				}
				break;
			case "Position_HuiChe":
				if (ViewJumpCtl.Instance.itemType == 5100 && tempItemType != 5100)
				{
					tempItemType = 5100;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<ShanLuHuiChe>();
					ItemStart();
				}
				break;
			case "Start_Position3":
				if (ViewJumpCtl.Instance.itemType == 604 && tempItemType != 604)
				{
					tempItemType = 604;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<CarBodyFire>();
					ItemStart();
				}
				break;
			case "Start_Position4":
				if (ViewJumpCtl.Instance.itemType == 605 && tempItemType != 605)
				{
					tempItemType = 605;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<SteerWheelBad>();
					ItemStart();
				}
				break;
			case "Start_Position6":
				if (ViewJumpCtl.Instance.itemType == 607 && tempItemType != 607)
				{
					tempItemType = 607;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 2)
					{
						curItemCtl = base.gameObject.AddComponent<TiredDrive>();
					}
					else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
					{
						curItemCtl = base.gameObject.AddComponent<TiredDrive>();
						yanShiMoHu = false;
					}
					ItemStart();
				}
				break;
			case "Start_Position5":
				if (tempItemType != 606)
				{
					tempItemType = 606;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<KengWaRoad>();
					ItemStart();
				}
				break;
			case "Start_Position8":
				if (tempItemType != 609)
				{
					tempItemType = 609;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<FrontFallStone>();
					ItemStart();
				}
				break;
			case "Start_Position11":
				if (tempItemType != 612)
				{
					tempItemType = 612;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<Landslide>();
					ItemStart();
				}
				break;
			case "StartMohu":
				if (ViewJumpCtl.Instance.itemType == 607 && (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5))
				{
					GameObject.Find("DriveCamera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = true;
					yanShiMoHu = true;
				}
				break;
			}
		}

		private void ItemStart()
		{
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			UnityEngine.Object.Destroy(PrefabTiShiText);
			ColliderDetection colliderDetection = ColliderDetection.instance;
			colliderDetection.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Remove(colliderDetection.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainItemReset));
			SoundPlayCtl.Instance.ClearClip();
			if (ViewJumpCtl.Instance.itemType == 607 && ViewJumpCtl.Instance.modelType == 5)
			{
				GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
			}
			ShanLuGenChe.isInitCar = false;
			if ((ViewJumpCtl.Instance.itemType == 510 || ViewJumpCtl.Instance.itemType == 703) && aiCarReset != null)
			{
				aiCarReset.SetActive(value: false);
			}
		}
	}
}
