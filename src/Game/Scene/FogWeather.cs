using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class FogWeather : ShanQuSceneBase
	{
		private float FogTime;

		private float JianGeTime = 10f;

		private bool isFogLightOpen;

		private bool isFrontLightNear;

		private bool isWidthLightOpen;

		private bool isBlinkLightOpen;

		protected override void Start()
		{
			SoundPlayCtl.Instance.PlaySound("雾天请开启雾灯和危险警报闪光灯");
		}

		private void Update()
		{
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.FireOn && CarState.Instance.CurSpeed > 3f)
				{
					FogTime += Time.deltaTime;
					if (CarState.Instance.CarSignal.fogLight == 0 && SettingInfoCtl.Instance.isJudgeFogLight && !isFogLightOpen && FogTime > 6f)
					{
						SoundPlayCtl.Instance.PlaySound("雾天未开启雾灯", 100f);
						isFogLightOpen = true;
					}
					if (CarState.Instance.CarSignal.frontLight != 1 && !isFrontLightNear && FogTime > 8f)
					{
						SoundPlayCtl.Instance.PlaySound("雾天未开启近光灯", 100f);
						isFrontLightNear = true;
					}
					if (CarState.Instance.CarSignal.widthLight == 1 && !isWidthLightOpen && FogTime > 10f)
					{
						SoundPlayCtl.Instance.PlaySound("雾天未开启示宽灯", 100f);
						isWidthLightOpen = true;
					}
					if (CarState.Instance.CarSignal.turnLight != 3 && !isBlinkLightOpen && FogTime > 12f)
					{
						SoundPlayCtl.Instance.PlaySound("雾天未开启危险警报闪光灯", 100f);
						isBlinkLightOpen = true;
					}
					JianGeTime += Time.deltaTime;
					if (JianGeTime > 10f && CarState.Instance.CurSpeed > 30f)
					{
						SoundPlayCtl.Instance.PlaySound("雾天驾驶速度过快", 100f);
						JianGeTime = 0f;
					}
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 5 && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && CarState.Instance.FireOn && DriveHistoryPlay.Instance.curCalculateSpeedH > 3f)
			{
				FogTime += Time.deltaTime;
				if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fogLight == 0 && SettingInfoCtl.Instance.isJudgeFogLight && !isFogLightOpen && FogTime > 6f)
				{
					SoundPlayCtl.Instance.PlaySound("雾天未开启雾灯", 100f);
					isFogLightOpen = true;
				}
				if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight != 1 && !isFrontLightNear && FogTime > 8f)
				{
					SoundPlayCtl.Instance.PlaySound("雾天未开启近光灯", 100f);
					isFrontLightNear = true;
				}
				if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.widthLight != 1 && !isWidthLightOpen && FogTime > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("雾天未开启示宽灯", 100f);
					isWidthLightOpen = true;
				}
				if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 3 && !isBlinkLightOpen && FogTime > 12f)
				{
					SoundPlayCtl.Instance.PlaySound("雾天未开启危险警报闪光灯", 100f);
					isBlinkLightOpen = true;
				}
				JianGeTime += Time.deltaTime;
				if (JianGeTime > 10f && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.curCalculateSpeedH > 30f)
				{
					SoundPlayCtl.Instance.PlaySound("雾天驾驶速度过快", 100f);
					JianGeTime = 0f;
				}
			}
		}
	}
}
