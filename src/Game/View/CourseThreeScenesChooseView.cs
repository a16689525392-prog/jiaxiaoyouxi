using Global;
using Scene;
using UnityEngine;

namespace View
{
	public class CourseThreeScenesChooseView : ViewBase
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
				SceneJumpCtl.Instance.CoursethreeName = CoursethreeScenesName.NanChang;
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeChooseView);
				StartCoroutine(new CourseThreeItemPositionCtl().ReadCourseThreeItemPostion(DataCenter.Instance.itemPositionList));
			}
			else if (go == GetBtn(1))
			{
				SceneJumpCtl.Instance.CoursethreeName = CoursethreeScenesName.RenCai;
				SceneJumpCtl.Instance.SceneWayNumber = 3;
				SceneJumpCtl.Instance.SceneWay = 1;
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeChooseView);
				StartCoroutine(new CourseThreeItemPositionCtl().ReadCourseThreeItemPostion(DataCenter.Instance.itemPositionList));
			}
			else if (go == GetBtn(2))
			{
				SceneJumpCtl.Instance.CoursethreeName = CoursethreeScenesName.ChengDong;
				SceneJumpCtl.Instance.SceneWayNumber = 3;
				SceneJumpCtl.Instance.SceneWay = 1;
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeChooseView);
				StartCoroutine(new CourseThreeItemPositionCtl().ReadCourseThreeItemPostion(DataCenter.Instance.itemPositionList));
			}
			else if (go == GetBtn(3))
			{
				SceneJumpCtl.Instance.CoursethreeName = CoursethreeScenesName.BaiYun;
				SceneJumpCtl.Instance.SceneWayNumber = 3;
				SceneJumpCtl.Instance.SceneWay = 1;
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeChooseView);
				StartCoroutine(new CourseThreeItemPositionCtl().ReadCourseThreeItemPostion(DataCenter.Instance.itemPositionList));
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			ViewJumpCtl.Instance.Enter = false;
		}

		private void JudgeIsEnter(CoursethreeScenesName Scene)
		{
			if (NetworkManagement.Instance.IsYouKeModel)
			{
				ViewJumpCtl.Instance.ShowMsgTips("请先登录!");
			}
			else if (NetworkManagement.Instance.GetUserData() > 0f)
			{
				SceneJumpCtl.Instance.CoursethreeName = Scene;
				CoursethreeScenesName coursethreeName = SceneJumpCtl.Instance.CoursethreeName;
				if (coursethreeName == CoursethreeScenesName.HuNanYZ)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.HuNanYZCourseThreeChooseView);
				}
			}
			else
			{
				ViewJumpCtl.Instance.ShowMsgTips("没有训练学时,请前往充值!");
				ViewJumpCtl.Instance.EnterView(ViewType.RechargeView);
			}
		}
	}
}
