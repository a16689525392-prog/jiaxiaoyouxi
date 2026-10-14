using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class BrakeBad : YingJiXunLianCtlBase
	{
		private static BrakeBad instance;

		private RaycastHit brakeBadHitInfo;

		private bool isOverSpeed;

		private bool isHorn;

		private bool isGear1;

		private bool gearSound;

		private bool isBlink;

		public bool isBrakeBad;

		public bool isJudgeAction;

		private float afterBrakeBadTime;

		public static BrakeBad Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.gear == 1)
			{
				isGear1 = true;
			}
			else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 1)
			{
				isGear1 = true;
			}
			else
			{
				isGear1 = false;
			}
			if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out brakeBadHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out brakeBadHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out brakeBadHitInfo, 8)) && brakeBadHitInfo.collider.name == "JudgeSpeed" && !isOverSpeed)
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed > 30f)
				{
					SoundPlayCtl.Instance.PlaySound("通过斑马线车速高于30公里/时", 100f);
					isOverSpeed = true;
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH > 30f)
				{
					SoundPlayCtl.Instance.PlaySound("通过斑马线车速高于30公里/时", 100f);
					isOverSpeed = true;
				}
			}
			if (itemStep == 2)
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.footBrake > 0.1f)
				{
					if (!isBrakeBad)
					{
						SoundPlayCtl.Instance.PlaySound("车辆突发故障,制动装置失效");
						isBrakeBad = true;
						afterBrakeBadTime = 0f;
						itemStep = 3;
					}
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.1f && !isBrakeBad)
				{
					SoundPlayCtl.Instance.PlaySound("车辆突发故障,制动装置失效");
					isBrakeBad = true;
					afterBrakeBadTime = 0f;
					itemStep = 3;
				}
			}
			if (itemStep == 3 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out brakeBadHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out brakeBadHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out brakeBadHitInfo, 8)))
			{
				if (brakeBadHitInfo.collider.name == "Judge")
				{
					afterBrakeBadTime += Time.deltaTime;
					if (ViewJumpCtl.Instance.modelType == 2 && !isHorn)
					{
						if (CarState.Instance.CarSignal.horn == 1)
						{
							isHorn = true;
							Debug.Log("horn=" + isHorn);
						}
						else if (afterBrakeBadTime > 60f)
						{
							SoundPlayCtl.Instance.PlaySound("当车辆制动失效时,未鸣喇叭", 100f);
							isHorn = true;
							Debug.Log("horn=" + isHorn);
						}
					}
					else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && !isHorn)
					{
						if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn == 1)
						{
							isHorn = true;
							Debug.Log("horn=" + isHorn);
						}
						else if (afterBrakeBadTime > 60f)
						{
							SoundPlayCtl.Instance.PlaySound("当车辆制动失效时,未鸣喇叭", 100f);
							isHorn = true;
							Debug.Log("horn=" + isHorn);
						}
					}
				}
				if (brakeBadHitInfo.collider.name == "Judge")
				{
					afterBrakeBadTime += Time.deltaTime;
					if (ViewJumpCtl.Instance.modelType == 2 && !isBlink)
					{
						if (CarState.Instance.CarSignal.turnLight == 3)
						{
							isBlink = true;
							Debug.Log("isBlink=" + isBlink);
						}
						else if (afterBrakeBadTime > 62f && CarState.Instance.CarSignal.turnLight != 3)
						{
							SoundPlayCtl.Instance.PlaySound("当车辆制动失效时,未开启双闪灯", 100f);
							isBlink = true;
							Debug.Log("isBlink=" + isBlink);
						}
					}
					else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && !isBlink)
					{
						if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 3)
						{
							isBlink = true;
							Debug.Log("isBlink=" + isBlink);
						}
						else if (afterBrakeBadTime > 62f && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 3)
						{
							SoundPlayCtl.Instance.PlaySound("当车辆制动失效时,未开启双闪灯", 100f);
							isBlink = true;
							Debug.Log("isBlink=" + isBlink);
						}
					}
				}
				if (brakeBadHitInfo.collider.name == "Judge" && !isGear1 && !gearSound)
				{
					afterBrakeBadTime += Time.deltaTime;
					if (afterBrakeBadTime > 64f)
					{
						SoundPlayCtl.Instance.PlaySound("当车辆制动失效时,未逐级降档", 100f);
						gearSound = true;
						Debug.Log("gearSound=" + gearSound);
					}
				}
				else if (brakeBadHitInfo.collider.name == "Judge" && isGear1 && !gearSound)
				{
					gearSound = true;
					Debug.Log("gearSound=" + gearSound);
				}
				if (brakeBadHitInfo.collider.name == "Judge" && isHorn && isBlink && gearSound && !isJudgeAction)
				{
					isJudgeAction = true;
					Debug.Log("isJudgeAction=" + isJudgeAction);
				}
			}
			if (isJudgeAction && itemStep == 3 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out brakeBadHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out brakeBadHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out brakeBadHitInfo, 8)))
			{
				if (brakeBadHitInfo.collider.name == "Judge" && ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed < 10f)
				{
					afterBrakeBadTime = 0f;
					Debug.Log(afterBrakeBadTime);
					itemStep = 4;
				}
				else if (brakeBadHitInfo.collider.name == "Judge" && (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH < 10f)
				{
					afterBrakeBadTime = 0f;
					Debug.Log(afterBrakeBadTime);
					itemStep = 4;
				}
			}
			if (itemStep != 4)
			{
				return;
			}
			afterBrakeBadTime += Time.deltaTime;
			Debug.Log(afterBrakeBadTime);
			if (!(afterBrakeBadTime > 5f))
			{
				return;
			}
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.CarSignal.handBrake < 0.6f)
				{
					SoundPlayCtl.Instance.PlaySound("当车辆制动失效时,未停车", 100f);
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("刹车失灵项目结束");
					itemStep = 99;
				}
				else
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("刹车失灵项目结束");
					itemStep = 99;
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
			{
				if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake < 0.6f)
				{
					SoundPlayCtl.Instance.PlaySound("当车辆制动失效时,未停车", 100f);
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("刹车失灵项目结束");
					itemStep = 99;
				}
				else
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("刹车失灵项目结束");
					itemStep = 99;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType != 611)
			{
				return;
			}
			if (itemStep == 0 && other.name == "Start_Position10")
			{
				SoundPlayCtl.Instance.PlaySound("刹车失灵项目开始");
				itemStep = 1;
			}
			else if (itemStep == 1 && other.name == "BanMaXian")
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.footBrake < 0.05f)
				{
					SoundPlayCtl.Instance.PlaySound("斑马线前未踩脚刹", 100f);
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake < 0.05f)
				{
					SoundPlayCtl.Instance.PlaySound("斑马线前未踩脚刹", 100f);
				}
				itemStep = 2;
			}
		}
	}
}
