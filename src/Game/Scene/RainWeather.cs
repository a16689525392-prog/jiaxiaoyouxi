using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class RainWeather : XiangCunSceneBase
	{
		private bool isYuGuaOpen;

		private float RainTime;

		private float JianGeTime = 10f;

		protected override void Start()
		{
			SoundPlayCtl.Instance.PlaySound("雨天请开启雨刮器");
		}

		private void Update()
		{
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (CarState.Instance.FireOn && CarState.Instance.CurSpeed > 3f)
				{
					RainTime += Time.deltaTime;
					if (RainTime > 10f && !isYuGuaOpen && CarState.Instance.CarSignal.rainBrush == 0)
					{
						SoundPlayCtl.Instance.PlaySound("雨天未开启雨刮器", 100f);
						isYuGuaOpen = true;
					}
				}
				JianGeTime += Time.deltaTime;
				if (JianGeTime > 10f && CarState.Instance.CurSpeed > 30f)
				{
					SoundPlayCtl.Instance.PlaySound("雨天驾驶速度过快", 100f);
					JianGeTime = 0f;
				}
			}
			else
			{
				if (ViewJumpCtl.Instance.modelType != 5)
				{
					return;
				}
				if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && CarState.Instance.FireOn && DriveHistoryPlay.Instance.curCalculateSpeedH > 3f)
				{
					RainTime += Time.deltaTime;
					if (RainTime > 10f && !isYuGuaOpen && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.rainBrush == 0)
					{
						SoundPlayCtl.Instance.PlaySound("雨天未开启雨刮器", 100f);
						isYuGuaOpen = true;
					}
				}
				JianGeTime += Time.deltaTime;
				if (JianGeTime > 10f && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.curCalculateSpeedH > 30f)
				{
					SoundPlayCtl.Instance.PlaySound("雨天驾驶速度过快", 100f);
					JianGeTime = 0f;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "IfYouZhuan")
			{
				itemStep = 1;
			}
			else if (itemStep == 1 && other.name == "BanYouZhuan")
			{
				SoundPlayCtl.Instance.PlaySound("违禁右转弯", 100f);
				itemStep = 2;
			}
			else if (itemStep != 3 && other.name == "IfZhiXing")
			{
				itemStep = 3;
			}
			else if (itemStep == 3 && other.name == "BanZhiXing")
			{
				SoundPlayCtl.Instance.PlaySound("违禁直行", 100f);
				itemStep = 4;
			}
			else if (itemStep != 5 && other.name == "IfZuoZhuan")
			{
				itemStep = 5;
			}
			else if (itemStep == 5 && other.name == "BanZuoZhuan")
			{
				SoundPlayCtl.Instance.PlaySound("违禁左转弯", 100f);
				itemStep = 6;
			}
		}
	}
}
