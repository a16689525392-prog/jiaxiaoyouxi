using System;
using Car;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class BiaoDingDataView : ViewBase
	{
		public GameObject[] LocalDataText = new GameObject[15];

		public GameObject[] CircuitBoardDataText = new GameObject[15];

		public Text[] LocalDataValue = new Text[15];

		public Text[] CircuitBoardDataValue = new Text[15];

		private bool isReadOriData;

		private bool isReadLocalData;

		private int GetCircuitBoardSignalType;

		public override void Awake()
		{
			base.Awake();
			isReadOriData = false;
			isReadLocalData = false;
			if (!SettingInfoCtl.Instance.ReadBiaoDingFile())
			{
				for (int i = 0; i < 15; i++)
				{
					LocalDataValue[i].text = "null";
					LocalDataValue[i].color = Color.red;
				}
				isReadLocalData = true;
			}
		}

		private void Update()
		{
			if (!isReadOriData)
			{
				ReadOriBiaoDingData();
			}
			if (!isReadLocalData)
			{
				ReadLocalData();
			}
		}

		private void ReadLocalData()
		{
			for (int i = 0; i < 15; i++)
			{
				LocalDataValue[i].text = GetByteValue(SettingInfoCtl.Instance.biaoDingData[2 * i], SettingInfoCtl.Instance.biaoDingData[2 * i + 1]).ToString();
			}
			isReadLocalData = true;
		}

		private void ReadOriBiaoDingData()
		{
			SignalSourceCtl.Instance.SetCircuitBoardSignalType(4);
			if (SignalSourceCtl.Instance.GetCircuitBoardSignalType() == 4)
			{
				GetCircuitBoardSignalType = Convert.ToInt32(SignalSourceCtl.Instance.GetReadBuffer(62));
				if (GetCircuitBoardSignalType == 3)
				{
					LocalDataText[1].gameObject.SetActive(value: false);
					LocalDataText[2].gameObject.SetActive(value: false);
					LocalDataText[4].gameObject.SetActive(value: false);
					LocalDataText[5].gameObject.SetActive(value: false);
					CircuitBoardDataText[1].gameObject.SetActive(value: false);
					CircuitBoardDataText[2].gameObject.SetActive(value: false);
					CircuitBoardDataText[4].gameObject.SetActive(value: false);
					CircuitBoardDataText[5].gameObject.SetActive(value: false);
					LocalDataValue[1].gameObject.SetActive(value: false);
					LocalDataValue[2].gameObject.SetActive(value: false);
					LocalDataValue[4].gameObject.SetActive(value: false);
					LocalDataValue[5].gameObject.SetActive(value: false);
					CircuitBoardDataValue[1].gameObject.SetActive(value: false);
					CircuitBoardDataValue[2].gameObject.SetActive(value: false);
					CircuitBoardDataValue[4].gameObject.SetActive(value: false);
					CircuitBoardDataValue[5].gameObject.SetActive(value: false);
				}
				for (int i = 0; i < 15; i++)
				{
					CircuitBoardDataValue[i].text = GetByteValue(SignalSourceCtl.Instance.GetReadBuffer(32 + 2 * i), SignalSourceCtl.Instance.GetReadBuffer(32 + 2 * i + 1)).ToString();
				}
				isReadOriData = true;
			}
		}

		private float GetByteValue(byte a, byte b)
		{
			return a * 256 + b;
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
