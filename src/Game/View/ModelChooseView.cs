using Car;
using Global;
using Scene;
using UnityEngine;

namespace View
{
	public class ModelChooseView : ViewBase
	{
		public override void Start()
		{
			base.Start();
			if (SettingInfoCtl.Instance.carType != CarType.JieDaAuto && SettingInfoCtl.Instance.carType != CarType.JieDaManual && SettingInfoCtl.Instance.carType != CarType.AiLiSheManual && SettingInfoCtl.Instance.carType != CarType.AiLiSheAuto)
			{
				GetBtn(1).gameObject.SetActive(value: false);
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0) && go.activeInHierarchy)
			{
				ViewJumpCtl.Instance.modelType = 0;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1) && go.activeInHierarchy)
			{
				ViewJumpCtl.Instance.modelType = 1;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2) && go.activeInHierarchy)
			{
				ViewJumpCtl.Instance.modelType = 2;
				SendBtnLevel(SceneJumpCtl.Instance.CoursetwoName);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}

		private void SendBtnLevel(CoursetwoScenesName CoursetwoName)
		{
			switch (CoursetwoName)
			{
			case CoursetwoScenesName.YiAnDa:
				NetworkManagement.Instance.BtnLevel = "A" + GetLevel(1);
				break;
			case CoursetwoScenesName.QingHu:
				NetworkManagement.Instance.BtnLevel = "A" + GetLevel(7);
				break;
			case CoursetwoScenesName.WenZhou:
				NetworkManagement.Instance.BtnLevel = "A" + GetLevel(13);
				break;
			case CoursetwoScenesName.HuNanYZ:
				NetworkManagement.Instance.BtnLevel = "A" + GetLevel(19);
				break;
			case CoursetwoScenesName.ShiJiaZhuang:
				NetworkManagement.Instance.BtnLevel = "A" + GetLevel(25);
				break;
			case CoursetwoScenesName.FoShan:
				NetworkManagement.Instance.BtnLevel = "A" + GetLevel(31);
				break;
			case CoursetwoScenesName.HeBei:
				NetworkManagement.Instance.BtnLevel = "A" + GetLevel(37);
				break;
			}
			NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
		}

		private int GetLevel(int i)
		{
			switch (ViewJumpCtl.Instance.itemType)
			{
			case 1:
				i++;
				break;
			case 3:
				i += 2;
				break;
			case 4:
				i += 3;
				break;
			case 2:
				i += 4;
				break;
			}
			return i;
		}
	}
}
