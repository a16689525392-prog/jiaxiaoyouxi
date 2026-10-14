using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class QiFuLu : CourseTwoItemCtlBase
	{
		private RaycastHit qiFuLuHitInfo;

		private void Update()
		{
			UpdateQiFuLu();
			UpdateCarStop();
		}

		private void UpdateQiFuLu()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				if (CarState.Instance.CarSignal.turnLight == 2)
				{
					SoundPlayCtl.Instance.PlaySound("未正确开启转向灯", 10f);
				}
				else if (CarState.Instance.CarSignal.turnLight == 0)
				{
					SoundPlayCtl.Instance.PlaySound("未开启左转向灯", 10f);
				}
				itemStep++;
			}
			if (itemStep == 3)
			{
				if (CarState.Instance.CurSpeed > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("通过起伏路面时,车速控制不当，车辆严重跳跃", 100f);
				}
				itemStep++;
			}
			if (itemStep == 4 && (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out qiFuLuHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out qiFuLuHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out qiFuLuHitInfo, 8)) && qiFuLuHitInfo.collider.name == "Qi_LowSpeed" && (CarState.Instance.CarSignal.cluth < 0.1f || CarState.Instance.CarSignal.footBrake < 0.05f) && CarState.Instance.CurSpeed > 6f)
			{
				SoundPlayCtl.Instance.PlaySound("通过起伏路前不减速", 10f);
				itemStep++;
			}
			if (itemStep == 6 && CarState.Instance.CurSpeed > 10f)
			{
				SoundPlayCtl.Instance.PlaySound("通过起伏路面时,车速控制不当，车辆严重跳跃", 100f);
				itemStep++;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_QiFu")
			{
				SoundPlayCtl.Instance.PlaySound("起伏路驾驶考试开始");
				itemStep = 1;
			}
			else if (itemStep != 3 && other.name == "QiFu1")
			{
				itemStep = 3;
			}
			else if (itemStep != 6 && other.name == "QiFu2")
			{
				itemStep = 6;
			}
			else if (itemStep != 99 && other.name == "End_QiFu")
			{
				if (CarState.Instance.CarSignal.turnLight != 0)
				{
					SoundPlayCtl.Instance.PlaySound("未及时关闭转向灯", 10f);
				}
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("起伏路驾驶考试结束");
				itemStep = 99;
			}
		}
	}
}
