using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class BusBlindArea : YingJiXunLianCtlBase
	{
		private static BusBlindArea instance;

		public GameObject InstancePeople;

		public static BusBlindArea Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (itemStep < 1)
			{
				if (InstancePeople != null)
				{
					Object.Destroy(InstancePeople);
				}
				SoundPlayCtl.Instance.PlaySound("通过公交盲区项目开始");
				itemStep = 1;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 1 && other.name == "InstancePeople")
			{
				InstancePeople = Object.Instantiate(Resources.Load("AppearPerson/People")) as GameObject;
				GameObject gameObject = GameObject.Find("ManStartPosition");
				InstancePeople.transform.parent = gameObject.transform;
				InstancePeople.transform.localPosition = new Vector3(0f, 0f, 0f);
				InstancePeople.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
				if (ViewJumpCtl.Instance.modelType == 2)
				{
					if (CarState.Instance.CurSpeed >= 6f)
					{
						SoundPlayCtl.Instance.PlaySound("通过公交盲区未减速行驶", 100f);
					}
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curCalculateSpeedH >= 6f)
				{
					SoundPlayCtl.Instance.PlaySound("通过公交盲区未减速行驶", 100f);
				}
				itemStep = 2;
			}
			else if (itemStep != 99 && other.name == "EndBusBlindArea")
			{
				SoundPlayCtl.Instance.PlaySound("通过公交盲区项目结束");
				itemStep = 99;
			}
			if (itemStep != 100 && (other.name == "child01_f_mediumpoly" || other.name == "sportive03_m_mediumpoly" || other.name == "cman0007FBXASC045tpFBXASC046Mesh01"))
			{
				Object.Destroy(InstancePeople);
				SoundPlayCtl.Instance.PlaySound("撞行人", 100f);
				itemStep = 100;
			}
		}
	}
}
