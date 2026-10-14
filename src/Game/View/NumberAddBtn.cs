using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class NumberAddBtn : BtnEntity
	{
		private Image btnImage;

		private Text numberText;

		private Color btnImageOriColor;

		private Color numberTextOriColor;

		private void Awake()
		{
			InitBtn();
		}

		public override void Select()
		{
			btnImage.color = Color.green;
			numberText.color = Color.red;
		}

		public override void Exit()
		{
			btnImage.color = btnImageOriColor;
			numberText.color = numberTextOriColor;
		}

		private void InitBtn()
		{
			btnImage = base.transform.Find("BtnImage").GetComponent<Image>();
			numberText = base.transform.Find("NumberText").GetComponent<Text>();
			btnImageOriColor = btnImage.color;
			numberTextOriColor = numberText.color;
		}

		public void SetNumberText(int value)
		{
			numberText.text = value.ToString();
		}

		public void SetHistoryTimeText(int value)
		{
			numberText.text = value.ToString("f0") + " 分钟";
		}
	}
}
