using UnityEngine;

namespace View
{
	public class LiLunModelChooseView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.itemType = 1001;
				ViewJumpCtl.Instance.EnterView(ViewType.TiMuShowView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.itemType = 1002;
				ViewJumpCtl.Instance.EnterView(ViewType.TiMuShowView);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.itemType = 1003;
				ViewJumpCtl.Instance.EnterView(ViewType.TiMuShowView);
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
