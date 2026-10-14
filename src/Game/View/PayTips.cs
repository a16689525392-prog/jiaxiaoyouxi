using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class PayTips : MonoBehaviour
	{
		public Text resultText;

		public GameObject icon;

		public Image bg;

		public static List<GameObject> payTipsList = new List<GameObject>();

		private Coroutine cor;

		private float value;

		private bool isUp;

		private Vector3 targetPos;

		private Vector3 oriPosition;

		private void Start()
		{
			cor = StartCoroutine(DestroyThis());
		}

		private void Update()
		{
			if (isUp)
			{
				value += Time.deltaTime * 3f;
				base.transform.localPosition = Vector3.Lerp(oriPosition, targetPos, value);
				if (value >= 1f)
				{
					base.transform.localPosition = targetPos;
					isUp = false;
					value = 0f;
				}
			}
		}

		public void SetTipsContent(string content)
		{
			resultText.text = content;
			icon.transform.localPosition = new Vector3(-19f - (float)(7 * resultText.text.Length), 0f);
			if (resultText.text.Length > 11)
			{
				bg.rectTransform.sizeDelta = new Vector2(300f + 15f * (float)(resultText.text.Length - 11), 25f);
			}
		}

		public void UpMove()
		{
			isUp = true;
			value = 0f;
			oriPosition = base.transform.localPosition;
			targetPos = oriPosition + new Vector3(0f, 27f, 0f);
		}

		private IEnumerator DestroyThis(float time = 3f)
		{
			yield return new WaitForSeconds(time);
			cor = null;
			payTipsList.Remove(base.gameObject);
			Object.Destroy(base.gameObject);
		}

		private void OnDestroy()
		{
			if (cor != null)
			{
				StopCoroutine(cor);
			}
		}
	}
}
