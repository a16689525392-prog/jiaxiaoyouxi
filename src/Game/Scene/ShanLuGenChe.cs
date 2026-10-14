using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class ShanLuGenChe : ShanQuSceneBase
	{
		public static bool isInitCar;

		private GameObject aiCar;

		private AiCarManager curscript;

		private ShanQuSceneModel sceneModel;

		public static bool wanDaoOverStart;

		private bool notsafe;

		protected override void Start()
		{
			aiCar = GameObject.Find("Road_ShanQuDaoLu/AICar");
			curscript = base.gameObject.AddComponent<AiCarManager>();
			sceneModel = (ShanQuSceneModel)SceneModelBase.Instance;
		}

		private void Update()
		{
			if (!isInitCar && ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.FireOn && CarState.Instance.CurSpeed > 3f)
				{
					Object.Destroy(aiCar);
					if (curscript != null)
					{
						Object.Destroy(curscript);
					}
					ShanQuSceneCtl.Instance.aiCarReset.SetActive(value: false);
					curscript.Init(sceneModel.wayPointsGroup.transform);
					isInitCar = true;
					ShanQuSceneCtl.isReset = false;
				}
			}
			else if (!isInitCar && ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && CarState.Instance.FireOn && DriveHistoryPlay.Instance.curCalculateSpeedH > 3f)
			{
				Object.Destroy(aiCar);
				if (curscript != null)
				{
					Object.Destroy(curscript);
				}
				ShanQuSceneCtl.Instance.aiCarReset.SetActive(value: false);
				curscript.Init(sceneModel.wayPointsGroup.transform);
				isInitCar = true;
				ShanQuSceneCtl.isReset = false;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType == 510 || ViewJumpCtl.Instance.itemType == 703)
			{
				if (itemStep == 0 && other.name == "GenChe_Start")
				{
					SoundPlayCtl.Instance.PlaySound("山路跟车,请保持安全车距");
					itemStep = 1;
				}
				else if (itemStep != 2 && other.name == "SafeDistance" && !wanDaoOverStart)
				{
					SoundPlayCtl.Instance.PlaySound("未与前车保持安全车距", 100f);
					itemStep = 2;
					notsafe = true;
				}
				else if (itemStep != 3 && other.name == "ChaoChe_Start")
				{
					SoundPlayCtl.Instance.PlaySound("准备超车");
					itemStep = 3;
				}
				if (notsafe && other.name == "OverCar")
				{
					SoundPlayCtl.Instance.PlaySound("未保证安全距离情况下抢道", 100f);
					notsafe = false;
				}
				if (!wanDaoOverStart && other.name == "wanDaoOverStart")
				{
					wanDaoOverStart = true;
					Debug.Log("进入弯道");
				}
				if (wanDaoOverStart && other.name == "OverCar")
				{
					SoundPlayCtl.Instance.PlaySound("弯道内超车", 100f);
					wanDaoOverStart = false;
				}
				if (other.name == "WanDaoChaoCheover")
				{
					wanDaoOverStart = false;
					Debug.Log("驶出弯道");
				}
				if (itemStep != 99 && other.name == "End")
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("项目结束");
					itemStep = 99;
				}
			}
		}
	}
}
