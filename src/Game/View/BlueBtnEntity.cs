using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class BlueBtnEntity : BtnEntity
	{
		private Image btnImage;

		private Text btnText;

		private Color btnImageOriColor;

		private Color btnTextOriColor;

		private void Awake()
		{
			InitBtn();
		}

		private void Update()
		{
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

		private void InitBtn()
		{
			btnImage = base.transform.Find("BtnImage").GetComponent<Image>();
			btnText = base.transform.Find("BtnText").GetComponent<Text>();
			btnImageOriColor = btnImage.color;
			btnTextOriColor = btnText.color;
		}
	}
}
