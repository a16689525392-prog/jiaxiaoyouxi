using System.Collections;
using UnityEngine;

namespace View
{
	public class HandbreakAnimation : ActionAnimation
	{
		public Transform handbreakIconUp;

		public Transform handbreakIconDown;

		private Vector3 upAngle = Vector3.zero;

		private Vector3 downAngel = new Vector3(0f, 0f, 35f);

		private Vector3 initAngle;

		private Vector3 targetAngle;

		private float time = 0.5f;

		public override void PlayAnimation(int action)
		{
			curAction = action;
			isPlayEnd = false;
			switch (action)
			{
			case 27:
				initAngle = downAngel;
				targetAngle = upAngle;
				break;
			case 29:
				initAngle = upAngle;
				targetAngle = downAngel;
				break;
			}
			StartCoroutine(AngleLerp());
		}

		private IEnumerator AngleLerp()
		{
			float curTime = 0f;
			if (curAction == 29)
			{
				curTime -= 0.5f;
			}
			while (curTime < time)
			{
				curTime += Time.deltaTime;
				switch (curAction)
				{
				case 27:
					handbreakIconDown.gameObject.SetActive(value: false);
					handbreakIconUp.localEulerAngles = Vector3.Lerp(initAngle, targetAngle, curTime / time);
					break;
				case 29:
					handbreakIconUp.gameObject.SetActive(value: true);
					handbreakIconDown.gameObject.SetActive(value: false);
					if (curTime > 0f)
					{
						handbreakIconUp.gameObject.SetActive(value: false);
						handbreakIconDown.gameObject.SetActive(value: true);
						handbreakIconDown.localEulerAngles = Vector3.Lerp(initAngle, targetAngle, curTime / time);
					}
					break;
				}
				yield return 0;
			}
			isPlayEnd = true;
		}
	}
}
