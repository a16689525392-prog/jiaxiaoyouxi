using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class LightGuideView : MonoBehaviour
	{
		public Text contentText;

		private void Update()
		{
			contentText.text = NightLightCtl.actionGuide;
		}
	}
}
