using Scene;
using UnityEngine;
using View;

namespace Car
{
	public class AiCarManager : MonoBehaviour
	{
		private static AiCarManager instance;

		public const int CAR_NUMTaiZhouChangTan = 21;

		public const int CAR_NUMXiangCun = 8;

		public const int CAR_NUMCity = 10;

		public const int CAR_NUMShanQu = 1;

		private Transform aiCarGroup;

		private Transform waypointGroup;

		public static int childWayPointsIndex;

		private bool isInit;

		public static AiCarManager Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		public void Init(Transform wayPoints)
		{
			waypointGroup = wayPoints;
			isInit = true;
			aiCarGroup = CreateAiCarGroup();
			if (ViewJumpCtl.Instance.otherType == 505 && ViewJumpCtl.Instance.itemType == 5050)
			{
				int num = 3;
				GameObject obj = Object.Instantiate(Resources.Load<GameObject>("AICar/AICar" + num));
				obj.transform.SetParent(aiCarGroup);
				childWayPointsIndex = 17;
				obj.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(childWayPointsIndex).GetComponent<WayPoint>(), this);
			}
			if (ViewJumpCtl.Instance.otherType == 505 && ViewJumpCtl.Instance.itemType == 701)
			{
				for (int i = 0; i < 3; i++)
				{
					int num2 = i + 1;
					GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("AICar/AICar" + num2));
					gameObject.transform.SetParent(aiCarGroup);
					switch (i)
					{
					case 0:
						childWayPointsIndex = 30;
						break;
					case 1:
						childWayPointsIndex = 135;
						break;
					case 2:
						childWayPointsIndex = 235;
						break;
					}
					gameObject.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(childWayPointsIndex).GetComponent<WayPoint>(), this);
				}
			}
			if (ViewJumpCtl.Instance.itemType == 511 || ViewJumpCtl.Instance.itemType == 403)
			{
				for (int j = 0; j < 10; j++)
				{
					int num3 = Random.Range(1, 4);
					GameObject gameObject2 = Object.Instantiate(Resources.Load<GameObject>("AICar/AICar" + num3));
					gameObject2.transform.SetParent(aiCarGroup);
					switch (j)
					{
					case 0:
						childWayPointsIndex = 144;
						break;
					case 19:
						childWayPointsIndex = 149;
						break;
					case 12:
						childWayPointsIndex = 151;
						break;
					case 3:
						childWayPointsIndex = 326;
						break;
					case 14:
						childWayPointsIndex = 329;
						break;
					case 5:
						childWayPointsIndex = 332;
						break;
					case 16:
						childWayPointsIndex = 270;
						break;
					case 7:
						childWayPointsIndex = 273;
						break;
					case 18:
						childWayPointsIndex = 275;
						break;
					case 9:
						childWayPointsIndex = 49;
						break;
					case 10:
						childWayPointsIndex = 52;
						break;
					case 11:
						childWayPointsIndex = 70;
						break;
					case 2:
						childWayPointsIndex = 177;
						break;
					case 13:
						childWayPointsIndex = 181;
						break;
					case 4:
						childWayPointsIndex = 184;
						break;
					case 15:
						childWayPointsIndex = 185;
						break;
					case 6:
						childWayPointsIndex = 187;
						break;
					case 17:
						childWayPointsIndex = 188;
						break;
					case 8:
						childWayPointsIndex = 190;
						break;
					case 1:
						childWayPointsIndex = 191;
						break;
					}
					gameObject2.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(childWayPointsIndex).GetComponent<WayPoint>(), this);
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 401)
			{
				for (int k = 0; k < 8; k++)
				{
					int num4 = Random.Range(1, 4);
					GameObject gameObject3 = Object.Instantiate(Resources.Load<GameObject>("AICar/AICar" + num4));
					gameObject3.transform.SetParent(aiCarGroup);
					switch (k)
					{
					case 0:
						childWayPointsIndex = 144;
						break;
					case 9:
						childWayPointsIndex = 20;
						break;
					case 2:
						childWayPointsIndex = 51;
						break;
					case 6:
						childWayPointsIndex = 326;
						break;
					case 4:
						childWayPointsIndex = 280;
						break;
					case 8:
						childWayPointsIndex = 99;
						break;
					case 1:
						childWayPointsIndex = 220;
						break;
					case 7:
						childWayPointsIndex = 183;
						break;
					case 5:
						childWayPointsIndex = 5;
						break;
					case 3:
						childWayPointsIndex = 360;
						break;
					}
					gameObject3.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(childWayPointsIndex).GetComponent<WayPoint>(), this);
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 407)
			{
				for (int l = 0; l < 21; l++)
				{
					int num5 = Random.Range(1, 6);
					GameObject gameObject4 = Object.Instantiate(Resources.Load<GameObject>("AICar/AICar" + num5));
					gameObject4.transform.SetParent(aiCarGroup);
					switch (l)
					{
					case 0:
						childWayPointsIndex = 6;
						break;
					case 1:
						childWayPointsIndex = 36;
						break;
					case 2:
						childWayPointsIndex = 66;
						break;
					case 3:
						childWayPointsIndex = 86;
						break;
					case 4:
						childWayPointsIndex = 116;
						break;
					case 5:
						childWayPointsIndex = 136;
						break;
					case 6:
						childWayPointsIndex = 166;
						break;
					case 7:
						childWayPointsIndex = 196;
						break;
					case 8:
						childWayPointsIndex = 216;
						break;
					case 9:
						childWayPointsIndex = 236;
						break;
					case 10:
						childWayPointsIndex = 266;
						break;
					case 11:
						childWayPointsIndex = 296;
						break;
					case 12:
						childWayPointsIndex = 316;
						break;
					case 13:
						childWayPointsIndex = 336;
						break;
					case 14:
						childWayPointsIndex = 366;
						break;
					case 15:
						childWayPointsIndex = 396;
						break;
					case 16:
						childWayPointsIndex = 416;
						break;
					case 17:
						childWayPointsIndex = 436;
						break;
					case 18:
						childWayPointsIndex = 466;
						break;
					case 19:
						childWayPointsIndex = 496;
						break;
					case 20:
						childWayPointsIndex = 516;
						break;
					}
					gameObject4.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(childWayPointsIndex).GetComponent<WayPoint>(), this);
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 510 || ViewJumpCtl.Instance.itemType == 703)
			{
				for (int m = 0; m < 1; m++)
				{
					GameObject gameObject5 = GameObject.Find("WayPoint_065");
					GameObject obj2 = Object.Instantiate(Resources.Load<GameObject>("AICar/AICar"), gameObject5.transform);
					int index = 0;
					obj2.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(index).GetComponent<WayPoint>(), this);
				}
				if (ShanQuSceneCtl.isReset)
				{
					Object.Destroy(GameObject.Find("Road_ShanQuDaoLu/Trigger_Position/WayPoints/WayPoint_065/AICar(Clone)"));
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 5100)
			{
				for (int n = 0; n < 1; n++)
				{
					if (ShanLuHuiChe.Instance.CarNum == 0f)
					{
						GameObject gameObject6 = GameObject.Find("WayPoint_056");
						GameObject obj3 = Object.Instantiate(Resources.Load<GameObject>("AICar/MeetCar0"), gameObject6.transform);
						int index2 = 131;
						obj3.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(index2).GetComponent<WayPoint>(), this);
					}
					if (ShanLuHuiChe.Instance.CarNum == 1f)
					{
						GameObject gameObject7 = GameObject.Find("WayPoint_013");
						GameObject obj4 = Object.Instantiate(Resources.Load<GameObject>("AICar/MeetCar1"), gameObject7.transform);
						int index3 = 88;
						obj4.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(index3).GetComponent<WayPoint>(), this);
					}
					if (ShanLuHuiChe.Instance.CarNum == 2f)
					{
						GameObject gameObject8 = GameObject.Find("WayPoint_01");
						GameObject obj5 = Object.Instantiate(Resources.Load<GameObject>("AICar/MeetCar2"), gameObject8.transform);
						int index4 = 76;
						obj5.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(index4).GetComponent<WayPoint>(), this);
					}
					if (ShanQuSceneCtl.isReset)
					{
						Object.Destroy(GameObject.Find("Road_ShanQuDaoLu/Trigger_Position/WayPoints/WayPoint_047/MeetCar0(Clone)"));
						Object.Destroy(GameObject.Find("Road_ShanQuDaoLu/Trigger_Position/WayPoints/WayPoint_013/MeetCar1(Clone)"));
						Object.Destroy(GameObject.Find("Road_ShanQuDaoLu/Trigger_Position/WayPoints/WayPoint_01/MeetCar2(Clone)"));
					}
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 404)
			{
				for (int num6 = 0; num6 < 1; num6++)
				{
					GameObject gameObject9 = GameObject.Find("WayPoint00");
					GameObject obj6 = Object.Instantiate(Resources.Load<GameObject>("AICar/AICar"), gameObject9.transform);
					int index5 = 0;
					obj6.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(index5).GetComponent<WayPoint>(), this);
				}
				if (HighRoadSceneCtl.Instance.isReset)
				{
					MonoBehaviour.print("ddd");
					Object.Destroy(GameObject.Find("Road_GaoSuGongLu /WayPoints/WayPoint00/AICar(Clone)"));
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 506)
			{
				for (int num7 = 0; num7 < 1; num7++)
				{
					GameObject gameObject10 = GameObject.Find("WayPoint55");
					GameObject obj7 = Object.Instantiate(Resources.Load<GameObject>("AICar/SuiDaoAICar"), gameObject10.transform);
					int index6 = 57;
					obj7.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(index6).GetComponent<WayPoint>(), this);
				}
				if (HighRoadSceneCtl.Instance.isReset)
				{
					Object.Destroy(GameObject.Find("Road_GaoSuGongLu /WayPoints/WayPoint98/SuiDaoAICar(Clone)"));
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 5111)
			{
				for (int num8 = 0; num8 < 1; num8++)
				{
					GameObject gameObject11 = GameObject.Find("WayPoint0");
					GameObject obj8 = Object.Instantiate(Resources.Load<GameObject>("AICar/AICar"), gameObject11.transform);
					int index7 = 0;
					obj8.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(index7).GetComponent<WayPoint>(), this);
				}
				if (CtiyDriveSkill.Instance.isReset)
				{
					Object.Destroy(GameObject.Find("DriveSkill/CityGenChe/WayPoints/WayPoint0/AICar(Clone)"));
				}
			}
		}

		public void OtherInit(Transform wayPoints)
		{
			waypointGroup = wayPoints;
			isInit = true;
			aiCarGroup = CreateAiCarGroup();
			for (int i = 0; i < 15; i++)
			{
				int num = Random.Range(1, 4);
				GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("AICar/AICar" + num));
				gameObject.transform.SetParent(aiCarGroup);
				switch (i)
				{
				case 0:
					childWayPointsIndex = 1419;
					break;
				case 1:
					childWayPointsIndex = 1400;
					break;
				case 2:
					childWayPointsIndex = 1520;
					break;
				case 3:
					childWayPointsIndex = 1113;
					break;
				case 4:
					childWayPointsIndex = 1358;
					break;
				case 5:
					childWayPointsIndex = 983;
					break;
				case 6:
					childWayPointsIndex = 333;
					break;
				case 7:
					childWayPointsIndex = 303;
					break;
				case 8:
					childWayPointsIndex = 1127;
					break;
				case 9:
					childWayPointsIndex = 863;
					break;
				case 10:
					childWayPointsIndex = 618;
					break;
				case 11:
					childWayPointsIndex = 1398;
					break;
				case 12:
					childWayPointsIndex = 1023;
					break;
				case 13:
					childWayPointsIndex = 898;
					break;
				case 14:
					childWayPointsIndex = 265;
					break;
				}
				gameObject.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(childWayPointsIndex).GetComponent<WayPoint>(), this);
			}
		}

		public void ResetCarPosition(GameObject gameObject)
		{
			if (!isInit)
			{
				Debug.LogError("调用前没有掉用[Init]方法进行初始化");
				return;
			}
			int index = 0;
			gameObject.GetComponent<AiCar>().SetWayPoint(waypointGroup.GetChild(index).GetComponent<WayPoint>(), this);
		}

		public Transform CreateAiCarGroup()
		{
			if (!isInit)
			{
				Debug.LogError("调用前没有掉用[Init]方法进行初始化");
				return null;
			}
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			obj.transform.SetParent(base.transform);
			Object.Destroy(obj.GetComponent<MeshRenderer>());
			Object.Destroy(obj.GetComponent<BoxCollider>());
			obj.name = "AiCarGroup";
			return obj.transform;
		}
	}
}
