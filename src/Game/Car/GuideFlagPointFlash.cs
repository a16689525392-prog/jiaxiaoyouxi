using System.Collections;
using UnityEngine;

namespace Car
{
	public class GuideFlagPointFlash : MonoBehaviour
	{
		public float intervalTime = 0.5f;

		private MeshRenderer mr;

		private Coroutine cor;

		private bool isFlash = true;

		private void OnEnable()
		{
			mr = base.gameObject.GetComponent<MeshRenderer>();
			isFlash = true;
			cor = StartCoroutine(Flash());
		}

		private void OnDisable()
		{
			StopCoroutine(cor);
		}

		private IEnumerator Flash()
		{
			while (isFlash)
			{
				mr.enabled = !mr.enabled;
				yield return new WaitForSeconds(0.5f);
			}
		}

		private void OnDestroy()
		{
			isFlash = false;
		}
	}
}
