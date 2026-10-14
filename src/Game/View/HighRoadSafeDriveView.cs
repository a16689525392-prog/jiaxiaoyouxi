using UnityEngine;

namespace View
{
	public class HighRoadSafeDriveView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 506;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 507;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
