using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class GaoSuRoadStopping : HighRoadSceneBase
	{
		private RaycastHit stoppingHitInfo;

		private bool isRoadError;

		private bool isLowSpeed;

		private bool isOpenLight;

		private bool isUpHandBrake;

		private float stopTime;

		private void Update()
		{
			if (itemStep == 3 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out stoppingHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out stoppingHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out stoppingHitInfo, 8)) && stoppingHitInfo.collider.name == "isLowSpeed")
			{
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CurSpeed < 30f && !isLowSpeed)
					{
						SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 100f);
						isLowSpeed = true;
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.curCalculateSpeedH < 30f && !isLowSpeed)
				{
					SoundPlayCtl.Instance.PlaySound("高速公路超低速行驶", 100f);
					isLowSpeed = true;
				}
			}
			if (itemStep == 4 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out stoppingHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out stoppingHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out stoppingHitInfo, 8)) && stoppingHitInfo.collider.name == "AidRoad" && !isRoadError)
			{
				SoundPlayCtl.Instance.PlaySound("行驶中,占用应急车道", 100f);
				isRoadError = true;
			}
			if (itemStep != 5 || (!ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out stoppingHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out stoppingHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out stoppingHitInfo, 8)))
			{
				return;
			}
			if (stoppingHitInfo.collider.name == "StoppingArea" && !isRoadError)
			{
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CarSignal.gear == 0 && CarState.Instance.CurSpeed < 1f)
					{
						stopTime += Time.deltaTime;
						if (stopTime > 5f && !isOpenLight && CarState.Instance.CarSignal.turnLight != 3)
						{
							SoundPlayCtl.Instance.PlaySound("停车后,未开危险警示灯提醒其他车辆", 10f);
							isOpenLight = true;
						}
						if (stopTime > 7f && !isUpHandBrake && CarState.Instance.CarSignal.handBrake < 0.7f)
						{
							SoundPlayCtl.Instance.PlaySound("停车后,未拉起手刹", 10f);
							isUpHandBrake = true;
						}
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 0 && DriveHistoryPlay.Instance.curCalculateSpeedH < 1f)
				{
					stopTime += Time.deltaTime;
					if (stopTime > 5f && !isOpenLight && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 3)
					{
						SoundPlayCtl.Instance.PlaySound("停车后,未开危险警示灯提醒其他车辆", 10f);
						isOpenLight = true;
					}
					if (stopTime > 7f && !isUpHandBrake && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake < 0.7f)
					{
						SoundPlayCtl.Instance.PlaySound("停车后,未拉起手刹", 10f);
						isUpHandBrake = true;
					}
				}
			}
			else
			{
				if ((!(stoppingHitInfo.collider.name == "ForbidStopping") && !(stoppingHitInfo.collider.name == "AidRoad")) || isRoadError)
				{
					return;
				}
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CarSignal.footBrake > 0.6f && CarState.Instance.CurSpeed < 1f)
					{
						stopTime += Time.deltaTime;
						if (stopTime > 3f)
						{
							SoundPlayCtl.Instance.PlaySound("未将机动车停靠在安全停车区", 100f);
							itemStep++;
						}
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.6f && DriveHistoryPlay.Instance.curCalculateSpeedH < 1f)
				{
					stopTime += Time.deltaTime;
					if (stopTime > 3f)
					{
						SoundPlayCtl.Instance.PlaySound("未将机动车停靠在安全停车区", 100f);
						itemStep++;
					}
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_Stopping")
			{
				SoundPlayCtl.Instance.PlaySound("请从前方匝道口进入高速公路");
				itemStep = 1;
			}
			else if (itemStep != 2 && other.name == "TurnLeft")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.frontLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("路口左转");
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("路口左转");
				}
				itemStep = 2;
			}
			else if (itemStep != 3 && other.name == "TurnLeftLightOpen")
			{
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CarSignal.turnLight == 0)
					{
						SoundPlayCtl.Instance.PlaySound("未开启左转向灯", 100f);
					}
					else if (CarState.Instance.CarSignal.turnLight == 2)
					{
						SoundPlayCtl.Instance.PlaySound("未正确开启转向灯", 10f);
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 5)
				{
					if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 0)
					{
						SoundPlayCtl.Instance.PlaySound("未开启左转向灯", 100f);
					}
					else if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 2)
					{
						SoundPlayCtl.Instance.PlaySound("未正确开启转向灯", 10f);
					}
				}
				itemStep = 3;
			}
			else if (itemStep != 4 && other.name == "StartJudgeAidRoad")
			{
				itemStep = 4;
			}
			else if (itemStep != 5 && other.name == "GuZhang")
			{
				SoundPlayCtl.Instance.PlaySound("车辆发生故障,请靠边停车");
				itemStep = 5;
			}
			else if (itemStep != 99 && other.name == "NotStopping")
			{
				SoundPlayCtl.Instance.PlaySound("高速公路车辆发生故障,未及时平稳靠边停车", 100f);
				ItemEnd();
				itemStep = 99;
			}
		}
	}
}
