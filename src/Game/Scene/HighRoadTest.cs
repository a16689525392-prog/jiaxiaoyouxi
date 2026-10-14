using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class HighRoadTest : HighRoadSceneBase
	{
		public static HighRoadTest Instance;

		private RaycastHit stoppingHitInfo;

		private bool isRoadError;

		private bool isOpenLight;

		private bool isUpHandBrake;

		private float stopTime;

		private bool BackCar;

		private float JianGeTime;

		private float LowSpeedTime;

		private float HighSpeedTime;

		private void Awake()
		{
			Instance = this;
		}

		protected override void Start()
		{
			base.Start();
			SoundPlayCtl.Instance.PlaySound("请从前方右边第一或第二个收费站驶入");
		}

		private void Update()
		{
			if (itemStep >= 1)
			{
				if (CarState.Instance.FireOn && CarState.Instance.CurSpeed > 3f && (CarState.Instance.CarSignal.gear == 6 || CarState.Instance.CarSignal.gear == 102) && !BackCar)
				{
					SoundPlayCtl.Instance.PlaySound("驾驶机动车在高速公路上倒车", 20f);
					BackCar = true;
				}
				if (BackCar)
				{
					JianGeTime += Time.deltaTime;
					if (JianGeTime > 10f)
					{
						BackCar = false;
					}
				}
				if (itemStep == 2)
				{
					if (CarState.Instance.CurSpeed < 60f)
					{
						LowSpeedTime += Time.deltaTime;
						if (LowSpeedTime > 15f)
						{
							SoundPlayCtl.Instance.PlaySound("高速公路低于最低时速行驶", 15f);
							LowSpeedTime = 0f;
						}
					}
					else
					{
						LowSpeedTime = 0f;
					}
					if (CarState.Instance.CurSpeed > 100f)
					{
						HighSpeedTime += Time.deltaTime;
						if (HighSpeedTime > 6f)
						{
							SoundPlayCtl.Instance.PlaySound("高速公路超速行驶", 15f);
							HighSpeedTime = 0f;
						}
					}
					else
					{
						HighSpeedTime = 0f;
					}
				}
				if (itemStep == 3)
				{
					if (CarState.Instance.CurSpeed < 30f)
					{
						LowSpeedTime += Time.deltaTime;
						if (LowSpeedTime > 12f)
						{
							SoundPlayCtl.Instance.PlaySound("高速公路低于最低时速行驶", 15f);
							LowSpeedTime = 0f;
						}
					}
					else
					{
						LowSpeedTime = 0f;
					}
					if (CarState.Instance.CurSpeed > 60f)
					{
						HighSpeedTime += Time.deltaTime;
						if (HighSpeedTime > 6f)
						{
							SoundPlayCtl.Instance.PlaySound("高速公路超速行驶", 15f);
							HighSpeedTime = 0f;
						}
					}
					else
					{
						HighSpeedTime = 0f;
					}
				}
				if (itemStep == 5)
				{
					if (CarState.Instance.CurSpeed < 20f)
					{
						LowSpeedTime += Time.deltaTime;
						if (LowSpeedTime > 10f)
						{
							SoundPlayCtl.Instance.PlaySound("高速公路低于最低时速行驶", 15f);
							LowSpeedTime = 0f;
						}
					}
					else
					{
						LowSpeedTime = 0f;
					}
					if (CarState.Instance.CurSpeed > 40f)
					{
						HighSpeedTime += Time.deltaTime;
						if (HighSpeedTime > 3f)
						{
							SoundPlayCtl.Instance.PlaySound("高速公路超速行驶", 15f);
							HighSpeedTime = 0f;
						}
					}
					else
					{
						HighSpeedTime = 0f;
					}
				}
			}
			if (itemStep == 8 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out stoppingHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out stoppingHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out stoppingHitInfo, 8)) && stoppingHitInfo.collider.name == "AidRoad" && !isRoadError)
			{
				SoundPlayCtl.Instance.PlaySound("行驶中,占用应急车道", 10f);
				isRoadError = true;
			}
			if (itemStep != 9)
			{
				return;
			}
			if (CarState.Instance.FireOn)
			{
				CarState.Instance.FireOn = false;
			}
			if (CarState.Instance.carRigibody.velocity.magnitude * 3.6f > 5f)
			{
				CarState.Instance.carRigibody.velocity -= new Vector3(0.01f, 0.01f, 0.01f);
			}
			if (!ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out stoppingHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out stoppingHitInfo, 8) && !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out stoppingHitInfo, 8))
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
					if (CarState.Instance.CurSpeed < 1f)
					{
						stopTime += Time.deltaTime;
						if (stopTime > 3f)
						{
							SoundPlayCtl.Instance.PlaySound("未将机动车停靠在安全停车区", 100f);
							itemStep++;
						}
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.curCalculateSpeedH < 1f)
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
			if (itemStep == 0 && other.name == "Start_HighRoadTest_False")
			{
				itemStep = -1;
				SoundPlayCtl.Instance.PlaySound("未按规定路线行驶", 10f);
			}
			if (itemStep <= 0 && other.name == "Start_HighRoadTest_True")
			{
				itemStep = 1;
			}
			else if (itemStep == 1 && other.name == "Start_HighRoadTest_AddSpeed")
			{
				itemStep = 2;
				LowSpeedTime = 0f;
				HighSpeedTime = 0f;
			}
			else if (itemStep == 2 && other.name == "HighRoadTest_Speed60")
			{
				itemStep = 3;
				LowSpeedTime = 0f;
				HighSpeedTime = 0f;
			}
			else if (itemStep == 3 && other.name == "Start_Stopping")
			{
				SoundPlayCtl.Instance.PlaySound("请从前方匝道口进入高速公路");
				itemStep = 4;
			}
			else if (itemStep == 4 && other.name == "HighRoadTest_Speed40")
			{
				itemStep = 5;
				LowSpeedTime = 0f;
				HighSpeedTime = 0f;
			}
			else if (itemStep == 5 && other.name == "TurnLeft")
			{
				SoundPlayCtl.Instance.PlaySound("路口左转");
				itemStep = 6;
			}
			else if (itemStep == 6 && other.name == "TurnLeftLightOpen")
			{
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CarSignal.turnLight == 0)
					{
						SoundPlayCtl.Instance.PlaySound("未开启左转向灯", 10f);
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
						SoundPlayCtl.Instance.PlaySound("未开启左转向灯", 10f);
					}
					else if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 2)
					{
						SoundPlayCtl.Instance.PlaySound("未正确开启转向灯", 10f);
					}
				}
				itemStep = 7;
			}
			else if (itemStep == 7 && other.name == "StartJudgeAidRoad")
			{
				itemStep = 8;
			}
			else if (itemStep == 8 && other.name == "GuZhang")
			{
				AudioSourceCtl.Instance.PlayAudio("TirePuncture");
				SoundPlayCtl.Instance.PlaySound("车辆发生故障,请靠边停车");
				itemStep = 9;
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
