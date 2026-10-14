using UnityEngine;

namespace View
{
	public class CourseThreeModelChooseView : ViewBase
	{
		public override void Start()
		{
			base.Start();
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.modelType = 1;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.modelType = 2;
				ViewJumpCtl.Instance.EnterScene();
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
