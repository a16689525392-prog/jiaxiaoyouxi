using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View
{
	public class CountDownCtl : MonoBehaviour
	{
		public List<Sprite> numberSpriteList = new List<Sprite>();

		[HideInInspector]
		public UnityAction CountDownEndAction;

		public Image timeImage;

		private Animator animator;

		private Coroutine cor;

		private void Awake()
		{
			timeImage.sprite = null;
			animator = base.transform.GetComponent<Animator>();
			SetCountDownTime(6);
		}

		public void SetCountDownTime(int time)
		{
			if (cor != null)
			{
				StopCountDown();
			}
			base.gameObject.SetActive(value: true);
			cor = StartCoroutine(CountDown(time));
		}

		private IEnumerator CountDown(int time)
		{
			for (int startIndex = time - 1; startIndex > 0; startIndex--)
			{
				timeImage.sprite = numberSpriteList[startIndex];
				animator.SetTrigger("Scale");
				yield return new WaitForSeconds(1f);
			}
			if (time <= 0)
			{
				timeImage.sprite = numberSpriteList[0];
				yield return new WaitForSeconds(0.3f);
			}
			if (CountDownEndAction != null)
			{
				CountDownEndAction();
			}
			timeImage.sprite = null;
			base.gameObject.SetActive(value: false);
			yield return 0;
		}

		public void StopCountDown()
		{
			if (cor != null)
			{
				StopCoroutine(cor);
			}
			timeImage.sprite = null;
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
		}
	}
}
