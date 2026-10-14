using System;
using System.IO;
using System.Xml;
using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class TrainSceneBase : SceneBase
	{
		public static TrainSceneBase BaseInstance;

		public UnityAction ResetTime;

		[HideInInspector]
		public GameObject Car;

		[HideInInspector]
		public GameObject moniCar;

		public int recordCurYanShiIndex;

		[HideInInspector]
		public GameObject mainCamView;

		private float errorIntervalTime = 10f;

		private float errorIntervalTime2 = 10f;

		private float errorIntervalTime3 = 3f;

		private float errorIntervalTime4 = 10f;

		private float errorIntervalTime5 = 10f;

		private bool isShake = true;

		private bool isHit;

		private bool preFireOn;

		private int preGear;

		private float ActiveTime;

		private AudioSource footBrakeAudio;

		private float GearChangeTime;

		public UnityAction ResetAction { get; set; }

		protected override void Awake()
		{
			base.Awake();
			BaseInstance = this;
			ResetAction = (UnityAction)Delegate.Combine(ResetAction, new UnityAction(TrainSceneReset));
			ResetTime = (UnityAction)Delegate.Combine(ResetTime, new UnityAction(SceneResetTime));
			ModelDetail();
			SkyBoxCtl.Instance.ChangeSkyBox(ViewJumpCtl.Instance.otherType);
		}

		private void ModelDetail()
		{
			switch (ViewJumpCtl.Instance.modelType)
			{
			case 1:
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang || ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
				{
					base.gameObject.AddComponent<CarDriveHistoryPlay>();
				}
				break;
			case 2:
				ResetAction = (UnityAction)Delegate.Combine(ResetAction, new UnityAction(CarReset));
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.FreeDriving || ViewJumpCtl.Instance.trainType == TrainType.DriveSkill || ViewJumpCtl.Instance.trainType == TrainType.CourseThree || ViewJumpCtl.Instance.trainType == TrainType.YingjiXunlian || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang)
				{
					base.gameObject.AddComponent<DriveHistoryRecord>();
				}
				break;
			case 0:
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.CourseThree || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang || ViewJumpCtl.Instance.trainType == TrainType.YingjiXunlian)
				{
					ResetAction = (UnityAction)Delegate.Combine(ResetAction, new UnityAction(CarReset));
				}
				base.gameObject.AddComponent<DriveHistoryPlay>();
				break;
			case 5:
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.FreeDriving || ViewJumpCtl.Instance.trainType == TrainType.DriveSkill || ViewJumpCtl.Instance.trainType == TrainType.CourseThree || ViewJumpCtl.Instance.trainType == TrainType.YingjiXunlian || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang)
				{
					ResetAction = (UnityAction)Delegate.Combine(ResetAction, new UnityAction(CarReset));
				}
				base.gameObject.AddComponent<DriveHistoryPlay>();
				break;
			case 3:
			case 4:
				break;
			}
		}

		protected override void Start()
		{
			base.Start();
			CreateCar();
			ColliderDetection instance = ColliderDetection.instance;
			instance.BumpGuardBarCallback = (UnityAction<Collision>)Delegate.Combine(instance.BumpGuardBarCallback, new UnityAction<Collision>(GuardBarCollisionEvent));
			ColliderDetection instance2 = ColliderDetection.instance;
			instance2.BumpGanZiCallback = (UnityAction<Collision>)Delegate.Combine(instance2.BumpGanZiCallback, new UnityAction<Collision>(GanZiCollisionEvent));
			ColliderDetection instance3 = ColliderDetection.instance;
			instance3.PressEdgeLineCallback = (UnityAction<RaycastHit>)Delegate.Combine(instance3.PressEdgeLineCallback, new UnityAction<RaycastHit>(WheelHitEvent));
			ViewJumpCtl.Instance.EnterView(ViewType.SceneMenuView, isDestroyCurView: true, isSave: false);
			ViewJumpCtl.Instance.SetBackgroundActive(active: false);
			if (ViewJumpCtl.Instance.modelType == 2 && (ViewJumpCtl.Instance.trainType == TrainType.DriveSkill || ViewJumpCtl.Instance.trainType == TrainType.FreeDriving))
			{
				DriveInstructionView.Instance.GetTextInfo();
			}
			if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView", Vector3.zero);
			}
			if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView_DF", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.YongShi)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView_YongShi", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraViewDaBa", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.KaLuoLa)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView_KaLuoLa", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QiRuiE3)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView_QiRuiE3", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1121)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView_JieFang1121", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.TianJin)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView_TianJin", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1125)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView_JieFang1125", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView_KangMingSi", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.PiKa)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView_PiKa", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.AiLiSheManual || SettingInfoCtl.Instance.carType == CarType.AiLiSheAuto)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraView_AiLiShe", Vector3.zero);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QianYinChe)
			{
				mainCamView = ViewJumpCtl.Instance.CreateViewPrefab("NewMainCameraViewQianYinChe", Vector3.zero);
			}
			if (ViewJumpCtl.Instance.modelType == 1 && (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang || ViewJumpCtl.Instance.trainType == TrainType.CourseThree))
			{
				SoundPlayCtl.Instance.PlaySound("指导模式中,车辆会根据固定路线行驶,方向盘不会发挥作用,请根据右侧指导内容进行操作");
			}
			ResetAction = (UnityAction)Delegate.Combine(ResetAction, new UnityAction(ColliderDetection.instance.Reset));
			if (ViewJumpCtl.Instance.modelType == 5)
			{
				LiShiPlay();
			}
			if (ViewJumpCtl.Instance.modelType == 0 && ViewJumpCtl.Instance.trainType == TrainType.YingjiXunlian)
			{
				if (SettingInfoCtl.Instance.isManualGear)
				{
					StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "history" + SettingInfoCtl.Instance.isManualGear.ToString() + (int)SettingInfoCtl.Instance.carType + ViewJumpCtl.Instance.itemType + SettingInfoCtl.Instance.isCorrect.ToString()));
				}
				else if (!SettingInfoCtl.Instance.isManualGear)
				{
					StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "history" + (!SettingInfoCtl.Instance.isManualGear).ToString() + (int)SettingInfoCtl.Instance.carType + ViewJumpCtl.Instance.itemType + SettingInfoCtl.Instance.isCorrect.ToString()));
				}
			}
		}

		protected override void Update()
		{
			base.Update();
			CommonErrorJudge();
			errorIntervalTime += Time.deltaTime;
			errorIntervalTime2 += Time.deltaTime;
			errorIntervalTime3 += Time.deltaTime;
			errorIntervalTime4 += Time.deltaTime;
			errorIntervalTime5 += Time.deltaTime;
			if (SignalSourceCtl.Instance.GetCircuitBoardSignalType() != 1)
			{
				SignalSourceCtl.Instance.SetWriteBuffer(29, 192);
			}
		}

		protected void CreateCar()
		{
			Car = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(CarLoad.GetCarPath(SettingInfoCtl.Instance.carType)));
			Car.transform.position = SceneModelBase.Instance.GetStartPosition().position;
			if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
			{
				Car.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
			{
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo && SceneJumpCtl.Instance.CoursetwoName == CoursetwoScenesName.QingHu)
				{
					Car.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
				}
				else if ((ViewJumpCtl.Instance.trainType == TrainType.CourseThree || ViewJumpCtl.Instance.trainType == TrainType.NightLight) && SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
				{
					Car.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
				}
				else
				{
					Car.transform.localScale = Vector3.one;
				}
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
			{
				Car.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.TianJin)
			{
				Car.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
			{
				Car.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1121)
			{
				Car.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1125)
			{
				Car.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.YongShi)
			{
				Car.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QianYinChe)
			{
				Car.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			}
			Car.transform.eulerAngles = SceneModelBase.Instance.GetStartPosition().eulerAngles;
			SignalSourceCtl.Instance.SetWriteBuffer(29, 192);
			ReadMirrorDataXml();
		}

		private void TrainSceneReset()
		{
			DataCenter.Instance.score = 100f;
			SoundPlayCtl.Instance.SoundListReset();
			SceneTextShow.Instance.ResetNormalText();
			errorIntervalTime = 0f;
			errorIntervalTime2 = 0f;
			errorIntervalTime4 = 0f;
			errorIntervalTime5 = 0f;
			CarState.Instance.FireOn = false;
			if (ViewJumpCtl.Instance.itemType == 601)
			{
				UnityEngine.Object.Destroy(AppearPerson.Instance.InstancePeople);
			}
			if (ViewJumpCtl.Instance.itemType == 603)
			{
				AudioSourceCtl.Instance.StopAllAudio();
			}
			if (ViewJumpCtl.Instance.itemType == 608)
			{
				UnityEngine.Object.Destroy(FrontCarFlash.Instance.InstanceStopLine);
				UnityEngine.Object.Destroy(FrontCarFlash.Instance.InstanceCar);
			}
			if (ViewJumpCtl.Instance.itemType == 604)
			{
				UnityEngine.Object.Destroy(CarBodyFire.Instance.instanceFire);
			}
			if (ViewJumpCtl.Instance.itemType == 511 || ViewJumpCtl.Instance.itemType == 403)
			{
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople1);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople2);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople3);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople4);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople20);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople30);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople40);
			}
		}

		private void SceneResetTime()
		{
			if (ViewJumpCtl.Instance.modelType == 0)
			{
				if (ViewJumpCtl.Instance.trainType == TrainType.YingjiXunlian)
				{
					if (SettingInfoCtl.Instance.isManualGear)
					{
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "history" + SettingInfoCtl.Instance.isManualGear.ToString() + (int)SettingInfoCtl.Instance.carType + ViewJumpCtl.Instance.itemType + SettingInfoCtl.Instance.isCorrect.ToString()));
					}
					else if (!SettingInfoCtl.Instance.isManualGear)
					{
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "history" + (!SettingInfoCtl.Instance.isManualGear).ToString() + (int)SettingInfoCtl.Instance.carType + ViewJumpCtl.Instance.itemType + SettingInfoCtl.Instance.isCorrect.ToString()));
					}
				}
				else if (ViewJumpCtl.Instance.trainType != TrainType.CourseTwo && ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
				{
					StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "CourseThree"));
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 5)
			{
				LiShiPlay();
			}
			DataCenter.Instance.score = 100f;
			SoundPlayCtl.Instance.SoundListReset();
			SceneTextShow.Instance.ResetNormalText();
			errorIntervalTime = 0f;
			errorIntervalTime2 = 0f;
			errorIntervalTime4 = 0f;
			errorIntervalTime5 = 0f;
			CarState.Instance.FireOn = false;
			if (ViewJumpCtl.Instance.itemType == 601)
			{
				UnityEngine.Object.Destroy(AppearPerson.Instance.InstancePeople);
			}
			if (ViewJumpCtl.Instance.itemType == 603)
			{
				AudioSourceCtl.Instance.StopPlayAudio(DrunkDriving.Instance.alarmAudioSource);
			}
			if (ViewJumpCtl.Instance.itemType == 608)
			{
				UnityEngine.Object.Destroy(FrontCarFlash.Instance.InstanceStopLine);
				UnityEngine.Object.Destroy(FrontCarFlash.Instance.InstanceCar);
			}
			if (ViewJumpCtl.Instance.itemType == 604)
			{
				UnityEngine.Object.Destroy(CarBodyFire.Instance.instanceFire);
			}
			if (ViewJumpCtl.Instance.itemType == 511 || ViewJumpCtl.Instance.itemType == 403)
			{
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople1);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople2);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople3);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople4);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople20);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople30);
				UnityEngine.Object.Destroy(CityRoadSceneCtl.Instance.InstancePeople40);
			}
		}

		private void LiShiPlay()
		{
			string text = "";
			text = ((Application.platform == RuntimePlatform.WindowsPlayer) ? (Application.streamingAssetsPath + "/RecordFiles/history" + SettingInfoCtl.Instance.isManualGear.ToString() + (int)SettingInfoCtl.Instance.carType + ViewJumpCtl.Instance.itemType + DriveHistoryView.instance.Pindex + ".bytes") : ((Application.platform != RuntimePlatform.Android) ? (Application.streamingAssetsPath + "/RecordFiles/history" + SettingInfoCtl.Instance.isManualGear.ToString() + (int)SettingInfoCtl.Instance.carType + ViewJumpCtl.Instance.itemType + DriveHistoryView.instance.Pindex + ".bytes") : (Application.persistentDataPath + "/RecordFiles/history" + SettingInfoCtl.Instance.isManualGear.ToString() + (int)SettingInfoCtl.Instance.carType + ViewJumpCtl.Instance.itemType + DriveHistoryView.instance.Pindex + ".bytes")));
			StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryDriveFiles(text));
		}

		protected virtual void WheelHitEvent(RaycastHit hitInfo)
		{
			if (hitInfo.collider.gameObject.layer == 26 && errorIntervalTime5 > 10f && ViewJumpCtl.Instance.modelType != 1 && ViewJumpCtl.Instance.modelType != 0)
			{
				SoundPlayCtl.Instance.PlaySound("未按规定路线行驶", 100f);
				errorIntervalTime5 = 0f;
			}
			if (hitInfo.collider.gameObject.layer == 25 && errorIntervalTime4 > 10f && ViewJumpCtl.Instance.modelType != 1 && ViewJumpCtl.Instance.modelType != 0)
			{
				SoundPlayCtl.Instance.PlaySound("触压实线", 100f);
				errorIntervalTime4 = 0f;
			}
			if (hitInfo.collider.gameObject.layer == 10 && errorIntervalTime > 10f && ViewJumpCtl.Instance.modelType != 1 && ViewJumpCtl.Instance.modelType != 0)
			{
				SoundPlayCtl.Instance.PlaySound("触压道路边缘线", 100f);
				errorIntervalTime = 0f;
			}
			if (hitInfo.collider.gameObject.layer == 19)
			{
				if ((ViewJumpCtl.Instance.itemType == 15 || ViewJumpCtl.Instance.itemType == 5090) && errorIntervalTime > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("转弯过程中,车速控制不稳,车轮轧弯道中心线", 100f);
					errorIntervalTime = 0f;
				}
				if (ViewJumpCtl.Instance.itemType == 6 && errorIntervalTime > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("轧,碰,擦圆饼", 5f);
					errorIntervalTime = 0f;
				}
			}
		}

		protected virtual void GanZiCollisionEvent(Collision collision)
		{
			if (ViewJumpCtl.Instance.itemType == 9 && errorIntervalTime2 > 10f)
			{
				SoundPlayCtl.Instance.PlaySound("碰擦悬杆", 10f);
				AudioSourceCtl.Instance.PlayAudio("CarHit");
				errorIntervalTime2 = 0f;
			}
			if (ViewJumpCtl.Instance.itemType == 5 && errorIntervalTime2 > 10f)
			{
				SoundPlayCtl.Instance.PlaySound("碰擦悬杆", 100f);
				AudioSourceCtl.Instance.PlayAudio("CarHit");
				CarState.Instance.FireOn = false;
				errorIntervalTime2 = 0f;
			}
		}

		public bool GetShakeStatus()
		{
			return isShake;
		}

		protected virtual void GuardBarCollisionEvent(Collision collision)
		{
			if (errorIntervalTime2 > 10f)
			{
				SoundPlayCtl.Instance.PlaySound("撞护栏", 100f);
				AudioSourceCtl.Instance.PlayAudio("CarHit");
				errorIntervalTime2 = 0f;
				CarState.Instance.FireOn = false;
			}
		}

		protected void CommonErrorJudge()
		{
			if (CarState.Instance.FireOn != preFireOn && CarState.Instance.FireOn && CarState.Instance.CarSignal.fire == 1)
			{
				if (!CarActionJudge.Instance.JudgeActionDone(18))
				{
					_ = SettingInfoCtl.Instance.isManualGear;
				}
				CarActionJudge.Instance.JudgeActionDone(21);
				if (!CarActionJudge.Instance.JudgeActionDone(39) && !CarActionJudge.Instance.JudgeActionDone(67) && !CarActionJudge.Instance.JudgeActionDone(63) && ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("点火时挡位未挂在空挡", 100f);
				}
				if (CarActionJudge.Instance.JudgeActionDone(47) && ViewJumpCtl.Instance.modelType != 0)
				{
					SoundPlayCtl.Instance.PlaySound("起步前未系上安全带", 100f);
				}
				if (CarState.Instance.CarSignal.oil > 0.5f)
				{
					SoundPlayCtl.Instance.PlaySound("起步时猛踩油门", 10f);
				}
			}
			if (ViewJumpCtl.Instance.modelType == 2 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain)
			{
				if (SettingInfoCtl.Instance.isManualGear && CarState.Instance.FireOn && CarState.Instance.CarSignal.gear >= 2 && CarState.Instance.CarSignal.gear != 6 && CarState.Instance.CarSignal.footBrake <= 0.15f && CarState.Instance.CarSignal.cluth <= 0.9f && CarState.Instance.CarSignal.cluth >= 0.8f && CarState.Instance.CurSpeed <= 3f && errorIntervalTime3 >= 3f)
				{
					SoundPlayCtl.Instance.PlaySound("高挡起步", 10f);
					errorIntervalTime3 = 0f;
				}
				if (CarState.Instance.FireOn && (CarState.Instance.CarSignal.gear == 102 || CarState.Instance.CarSignal.gear == 6) && CarState.Instance.CarSignal.gear != preGear && CarState.Instance.CurSpeed >= 3f)
				{
					SoundPlayCtl.Instance.PlaySound("车辆未停稳时挂倒挡", 10f);
				}
				if (SettingInfoCtl.Instance.isManualGear && CarState.Instance.FireOn && CarState.Instance.CarSignal.gear != 0 && CarState.Instance.CarSignal.gear != preGear && CarState.Instance.CarSignal.cluth < 0.7f)
				{
					SoundPlayCtl.Instance.PlaySound("换挡时未将离合器踩到底", 10f);
				}
				if (CarState.Instance.FireOn != preFireOn && !CarState.Instance.FireOn && CarState.Instance.CurSpeed >= 3f && ViewJumpCtl.Instance.itemType != 610)
				{
					SoundPlayCtl.Instance.PlaySound("行驶中熄火", 10f);
				}
				if (MoNiCarDynamic.Instance.JudgeFireOff())
				{
					if ((double)CarState.Instance.CarSignal.handBrake > 0.8)
					{
						SoundPlayCtl.Instance.PlaySound("起步未松手刹");
					}
					SoundPlayCtl.Instance.PlaySound("操作不当造成发动机熄火", 10f);
					CarState.Instance.FireOn = false;
				}
			}
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.CurSpeed > 30f && CarState.Instance.CarSignal.footBrake > 0.6f && errorIntervalTime > 10f)
				{
					footBrakeAudio = AudioSourceCtl.Instance.PlayAudio("CarBrake");
					errorIntervalTime = 0f;
				}
			}
			else if ((ViewJumpCtl.Instance.modelType == 5 || ViewJumpCtl.Instance.modelType == 0) && DriveHistoryPlay.Instance.curCalculateSpeedH > 30f && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.6f && errorIntervalTime > 10f)
			{
				footBrakeAudio = AudioSourceCtl.Instance.PlayAudio("CarBrake");
				errorIntervalTime = 0f;
			}
			if (CarState.Instance.CarSignal.gear == 0)
			{
				GearChangeTime += Time.deltaTime;
				if (GearChangeTime >= 0.3f)
				{
					preGear = CarState.Instance.CarSignal.gear;
				}
			}
			else
			{
				preGear = CarState.Instance.CarSignal.gear;
				GearChangeTime = 0f;
			}
			preFireOn = CarState.Instance.FireOn;
			ActiveTime += Time.deltaTime;
			if (ActiveTime > 10f)
			{
				DriveInstructionView.Instance.Exit();
				ActiveTime = 0f;
			}
			if (CarState.Instance.FireOn || CarState.Instance.CarSignal.cluth > 0.3f || CarState.Instance.CarSignal.footBrake > 0.3f || CarState.Instance.CarSignal.oil > 0.3f)
			{
				DriveInstructionView.Instance.Exit();
			}
		}

		protected void CarReset()
		{
			Car.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Car.transform.position = SceneModelBase.Instance.GetStartPosition().position;
			if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
			{
				Car.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
			{
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo && SceneJumpCtl.Instance.CoursetwoName == CoursetwoScenesName.QingHu)
				{
					Car.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
				}
				else if ((ViewJumpCtl.Instance.trainType == TrainType.CourseThree || ViewJumpCtl.Instance.trainType == TrainType.NightLight) && SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
				{
					Car.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
				}
				else
				{
					Car.transform.localScale = Vector3.one;
				}
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
			{
				Car.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.TianJin)
			{
				Car.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
			{
				Car.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1121)
			{
				Car.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1125)
			{
				Car.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.YongShi)
			{
				Car.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QianYinChe)
			{
				Car.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			}
			Car.transform.eulerAngles = SceneModelBase.Instance.GetStartPosition().eulerAngles;
			CarNiXing.NiXing = false;
			CarNiXing.JianGeTime = 0f;
		}

		public void ReadMirrorDataXml()
		{
			string text = "";
			text = ((Application.platform != RuntimePlatform.Android) ? Path.Combine(Application.streamingAssetsPath, "SettingFile/MirrorData.xml") : Path.Combine(Application.persistentDataPath, "SettingFile/MirrorData.xml"));
			if (!File.Exists(text))
			{
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(text);
			foreach (XmlNode item in xmlDocument.SelectNodes("MirrorData/CarType"))
			{
				if (!(item.Attributes["name"].Value == SettingInfoCtl.Instance.carType.ToString()))
				{
					continue;
				}
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
				{
					XmlNode xmlNode2 = item.SelectSingleNode("CourseTwo/leftReviewCameraX");
					XmlNode xmlNode3 = item.SelectSingleNode("CourseTwo/leftReviewCameraY");
					XmlNode xmlNode4 = item.SelectSingleNode("CourseTwo/rightReviewCameraX");
					XmlNode xmlNode5 = item.SelectSingleNode("CourseTwo/rightReviewCameraY");
					if (xmlNode2 != null && xmlNode2 != null && xmlNode2 != null && xmlNode2 != null)
					{
						CarCameraCtl.Instance.leftReviewCamera.localEulerAngles = new Vector3(float.Parse(xmlNode2.InnerText), float.Parse(xmlNode3.InnerText));
						CarCameraCtl.Instance.rightReviewCamera.localEulerAngles = new Vector3(float.Parse(xmlNode4.InnerText), float.Parse(xmlNode5.InnerText));
					}
				}
				else
				{
					XmlNode xmlNode6 = item.SelectSingleNode("Other/leftReviewCameraX");
					XmlNode xmlNode7 = item.SelectSingleNode("Other/leftReviewCameraY");
					XmlNode xmlNode8 = item.SelectSingleNode("Other/rightReviewCameraX");
					XmlNode xmlNode9 = item.SelectSingleNode("Other/rightReviewCameraY");
					if (xmlNode6 != null && xmlNode6 != null && xmlNode6 != null && xmlNode6 != null)
					{
						CarCameraCtl.Instance.leftReviewCamera.localEulerAngles = new Vector3(float.Parse(xmlNode6.InnerText), float.Parse(xmlNode7.InnerText));
						CarCameraCtl.Instance.rightReviewCamera.localEulerAngles = new Vector3(float.Parse(xmlNode8.InnerText), float.Parse(xmlNode9.InnerText));
					}
				}
				break;
			}
		}

		public virtual void OnDestroy()
		{
			ViewJumpCtl.Instance.otherType = 504;
			if (mainCamView != null)
			{
				UnityEngine.Object.Destroy(mainCamView);
				PlayerPrefs.SetInt("isrecordorplay", 1);
			}
			SceneMenuView.isReset = false;
			DriveInstructionView.Instance.Exit();
		}
	}
}
