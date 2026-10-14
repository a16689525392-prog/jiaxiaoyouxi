using System.Collections;
using Car;
using Global;
using UnityEngine;

namespace View
{
	public class PenalAnimation : ActionAnimation
	{
		public Transform cluthIcon;

		public Transform footbreakIcon;

		public Transform oilIcon;

		public Transform cluthUp;

		public Transform cluthDown;

		public Transform footbreakUp;

		public Transform footbreakDown;

		public Transform oilUp;

		public Transform oilDown;

		private Transform icon;

		private Transform dirImg;

		private Vector3 UpVec3 = new Vector3(456f, 217f, 0f);

		private Vector3 DownVec3 = new Vector3(456f, 165f, 0f);

		private Vector3 TaponVec3 = new Vector3(456f, 200f, 0f);

		private Vector3 HalfVec3 = new Vector3(456f, 190f, 0f);

		private Vector3 Vec4_3 = new Vector3(456f, 203f, 0f);

		private Vector3 Vec4_1 = new Vector3(456f, 178f, 0f);

		private float baseTime = 0.5f;

		private Vector3 initpos;

		private int actionType;

		private void Start()
		{
			if (!SettingInfoCtl.Instance.isManualGear)
			{
				cluthIcon.parent.gameObject.SetActive(value: false);
			}
		}

		protected override void Update()
		{
			base.Update();
			if (actionType == 0)
			{
				footbreakIcon.localPosition = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.footBrake, 0f);
				oilIcon.localPosition = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.oil, 0f);
			}
			else if (actionType == 1)
			{
				cluthIcon.localPosition = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.cluth, 0f);
				oilIcon.localPosition = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.oil, 0f);
			}
			else if (actionType == 2)
			{
				cluthIcon.localPosition = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.cluth, 0f);
				footbreakIcon.localPosition = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.footBrake, 0f);
			}
		}

		public override void PlayAnimation(int action)
		{
			curAction = action;
			isPlayEnd = false;
			switch (action)
			{
			case 18:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.cluth, 0f);
				icon = cluthIcon;
				actionType = 0;
				InitDirImg();
				dirImg = cluthDown;
				StartCoroutine(VectorLerp(baseTime, initpos, DownVec3));
				break;
			case 19:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.cluth, 0f);
				icon = cluthIcon;
				actionType = 0;
				InitDirImg();
				dirImg = cluthUp;
				StartCoroutine(VectorLerp(baseTime * 2f, initpos, HalfVec3));
				break;
			case 20:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.cluth, 0f);
				icon = cluthIcon;
				actionType = 0;
				InitDirImg();
				dirImg = cluthUp;
				StartCoroutine(VectorLerp(baseTime, initpos, UpVec3));
				break;
			case 41:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.cluth, 0f);
				icon = cluthIcon;
				actionType = 0;
				InitDirImg();
				dirImg = cluthUp;
				StartCoroutine(VectorLerp(baseTime * 5f, initpos, UpVec3));
				break;
			case 111:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.cluth, 0f);
				icon = cluthIcon;
				actionType = 0;
				InitDirImg();
				dirImg = cluthUp;
				StartCoroutine(VectorLerp(baseTime * 2f, initpos, Vec4_3));
				break;
			case 112:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.cluth, 0f);
				icon = cluthIcon;
				actionType = 0;
				InitDirImg();
				dirImg = cluthDown;
				StartCoroutine(VectorLerp(baseTime * 2f, initpos, Vec4_1));
				break;
			case 21:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.footBrake, 0f);
				icon = footbreakIcon;
				actionType = 1;
				InitDirImg();
				dirImg = footbreakDown;
				StartCoroutine(VectorLerp(baseTime, initpos, DownVec3));
				break;
			case 22:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.footBrake, 0f);
				icon = footbreakIcon;
				actionType = 1;
				InitDirImg();
				dirImg = footbreakDown;
				StartCoroutine(VectorLerp(baseTime, initpos, TaponVec3));
				break;
			case 23:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.footBrake, 0f);
				icon = footbreakIcon;
				actionType = 1;
				InitDirImg();
				dirImg = footbreakUp;
				StartCoroutine(VectorLerp(baseTime, initpos, UpVec3));
				break;
			case 68:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.footBrake, 0f);
				icon = footbreakIcon;
				actionType = 1;
				InitDirImg();
				dirImg = footbreakUp;
				StartCoroutine(VectorLerp(baseTime, initpos, Vec4_3));
				break;
			case 24:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.oil, 0f);
				icon = oilIcon;
				actionType = 2;
				InitDirImg();
				dirImg = oilDown;
				StartCoroutine(VectorLerp(baseTime, initpos, DownVec3));
				break;
			case 25:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.oil, 0f);
				icon = oilIcon;
				actionType = 2;
				InitDirImg();
				dirImg = oilDown;
				StartCoroutine(VectorLerp(baseTime, initpos, TaponVec3));
				break;
			case 26:
				initpos = new Vector3(456f, 217f - 52f * CarState.Instance.CarSignal.oil, 0f);
				icon = oilIcon;
				actionType = 2;
				InitDirImg();
				dirImg = oilUp;
				StartCoroutine(VectorLerp(baseTime, initpos, UpVec3));
				break;
			}
		}

		private IEnumerator VectorLerp(float time, Vector3 initPos, Vector3 tarPos)
		{
			float curTime = 0f;
			dirImg.gameObject.SetActive(value: true);
			while (curTime < time)
			{
				curTime += Time.deltaTime;
				icon.localPosition = Vector3.Lerp(initPos, tarPos, curTime / time);
				yield return 0;
			}
			icon.localPosition = tarPos;
			isPlayEnd = true;
		}

		private void InitDirImg()
		{
			cluthUp.gameObject.SetActive(value: false);
			cluthDown.gameObject.SetActive(value: false);
			footbreakUp.gameObject.SetActive(value: false);
			footbreakDown.gameObject.SetActive(value: false);
			oilUp.gameObject.SetActive(value: false);
			oilDown.gameObject.SetActive(value: false);
		}
	}
}
