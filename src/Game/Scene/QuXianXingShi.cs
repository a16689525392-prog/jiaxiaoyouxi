using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class QuXianXingShi : CourseTwoItemCtlBase
	{
		private void Update()
		{
			UpdateCarStop();
		}

		private void UpdateJudgeQuXian()
		{
			if (itemStep == 2 && CarState.Instance.CarSignal.gear == 1 && SettingInfoCtl.Instance.isManualGear)
			{
				itemStep++;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "Start_QuXian" && itemStep != 1)
			{
				SoundPlayCtl.Instance.PlaySound("曲线行驶项目开始");
				itemStep = 1;
			}
			if (itemStep == 1 && other.name == "Step0")
			{
				itemStep++;
			}
			else if (other.name == "End" && itemStep != 99)
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("曲线行驶项目结束");
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
				if (ViewJumpCtl.Instance.IsKaoShi && CourseTwoKaoShi.KaoShiStep == CourseTwoScene.Instance.indexID - 1)
				{
					CourseTwoKaoShi.KaoShiStep = CourseTwoScene.Instance.indexID;
				}
			}
		}
	}
}
