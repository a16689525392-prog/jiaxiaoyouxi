using Car;
using UnityEngine;
using UnityEngine.Events;

namespace Global
{
	public class ExternalInputCtl : MonoBehaviour
	{
		private static ExternalInputCtl instance;

		public UnityAction rightKeyDown;

		public UnityAction leftKeyDown;

		public UnityAction backKeyDown;

		public UnityAction enterKeyDown;

		public bool isCircuitBoardSignal;

		public float ControlTime;

		private int curInputIndex;

		public static ExternalInputCtl Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			if (SignalSourceCtl.Instance.connectStatus == 1)
			{
				curInputIndex = SignalSourceCtl.Instance.GetReadBuffer(31) / 8;
			}
		}

		private void Update()
		{
			UpdateInput();
		}

		private void UpdateInput()
		{
			if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow)) && rightKeyDown != null)
			{
				rightKeyDown();
			}
			if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow)) && leftKeyDown != null)
			{
				leftKeyDown();
			}
			if (Input.GetKeyDown(KeyCode.Escape) && backKeyDown != null)
			{
				backKeyDown();
			}
			if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
			{
				enterKeyDown?.Invoke();
			}
			if (DataCenter.Instance.UsbVersion == 240 || DataCenter.Instance.UsbVersion == 224 || !MyDiagnosis.Instance.JudgeSoftCorrect() || SignalSourceCtl.Instance.GetReadBuffer(31) / 8 == curInputIndex)
			{
				return;
			}
			curInputIndex = SignalSourceCtl.Instance.GetReadBuffer(31) / 8;
			switch (curInputIndex)
			{
			case 1:
				if (leftKeyDown != null)
				{
					isCircuitBoardSignal = true;
					leftKeyDown();
				}
				break;
			case 3:
				if (rightKeyDown != null)
				{
					isCircuitBoardSignal = true;
					rightKeyDown();
				}
				break;
			case 8:
				if (backKeyDown != null)
				{
					isCircuitBoardSignal = true;
					backKeyDown();
				}
				break;
			case 4:
				if (enterKeyDown != null)
				{
					isCircuitBoardSignal = true;
					enterKeyDown();
				}
				break;
			}
			isCircuitBoardSignal = false;
		}
	}
}
