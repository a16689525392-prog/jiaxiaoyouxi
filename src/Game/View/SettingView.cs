using Car;
using UnityEngine;

namespace View
{
	public class SettingView : ViewBase
	{
		public static int curClickBtnIndex;

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				if (SignalSourceCtl.Instance.connectStatus == 2)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView);
				}
				else
				{
					ViewJumpCtl.Instance.EnterView(ViewType.BiaoDingChooseView);
				}
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.CanShuSettingView);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.ChangePassword);
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.SetResolutionView);
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.AutoGearSettingView);
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.SignalTestView);
			}
			else if (go == GetBtn(6))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			curClickBtnIndex = mSelectBtnIndex;
		}
	}
}
