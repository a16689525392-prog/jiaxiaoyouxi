using UnityEngine;

namespace View
{
	public class YingjiXunlianView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.itemType = 601;
				NetworkManagement.Instance.BtnLevel = "E1";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.itemType = 602;
				NetworkManagement.Instance.BtnLevel = "E2";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.itemType = 603;
				NetworkManagement.Instance.BtnLevel = "E3";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.itemType = 604;
				NetworkManagement.Instance.BtnLevel = "E4";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.itemType = 605;
				NetworkManagement.Instance.BtnLevel = "E5";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.itemType = 606;
				NetworkManagement.Instance.BtnLevel = "E6";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(6))
			{
				ViewJumpCtl.Instance.itemType = 607;
				NetworkManagement.Instance.BtnLevel = "E7";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(7))
			{
				ViewJumpCtl.Instance.itemType = 608;
				NetworkManagement.Instance.BtnLevel = "E8";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(8))
			{
				ViewJumpCtl.Instance.itemType = 609;
				NetworkManagement.Instance.BtnLevel = "E9";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(9))
			{
				ViewJumpCtl.Instance.itemType = 611;
				NetworkManagement.Instance.BtnLevel = "E10";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(10))
			{
				ViewJumpCtl.Instance.itemType = 612;
				NetworkManagement.Instance.BtnLevel = "E11";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(11))
			{
				ViewJumpCtl.Instance.itemType = 610;
				NetworkManagement.Instance.BtnLevel = "E12";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(12))
			{
				ViewJumpCtl.Instance.itemType = 613;
				NetworkManagement.Instance.BtnLevel = "E13";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(13))
			{
				ViewJumpCtl.Instance.itemType = 614;
				NetworkManagement.Instance.BtnLevel = "E14";
				ViewJumpCtl.Instance.EnterView(ViewType.YingJiModelChooseView);
			}
			else if (go == GetBtn(14))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
