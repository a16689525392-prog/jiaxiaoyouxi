using System.Collections;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.UI;

namespace Scene
{
	public class NormalText : MonoBehaviour
	{
		private void OnEnable()
		{
			StartCoroutine(SetDisable(3f));
		}

		private IEnumerator SetDisable(float time)
		{
			yield return new WaitForSeconds(time);
			yield return new WaitForSeconds((float)base.transform.GetComponent<Text>().text.Length * 0.2f);
			SceneTextShow.Instance.SetTextDisable(base.gameObject);
		}

		public void ShowText(string content)
		{
			base.transform.GetComponent<Text>().text = content;
		}
	}
}
