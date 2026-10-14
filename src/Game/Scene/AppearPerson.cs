using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class AppearPerson : YingJiXunLianCtlBase
	{
		private static AppearPerson instance;

		public GameObject InstancePeople;

		private float time;

		public static AppearPerson Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			time += Time.deltaTime;
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType != 601)
			{
				return;
			}
			if (itemStep != 1 && other.name == "Start_Position0")
			{
				SoundPlayCtl.Instance.PlaySound("出现行人项目开始");
				itemStep = 1;
			}
			else if (itemStep == 1 && other.name == "InstancePeople")
			{
				InstancePeople = Object.Instantiate(Resources.Load("AppearPerson/People")) as GameObject;
				GameObject gameObject = GameObject.Find("ManStartPosition");
				InstancePeople.transform.parent = gameObject.transform;
				InstancePeople.transform.localPosition = new Vector3(0f, 0f, 0f);
				Instance.InstancePeople.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
				itemStep = 2;
			}
			else if (itemStep == 2 && other.name == "CarSlowPosition")
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
				else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
				{
					if (DriveHistoryPlay.Instance.curCalculateSpeedH > 2f && (People.Instance.MoveSpeed == 1.85f || People.Instance.MoveSpeed == 1.9f))
					{
						SoundPlayCtl.Instance.PlaySound("遇行人未减速停车", 100f);
						itemStep = 3;
					}
					if (People.Instance.MoveSpeed == 2f || People.Instance.MoveSpeed == 0f)
					{
						itemStep = 3;
					}
				}
			}
			else if (itemStep == 3 && (other.name == "child01_f_mediumpoly" || other.name == "sportive03_m_mediumpoly" || other.name == "cman0007FBXASC045tpFBXASC046Mesh01"))
			{
				Object.Destroy(InstancePeople);
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				itemStep = 4;
			}
			else if (itemStep != 99 && other.name == "AppearPersonEnd")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("出现行人项目结束");
				itemStep = 99;
			}
		}
	}
}
