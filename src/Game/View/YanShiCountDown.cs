using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View
{
	public class YanShiCountDown : MonoBehaviour
	{
		private static YanShiCountDown instance;

		public List<Sprite> numberTimeList = new List<Sprite>();

		[HideInInspector]
		public UnityAction CountDownStopAction;

		public Image countImage;

		private Animator animator;

		private Coroutine cor;

		public static YanShiCountDown Instance => instance;

		private void Awake()
		{
			instance = this;
			countImage.sprite = null;
			animator = base.transform.GetComponent<Animator>();
			CountDownTime(6);
		}

		public void CountDownTime(int time)
		{
			if (cor != null)
			{
				CountDownEnd();
			}
			base.gameObject.SetActive(value: true);
			cor = StartCoroutine(CountDownPlay(time));
		}

		private IEnumerator CountDownPlay(int time)
		{
			for (int startIndex = time - 1; startIndex > 0; startIndex--)
			{
				countImage.sprite = numberTimeList[startIndex];
				animator.SetTrigger("Scale");
				yield return new WaitForSeconds(1f);
			}
			if (time <= 0)
			{
				countImage.sprite = numberTimeList[0];
				yield return new WaitForSeconds(0.3f);
			}
			if (CountDownStopAction != null)
			{
				CountDownStopAction();
			}
			countImage.sprite = null;
			base.gameObject.SetActive(value: false);
			yield return 0;
		}

		public void CountDownEnd()
		{
			if (cor != null)
			{
				StopCoroutine(cor);
			}
			countImage.sprite = null;
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
		}
	}
}
