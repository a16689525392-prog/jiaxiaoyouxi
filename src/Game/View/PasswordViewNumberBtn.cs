using UnityEngine;

namespace View
{
	public class PasswordViewNumberBtn : BtnEntity
	{
		private GameObject btnImage;

		private void Awake()
		{
			btnImage = base.transform.Find("BtnImage").gameObject;
		}

		public override void Select()
		{
			btnImage.SetActive(value: true);
		}

		public override void Exit()
		{
			btnImage.SetActive(value: false);
		}
	}
}
