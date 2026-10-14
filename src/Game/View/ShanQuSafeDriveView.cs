using UnityEngine;

namespace View
{
	public class ShanQuSafeDriveView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 510;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 5100;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 509;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 5090;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
