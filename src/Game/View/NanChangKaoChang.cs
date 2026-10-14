using UnityEngine;

namespace View
{
	public class NanChangKaoChang : ViewBase
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
				ViewJumpCtl.Instance.EnterView(ViewType.YADCourseTwoChooseView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
