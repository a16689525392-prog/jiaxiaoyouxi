using UnityEngine;

namespace View
{
	public class BadWeatherView : ViewBase
	{
		public static string scene;

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				scene = "rain";
				ViewJumpCtl.Instance.otherType = 500;
				ViewJumpCtl.Instance.itemType = 51200;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				scene = "fog";
				ViewJumpCtl.Instance.otherType = 501;
				ViewJumpCtl.Instance.itemType = 51201;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				scene = "snow";
				ViewJumpCtl.Instance.otherType = 502;
				ViewJumpCtl.Instance.itemType = 51202;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 503;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
