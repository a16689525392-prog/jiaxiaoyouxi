using Car;
using Global;
using Scene;
using UnityEngine;
using View;

public class SnowGlassCtl : MonoBehaviour
{
	public GameObject snowBrush;

	public GameObject snowBrush1;

	private Transform leftRainBrush;

	private Vector3 initAngles;

	private void Start()
	{
		if (ViewJumpCtl.Instance.otherType != 502)
		{
			base.gameObject.SetActive(value: false);
		}
		initAngles = snowBrush.transform.localEulerAngles;
		if (TrainSceneBase.BaseInstance.mainCamView != null)
		{
			if (SettingInfoCtl.Instance.carType == CarType.JieDaManual || SettingInfoCtl.Instance.carType == CarType.JieDaAuto)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<MainCameraViewOldJieDa>().rainBrushLeft.transform;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<DFMainCameraView>().rainBrushLeft.transform;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.YongShi || SettingInfoCtl.Instance.carType == CarType.DFMengShi)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewYongShi>().rainBrushLeft.transform;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewDaBa>().rainBrushLeft.transform;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.KaLuoLa)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewKaLuoLa>().rainBrushLeft.transform;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QiRuiE3 || SettingInfoCtl.Instance.carType == CarType.PiKa || SettingInfoCtl.Instance.carType == CarType.AiLiSheManual || SettingInfoCtl.Instance.carType == CarType.AiLiSheAuto)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewQiRuiE3>().rainBrushLeft.transform;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1121 || SettingInfoCtl.Instance.carType == CarType.JieFang1125)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewJieFang>().rainBrushLeft.transform;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.TianJin || SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewTianJin>().rainBrushLeft.transform;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QianYinChe)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewQianYinChe>().rainBrushLeft.transform;
			}
		}
		GetComponent<ParticleSystem>().Play();
	}

	private void Update()
	{
		snowBrush.transform.localEulerAngles = new Vector3(initAngles.x, initAngles.y, leftRainBrush.localEulerAngles.z);
		snowBrush1.transform.localEulerAngles = new Vector3(initAngles.x, initAngles.y, leftRainBrush.localEulerAngles.z);
	}
}
