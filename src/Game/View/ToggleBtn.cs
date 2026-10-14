using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class ToggleBtn : BtnEntity
	{
		private GameObject toggleImage;

		private Image btnImage;

		[HideInInspector]
		public Text btnText;

		private Color btnImageOriColor;

		private Color btnTextOriColor;

		private void Awake()
		{
			InitBtn();
		}

		private void InitBtn()
		{
			toggleImage = base.transform.Find("ToggleImage").gameObject;
			btnImage = base.transform.Find("BtnImage").GetComponent<Image>();
			btnText = base.transform.Find("BtnText").GetComponent<Text>();
			btnImageOriColor = btnImage.color;
			btnTextOriColor = btnText.color;
		}

		public override void Exit()
		{
			btnText.color = btnTextOriColor;
			btnImage.color = btnImageOriColor;
		}

		public override void Select()
		{
			btnImage.color = normalSelectImageColor;
			btnText.color = normalSelectTextColor;
		}

		public void SetToggleImageActive(bool isActive)
		{
			toggleImage.SetActive(isActive);
		}
	}
}
