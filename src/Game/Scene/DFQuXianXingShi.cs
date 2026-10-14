using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class DFQuXianXingShi : CourseTwoItemCtlBase
	{
		private void Update()
		{
			UpdateCarStop();
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
			else if (other.name == "End_QuXian" && itemStep != 99)
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("曲线行驶项目结束");
				itemStep = 99;
			}
		}
	}
}
