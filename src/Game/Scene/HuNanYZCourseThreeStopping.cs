using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class HuNanYZCourseThreeStopping : CourseThreeItemBase
	{
		private float time;

		private void Update()
		{
			UpdateJudgeStopping();
		}

		private void UpdateJudgeStopping()
		{
			if (itemStep == 0)
			{
				SoundPlayCtl.Instance.PlaySound("靠边停车项目开始");
				itemStep++;
			}
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				itemStep++;
			}
			if (itemStep == 2)
			{
				if (CarState.Instance.CarSignal.turnLight == 1)
				{
					SoundPlayCtl.Instance.PlaySound("靠边停车时,未正确开启右转向灯", 10f);
					itemStep++;
				}
				else if (CarState.Instance.CarSignal.turnLight == 2)
				{
					itemStep++;
				}
			}
			if (itemStep > 1 && itemStep < 4 && CarState.Instance.CurSpeed < 0.1f)
			{
				if (itemStep == 2)
				{
					SoundPlayCtl.Instance.PlaySound("靠边停车时,未开启右转向灯", 10f);
				}
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[2].position, out var hitinfo, 8))
				{
					if (ColliderDetection.instance.GetTriggerRaycastInfoName(ColliderDetection.instance.carMidPoint[2].position, out hitinfo, 8) == "30between50")
					{
						SoundPlayCtl.Instance.PlaySound("停车后,车辆距离道路边缘线超过规定距离", 10f);
					}
					else if (ColliderDetection.instance.GetTriggerRaycastInfoName(ColliderDetection.instance.carMidPoint[2].position, out hitinfo, 8) != "Less30")
					{
						SoundPlayCtl.Instance.PlaySound("停车后,车辆距离道路边缘线超过规定距离", 100f);
					}
				}
				itemStep = 4;
			}
			if (itemStep >= 4 && itemStep != 99)
			{
				time += Time.deltaTime;
				if (itemStep == 5 || (CarState.Instance.CarSignal.turnLight == 0 && !CarState.Instance.FireOn && CarState.Instance.CarSignal.handBrake > 0.7f))
				{
					SoundPlayCtl.Instance.PlaySound("靠边停车项目结束", 0f, 0, 2.5f);
					itemStep = 99;
					NetworkManagement.Instance.TrainingIsPass = true;
					if (ViewJumpCtl.Instance.IsKaoShi)
					{
						HuNanYZCourseThreeKaoShi.Instance.isLastItem = true;
					}
				}
				if (itemStep != 5 && time > 8f && itemStep != 99)
				{
					if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1)
					{
						if (CarState.Instance.CarSignal.handBrake < 0.7f)
						{
							SoundPlayCtl.Instance.PlaySound("停车后,未及时拉紧手刹", 10f);
						}
						if (CarState.Instance.FireOn)
						{
							SoundPlayCtl.Instance.PlaySound("停车后,未及时熄火", 10f);
						}
					}
					else if (ViewJumpCtl.Instance.modelType == 5)
					{
						if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 0)
						{
							SoundPlayCtl.Instance.PlaySound("停车后,未及时关闭转向灯", 10f);
						}
						else if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake < 0.7f)
						{
							SoundPlayCtl.Instance.PlaySound("停车后,未及时拉紧手刹", 10f);
						}
						else if (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fire == 1)
						{
							SoundPlayCtl.Instance.PlaySound("停车后,未及时熄火", 10f);
						}
					}
					itemStep = 5;
				}
			}
			if (CourseThree.Instance.dirveLength - startLength > 300f && itemStep != 99)
			{
				SoundPlayCtl.Instance.PlaySound("未在规定距离内完成靠边停车操作", 100f);
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
			}
		}
	}
}
