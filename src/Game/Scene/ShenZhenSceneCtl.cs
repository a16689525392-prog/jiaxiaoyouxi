using System;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class ShenZhenSceneCtl : TrainSceneBase
	{
		public static ShenZhenSceneCtl instance;

		public int itemStep;

		private RaycastHit shenZhenHitInfo;

		private bool isAidRoad;

		private bool isRightRoad;

		private bool isErrorRoad;

		public GameObject ElectricCarandMan;

		public GameObject People;

		public GameObject AiCar;

		private float SteerWheelTime;

		public float SteerWheelVaule;

		public bool DefenseTestElectricCar_Oil;

		public bool DefenseTestElectricCar_Footbreak;

		public bool DefenseTestElectricCar_Horn;

		public bool DefenseTestElectricCar_SteerWheel;

		public bool DefenseTestPeople_Oil;

		public bool DefenseTestPeople_Footbreak;

		public bool DefenseTestPeople_Horn;

		public bool DefenseTestPeople_SteerWheel;

		public bool DefenseTestAiCar_Oil;

		public bool DefenseTestAiCar_Footbreak;

		public bool DefenseTestAiCar_SteerWheel;

		public GameObject InstanceElectricCarandMan;

		public GameObject InstancePeople;

		public GameObject InstanceAiCar;

		protected override void Start()
		{
			base.Start();
			instance = this;
			ColliderDetection colliderDetection = ColliderDetection.instance;
			colliderDetection.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Combine(colliderDetection.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			base.ResetAction = (UnityAction)Delegate.Combine(base.ResetAction, new UnityAction(TrainSceneReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			SteerWheelVaule = CarState.Instance.CarSignal.steerWheel;
		}

		private void FixedUpdate()
		{
			if (ViewJumpCtl.Instance.itemType == 706)
			{
				SteerWheelTime += Time.deltaTime;
				if (SteerWheelTime > 0.2f)
				{
					SteerWheelVaule = CarState.Instance.CarSignal.steerWheel;
					SteerWheelTime = 0f;
				}
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel - SteerWheelVaule) > 0.15f)
				{
					SoundPlayCtl.Instance.PlaySound("行驶中猛转方向盘", 5f);
				}
				if (itemStep == 3 && InstanceAiCar != null && InstanceAiCar.transform.localPosition.z > -0.955f)
				{
					InstanceAiCar.transform.localPosition -= new Vector3(0f, 0f, 0.0025f);
				}
			}
			if (ViewJumpCtl.Instance.itemType != 405)
			{
				return;
			}
			if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out shenZhenHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out shenZhenHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out shenZhenHitInfo, 8))
			{
				if (shenZhenHitInfo.collider.name == "AidRoad" && !isAidRoad)
				{
					SoundPlayCtl.Instance.PlaySound("行驶中,占用应急车道", 100f);
					isAidRoad = true;
				}
				if (shenZhenHitInfo.collider.name == "Road0" && !isRightRoad)
				{
					isRightRoad = true;
					isErrorRoad = false;
				}
				if (shenZhenHitInfo.collider.name == "Road1" && !isErrorRoad)
				{
					SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
					isErrorRoad = true;
					isRightRoad = false;
				}
			}
			if (itemStep == 1)
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.horn == 1)
				{
					SoundPlayCtl.Instance.PlaySound("违禁鸣喇叭", 100f);
					itemStep = 2;
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn == 1)
				{
					SoundPlayCtl.Instance.PlaySound("违禁鸣喇叭", 100f);
					itemStep = 2;
				}
			}
			else if (itemStep == 3)
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.horn == 1)
				{
					SoundPlayCtl.Instance.PlaySound("违禁鸣喇叭", 100f);
					itemStep = 4;
				}
				else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn == 1)
				{
					SoundPlayCtl.Instance.PlaySound("违禁鸣喇叭", 100f);
					itemStep = 4;
				}
			}
		}

		private void TriggerAreaEvent(Collider other)
		{
			if (ViewJumpCtl.Instance.itemType == 706)
			{
				if (itemStep == 0 && other.name == "ElectricCarandMan_Move")
				{
					InstanceElectricCarandMan = UnityEngine.Object.Instantiate(Resources.Load("AppearPerson/ElectricCarandMan")) as GameObject;
					GameObject gameObject = GameObject.Find("ElectricCarandMan_StartPosition");
					InstanceElectricCarandMan.transform.parent = gameObject.transform;
					InstanceElectricCarandMan.transform.localPosition = new Vector3(0f, 0.0036f, 0f);
					InstanceElectricCarandMan.transform.localEulerAngles = new Vector3(-90f, 0f, 180f);
					InstanceElectricCarandMan.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
					ElectricCarandMan.SetActive(value: false);
					itemStep++;
				}
				if (itemStep == 1 && other.name == "People_Move")
				{
					InstancePeople = UnityEngine.Object.Instantiate(Resources.Load("AppearPerson/People")) as GameObject;
					GameObject gameObject2 = GameObject.Find("People_StartPosition");
					InstancePeople.transform.parent = gameObject2.transform;
					InstancePeople.transform.localPosition = new Vector3(0.0024f, -0.007f, 0f);
					InstancePeople.transform.localEulerAngles = new Vector3(0f, 183.261f, 0f);
					InstancePeople.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
					People.SetActive(value: false);
					itemStep++;
				}
				if (itemStep == 2 && other.name == "Car_Move")
				{
					InstanceAiCar = UnityEngine.Object.Instantiate(Resources.Load("AppearPerson/NightTestAICar")) as GameObject;
					GameObject gameObject3 = GameObject.Find("Car_StartPosition");
					InstanceAiCar.transform.parent = gameObject3.transform;
					InstanceAiCar.transform.localPosition = new Vector3(0f, -0.0018f, 0f);
					InstanceAiCar.transform.localEulerAngles = new Vector3(0f, -180f, 0f);
					InstanceAiCar.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
					AiCar.SetActive(value: false);
					itemStep++;
				}
			}
			if (ViewJumpCtl.Instance.itemType == 405)
			{
				if (itemStep == 0 && other.name == "NoHorn")
				{
					itemStep = 1;
				}
				else if (itemStep != 2 && other.name == "CanHorn")
				{
					itemStep = 2;
				}
				else if (itemStep != 3 && other.name == "NoHorn")
				{
					itemStep = 3;
				}
				else if (itemStep != 0 && other.name == "CanHorn1")
				{
					itemStep = 0;
				}
			}
			if (other.tag == "AI_People")
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				UnityEngine.Object.Destroy(other.transform.parent.parent.gameObject);
			}
		}

		private void ResetTimePlay()
		{
			isAidRoad = false;
			isRightRoad = false;
			isErrorRoad = false;
			itemStep = 0;
			DefenseTestElectricCar_Oil = false;
			DefenseTestElectricCar_Footbreak = false;
			DefenseTestElectricCar_Horn = false;
			DefenseTestElectricCar_SteerWheel = false;
			DefenseTestPeople_Oil = false;
			DefenseTestPeople_Footbreak = false;
			DefenseTestPeople_Horn = false;
			DefenseTestPeople_SteerWheel = false;
			DefenseTestAiCar_Oil = false;
			DefenseTestAiCar_Footbreak = false;
			DefenseTestAiCar_SteerWheel = false;
			if (InstanceElectricCarandMan != null)
			{
				UnityEngine.Object.Destroy(InstanceElectricCarandMan);
			}
			ElectricCarandMan.SetActive(value: true);
			if (InstancePeople != null)
			{
				UnityEngine.Object.Destroy(InstancePeople);
			}
			People.SetActive(value: true);
			if (InstanceAiCar != null)
			{
				UnityEngine.Object.Destroy(InstanceAiCar);
			}
			AiCar.SetActive(value: true);
		}

		private void TrainSceneReset()
		{
			isAidRoad = false;
			isRightRoad = false;
			isErrorRoad = false;
			itemStep = 0;
			DefenseTestElectricCar_Oil = false;
			DefenseTestElectricCar_Footbreak = false;
			DefenseTestElectricCar_Horn = false;
			DefenseTestElectricCar_SteerWheel = false;
			DefenseTestPeople_Oil = false;
			DefenseTestPeople_Footbreak = false;
			DefenseTestPeople_Horn = false;
			DefenseTestPeople_SteerWheel = false;
			DefenseTestAiCar_Oil = false;
			DefenseTestAiCar_Footbreak = false;
			DefenseTestAiCar_SteerWheel = false;
			if (InstanceElectricCarandMan != null)
			{
				UnityEngine.Object.Destroy(InstanceElectricCarandMan);
			}
			ElectricCarandMan.SetActive(value: true);
			if (InstancePeople != null)
			{
				UnityEngine.Object.Destroy(InstancePeople);
			}
			People.SetActive(value: true);
			if (InstanceAiCar != null)
			{
				UnityEngine.Object.Destroy(InstanceAiCar);
			}
			AiCar.SetActive(value: true);
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			ColliderDetection colliderDetection = ColliderDetection.instance;
			colliderDetection.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Remove(colliderDetection.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainSceneReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			SoundPlayCtl.Instance.ClearClip();
		}
	}
}
