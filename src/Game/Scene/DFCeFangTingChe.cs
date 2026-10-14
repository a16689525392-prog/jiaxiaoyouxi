using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class DFCeFangTingChe : CourseTwoItemCtlBase
	{
		private void Update()
		{
			UpdateJudgeCeFang();
		}

		private void UpdateJudgeCeFang()
		{
			if (itemStep == 1)
			{
				if ((ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6) && CarActionJudge.Instance.JudgeActionDone(114) && CarState.Instance.CurSpeed > 0.15f)
				{
					if (CarState.Instance.CarSignal.turnLight == 1 && SettingInfoCtl.Instance.isCeFangJudgeLight)
					{
						SoundPlayCtl.Instance.PlaySound("倒车时,未正确开启右转向灯", 10f);
					}
					else if (CarState.Instance.CarSignal.turnLight == 0 && SettingInfoCtl.Instance.isCeFangJudgeLight)
					{
						SoundPlayCtl.Instance.PlaySound("倒车时,未开启右转向灯", 10f);
					}
					itemStep++;
				}
				else if (ViewJumpCtl.Instance.modelType == 0 && CarActionJudge.Instance.JudgeActionDone(114) && DriveHistoryPlay.Instance.curCalculateSpeedH > 0.15f)
				{
					if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 1 && SettingInfoCtl.Instance.isCeFangJudgeLight)
					{
						SoundPlayCtl.Instance.PlaySound("倒车时,未正确开启右转向灯", 10f);
					}
					else if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 0 && SettingInfoCtl.Instance.isCeFangJudgeLight)
					{
						SoundPlayCtl.Instance.PlaySound("倒车时,未开启右转向灯", 10f);
					}
					itemStep++;
				}
			}
			else
			{
				if (itemStep != 3)
				{
					return;
				}
				if ((ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6) && !CarActionJudge.Instance.JudgeActionDone(114) && CarState.Instance.CurSpeed > 0.15f)
				{
					if (CarState.Instance.CarSignal.turnLight == 2)
					{
						SoundPlayCtl.Instance.PlaySound("出库时,未正确开启左转向灯", 10f);
					}
					else if (CarState.Instance.CarSignal.turnLight == 0)
					{
						SoundPlayCtl.Instance.PlaySound("出库时,未开启左转向灯", 10f);
					}
					itemStep++;
				}
				else if (ViewJumpCtl.Instance.modelType == 0 && !CarActionJudge.Instance.JudgeActionDone(114) && DriveHistoryPlay.Instance.curCalculateSpeedH > 0.15f)
				{
					if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 2)
					{
						SoundPlayCtl.Instance.PlaySound("出库时,未正确开启左转向灯", 10f);
					}
					else if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 0)
					{
						SoundPlayCtl.Instance.PlaySound("出库时,未开启左转向灯", 10f);
					}
					itemStep++;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_CeFang")
			{
				SoundPlayCtl.Instance.PlaySound("侧方停车项目开始");
				itemStep = 1;
			}
			else if (itemStep != 3 && other.name == "Step1")
			{
				itemStep = 3;
			}
			else if (itemStep != 99 && other.name == "End_CeFang")
			{
				if (itemStep < 3)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行侧方停车操作", 100f);
				}
				else if ((ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6) && CarState.Instance.CarSignal.turnLight != 0 && SettingInfoCtl.Instance.isCeFangJudgeLight)
				{
					SoundPlayCtl.Instance.PlaySound("未及时关闭转向灯", 10f);
				}
				else if (ViewJumpCtl.Instance.modelType == 0 && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 0 && SettingInfoCtl.Instance.isCeFangJudgeLight)
				{
					SoundPlayCtl.Instance.PlaySound("未及时关闭转向灯", 10f);
				}
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("侧方停车项目结束");
				itemStep = 99;
			}
		}
	}
}
