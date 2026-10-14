using System;
using System.Collections.Generic;
using Car;
using Global;
using Scene;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using View;

public class NewMainCameraViewKaLuoLa : MonoBehaviour
{
	public GameObject flashGroup;

	public GameObject flashGroup1;

	public GameObject cameraGroup;

	public GameObject cameraGroup1;

	public GameObject rainBrushLeft;

	public GameObject rainBrushRight;

	public List<Sprite> rainBrushSpriteList = new List<Sprite>();

	public List<Image> blackImageList = new List<Image>();

	public List<Image> steerWheelImage = new List<Image>();

	public List<Text> lightTextList = new List<Text>();

	public GameObject RightCamera;

	public GameObject rightMirror;

	public GameObject Lzhizhen;

	public GameObject Rzhizhen;

	public GameObject RightMirrorNew;

	private float turnLightIntervalTime;

	private AudioSource safeBeltAudio;

	private float safeBeltFlashTime;

	private AudioSource turnLightAudio;

	private AudioSource fireAudioSource;

	private AudioSource hornAudio;

	private float steerWheelValue;

	private float speedChangDeltaTime;

	private float preShowSpeed;

	private float curSpeed;

	private Vector3[] gearPosition = new Vector3[7]
	{
		new Vector3(0f, 0f),
		new Vector3(-14f, 17f),
		new Vector3(-14f, -17f),
		new Vector3(0f, 17f),
		new Vector3(0f, -17f),
		new Vector3(14f, 17f),
		new Vector3(14f, -17f)
	};

	private int preGear;

	private Vector3 tempVec0;

	private Vector3 targetPositon;

	private int step = -1;

	private Color gearColor = new Color(0f, 1f, 81f / 85f);

	private float rainBrushSpeed;

	private float rainBrushDirect = 1f;

	private float zValue;

	private float RainBrushIntervalTime;

	private bool IsRainBrushInterval;

	private int RainBrushIntervalSpeed = 1;

	private Color disableColor = new Color(0.23529412f, 0.23529412f, 0.23529412f, 40f / 51f);

	private AudioSource windAudio;

	private Image turnRight;

	private Image turnLeft;

	private Image farLight;

	private Image nearLight;

	private Image widthLight;

	private Image fogLight;

	private Image safeBelt;

	private Image door;

	private Image handbreak;

	private Image cluth;

	private Image oil;

	private Image footbreak;

	private Image leftSteerWheel;

	private Image rightSteerWheel;

	private GameObject steerWheel;

	private Text speed;

	private Text engineSpeed;

	private GameObject gear;

	private GameObject gearIcon;

	private Text gearText;

	private Text manualGearText;

	private Text sumScore;

	private void Awake()
	{
		GetSignalGo();
		TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
		baseInstance.ResetAction = (UnityAction)Delegate.Combine(baseInstance.ResetAction, new UnityAction(Reset));
		gearIcon.transform.localPosition = gearPosition[0];
	}

