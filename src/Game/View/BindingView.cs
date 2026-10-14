using Global;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class BindingView : ViewBase
	{
		public GameObject BiaKuangImage;

		public RawImage BindingCode;

		public Text StrBingding;

		private float Stime;

		public override void Start()
		{
			base.Start();
			if (DataCenter.Instance.UsbVersion == 0)
			{
				NetworkManagement.Instance.GetBindingCode(DataCenter.Instance.SerialNumber, "30", "2");
			}
			else
			{
				NetworkManagement.Instance.GetBindingCode(DataCenter.Instance.SerialNumber, "30", "1");
			}
			if (NetworkManagement.Instance.urlBindingCode != "")
			{
				BindingCode.texture = NetworkManagement.Instance.Base64StringToTexture(NetworkManagement.Instance.urlBindingCode);
			}
			else
			{
				ViewJumpCtl.Instance.ShowMsgTips("获取厂商绑定二维码失败!");
			}
		}

		private void Update()
		{
			Stime += Time.deltaTime;
			if (Stime > 0.5f && NetworkManagement.Instance.StrBinding == "")
			{
				if (DataCenter.Instance.UsbVersion == 0)
				{
					NetworkManagement.Instance.GetBindingCode(DataCenter.Instance.SerialNumber, "30", "2");
				}
				else
				{
					NetworkManagement.Instance.GetBindingCode(DataCenter.Instance.SerialNumber, "30", "1");
				}
				Stime = 0f;
			}
			if (NetworkManagement.Instance.StrBinding != "")
			{
				BiaKuangImage.SetActive(value: false);
				StrBingding.gameObject.SetActive(value: true);
				StrBingding.text = NetworkManagement.Instance.StrBinding;
			}
			else if (NetworkManagement.Instance.IsNetWork)
			{
				BiaKuangImage.SetActive(value: true);
				StrBingding.gameObject.SetActive(value: true);
			}
			else
			{
				BiaKuangImage.SetActive(value: false);
				StrBingding.gameObject.SetActive(value: true);
				StrBingding.text = "未连接网络!";
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.ReturnPreView();
				NetworkManagement.Instance.StrBinding = "";
			}
		}
	}
}
