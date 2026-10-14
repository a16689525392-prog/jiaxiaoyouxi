using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class BoomWheel : YingJiXunLianCtlBase
	{
		private static BoomWheel instance;

		private RaycastHit boomWheelHitInfo;

		private float boomTime;

		private float stopTime;

		private bool isFootBrake;

		private bool slowBrake;

		public static BoomWheel Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out boomWheelHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out boomWheelHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out boomWheelHitInfo, 8)) && boomWheelHitInfo.collider.name == "FootBrake")
			{
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CurSpeed > 10f && !isFootBrake && (CarState.Instance.CarSignal.footBrake > 0.6f || CarState.Instance.CarSignal.handBrake > 0.6f))
					{
						SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,刹车过急", 100f);
						isFootBrake = true;
					}
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH > 10f && !isFootBrake && (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.6f || DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake > 0.6f))
				{
					SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,刹车过急", 100f);
					isFootBrake = true;
				}
			}
			if (itemStep == 2)
			{
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CarSignal.footBrake < 0.1f && !slowBrake)
					{
						boomTime += Time.deltaTime;
						if (boomTime > 5f)
						{
							SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,未轻踩脚刹", 100f);
							slowBrake = true;
						}
					}
					else if (CarState.Instance.CurSpeed > 10f)
					{
						boomTime += Time.deltaTime;
						if (boomTime > 20f)
						{
							SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,未减速靠边停车", 100f);
							itemStep = 5;
						}
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
				{
					if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake < 0.1f && !slowBrake)
					{
						boomTime += Time.deltaTime;
						if (boomTime > 5f)
						{
							SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,未轻踩脚刹", 100f);
							slowBrake = true;
						}
					}
					else if (DriveHistoryPlay.Instance.curCalculateSpeedH > 10f)
					{
						boomTime += Time.deltaTime;
						if (boomTime > 15f)
						{
							SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,未减速靠边停车", 100f);
							itemStep = 5;
						}
					}
				}
			}
			if (itemStep == 3 && ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed < 0.1f)
			{
				stopTime += Time.deltaTime;
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out boomWheelHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out boomWheelHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out boomWheelHitInfo, 8)) && boomWheelHitInfo.collider.name == "KaoBian" && stopTime > 7f)
				{
					if (CarState.Instance.CarSignal.handBrake < 0.7f || CarState.Instance.CarSignal.turnLight != 3)
					{
						SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,靠边停车后,未在规定时间内拉手刹或未开启双闪灯", 100f);
						itemStep = 4;
					}
					if (CarState.Instance.CarSignal.handBrake > 0.7f || CarState.Instance.CarSignal.turnLight == 3)
					{
						ItemEnd();
						SoundPlayCtl.Instance.PlaySound("突然爆胎项目结束");
						itemStep = 99;
					}
				}
			}
			else
			{
				if (itemStep != 3 || (ViewJumpCtl.Instance.modelType != 0 && ViewJumpCtl.Instance.modelType != 5) || !(DriveHistoryPlay.Instance.curCalculateSpeedH < 0.1f))
				{
					return;
				}
				stopTime += Time.deltaTime;
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out boomWheelHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out boomWheelHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out boomWheelHitInfo, 8)) && boomWheelHitInfo.collider.name == "KaoBian" && stopTime > 7f)
				{
					if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake < 0.7f || DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 3)
					{
						SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,靠边停车后,未在规定时间内拉手刹或未开启双闪灯", 100f);
						itemStep = 4;
					}
					if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake > 0.7f || DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 3)
					{
						ItemEnd();
						SoundPlayCtl.Instance.PlaySound("突然爆胎项目结束");
						itemStep = 99;
					}
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType != 602)
			{
				return;
			}
			if (itemStep == 0 && other.name == "Start_Position1")
			{
				SoundPlayCtl.Instance.PlaySound("突然爆胎项目开始");
				itemStep = 1;
			}
			else if (itemStep == 1 && other.name == "Boom")
			{
				AudioSourceCtl.Instance.PlayAudio("TirePuncture");
				itemStep = 2;
			}
			else if (itemStep == 2 && other.name == "LowSpeed")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,未降低车速情况下转向", 100f);
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,未降低车速情况下转向", 100f);
				}
				itemStep = 3;
			}
			else if (itemStep != 5 && other.name == "NotKaoBian")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed < 10f)
				{
					SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,未靠边停车", 100f);
					itemStep = 5;
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH < 10f)
				{
					SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,未靠边停车", 100f);
					itemStep = 5;
				}
			}
			else if (itemStep != 99 && other.name == "BoolWheelEnd")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("突然爆胎项目结束");
				itemStep = 99;
			}
		}
	}
}
