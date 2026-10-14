using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class SuiDaoTest : HighRoadSceneBase
	{
		private RaycastHit suiDaoHitInfo;

		private bool isOnHorn;

		private bool isRoadError;

		private bool isRoadChange;

		private bool isAiRoad;

		public static bool isInitCar;

		private bool isEnd;

		private float errorShoudTime = 10f;

		private float lowSpeedTime = 10f;

		private float HighSpeedTime;

		protected override void Start()
		{
		}

		private void Update()
		{
			if (!isInitCar && ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.FireOn && CarState.Instance.CurSpeed > 3f)
				{
					HighRoadSceneCtl.Instance.aiCarReset.SetActive(value: false);
					base.gameObject.AddComponent<AiCarManager>();
					HighRoadSceneModel highRoadSceneModel = (HighRoadSceneModel)SceneModelBase.Instance;
					base.gameObject.GetComponent<AiCarManager>().Init(highRoadSceneModel.wayPointsGroup.transform);
					HighRoadSceneCtl.Instance.isReset = false;
					isInitCar = true;
				}
			}
			else if (!isInitCar && ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && CarState.Instance.FireOn && DriveHistoryPlay.Instance.curCalculateSpeedH > 3f)
			{
				base.gameObject.AddComponent<AiCarManager>();
				HighRoadSceneModel highRoadSceneModel2 = (HighRoadSceneModel)SceneModelBase.Instance;
				base.gameObject.GetComponent<AiCarManager>().Init(highRoadSceneModel2.wayPointsGroup.transform);
				HighRoadSceneCtl.Instance.isReset = false;
				isInitCar = true;
			}
			if (itemStep == 2 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out suiDaoHitInfo, 8)))
			{
				if (suiDaoHitInfo.collider.name == "Tunnel_InHorn")
				{
					if (ViewJumpCtl.Instance.modelType == 2 && !isOnHorn && CarState.Instance.CarSignal.horn == 1)
					{
						isOnHorn = true;
					}
					else if (ViewJumpCtl.Instance.modelType == 5 && !isOnHorn && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn == 1)
					{
						isOnHorn = true;
					}
				}
				else if (suiDaoHitInfo.collider.name == "Tunnel_Front")
				{
					if (!isOnHorn)
					{
						SoundPlayCtl.Instance.PlaySound("驶抵隧道入口时,未鸣喇叭", 10f);
						isOnHorn = true;
					}
					if (ViewJumpCtl.Instance.modelType == 2)
					{
						if (CarState.Instance.CurSpeed < 60f)
						{
							SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 10f);
						}
						else if (CarState.Instance.CurSpeed > 80f)
						{
							SoundPlayCtl.Instance.PlaySound("驶抵隧道时,未减速", 25f);
						}
					}
					else if (ViewJumpCtl.Instance.modelType == 5)
					{
						if (DriveHistoryPlay.Instance.curCalculateSpeedH < 60f)
						{
							SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 10f);
						}
						else if (DriveHistoryPlay.Instance.curCalculateSpeedH > 80f)
						{
							SoundPlayCtl.Instance.PlaySound("驶抵隧道时,未减速", 25f);
						}
					}
					itemStep++;
				}
			}
			errorShoudTime += Time.deltaTime;
			lowSpeedTime += Time.deltaTime;
			if (itemStep >= 3 && itemStep != 99)
			{
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CurSpeed > 80f && errorShoudTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("隧道内驾驶时,车速过快", 25f);
						errorShoudTime = 0f;
					}
					else if (CarState.Instance.CurSpeed < 60f && lowSpeedTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 25f);
						lowSpeedTime = 0f;
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 5)
				{
					if (DriveHistoryPlay.Instance.curCalculateSpeedH > 80f && errorShoudTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("隧道内驾驶时,车速过快", 25f);
						errorShoudTime = 0f;
					}
					else if (DriveHistoryPlay.Instance.curCalculateSpeedH < 60f && lowSpeedTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 25f);
						lowSpeedTime = 0f;
					}
				}
			}
			if (itemStep == 7 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out suiDaoHitInfo, 8)))
			{
				if (suiDaoHitInfo.collider.name == "Tunnel_OutHorn")
				{
					if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.horn == 1 && !isOnHorn)
					{
						isOnHorn = true;
					}
					else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn == 1 && !isOnHorn)
					{
						isOnHorn = true;
					}
				}
				if (suiDaoHitInfo.collider.name == "Tunnel_BeforeOut" && !isOnHorn)
				{
					SoundPlayCtl.Instance.PlaySound("驶抵隧道出口时,未鸣喇叭", 10f);
					isOnHorn = true;
				}
			}
			if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out suiDaoHitInfo, 8))
			{
				if (suiDaoHitInfo.collider.name == "AidRoad" && !isAiRoad)
				{
					SoundPlayCtl.Instance.PlaySound("行驶中,占用应急车道", 100f);
					isAiRoad = true;
				}
				if (suiDaoHitInfo.collider.name == "Tunnel_Road_1" && !isRoadChange)
				{
					SoundPlayCtl.Instance.PlaySound("不按规定车道行驶", 10f);
					isRoadChange = true;
				}
				if (suiDaoHitInfo.collider.name == "Tunnel_Road_2" && !isRoadError)
				{
					SoundPlayCtl.Instance.PlaySound("变更车道", 10f);
					isRoadError = true;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_SuiDao")
			{
				SoundPlayCtl.Instance.PlaySound("前方隧道，请规范驾驶");
				itemStep = 1;
			}
			if (itemStep == 1 && other.name == "OpenFrontLight")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.frontLight == 2)
				{
					SoundPlayCtl.Instance.PlaySound("未正确使用灯光", 25f);
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 2)
				{
					SoundPlayCtl.Instance.PlaySound("未正确使用灯光", 25f);
				}
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.frontLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("驶抵隧道时,未开启前照灯", 10f);
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("驶抵隧道时,未开启前照灯", 10f);
				}
				itemStep = 2;
			}
			if (itemStep != 7 && other.name == "Out_OpenHorn")
			{
				itemStep = 7;
				isOnHorn = false;
			}
			else if (itemStep != 99 && other.name == "End_SuiDao")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.frontLight != 0)
				{
					SoundPlayCtl.Instance.PlaySound("驶出隧道后,未关闭前照灯", 25f);
					itemStep++;
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight != 0)
				{
					SoundPlayCtl.Instance.PlaySound("驶出隧道后,未关闭前照灯", 25f);
					itemStep++;
				}
				isOnHorn = false;
				ItemEnd();
				itemStep = 99;
			}
		}
	}
}
