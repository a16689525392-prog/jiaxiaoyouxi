using UnityEngine;

namespace View
{
	public class LightExamChooseView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.itemType = 100;
				ViewJumpCtl.Instance.modelType = 1;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.itemType = 100;
				ViewJumpCtl.Instance.modelType = 2;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.itemType = 101;
				ViewJumpCtl.Instance.modelType = 1;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.itemType = 101;
				ViewJumpCtl.Instance.modelType = 2;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
