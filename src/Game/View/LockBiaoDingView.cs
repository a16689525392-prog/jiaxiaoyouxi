using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Car;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class LockBiaoDingView : ViewBase
	{
		public GameObject gameProtocolView;

		public GameObject itemView;

		public Text tipsText;

		private int calCount = 30;

		private List<LockCalibrationData> lockCalInfoList = new List<LockCalibrationData>();

		private bool isSave;

		private List<Text> itemValueList = new List<Text>();

		private List<Image> itemValueBgList = new List<Image>();

		private Color notCalColor = new Color(0.3372549f, 0.3372549f, 0.3372549f, 23f / 51f);

		private Color curSelectColor = new Color(0f, 1f, 48f / 85f, 0.5f);

		private Color calColor = new Color(0f, 0.47843137f, 0.69803923f, 0.5f);

		private List<CalTips> calTipsList = new List<CalTips>();

		private Transform xyAxis;

		private Transform zAxis;

		private Transform rAxis;

		private Transform uAxis;

		private Transform vAxis;

		private Transform povAxis;

		private Transform btnSignal;

		private Transform pov;

		private List<GameObject> gameBtnSignalList = new List<GameObject>();

		private Color curOpenColor = new Color(0f, 1f, 48f / 85f, 20f / 51f);

		private Color initColor = new Color(48f / 85f, 48f / 85f, 48f / 85f, 20f / 51f);

		private JoyOriginalData joydata;

		private const float MAX_VALUE = 65535f;

		private int showTipsIndex;

		private JoyOriginalData preOriData;

		private JoyOriginalData curOriData;

		private List<int> btnRecordList = new List<int>();

		private bool isjudgePersistent;

		private float intervalTime;

		private float changeValue;

		private LockCalibrationData maxChangeJoyData = new LockCalibrationData();

		public override void Awake()
		{
			base.Awake();
			ReadCalibrationXml();
			tipsText.text = calTipsList[showTipsIndex++].content;
			InitGameProtocol();
			InitItemGroup();
		}

		private void Update()
		{
			UpdateSignalShow();
			CalibrationSignal();
			UpdateShowCalStatus();
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				if (showTipsIndex == 1)
				{
					tipsText.text = calTipsList[showTipsIndex++].content;
					preOriData = SignalSourceCtl.Instance.joyOriData;
					btnRecordList.Add((int)(100 + preOriData.dwPOV));
					return;
				}
				LockCalibrationData item = new LockCalibrationData();
				lockCalInfoList.Add(item);
				changeValue = 0f;
				if (lockCalInfoList.Count == 2)
				{
					lockCalInfoList.Add(new LockCalibrationData());
					tipsText.text = calTipsList[showTipsIndex++].content;
				}
				if (showTipsIndex < calTipsList.Count)
				{
					tipsText.text = calTipsList[showTipsIndex++].content;
					preOriData = SignalSourceCtl.Instance.joyOriData;
				}
			}
			else if (go == GetBtn(1))
			{
				StartCoroutine(SaveCalData());
				isSave = true;
				ViewJumpCtl.Instance.ShowMsgTips("保存成功");
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}

		private void InitItemGroup()
		{
			Transform transform = itemView.transform.Find("ItemGroup");
			for (int i = 0; i < transform.childCount; i++)
			{
				itemValueList.Add(transform.GetChild(i).Find("ItemValue").GetComponent<Text>());
				itemValueBgList.Add(transform.GetChild(i).Find("ValueBg").GetComponent<Image>());
			}
			if (SettingInfoCtl.Instance.isManualGear)
			{
				for (int j = 0; j < 4; j++)
				{
					itemValueList[12 + j].transform.parent.gameObject.SetActive(value: false);
				}
				return;
			}
			itemValueList[2].transform.parent.gameObject.SetActive(value: false);
			for (int k = 0; k < 6; k++)
			{
				itemValueList[6 + k].transform.parent.gameObject.SetActive(value: false);
			}
		}

		private void UpdateShowCalStatus()
		{
			int num = 0;
			foreach (LockCalibrationData lockCalInfo in lockCalInfoList)
			{
				switch (lockCalInfo.type)
				{
				case 1:
					itemValueBgList[num].color = calColor;
					itemValueList[num].text = (lockCalInfo.btnIndex + 1).ToString();
					break;
				case 2:
					itemValueBgList[num].color = calColor;
					itemValueList[num].text = "X轴";
					break;
				case 3:
					itemValueBgList[num].color = calColor;
					itemValueList[num].text = "Y轴";
					break;
				case 4:
					itemValueBgList[num].color = calColor;
					itemValueList[num].text = "Z轴";
					break;
				case 5:
					itemValueBgList[num].color = calColor;
					itemValueList[num].text = "R轴";
					break;
				case 6:
					itemValueBgList[num].color = calColor;
					itemValueList[num].text = "U轴";
					break;
				case 7:
					itemValueBgList[num].color = calColor;
					itemValueList[num].text = "V轴";
					break;
				case 8:
				case 10:
					itemValueBgList[num].color = calColor;
					itemValueList[num].text = "P轴";
					break;
				case 9:
					itemValueBgList[num].color = notCalColor;
					itemValueList[num].text = "无";
					break;
				}
				num++;
			}
			if (num < calCount)
			{
				itemValueBgList[num].color = curSelectColor;
				itemValueList[num].text = "无";
			}
			num++;
			for (int i = num; i < calCount; i++)
			{
				itemValueBgList[num].color = notCalColor;
				itemValueList[num].text = "无";
			}
		}

		private IEnumerator SaveCalData()
		{
			if (lockCalInfoList.Count < calCount)
			{
				for (int i = lockCalInfoList.Count; i < calCount; i++)
				{
					LockCalibrationData item = new LockCalibrationData();
					lockCalInfoList.Add(item);
				}
			}
			lockCalInfoList[22].isPersistentSignal = true;
			lockCalInfoList[23].isPersistentSignal = true;
			SignalSourceCtl.Instance.lockSignalReader.SaveLockCalFile(lockCalInfoList);
			yield return 0;
			SignalSourceCtl.Instance.lockSignalReader.ReadLockCalFile();
		}

		private void ReadCalibrationXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Resources.Load<TextAsset>("LockCalxml").text);
			foreach (XmlElement childNode in xmlDocument.SelectSingleNode("tips").ChildNodes)
			{
				CalTips calTips = new CalTips();
				calTips.calIndex = int.Parse(childNode.GetAttribute("id"));
				calTips.content = childNode.InnerText;
				calTipsList.Add(calTips);
			}
		}

		private void InitGameProtocol()
		{
			xyAxis = gameProtocolView.transform.GetChild(1).GetChild(0);
			zAxis = gameProtocolView.transform.GetChild(1).GetChild(1);
			rAxis = gameProtocolView.transform.GetChild(1).GetChild(2);
			uAxis = gameProtocolView.transform.GetChild(1).GetChild(3);
			vAxis = gameProtocolView.transform.GetChild(1).GetChild(4);
			povAxis = gameProtocolView.transform.GetChild(1).GetChild(5);
			btnSignal = gameProtocolView.transform.GetChild(2);
			pov = gameProtocolView.transform.GetChild(3);
			for (int i = 0; i < btnSignal.childCount; i++)
			{
				gameBtnSignalList.Add(btnSignal.GetChild(i).gameObject);
			}
		}

		private void UpdateSignalShow()
		{
			joydata = SignalSourceCtl.Instance.joyOriData;
			xyAxis.Find("JiaHao").transform.localPosition = new Vector3(100f * ((float)joydata.dwXpos / 65535f - 0.5f), 100f * ((float)joydata.dwYpos / 65535f - 0.5f));
			zAxis.Find("Value").GetComponent<Text>().text = joydata.dwZpos.ToString("f0");
			zAxis.Find("SliderImage").GetComponent<Image>().fillAmount = (float)joydata.dwZpos / 65535f;
			rAxis.Find("Value").GetComponent<Text>().text = joydata.dwRpos.ToString("f0");
			rAxis.Find("SliderImage").GetComponent<Image>().fillAmount = (float)joydata.dwRpos / 65535f;
			uAxis.Find("Value").GetComponent<Text>().text = joydata.dwUpos.ToString("f0");
			uAxis.Find("SliderImage").GetComponent<Image>().fillAmount = (float)joydata.dwUpos / 65535f;
			vAxis.Find("Value").GetComponent<Text>().text = joydata.dwVpos.ToString("f0");
			vAxis.Find("SliderImage").GetComponent<Image>().fillAmount = (float)joydata.dwVpos / 65535f;
			povAxis.Find("Value").GetComponent<Text>().text = joydata.dwPOV.ToString("f0");
			povAxis.Find("SliderImage").GetComponent<Image>().fillAmount = (float)joydata.dwPOV / 65535f;
			for (int i = 0; i < 4; i++)
			{
				if (joydata.dwPOV == 9000 * i)
				{
					pov.GetChild(i + 1).GetComponent<Image>().color = curOpenColor;
				}
				else
				{
					pov.GetChild(i + 1).GetComponent<Image>().color = initColor;
				}
			}
			for (int j = 0; j < 32; j++)
			{
				if (joydata.btnStateArray[j])
				{
					gameBtnSignalList[j].GetComponent<Image>().color = curOpenColor;
				}
				else
				{
					gameBtnSignalList[j].GetComponent<Image>().color = initColor;
				}
			}
		}

		private void CalibrationSignal()
		{
			if (lockCalInfoList.Count == calCount)
			{
				if (!isSave)
				{
					showTipsIndex = calTipsList.Count - 1;
					tipsText.text = calTipsList[showTipsIndex].content;
				}
				else
				{
					tipsText.text = "保存成功";
				}
			}
			else
			{
				if (showTipsIndex <= 1)
				{
					return;
				}
				if (lockCalInfoList.Count < 5)
				{
					DetectionAxisChange();
					return;
				}
				if (lockCalInfoList.Count == 5)
				{
					DetectionAxisChange();
					DetectionBtnChange();
					return;
				}
				if (SettingInfoCtl.Instance.isManualGear)
				{
					if (lockCalInfoList.Count == 12)
					{
						for (int i = 0; i < 4; i++)
						{
							lockCalInfoList.Add(new LockCalibrationData());
							tipsText.text = calTipsList[showTipsIndex++].content;
						}
					}
				}
				else if (lockCalInfoList.Count == 6)
				{
					for (int j = 0; j < 6; j++)
					{
						lockCalInfoList.Add(new LockCalibrationData());
						tipsText.text = calTipsList[showTipsIndex++].content;
					}
				}
				DetectionBtnChange();
			}
		}

		private void DetectionBtnChange()
		{
			if (isjudgePersistent)
			{
				return;
			}
			curOriData = SignalSourceCtl.Instance.joyOriData;
			for (int i = 0; i < 32; i++)
			{
				if (preOriData.btnStateArray[i] != curOriData.btnStateArray[i] && !JudgeBtnIndexCal(i))
				{
					StartCoroutine(JudgeBtnSignalPersistent(isPov: false, i));
					break;
				}
			}
			if (Mathf.Abs((int)(curOriData.dwPOV - preOriData.dwPOV)) > 50 && !JudgeBtnIndexCal((int)(100 + curOriData.dwPOV)))
			{
				StartCoroutine(JudgeBtnSignalPersistent(isPov: true, (int)curOriData.dwPOV));
			}
		}

		private IEnumerator JudgeBtnSignalPersistent(bool isPov, int btnIndex)
		{
			isjudgePersistent = true;
			yield return new WaitForSeconds(1.5f);
			LockCalibrationData lockCalibrationData = new LockCalibrationData();
			curOriData = SignalSourceCtl.Instance.joyOriData;
			if (!isPov)
			{
				if (curOriData.btnStateArray[btnIndex])
				{
					lockCalibrationData.isPersistentSignal = true;
				}
				else
				{
					lockCalibrationData.isPersistentSignal = false;
				}
				btnRecordList.Add(btnIndex);
				lockCalibrationData.type = 1;
			}
			else
			{
				if (curOriData.dwPOV == btnIndex)
				{
					lockCalibrationData.isPersistentSignal = true;
				}
				else
				{
					lockCalibrationData.isPersistentSignal = false;
				}
				btnRecordList.Add((int)(100 + curOriData.dwPOV));
				lockCalibrationData.type = 10;
			}
			lockCalibrationData.btnIndex = btnIndex;
			lockCalInfoList.Add(lockCalibrationData);
			tipsText.text = calTipsList[showTipsIndex++].content;
			preOriData = curOriData;
			isjudgePersistent = false;
		}

		private bool JudgeBtnIndexCal(int index)
		{
			foreach (int btnRecord in btnRecordList)
			{
				if (btnRecord == index)
				{
					return true;
				}
			}
			return false;
		}

		private void DetectionAxisChange()
		{
			intervalTime += Time.deltaTime;
			curOriData = SignalSourceCtl.Instance.joyOriData;
			DetecionSignalAxis(preOriData.dwXpos, curOriData.dwXpos, 2);
			DetecionSignalAxis(preOriData.dwYpos, curOriData.dwYpos, 3);
			DetecionSignalAxis(preOriData.dwZpos, curOriData.dwZpos, 4);
			DetecionSignalAxis(preOriData.dwRpos, curOriData.dwRpos, 5);
			DetecionSignalAxis(preOriData.dwUpos, curOriData.dwUpos, 6);
			DetecionSignalAxis(preOriData.dwVpos, curOriData.dwVpos, 7);
			DetecionSignalAxis(preOriData.dwPOV, curOriData.dwPOV, 8);
			if (!(intervalTime > 2f))
			{
				return;
			}
			if (changeValue < 1000f)
			{
				intervalTime = 0f;
			}
			else if (lockCalInfoList.Count >= 2 || !(intervalTime < 3.5f))
			{
				lockCalInfoList.Add(RecordAxisCalData());
				intervalTime = 0f;
				changeValue = 0f;
				if (!SettingInfoCtl.Instance.isManualGear && lockCalInfoList.Count == 2)
				{
					lockCalInfoList.Add(new LockCalibrationData());
					tipsText.text = calTipsList[showTipsIndex++].content;
				}
				tipsText.text = calTipsList[showTipsIndex++].content;
				preOriData = curOriData;
			}
		}

		private void DetecionSignalAxis(uint preValue, uint curValue, int type)
		{
			if (!(Mathf.Abs((int)preValue - curValue) > changeValue))
			{
				return;
			}
			if (lockCalInfoList.Count > 0 && lockCalInfoList[lockCalInfoList.Count - 1].type == type)
			{
				float num = Mathf.InverseLerp(lockCalInfoList[lockCalInfoList.Count - 1].preValue, lockCalInfoList[lockCalInfoList.Count - 1].curValue, curValue);
				if (num > 0f && num < 1f)
				{
					preOriData = curOriData;
					return;
				}
			}
			changeValue = Mathf.Abs((int)(preValue - curValue));
			intervalTime = 0f;
			maxChangeJoyData.type = type;
			maxChangeJoyData.preValue = (int)preValue;
			maxChangeJoyData.curValue = (int)curValue;
		}

		private LockCalibrationData RecordAxisCalData()
		{
			LockCalibrationData result = new LockCalibrationData
			{
				type = maxChangeJoyData.type,
				preValue = maxChangeJoyData.preValue,
				curValue = maxChangeJoyData.curValue
			};
			maxChangeJoyData.Reset();
			return result;
		}
	}
}
