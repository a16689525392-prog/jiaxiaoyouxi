using Scene;
using UnityEngine;

namespace Car
{
	public class CarState : MonoBehaviour
	{
		private static CarState instance;

		public int CarDriveState;

		public Rigidbody carRigibody;

		public static CarState Instance => instance;

		public bool FireOn
		{
			get
			{
				if (CurCar != null)
				{
					return CurCar.GetComponent<CarCtl>().isFireOn;
				}
				return false;
			}
			set
			{
				CurCar.GetComponent<CarCtl>().isFireOn = value;
			}
		}

		public GameObject CurCar
		{
			get
			{
				if (TrainSceneBase.BaseInstance != null)
				{
					return TrainSceneBase.BaseInstance.Car;
				}
				return null;
			}
		}

		public Rigidbody CarRigibody => carRigibody;

		public float CurSpeed
		{
			get
			{
				if (CurCar != null && carRigibody != null)
				{
					return carRigibody.velocity.magnitude * 3.6f;
				}
				return 0f;
			}
		}

		public float CurEngineSpeed
		{
			get
			{
				if (CurCar != null)
				{
					return MoNiCarDynamic.Instance.dynamicData.engineSpeed;
				}
				return 0f;
			}
		}

		public CarSignal CarSignal
		{
			get
			{
				if (SignalSourceCtl.Instance.connectStatus == 2)
				{
					return SignalSourceCtl.Instance.gameProtocolSignal;
				}
				return SignalSourceCtl.Instance.circuitBoardSignal;
			}
			set
			{
				if (SignalSourceCtl.Instance.connectStatus == 2)
				{
					SignalSourceCtl.Instance.gameProtocolSignal = value;
				}
				else
				{
					SignalSourceCtl.Instance.circuitBoardSignal = value;
				}
			}
		}

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (CurCar != null && carRigibody == null)
			{
				carRigibody = CurCar.transform.GetComponent<Rigidbody>();
			}
		}

		public void StopCar()
		{
			FireOn = false;
			carRigibody.velocity = Vector3.zero;
		}
	}
}
