using SoundAndTextShow;
using UnityEngine;

namespace View
{
	public class TeachingVedioChooseView : ViewBase
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
				SceneTextShow.VideoNumber = 1;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(1))
			{
				SceneTextShow.VideoNumber = 2;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(2))
			{
				SceneTextShow.VideoNumber = 3;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(3))
			{
				SceneTextShow.VideoNumber = 4;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(4))
			{
				SceneTextShow.VideoNumber = 5;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(5))
			{
				SceneTextShow.VideoNumber = 6;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(6))
			{
				SceneTextShow.VideoNumber = 7;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(7))
			{
				SceneTextShow.VideoNumber = 8;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(8))
			{
				SceneTextShow.VideoNumber = 9;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(9))
			{
				SceneTextShow.VideoNumber = 10;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(10))
			{
				SceneTextShow.VideoNumber = 11;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(11))
			{
				SceneTextShow.VideoNumber = 12;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(12))
			{
				SceneTextShow.VideoNumber = 13;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(13))
			{
				SceneTextShow.VideoNumber = 14;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(14))
			{
				SceneTextShow.VideoNumber = 15;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(15))
			{
				SceneTextShow.VideoNumber = 16;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(16))
			{
				SceneTextShow.VideoNumber = 17;
				ViewJumpCtl.Instance.EnterView(ViewType.SecurityCheckView);
			}
			else if (go == GetBtn(17))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
