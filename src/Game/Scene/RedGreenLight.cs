using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Scene
{
	public class RedGreenLight : CityRoadSceneBase
	{
		private static RedGreenLight instance;

		private Color greenLightTextColor = Color.green;

		private Color redLightTextColor = Color.red;

		private Color yellowLightTextColor = Color.yellow;

		public GameObject timeText;

		public GameObject timeText1;

		public GameObject timeText2;

		public GameObject timeText3;

		private float RedLightTime = 30f;

		private float GreenLightTime = 30f;

		public bool isRed;

		private float RedLightTime2 = 30f;

		private float GreenLightTime2 = 30f;

		public bool isRed2;

		private float RedLightTime1 = 30f;

		private float GreenLightTime1 = 30f;

		public bool isGreen;

		private float RedLightTime3 = 30f;

		private float GreenLightTime3 = 30f;

		public bool isGreen2;

		private RaycastHit cityRoadHitInfo;

		public GameObject InstancePeople2;

		public GameObject InstancePeople3;

		public GameObject InstancePeople4;

		public GameObject InstancePeople20;

		public GameObject InstancePeople30;

		public GameObject InstancePeople40;

		private bool isInstanceMan2;

		private bool isInstanceMan3;

		private bool isInstanceMan4;

		public bool isInstanceMan20;

		public bool isInstanceMan30;

		public bool isInstanceMan40;

		private GameObject PrefabTiShiText;

		private bool isCommonStart;

		public GameObject redGreenLight;

		private GameObject greenLightShow;

		private GameObject redLightShow;

		private GameObject yellowLightShow;

		public GameObject redGreenLight1;

		private GameObject greenLightShow1;

		private GameObject redLightShow1;

		private GameObject yellowLightShow1;

		public GameObject redGreenLight2;

		private GameObject greenLightShow2;

		private GameObject redLightShow2;

		private GameObject yellowLightShow2;

		public GameObject redGreenLight3;

		private GameObject greenLightShow3;

		private GameObject redLightShow3;

		private GameObject yellowLightShow3;

		public GameObject InstancePeople;

		public GameObject InstancePeople1;

		private bool isPeoPle;

		private bool isPeoPle1;

		private int itemStep1;

		private int itemStep2;

		private int itemStep3;

		private bool isColliderMan;

		private bool isColliderOldMan;

		private bool isColliderGirl;

		public static RedGreenLight Instance => instance;

		private void Awake()
		{
			InitSignalLightGo();
			instance = this;
		}

		private void FixedUpdate()
		{
			if (ViewJumpCtl.Instance.itemType == 403 && !isCommonStart)
			{
				SoundPlayCtl.Instance.PlaySound("起步，汇入车流");
				PrefabTiShiText = Object.Instantiate(Resources.Load("TiShi/QiBuTiShiText")) as GameObject;
				GameObject gameObject = GameObject.Find("SceneText");
				PrefabTiShiText.transform.parent = gameObject.transform;
				PrefabTiShiText.transform.localPosition = new Vector3(0f, 130f, 0f);
				PrefabTiShiText.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
				PrefabTiShiText.transform.localScale = new Vector3(1f, 1f, 1f);
				isCommonStart = true;
			}
			if (CarState.Instance.CurSpeed > 5f)
			{
				Object.Destroy(PrefabTiShiText);
			}
			if (RedLightTime > 0f)
			{
				isRed = true;
			}
			else
			{
				isRed = false;
			}
			if (isRed)
			{
				RedLightTime -= Time.deltaTime;
				if (RedLightTime > 3f)
				{
					redLightShow.SetActive(value: true);
					greenLightShow.SetActive(value: false);
					yellowLightShow.SetActive(value: false);
					timeText.GetComponent<Text>().text = ((int)RedLightTime).ToString();
					timeText.GetComponent<Text>().color = redLightTextColor;
				}
				else if (RedLightTime > 0f && RedLightTime < 3f)
				{
					redLightShow.SetActive(value: false);
					greenLightShow.SetActive(value: false);
					yellowLightShow.SetActive(value: true);
					timeText.GetComponent<Text>().text = ((int)RedLightTime).ToString();
					timeText.GetComponent<Text>().color = yellowLightTextColor;
				}
			}
			else if (!isRed && RedLightTime < 0f)
			{
				GreenLightTime -= Time.deltaTime;
				redLightShow.SetActive(value: false);
				greenLightShow.SetActive(value: true);
				yellowLightShow.SetActive(value: false);
				timeText.GetComponent<Text>().text = ((int)GreenLightTime).ToString();
				timeText.GetComponent<Text>().color = greenLightTextColor;
			}
			if (GreenLightTime < 0f)
			{
				RedLightTime = 30f;
				GreenLightTime = 30f;
			}
			if (!isRed && !isInstanceMan40)
			{
				InstancePeople40 = Object.Instantiate(Resources.Load("AppearPerson/Girl1")) as GameObject;
				GameObject gameObject2 = GameObject.Find("ManStartPosition40");
				InstancePeople40.transform.parent = gameObject2.transform;
				InstancePeople40.transform.localPosition = new Vector3(0f, -0.2f, 0f);
				InstancePeople40.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
				InstancePeople40.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
				isInstanceMan40 = true;
			}
			if (GreenLightTime1 > 0f)
			{
				isGreen = true;
			}
			else
			{
				isGreen = false;
			}
			if (isGreen)
			{
				GreenLightTime1 -= Time.deltaTime;
				if (GreenLightTime1 > 3f)
				{
					redLightShow1.SetActive(value: false);
					greenLightShow1.SetActive(value: true);
					yellowLightShow1.SetActive(value: false);
					timeText1.GetComponent<Text>().text = ((int)GreenLightTime1).ToString();
					timeText1.GetComponent<Text>().color = greenLightTextColor;
				}
				else if (GreenLightTime1 > 0f && GreenLightTime1 < 3f)
				{
					redLightShow1.SetActive(value: false);
					greenLightShow1.SetActive(value: false);
					yellowLightShow1.SetActive(value: true);
					timeText1.GetComponent<Text>().text = ((int)GreenLightTime1).ToString();
					timeText1.GetComponent<Text>().color = yellowLightTextColor;
				}
			}
			else if (!isGreen && GreenLightTime1 < 0f)
			{
				RedLightTime1 -= Time.deltaTime;
				redLightShow1.SetActive(value: true);
				greenLightShow1.SetActive(value: false);
				yellowLightShow1.SetActive(value: false);
				timeText1.GetComponent<Text>().text = ((int)RedLightTime1).ToString();
				timeText1.GetComponent<Text>().color = redLightTextColor;
			}
			if (RedLightTime1 < 0f)
			{
				RedLightTime1 = 30f;
				GreenLightTime1 = 30f;
			}
			if (isGreen && !isInstanceMan20)
			{
				InstancePeople20 = Object.Instantiate(Resources.Load("AppearPerson/Man1")) as GameObject;
				GameObject gameObject3 = GameObject.Find("ManStartPosition20");
				InstancePeople20.transform.parent = gameObject3.transform;
				InstancePeople20.transform.localPosition = new Vector3(0f, -0.2f, 0f);
				InstancePeople20.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
				InstancePeople20.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
				isInstanceMan20 = true;
			}
			if (RedLightTime2 > 0f)
			{
				isRed2 = true;
			}
			else
			{
				isRed2 = false;
			}
			if (isRed2)
			{
				RedLightTime2 -= Time.deltaTime;
				if (RedLightTime2 > 3f)
				{
					redLightShow2.SetActive(value: true);
					greenLightShow2.SetActive(value: false);
					yellowLightShow2.SetActive(value: false);
					timeText2.GetComponent<Text>().text = ((int)RedLightTime2).ToString();
					timeText2.GetComponent<Text>().color = redLightTextColor;
				}
				else if (RedLightTime2 > 0f && RedLightTime2 < 3f)
				{
					redLightShow2.SetActive(value: false);
					greenLightShow2.SetActive(value: false);
					yellowLightShow2.SetActive(value: true);
					timeText2.GetComponent<Text>().text = ((int)RedLightTime2).ToString();
					timeText2.GetComponent<Text>().color = yellowLightTextColor;
				}
			}
			else if (!isRed2 && RedLightTime2 < 0f)
			{
				GreenLightTime2 -= Time.deltaTime;
				redLightShow2.SetActive(value: false);
				greenLightShow2.SetActive(value: true);
				yellowLightShow2.SetActive(value: false);
				timeText2.GetComponent<Text>().text = ((int)GreenLightTime2).ToString();
				timeText2.GetComponent<Text>().color = greenLightTextColor;
			}
			if (GreenLightTime2 < 0f)
			{
				RedLightTime2 = 30f;
				GreenLightTime2 = 30f;
			}
			if (!isRed2 && !isInstanceMan30)
			{
				InstancePeople30 = Object.Instantiate(Resources.Load("AppearPerson/OldMan1")) as GameObject;
				GameObject gameObject4 = GameObject.Find("ManStartPosition30");
				InstancePeople30.transform.parent = gameObject4.transform;
				InstancePeople30.transform.localPosition = new Vector3(0f, -0.2f, 0f);
				InstancePeople30.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
				InstancePeople30.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
				isInstanceMan30 = true;
			}
			if (GreenLightTime3 > 0f)
			{
				isGreen2 = true;
			}
			else
			{
				isGreen2 = false;
			}
			if (isGreen2)
			{
				GreenLightTime3 -= Time.deltaTime;
				if (GreenLightTime3 > 3f)
				{
					redLightShow3.SetActive(value: false);
					greenLightShow3.SetActive(value: true);
					yellowLightShow3.SetActive(value: false);
					timeText3.GetComponent<Text>().text = ((int)GreenLightTime3).ToString();
					timeText3.GetComponent<Text>().color = greenLightTextColor;
				}
				else if (GreenLightTime3 > 0f && GreenLightTime3 < 3f)
				{
					redLightShow3.SetActive(value: false);
					greenLightShow3.SetActive(value: false);
					yellowLightShow3.SetActive(value: true);
					timeText3.GetComponent<Text>().text = ((int)GreenLightTime3).ToString();
					timeText3.GetComponent<Text>().color = yellowLightTextColor;
				}
			}
			else if (!isGreen2 && GreenLightTime3 < 0f)
			{
				RedLightTime3 -= Time.deltaTime;
				redLightShow3.SetActive(value: true);
				greenLightShow3.SetActive(value: false);
				yellowLightShow3.SetActive(value: false);
				timeText3.GetComponent<Text>().text = ((int)RedLightTime3).ToString();
				timeText3.GetComponent<Text>().color = redLightTextColor;
			}
			if (RedLightTime3 < 0f)
			{
				RedLightTime3 = 30f;
				GreenLightTime3 = 30f;
			}
			if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out cityRoadHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out cityRoadHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out cityRoadHitInfo, 8))
			{
				if (cityRoadHitInfo.collider.name == "ManStart2" && !isInstanceMan2)
				{
					InstancePeople2 = Object.Instantiate(Resources.Load("AppearPerson/Man")) as GameObject;
					GameObject gameObject5 = GameObject.Find("ManStartPosition2");
					InstancePeople2.transform.parent = gameObject5.transform;
					InstancePeople2.transform.localPosition = new Vector3(0f, 0f, 0f);
					InstancePeople2.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
					InstancePeople2.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
					isInstanceMan2 = true;
				}
				if (cityRoadHitInfo.collider.name == "ManStart3" && !isInstanceMan3)
				{
					InstancePeople3 = Object.Instantiate(Resources.Load("AppearPerson/OldMan")) as GameObject;
					GameObject gameObject6 = GameObject.Find("ManStartPosition3");
					InstancePeople3.transform.parent = gameObject6.transform;
					InstancePeople3.transform.localPosition = new Vector3(-11f, 0.5f, 0f);
					InstancePeople3.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
					InstancePeople3.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
					isInstanceMan3 = true;
				}
				if (cityRoadHitInfo.collider.name == "ManStart4" && !isInstanceMan4)
				{
					InstancePeople4 = Object.Instantiate(Resources.Load("AppearPerson/Girl")) as GameObject;
					GameObject gameObject7 = GameObject.Find("ManStartPosition4");
					InstancePeople4.transform.parent = gameObject7.transform;
					InstancePeople4.transform.localPosition = new Vector3(8.5f, 0.15f, 0f);
					InstancePeople4.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
					InstancePeople4.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
					isInstanceMan4 = true;
				}
			}
		}

		private void InitSignalLightGo()
		{
			greenLightShow = redGreenLight.transform.Find("GreenLight").gameObject;
			redLightShow = redGreenLight.transform.Find("RedLight").gameObject;
			yellowLightShow = redGreenLight.transform.Find("YellowLight").gameObject;
			greenLightShow1 = redGreenLight1.transform.Find("GreenLight1").gameObject;
			redLightShow1 = redGreenLight1.transform.Find("RedLight1").gameObject;
			yellowLightShow1 = redGreenLight1.transform.Find("YellowLight1").gameObject;
			greenLightShow2 = redGreenLight2.transform.Find("GreenLight2").gameObject;
			redLightShow2 = redGreenLight2.transform.Find("RedLight2").gameObject;
			yellowLightShow2 = redGreenLight2.transform.Find("YellowLight2").gameObject;
			greenLightShow3 = redGreenLight3.transform.Find("GreenLight3").gameObject;
			redLightShow3 = redGreenLight3.transform.Find("RedLight3").gameObject;
			yellowLightShow3 = redGreenLight3.transform.Find("YellowLight3").gameObject;
		}

		public override void TriggerEvent(Collider other)
		{
			if (ViewJumpCtl.Instance.itemType != 511 && ViewJumpCtl.Instance.itemType != 403)
			{
				return;
			}
			if (other.name == "StartMan" && !isPeoPle)
			{
				InstancePeople = Object.Instantiate(Resources.Load("AppearPerson/People0")) as GameObject;
				GameObject gameObject = GameObject.Find("ManStartPosition0");
				InstancePeople.transform.parent = gameObject.transform;
				InstancePeople.transform.localPosition = new Vector3(0f, 0f, 0f);
				InstancePeople.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
				isPeoPle = true;
			}
			else if (other.name == "Start" && itemStep == 0)
			{
				Debug.Log("ppp");
				SoundPlayCtl.Instance.PlaySound("前方十字路口,请注意红绿灯");
				itemStep = 1;
			}
			else if (other.name == "Man1(Clone)" && !isColliderMan)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople20);
				isColliderMan = true;
				Debug.Log(isColliderMan);
			}
			else if (other.name == "Girl1(Clone)" && !isColliderGirl)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople40);
				isColliderGirl = true;
			}
			else if (other.name == "OldMan1(Clone)" && !isColliderOldMan)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople30);
				isColliderOldMan = true;
			}
			else if (other.name == "BanMaXian" && itemStep == 1)
			{
				if (isRed)
				{
					SoundPlayCtl.Instance.PlaySound("斑马线前未停车", 100f);
				}
				itemStep = 2;
			}
			else if (other.name == "RunRedLight" && itemStep == 2)
			{
				if (isRed)
				{
					SoundPlayCtl.Instance.PlaySound("闯红灯", 100f);
				}
				itemStep = 0;
				isColliderMan = false;
			}
			if (other.name == "StartMan1" && !isPeoPle1)
			{
				InstancePeople1 = Object.Instantiate(Resources.Load("AppearPerson/People1")) as GameObject;
				GameObject gameObject2 = GameObject.Find("ManStartPosition1");
				InstancePeople1.transform.parent = gameObject2.transform;
				InstancePeople1.transform.localPosition = new Vector3(0f, 0f, 0f);
				InstancePeople1.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
				InstancePeople1.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
				isPeoPle1 = true;
			}
			else if (other.name == "Start1" && itemStep1 == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方十字路口,请注意红绿灯");
				itemStep1 = 1;
			}
			else if (other.name == "Man1(Clone)" && !isColliderMan)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople20);
				isColliderMan = true;
			}
			else if (other.name == "Girl1(Clone)" && !isColliderGirl)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople40);
				isColliderGirl = true;
			}
			else if (other.name == "OldMan1(Clone)" && !isColliderOldMan)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople30);
				isColliderOldMan = true;
			}
			else if (other.name == "BanMaXian1" && itemStep1 == 1)
			{
				if (!isGreen)
				{
					SoundPlayCtl.Instance.PlaySound("斑马线前未停车", 100f);
				}
				itemStep1 = 2;
			}
			else if (other.name == "RunRedLight1" && itemStep1 == 2)
			{
				if (!isGreen)
				{
					SoundPlayCtl.Instance.PlaySound("闯红灯", 100f);
				}
				itemStep1 = 0;
				isColliderGirl = false;
			}
			if (other.name == "Start2" && itemStep2 == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方十字路口,请注意红绿灯");
				itemStep2 = 1;
			}
			else if (other.name == "Man1(Clone)" && !isColliderMan)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople20);
				isColliderMan = true;
			}
			else if (other.name == "Girl1(Clone)" && !isColliderGirl)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople40);
				isColliderGirl = true;
			}
			else if (other.name == "OldMan1(Clone)" && !isColliderOldMan)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople30);
				isColliderOldMan = true;
			}
			else if (other.name == "BanMaXian2" && itemStep2 == 1)
			{
				if (isRed2)
				{
					SoundPlayCtl.Instance.PlaySound("斑马线前未停车", 100f);
				}
				itemStep2 = 2;
			}
			else if (other.name == "RunRedLight2" && itemStep2 == 2)
			{
				if (isRed2)
				{
					SoundPlayCtl.Instance.PlaySound("闯红灯", 100f);
				}
				itemStep2 = 0;
				isColliderMan = false;
			}
			if (other.name == "Start3" && itemStep3 == 0)
			{
				SoundPlayCtl.Instance.PlaySound("前方十字路口,请注意红绿灯");
				itemStep3 = 1;
			}
			else if (other.name == "Man1(Clone)" && !isColliderMan)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople20);
				isColliderMan = true;
			}
			else if (other.name == "Girl1(Clone)" && !isColliderGirl)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople40);
				isColliderGirl = true;
			}
			else if (other.name == "OldMan1(Clone)" && !isColliderOldMan)
			{
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				Object.Destroy(InstancePeople30);
				isColliderOldMan = true;
			}
			else if (other.name == "BanMaXian3" && itemStep3 == 1)
			{
				if (!isGreen2)
				{
					SoundPlayCtl.Instance.PlaySound("斑马线前未停车", 100f);
				}
				itemStep3 = 2;
			}
			else if (other.name == "RunRedLight3" && itemStep3 == 2)
			{
				if (!isGreen2)
				{
					SoundPlayCtl.Instance.PlaySound("闯红灯", 100f);
				}
				itemStep3 = 0;
				isColliderOldMan = false;
			}
		}
	}
}
