using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class AllYJXunLian : YingJiXunLianCtlBase
	{
		private static AllYJXunLian instance;

		public GameObject InstanceGo;

		private RaycastHit HitInfo;

		private bool IsInstanceGo;

		public int Itemindex;

		private float stopTime;

		private float notstopTime;

		private bool isStopCar;

		private bool isSafeDistence = true;

		private bool isLowSpeed;

		private float NoOilTime;

		private float boomTime;

		private bool FastFootBrake;

		public static AllYJXunLian Instance => instance;

		private void Awake()
		{
			instance = this;
			Itemindex = 0;
			IsInstanceGo = false;
			GameObject.Find("DriveCamera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
		}

		private void Update()
		{
			if (SceneMenuView.KaoShiReset)
			{
				Itemindex = 0;
				GameObject gameObject = GameObject.Find("InstanceGameobject");
				if (gameObject.transform.childCount > 0)
				{
					for (int i = 0; i < gameObject.transform.childCount; i++)
					{
						Object.Destroy(gameObject.transform.GetChild(i).gameObject);
					}
				}
				GameObject gameObject2 = GameObject.Find("Fire(Clone)");
				if ((bool)gameObject2)
				{
					Object.Destroy(gameObject2);
				}
				IsInstanceGo = false;
				GameObject.Find("DriveCamera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
				GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
				SceneMenuView.KaoShiReset = false;
			}
			if (itemStep == 99)
			{
				SoundPlayCtl.Instance.PlaySound("请继续行驶");
				Itemindex++;
				itemStep = 0;
				stopTime = 0f;
			}
			if (Itemindex == 1)
			{
				if (itemStep == 2)
				{
					if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH < 0.5f)
					{
						stopTime += Time.deltaTime;
						if (InstanceGo != null && stopTime > 5f)
						{
							Object.Destroy(InstanceGo);
							SoundPlayCtl.Instance.PlaySound("前方落石项目结束");
							itemStep = 99;
						}
					}
					if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed < 0.5f)
					{
						stopTime += Time.deltaTime;
						if (stopTime > 5f)
						{
							SoundPlayCtl.Instance.PlaySound("前方落石项目结束");
							itemStep = 99;
						}
					}
				}
				else if (itemStep == 3)
				{
					notstopTime += Time.deltaTime;
					if (notstopTime > 4f)
					{
						SoundPlayCtl.Instance.PlaySound("前方落石项目结束");
						itemStep = 99;
					}
				}
			}
			else if (Itemindex == 2)
			{
				if (ViewJumpCtl.Instance.modelType == 2 && InstanceGo != null && itemStep == 0)
				{
					Object.Destroy(InstanceGo);
					itemStep++;
				}
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && itemStep == 0)
				{
					itemStep++;
				}
				if (itemStep == 3 && ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.footBrake > 0.6f && CarState.Instance.CurSpeed < 1f)
				{
					isStopCar = true;
				}
				if (isStopCar && itemStep == 3)
				{
					stopTime += Time.deltaTime;
					if (stopTime > 3f && ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.turnLight != 3)
					{
						SoundPlayCtl.Instance.PlaySound("停车后,未开危险警示灯提醒其他车辆", 100f);
						isStopCar = false;
						itemStep = 4;
					}
					if (stopTime > 5f)
					{
						SoundPlayCtl.Instance.PlaySound("前车双闪项目结束");
						itemStep = 99;
					}
				}
				if ((!isStopCar && itemStep == 4) || (itemStep == 5 && !isSafeDistence))
				{
					SoundPlayCtl.Instance.PlaySound("前车双闪项目结束");
					itemStep = 99;
				}
			}
			else if (Itemindex == 3)
			{
				if (InstanceGo != null && itemStep == 0)
				{
					Object.Destroy(InstanceGo);
					itemStep++;
				}
			}
			else
			{
				if (Itemindex == 4)
				{
					return;
				}
				if (Itemindex == 5)
				{
					if (itemStep > 0 && itemStep != 99)
					{
						CarCtl.IsNoOil = true;
						if (CarState.Instance.FireOn)
						{
							CarState.Instance.FireOn = false;
						}
						if (CarState.Instance.carRigibody.velocity.magnitude * 3.6f > 5f)
						{
							CarState.Instance.carRigibody.velocity -= new Vector3(0.01f, 0.01f, 0.01f);
						}
					}
					if (itemStep == 1)
					{
						NoOilTime += Time.deltaTime;
						if (CarState.Instance.CarSignal.turnLight == 3)
						{
							itemStep++;
							NoOilTime = 0f;
						}
						if (NoOilTime > 5f && CarState.Instance.CarSignal.turnLight != 3)
						{
							SoundPlayCtl.Instance.PlaySound("没油熄火时,未及时打开双闪", 100f);
							itemStep++;
							NoOilTime = 0f;
						}
					}
					if (itemStep == 2)
					{
						NoOilTime += Time.deltaTime;
						if ((!ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out HitInfo, 8) || !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[1].position, out HitInfo, 8)) && NoOilTime >= 20f)
						{
							SoundPlayCtl.Instance.PlaySound("未进行靠边停车操作", 100f);
							itemStep = 99;
							SoundPlayCtl.Instance.PlaySound("中途没油项目结束");
							CarCtl.IsNoOil = false;
						}
						if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out HitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[1].position, out HitInfo, 8) && NoOilTime < 20f)
						{
							itemStep++;
							NoOilTime = 0f;
						}
					}
					if (itemStep != 3)
					{
						return;
					}
					NoOilTime += Time.deltaTime;
					if (NoOilTime > 10f && CarState.Instance.CurSpeed >= 0.1f)
					{
						SoundPlayCtl.Instance.PlaySound("未进行靠边停车操作", 100f);
						itemStep = 99;
						SoundPlayCtl.Instance.PlaySound("中途没油项目结束");
						CarCtl.IsNoOil = false;
					}
					if (CarState.Instance.CurSpeed < 0.1f)
					{
						if (CarState.Instance.CarSignal.handBrake > 0.7f && NoOilTime < 12f)
						{
							itemStep = 99;
							SoundPlayCtl.Instance.PlaySound("中途没油项目结束");
							CarCtl.IsNoOil = false;
						}
						else if (NoOilTime > 12f)
						{
							SoundPlayCtl.Instance.PlaySound("停车后未拉紧手刹", 100f);
							itemStep = 99;
							SoundPlayCtl.Instance.PlaySound("中途没油项目结束");
							CarCtl.IsNoOil = false;
						}
					}
				}
				else if (Itemindex == 6)
				{
					CarCtl.IsNoOil = false;
					if (itemStep >= 2 && itemStep != 99 && (!ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out HitInfo, 8) || !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[1].position, out HitInfo, 8)) && CarState.Instance.CarSignal.footBrake > 0.6f && !FastFootBrake)
					{
						SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,刹车过急", 100f);
						FastFootBrake = true;
					}
					if (itemStep == 2)
					{
						if (CarState.Instance.CarSignal.footBrake < 0.1f)
						{
							boomTime += Time.deltaTime;
							if (boomTime > 5f)
							{
								SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,未轻踩脚刹", 100f);
								itemStep++;
								boomTime = 0f;
							}
						}
						else if (CarState.Instance.CarSignal.footBrake >= 0.1f && CarState.Instance.CarSignal.footBrake < 0.6f)
						{
							itemStep++;
							boomTime = 0f;
						}
					}
					if (itemStep == 3)
					{
						boomTime += Time.deltaTime;
						if (!ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out HitInfo, 8) || !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[1].position, out HitInfo, 8))
						{
							boomTime += Time.deltaTime;
							if (boomTime >= 6f)
							{
								if (ViewJumpCtl.Instance.modelType != 0 && ViewJumpCtl.Instance.modelType != 5)
								{
									SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,未减速靠边停车", 100f);
								}
								itemStep++;
								boomTime = 0f;
							}
						}
					}
					if (itemStep < 3 || !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out HitInfo, 8) || !ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[1].position, out HitInfo, 8) || !(CarState.Instance.CurSpeed < 0.1f))
					{
						return;
					}
					boomTime += Time.deltaTime;
					if (boomTime >= 6f)
					{
						if (CarState.Instance.CarSignal.handBrake < 0.7f || CarState.Instance.CarSignal.turnLight != 3)
						{
							SoundPlayCtl.Instance.PlaySound("当车辆爆胎时,靠边停车后,未在规定时间内拉手刹或未开启双闪灯", 100f);
							itemStep = 99;
							SoundPlayCtl.Instance.PlaySound("突然爆胎项目结束");
						}
					}
					else if (CarState.Instance.CarSignal.handBrake > 0.7f && CarState.Instance.CarSignal.turnLight == 3)
					{
						SoundPlayCtl.Instance.PlaySound("突然爆胎项目结束");
						itemStep = 99;
					}
				}
				else if (Itemindex == 7)
				{
					if (itemStep == 2)
					{
						stopTime += Time.deltaTime;
						if (stopTime > 3f)
						{
							if (CarState.Instance.CurSpeed > 10f)
							{
								SoundPlayCtl.Instance.PlaySound("疲劳驾驶时,未减速行驶", 100f);
								stopTime = 0f;
								itemStep++;
							}
							else
							{
								stopTime = 0f;
								itemStep++;
							}
						}
					}
					if (itemStep != 3)
					{
						return;
					}
					stopTime += Time.deltaTime;
					if (stopTime > 10f)
					{
						if (CarState.Instance.CurSpeed > 0.1f)
						{
							SoundPlayCtl.Instance.PlaySound("疲劳驾驶时,未将车辆停靠在最近可以停车处", 100f);
							stopTime = 0f;
							itemStep = 99;
							SoundPlayCtl.Instance.PlaySound("疲劳驾驶项目结束");
							GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
							GameObject.Find("DriveCamera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
						}
						else
						{
							stopTime = 0f;
							itemStep = 99;
							SoundPlayCtl.Instance.PlaySound("疲劳驾驶项目结束");
							GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
							GameObject.Find("DriveCamera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
						}
					}
				}
				else
				{
					if (Itemindex == 8 || Itemindex != 9 || itemStep != 2)
					{
						return;
					}
					stopTime += Time.deltaTime;
					if (stopTime > 5f)
					{
						if (CarState.Instance.CurSpeed > 0.1f)
						{
							SoundPlayCtl.Instance.PlaySound("当车辆着火时,未及时停车", 100f);
							SoundPlayCtl.Instance.PlaySound("车身起火项目结束");
							Object.Destroy(InstanceGo, 1f);
							itemStep = 99;
						}
						else
						{
							SoundPlayCtl.Instance.PlaySound("车身起火项目结束");
							Object.Destroy(InstanceGo, 1f);
							itemStep = 99;
						}
					}
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (Itemindex == 0 && other.name == "Start_Position_FrontFallStone")
			{
				Itemindex++;
			}
			if (Itemindex == 1)
			{
				if (other.name == "StartStopCar_Position_FrontFallStone" && !IsInstanceGo)
				{
					InstanceGo = Object.Instantiate(Resources.Load("AppearPerson/Stone")) as GameObject;
					GameObject gameObject = GameObject.Find("InstanceGameobject");
					InstanceGo.transform.parent = gameObject.transform;
					InstanceGo.transform.localPosition = new Vector3(-2927.5f, -2.3f, -1.6f);
					IsInstanceGo = true;
				}
				if (itemStep == 0 && other.name == "Start_Position_FrontFallStone")
				{
					SoundPlayCtl.Instance.PlaySound("前方落石项目开始");
					itemStep = 1;
				}
				else if (itemStep == 1 && other.name == "StartStopCar_Position_FrontFallStone")
				{
					itemStep = 2;
				}
				else if (itemStep != 3 && other.name == "StopCar_Position_FrontFallStone")
				{
					if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CurSpeed > 1f)
					{
						SoundPlayCtl.Instance.PlaySound("前方道路遇到落石时,未减速至停车", 100f);
						itemStep = 3;
					}
				}
				else if (itemStep != 99 && other.name == "End_Position_FrontFallStone")
				{
					SoundPlayCtl.Instance.PlaySound("前方落石项目结束");
					itemStep = 99;
				}
			}
			else if (Itemindex == 2)
			{
				if (itemStep == 1 && other.name == "Start_Position_FrontCarFlash")
				{
					InstanceGo = Object.Instantiate(Resources.Load("FlashCar")) as GameObject;
					GameObject gameObject2 = GameObject.Find("InstanceGameobject");
					InstanceGo.transform.parent = gameObject2.transform;
					InstanceGo.transform.localPosition = new Vector3(-3756f, -113.7f, -846.5f);
					InstanceGo.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
					IsInstanceGo = true;
					SoundPlayCtl.Instance.PlaySound("前车双闪项目开始");
					itemStep++;
				}
				else if (itemStep == 2 && other.name == "StartStopCar_Position_FrontCarFlash")
				{
					itemStep++;
				}
				else if (itemStep != 5 && isSafeDistence && other.name == "StopCar_Position_FrontCarFlash")
				{
					if (ViewJumpCtl.Instance.modelType != 0 && ViewJumpCtl.Instance.modelType != 5)
					{
						SoundPlayCtl.Instance.PlaySound("未与前车保持安全车距", 100f);
					}
					isSafeDistence = false;
					itemStep = 5;
				}
				else if (itemStep != 99 && other.name == "End_Position_FrontCarFlash")
				{
					SoundPlayCtl.Instance.PlaySound("前车双闪项目结束");
					itemStep = 99;
				}
			}
			else if (Itemindex == 3)
			{
				if (itemStep == 1 && other.name == "Start_Position_KengWaRoad")
				{
					SoundPlayCtl.Instance.PlaySound("坑洼路面项目开始");
					itemStep++;
				}
				else if (itemStep == 2 && other.name == "SlowDown_KengWaRoad1")
				{
					if (CarState.Instance.CurSpeed > 10f && !isLowSpeed)
					{
						SoundPlayCtl.Instance.PlaySound("通过坑洼路面时,未轻踩脚刹低速平稳通过", 100f);
						isLowSpeed = true;
					}
					itemStep++;
				}
				else if (itemStep == 3 && other.name == "SlowDown_KengWaRoad2")
				{
					if (CarState.Instance.CurSpeed > 10f && !isLowSpeed)
					{
						SoundPlayCtl.Instance.PlaySound("通过坑洼路面时,未轻踩脚刹低速平稳通过", 100f);
						isLowSpeed = true;
					}
					itemStep++;
				}
				else if (itemStep == 4 && other.name == "SlowDown_KengWaRoad3")
				{
					if (CarState.Instance.CurSpeed > 10f && !isLowSpeed)
					{
						SoundPlayCtl.Instance.PlaySound("通过坑洼路面时,未轻踩脚刹低速平稳通过", 100f);
						isLowSpeed = true;
					}
					itemStep++;
				}
				else if (itemStep != 99 && other.name == "End_KengWaRoad")
				{
					SoundPlayCtl.Instance.PlaySound("坑洼路面项目结束");
					itemStep = 99;
				}
			}
			else if (Itemindex == 4)
			{
				if (itemStep == 0 && other.name == "Start_Position_AppearPerson")
				{
					SoundPlayCtl.Instance.PlaySound("出现行人项目开始");
					itemStep = 1;
				}
				else if (itemStep == 1 && other.name == "InstancePeople_AppearPerson")
				{
					InstanceGo = Object.Instantiate(Resources.Load("AppearPerson/People")) as GameObject;
					GameObject gameObject3 = GameObject.Find("InstanceGameobject");
					InstanceGo.transform.parent = gameObject3.transform;
					InstanceGo.transform.localPosition = new Vector3(-3168f, -113.7f, -878.6f);
					InstanceGo.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					IsInstanceGo = true;
					itemStep = 2;
				}
				else if (itemStep == 2 && other.name == "CarSlowPosition_AppearPerson")
				{
					if (ViewJumpCtl.Instance.modelType == 2)
					{
						if (CarState.Instance.CurSpeed > 2f && (People.Instance.MoveSpeed == 1.5f || People.Instance.MoveSpeed == 1.6f || People.Instance.MoveSpeed == 1.8f || People.Instance.MoveSpeed == 2f))
						{
							SoundPlayCtl.Instance.PlaySound("遇行人未减速停车", 100f);
							itemStep = 3;
						}
						if (People.Instance.MoveSpeed == 0f)
						{
							itemStep = 3;
						}
					}
				}
				else if (itemStep == 3 && (other.name == "child01_f_mediumpoly" || other.name == "sportive03_m_mediumpoly" || other.name == "cman0007FBXASC045tpFBXASC046Mesh01"))
				{
					Object.Destroy(InstanceGo);
					SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
					itemStep = 4;
				}
				else if (itemStep != 99 && other.name == "End_AppearPerson")
				{
					SoundPlayCtl.Instance.PlaySound("出现行人项目结束");
					itemStep = 99;
				}
			}
			else if (Itemindex == 5)
			{
				if (itemStep == 0 && other.name == "TiShiNoOil")
				{
					if (InstanceGo != null)
					{
						Object.Destroy(InstanceGo);
					}
					SoundPlayCtl.Instance.PlaySound("中途没油");
					itemStep++;
				}
			}
			else if (Itemindex == 6)
			{
				if (itemStep == 0 && other.name == "Start_Position_BoomWheel")
				{
					SoundPlayCtl.Instance.PlaySound("突然爆胎项目开始");
					itemStep++;
				}
				else if (itemStep == 1 && other.name == "Boom")
				{
					AudioSourceCtl.Instance.PlayAudio("TirePuncture");
					itemStep = 2;
				}
				else if (itemStep != 99 && other.name == "End_BoolWheel")
				{
					SoundPlayCtl.Instance.PlaySound("突然爆胎项目结束");
					itemStep = 99;
				}
			}
			else if (Itemindex == 7)
			{
				if (itemStep == 0 && other.name == "Start_Position_TiredDrive")
				{
					GameObject.Find("DriveCamera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
					SoundPlayCtl.Instance.PlaySound("疲劳驾驶项目开始");
					itemStep++;
				}
				else if (itemStep == 1 && other.name == "StartMohu")
				{
					GameObject.Find("DriveCamera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = true;
					stopTime = 0f;
					itemStep++;
				}
				else if (itemStep != 99 && other.name == "End_TiredDrive")
				{
					stopTime = 0f;
					itemStep = 99;
					SoundPlayCtl.Instance.PlaySound("疲劳驾驶项目结束");
					GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
					GameObject.Find("DriveCamera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
				}
			}
			else if (Itemindex == 8)
			{
				if (itemStep == 0 && other.name == "Start_Position_BusBlindArea")
				{
					SoundPlayCtl.Instance.PlaySound("通过公交盲区项目开始");
					itemStep++;
				}
				else if (itemStep == 1 && other.name == "InstancePeople_AllYJXunLian")
				{
					InstanceGo = Object.Instantiate(Resources.Load("AppearPerson/People")) as GameObject;
					GameObject gameObject4 = GameObject.Find("InstanceGameobject");
					InstanceGo.transform.parent = gameObject4.transform;
					InstanceGo.transform.localPosition = new Vector3(-3041.92f, -113.666f, -61.78f);
					InstanceGo.transform.localEulerAngles = new Vector3(0f, -90.56f, 0f);
					if (CarState.Instance.CurSpeed >= 6f)
					{
						SoundPlayCtl.Instance.PlaySound("通过公交盲区未减速行驶", 100f);
					}
					itemStep = 2;
				}
				else if (itemStep != 99 && other.name == "EndBusBlindArea_AllYJXunLian")
				{
					SoundPlayCtl.Instance.PlaySound("通过公交盲区项目结束");
					itemStep = 99;
				}
				if (itemStep == 2 && (other.name == "child01_f_mediumpoly" || other.name == "sportive03_m_mediumpoly" || other.name == "cman0007FBXASC045tpFBXASC046Mesh01"))
				{
					Object.Destroy(InstanceGo);
					SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
					itemStep++;
				}
			}
			else if (Itemindex == 9)
			{
				stopTime = 0f;
				if (itemStep == 0 && other.name == "Start_Position_CarBodyFire")
				{
					SoundPlayCtl.Instance.PlaySound("车身起火项目开始");
					stopTime = 0f;
					itemStep = 1;
				}
				else if (itemStep == 1 && other.name == "Fire")
				{
					InstanceGo = Object.Instantiate(Resources.Load("CarBodyFire/Prefabs/Fire")) as GameObject;
					GameObject gameObject5 = GameObject.Find("Frame");
					InstanceGo.transform.parent = gameObject5.transform;
					InstanceGo.transform.localPosition = new Vector3(0f, -1f, 2f);
					InstanceGo.transform.localScale = new Vector3(10f, 10f, 5f);
					InstanceGo.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
					itemStep = 2;
				}
				else if (itemStep != 99 && other.name == "End_CarBodyFire")
				{
					SoundPlayCtl.Instance.PlaySound("车身起火项目结束");
					Object.Destroy(InstanceGo);
					itemStep = 99;
				}
			}
		}
	}
}
