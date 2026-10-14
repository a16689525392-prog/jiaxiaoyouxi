using System;
using Global;
using Scene;
using View;

namespace Car
{
	[Serializable]
	public struct CarSignal
	{
		public float oil;

		public float footBrake;

		public float handBrake;

		public float cluth;

		public float steerWheel;

		public int gear;

		public int turnLight;

		public int horn;

		public int fire;

		public int power;

		public int safeBelt;

		public int door;

		public int rainBrush;

		public int frontLight;

		public int widthLight;

		public int fogLight;

		public int sprayWater;

		public int ChangeViewPoint;

		public void InitCarOtherSignal()
		{
			if (DriveHistoryPlay.Instance != null && (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5))
			{
				if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count)
				{
					oil = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.oil;
					footBrake = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake;
					cluth = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.cluth;
					handBrake = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake;
					steerWheel = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.steerWheel;
					gear = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear;
					turnLight = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight;
					horn = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn;
					fire = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fire;
					power = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.power;
					safeBelt = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.safeBelt;
					door = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.door;
					rainBrush = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush;
					frontLight = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight;
					if (SignalSourceCtl.Instance.Pid != "4200")
					{
						widthLight = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.widthLight;
					}
					fogLight = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fogLight;
					sprayWater = DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.sprayWater;
				}
			}
			else
			{
				turnLight = 0;
				rainBrush = 0;
				frontLight = 0;
				if (SettingInfoCtl.Instance.isManualGear)
				{
					gear = 0;
				}
				else
				{
					gear = 104;
				}
				horn = 0;
				fire = 0;
				power = 0;
				if (SettingInfoCtl.Instance.isReverseSafetyBelt)
				{
					safeBelt = 0;
				}
				else
				{
					safeBelt = 1;
				}
				door = 1;
				ChangeViewPoint = 0;
				if (SignalSourceCtl.Instance.Pid != "4200")
				{
					widthLight = 0;
				}
				fogLight = 0;
				sprayWater = 0;
			}
		}

		public void SetData()
		{
		}
	}
}
