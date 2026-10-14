using UnityEngine;

namespace View
{
	public class CourseTwoDFChooseView : ViewBase
	{
		public static int curClickBtnIndex;

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.itemType = 5;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.itemType = 1;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.itemType = 2;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.itemType = 3;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.itemType = 4;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.itemType = 6;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(6))
			{
				ViewJumpCtl.Instance.itemType = 7;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(7))
			{
				ViewJumpCtl.Instance.itemType = 8;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(8))
			{
				ViewJumpCtl.Instance.itemType = 9;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(9))
			{
				ViewJumpCtl.Instance.itemType = 13;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(10))
			{
				ViewJumpCtl.Instance.itemType = 12;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(11))
			{
				ViewJumpCtl.Instance.itemType = 11;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(12))
			{
				ViewJumpCtl.Instance.itemType = 14;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(13))
			{
				ViewJumpCtl.Instance.itemType = 15;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(14))
			{
				ViewJumpCtl.Instance.itemType = 16;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(15))
			{
				ViewJumpCtl.Instance.itemType = 10;
				ViewJumpCtl.Instance.EnterView(ViewType.DaCheModelChooseView);
			}
			else if (go == GetBtn(16))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			curClickBtnIndex = mSelectBtnIndex;
		}
	}
}
