using System;
using System.Collections;
using Car;
using Global;
using Scene;
using UnityEngine;
using View;

public class RainGlassCtl : MonoBehaviour
{
	public GameObject rainGlass;

	public GameObject rainGlass1;

	public GameObject rainGlass2;

	private GameObject leftRainBrush;

	private Material rainMaterial;

	private bool isShowRainBrush;

	private Coroutine cor;

	private void Start()
	{
		if (DataCenter.Instance.tripleState == 0)
		{
			rainGlass.SetActive(value: true);
			rainGlass1.SetActive(value: true);
			rainGlass2.SetActive(value: true);
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			rainGlass.SetActive(value: false);
			rainGlass1.SetActive(value: false);
			rainGlass2.SetActive(value: false);
		}
		else
		{
			rainGlass.SetActive(value: true);
			rainGlass1.SetActive(value: false);
			rainGlass2.SetActive(value: false);
			rainGlass.transform.localScale = new Vector3(Convert.ToSingle(Screen.width) / 1920f * 1.5f, 1.5f, 1.5f);
		}
		if (TrainSceneBase.BaseInstance.mainCamView != null)
		{
			if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<MainCameraViewOldJieDa>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<DFMainCameraView>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.YongShi || SettingInfoCtl.Instance.carType == CarType.DFMengShi)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewYongShi>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewDaBa>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.KaLuoLa)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewKaLuoLa>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QiRuiE3 || SettingInfoCtl.Instance.carType == CarType.PiKa || SettingInfoCtl.Instance.carType == CarType.AiLiSheManual || SettingInfoCtl.Instance.carType == CarType.AiLiSheAuto)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewQiRuiE3>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1121)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewJieFang>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.TianJin || SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewTianJin>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1125)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewJieFang>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QianYinChe)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewQianYinChe>().rainBrushLeft;
			}
		}
		rainMaterial = rainGlass.GetComponent<Renderer>().material;
		rainMaterial.SetFloat("_DeltaAngle", 0f);
		rainMaterial.SetFloat("_AppearDegree", 20f);
	}

	private void Update()
	{
		if (!rainGlass.activeSelf)
		{
			return;
		}
		if (leftRainBrush == null)
		{
			if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<MainCameraViewOldJieDa>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<DFMainCameraView>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.YongShi || SettingInfoCtl.Instance.carType == CarType.DFMengShi)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewYongShi>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewDaBa>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.KaLuoLa)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewKaLuoLa>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QiRuiE3 || SettingInfoCtl.Instance.carType == CarType.PiKa || SettingInfoCtl.Instance.carType == CarType.AiLiSheManual || SettingInfoCtl.Instance.carType == CarType.AiLiSheAuto)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewQiRuiE3>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1121)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewJieFang>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.TianJin || SettingInfoCtl.Instance.carType == CarType.DFKangMingSi)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewTianJin>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.JieFang1125)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewJieFang>().rainBrushLeft;
			}
			else if (SettingInfoCtl.Instance.carType == CarType.QianYinChe)
			{
				leftRainBrush = TrainSceneBase.BaseInstance.mainCamView.GetComponent<NewMainCameraViewQianYinChe>().rainBrushLeft;
			}
		}
		if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
		{
			if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush != 0 && !isShowRainBrush)
			{
				if (cor != null)
				{
					StopCoroutine(cor);
					cor = null;
				}
				rainMaterial.SetFloat("_DeltaAngle", leftRainBrush.transform.localEulerAngles.z);
				if (leftRainBrush.transform.localEulerAngles.z > 80f)
				{
					isShowRainBrush = true;
				}
				else if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush == 0 && isShowRainBrush && cor == null)
				{
					cor = StartCoroutine(ShowRainGlass(2f));
					rainMaterial.SetFloat("_AppearDegree", 20f);
				}
			}
		}
		else if (CarState.Instance.CarSignal.rainBrush != 0 && !isShowRainBrush)
		{
			if (cor != null)
			{
				StopCoroutine(cor);
				cor = null;
			}
			rainMaterial.SetFloat("_DeltaAngle", leftRainBrush.transform.localEulerAngles.z);
			if (leftRainBrush.transform.localEulerAngles.z > 88f)
			{
				isShowRainBrush = true;
			}
		}
		else if (CarState.Instance.CarSignal.rainBrush == 0 && isShowRainBrush && cor == null)
		{
			cor = StartCoroutine(ShowRainGlass(2f));
			rainMaterial.SetFloat("_AppearDegree", 20f);
		}
	}

	private IEnumerator ShowRainGlass(float showTime)
	{
		float time = 0f;
		yield return new WaitForSeconds(3f);
		while (showTime > 0f && time / showTime < 1f)
		{
			time += Time.deltaTime;
			float value = 20f + time / showTime * 108f;
			rainMaterial.SetFloat("_AppearDegree", value);
			yield return 0;
		}
		rainMaterial.SetFloat("_DeltaAngle", 0f);
		rainMaterial.SetFloat("_AppearDegree", 20f);
		isShowRainBrush = false;
		cor = null;
	}
}
