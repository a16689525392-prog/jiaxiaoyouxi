using UnityEngine;

namespace View
{
	public class HuNanYZCourseThreeChooseView : ViewBase
	{
		public static int curClickBtnIndex;

		public override void Start()
		{
			base.Start();
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.trainType = TrainType.NightLight;
				ViewJumpCtl.Instance.itemType = 102;
				NetworkManagement.Instance.BtnLevel = "B16";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 200;
				NetworkManagement.Instance.BtnLevel = "B17";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 208;
				NetworkManagement.Instance.BtnLevel = "B18";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 204;
				NetworkManagement.Instance.BtnLevel = "B19";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 211;
				NetworkManagement.Instance.BtnLevel = "B20";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 203;
				NetworkManagement.Instance.BtnLevel = "B21";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(6))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 209;
				NetworkManagement.Instance.BtnLevel = "B22";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(7))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 214;
				NetworkManagement.Instance.BtnLevel = "B23";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(8))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 201;
				NetworkManagement.Instance.BtnLevel = "B24";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(9))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 202;
				NetworkManagement.Instance.BtnLevel = "B25";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(10))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 210;
				NetworkManagement.Instance.BtnLevel = "B26";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(11))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 207;
				NetworkManagement.Instance.BtnLevel = "B27";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(12))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 206;
				NetworkManagement.Instance.BtnLevel = "B28";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(13))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 205;
				NetworkManagement.Instance.BtnLevel = "B29";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(14))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 212;
				NetworkManagement.Instance.BtnLevel = "B30";
				ViewJumpCtl.Instance.EnterView(ViewType.CourseThreeModelChooseView);
			}
			else if (go == GetBtn(15))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseThree;
				ViewJumpCtl.Instance.itemType = 1300;
				ViewJumpCtl.Instance.modelType = 2;
				NetworkManagement.Instance.BtnLevel = "B31";
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(16))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
