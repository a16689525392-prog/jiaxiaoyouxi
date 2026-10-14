using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class DrunkDriving : YingJiXunLianCtlBase
	{
		private static DrunkDriving instance;

		private bool isFire;

		public AudioSource alarmAudioSource;

		public static DrunkDriving Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (!isFire)
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.fire == 1)
				{
					SoundPlayCtl.Instance.PlaySound("酒驾或毒驾时,启动车辆", 100f);
					isFire = true;
				}
				else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fire == 1)
				{
					SoundPlayCtl.Instance.PlaySound("酒驾或毒驾时,启动车辆", 100f);
					isFire = true;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType != 603)
			{
				return;
			}
			if (itemStep == 0 && other.name == "Start_Position2")
			{
				SoundPlayCtl.Instance.PlaySound("酒驾(毒驾)项目开始");
				itemStep = 1;
			}
			else if (itemStep != 2 && other.name == "AlarmSound")
			{
				if (alarmAudioSource == null)
				{
					alarmAudioSource = AudioSourceCtl.Instance.PlayAudio("AlarmSound", isLoop: true);
				}
				itemStep = 2;
			}
			else if (itemStep != 99 && other.name == "DrunkDrivingEnd")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("酒驾(毒驾)项目结束");
				itemStep = 99;
			}
		}
	}
}
