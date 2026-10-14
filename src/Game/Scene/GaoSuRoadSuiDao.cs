using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class GaoSuRoadSuiDao : HighRoadSceneBase
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

		private GameObject aiCar;

		protected override void Start()
		{
			aiCar = GameObject.Find("Road_GaoSuGongLu/SuiDaoAICar");
		}

		private void Update()
		{
			if (!isInitCar && ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.FireOn && CarState.Instance.CurSpeed > 3f)
				{
					Object.Destroy(aiCar);
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
				Object.Destroy(aiCar);
				base.gameObject.AddComponent<AiCarManager>();
				HighRoadSceneModel highRoadSceneModel2 = (HighRoadSceneModel)SceneModelBase.Instance;
				base.gameObject.GetComponent<AiCarManager>().Init(highRoadSceneModel2.wayPointsGroup.transform);
				HighRoadSceneCtl.Instance.isReset = false;
				isInitCar = true;
			}
			if (isEnd)
			{
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CurSpeed < 60f && lowSpeedTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 10f);
						lowSpeedTime = 0f;
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curCalculateSpeedH < 60f && lowSpeedTime > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 10f);
					lowSpeedTime = 0f;
				}
			}
			errorShoudTime += Time.deltaTime;
			lowSpeedTime += Time.deltaTime;
			if (itemStep == 2 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out suiDaoHitInfo, 8)) && !(suiDaoHitInfo.collider.name == "Tunnel_InHorn") && suiDaoHitInfo.collider.name == "Tunnel_Front")
			{
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CurSpeed < 60f)
					{
						SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 10f);
					}
					else if (CarState.Instance.CurSpeed > 80f)
					{
						SoundPlayCtl.Instance.PlaySound("驶抵隧道时,未减速", 10f);
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
						SoundPlayCtl.Instance.PlaySound("驶抵隧道时,未减速", 10f);
					}
				}
				itemStep++;
			}
			if (itemStep == 4 || itemStep == 5 || itemStep == 6)
			{
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CurSpeed > 80f && errorShoudTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("隧道内驾驶时,车速过快", 100f);
						errorShoudTime = 0f;
					}
					else if (CarState.Instance.CurSpeed < 60f && lowSpeedTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 10f);
						lowSpeedTime = 0f;
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 5)
				{
					if (DriveHistoryPlay.Instance.curCalculateSpeedH > 80f && errorShoudTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("隧道内驾驶时,车速过快", 100f);
						errorShoudTime = 0f;
					}
					else if (DriveHistoryPlay.Instance.curCalculateSpeedH < 60f && lowSpeedTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 10f);
						lowSpeedTime = 0f;
					}
				}
			}
			if (itemStep == 7 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out suiDaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out suiDaoHitInfo, 8)))
			{
				_ = suiDaoHitInfo.collider.name == "Tunnel_OutHorn";
				_ = suiDaoHitInfo.collider.name == "Tunnel_BeforeOut";
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
					SoundPlayCtl.Instance.PlaySound("不按规定车道行驶", 100f);
					isRoadChange = true;
				}
				if (suiDaoHitInfo.collider.name == "Tunnel_Road_2" && !isRoadError)
				{
					SoundPlayCtl.Instance.PlaySound("变更车道", 100f);
					isRoadError = true;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "StartAiCar")
			{
				itemStep = 1;
			}
			if (itemStep == 1 && other.name == "Start_SuiDao")
			{
				SoundPlayCtl.Instance.PlaySound("前方隧道，请规范驾驶");
				itemStep = 2;
			}
			if (itemStep != 4 && other.name == "OpenFrontLight")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.frontLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("驶抵隧道时,未开启前照灯", 100f);
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("驶抵隧道时,未开启前照灯", 100f);
				}
				itemStep = 4;
			}
			if (itemStep != 5 && itemStep != 99 && other.name == "OverCar")
			{
				SoundPlayCtl.Instance.PlaySound("隧道内超车", 100f);
				itemStep = 5;
			}
			if (itemStep != 6 && itemStep != 99 && other.name == "SafeDistance")
			{
				SoundPlayCtl.Instance.PlaySound("未与前车保持安全车距", 100f);
				itemStep = 6;
			}
			if (itemStep != 7 && other.name == "Out_OpenHorn")
			{
				itemStep = 7;
			}
			else if (itemStep != 99 && other.name == "End_SuiDao")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.frontLight != 0)
				{
					SoundPlayCtl.Instance.PlaySound("驶出隧道后,未关闭前照灯", 10f);
					itemStep++;
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight != 0)
				{
					SoundPlayCtl.Instance.PlaySound("驶出隧道后,未关闭前照灯", 10f);
					itemStep++;
				}
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("通过隧道项目结束");
				itemStep = 99;
			}
		}
	}
}
