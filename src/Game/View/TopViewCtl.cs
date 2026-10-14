using Car;
using Global;
using UnityEngine;

namespace View
{
	public class TopViewCtl : MonoBehaviour
	{
		public AddCameraRenderTexture rt;

		private void Start()
		{
			if (ViewJumpCtl.Instance.trainType == TrainType.BasicTrain && ViewJumpCtl.Instance.itemType == 300)
			{
				rt.SetCarCameraType(CarCameraType.WheelCamera);
			}
			if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa || SettingInfoCtl.Instance.carType == CarType.DaBaChe || SettingInfoCtl.Instance.carType == CarType.TianJin || SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
			{
				base.transform.GetChild(2).gameObject.SetActive(value: false);
				base.transform.GetChild(3).gameObject.SetActive(value: true);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
			{
				base.transform.GetChild(3).gameObject.SetActive(value: false);
				base.transform.GetChild(2).gameObject.SetActive(value: true);
			}
			if (SettingInfoCtl.Instance.carType == CarType.QiRuiE3)
			{
				base.transform.localPosition = new Vector3(40.25f, 0f, 0f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.PiKa)
			{
				base.transform.localPosition = new Vector3(115f, -3.5f, 0f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.AiLiSheManual || SettingInfoCtl.Instance.carType == CarType.AiLiSheAuto)
			{
				base.transform.localPosition = new Vector3(92f, 0f, 0f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
			{
				base.transform.localPosition = new Vector3(110f, 0f, 0f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
			{
				base.transform.localPosition = new Vector3(175f, 0f, 0f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1121)
			{
				base.transform.localPosition = new Vector3(-56.5f, -6.9f, 0f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.TianJin)
			{
				base.transform.localPosition = new Vector3(126f, 0f, 0f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1125)
			{
				base.transform.localPosition = new Vector3(-57.6f, 0f, 0f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
			{
				base.transform.localPosition = new Vector3(45.75f, -3.5f, 0f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.YongShi)
			{
				base.transform.localPosition = new Vector3(330f, -5f, 0f);
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QianYinChe)
			{
				base.transform.localPosition = new Vector3(45.75f, -3.5f, 0f);
			}
		}
	}
}