	private void Start()
	{
		TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
		baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
		if (ViewJumpCtl.Instance.itemType == 603 || ViewJumpCtl.Instance.itemType == 607)
		{
			GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = true;
		}
		base.transform.SetAsFirstSibling();
		if (Screen.width / Screen.height >= 3)
		{
			RightMirrorNew.SetActive(value: false);
		}
		else if (Screen.width / Screen.height < 3)
		{
			RightMirrorNew.SetActive(value: true);
		}
		GetComponent<RectTransform>().sizeDelta = base.transform.parent.GetComponent<RectTransform>().sizeDelta;
		if (ViewJumpCtl.Instance.otherType == 505)
		{
			foreach (Image blackImage in blackImageList)
			{
				blackImage.color = new Color(10f / 51f, 10f / 51f, 10f / 51f);
			}
			foreach (Image item in steerWheelImage)
			{
				item.color = new Color(16f / 51f, 16f / 51f, 16f / 51f);
			}
			foreach (Text lightText in lightTextList)
			{
				lightText.color = new Color(0f, 81f / 85f, 0.9098039f);
			}
		}
		if (SettingInfoCtl.Instance.IsFangXiangPan)
		{
			leftSteerWheel.gameObject.SetActive(value: false);
			rightSteerWheel.gameObject.SetActive(value: false);
			steerWheel.gameObject.SetActive(value: true);
		}
		else
		{
			leftSteerWheel.gameObject.SetActive(value: true);
			rightSteerWheel.gameObject.SetActive(value: true);
			steerWheel.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		UpdateSignalShow();
		float num = Convert.ToSingle(engineSpeed.text) / 7000f * 250f;
		Lzhizhen.transform.localEulerAngles = new Vector3(0f, 0f, 360f - num + 25f);
		float num2 = Convert.ToSingle(speed.text) / 220f * 263f;
		Rzhizhen.transform.localEulerAngles = new Vector3(0f, 0f, 360f - num2 + 43f);
	}

	private void UpdateSignalShow()
	{
		UpdateBtnSignalShow();
		UpdateTurnLight();
		UpdateFire();
		UpdateHorn();
		UpdatePanelSignalShow();
		UpdateSpeedShow();
		UpdateGearShow();
		UpdateRainBrush();
		UpdateEngineWind();
	}

	private void ResetTimePlay()
	{
		fireAudioSource = null;
	}

	private void UpdateBtnSignalShow()
	{
		JudgeIconShow(farLight, 0, Color.blue);
		JudgeIconShow(nearLight, 2, Color.green);
		JudgeIconShow(widthLight, 4, Color.green);
		if (ViewJumpCtl.Instance.modelType != 0)
		{
			if (CarActionJudge.Instance.JudgeActionDone(47))
			{
				if (safeBeltAudio == null)
				{
					safeBeltAudio = AudioSourceCtl.Instance.PlayAudio("SafeBeltAlarm", isLoop: true);
					safeBeltAudio.volume = 0.2f;
				}
				safeBeltFlashTime += Time.deltaTime;
				if (safeBeltFlashTime < 0.6f)
				{
					JudgeIconShow(safeBelt, 47, Color.red);
				}
				else
				{
					JudgeIconShow(safeBelt, 47, new Color(0.23529412f, 0.23529412f, 0.23529412f, 40f / 51f));
					if (safeBeltFlashTime >= 1.2f)
					{
						safeBeltFlashTime = 0f;
					}
				}
			}
			else if (safeBeltAudio != null)
			{
				AudioSourceCtl.Instance.StopPlayAudio(safeBeltAudio);
				JudgeIconShow(safeBelt, 47, new Color(0.23529412f, 0.23529412f, 0.23529412f, 40f / 51f));
				safeBeltAudio = null;
			}
		}
		if (ViewJumpCtl.Instance.modelType != 0)
		{
			JudgeIconShow(door, 49, Color.red);
		}
		JudgeIconShow(fogLight, 50, Color.yellow);
	}

	private void UpdateTurnLight()
	{
		turnLightIntervalTime += Time.deltaTime;
		if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 0)
		{
			if (turnLightAudio == null)
			{
				turnLightAudio = AudioSourceCtl.Instance.PlayAudio("CarTurnLight", isLoop: true);
				turnLightAudio.volume = 0.8f;
			}
		}
		else if (CarState.Instance.CarSignal.turnLight != 0 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6 || ViewJumpCtl.Instance.modelType == 1))
		{
			if (turnLightAudio == null)
			{
				turnLightAudio = AudioSourceCtl.Instance.PlayAudio("CarTurnLight", isLoop: true);
				turnLightAudio.volume = 0.8f;
			}
		}
		else
		{
			turnLightIntervalTime = 0f;
			if (turnLightAudio != null)
			{
				AudioSourceCtl.Instance.StopPlayAudio(turnLightAudio);
				turnLightAudio = null;
			}
		}
		if (turnLightIntervalTime < 0.6f)
		{
			if (CarActionJudge.Instance.JudgeActionDone(52))
			{
				turnLeft.color = Color.green;
				turnRight.color = Color.green;
			}
			else
			{
				JudgeIconShow(turnLeft, 10, Color.green);
				JudgeIconShow(turnRight, 11, Color.green);
			}
		}
		else
		{
			turnLeft.color = disableColor;
			turnRight.color = disableColor;
			if (turnLightIntervalTime >= 1.2f)
			{
				turnLightIntervalTime = 0f;
			}
		}
	}

	private void UpdateFire()
	{
		if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
		{
			if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fire == 1 && fireAudioSource == null)
			{
				fireAudioSource = AudioSourceCtl.Instance.PlayAudio("CarFire", isLoop: true);
				if (fireAudioSource != null)
				{
					fireAudioSource.loop = false;
				}
			}
		}
		else
		{
			if (ViewJumpCtl.Instance.modelType != 2 && ViewJumpCtl.Instance.modelType != 6 && ViewJumpCtl.Instance.modelType != 1)
			{
				return;
			}
			if (CarState.Instance.FireOn)
			{
				if (fireAudioSource == null)
				{
					fireAudioSource = AudioSourceCtl.Instance.PlayAudio("CarFire", isLoop: true);
					if (fireAudioSource != null)
					{
						fireAudioSource.loop = false;
					}
				}
			}
			else if (fireAudioSource != null)
			{
				AudioSourceCtl.Instance.StopPlayAudio(fireAudioSource);
				fireAudioSource = null;
			}
		}
	}

	private void UpdateHorn()
	{
		if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn == 1)
		{
			if (hornAudio == null)
			{
				hornAudio = AudioSourceCtl.Instance.PlayAudio("CarHorn", isLoop: true);
			}
		}
		else if (CarState.Instance.CarSignal.horn == 1 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6 || ViewJumpCtl.Instance.modelType == 1))
		{
			if (hornAudio == null)
			{
				hornAudio = AudioSourceCtl.Instance.PlayAudio("CarHorn", isLoop: true);
			}
		}
		else if (hornAudio != null)
		{
			AudioSourceCtl.Instance.StopPlayAudio(hornAudio);
			hornAudio = null;
		}
	}

	private void UpdatePanelSignalShow()
	{
		JudgeIconShow(handbreak, 70, Color.red);
		if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
		{
			oil.fillAmount = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.oil;
		}
		else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6 || ViewJumpCtl.Instance.modelType == 1)
		{
			oil.fillAmount = CarState.Instance.CarSignal.oil;
		}
		if (SettingInfoCtl.Instance.isManualGear)
		{
			if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
			{
				cluth.fillAmount = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.cluth;
				footbreak.fillAmount = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake;
			}
			else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6 || ViewJumpCtl.Instance.modelType == 1)
			{
				cluth.fillAmount = CarState.Instance.CarSignal.cluth;
				footbreak.fillAmount = CarState.Instance.CarSignal.footBrake;
			}
		}
		else
		{
			if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
			{
				cluth.fillAmount = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake;
			}
			else
			{
				cluth.fillAmount = CarState.Instance.CarSignal.footBrake;
			}
			footbreak.fillAmount = 0f;
		}
		if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
		{
			if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel > 0f)
			{
				rightSteerWheel.fillAmount = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel;
				leftSteerWheel.fillAmount = 0f;
				steerWheelValue = 0f - DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel;
				steerWheel.transform.localRotation = Quaternion.Euler(new Vector4(0f, 0f, 0f + steerWheelValue * CarSignalValue.SteerWheeMaxlAngle, 0f));
			}
			else if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel < 0f)
			{
				leftSteerWheel.fillAmount = 0f - DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel;
				rightSteerWheel.fillAmount = 0f;
				steerWheelValue = 0f - DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel;
				steerWheel.transform.localRotation = Quaternion.Euler(new Vector4(0f, 0f, 0f + steerWheelValue * CarSignalValue.SteerWheeMaxlAngle, 0f));
			}
		}
		else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6 || ViewJumpCtl.Instance.modelType == 1)
		{
			if (CarState.Instance.CarSignal.steerWheel > 0f)
			{
				rightSteerWheel.fillAmount = CarState.Instance.CarSignal.steerWheel;
				leftSteerWheel.fillAmount = 0f;
				steerWheelValue = 0f - CarState.Instance.CarSignal.steerWheel;
				steerWheel.transform.localRotation = Quaternion.Euler(new Vector4(0f, 0f, 0f + steerWheelValue * CarSignalValue.SteerWheeMaxlAngle, 0f));
			}
			else
			{
				leftSteerWheel.fillAmount = 0f - CarState.Instance.CarSignal.steerWheel;
				rightSteerWheel.fillAmount = 0f;
				steerWheelValue = 0f - CarState.Instance.CarSignal.steerWheel;
				steerWheel.transform.localRotation = Quaternion.Euler(new Vector4(0f, 0f, 0f + steerWheelValue * CarSignalValue.SteerWheeMaxlAngle, 0f));
			}
		}
	}

	private void UpdateSpeedShow()
	{
		speedChangDeltaTime += Time.deltaTime;
		if (ViewJumpCtl.Instance.modelType == 1 && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
		{
			curSpeed = CarDriveHistoryPlay.Instance.curCalculateSpeed;
			engineSpeed.text = CarDriveHistoryPlay.Instance.curCalculateEngineSpeed.ToString("f0");
		}
		else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
		{
			curSpeed = DriveHistoryPlay.Instance.curCalculateSpeedH;
			engineSpeed.text = DriveHistoryPlay.Instance.curCalculateEngineSpeedH.ToString("f0");
		}
		else
		{
			curSpeed = CarState.Instance.CurSpeed;
			engineSpeed.text = CarState.Instance.CurEngineSpeed.ToString("f0");
		}
		if (curSpeed > 0.1f && curSpeed < 1f)
		{
			speed.text = "1";
			preShowSpeed = curSpeed;
		}
		else if (Mathf.Abs(curSpeed - preShowSpeed) > 1f)
		{
			speed.text = curSpeed.ToString("f0");
			preShowSpeed = curSpeed;
		}
		else if (speedChangDeltaTime > 1f)
		{
			speedChangDeltaTime = 0f;
			speed.text = curSpeed.ToString("f0");
			preShowSpeed = curSpeed;
		}
	}

	private void UpdateGearShow()
	{
		if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
		{
			if (SettingInfoCtl.Instance.isManualGear)
			{
				if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 0)
				{
					gear.GetComponent<Image>().color = Color.white;
					gearIcon.GetComponent<Image>().color = Color.white;
					manualGearText.color = Color.white;
				}
				else
				{
					gear.GetComponent<Image>().color = gearColor;
					gearIcon.GetComponent<Image>().color = gearColor;
					manualGearText.color = gearColor;
				}
			}
			else if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 0 || DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 104))
			{
				gear.GetComponent<Image>().color = Color.white;
				gearIcon.GetComponent<Image>().color = Color.white;
				gearText.color = Color.white;
			}
			else
			{
				gear.GetComponent<Image>().color = gearColor;
				gearIcon.GetComponent<Image>().color = gearColor;
				gearText.color = gearColor;
			}
		}
		else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6 || ViewJumpCtl.Instance.modelType == 1)
		{
			if (SettingInfoCtl.Instance.isManualGear)
			{
				if (CarState.Instance.CarSignal.gear == 0)
				{
					gear.GetComponent<Image>().color = Color.white;
					gearIcon.GetComponent<Image>().color = Color.white;
					manualGearText.color = Color.white;
				}
				else
				{
					gear.GetComponent<Image>().color = gearColor;
					gearIcon.GetComponent<Image>().color = gearColor;
					manualGearText.color = gearColor;
				}
			}
			else if (CarState.Instance.CarSignal.gear == 104)
			{
				gear.GetComponent<Image>().color = Color.white;
				gearIcon.GetComponent<Image>().color = Color.white;
				gearText.color = Color.white;
			}
			else
			{
				gear.GetComponent<Image>().color = gearColor;
				gearIcon.GetComponent<Image>().color = gearColor;
				gearText.color = gearColor;
			}
		}
		if (SettingInfoCtl.Instance.isManualGear)
		{
			if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
			{
				switch (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear)
				{
				case 1:
					manualGearText.text = "1";
					break;
				case 2:
					manualGearText.text = "2";
					break;
				case 3:
					manualGearText.text = "3";
					break;
				case 4:
					manualGearText.text = "4";
					break;
				case 5:
					manualGearText.text = "5";
					break;
				case 0:
					manualGearText.text = "N";
					break;
				case 6:
					manualGearText.text = "R";
					break;
				default:
					manualGearText.text = "N";
					break;
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6 || ViewJumpCtl.Instance.modelType == 1)
			{
				switch (CarState.Instance.CarSignal.gear)
				{
				case 1:
					manualGearText.text = "1";
					break;
				case 2:
					manualGearText.text = "2";
					break;
				case 3:
					manualGearText.text = "3";
					break;
				case 4:
					manualGearText.text = "4";
					break;
				case 5:
					manualGearText.text = "5";
					break;
				case 0:
					manualGearText.text = "N";
					break;
				case 6:
					manualGearText.text = "R";
					break;
				default:
					manualGearText.text = "N";
					break;
				}
			}
		}
		else if (ViewJumpCtl.Instance.modelType == 0 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
		{
			switch (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear)
			{
			case 1:
			case 4:
				gearText.text = "D档";
				break;
			case 5:
				gearText.text = "P档";
				break;
			case 0:
				gearText.text = "N档";
				break;
			case 6:
				gearText.text = "R档";
				break;
			default:
				gearText.text = "N档";
				break;
			}
		}
		else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
		{
			switch (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear)
			{
			case 100:
				gearText.text = "D档";
				break;
			case 101:
				gearText.text = "P档";
				break;
			case 104:
				gearText.text = "N档";
				break;
			case 102:
				gearText.text = "R档";
				break;
			default:
				gearText.text = "N档";
				break;
			}
		}
		else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6 || ViewJumpCtl.Instance.modelType == 1)
		{
			switch (CarState.Instance.CarSignal.gear)
			{
			case 100:
				gearText.text = "D档";
				break;
			case 101:
				gearText.text = "P档";
				break;
			case 104:
				gearText.text = "N档";
				break;
			case 102:
				gearText.text = "R档";
				break;
			default:
				gearText.text = "N档";
				break;
			}
		}
	}

	private void UpdateRainBrush()
	{
		zValue = rainBrushLeft.transform.localEulerAngles.z;
		if (zValue < 0f)
		{
			zValue = (360f - zValue) % 360f;
		}
		if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
		{
			if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush == 2 && rainBrushSpeed != 2.3f)
			{
				rainBrushSpeed = 2.3f;
				rainBrushLeft.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
				rainBrushRight.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
			}
			else if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush == 1 && rainBrushSpeed != 1.2f)
			{
				rainBrushSpeed = 1.2f;
				rainBrushLeft.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
				rainBrushRight.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
			}
			else if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush == 3 && rainBrushSpeed != 1.2f)
			{
				rainBrushSpeed = 1.2f;
				rainBrushLeft.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
				rainBrushRight.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
			}
			else if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush == 0 && rainBrushSpeed != 0f)
			{
				if (zValue < 3f || zValue > 355f)
				{
					rainBrushSpeed = 0f;
					Vector3 localEulerAngles = rainBrushLeft.transform.localEulerAngles;
					rainBrushLeft.transform.localEulerAngles = new Vector3(localEulerAngles.x, localEulerAngles.y, 0f);
					rainBrushRight.transform.localEulerAngles = new Vector3(localEulerAngles.x, localEulerAngles.y, 0f);
					rainBrushLeft.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[0];
					rainBrushRight.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[0];
				}
				else
				{
					rainBrushSpeed = 1f;
				}
			}
		}
		else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6 || ViewJumpCtl.Instance.modelType == 1)
		{
			if (CarState.Instance.CarSignal.rainBrush == 2 && rainBrushSpeed != 2.3f)
			{
				rainBrushSpeed = 2.3f;
				rainBrushLeft.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
				rainBrushRight.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
			}
			else if (CarState.Instance.CarSignal.rainBrush == 1 && rainBrushSpeed != 1.2f)
			{
				rainBrushSpeed = 1.2f;
				rainBrushLeft.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
				rainBrushRight.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
			}
			else if (CarState.Instance.CarSignal.rainBrush == 3 && rainBrushSpeed != 1.2f)
			{
				rainBrushSpeed = 1.2f;
				rainBrushLeft.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
				rainBrushRight.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[1];
			}
			else if (CarState.Instance.CarSignal.rainBrush == 0 && rainBrushSpeed != 0f)
			{
				if (zValue < 3f || zValue > 355f)
				{
					rainBrushSpeed = 0f;
					Vector3 localEulerAngles2 = rainBrushLeft.transform.localEulerAngles;
					rainBrushLeft.transform.localEulerAngles = new Vector3(localEulerAngles2.x, localEulerAngles2.y, 0f);
					rainBrushRight.transform.localEulerAngles = new Vector3(localEulerAngles2.x, localEulerAngles2.y, 0f);
					rainBrushLeft.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[0];
					rainBrushRight.GetComponentInChildren<Image>().sprite = rainBrushSpriteList[0];
				}
				else
				{
					rainBrushSpeed = 1f;
				}
			}
		}
		if (rainBrushSpeed == 0f)
		{
			return;
		}
		if (zValue > 88f && zValue < 110f)
		{
			rainBrushDirect = -1f;
		}
		else if (zValue < 2f || zValue > 355f)
		{
			if (CarState.Instance.CarSignal.rainBrush == 3 && !IsRainBrushInterval && rainBrushDirect == -1f)
			{
				RainBrushIntervalTime = 0f;
				RainBrushIntervalSpeed = 0;
				IsRainBrushInterval = true;
			}
			rainBrushDirect = 1f;
		}
		if (CarState.Instance.CarSignal.rainBrush == 3 && IsRainBrushInterval)
		{
			RainBrushIntervalTime += Time.deltaTime;
			if (RainBrushIntervalTime > 1f)
			{
				RainBrushIntervalTime = 0f;
				IsRainBrushInterval = false;
				RainBrushIntervalSpeed = 1;
			}
		}
		if (CarState.Instance.CarSignal.rainBrush == 3)
		{
			float z = rainBrushSpeed * 45f * Time.deltaTime * rainBrushDirect * (float)RainBrushIntervalSpeed;
			rainBrushLeft.transform.Rotate(new Vector3(0f, 0f, z));
			rainBrushRight.transform.Rotate(new Vector3(0f, 0f, z));
		}
		else
		{
			float z2 = rainBrushSpeed * 45f * Time.deltaTime * rainBrushDirect;
			rainBrushLeft.transform.Rotate(new Vector3(0f, 0f, z2));
			rainBrushRight.transform.Rotate(new Vector3(0f, 0f, z2));
		}
	}

	private void JudgeIconShow(Image icon, int action, Color color)
	{
		if (CarActionJudge.Instance.JudgeActionDone(action))
		{
			icon.color = color;
		}
		else
		{
			icon.color = disableColor;
		}
	}

	private void UpdateEngineWind()
	{
		if (ViewJumpCtl.Instance.modelType == 1 && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
		{
			curSpeed = CarDriveHistoryPlay.Instance.curCalculateSpeed;
		}
		else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
		{
			curSpeed = DriveHistoryPlay.Instance.curCalculateSpeedH;
		}
		else
		{
			curSpeed = CarState.Instance.CurSpeed;
		}
		if (curSpeed >= 20f && curSpeed <= 200f)
		{
			if (curSpeed >= 160f)
			{
				curSpeed = 160f;
			}
			if (windAudio == null)
			{
				windAudio = AudioSourceCtl.Instance.PlayAudio("AccelerationLow", isLoop: true);
			}
			windAudio.loop = true;
			windAudio.pitch = (1f / (curSpeed - 20f) + 0.5f) * ((curSpeed - 20f) / curSpeed);
		}
		else if (windAudio != null)
		{
			AudioSourceCtl.Instance.StopPlayAudio(windAudio);
			windAudio = null;
		}
	}

	private void Reset()
	{
		fireAudioSource = null;
	}

	private void GetSignalGo()
	{
		Transform transform = base.transform.Find("View");
		cluth = transform.Find("Penal/Cluth").GetComponent<Image>();
		oil = transform.Find("Penal/Oil").GetComponent<Image>();
		leftSteerWheel = transform.Find("Penal/SteerWheelLeft").GetComponent<Image>();
		rightSteerWheel = transform.Find("Penal/SteerWheelRight").GetComponent<Image>();
		steerWheel = transform.Find("Penal/SteerWheel").gameObject;
		gearText = transform.Find("Penal/Gear/GearText").GetComponent<Text>();
		manualGearText = transform.Find("Penal/Gear/ManualGearText").GetComponent<Text>();
		gear = transform.Find("Gear").gameObject;
		gearIcon = gear.transform.Find("Icon").gameObject;
		gearIcon.transform.localPosition = gearPosition[6];
		Transform transform2 = transform.Find("LeftBtnSignal");
		turnLeft = transform2.Find("TurnLeft").GetComponent<Image>();
		safeBelt = transform2.Find("SafeBelt").GetComponent<Image>();
		door = transform2.Find("Door").GetComponent<Image>();
		handbreak = transform2.Find("Handbrake").GetComponent<Image>();
		Transform transform3 = transform.Find("RightBtnSignal");
		turnRight = transform3.Find("TurnRight").GetComponent<Image>();
		nearLight = transform3.Find("NearLight").GetComponent<Image>();
		farLight = transform3.Find("FarLight").GetComponent<Image>();
		widthLight = transform3.Find("WidthLight").GetComponent<Image>();
		fogLight = transform3.Find("FogLight").GetComponent<Image>();
		if (SettingInfoCtl.Instance.isManualGear)
		{
			gearText.gameObject.SetActive(value: false);
			gear.SetActive(value: false);
			gearIcon.SetActive(value: false);
		}
		else
		{
			manualGearText.gameObject.SetActive(value: false);
			gear.SetActive(value: false);
			gearIcon.SetActive(value: false);
		}
		Transform transform4 = transform.Find("EleScreen");
		footbreak = transform4.Find("Footbreak").GetComponent<Image>();
		speed = transform4.Find("Speed").GetComponent<Text>();
		engineSpeed = transform4.Find("EngineSpeed").GetComponent<Text>();
	}
}
