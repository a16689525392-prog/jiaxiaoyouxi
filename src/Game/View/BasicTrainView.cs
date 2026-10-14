using Global;
using UnityEngine;

namespace View
{
	public class BasicTrainView : ViewBase
	{
		public override void Start()
		{
			base.Start();
			ViewJumpCtl.Instance.trainType = TrainType.BasicTrain;
			if (!SettingInfoCtl.Instance.isManualGear)
			{
				GetBtn(1).SetActive(value: false);
				GetBtn(2).SetActive(value: false);
				GetBtn(3).SetActive(value: false);
			}
			if (DataCenter.Instance.UsbVersion == 0)
			{
				GetBtn(6).SetActive(value: false);
				GetBtn(7).SetActive(value: false);
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.itemType = 300;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.itemType = 302;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.itemType = 301;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.itemType = 303;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.itemType = 307;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.itemType = 308;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(6))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseOne;
				ViewJumpCtl.Instance.EnterView(ViewType.LiLunModelChooseView);
			}
			else if (go == GetBtn(7))
			{
				ViewJumpCtl.Instance.trainType = TrainType.CourseFour;
				ViewJumpCtl.Instance.EnterView(ViewType.LiLunModelChooseView);
			}
			else if (go == GetBtn(8))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
