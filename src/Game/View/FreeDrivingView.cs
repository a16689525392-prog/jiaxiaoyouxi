using UnityEngine;

namespace View
{
	public class FreeDrivingView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 401;
				NetworkManagement.Instance.BtnLevel = "D1";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 402;
				NetworkManagement.Instance.BtnLevel = "D2";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 400;
				NetworkManagement.Instance.BtnLevel = "D3";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 403;
				NetworkManagement.Instance.BtnLevel = "D4";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
