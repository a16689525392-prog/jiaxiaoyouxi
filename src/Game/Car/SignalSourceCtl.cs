using Global;
using UnityEngine;
using UnityEngine.Events;

namespace Car
{
	public class SignalSourceCtl : MonoBehaviour
	{
		public static SignalSourceCtl Instance;

		public string Pid = "";

		public string Vid = "";

		public CarSignal gameProtocolSignal;

		public CarSignal circuitBoardSignal;

		public JoyOriginalData joyOriData = new JoyOriginalData();

		public int connectStatus;

		private bool isReadKeyFile;

		public UnityAction disConnect;

		private bool isPlaying = true;

		public ReadCircuitBoardSignal cbSignalReader = new ReadCircuitBoardSignal();

		public ReadLockSignal lockSignalReader = new ReadLockSignal();

		private void Awake()
		{
			Instance = this;
			SettingInfoCtl.Instance.ReadSettingFile();
			circuitBoardSignal.power = 1;
			circuitBoardSignal.safeBelt = 1;
			circuitBoardSignal.gear = 0;
		}

		private void UpdateSignal()
		{
		}

		private void Update()
		{
			circuitBoardSignal.oil = (Input.GetKey(KeyCode.W) ? 1 : 0);
			circuitBoardSignal.footBrake = (Input.GetKey(KeyCode.S) ? 1 : 0);
			circuitBoardSignal.handBrake = (Input.GetKey(KeyCode.Space) ? 1 : 0);
			circuitBoardSignal.cluth = (Input.GetKey(KeyCode.LeftShift) ? 1 : 0);
			circuitBoardSignal.steerWheel = ((float)(Input.GetKey(KeyCode.D) ? 1 : 0) - (float)(Input.GetKey(KeyCode.A) ? 1 : 0)) * 0.65f;
			circuitBoardSignal.fire = (Input.GetKey(KeyCode.F) ? 1 : 0);
			circuitBoardSignal.horn = (Input.GetKey(KeyCode.H) ? 1 : 0);
			if (Input.GetKeyDown(KeyCode.P))
			{
				ref CarSignal reference = ref circuitBoardSignal;
				reference.power = ((reference.power == 0) ? 1 : 0);
			}
			if (Input.GetKeyDown(KeyCode.B))
			{
				ref CarSignal reference2 = ref circuitBoardSignal;
				reference2.safeBelt = ((reference2.safeBelt == 0) ? 1 : 0);
			}
			if (Input.GetKeyDown(KeyCode.L))
			{
				ref CarSignal reference3 = ref circuitBoardSignal;
				reference3.frontLight = ((reference3.frontLight == 0) ? 1 : 0);
			}
			if (Input.GetKeyDown(KeyCode.V))
			{
				ref CarSignal reference4 = ref circuitBoardSignal;
				reference4.rainBrush = ((reference4.rainBrush == 0) ? 1 : 0);
			}
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				circuitBoardSignal.gear = 1;
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				circuitBoardSignal.gear = 2;
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				circuitBoardSignal.gear = 3;
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				circuitBoardSignal.gear = 4;
			}
			if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				circuitBoardSignal.gear = 5;
			}
			if (Input.GetKeyDown(KeyCode.R))
			{
				circuitBoardSignal.gear = 6;
			}
			if (Input.GetKeyDown(KeyCode.N))
			{
				circuitBoardSignal.gear = 0;
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				circuitBoardSignal.turnLight = 1;
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				circuitBoardSignal.turnLight = 2;
			}
			if (Input.GetKeyDown(KeyCode.X))
			{
				circuitBoardSignal.turnLight = 0;
			}
			if (CarState.Instance.CurCar != null && CarState.Instance.CurSpeed < 6f && !(circuitBoardSignal.cluth > 0.25f) && !(circuitBoardSignal.oil <= 0f))
			{
				circuitBoardSignal.cluth = 0.35f;
			}
		}

		private void JudgeConnect()
		{
			if (cbSignalReader.JudgeCircuitConnect())
			{
				connectStatus = 1;
				Debug.Log("连接到电路板");
			}
		}

		public byte GetReadBuffer(int index)
		{
			return cbSignalReader.GetReadBuffer(index);
		}

		public void SetWriteBuffer(int index, byte value)
		{
			cbSignalReader.SetWriteBuffer(index, value);
		}

		public void SetCircuitBoardSignalType(int type)
		{
			switch (type)
			{
			case 0:
				cbSignalReader.SetWriteBuffer(29, 0);
				break;
			case 1:
				cbSignalReader.SetWriteBuffer(29, 192);
				break;
			case 2:
				cbSignalReader.SetWriteBuffer(29, 83);
				cbSignalReader.SetWriteBuffer(62, 3);
				Debug.Log("三点标定");
				break;
			case 3:
				cbSignalReader.SetWriteBuffer(29, 83);
				cbSignalReader.SetWriteBuffer(62, 7);
				Debug.Log("七点标定");
				break;
			case 4:
				cbSignalReader.SetWriteBuffer(29, 67);
				break;
			case 5:
				cbSignalReader.SetWriteBuffer(29, 63);
				break;
			}
		}

		public int GetCircuitBoardSignalType()
		{
			int result = 0;
			switch (cbSignalReader.GetReadBuffer(29))
			{
			case 0:
				result = 0;
				break;
			case 192:
				result = 1;
				break;
			case 83:
				result = 2;
				break;
			case 67:
				result = 4;
				break;
			case 63:
				result = 5;
				break;
			}
			return result;
		}

		public void SetCarShakeStatus(bool isShake)
		{
			if (isShake)
			{
				if (CarShake.Instance != null)
				{
					CarShake.Instance.StartCarShake();
				}
				cbSignalReader.SetWriteBuffer(18, (byte)SettingInfoCtl.Instance.zhengDongForce);
			}
			else if (!isShake)
			{
				if (CarShake.Instance != null)
				{
					CarShake.Instance.StopCarShake();
				}
				cbSignalReader.SetWriteBuffer(18, 0);
			}
		}

		private void OnDestroy()
		{
		}
	}
}
