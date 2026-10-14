using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class DaoZhuang : CourseTwoItemCtlBase
	{
		private RaycastHit daoZhuangHitInfo;

		public bool isOver;

		private void Update()
		{
			UpdateJudgeDaoZhuang();
		}

		private void UpdateJudgeDaoZhuang()
		{
			if (itemStep == 1)
			{
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[0].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[1].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[2].position, out daoZhuangHitInfo, 8)) && daoZhuangHitInfo.collider.name == "Zhuang_JiaFront")
				{
					SoundPlayCtl.Instance.PlaySound("不按规定路线顺序行驶", 100f);
					itemStep++;
				}
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out daoZhuangHitInfo, 8)) && daoZhuangHitInfo.collider.name == "Zhuang_YiFront" && !CarActionJudge.Instance.JudgeActionDone(114))
				{
					SoundPlayCtl.Instance.PlaySound("倒库不入", 100f);
					itemStep++;
				}
			}
			if (itemStep == 2)
			{
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[2].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[2].position, out daoZhuangHitInfo, 8)) && daoZhuangHitInfo.collider.name == "Zhuang_Jia" && !CarActionJudge.Instance.JudgeActionDone(114))
				{
					SoundPlayCtl.Instance.PlaySound("倒库不入", 100f);
					itemStep++;
				}
				if (!CarActionJudge.Instance.JudgeActionDone(114))
				{
					itemStep++;
				}
			}
			else if (itemStep == 3)
			{
				if (CarActionJudge.Instance.JudgeActionDone(114))
				{
					itemStep++;
				}
			}
			else if (itemStep == 4)
			{
				if (!CarActionJudge.Instance.JudgeActionDone(114))
				{
					itemStep++;
				}
			}
			else if (itemStep == 5)
			{
				if (CarActionJudge.Instance.JudgeActionDone(114))
				{
					itemStep++;
				}
			}
			else if (itemStep == 7)
			{
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[0].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[0].position, out daoZhuangHitInfo, 8)) && daoZhuangHitInfo.collider.name == "Zhuang_Yi" && !CarActionJudge.Instance.JudgeActionDone(114))
				{
					SoundPlayCtl.Instance.PlaySound("移库不入", 100f);
					itemStep++;
				}
				if (!CarActionJudge.Instance.JudgeActionDone(114))
				{
					itemStep++;
				}
			}
			else if (itemStep == 8)
			{
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out daoZhuangHitInfo, 8))
				{
					if (daoZhuangHitInfo.collider.name == "Zhuang_JiaFront")
					{
						SoundPlayCtl.Instance.PlaySound("不按规定路线顺序行驶", 100f);
						itemStep++;
					}
					else if (daoZhuangHitInfo.collider.name == "Zhuang_YiFront")
					{
						itemStep++;
					}
				}
			}
			else if (itemStep == 10)
			{
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[0].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[1].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[2].position, out daoZhuangHitInfo, 8)) && daoZhuangHitInfo.collider.name == "Zhuang_Yi")
				{
					SoundPlayCtl.Instance.PlaySound("不按规定路线顺序行驶", 100f);
					itemStep++;
				}
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[2].position, out daoZhuangHitInfo, 8)) && !isOver && daoZhuangHitInfo.collider.name == "Zhuang_JiaFront" && !CarActionJudge.Instance.JudgeActionDone(114))
				{
					SoundPlayCtl.Instance.PlaySound("移库不入", 100f);
					itemStep++;
				}
				if ((ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[0].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[0].position, out daoZhuangHitInfo, 8) || ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carRearPoint[0].position, out daoZhuangHitInfo, 8)) && daoZhuangHitInfo.collider.name == "Zhuang_Yi" && !CarActionJudge.Instance.JudgeActionDone(114))
				{
					SoundPlayCtl.Instance.PlaySound("移库不入", 100f);
					itemStep++;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start_DaoZhuang")
			{
				SoundPlayCtl.Instance.PlaySound("倒桩考试开始");
				itemStep = 1;
			}
			if (itemStep == 1 && other.name == "Yi_KuDi")
			{
				itemStep = 2;
			}
			if (itemStep == 6 && other.name == "Jia_KuDi")
			{
				itemStep = 7;
			}
			if (itemStep == 9 && other.name == "OutKu")
			{
				itemStep = 10;
			}
			if (itemStep == 10 && other.name == "Jia_KuDi")
			{
				isOver = true;
			}
			if (itemStep < 11 && !isOver && other.name == "End_DaoZhuang")
			{
				SoundPlayCtl.Instance.PlaySound("不按规定路线顺序行驶", 100f);
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("倒桩考试结束");
				itemStep = 99;
			}
			else if ((itemStep == 11 || isOver) && other.name == "End_DaoZhuang")
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("倒桩考试结束");
				itemStep = 99;
			}
		}
	}
}
