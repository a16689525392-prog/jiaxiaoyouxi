using Scene;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Car
{
	public class CarNiXing : MonoBehaviour
	{
		public static float StartAngle;

		private float EndAngle;

		public static float JianGeTime;

		public static bool NiXing;

		private GameObject aicar;

		private GameObject aicar1;

		private GameObject aicar2;

		private GameObject aicar3;

		private GameObject aicar4;

		private float huichetiem = 10f;

		private int index;

		private int errorIndex;

		private float DriveTime = 3f;

		private void Awake()
		{
			if (ViewJumpCtl.Instance.itemType == 5110 || ViewJumpCtl.Instance.itemType == 5111)
			{
				base.gameObject.AddComponent<AICarMove>();
			}
		}

		private void Start()
		{
			StartAngle = base.transform.eulerAngles.y;
			if (ViewJumpCtl.Instance.otherType == 505 && ViewJumpCtl.Instance.itemType == 5050)
			{
				aicar = GameObject.Find("AICar3(Clone)");
			}
			if (ViewJumpCtl.Instance.otherType == 505 && ViewJumpCtl.Instance.itemType == 701)
			{
				aicar1 = GameObject.Find("AICar1(Clone)");
				aicar2 = GameObject.Find("AICar2(Clone)");
				aicar3 = GameObject.Find("AICar3(Clone)");
			}
		}

		private void Update()
		{
			if (ViewJumpCtl.Instance.itemType == 706)
			{
				if (ShenZhenSceneCtl.instance.InstanceElectricCarandMan != null && ShenZhenSceneCtl.instance.itemStep == 1)
				{
					if (Vector3.Distance(base.transform.position, ShenZhenSceneCtl.instance.InstanceElectricCarandMan.transform.position) < 45f && Vector3.Distance(base.transform.position, ShenZhenSceneCtl.instance.InstanceElectricCarandMan.transform.position) > 20f)
					{
						if (CarState.Instance.CarSignal.oil < 0.03f)
						{
							ShenZhenSceneCtl.instance.DefenseTestElectricCar_Oil = true;
						}
						if (CarState.Instance.CarSignal.footBrake > 0.2f)
						{
							ShenZhenSceneCtl.instance.DefenseTestElectricCar_Footbreak = true;
						}
						if (CarState.Instance.CarSignal.horn == 1)
						{
							ShenZhenSceneCtl.instance.DefenseTestElectricCar_Horn = true;
						}
						if (CarState.Instance.CarSignal.steerWheel < -0.15f)
						{
							ShenZhenSceneCtl.instance.DefenseTestElectricCar_SteerWheel = true;
						}
					}
					else if (Vector3.Distance(base.transform.position, ShenZhenSceneCtl.instance.InstanceElectricCarandMan.transform.position) <= 20f)
					{
						if (!ShenZhenSceneCtl.instance.DefenseTestElectricCar_Oil)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未松油门减速避让非机动车", 10f);
							ShenZhenSceneCtl.instance.DefenseTestElectricCar_Oil = true;
						}
						if (!ShenZhenSceneCtl.instance.DefenseTestElectricCar_Footbreak)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未踩制动减速避让非机动车", 10f);
							ShenZhenSceneCtl.instance.DefenseTestElectricCar_Footbreak = true;
						}
						if (!ShenZhenSceneCtl.instance.DefenseTestElectricCar_Horn)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未鸣喇叭示意非机动车", 10f);
							ShenZhenSceneCtl.instance.DefenseTestElectricCar_Horn = true;
						}
						if (!ShenZhenSceneCtl.instance.DefenseTestElectricCar_SteerWheel)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未打转向盘避让非机动车", 10f);
							ShenZhenSceneCtl.instance.DefenseTestElectricCar_SteerWheel = true;
						}
					}
				}
				if (ShenZhenSceneCtl.instance.InstancePeople != null && ShenZhenSceneCtl.instance.itemStep == 2)
				{
					if (Vector3.Distance(base.transform.position, ShenZhenSceneCtl.instance.InstancePeople.transform.position) < 45f && Vector3.Distance(base.transform.position, ShenZhenSceneCtl.instance.InstancePeople.transform.position) > 20f)
					{
						if (CarState.Instance.CarSignal.oil < 0.03f)
						{
							ShenZhenSceneCtl.instance.DefenseTestPeople_Oil = true;
						}
						if (CarState.Instance.CarSignal.footBrake > 0.2f)
						{
							ShenZhenSceneCtl.instance.DefenseTestPeople_Footbreak = true;
						}
						if (CarState.Instance.CarSignal.horn == 1)
						{
							ShenZhenSceneCtl.instance.DefenseTestPeople_Horn = true;
						}
						if (CarState.Instance.CarSignal.steerWheel < -0.15f)
						{
							ShenZhenSceneCtl.instance.DefenseTestPeople_SteerWheel = true;
						}
					}
					else if (Vector3.Distance(base.transform.position, ShenZhenSceneCtl.instance.InstancePeople.transform.position) <= 20f)
					{
						if (!ShenZhenSceneCtl.instance.DefenseTestPeople_Oil)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未松油门减速避让行人", 10f);
							ShenZhenSceneCtl.instance.DefenseTestPeople_Oil = true;
						}
						if (!ShenZhenSceneCtl.instance.DefenseTestPeople_Footbreak)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未踩制动减速避让行人", 10f);
							ShenZhenSceneCtl.instance.DefenseTestPeople_Footbreak = true;
						}
						if (!ShenZhenSceneCtl.instance.DefenseTestPeople_Horn)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未鸣喇叭示意行人", 10f);
							ShenZhenSceneCtl.instance.DefenseTestPeople_Horn = true;
						}
						if (!ShenZhenSceneCtl.instance.DefenseTestPeople_SteerWheel)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未打转向盘避让行人", 10f);
							ShenZhenSceneCtl.instance.DefenseTestPeople_SteerWheel = true;
						}
					}
				}
				if (ShenZhenSceneCtl.instance.InstanceAiCar != null && ShenZhenSceneCtl.instance.itemStep == 3)
				{
					if (Vector3.Distance(base.transform.position, ShenZhenSceneCtl.instance.InstanceAiCar.transform.position) < 45f && Vector3.Distance(base.transform.position, ShenZhenSceneCtl.instance.InstanceAiCar.transform.position) > 20f)
					{
						if (CarState.Instance.CarSignal.oil < 0.03f)
						{
							ShenZhenSceneCtl.instance.DefenseTestAiCar_Oil = true;
						}
						if (CarState.Instance.CarSignal.footBrake > 0.2f)
						{
							ShenZhenSceneCtl.instance.DefenseTestAiCar_Footbreak = true;
						}
						if (CarState.Instance.CarSignal.steerWheel < -0.15f)
						{
							ShenZhenSceneCtl.instance.DefenseTestAiCar_SteerWheel = true;
						}
					}
					else if (Vector3.Distance(base.transform.position, ShenZhenSceneCtl.instance.InstanceAiCar.transform.position) <= 20f)
					{
						if (!ShenZhenSceneCtl.instance.DefenseTestAiCar_Oil)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未松油门减速避让车辆", 5f);
							ShenZhenSceneCtl.instance.DefenseTestAiCar_Oil = true;
						}
						if (!ShenZhenSceneCtl.instance.DefenseTestAiCar_Footbreak)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未踩制动减速避让车辆", 5f);
							ShenZhenSceneCtl.instance.DefenseTestAiCar_Footbreak = true;
						}
						if (!ShenZhenSceneCtl.instance.DefenseTestAiCar_SteerWheel)
						{
							SoundPlayCtl.Instance.PlaySound("行驶中未打转向盘避让车辆", 5f);
							ShenZhenSceneCtl.instance.DefenseTestAiCar_SteerWheel = true;
						}
					}
				}
			}
			huichetiem += Time.deltaTime;
			if (ViewJumpCtl.Instance.otherType == 505 && ViewJumpCtl.Instance.itemType == 5050 && Vector3.Distance(base.transform.position, aicar.transform.position) < 100f && huichetiem >= 10f && CarState.Instance.CarSignal.frontLight != 1)
			{
				SoundPlayCtl.Instance.PlaySound("夜间会车未切换成近光灯", 10f);
				huichetiem = 0f;
			}
			if (ViewJumpCtl.Instance.otherType == 505 && ViewJumpCtl.Instance.itemType == 701)
			{
				if ((Vector3.Distance(base.transform.position, aicar1.transform.position) < 10f || Vector3.Distance(base.transform.position, aicar2.transform.position) < 10f || Vector3.Distance(base.transform.position, aicar3.transform.position) < 10f) && huichetiem >= 10f && CarState.Instance.CarSignal.frontLight != 1)
				{
					SoundPlayCtl.Instance.PlaySound("夜间会车未切换成近光灯", 10f);
					huichetiem = 0f;
				}
				if (NightTestSceneCtl.instance.InstanceNightTestAICar != null && Vector3.Distance(base.transform.position, NightTestSceneCtl.instance.InstanceNightTestAICar.transform.position) < 15f && CarState.Instance.CarSignal.frontLight != 1 && !NightTestSceneCtl.instance.isGenCheFalse)
				{
					SoundPlayCtl.Instance.PlaySound("夜间同方向近距离跟车行驶，未使用近光灯", 10f);
					NightTestSceneCtl.instance.isGenCheFalse = true;
				}
				if (NightTestSceneCtl.instance.InstanceElectricCarandMan != null && Vector3.Distance(base.transform.position, NightTestSceneCtl.instance.InstanceElectricCarandMan.transform.position) < 15f && CarState.Instance.CarSignal.frontLight != 1 && !NightTestSceneCtl.instance.isElectricCarHuiCheFalse)
				{
					SoundPlayCtl.Instance.PlaySound("夜间在窄路与非机动车会车，未使用近光灯", 10f);
					NightTestSceneCtl.instance.isElectricCarHuiCheFalse = true;
				}
			}
			if (ViewJumpCtl.Instance.itemType == 704)
			{
				if (HighRoadTest.Instance.itemStep >= 1 && HighRoadTest.Instance.itemStep <= 4 && base.transform.localEulerAngles.y < 330f && base.transform.localEulerAngles.y > 280f && !NiXing)
				{
					SoundPlayCtl.Instance.PlaySound("逆向行驶", 20f);
					NiXing = true;
					JianGeTime = 0f;
				}
				if (HighRoadTest.Instance.itemStep >= 7 && base.transform.localEulerAngles.y < 250f && base.transform.localEulerAngles.y > 200f && !NiXing)
				{
					SoundPlayCtl.Instance.PlaySound("逆向行驶", 20f);
					NiXing = true;
					JianGeTime = 0f;
				}
			}
			EndAngle = base.transform.eulerAngles.y;
			if ((ViewJumpCtl.Instance.itemType == 210 || ViewJumpCtl.Instance.itemType == 211 || ViewJumpCtl.Instance.itemType == 212) && EndAngle > 180f)
			{
				EndAngle -= 360f;
			}
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree && SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang && Mathf.Abs(EndAngle - StartAngle) > 180f && !NiXing)
			{
				SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
				NiXing = true;
				JianGeTime = 0f;
			}
			if (NiXing)
			{
				JianGeTime += Time.deltaTime;
				if (JianGeTime > 10f)
				{
					NiXing = false;
				}
			}
			DriveTime += Time.deltaTime;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "AI_People")
			{
				Object.Destroy(other.transform.parent.parent.gameObject);
			}
			if (index == 0 && other.name == "LuKouYouZhuan1")
			{
				StartAngle = base.transform.eulerAngles.y;
				index = 1;
			}
			else if (index == 1 && other.name == "LuKouYouZhuanError")
			{
				SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
				index = 2;
			}
			else if ((index == 1 || index == 2) && other.name == "LuKouYouZhuan2")
			{
				StartAngle = base.transform.eulerAngles.y;
				index = 3;
			}
			else if ((index == 0 || index == 3) && other.name == "DiaoTou1")
			{
				StartAngle = base.transform.eulerAngles.y;
				index = 4;
			}
			else if (index == 4 && other.name == "DiaoTouError")
			{
				SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
				index = 5;
			}
			else if ((index == 4 || index == 5) && (other.name == "DiaoTou2" || other.name == "DiaoTou3" || other.name == "DiaoTou4"))
			{
				StartAngle = base.transform.eulerAngles.y;
				index = 6;
			}
			else if ((index == 0 || index == 6) && other.name == "LuKouZuoZhuan1")
			{
				StartAngle = base.transform.eulerAngles.y;
				index = 7;
			}
			else if (index == 7 && other.name == "LuKouYouZhuan1")
			{
				SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
				index = 8;
			}
			else if ((index == 7 || index == 8) && other.name == "LuKouZuoZhuan2")
			{
				StartAngle = base.transform.eulerAngles.y;
				index = 7;
			}
			if (ViewJumpCtl.Instance.itemType != 210 && ViewJumpCtl.Instance.itemType != 211 && ViewJumpCtl.Instance.itemType != 212)
			{
				if (errorIndex != 1 && other.name == "RoadNiXing0")
				{
					errorIndex = 1;
				}
				else if (errorIndex == 1 && other.name == "RoadNiXing1")
				{
					SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
					errorIndex = 2;
				}
				if (errorIndex != 3 && other.name == "RoadNiXing2")
				{
					errorIndex = 3;
				}
				else if (errorIndex == 3 && other.name == "RoadNiXing3")
				{
					SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
					errorIndex = 4;
				}
				if (errorIndex != 5 && other.name == "RoadNiXing4")
				{
					errorIndex = 5;
				}
				else if (errorIndex == 5 && other.name == "RoadNiXing5")
				{
					SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
					errorIndex = 6;
				}
				if (errorIndex != 7 && other.name == "RoadNiXing6")
				{
					errorIndex = 7;
				}
				else if (errorIndex == 7 && other.name == "RoadNiXing7")
				{
					SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
					errorIndex = 8;
				}
				if (errorIndex != 9 && other.name == "RoadNiXing8")
				{
					errorIndex = 9;
				}
				else if (errorIndex == 9 && other.name == "RoadNiXing9")
				{
					SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
					errorIndex = 10;
				}
				if (errorIndex != 11 && other.name == "RoadNiXing10")
				{
					errorIndex = 11;
				}
				else if (errorIndex == 11 && other.name == "RoadNiXing11")
				{
					SoundPlayCtl.Instance.PlaySound("逆向行驶", 100f);
					errorIndex = 12;
				}
			}
		}

		private void OnDestroy()
		{
			StartAngle = base.transform.eulerAngles.y;
			NiXing = false;
			JianGeTime = 0f;
		}
	}
}
