using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class DanBianQiao : CourseTwoItemCtlBase
	{
		private RaycastHit danBianQiaoHitInfo;

		private void Update()
		{
			UpdateDanBianQiao();
			UpdateCarStop();
		}

		private void UpdateDanBianQiao()
		{
			if (itemStep == 1 && CarState.Instance.CurSpeed > 3f)
			{
				if (CarState.Instance.CarSignal.turnLight != 2)
				{
					_ = CarState.Instance.CarSignal.turnLight;
				}
				itemStep++;
			}
			if (itemStep == 2)
			{
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftFrontPoint[0].position, out danBianQiaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftFrontPoint[1].position, out danBianQiaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftFrontPoint[2].position, out danBianQiaoHitInfo, 8)) && danBianQiaoHitInfo.collider.name == "Qiao_Qiao")
				{
					itemStep++;
				}
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftFrontPoint[0].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftFrontPoint[1].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftFrontPoint[2].position, out danBianQiaoHitInfo, 8) && danBianQiaoHitInfo.collider.name == "Qiao_Fall")
				{
					SoundPlayCtl.Instance.PlaySound("有一车轮未上桥", 10f);
					itemStep++;
				}
			}
			if (itemStep == 3)
			{
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftRearPoint[0].position, out danBianQiaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftRearPoint[1].position, out danBianQiaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftRearPoint[2].position, out danBianQiaoHitInfo, 8)) && danBianQiaoHitInfo.collider.name == "Qiao_Qiao")
				{
					itemStep++;
				}
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftRearPoint[0].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftRearPoint[1].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftRearPoint[2].position, out danBianQiaoHitInfo, 8) && danBianQiaoHitInfo.collider.name == "Qiao_Fall")
				{
					SoundPlayCtl.Instance.PlaySound("有一车轮未上桥", 10f);
					itemStep++;
				}
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftFrontPoint[0].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftFrontPoint[1].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelLeftFrontPoint[2].position, out danBianQiaoHitInfo, 8) && danBianQiaoHitInfo.collider.name == "Qiao_Fall")
				{
					SoundPlayCtl.Instance.PlaySound("已骑上桥面,在行驶中出现一个车轮掉下桥面", 10f);
					itemStep++;
				}
			}
			if (itemStep == 4)
			{
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightFrontPoint[0].position, out danBianQiaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightFrontPoint[1].position, out danBianQiaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightFrontPoint[2].position, out danBianQiaoHitInfo, 8)) && danBianQiaoHitInfo.collider.name == "Qiao_Qiao")
				{
					itemStep++;
				}
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightFrontPoint[0].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightFrontPoint[1].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightFrontPoint[2].position, out danBianQiaoHitInfo, 8) && danBianQiaoHitInfo.collider.name == "Qiao_Fall")
				{
					SoundPlayCtl.Instance.PlaySound("有一车轮未上桥", 10f);
					itemStep++;
				}
			}
			if (itemStep == 5)
			{
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightRearPoint[0].position, out danBianQiaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightRearPoint[1].position, out danBianQiaoHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightRearPoint[2].position, out danBianQiaoHitInfo, 8)) && danBianQiaoHitInfo.collider.name == "Qiao_Qiao")
				{
					itemStep++;
				}
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightRearPoint[0].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightRearPoint[1].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightRearPoint[2].position, out danBianQiaoHitInfo, 8) && danBianQiaoHitInfo.collider.name == "Qiao_Fall")
				{
					SoundPlayCtl.Instance.PlaySound("未通过单边桥", 100f);
					itemStep++;
				}
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightFrontPoint[0].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightFrontPoint[1].position, out danBianQiaoHitInfo, 8) && ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.wheelRightFrontPoint[2].position, out danBianQiaoHitInfo, 8) && danBianQiaoHitInfo.collider.name == "Qiao_Fall")
				{
					SoundPlayCtl.Instance.PlaySound("已骑上桥面,在行驶中出现一个车轮掉下桥面", 10f);
					itemStep++;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_Qiao")
			{
				SoundPlayCtl.Instance.PlaySound("通过单边桥考试开始");
				itemStep = 1;
			}
			else if (itemStep != 99 && other.name == "End_Qiao")
			{
				if (itemStep < 6)
				{
					SoundPlayCtl.Instance.PlaySound("未通过单边桥", 100f);
				}
				_ = CarState.Instance.CarSignal.turnLight;
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("通过单边桥考试结束");
				itemStep = 99;
			}
		}
	}
}
