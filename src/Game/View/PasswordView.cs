using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View
{
	public class PasswordView : ViewBase
	{
		public GameObject tips;

		public Text password;

		public UnityAction<string> EnterAction;

		public UnityAction<string> btnClickAction;

		private string value = "";

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				value += "1";
			}
			else if (go == GetBtn(1))
			{
				value += "2";
			}
			else if (go == GetBtn(2))
			{
				value += "3";
			}
			else if (go == GetBtn(3))
			{
				value += "4";
			}
			else if (go == GetBtn(4))
			{
				value += "5";
			}
			else if (go == GetBtn(5))
			{
				value += "6";
			}
			else if (go == GetBtn(6))
			{
				value += "7";
			}
			else if (go == GetBtn(7))
			{
				value += "8";
			}
			else if (go == GetBtn(8))
			{
				value += "9";
			}
			else if (go == GetBtn(9))
			{
				value += "0";
			}
			else if (go == GetBtn(10))
			{
				if (value != "")
				{
					value = value.Substring(0, value.Length - 1);
				}
			}
			else if (go == GetBtn(11))
			{
				EnterAction?.Invoke(value);
			}
			else if (go == GetBtn(12))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			btnClickAction?.Invoke(value);
			password.text = "";
			for (int i = 0; i < value.Length; i++)
			{
				password.text += "*";
			}
		}

		public void ResetPasswordInput()
		{
			value = "";
		}

		public void ShowDefaultTips(bool active)
		{
			tips.SetActive(active);
		}
	}
}
