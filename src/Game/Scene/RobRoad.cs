using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class RobRoad : YingJiXunLianCtlBase
	{
		private static RobRoad instance;

		public GameObject InstanceElectricCarandMan;

		private int index;

		private float Rtiem;

		private bool issafe;

		public static RobRoad Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (itemStep < 1)
			{
				index = 0;
				issafe = true;
				GameObject gameObject = GameObject.Find("ElectricCarandManStartPosition");
				if (gameObject.transform.childCount != 0)
				{
					Object.Destroy(gameObject.transform.GetChild(0).gameObject);
				}
				ChengDongSceneModel.instance.ElectricCar.SetActive(value: true);
				SoundPlayCtl.Instance.PlaySound("非机动车抢道项目开始");
				itemStep = 1;
			}
			if (!(InstanceElectricCarandMan != null) || itemStep != 2)
			{
				return;
			}
			if (Vector3.Distance(CarCtl.instance.m_Rigidbody.transform.position, InstanceElectricCarandMan.transform.position) < 18f && index == 0)
			{
				index = 1;
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					ElectricCarandMan.intance.speed = 1.5f;
					if (CarState.Instance.CurSpeed >= 13f)
					{
						SoundPlayCtl.Instance.PlaySound("非机动车抢道时未减速行驶", 100f);
					}
				}
				else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
				{
					ElectricCarandMan.intance.speed = 2f;
					if (DriveHistoryPlay.Instance.curCalculateSpeedH >= 13f)
					{
						SoundPlayCtl.Instance.PlaySound("非机动车抢道时未减速行驶", 100f);
					}
				}
			}
			if (index >= 1 && Vector3.Distance(CarCtl.instance.m_Rigidbody.transform.position, InstanceElectricCarandMan.transform.position) < 10f && issafe)
			{
				SoundPlayCtl.Instance.PlaySound("未与非机动车保持安全距离", 100f);
				issafe = false;
			}
			if (index == 1)
			{
				InstanceElectricCarandMan.transform.localEulerAngles += new Vector3(0f, 0f, 1f);
				if (InstanceElectricCarandMan.transform.localEulerAngles.z > 36f)
				{
					index = 2;
					Rtiem = 0f;
				}
			}
			if (index == 2)
			{
				Rtiem += Time.deltaTime;
				if (Rtiem > 4f)
				{
					index = 3;
					Rtiem = 0f;
				}
			}
			if (index == 3)
			{
				InstanceElectricCarandMan.transform.localEulerAngles += new Vector3(0f, 0f, -1f);
				if (InstanceElectricCarandMan.transform.localEulerAngles.z < 2f)
				{
					index = 4;
					InstanceElectricCarandMan.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					ElectricCarandMan.intance.speed = 5f;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 1 && other.name == "InstanceElectricCarandMan")
			{
				InstanceElectricCarandMan = Object.Instantiate(Resources.Load("AppearPerson/ElectricCarandMan")) as GameObject;
				GameObject gameObject = GameObject.Find("ElectricCarandManStartPosition");
				InstanceElectricCarandMan.transform.parent = gameObject.transform;
				InstanceElectricCarandMan.transform.localPosition = new Vector3(0f, 0f, -0.06f);
				InstanceElectricCarandMan.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
				ChengDongSceneModel.instance.ElectricCar.SetActive(value: false);
				itemStep = 2;
			}
			else if (itemStep != 99 && other.name == "EndRobRoad")
			{
				SoundPlayCtl.Instance.PlaySound("非机动车抢道项目结束");
				itemStep = 99;
			}
			if (itemStep != 100 && other.name == "AE_Scooter_01_LOD1")
			{
				Object.Destroy(InstanceElectricCarandMan);
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				itemStep = 100;
			}
		}
	}
}
