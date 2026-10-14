using UnityEngine;

namespace View
{
	public class DaCheCourseTwoChooseView : ViewBase
	{
		public static int curClickBtnIndex;

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.itemType = 9;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.itemType = 13;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.itemType = 12;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.itemType = 11;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.itemType = 14;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.itemType = 15;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(6))
			{
				ViewJumpCtl.Instance.itemType = 16;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(7))
			{
				ViewJumpCtl.Instance.itemType = 10;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(8))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
