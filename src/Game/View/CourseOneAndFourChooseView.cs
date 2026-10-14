using UnityEngine;

namespace View
{
	public class CourseOneAndFourChooseView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseOne;
				ViewJumpCtl.Instance.EnterView(ViewType.LiLunModelChooseView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseFour;
				ViewJumpCtl.Instance.EnterView(ViewType.LiLunModelChooseView);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
