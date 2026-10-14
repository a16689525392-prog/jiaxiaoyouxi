using Global;
using UnityEngine;

namespace View
{
	public class BiaoDingChooseView : ViewBase
	{
		public override void Start()
		{
			base.Start();
			if (!SettingInfoCtl.Instance.isManualGear)
			{
				GetBtn(2).gameObject.SetActive(value: false);
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.itemType = 5;
				ViewJumpCtl.Instance.EnterView(ViewType.BiaoDingView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.itemType = 0;
				ViewJumpCtl.Instance.EnterView(ViewType.BiaoDingView);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.itemType = 1;
				ViewJumpCtl.Instance.EnterView(ViewType.BiaoDingView);
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.itemType = 2;
				ViewJumpCtl.Instance.EnterView(ViewType.BiaoDingView);
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.itemType = 3;
				ViewJumpCtl.Instance.EnterView(ViewType.BiaoDingView);
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.itemType = 4;
				ViewJumpCtl.Instance.EnterView(ViewType.BiaoDingView);
			}
			else if (go == GetBtn(6))
			{
				ViewJumpCtl.Instance.EnterView(ViewType.BiaoDingDataView);
			}
			else if (go == GetBtn(7))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
