using UnityEngine;

namespace View
{
	public class DriveSkillView : ViewBase
	{
		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				BadWeatherView.scene = "rain";
				ViewJumpCtl.Instance.otherType = 500;
				ViewJumpCtl.Instance.itemType = 51200;
				NetworkManagement.Instance.BtnLevel = "C1";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1))
			{
				BadWeatherView.scene = "fog";
				ViewJumpCtl.Instance.otherType = 501;
				ViewJumpCtl.Instance.itemType = 51201;
				NetworkManagement.Instance.BtnLevel = "C2";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				BadWeatherView.scene = "snow";
				ViewJumpCtl.Instance.otherType = 502;
				ViewJumpCtl.Instance.itemType = 51202;
				NetworkManagement.Instance.BtnLevel = "C3";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 503;
				NetworkManagement.Instance.BtnLevel = "C4";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.itemType = 5120;
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 510;
				NetworkManagement.Instance.BtnLevel = "C5";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.itemType = 5120;
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 5100;
				NetworkManagement.Instance.BtnLevel = "C6";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(6))
			{
				ViewJumpCtl.Instance.itemType = 5120;
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 509;
				NetworkManagement.Instance.BtnLevel = "C7";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(7))
			{
				ViewJumpCtl.Instance.itemType = 5120;
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 5090;
				NetworkManagement.Instance.BtnLevel = "C8";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(8))
			{
				ViewJumpCtl.Instance.otherType = 505;
				ViewJumpCtl.Instance.itemType = 5050;
				NetworkManagement.Instance.BtnLevel = "C9";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(9))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 511;
				NetworkManagement.Instance.BtnLevel = "C10";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(10))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 5110;
				NetworkManagement.Instance.BtnLevel = "C11";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(11))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 5111;
				NetworkManagement.Instance.BtnLevel = "C12";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(12))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 506;
				NetworkManagement.Instance.BtnLevel = "C13";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(13))
			{
				ViewJumpCtl.Instance.otherType = 504;
				ViewJumpCtl.Instance.itemType = 507;
				NetworkManagement.Instance.BtnLevel = "C14";
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(14))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
