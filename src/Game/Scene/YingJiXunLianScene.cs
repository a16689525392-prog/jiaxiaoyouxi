using System;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class YingJiXunLianScene : TrainSceneBase
	{
		private static YingJiXunLianScene instance;

		private int tempItemType;

		private GameObject PrefabTiShiText;

		private YingJiXunLianCtlBase curItemCtl;

		public static YingJiXunLianScene Instance => instance;

		protected override void Awake()
		{
			instance = this;
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
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
		}

		private void TrainItemReset()
		{
			tempItemType = -1;
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
		}

		private void ResetTimePlay()
		{
			CarState.Instance.FireOn = false;
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
			case 601:
				JudgeItemReset("Start_Position0");
				break;
			case 602:
				JudgeItemReset("Start_Position1");
				break;
			case 603:
				JudgeItemReset("Start_Position2");
				break;
			case 610:
				JudgeItemReset("Start_Position3");
				break;
			case 613:
				JudgeItemReset("Start_Position4");
				break;
			case 614:
				JudgeItemReset("Start_Position5");
				break;
			case 615:
				JudgeItemReset("Start_Position6");
				break;
			case 611:
				JudgeItemReset("Start_Position10");
				break;
			case 604:
			case 605:
			case 606:
			case 607:
			case 608:
			case 609:
			case 612:
				break;
			}
		}

		private void JudgeItemReset(string other)
		{
			switch (other)
			{
			case "Start_Position0":
				if (tempItemType != 601)
				{
					tempItemType = 601;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<AppearPerson>();
					ItemStart();
				}
				break;
			case "Start_Position1":
				if (tempItemType != 602)
				{
					tempItemType = 602;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<BoomWheel>();
					ItemStart();
				}
				break;
			case "Start_Position2":
				if (tempItemType != 603)
				{
					tempItemType = 603;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<DrunkDriving>();
					ItemStart();
				}
				break;
			case "Start_Position10":
				if (ViewJumpCtl.Instance.itemType == 611 && tempItemType != 611)
				{
					tempItemType = 611;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<BrakeBad>();
					ItemStart();
				}
				break;
			case "Start_Position3":
				if (ViewJumpCtl.Instance.itemType == 610 && tempItemType != 610)
				{
					tempItemType = 610;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<NoOil>();
					ItemStart();
				}
				break;
			case "Start_Position4":
				if (ViewJumpCtl.Instance.itemType == 613 && tempItemType != 613)
				{
					tempItemType = 613;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<BusBlindArea>();
					ItemStart();
				}
				break;
			case "Start_Position5":
				if (ViewJumpCtl.Instance.itemType == 614 && tempItemType != 614)
				{
					tempItemType = 614;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<RobRoad>();
					ItemStart();
				}
				break;
			case "Start_Position6":
				if (ViewJumpCtl.Instance.itemType == 615 && tempItemType != 615)
				{
					tempItemType = 615;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<AllYJXunLian>();
					ItemStart();
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
			if ((ViewJumpCtl.Instance.itemType == 603 || ViewJumpCtl.Instance.itemType == 615) && ViewJumpCtl.Instance.modelType == 5)
			{
				GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
			}
		}
	}
}
