using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class InputBtn : BtnEntity
	{
		private Text btnText;

		private Image btnImage;

		private Text inputText;

		private Color textOriColor;

		private Color imageOriColor;

		private void Awake()
		{
			btnText = base.transform.Find("BtnText").GetComponent<Text>();
			btnImage = base.transform.Find("BtnImage").GetComponent<Image>();
			inputText = base.transform.Find("InputText").GetComponent<Text>();
			textOriColor = btnText.color;
			imageOriColor = btnImage.color;
		}

		public override void Select()
		{
			btnText.color = Color.red;
			btnImage.color = new Color(0.5137255f, 0.8784314f, 1f, 22f / 85f);
		}

		public override void Exit()
		{
			btnText.color = textOriColor;
			btnImage.color = imageOriColor;
		}

		public void SetInputContent(string value)
		{
			inputText.text = value;
		}

		public string GetInputContent()
		{
			return inputText.text;
		}
	}
}
