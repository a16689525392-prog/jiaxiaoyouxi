using Scene;
using UnityEngine;

namespace View
{
	public class CourseThreeChooseView : ViewBase
	{
		public static int curClickBtnIndex;

		public override void Start()
		{
			base.Start();
			GetBtn(2).SetActive(value: false);
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.trainType = TrainType.NightLight;
				ViewJumpCtl.Instance.itemType = 101;
				NetworkManagement.Instance.BtnLevel = "B1";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 1300;
				NetworkManagement.Instance.BtnLevel = "B15";
				ViewJumpCtl.Instance.modelType = 0;
				if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
				{
					ViewJumpCtl.Instance.EnterScene();
				}
				else
				{
					ViewJumpCtl.Instance.EnterView(ViewType.WayChooseView);
				}
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 1300;
				NetworkManagement.Instance.BtnLevel = "B15";
				ViewJumpCtl.Instance.modelType = 1;
				ViewJumpCtl.Instance.EnterView(ViewType.WayChooseView);
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 1300;
				NetworkManagement.Instance.BtnLevel = "B15";
				ViewJumpCtl.Instance.modelType = 2;
				if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
				{
					ViewJumpCtl.Instance.EnterScene();
				}
				else
				{
					ViewJumpCtl.Instance.EnterView(ViewType.WayChooseView);
				}
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
