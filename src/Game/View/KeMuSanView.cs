using UnityEngine;

namespace View
{
	public class KeMuSanView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
