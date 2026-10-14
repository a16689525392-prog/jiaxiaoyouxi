using System.Collections;
using Car;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class SteerWheelAnimation : ActionAnimation
	{
		public Transform icon;

		public Text value;

		private Vector3 CenterRotationAngle = Vector3.zero;

		private Vector3 initAngle;

		private Vector3 targetAngle;

		private float time;

		private const float BaseTime = 0.4f;

		public override void PlayAnimation(int action)
		{
			curAction = action;
			isPlayEnd = false;
			initAngle = new Vector3(0f, 0f, CarState.Instance.CarSignal.steerWheel * (0f - CarSignalValue.SteerWheeMaxlAngle));
			switch (action)
			{
			case 32:
				targetAngle = new Vector3(0f, 0f, 90f);
				time = 0.4f;
				break;
			case 30:
				time = 0.8f;
				targetAngle = new Vector3(0f, 0f, 180f);
				break;
			case 16:
				time = 1.6f;
				targetAngle = new Vector3(0f, 0f, 360f);
				break;
			case 13:
				time = 2.4f;
				targetAngle = new Vector3(0f, 0f, CarSignalValue.SteerWheeMaxlAngle);
				break;
			case 44:
				time = 0.4f;
				targetAngle = new Vector3(0f, 0f, 45f);
				break;
			case 15:
				time = 2.4f * Mathf.Abs(CarState.Instance.CarSignal.steerWheel);
				targetAngle = new Vector3(0f, 0f, 0f);
				break;
			case 33:
				time = 0.4f;
				targetAngle = new Vector3(0f, 0f, -90f);
				break;
			case 31:
				time = 0.8f;
				targetAngle = new Vector3(0f, 0f, -180f);
				break;
			case 17:
				time = 1.6f;
				targetAngle = new Vector3(0f, 0f, -360f);
				break;
			case 14:
				time = 2.4f;
				targetAngle = new Vector3(0f, 0f, 0f - CarSignalValue.SteerWheeMaxlAngle);
				break;
			case 45:
				time = 0.4f;
				targetAngle = new Vector3(0f, 0f, -45f);
				break;
			}
			StartCoroutine(AngleLerp(time, initAngle, targetAngle));
		}

		private IEnumerator AngleLerp(float time, Vector3 initAngle, Vector3 targetAngle)
		{
			float curTime = 0f;
			while (curTime < time)
			{
				curTime += Time.deltaTime;
				icon.localEulerAngles = Vector3.Lerp(initAngle, targetAngle, curTime / time);
				value.text = (initAngle.z + (targetAngle.z - initAngle.z) * curTime / time).ToString("f0");
				yield return 0;
			}
			icon.localEulerAngles = targetAngle;
			value.text = targetAngle.z.ToString("f0");
			isPlayEnd = true;
		}
	}
}
