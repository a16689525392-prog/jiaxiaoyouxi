using System.Collections;
using Car;
using UnityEngine;

namespace View
{
	public class GearAnimation : ActionAnimation
	{
		public Transform gearIcon;

		private Vector3[] GearPosition = new Vector3[7]
		{
			new Vector3(0f, 0f),
			new Vector3(-22f, 26f),
			new Vector3(-22f, -26f),
			new Vector3(0f, 26f),
			new Vector3(0f, -26f),
			new Vector3(22f, 26f),
			new Vector3(22f, -26f)
		};

		private Vector3 tempVec0;

		private Vector3 initPositon;

		private Vector3 targetPositon;

		private int targetGear;

		public override void PlayAnimation(int action)
		{
			isPlayEnd = false;
			gearIcon.localPosition = GearPosition[CarState.Instance.CarSignal.gear];
			initPositon = gearIcon.localPosition;
			curAction = action;
			switch (action)
			{
			case 34:
				targetGear = 1;
				break;
			case 35:
				targetGear = 2;
				break;
			case 36:
				targetGear = 3;
				break;
			case 37:
				targetGear = 4;
				break;
			case 38:
				targetGear = 5;
				break;
			case 40:
				targetGear = 6;
				break;
			case 39:
				targetGear = 0;
				break;
			}
			if (CarState.Instance.CarSignal.gear != targetGear)
			{
				targetPositon = GearPosition[targetGear];
				StartCoroutine(PosLerp());
			}
		}

		private IEnumerator PosLerp()
		{
			_ = Vector3.zero;
			Vector3 curVec = Vector3.zero;
			float timeCount = 0f;
			int step = 0;
			Vector3 tempVec = new Vector3(initPositon.x, 0f);
			while (step != -1)
			{
				timeCount += Time.deltaTime * 2f;
				switch (step)
				{
				case 0:
					gearIcon.localPosition = Vector3.Lerp(initPositon, tempVec, timeCount);
					if (timeCount >= 1f || Vector3.Distance(initPositon, tempVec) < 0.5f)
					{
						gearIcon.localPosition = tempVec;
						curVec = gearIcon.localPosition;
						timeCount = 0f;
						if (targetPositon.x != tempVec.x)
						{
							tempVec = new Vector3(targetPositon.x, 0f);
							step = 1;
						}
						else
						{
							step = 2;
						}
					}
					break;
				case 1:
					gearIcon.localPosition = Vector3.Lerp(curVec, tempVec, timeCount);
					if (timeCount >= 1f || Vector3.Distance(initPositon, tempVec) < 0.5f)
					{
						gearIcon.localPosition = tempVec;
						timeCount = 0f;
						curVec = gearIcon.localPosition;
						step = 2;
					}
					break;
				case 2:
					gearIcon.localPosition = Vector3.Lerp(curVec, targetPositon, timeCount);
					if (timeCount >= 1f || Vector3.Distance(initPositon, targetPositon) < 0.5f)
					{
						gearIcon.localPosition = targetPositon;
						isPlayEnd = true;
						yield break;
					}
					break;
				}
				yield return 0;
			}
		}
	}
}
