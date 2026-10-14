using Global;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class LoginView : ViewBase
	{
		public Text serialNumberText;

		public RawImage QRCode;

		public Text NetWorkErrorText;

		public Text RequestErrorText;

		private float PollingLoginStateTime;

		public override void Start()
		{
			base.Start();
			PollingLoginStateTime = 0f;
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				NetworkManagement.Instance.IsYouKeModel = true;
				ViewJumpCtl.Instance.EnterView(ViewType.MainView);
			}
			else if (go == GetBtn(1))
			{
				Application.Quit();
			}
		}

		private void Update()
		{
			serialNumberText.text = DataCenter.Instance.SerialNumber;
			if (NetworkManagement.Instance.urlQRCode == "")
			{
				NetworkManagement.Instance.GetQRCode("1", DataCenter.Instance.SerialNumber);
				QRCode.texture = NetworkManagement.Instance.Base64StringToTexture(NetworkManagement.Instance.urlQRCode);
			}
			if (NetworkManagement.Instance.StrGetQRCode != "")
			{
				RequestErrorText.gameObject.SetActive(value: true);
				RequestErrorText.text = NetworkManagement.Instance.StrGetQRCode;
			}
			else
			{
				RequestErrorText.gameObject.SetActive(value: false);
			}
			if (!NetworkManagement.Instance.islogin)
			{
				PollingLoginStateTime += Time.deltaTime;
				if (PollingLoginStateTime > 0.25f)
				{
					NetworkManagement.Instance.GetToken();
					PollingLoginStateTime = 0f;
				}
			}
			else
			{
				ViewJumpCtl.Instance.EnterView(ViewType.MainView);
			}
			if (NetworkManagement.Instance.IsNetWork)
			{
				NetWorkErrorText.gameObject.SetActive(value: false);
			}
			else
			{
				NetWorkErrorText.gameObject.SetActive(value: true);
			}
		}
	}
}
