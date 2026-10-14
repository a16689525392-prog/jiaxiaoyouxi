using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class FrontCarFlash : YingJiXunLianCtlBase
	{
		private static FrontCarFlash instance;

		public GameObject InstanceCar;

		public GameObject InstanceStopLine;

		private bool isStopCar;

		private bool isSafeDistence = true;

		private RaycastHit frontCarFlashHitInfo;

		private float flashTime;

		private float stopTime;

		public static FrontCarFlash Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (itemStep == 2 && ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.footBrake > 0.6f && CarState.Instance.CurSpeed < 1f)
			{
				isStopCar = true;
			}
			else if (itemStep == 2 && (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.6f && DriveHistoryPlay.Instance.curCalculateSpeedH < 1f)
			{
				isStopCar = true;
			}
			if (isStopCar && itemStep == 2)
			{
				stopTime += Time.deltaTime;
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out frontCarFlashHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out frontCarFlashHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out frontCarFlashHitInfo, 8)) && frontCarFlashHitInfo.collider.name == "isOpenLight")
				{
					if (stopTime > 3f && ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.turnLight != 3)
					{
						SoundPlayCtl.Instance.PlaySound("停车后,未开危险警示灯提醒其他车辆", 100f);
						isStopCar = false;
						itemStep = 3;
					}
					else if (stopTime > 3f && (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 3)
					{
						SoundPlayCtl.Instance.PlaySound("停车后,未开危险警示灯提醒其他车辆", 100f);
						isStopCar = false;
						itemStep = 3;
					}
				}
				if (stopTime > 5f)
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("前车双闪项目结束");
					itemStep = 99;
				}
			}
			if ((!isStopCar && itemStep == 3) || (itemStep == 4 && !isSafeDistence))
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("前车双闪项目结束");
				itemStep = 99;
			}
			if (itemStep == 2 || itemStep == 3)
			{
				flashTime += Time.deltaTime;
				if (flashTime > 0f && flashTime < 0.6f)
				{
					InstanceStopLine.gameObject.SetActive(value: true);
				}
				else if (flashTime >= 0.6f && flashTime <= 1.2f)
				{
					InstanceStopLine.gameObject.SetActive(value: false);
				}
				else
				{
					flashTime = 0f;
				}
			}
			if (itemStep == 3 || itemStep == 4)
			{
				InstanceStopLine.gameObject.SetActive(value: false);
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType == 608)
			{
				if (itemStep == 0 && other.name == "Start_Position7")
				{
					InstanceCar = Object.Instantiate(Resources.Load("FlashCar")) as GameObject;
					GameObject gameObject = GameObject.Find("CarPostion");
					InstanceCar.transform.parent = gameObject.transform;
					InstanceCar.transform.localPosition = new Vector3(0f, 0f, 0f);
					InstanceCar.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
					SoundPlayCtl.Instance.PlaySound("前车双闪项目开始");
					itemStep = 1;
				}
				else if (itemStep == 1 && other.name == "OpenLight")
				{
					InstanceStopLine = Object.Instantiate(Resources.Load("AppearPerson/CarStopLine")) as GameObject;
					GameObject gameObject2 = GameObject.Find("StopCar");
					InstanceStopLine.transform.parent = gameObject2.transform;
					InstanceStopLine.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
					InstanceStopLine.transform.localPosition = new Vector3(0f, 0f, 0f);
					InstanceStopLine.transform.localScale = new Vector3(0.01f, 0.0005f, 0.01f);
					itemStep = 2;
				}
				else if (itemStep != 4 && isSafeDistence && other.name == "StopCar")
				{
					SoundPlayCtl.Instance.PlaySound("未与前车保持安全车距", 100f);
					isSafeDistence = false;
					itemStep = 4;
				}
				else if (itemStep != 99 && other.name == "End")
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("前车双闪项目结束");
					itemStep = 99;
				}
			}
		}
	}
}
