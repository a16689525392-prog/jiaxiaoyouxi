using UnityEngine;

namespace View
{
	public class CourseThreeItemChooseView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go != GetBtn(14))
			{
				if (go == GetBtn(0))
				{
					ViewJumpCtl.Instance.itemType = 200;
				}
				else if (go == GetBtn(1))
				{
					ViewJumpCtl.Instance.itemType = 201;
				}
				else if (go == GetBtn(2))
				{
					ViewJumpCtl.Instance.itemType = 209;
				}
				else if (go == GetBtn(3))
				{
					ViewJumpCtl.Instance.itemType = 202;
				}
				else if (go == GetBtn(4))
				{
					ViewJumpCtl.Instance.itemType = 203;
				}
				else if (go == GetBtn(5))
				{
					ViewJumpCtl.Instance.itemType = 204;
				}
				else if (go == GetBtn(6))
				{
					ViewJumpCtl.Instance.itemType = 205;
				}
				else if (go == GetBtn(7))
				{
					ViewJumpCtl.Instance.itemType = 206;
				}
				else if (go == GetBtn(8))
				{
					ViewJumpCtl.Instance.itemType = 207;
				}
				else if (go == GetBtn(9))
				{
					ViewJumpCtl.Instance.itemType = 208;
				}
				else if (go == GetBtn(10))
				{
					ViewJumpCtl.Instance.itemType = 210;
				}
				else if (go == GetBtn(11))
				{
					ViewJumpCtl.Instance.itemType = 211;
				}
				else if (go == GetBtn(12))
				{
					ViewJumpCtl.Instance.itemType = 212;
				}
				else if (go == GetBtn(13))
				{
					ViewJumpCtl.Instance.itemType = 213;
				}
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(14))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
