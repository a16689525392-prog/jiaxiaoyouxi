using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class BiaoDingView : ViewBase
	{
		public Text title;

		public Text valueText;

		public GameObject itemGroup;

		public ScrollRect mScrollRect;

		public Text errorText;

		public bool isCheck;

		private float Checktime1;

		private float Checktime2;

		private int Checkindex;

		private int WriteCount;

		private int GetCircuitBoardSignalType;

		public GameObject CheckImage;

		private int value0;

		private int value1;

		private int valuemax;

		private int valuemin;

		private byte[] biaoDingData = new byte[30];

		private Coroutine cor;

		private List<int> wheelDataList = new List<int>();

		private int readSignalIndex;

		private int writeSignaIndex;

		private int mybiaodingIndex;

		private bool isReadOriData;

		private bool isChangeData;

		private float value;

		private void BiaoDingCheck()
		{
			if (Checkindex == 99)
			{
				isCheck = false;
				ViewJumpCtl.Instance.ReturnPreView();
				Checkindex = 100;
			}
			else if (Checkindex == 10)
			{
				SignalSourceCtl.Instance.SetCircuitBoardSignalType(3);
				for (int i = 0; i < 30; i++)
				{
					SignalSourceCtl.Instance.SetWriteBuffer(32 + i, biaoDingData[i]);
				}
				Checkindex = 20;
				Checktime2 = 0f;
			}
			else if (Checkindex == 20)
			{
				Checktime2 += Time.deltaTime;
				if (Checktime2 > 0.5f)
				{
					Checkindex = 30;
				}
			}
			else
			{
				if (Checkindex != 30)
				{
					return;
				}
				SignalSourceCtl.Instance.SetCircuitBoardSignalType(4);
				if (SignalSourceCtl.Instance.GetCircuitBoardSignalType() != 4)
				{
					return;
				}
				bool flag = false;
				GetCircuitBoardSignalType = Convert.ToInt32(SignalSourceCtl.Instance.GetReadBuffer(62));
				Debug.Log("当前板子是：" + GetCircuitBoardSignalType);
				if (GetCircuitBoardSignalType == 3)
				{
					for (int j = 0; j < 30; j++)
					{
						if (j != 1 && j != 2 && j != 3 && j != 4 && j != 5 && j != 7 && j != 8 && j != 9 && j != 10 && j != 11 && j != 13 && biaoDingData[j] != SignalSourceCtl.Instance.GetReadBuffer(32 + j))
						{
							flag = true;
							Debug.Log("i：" + j + "biaoDingData:" + biaoDingData[j] + "GetReadBuffer:" + SignalSourceCtl.Instance.GetReadBuffer(32 + j));
						}
					}
				}
				else if (GetCircuitBoardSignalType == 7)
				{
					for (int k = 0; k < 30; k++)
					{
						if (k != 1 && k != 3 && k != 5 && k != 7 && k != 9 && k != 8 && k != 9 && k != 11 && k != 13 && biaoDingData[k] != SignalSourceCtl.Instance.GetReadBuffer(32 + k))
						{
							flag = true;
							Debug.Log("i：" + k + "biaoDingData:" + biaoDingData[k] + "GetReadBuffer:" + SignalSourceCtl.Instance.GetReadBuffer(32 + k));
						}
					}
				}
				if (flag)
				{
					WriteCount++;
					if (WriteCount < 10)
					{
						Checkindex = 10;
						return;
					}
					ViewJumpCtl.Instance.ShowMsgTips("电路板数据标定失败!");
					Checkindex = 99;
				}
				else
				{
					ViewJumpCtl.Instance.ShowMsgTips("电路板数据标定成功!");
					Checkindex = 99;
				}
			}
		}

		public override void Awake()
		{
			CreateBiaoDingItem();
			base.Awake();
			errorText.text = "";
			GetSignalIndex();
			Checktime1 = 0f;
			Checktime2 = 0f;
			Checkindex = 0;
			WriteCount = 0;
		}

		private void Update()
		{
			valueText.text = (SignalSourceCtl.Instance.GetReadBuffer(readSignalIndex) * 256 + SignalSourceCtl.Instance.GetReadBuffer(readSignalIndex + 1)).ToString("f0");
			if (!isReadOriData)
			{
				ReadOriBiaoDingData();
			}
			if (isCheck)
			{
				BiaoDingCheck();
			}
		}

		public List<int> GetWheelDatas()
		{
			return wheelDataList;
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (ViewJumpCtl.Instance.itemType == 0)
			{
				if (go == GetBtn(0))
				{
					RecordBiaoDingData(0, go);
					SelectNextBtn();
					wheelDataList.Add(int.Parse(valueText.text));
					errorText.text = "";
				}
				else if (go == GetBtn(1))
				{
					RecordBiaoDingData(1, go);
					SelectNextBtn();
					wheelDataList.Add(int.Parse(valueText.text));
					errorText.text = "";
				}
				else if (go == GetBtn(2))
				{
					RecordBiaoDingData(2, go);
					SelectNextBtn();
					wheelDataList.Add(int.Parse(valueText.text));
					errorText.text = "";
				}
				else if (go == GetBtn(3))
				{
					RecordBiaoDingData(3, go);
					SelectNextBtn();
					cor = StartCoroutine(ShowNextPage());
					wheelDataList.Add(int.Parse(valueText.text));
					errorText.text = "";
				}
				else if (go == GetBtn(4))
				{
					RecordBiaoDingData(4, go);
					SelectNextBtn();
					wheelDataList.Add(int.Parse(valueText.text));
					errorText.text = "";
				}
				else if (go == GetBtn(5))
				{
					RecordBiaoDingData(5, go);
					SelectNextBtn();
					wheelDataList.Add(int.Parse(valueText.text));
					errorText.text = "";
				}
				else if (go == GetBtn(6))
				{
					RecordBiaoDingData(6, go);
					SelectNextBtn();
					wheelDataList.Add(int.Parse(valueText.text));
					JudgeSteerDataError();
				}
				else if (go == GetBtn(7))
				{
					SaveBiaoDingData();
				}
				else if (go == GetBtn(8))
				{
					BackClick();
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 5)
			{
				if (go == GetBtn(0))
				{
					RecordBiaoDingData(0, go);
					SelectNextBtn();
					wheelDataList.Add(int.Parse(valueText.text));
					errorText.text = "";
				}
				else if (go == GetBtn(1))
				{
					RecordBiaoDingData(3, go);
					SelectNextBtn();
					wheelDataList.Add(int.Parse(valueText.text));
					errorText.text = "";
				}
				else if (go == GetBtn(2))
				{
					RecordBiaoDingData(6, go);
					SelectNextBtn();
					wheelDataList.Add(int.Parse(valueText.text));
					errorText.text = "";
				}
				else if (go == GetBtn(3))
				{
					SaveBiaoDingData();
				}
				else if (go == GetBtn(4))
				{
					BackClick();
				}
			}
			else
			{
				if (ViewJumpCtl.Instance.itemType == 0 || ViewJumpCtl.Instance.itemType == 5)
				{
					return;
				}
				if (go == GetBtn(0))
				{
					RecordBiaoDingData(0, go);
					value0 = int.Parse(valueText.text);
					valuemax = int.Parse(valueText.text);
					SelectNextBtn();
				}
				else if (go == GetBtn(1))
				{
					RecordBiaoDingData(1, go);
					value1 = int.Parse(valueText.text);
					valuemin = int.Parse(valueText.text);
					SelectNextBtn();
				}
				else if (go == GetBtn(2))
				{
					if (value0 > value1)
					{
						value0 = (int)(0.93f * (float)value0);
						value1 = (int)(1.07f * (float)value1);
					}
					else
					{
						value1 = (int)(0.93f * (float)value1);
						value0 = (int)(1.07f * (float)value0);
					}
					biaoDingData[writeSignaIndex] = (byte)(value0 / 255);
					biaoDingData[writeSignaIndex + 1] = (byte)(value0 % 255);
					biaoDingData[writeSignaIndex + 2] = (byte)(value1 / 255);
					biaoDingData[writeSignaIndex + 3] = (byte)(value1 % 255);
					SaveBiaoDingData();
				}
				else if (go == GetBtn(3))
				{
					BackClick();
				}
			}
		}

		private void BackClick()
		{
			if (isChangeData && errorText.text == "")
			{
				ViewJumpCtl.Instance.EnterView(ViewType.MsgView, isDestroyCurView: false);
				ViewJumpCtl.Instance.curView.GetComponent<MsgView>().InitMsgView("修改的标定信息还未保存\n是否需要保存?", "保存", delegate
				{
					if (SaveBiaoDingData())
					{
						ViewJumpCtl.Instance.ReturnPreView();
					}
				}, "不保存", delegate
				{
					ViewJumpCtl.Instance.ReturnPreView();
				});
			}
			else
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}

		private void JudgeSteerDataError()
		{
			if (JudgeBiaoDingOver())
			{
				int num = 0;
				for (int i = 0; i < wheelDataList.Count - 1; i++)
				{
					if (wheelDataList[i] > wheelDataList[i + 1])
					{
						num++;
					}
					else if (wheelDataList[i] < wheelDataList[i + 1])
					{
						num--;
					}
				}
				if (num != wheelDataList.Count - 1 && num != -(wheelDataList.Count - 1))
				{
					Debug.Log(num);
					errorText.text = "存在标定数据错误,请检查后重新标定!";
				}
				else
				{
					errorText.text = "";
				}
			}
			else
			{
				errorText.text = "有数据还没有进行标定,请标定后保存!";
			}
		}

		private bool JudgeBiaoDingOver()
		{
			foreach (GameObject btn in btnList)
			{
				if (btn.transform.Find("BtnText").GetComponent<Text>().text == "确定")
				{
					return false;
				}
			}
			return true;
		}

		private bool SaveBiaoDingData()
		{
			if (errorText.text != "")
			{
				ViewJumpCtl.Instance.EnterView(ViewType.MsgView, isDestroyCurView: false);
				ViewJumpCtl.Instance.curView.GetComponent<MsgView>().InitMsgView("标定数据存在错误,请重新进入后标定", "确定", delegate
				{
					ViewJumpCtl.Instance.ReturnPreView();
				});
				return false;
			}
			bool flag = false;
			if (JudgeBiaoDingOver())
			{
				SignalSourceCtl.Instance.SetCircuitBoardSignalType(3);
				for (int num = 0; num < 30; num++)
				{
					SignalSourceCtl.Instance.SetWriteBuffer(32 + num, biaoDingData[num]);
					SettingInfoCtl.Instance.biaoDingData[num] = biaoDingData[num];
				}
				isCheck = true;
				Checkindex = 20;
				Checktime2 = 0f;
				SettingInfoCtl.Instance.SaveBiaoDingInfos();
				SettingInfoCtl.Instance.ReadBiaoDingFile();
				for (int num2 = 0; num2 < 30; num2++)
				{
					if (SettingInfoCtl.Instance.biaoDingData[num2] != biaoDingData[num2])
					{
						ViewJumpCtl.Instance.ShowMsgTips("保存标定信息到本机后核对异常！");
					}
				}
				ViewJumpCtl.Instance.ShowMsgTips("信息保存到本机并核对成功！");
				SettingInfoCtl.Instance.LastCalibratedCircuitBoard = DataCenter.Instance.SerialNumber;
				Debug.Log(SettingInfoCtl.Instance.LastCalibratedCircuitBoard);
				SettingInfoCtl.Instance.SaveSetingInfos();
				isChangeData = false;
				return true;
			}
			ViewJumpCtl.Instance.ShowMsgTips("当前信号标定还未完成 , 不能保存");
			return false;
		}

		private void CreateBiaoDingItem()
		{
			switch (ViewJumpCtl.Instance.itemType)
			{
			case 0:
				CreateItem("1. 请将方向盘左打一圈半(打满-540°) , 然后点击确定按钮", 0);
				CreateItem("2. 请将方向盘左打一圈(-360°) , 然后点击确定按钮", 1);
				CreateItem("3. 请将方向盘左打半圈(-180°) , 然后点击确定按钮", 2);
				CreateItem("4. 请将方向盘回正 , 然后点击确定按钮", 3);
				CreateItem("5. 请将方向盘右打半圈(180°) , 然后点击确定按钮", 4);
				CreateItem("6. 请将方向盘右打一圈(360°) , 然后点击确定按钮", 5);
				CreateItem("7. 请将方向盘右打一圈半(打满540°) , 然后点击确定按钮", 6);
				btnList[0].GetComponent<BtnEntity>().SetBtnIndex(7);
				btnList[1].GetComponent<BtnEntity>().SetBtnIndex(8);
				break;
			case 5:
				CreateItem("1. 请将方向盘向左打满, 然后点击确定按钮", 0);
				CreateItem("2. 请将方向盘回正 , 然后点击确定按钮", 1);
				CreateItem("3. 请将方向盘向右打满, 然后点击确定按钮", 2);
				btnList[0].GetComponent<BtnEntity>().SetBtnIndex(3);
				btnList[1].GetComponent<BtnEntity>().SetBtnIndex(4);
				break;
			case 1:
				CreateItem("1. 请将离合器踩到底 , 然后点击确定按钮", 0);
				CreateItem("2. 请将离合器松开 , 然后点击确定按钮", 1);
				btnList[0].GetComponent<BtnEntity>().SetBtnIndex(2);
				btnList[1].GetComponent<BtnEntity>().SetBtnIndex(3);
				itemGroup.GetComponent<GridLayoutGroup>().spacing = new Vector2(0f, 80f);
				itemGroup.transform.localPosition -= new Vector3(0f, 40f);
				break;
			case 2:
				CreateItem("1. 请将脚刹踩到底 , 然后点击确定按钮", 0);
				CreateItem("2. 请将脚刹松开 , 然后点击确定按钮", 1);
				btnList[0].GetComponent<BtnEntity>().SetBtnIndex(2);
				btnList[1].GetComponent<BtnEntity>().SetBtnIndex(3);
				itemGroup.GetComponent<GridLayoutGroup>().spacing = new Vector2(0f, 80f);
				itemGroup.transform.localPosition -= new Vector3(0f, 40f);
				break;
			case 3:
				CreateItem("1. 请将油门踩到底 , 然后点击确定按钮", 0);
				CreateItem("2. 请将油门松开 , 然后点击确定按钮", 1);
				btnList[0].GetComponent<BtnEntity>().SetBtnIndex(2);
				btnList[1].GetComponent<BtnEntity>().SetBtnIndex(3);
				itemGroup.GetComponent<GridLayoutGroup>().spacing = new Vector2(0f, 80f);
				itemGroup.transform.localPosition -= new Vector3(0f, 40f);
				break;
			case 4:
				CreateItem("1. 请将手刹松开 , 然后点击确定按钮", 0);
				CreateItem("2. 请将手刹拉紧 , 然后点击确定按钮", 1);
				btnList[0].GetComponent<BtnEntity>().SetBtnIndex(2);
				btnList[1].GetComponent<BtnEntity>().SetBtnIndex(3);
				itemGroup.GetComponent<GridLayoutGroup>().spacing = new Vector2(0f, 80f);
				itemGroup.transform.localPosition -= new Vector3(0f, 40f);
				break;
			}
		}

		private GameObject CreateItem(string text, int btnIndex, string btnName = "确定")
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("BtnAndTips/BiaoDingItem"));
			gameObject.GetComponent<BiaoDingItem>().SetShowText(text);
			gameObject.GetComponent<BiaoDingItem>().SetSureBtnName(btnName);
			gameObject.GetComponent<BiaoDingItem>().SetSureBtnIndex(btnIndex);
			gameObject.transform.SetParent(itemGroup.transform);
			gameObject.transform.localScale = Vector3.one;
			btnList.Add(gameObject.GetComponent<BiaoDingItem>().sureBtn);
			return gameObject;
		}

		private void GetSignalIndex()
		{
			switch (ViewJumpCtl.Instance.itemType)
			{
			case 0:
				title.text = "方向盘标定7";
				readSignalIndex = 40;
				writeSignaIndex = 0;
				mybiaodingIndex = 100;
				break;
			case 5:
				title.text = "方向盘标定3";
				readSignalIndex = 40;
				writeSignaIndex = 0;
				mybiaodingIndex = 100;
				break;
			case 1:
				title.text = "离合器标定";
				readSignalIndex = 32;
				writeSignaIndex = 14;
				mybiaodingIndex = 0;
				break;
			case 2:
				title.text = "脚刹标定";
				readSignalIndex = 34;
				writeSignaIndex = 18;
				mybiaodingIndex = 2;
				break;
			case 3:
				title.text = "油门标定";
				readSignalIndex = 36;
				writeSignaIndex = 22;
				mybiaodingIndex = 4;
				break;
			case 4:
				title.text = "手刹标定";
				readSignalIndex = 38;
				writeSignaIndex = 26;
				mybiaodingIndex = 6;
				break;
			}
		}

		private void ReadOriBiaoDingData()
		{
			SignalSourceCtl.Instance.SetCircuitBoardSignalType(4);
			if (SignalSourceCtl.Instance.GetCircuitBoardSignalType() == 4)
			{
				for (int i = 0; i < 30; i++)
				{
					biaoDingData[i] = SignalSourceCtl.Instance.GetReadBuffer(32 + i);
				}
				isReadOriData = true;
			}
		}

		private void RecordBiaoDingData(int index, GameObject go)
		{
			biaoDingData[writeSignaIndex + index * 2] = SignalSourceCtl.Instance.GetReadBuffer(readSignalIndex);
			biaoDingData[writeSignaIndex + index * 2 + 1] = SignalSourceCtl.Instance.GetReadBuffer(readSignalIndex + 1);
			isChangeData = true;
			go.GetComponent<NormalBtn>().btnText.text = valueText.text;
		}

		private IEnumerator ShowNextPage()
		{
			value = 1f;
			while (value > 0f)
			{
				value -= Time.deltaTime * 1.5f;
				if ((double)value < 0.5)
				{
					value = 0.45f;
				}
				mScrollRect.verticalScrollbar.value = value;
				yield return 0;
			}
			yield return 0;
		}

		private void OnDestroy()
		{
			if (cor != null)
			{
				StopCoroutine(cor);
			}
		}
	}
}
