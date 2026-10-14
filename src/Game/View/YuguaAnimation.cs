using System.Collections;
using UnityEngine;

namespace View
{
	public class YuguaAnimation : ActionAnimation
	{
		public Transform yuguaIconUp;

		public Transform yuguaIconDown;

		private Vector3 downAngle = new Vector3(0f, 0f, -120f);

		private Vector3 upAngel = new Vector3(0f, 0f, -180f);

		private Vector3 initAngle;

		private Vector3 targetAngle;

		private float time = 0.5f;

		public override void PlayAnimation(int action)
		{
			curAction = action;
			isPlayEnd = false;
			switch (action)
			{
			case 56:
				initAngle = downAngle;
				targetAngle = upAngel;
				break;
			case 54:
			case 55:
				initAngle = upAngel;
				targetAngle = downAngle;
				break;
			}
			StartCoroutine(AngleLerp());
		}

		private IEnumerator AngleLerp()
		{
			float curTime = 0f;
			if (curAction == 56)
			{
				curTime -= 0.5f;
			}
			while (curTime < time)
			{
				curTime += Time.deltaTime;
				switch (curAction)
				{
				case 54:
				case 55:
					yuguaIconDown.gameObject.SetActive(value: false);
					yuguaIconUp.localEulerAngles = Vector3.Lerp(initAngle, targetAngle, curTime / time);
					break;
				case 56:
					yuguaIconUp.gameObject.SetActive(value: false);
					yuguaIconDown.gameObject.SetActive(value: true);
					if (curTime > 0f)
					{
						yuguaIconUp.gameObject.SetActive(value: true);
						yuguaIconDown.gameObject.SetActive(value: false);
						yuguaIconUp.localEulerAngles = Vector3.Lerp(initAngle, targetAngle, curTime / time);
					}
					break;
				}
				yield return 0;
			}
			isPlayEnd = true;
		}
	}
}
