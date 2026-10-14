using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class ToggleItem : BtnEntity
	{
		private Image btnImage;

		private Text btnText;

		private Color btnImageOriColor;

		private Color btnTextOriColor;

		private GameObject checkMark;

		private void Awake()
		{
			InitToggle();
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

		public void SetCheck()
		{
			checkMark.SetActive(!checkMark.activeSelf);
		}

		public void SetCheck(bool isCheck)
		{
			checkMark.SetActive(isCheck);
		}

		private void InitToggle()
		{
			btnImage = base.transform.Find("BtnImage").GetComponent<Image>();
			btnText = base.transform.Find("BtnText").GetComponent<Text>();
			btnImageOriColor = btnImage.color;
			btnTextOriColor = btnText.color;
			checkMark = btnImage.transform.Find("Checkmark").gameObject;
		}
	}
}
