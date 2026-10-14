using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class DFPoDaoDingDian : CourseTwoItemCtlBase
	{
		private RaycastHit dFPoDaoHitInfo;

		private bool isGearNone;

		private bool isHikeHankbreak;

		private Vector3 targetPos;

		private float countermarchTime;

		private bool isCounterMarch;

		private void Update()
		{
			UpdateJudgePoDao();
		}

		private void UpdateJudgePoDao()
		{
			if (itemStep >= 2 && CarState.Instance.CurSpeed > 0.1f && Vector3.Angle(CarState.Instance.CurCar.GetComponent<Rigidbody>().velocity, CarState.Instance.CurCar.transform.forward) > 90f)
			{
				countermarchTime += Time.deltaTime;
			}
			else if (!isCounterMarch && countermarchTime > 1f && countermarchTime < 2f)
			{
				SoundPlayCtl.Instance.PlaySound("上坡时,车辆后溜距离大于30厘米", 10f);
				countermarchTime = 0f;
				isCounterMarch = true;
			}
			else if (!isCounterMarch && countermarchTime > 2f)
			{
				isCounterMarch = true;
				SoundPlayCtl.Instance.PlaySound("上坡时,车辆后溜距离大于50厘米", 100f);
				countermarchTime = 0f;
			}
			if (itemStep == 2 && CarState.Instance.CurSpeed < 0.05f && CarState.Instance.CarSignal.footBrake > 0.8f)
			{
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carFrontPoint[1].position, out dFPoDaoHitInfo, 8))
				{
					if (dFPoDaoHitInfo.collider.name != "CorrectPosition")
					{
						SoundPlayCtl.Instance.PlaySound("停车后,汽车前保险杠距离桩杆线超过30厘米", 10f);
					}
				}
				else
				{
					SoundPlayCtl.Instance.PlaySound("停车后,汽车前保险杠距离桩杆线超过50厘米", 100f);
				}
				if (ColliderDetection.instance.GetTriggerRaycastInfo(ColliderDetection.instance.carMidPoint[2].position, out dFPoDaoHitInfo, 8))
				{
					if (dFPoDaoHitInfo.collider.name == "RightEdge30")
					{
						SoundPlayCtl.Instance.PlaySound("车辆停止后，车身距离路边线超过30厘米", 10f);
					}
				}
				else
				{
					SoundPlayCtl.Instance.PlaySound("车辆停止后，车身距离路边线超过50厘米", 100f);
				}
				itemStep++;
			}
			if (itemStep == 3 || itemStep == 4)
			{
				if (!isGearNone && CarActionJudge.Instance.JudgeActionDone(115))
				{
					itemStep++;
					isGearNone = true;
				}
				if (!isHikeHankbreak && CarState.Instance.CarSignal.handBrake > 0.6f)
				{
					itemStep++;
					isHikeHankbreak = true;
				}
			}
			if (itemStep >= 3 && itemStep <= 5 && !CarActionJudge.Instance.JudgeActionDone(115) && !CarActionJudge.Instance.JudgeActionDone(114) && CarState.Instance.CurSpeed > 0.2f)
			{
				if (!isHikeHankbreak)
				{
					SoundPlayCtl.Instance.PlaySound("停车后,未拉紧手刹", 10f);
				}
				itemStep = 6;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (other.name == "Start_PoDao" && itemStep != 1)
			{
				SoundPlayCtl.Instance.PlaySound("坡道定点停车和起步考试开始");
				itemStep = 1;
			}
			else if (other.name == "Step0" && itemStep != 2)
			{
				itemStep = 2;
			}
			else if (other.name == "Step1" && itemStep != 6)
			{
				if (itemStep == 2)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行坡道定点停车操作", 100f);
				}
				itemStep = 6;
			}
			else if (other.name == "End_PoDao" && itemStep != 99)
			{
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("坡道定点停车和起步考试结束");
				itemStep = 99;
			}
		}
	}
}
