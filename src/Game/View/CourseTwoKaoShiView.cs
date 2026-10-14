using UnityEngine;

namespace View
{
	public class CourseTwoKaoShiView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.itemType = 1200;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
