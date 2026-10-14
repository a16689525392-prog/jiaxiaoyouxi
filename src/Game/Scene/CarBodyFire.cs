using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class CarBodyFire : YingJiXunLianCtlBase
	{
		private static CarBodyFire instance;

		public GameObject instanceFire;

		public bool JudgePlaySound;

		public float stopTime;

		public static CarBodyFire Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (JudgePlaySound || itemStep != 2)
			{
				return;
			}
			stopTime += Time.deltaTime;
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (stopTime > 7f && CarState.Instance.CurSpeed > 0.5f)
				{
					SoundPlayCtl.Instance.PlaySound("当车辆着火时,未及时停车", 100f);
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("车身起火项目结束");
					itemStep = 99;
				}
				else if (stopTime > 8.5f && CarState.Instance.CurSpeed < 0.5f)
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("车身起火项目结束");
					itemStep = 99;
					JudgePlaySound = true;
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
			{
				if (stopTime > 6f && DriveHistoryPlay.Instance.curCalculateSpeedH > 0.5f)
				{
					SoundPlayCtl.Instance.PlaySound("当车辆着火时,未及时停车", 100f);
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("车身起火项目结束");
					itemStep = 99;
				}
				else if (stopTime > 7.5f && DriveHistoryPlay.Instance.curCalculateSpeedH < 0.5f)
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("车身起火项目结束");
					itemStep = 99;
					JudgePlaySound = true;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType != 604)
			{
				return;
			}
			if (itemStep == 0 && other.name == "Start_Position3")
			{
				SoundPlayCtl.Instance.PlaySound("车身起火项目开始");
				itemStep = 1;
			}
			else if (itemStep != 2 && other.name == "Fire")
			{
				instanceFire = Object.Instantiate(Resources.Load("CarBodyFire/Prefabs/Fire")) as GameObject;
				GameObject gameObject = GameObject.Find("Frame");
				instanceFire.transform.parent = gameObject.transform;
				if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
				{
					instanceFire.transform.localPosition = new Vector3(0f, -1f, 2f);
				}
				if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
				{
					instanceFire.transform.localPosition = new Vector3(0f, -1.5f, 5f);
				}
				instanceFire.transform.localScale = new Vector3(10f, 10f, 5f);
				instanceFire.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
				itemStep = 2;
			}
			else if (itemStep != 99 && other.name == "CarBodyFireEnd")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("车身起火项目结束");
				itemStep = 99;
			}
		}
	}
}
