using Scene;
using UnityEngine;

namespace View
{
	public class CoursetwoScenesChooseOneView : ViewBase
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
				SceneJumpCtl.Instance.CoursetwoName = CoursetwoScenesName.YiAnDa;
				ViewJumpCtl.Instance.EnterView(ViewType.CourseTwoView);
			}
			else if (go == GetBtn(1))
			{
				JudgeIsEnter(CoursetwoScenesName.QingHu);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			ViewJumpCtl.Instance.Enter = false;
		}

		private void JudgeIsEnter(CoursetwoScenesName Scene)
		{
			if (NetworkManagement.Instance.IsYouKeModel)
			{
				ViewJumpCtl.Instance.ShowMsgTips("请先登录!");
			}
			else if (NetworkManagement.Instance.GetUserData() > 0f)
			{
				SceneJumpCtl.Instance.CoursetwoName = Scene;
				ViewJumpCtl.Instance.EnterView(ViewType.CourseTwoView);
			}
			else
			{
				ViewJumpCtl.Instance.ShowMsgTips("没有训练学时,请前往充值!");
			}
		}
	}
}
