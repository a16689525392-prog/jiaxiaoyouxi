using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class CircleBtnEntity : BtnEntity
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
			if (base.gameObject.activeInHierarchy)
			{
				btnText.color = btnTextOriColor;
				btnImage.color = btnImageOriColor;
				btnImage.transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}

		public override void Select()
		{
			if (base.gameObject.activeInHierarchy)
			{
				btnImage.color = normalSelectImageColor;
				btnImage.transform.localScale = new Vector3(1.05f, 1.05f, 5f);
				btnText.color = normalSelectTextColor;
			}
		}

		private void InitBtn()
		{
			btnImage = base.transform.Find("BtnImage").GetComponent<Image>();
			btnText = base.transform.Find("BtnText").GetComponent<Text>();
			btnImageOriColor = btnImage.color;
			btnTextOriColor = btnText.color;
			if (btnImage == null || btnText == null)
			{
				Debug.LogError(base.gameObject.name + "未找到BtnImage或者BtnText");
			}
		}
	}
}
