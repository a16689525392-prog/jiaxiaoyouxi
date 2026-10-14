using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class LoadingView : ViewBase
	{
		public Text progressText;

		public GameObject Imagecneterbig;

		public GameObject Imagecneter;

		public GameObject lightleft;

		public GameObject lightright;

		public Image ProgressImageLeft;

		public Image ProgressImageRight;

		private float value;

		public static LoadingView instance { get; private set; }

		public override void Awake()
		{
			base.Awake();
			instance = this;
			progressText.text = "0%";
			Imagecneterbig.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			Imagecneter.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			lightleft.transform.localPosition = new Vector3(Convert.ToSingle(Screen.width) / 1920f * -360f, 47.5f, 0f);
			lightright.transform.localPosition = new Vector3(Convert.ToSingle(Screen.width) / 1920f * 360f, 47.5f, 0f);
			ProgressImageLeft.transform.localPosition = new Vector3(Convert.ToSingle(Screen.width) / 1920f * -366f, 50f, 0f);
			ProgressImageRight.transform.localPosition = new Vector3(Convert.ToSingle(Screen.width) / 1920f * 366f, 50f, 0f);
		}

		public void Update()
		{
			Imagecneterbig.transform.localEulerAngles += new Vector3(0f, 0f, 1f);
			Imagecneter.transform.localEulerAngles -= new Vector3(0f, 0f, 1.5f);
		}

		public void ShowLoadingSceneProgress(AsyncOperation async)
		{
			if (async != null)
			{
				StartCoroutine(SetLoadingProgress(async));
			}
		}

		public IEnumerator SetLoadingProgress(AsyncOperation async)
		{
			async.allowSceneActivation = false;
			while (!async.isDone)
			{
				if (async.progress <= 0.9f)
				{
					value += 0.005f;
				}
				if (value >= 1f)
				{
					value = 1f;
				}
				ProgressImageLeft.fillAmount = value;
				ProgressImageRight.fillAmount = value;
				progressText.text = (value * 100f).ToString("f0") + "%";
				if (value == 1f)
				{
					async.allowSceneActivation = true;
				}
				yield return null;
			}
		}

		public void SetLoadingProgress(float value)
		{
			progressText.text = (value * 100f).ToString("f0") + "%";
		}
	}
}
