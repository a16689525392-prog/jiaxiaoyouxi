using System;
using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class CourseThree : TrainSceneBase
	{
		public GameObject StartPosition;

		public static CourseThree Instance;

		[HideInInspector]
		public float dirveLength;

		public bool IsCourseThreeKaoShi;

		private bool PlayPointFoodBrake;

		private float Geartime;

		private float OneGearDirveLength;

		private int curItemType;

		private CourseThreeItemBase curItemCtl;

		public int ItemStartChild0;

		public bool DecelerationDetectionState;

		protected override void Awake()
		{
			base.Awake();
			Instance = this;
			if (SceneJumpCtl.Instance.CoursethreeName != CoursethreeScenesName.NanChang && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.modelType != 5 && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
			{
				StartPosition = GameObject.Find("TriggerArea").transform.GetChild(SceneJumpCtl.Instance.SceneWay - 1).GetChild(0).gameObject;
			}
		}

		protected override void Start()
		{
			base.Start();
			if (ViewJumpCtl.Instance.trainType == TrainType.NightLight && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
			{
				base.gameObject.AddComponent<NightLightCtl>();
			}
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree && ViewJumpCtl.Instance.modelType != 5)
			{
				curItemType = ViewJumpCtl.Instance.itemType;
				if (ViewJumpCtl.Instance.itemType == 1300 && ViewJumpCtl.Instance.modelType != 1 && ViewJumpCtl.Instance.modelType != 0)
				{
					IsCourseThreeKaoShi = true;
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						base.gameObject.AddComponent<CourseThreeKaoShi>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						base.gameObject.AddComponent<HuNanYZCourseThreeKaoShi>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						base.gameObject.AddComponent<VipCourseThreeKaoShi>();
					}
				}
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					AddItemScript(ViewJumpCtl.Instance.itemType);
				}
				else if (ViewJumpCtl.Instance.modelType == 1)
				{
					base.gameObject.AddComponent<CourseThreeDriveGuide>();
					switch (ViewJumpCtl.Instance.itemType)
					{
					case 200:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("StartingGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("Starting.bytes"));
						break;
					case 201:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("OvertakeGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("Overtake.bytes"));
						break;
					case 209:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("MeetingGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("Meeting.bytes"));
						break;
					case 202:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("ThroughSchoolGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("ThroughSchool.bytes"));
						break;
					case 203:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("ChangeLaneGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("ChangeLane.bytes"));
						break;
					case 204:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("ThroughManLaneGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("ThroughManLane.bytes"));
						break;
					case 205:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("TurnRightGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("TurnRight.bytes"));
						break;
					case 206:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("TurnRoundGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("TurnRound.bytes"));
						break;
					case 207:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("ThroughBusStationGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("ThroughBusStation.bytes"));
						break;
					case 208:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("TurnLeftGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("TurnLeft.bytes"));
						break;
					case 210:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("AddAndSubGearGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("AddAndSubGear.bytes"));
						break;
					case 211:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("StraightDriveGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("StraightDrive.bytes"));
						break;
					case 214:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("StraightCrossroadsGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("StraightCrossroads.bytes"));
						break;
					case 212:
						StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("StoppingGuide.xml"));
						StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("Stopping.bytes"));
						break;
					case 1300:
						if (SceneJumpCtl.Instance.CoursethreeName != CoursethreeScenesName.NanChang)
						{
							switch (SceneJumpCtl.Instance.SceneWay)
							{
							case 1:
								StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("CourseThreeOneWayGuide.xml"));
								StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("CourseThreeOneWay.bytes"));
								break;
							case 2:
								StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("CourseThreeTwoWayGuide.xml"));
								StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("CourseThreeTwoWay.bytes"));
								break;
							case 3:
								StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("CourseThreeThreeWayGuide.xml"));
								StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("CourseThreeThreeWay.bytes"));
								break;
							case 4:
								StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("CourseThreeFourWayGuide.xml"));
								StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("CourseThreeFourWay.bytes"));
								break;
							}
						}
						else
						{
							StartCoroutine(CourseThreeDriveGuide.Instance.LoadGuideFile("CourseThreeGuide.xml"));
							StartCoroutine(CarDriveHistoryPlay.Instance.LoadHistoryDriveFiles("CourseThree.bytes"));
						}
						break;
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 0)
				{
					base.gameObject.AddComponent<CourseThreeYanShiGuide>();
					if (SceneJumpCtl.Instance.CoursethreeName != CoursethreeScenesName.NanChang)
					{
						switch (SceneJumpCtl.Instance.SceneWay)
						{
						case 1:
							StartCoroutine(CourseThreeYanShiGuide.Instance.LoadYanShiGuideFile("CourseThreeOneWayYanShiTips.xml"));
							StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("CourseThreeOneWay.bytes"));
							break;
						case 2:
							StartCoroutine(CourseThreeYanShiGuide.Instance.LoadYanShiGuideFile("CourseThreeTwoWayYanShiTips.xml"));
							StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("CourseThreeTwoWay.bytes"));
							break;
						case 3:
							StartCoroutine(CourseThreeYanShiGuide.Instance.LoadYanShiGuideFile("CourseThreeThreeWayYanShiTips.xml"));
							StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("CourseThreeThreeWay.bytes"));
							break;
						case 4:
							StartCoroutine(CourseThreeYanShiGuide.Instance.LoadYanShiGuideFile("CourseThreeFourWayYanShiTips.xml"));
							StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("CourseThreeFourWay.bytes"));
							break;
						}
					}
					else
					{
						StartCoroutine(CourseThreeYanShiGuide.Instance.LoadYanShiGuideFile("CourseThreeOneWayYanShiTips.xml"));
						StartCoroutine(DriveHistoryPlay.Instance.LoadYanShiDriveFiles("CourseThree.bytes"));
					}
				}
			}
			else if (ViewJumpCtl.Instance.trainType == TrainType.BasicTrain)
			{
				base.gameObject.AddComponent<BasicTrainDriveGuide>();
			}
			else if (ViewJumpCtl.Instance.trainType == TrainType.DriveSkill)
			{
				base.gameObject.AddComponent<CtiyDriveSkill>();
			}
			base.ResetAction = (UnityAction)Delegate.Combine(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			ColliderDetection instance = ColliderDetection.instance;
			instance.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Combine(instance.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			ColliderDetection instance2 = ColliderDetection.instance;
			instance2.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Combine(instance2.roadIndexChangeCallBack, new UnityAction<int, int>(JudgeTurnLight));
		}

		private void CoursethreeRule()
		{
			if (CarState.Instance.CarSignal.footBrake < 0.05f && !PlayPointFoodBrake && SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
			{
				PlayPointFoodBrake = true;
			}
			if (CarState.Instance.CarSignal.footBrake > 0.08f && PlayPointFoodBrake && SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
			{
				SoundPlayCtl.Instance.PlaySound("叮");
				PlayPointFoodBrake = false;
			}
			if (SettingInfoCtl.Instance.isManualGear && CarState.Instance.FireOn && CarState.Instance.CarSignal.cluth < 0.05f)
			{
				switch (CarState.Instance.CarSignal.gear)
				{
				case 1:
					if (CarState.Instance.CurSpeed > 19f)
					{
						Geartime += Time.deltaTime;
					}
					else
					{
						Geartime = 0f;
					}
					break;
				case 2:
					if ((CarState.Instance.CurSpeed > 0.5f && CarState.Instance.CurSpeed < 9f) || CarState.Instance.CurSpeed > 32f)
					{
						Geartime += Time.deltaTime;
					}
					else
					{
						Geartime = 0f;
					}
					break;
				case 3:
					if ((CarState.Instance.CurSpeed > 0.5f && CarState.Instance.CurSpeed < 15f) || CarState.Instance.CurSpeed > 50f)
					{
						Geartime += Time.deltaTime;
					}
					else
					{
						Geartime = 0f;
					}
					break;
				case 4:
					if ((CarState.Instance.CurSpeed > 0.5f && CarState.Instance.CurSpeed < 24f) || CarState.Instance.CurSpeed > 60f)
					{
						Geartime += Time.deltaTime;
					}
					else
					{
						Geartime = 0f;
					}
					break;
				}
			}
			if (Geartime > 4.5f && Geartime < 5f)
			{
				SoundPlayCtl.Instance.PlaySound("车速与挡位短时间不匹配", 10f);
				Geartime = 5f;
			}
			else if (Geartime > 10.5f && Geartime < 11f)
			{
				SoundPlayCtl.Instance.PlaySound("车速与挡位长时间不匹配", 100f);
				Geartime = 11f;
			}
			if (SettingInfoCtl.Instance.isManualGear && CarState.Instance.CarSignal.gear == 1 && CarState.Instance.CarSignal.cluth < 0.05f)
			{
				OneGearDirveLength += CarState.Instance.CurSpeed * Time.deltaTime * 0.27f;
			}
			else
			{
				OneGearDirveLength = 0f;
			}
			if (OneGearDirveLength > 50f && OneGearDirveLength < 51f)
			{
				SoundPlayCtl.Instance.PlaySound("一挡行驶超过五十米", 100f);
				OneGearDirveLength = 51f;
			}
			if (DecelerationDetectionState && CarState.Instance.CarSignal.footBrake >= 0.1f)
			{
				DecelerationDetectionState = false;
			}
		}

		protected override void Update()
		{
			base.Update();
			dirveLength += CarState.Instance.CurSpeed * Time.deltaTime;
			if (ViewJumpCtl.Instance.trainType != TrainType.BasicTrain)
			{
				CoursethreeRule();
			}
		}

		private void TrainItemReset()
		{
			if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 5)
			{
				AddItemScript(ViewJumpCtl.Instance.itemType);
			}
			dirveLength = 0f;
			StartPosition.transform.GetChild(0).gameObject.SetActive(value: true);
		}

		private void ResetTimePlay()
		{
			if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 5)
			{
				AddItemScript(ViewJumpCtl.Instance.itemType);
			}
			dirveLength = 0f;
		}

		private void TriggerAreaEvent(Collider other)
		{
			foreach (ItemPosition itemPosition in DataCenter.Instance.itemPositionList)
			{
				if (!(itemPosition.itemStartName == other.GetComponent<Collider>().name))
				{
					continue;
				}
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110)
				{
					int num = Convert.ToInt32(other.transform.GetChild(0).name);
					for (int i = 0; i < StartPosition.transform.childCount; i++)
					{
						if (i == num || i == num + 1)
						{
							StartPosition.transform.GetChild(i).gameObject.SetActive(value: true);
						}
						else
						{
							StartPosition.transform.GetChild(i).gameObject.SetActive(value: false);
						}
					}
				}
				if (curItemType != itemPosition.itemType && ViewJumpCtl.Instance.modelType != 1)
				{
					if (other.transform.GetChild(0) != null)
					{
						ItemStartChild0 = Convert.ToInt32(other.transform.GetChild(0).name);
					}
					AddItemScript(itemPosition.itemType);
				}
				break;
			}
			if (curItemCtl != null)
			{
				curItemCtl.TriggerEvent(other);
			}
			if (other.name == "DecelerationDetectionOpen")
			{
				DecelerationDetectionState = true;
			}
			if (other.name == "DecelerationDetectionClose")
			{
				if (DecelerationDetectionState && ViewJumpCtl.Instance.modelType != 0 && ViewJumpCtl.Instance.modelType != 1)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行减速操作", 100f);
				}
				DecelerationDetectionState = false;
			}
		}

		private void AddItemScript(int itemType)
		{
			if (curItemCtl != null)
			{
				UnityEngine.Object.Destroy(curItemCtl);
			}
			curItemType = itemType;
			switch (itemType)
			{
			case 200:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeStart>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeStart>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeStart>();
					}
				}
				break;
			case 201:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeOvertake>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeOvertake>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeOvertake>();
					}
				}
				break;
			case 202:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeThroughSchool>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeThroughSchool>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeThroughSchool>();
					}
				}
				break;
			case 203:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeChangeLane>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeChangeLane>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeChangeLane>();
					}
				}
				break;
			case 204:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeThroughManLane>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeThroughManLane>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeThroughManLane>();
					}
				}
				break;
			case 205:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeTurnRight>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeTurnRight>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeTurnRight>();
					}
				}
				break;
			case 206:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeTurnRound>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeTurnRound>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeTurnRound>();
					}
				}
				break;
			case 207:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeThroughBusStation>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeThroughBusStation>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeThroughBusStation>();
					}
				}
				break;
			case 208:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeTurnLeft>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeTurnLeft>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeTurnLeft>();
					}
				}
				break;
			case 210:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight && SettingInfoCtl.Instance.isManualGear)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeAddAndSubGear>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeAddAndSubGear>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeAddAndSubGear>();
					}
				}
				break;
			case 211:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeStraightDrive>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeStraightDrive>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeStraightDrive>();
					}
				}
				break;
			case 212:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeStopping>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeStopping>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeStopping>();
					}
				}
				break;
			case 209:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeThroughMeetting>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeThroughMeetting>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeThroughMeetting>();
					}
				}
				break;
			case 213:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeDaoCar>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeDaoCar>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeDaoCar>();
					}
				}
				break;
			case 214:
				if (ViewJumpCtl.Instance.itemType != 5111 && ViewJumpCtl.Instance.itemType != 5110 && ViewJumpCtl.Instance.trainType != TrainType.BasicTrain && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
				{
					if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
					{
						curItemCtl = base.gameObject.AddComponent<CourseThreeStraightCrossroads>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
					{
						curItemCtl = base.gameObject.AddComponent<HuNanYZCourseThreeStraightCrossroads>();
					}
					else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
					{
						curItemCtl = base.gameObject.AddComponent<VipCourseThreeStraightCrossroads>();
					}
				}
				break;
			}
		}

		private void JudgeTurnLight(int preIndex, int curIndex)
		{
			if (ViewJumpCtl.Instance.modelType == 2 && curItemType != 206)
			{
				if (curIndex > preIndex)
				{
					CarActionJudge.Instance.JudgeTurnLight3s(109);
				}
				else
				{
					CarActionJudge.Instance.JudgeTurnLight3s(110);
				}
			}
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			ColliderDetection instance = ColliderDetection.instance;
			instance.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Remove(instance.roadIndexChangeCallBack, new UnityAction<int, int>(JudgeTurnLight));
			ColliderDetection instance2 = ColliderDetection.instance;
			instance2.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Remove(instance2.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			SoundPlayCtl.Instance.ClearClip();
		}
	}
}
