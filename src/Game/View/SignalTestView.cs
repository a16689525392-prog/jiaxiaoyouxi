using System;
using System.Collections.Generic;
using Car;
using Global;
using Scene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View
{
	public class SignalTestView : ViewBase
	{
		public GameObject moniProtocol;

		private Transform cluthShow;

		private Transform footbreakShow;

		private Transform oilShow;

		private Transform handbreakShow;

		private Transform steerShow;

		private List<Image> moniBtnOpenImageList = new List<Image>();

		private List<GameObject> moniBtnTestImageList = new List<GameObject>();

		private const float MAX_VALUE = 65536f;

		private Color curOpenColor = new Color(0f, 1f, 48f / 85f, 20f / 51f);

		private Color initColor = new Color(48f / 85f, 48f / 85f, 48f / 85f, 20f / 51f);

		private int index;

		public override void Start()
		{
			base.Start();
			SignalSourceCtl instance = SignalSourceCtl.Instance;
			instance.disConnect = (UnityAction)Delegate.Combine(instance.disConnect, new UnityAction(Reset));
			InitMoniProtocol();
			InitViewShow();
		}

		private void Update()
		{
			SignalSourceCtl.Instance.SetCircuitBoardSignalType(5);
			if (SignalSourceCtl.Instance.GetCircuitBoardSignalType() == 5 && SignalSourceCtl.Instance.GetReadBuffer(57) == 1)
			{
				SignalSourceCtl.Instance.SetWriteBuffer(4, 97);
			}
			UpdateMoniProtocolSignalShow();
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(1))
			{
				go.GetComponent<ToggleItem>().SetCheck();
			}
			else if (go == GetBtn(2))
			{
				go.GetComponent<ToggleItem>().SetCheck();
			}
		}

		private void InitViewShow()
		{
			if (!SettingInfoCtl.Instance.isJudgeFogLight)
			{
				moniBtnOpenImageList[4].gameObject.SetActive(value: false);
			}
			if (!SettingInfoCtl.Instance.isJudgeSafetyBelt)
			{
				moniBtnOpenImageList[5].gameObject.SetActive(value: false);
			}
			if (!SettingInfoCtl.Instance.isJudgeFogLight)
			{
				moniBtnOpenImageList[4].gameObject.SetActive(value: false);
			}
			if (!SettingInfoCtl.Instance.isJudgeSafetyBelt)
			{
				moniBtnOpenImageList[5].gameObject.SetActive(value: false);
			}
			if (SettingInfoCtl.Instance.isManualGear)
			{
				for (int i = 0; i < 4; i++)
				{
					moniBtnOpenImageList[18 + i].gameObject.SetActive(value: false);
				}
				return;
			}
			for (int j = 0; j < 6; j++)
			{
				moniBtnOpenImageList[12 + j].gameObject.SetActive(value: false);
			}
			cluthShow.gameObject.SetActive(value: false);
		}

		private void InitMoniProtocol()
		{
			cluthShow = moniProtocol.transform.GetChild(1).GetChild(0);
			footbreakShow = moniProtocol.transform.GetChild(1).GetChild(1);
			oilShow = moniProtocol.transform.GetChild(1).GetChild(2);
			handbreakShow = moniProtocol.transform.GetChild(1).GetChild(3);
			steerShow = moniProtocol.transform.GetChild(1).GetChild(4);
			for (int i = 0; i < moniProtocol.transform.GetChild(2).childCount; i++)
			{
				moniBtnOpenImageList.Add(moniProtocol.transform.GetChild(2).GetChild(i).GetComponent<Image>());
				moniBtnTestImageList.Add(moniProtocol.transform.GetChild(2).GetChild(i).Find("IsTestOverImg")
					.gameObject);
				}
			}

			public void UpdateMoniProtocolSignalShow()
			{
				cluthShow.Find("Value").GetComponent<Text>().text = (CarState.Instance.CarSignal.cluth * 100f).ToString("f0");
				cluthShow.Find("FillImage").GetComponent<Image>().fillAmount = CarState.Instance.CarSignal.cluth;
				footbreakShow.Find("Value").GetComponent<Text>().text = (CarState.Instance.CarSignal.footBrake * 100f).ToString("f0");
				footbreakShow.Find("FillImage").GetComponent<Image>().fillAmount = CarState.Instance.CarSignal.footBrake;
				oilShow.Find("Value").GetComponent<Text>().text = (CarState.Instance.CarSignal.oil * 100f).ToString("f0");
				oilShow.Find("FillImage").GetComponent<Image>().fillAmount = CarState.Instance.CarSignal.oil;
				handbreakShow.Find("Value").GetComponent<Text>().text = (CarState.Instance.CarSignal.handBrake * 100f).ToString("f0");
				handbreakShow.Find("FillImage").GetComponent<Image>().fillAmount = CarState.Instance.CarSignal.handBrake;
				steerShow.Find("SteerWheel").localRotation = Quaternion.Euler(new Vector3(0f, 0f, (0f - CarState.Instance.CarSignal.steerWheel) * CarSignalValue.SteerWheeMaxlAngle));
				steerShow.Find("Value").GetComponent<Text>().text = (CarState.Instance.CarSignal.steerWheel * CarSignalValue.SteerWheeMaxlAngle).ToString("f0");
				UpdateMoniBtnSignalShow();
			}

			private void UpdateMoniBtnSignalShow()
			{
				index = 0;
				SetImageColor(2, index);
				SetImageColor(0, ++index);
				SetImageColor(10, ++index);
				SetImageColor(11, ++index);
				SetImageColor(50, ++index);
				SetImageColor(46, ++index);
				SetImageColor(54, ++index);
				SetImageColor(55, ++index);
				SetImageColor(57, ++index);
				SetImageColor(52, ++index);
				SetImageColor(59, ++index);
				SetImageColor(4, ++index);
				SetImageColor(34, ++index);
				SetImageColor(35, ++index);
				SetImageColor(36, ++index);
				SetImageColor(37, ++index);
				SetImageColor(38, ++index);
				SetImageColor(40, ++index);
				SetImageColor(63, ++index);
				SetImageColor(67, ++index);
				SetImageColor(65, ++index);
				SetImageColor(66, ++index);
				SetImageColor(60, ++index);
				SetImageColor(204, ++index);
			}

			private void SetImageColor(int action, int index)
			{
				if (CarActionJudge.Instance.JudgeActionDone(action))
				{
					moniBtnOpenImageList[index].color = curOpenColor;
					moniBtnTestImageList[index].SetActive(value: true);
				}
				else
				{
					moniBtnOpenImageList[index].color = initColor;
				}
			}

			private void Reset()
			{
				UpdateMoniProtocolSignalShow();
				foreach (GameObject moniBtnTestImage in moniBtnTestImageList)
				{
					moniBtnTestImage.SetActive(value: false);
				}
			}

			private void OnDestroy()
			{
				SignalSourceCtl instance = SignalSourceCtl.Instance;
				instance.disConnect = (UnityAction)Delegate.Remove(instance.disConnect, new UnityAction(Reset));
			}
		}
	}
