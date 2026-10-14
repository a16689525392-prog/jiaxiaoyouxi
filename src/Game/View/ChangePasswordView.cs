using Global;
using UnityEngine;

namespace View
{
	public class ChangePasswordView : ViewBase
	{
		private GameObject curBtn;

		public override void Start()
		{
			base.Start();
			GetBtn(0).GetComponent<InputBtn>().SetInputContent("");
			GetBtn(1).GetComponent<InputBtn>().SetInputContent("");
			GetBtn(2).GetComponent<InputBtn>().SetInputContent("");
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.PasswordView, isDestroyCurView: false);
				ViewJumpCtl.Instance.curView.GetComponent<PasswordView>().EnterAction = PasswordEnter;
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.PasswordView, isDestroyCurView: false);
				ViewJumpCtl.Instance.curView.GetComponent<PasswordView>().EnterAction = PasswordEnter;
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.PasswordView, isDestroyCurView: false);
				ViewJumpCtl.Instance.curView.GetComponent<PasswordView>().EnterAction = PasswordEnter;
			}
			else if (go == GetBtn(3))
			{
				if (GetBtn(0).GetComponent<InputBtn>().GetInputContent() != SettingInfoCtl.Instance.password)
				{
					ViewJumpCtl.Instance.ShowMsgTips("输入的原始密码错误,请重新输入");
				}
				else if (GetBtn(1).GetComponent<InputBtn>().GetInputContent() != GetBtn(2).GetComponent<InputBtn>().GetInputContent())
				{
					ViewJumpCtl.Instance.ShowMsgTips("两次输入的新密码不一致,请重新输入");
				}
				else if (GetBtn(1).GetComponent<InputBtn>().GetInputContent() == "")
				{
					ViewJumpCtl.Instance.ShowMsgTips("输入的新密码不能为空,请重新输入");
				}
				else
				{
					SettingInfoCtl.Instance.password = GetBtn(1).GetComponent<InputBtn>().GetInputContent();
					SettingInfoCtl.Instance.SaveSetingInfos();
					ViewJumpCtl.Instance.ShowMsgTips("修改密码成功");
					ViewJumpCtl.Instance.ReturnPreView();
				}
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			curBtn = go;
		}

		private void PasswordEnter(string value)
		{
			if (curBtn == GetBtn(0))
			{
				if (value != SettingInfoCtl.Instance.password)
				{
					ViewJumpCtl.Instance.ShowMsgTips("输入的原始密码错误,请重新输入");
				}
				else if (curBtn.GetComponent<InputBtn>() != null)
				{
					curBtn.GetComponent<InputBtn>().SetInputContent(value);
					ViewJumpCtl.Instance.ReturnPreView();
					ViewJumpCtl.Instance.curView.GetComponent<ViewBase>().SelectNextBtn();
				}
			}
			else if (curBtn == GetBtn(1))
			{
				if (curBtn.GetComponent<InputBtn>() != null)
				{
					curBtn.GetComponent<InputBtn>().SetInputContent(value);
					ViewJumpCtl.Instance.ReturnPreView();
					ViewJumpCtl.Instance.curView.GetComponent<ViewBase>().SelectNextBtn();
				}
			}
			else if (curBtn == GetBtn(2))
			{
				if (GetBtn(1).GetComponent<InputBtn>().GetInputContent() != value)
				{
					curBtn.GetComponent<InputBtn>().SetInputContent(value);
					ViewJumpCtl.Instance.ShowMsgTips("两次输入的新密码不一致,请重新输入");
					ViewJumpCtl.Instance.ReturnPreView();
				}
				else if (curBtn.GetComponent<InputBtn>() != null)
				{
					curBtn.GetComponent<InputBtn>().SetInputContent(value);
					ViewJumpCtl.Instance.ReturnPreView();
					ViewJumpCtl.Instance.curView.GetComponent<ViewBase>().SelectNextBtn();
				}
			}
		}
	}
}
