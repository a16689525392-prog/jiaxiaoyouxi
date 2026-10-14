using Scene;
using UnityEngine;

namespace View
{
	public class WayChooseView : ViewBase
	{
		public override void Start()
		{
			base.Start();
			for (int i = 0; i < 10; i++)
			{
				if (i >= SceneJumpCtl.Instance.SceneWayNumber)
				{
					GetBtn(i).SetActive(value: false);
				}
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				SceneJumpCtl.Instance.SceneWay = 1;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				SceneJumpCtl.Instance.SceneWay = 2;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				SceneJumpCtl.Instance.SceneWay = 3;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				SceneJumpCtl.Instance.SceneWay = 4;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(4))
			{
				SceneJumpCtl.Instance.SceneWay = 5;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(5))
			{
				SceneJumpCtl.Instance.SceneWay = 6;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(6))
			{
				SceneJumpCtl.Instance.SceneWay = 7;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(7))
			{
				SceneJumpCtl.Instance.SceneWay = 8;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(8))
			{
				SceneJumpCtl.Instance.SceneWay = 9;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(9))
			{
				SceneJumpCtl.Instance.SceneWay = 10;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(10))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
