using System.Collections;
using Car;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class KeTengBiaoDingView : ViewBase
	{
		public GameObject itemGroup;

		public GameObject penalSignalShow;

		private int passCount;

		private bool[] passFlag = new bool[5];

		private Image[] penalImageArray = new Image[4];

		private Image[] throughImageArray = new Image[5];

		public override void Awake()
		{
			base.Awake();
			InitView();
		}

		private void Update()
		{
			UpdateShowPenalSignal();
			if (!passFlag[0] && CarState.Instance.CarSignal.steerWheel < -0.9f)
			{
				StartCoroutine(ShowPassIcon(throughImageArray[0]));
				passFlag[0] = true;
				passCount++;
			}
			if (!passFlag[1] && CarState.Instance.CarSignal.steerWheel > 0.9f)
			{
				StartCoroutine(ShowPassIcon(throughImageArray[1]));
				passFlag[1] = true;
				passCount++;
			}
			if (!passFlag[2] && CarState.Instance.CarSignal.cluth >= 0.9f)
			{
				StartCoroutine(ShowPassIcon(throughImageArray[2]));
				passFlag[2] = true;
				passCount++;
			}
			if (!passFlag[3] && CarState.Instance.CarSignal.footBrake >= 0.9f)
			{
				StartCoroutine(ShowPassIcon(throughImageArray[3]));
				passFlag[3] = true;
				passCount++;
			}
			if (!passFlag[4] && CarState.Instance.CarSignal.oil >= 0.9f)
			{
				StartCoroutine(ShowPassIcon(throughImageArray[4]));
				passFlag[4] = true;
				passCount++;
			}
			if (passCount == 5)
			{
				ViewJumpCtl.Instance.ReturnPreView();
				passCount++;
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}

		private void InitView()
		{
			for (int i = 0; i < itemGroup.transform.childCount; i++)
			{
				throughImageArray[i] = itemGroup.transform.GetChild(i).Find("Icon").GetComponent<Image>();
				passFlag[i] = false;
			}
			for (int j = 0; j < penalSignalShow.transform.childCount; j++)
			{
				penalImageArray[j] = penalSignalShow.transform.GetChild(j).Find("Image").GetComponent<Image>();
			}
		}

		private void UpdateShowPenalSignal()
		{
			penalImageArray[0].fillAmount = CarState.Instance.CarSignal.cluth;
			penalImageArray[1].fillAmount = CarState.Instance.CarSignal.footBrake;
			penalImageArray[2].fillAmount = CarState.Instance.CarSignal.oil;
			penalImageArray[3].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, (0f - CarState.Instance.CarSignal.steerWheel) * CarSignalValue.SteerWheeMaxlAngle));
		}

		private IEnumerator ShowPassIcon(Image image)
		{
			float time = 0f;
			while (time <= 1f)
			{
				image.fillAmount = time;
				time += Time.deltaTime * 2f;
				yield return 1;
			}
			image.fillAmount = 1f;
			yield return 0;
		}
	}
}
