using Car;
using Global;
using Scene;
using UnityEngine;

namespace View
{
	public class YingJiModelChooseView : ViewBase
	{
		private bool btn;

		public override void Start()
		{
			if (ViewJumpCtl.Instance.itemType == 603 || ViewJumpCtl.Instance.itemType == 607 || ViewJumpCtl.Instance.itemType == 615)
			{
				GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
			}
			if (SettingInfoCtl.Instance.carType == CarType.JieDaAuto || SettingInfoCtl.Instance.carType == CarType.JieDaManual)
			{
				btn = true;
				return;
			}
			btnList[0].transform.gameObject.SetActive(value: false);
			btnList[1].transform.gameObject.SetActive(value: false);
			btn = false;
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0) && btn)
			{
				SettingInfoCtl.Instance.isCorrect = true;
				ViewJumpCtl.Instance.modelType = 0;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(1) && btn)
			{
				SettingInfoCtl.Instance.isCorrect = false;
				ViewJumpCtl.Instance.modelType = 0;
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.modelType = 2;
				NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
