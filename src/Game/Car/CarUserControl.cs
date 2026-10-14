using System;
using Scene;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Car
{
	[RequireComponent(typeof(CarCtl))]
	public class CarUserControl : MonoBehaviour
	{
		private CarCtl m_Car;

		private float handBrake = 1f;

		private void Awake()
		{
			m_Car = GetComponent<CarCtl>();
		}

		private void Start()
		{
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetAction = (UnityAction)Delegate.Combine(baseInstance.ResetAction, (UnityAction)delegate
			{
				handBrake = 1f;
			});
		}

		private void FixedUpdate()
		{
			MoNiCarDynamic.Instance.TransfromGeneralFormat(CarState.Instance);
			MoNiCarDynamic.Instance.DynamicCaculateLocal();
			if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
			{
				if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count - 1)
				{
					if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fire == 1)
					{
						handBrake = 0f;
					}
					m_Car.Move(DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fire, DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel, DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.oil, DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.cluth, DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake, handBrake + DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake, DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear);
				}
				else
				{
					m_Car.Move(0, 0f, 0f, 0f, 1f, 1f, 0);
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 1)
			{
				m_Car.Move(CarState.Instance.CarSignal.fire, 0f, 0f, 0f, 1f, 1f, 0);
			}
			else
			{
				if (CarState.Instance.FireOn)
				{
					handBrake = 0f;
				}
				m_Car.Move(CarState.Instance.CarSignal.fire, CarState.Instance.CarSignal.steerWheel, CarState.Instance.CarSignal.oil, CarState.Instance.CarSignal.cluth, CarState.Instance.CarSignal.footBrake, handBrake + CarState.Instance.CarSignal.handBrake, CarState.Instance.CarSignal.gear);
			}
		}
	}
}
