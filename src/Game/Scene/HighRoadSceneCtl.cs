using System;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class HighRoadSceneCtl : TrainSceneBase
	{
		private static HighRoadSceneCtl instance;

		public GameObject aiCarReset;

		private int tempItemType;

		private GameObject PrefabTiShiText;

		public bool isReset;

		private YingJiXunLianCtlBase curItemCtl;

		private HighRoadSceneBase curItemCtlAdd;

		public static HighRoadSceneCtl Instance => instance;

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

		private void ResetTimePlay()
		{
			CarState.Instance.FireOn = false;
			tempItemType = -1;
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
			if (!isReset)
			{
				isReset = true;
			}
			if (ViewJumpCtl.Instance.itemType == 506)
			{
				GaoSuRoadSuiDao.isInitCar = false;
				aiCarReset.SetActive(value: true);
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
			if (ViewJumpCtl.Instance.itemType == 506)
			{
				GaoSuRoadSuiDao.isInitCar = false;
				aiCarReset.SetActive(value: true);
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
			case 404:
				JudgeItemReset("Position_CommonStart");
				break;
			case 608:
				JudgeItemReset("Start_Position7");
				break;
			case 506:
				JudgeItemReset("Position_SuiDao");
				break;
			case 507:
				JudgeItemReset("Position_StopCar");
				break;
			case 704:
				JudgeItemReset("Position_HighRoadTest");
				break;
			case 705:
				JudgeItemReset("Position_SuiDaoTest");
				break;
			}
		}

		private void JudgeItemReset(string other)
		{
			switch (other)
			{
			case "Position_SuiDaoTest":
				if (ViewJumpCtl.Instance.itemType == 705 && tempItemType != 705)
				{
					tempItemType = 705;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<SuiDaoTest>();
					ItemStart();
				}
				break;
			case "Position_HighRoadTest":
				if (ViewJumpCtl.Instance.itemType == 704 && tempItemType != 704)
				{
					tempItemType = 704;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<HighRoadTest>();
					ItemStart();
				}
				break;
			case "Start_Position7":
				if (ViewJumpCtl.Instance.itemType == 608 && tempItemType != 608)
				{
					tempItemType = 608;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					curItemCtl = base.gameObject.AddComponent<FrontCarFlash>();
					ItemStart();
				}
				break;
			case "Position_SuiDao":
				if (ViewJumpCtl.Instance.itemType == 506 && tempItemType != 506)
				{
					tempItemType = 506;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<GaoSuRoadSuiDao>();
					ItemStart();
				}
				break;
			case "Position_StopCar":
				if (ViewJumpCtl.Instance.itemType == 507 && tempItemType != 507)
				{
					tempItemType = 507;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<GaoSuRoadStopping>();
					ItemStart();
				}
				break;
			case "Position_CommonStart":
				if (ViewJumpCtl.Instance.itemType == 404 && tempItemType != 404)
				{
					tempItemType = 404;
					if (curItemCtlAdd != null)
					{
						UnityEngine.Object.Destroy(curItemCtlAdd);
					}
					curItemCtlAdd = base.gameObject.AddComponent<GaoSuRoadOver>();
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
			if (ViewJumpCtl.Instance.itemType == 506)
			{
				if (aiCarReset != null)
				{
					aiCarReset.SetActive(value: false);
				}
				GaoSuRoadSuiDao.isInitCar = false;
			}
		}
	}
}
