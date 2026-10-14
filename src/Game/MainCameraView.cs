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

public class MainCameraView : MonoBehaviour
{
	public GameObject flashGroup;

	public GameObject rainBrushLeft;

	public GameObject rainBrushRight;

	public List<Sprite> rainBrushSpriteList = new List<Sprite>();

	public List<Image> blackImageList = new List<Image>();

	public List<Text> lightTextList = new List<Text>();

	public GameObject RightDuiXianView;

	public GameObject RightHouShiView;

	public Text GearT;

	public GameObject Lzhizhen;

	public GameObject Rzhizhen;

	public GameObject Rhoushijing;

	private float turnLightIntervalTime;

	private AudioSource safeBeltAudio;

	private float safeBeltFlashTime;

	private AudioSource turnLightAudio;

	private AudioSource fireAudioSource;

	private AudioSource hornAudio;

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

	private Color disableColor = new Color(0.42745098f, 0.6627451f, 0.6784314f, 0.23921569f);

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

	private Text speed;

	private Text engineSpeed;

	private GameObject gear;

	private GameObject gearIcon;

	private Text gearText;

	private Text sumScore;

	private Image fangxiangpan;

	private Image IMfangxiangpan;

	private Transform BG;

	private Image zhedangL;

	private Image zhedangR;

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
		if (ViewJumpCtl.Instance.otherType == 505)
		{
			IMfangxiangpan.color = new Color(0f, 0f, 0f);
			zhedangL.color = new Color(0f, 0f, 0f);
			zhedangR.color = new Color(0f, 0f, 0f);
			BG.transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 0f, 0f);
			BG.transform.GetChild(1).GetComponent<Image>().color = new Color(0f, 0f, 0f);
		}
		if (ViewJumpCtl.Instance.itemType == 603 || ViewJumpCtl.Instance.itemType == 607)
		{
			GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = true;
		}
		base.transform.SetAsFirstSibling();
		if (Screen.width / Screen.height >= 2)
		{
			RightDuiXianView.transform.localPosition = new Vector3(1230f, -60f, 0f);
			RightHouShiView.transform.localPosition = new Vector3(1230f, -60f, 0f);
			Rhoushijing.gameObject.SetActive(value: false);
		}
		if (DataCenter.Instance.UsbVersion == 64 || DataCenter.Instance.UsbVersion == 96 || DataCenter.Instance.UsbVersion == 128)
		{
			if (Screen.width / Screen.height >= 2)
			{
				RightDuiXianView.transform.localPosition = new Vector3(1200f, -40f, 0f);
				RightHouShiView.transform.localPosition = new Vector3(1200f, -40f, 0f);
			}
			else if (Screen.width / Screen.height < 2)
			{
				base.transform.localPosition -= new Vector3(0f, 10f, 0f);
			}
		}
		else
		{
			base.transform.localPosition -= new Vector3(0f, 10f, 0f);
		}
		GetComponent<RectTransform>().sizeDelta = base.transform.parent.GetComponent<RectTransform>().sizeDelta;
		if (ViewJumpCtl.Instance.otherType != 505)
		{
			return;
		}
		foreach (Image blackImage in blackImageList)
		{
			blackImage.color = new Color(10f / 51f, 10f / 51f, 10f / 51f);
		}
		foreach (Text lightText in lightTextList)
		{
			lightText.color = new Color(0f, 81f / 85f, 0.9098039f);
		}
	}

	private void Update()
	{
		UpdateSignalShow();
		if (CarState.Instance.CarSignal.gear != 6)
		{
			GearT.text = CarState.Instance.CarSignal.gear.ToString();
		}
		else if (CarState.Instance.CarSignal.gear == 6)
		{
			GearT.text = "R";
		}
		float num = Convert.ToSingle(engineSpeed.text) / 8000f * 277f;
		Lzhizhen.transform.localEulerAngles = new Vector3(0f, 0f, 360f - num);
		float num2 = Convert.ToSingle(speed.text) / 140f * 186f;
		Rzhizhen.transform.localEulerAngles = new Vector3(0f, 0f, 360f - num2 + 15f);
		float z = Convert.ToSingle(((0f - CarState.Instance.CarSignal.steerWheel) * CarSignalValue.SteerWheeMaxlAngle).ToString("f2"));
		fangxiangpan.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, z));
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
					JudgeIconShow(safeBelt, 47, new Color(0.42745098f, 0.6627451f, 0.6784314f, 0.23921569f));
					if (safeBeltFlashTime >= 1.2f)
					{
						safeBeltFlashTime = 0f;
					}
				}
			}
			else if (safeBeltAudio != null)
			{
				AudioSourceCtl.Instance.StopPlayAudio(safeBeltAudio);
				JudgeIconShow(safeBelt, 47, new Color(0.42745098f, 0.6627451f, 0.6784314f, 0.23921569f));
				safeBeltAudio = null;
			}
		}
		JudgeIconShow(door, 49, Color.red);
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
		else if (CarState.Instance.CarSignal.turnLight != 0 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
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
			if (ViewJumpCtl.Instance.modelType != 2 && ViewJumpCtl.Instance.modelType != 1)
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
		else if (CarState.Instance.CarSignal.horn == 1 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
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
			footbreak.fillAmount = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.oil;
		}
		else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1)
		{
			footbreak.fillAmount = CarState.Instance.CarSignal.oil;
		}
		if (SettingInfoCtl.Instance.isManualGear)
		{
			if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
			{
				cluth.fillAmount = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.cluth;
				oil.fillAmount = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake;
			}
			else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1)
			{
				cluth.fillAmount = CarState.Instance.CarSignal.cluth;
				oil.fillAmount = CarState.Instance.CarSignal.footBrake;
			}
		}
		else
		{
			if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
			{
				oil.fillAmount = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake;
			}
			else
			{
				oil.fillAmount = CarState.Instance.CarSignal.footBrake;
			}
			cluth.fillAmount = 0f;
		}
		if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel > 0f)
		{
			rightSteerWheel.fillAmount = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel;
			leftSteerWheel.fillAmount = 0f;
		}
		else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel < 0f)
		{
			leftSteerWheel.fillAmount = 0f - DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel;
			rightSteerWheel.fillAmount = 0f;
		}
		else if (CarState.Instance.CarSignal.steerWheel > 0f)
		{
			rightSteerWheel.fillAmount = CarState.Instance.CarSignal.steerWheel;
			leftSteerWheel.fillAmount = 0f;
		}
		else
		{
			leftSteerWheel.fillAmount = 0f - CarState.Instance.CarSignal.steerWheel;
			rightSteerWheel.fillAmount = 0f;
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
		if (SettingInfoCtl.Instance.isManualGear)
		{
			if (ViewJumpCtl.Instance.modelType == 0)
			{
				if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 0)
				{
					gear.GetComponent<Image>().color = Color.white;
					gearIcon.GetComponent<Image>().color = Color.white;
				}
				else
				{
					gear.GetComponent<Image>().color = gearColor;
					gearIcon.GetComponent<Image>().color = gearColor;
				}
			}
			else if (CarState.Instance.CarSignal.gear == 0)
			{
				gear.GetComponent<Image>().color = Color.white;
				gearIcon.GetComponent<Image>().color = Color.white;
			}
			else
			{
				gear.GetComponent<Image>().color = gearColor;
				gearIcon.GetComponent<Image>().color = gearColor;
			}
			if (ViewJumpCtl.Instance.modelType == 0)
			{
				if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear != preGear)
				{
					if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 0)
					{
						tempVec0 = new Vector3(gearPosition[preGear].x, 0f);
						targetPositon = gearPosition[DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear];
					}
					else
					{
						tempVec0 = new Vector3(gearPosition[DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear].x, 0f);
						targetPositon = gearPosition[DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear];
					}
					step = 0;
					preGear = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear;
				}
			}
			else if (CarState.Instance.CarSignal.gear != preGear)
			{
				if (CarState.Instance.CarSignal.gear == 0)
				{
					tempVec0 = new Vector3(gearPosition[preGear].x, 0f);
					targetPositon = gearPosition[CarState.Instance.CarSignal.gear];
				}
				else
				{
					tempVec0 = new Vector3(gearPosition[CarState.Instance.CarSignal.gear].x, 0f);
					targetPositon = gearPosition[CarState.Instance.CarSignal.gear];
				}
				step = 0;
				preGear = CarState.Instance.CarSignal.gear;
			}
			if (step == 0)
			{
				gearIcon.transform.localPosition = Vector3.Lerp(gearIcon.transform.localPosition, tempVec0, Time.deltaTime * 10f);
				if ((double)(gearIcon.transform.localPosition - tempVec0).magnitude < 0.5)
				{
					step = 1;
					gearIcon.transform.localPosition = tempVec0;
				}
			}
			else if (step == 1)
			{
				gearIcon.transform.localPosition = Vector3.Lerp(gearIcon.transform.localPosition, targetPositon, Time.deltaTime * 10f);
				if ((double)(gearIcon.transform.localPosition - targetPositon).magnitude < 0.5)
				{
					step = -1;
					gearIcon.transform.localPosition = targetPositon;
				}
			}
		}
		else if (ViewJumpCtl.Instance.modelType == 0 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
		{
			switch (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear)
			{
			case 1:
			case 4:
				gearText.text = "D";
				break;
			case 5:
				gearText.text = "P";
				break;
			case 2:
			case 3:
				gearText.text = "L";
				break;
			case 0:
				gearText.text = "N";
				break;
			case 6:
				gearText.text = "R";
				break;
			default:
				gearText.text = "N";
				break;
			}
		}
		else
		{
			switch (CarState.Instance.CarSignal.gear)
			{
			case 100:
				gearText.text = "D";
				break;
			case 101:
				gearText.text = "P";
				break;
			case 103:
				gearText.text = "L";
				break;
			case 104:
				gearText.text = "N";
				break;
			case 102:
				gearText.text = "R";
				break;
			default:
				gearText.text = "N";
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
			}
			else if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush == 1 && rainBrushSpeed != 1.2f)
			{
				rainBrushSpeed = 1.2f;
			}
			else if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush == 0 && rainBrushSpeed != 0f)
			{
				if (zValue < 3f || zValue > 355f)
				{
					rainBrushSpeed = 0f;
					Vector3 localEulerAngles = rainBrushLeft.transform.localEulerAngles;
					rainBrushLeft.transform.localEulerAngles = new Vector3(localEulerAngles.x, localEulerAngles.y, 0f);
					rainBrushRight.transform.localEulerAngles = new Vector3(localEulerAngles.x, localEulerAngles.y, 0f);
				}
				else
				{
					rainBrushSpeed = 1f;
				}
			}
		}
		else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1)
		{
			if (CarState.Instance.CarSignal.rainBrush == 2 && rainBrushSpeed != 2.3f)
			{
				rainBrushSpeed = 2.3f;
			}
			else if (CarState.Instance.CarSignal.rainBrush == 1 && rainBrushSpeed != 1.2f)
			{
				rainBrushSpeed = 1.2f;
			}
			else if (CarState.Instance.CarSignal.rainBrush == 0 && rainBrushSpeed != 0f)
			{
				if (zValue < 3f || zValue > 355f)
				{
					rainBrushSpeed = 0f;
					Vector3 localEulerAngles2 = rainBrushLeft.transform.localEulerAngles;
					rainBrushLeft.transform.localEulerAngles = new Vector3(localEulerAngles2.x, localEulerAngles2.y, 0f);
					rainBrushRight.transform.localEulerAngles = new Vector3(localEulerAngles2.x, localEulerAngles2.y, 0f);
				}
				else
				{
					rainBrushSpeed = 1f;
				}
			}
		}
		if (rainBrushSpeed != 0f)
		{
			if (zValue > 88f && zValue < 110f)
			{
				rainBrushDirect = -1f;
			}
			else if (zValue < 2f || zValue > 355f)
			{
				rainBrushDirect = 1f;
			}
			float z = rainBrushSpeed * 45f * Time.deltaTime * rainBrushDirect;
			rainBrushLeft.transform.Rotate(new Vector3(0f, 0f, z));
			rainBrushRight.transform.Rotate(new Vector3(0f, 0f, z));
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
		BG = base.transform.Find("BG");
		Transform transform = base.transform.Find("View");
		Transform transform2 = base.transform.Find("Mirror");
		zhedangL = BG.Find("zhedangL").GetComponent<Image>();
		zhedangR = transform2.Find("zhedangR").GetComponent<Image>();
		cluth = transform.Find("Penal/Cluth").GetComponent<Image>();
		oil = transform.Find("Penal/Oil").GetComponent<Image>();
		leftSteerWheel = transform.Find("Penal/SteerWheelLeft").GetComponent<Image>();
		rightSteerWheel = transform.Find("Penal/SteerWheelRight").GetComponent<Image>();
		gearText = transform.Find("GearText").GetComponent<Text>();
		gear = transform.Find("Gear").gameObject;
		gearIcon = gear.transform.Find("Icon").gameObject;
		gearIcon.transform.localPosition = gearPosition[6];
		Transform transform3 = transform.Find("LeftBtnSignal");
		turnLeft = transform3.Find("TurnLeft").GetComponent<Image>();
		safeBelt = transform3.Find("SafeBelt").GetComponent<Image>();
		door = transform3.Find("Door").GetComponent<Image>();
		handbreak = transform3.Find("Handbrake").GetComponent<Image>();
		Transform transform4 = transform.Find("RightBtnSignal");
		turnRight = transform4.Find("TurnRight").GetComponent<Image>();
		nearLight = transform4.Find("NearLight").GetComponent<Image>();
		farLight = transform4.Find("FarLight").GetComponent<Image>();
		widthLight = transform4.Find("WidthLight").GetComponent<Image>();
		fogLight = transform4.Find("FogLight").GetComponent<Image>();
		if (SettingInfoCtl.Instance.isManualGear)
		{
			gearText.gameObject.SetActive(value: false);
		}
		else
		{
			gear.SetActive(value: false);
			gearIcon.SetActive(value: false);
		}
		Transform transform5 = transform.Find("EleScreen");
		footbreak = transform5.Find("Footbreak").GetComponent<Image>();
		fangxiangpan = transform5.Find("Fangxiangpan").GetComponent<Image>();
		IMfangxiangpan = transform5.Find("Fangxiangpan/fangxiangpan").GetComponent<Image>();
		speed = transform5.Find("Speed").GetComponent<Text>();
		engineSpeed = transform5.Find("EngineSpeed").GetComponent<Text>();
	}
}
