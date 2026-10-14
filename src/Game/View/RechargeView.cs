using Global;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class RechargeView : ViewBase
	{
		public RawImage RechargeCode;

		public override void Start()
		{
			base.Start();
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				NetworkManagement.Instance.GetRechargeCode(DataCenter.Instance.SerialNumber);
				if (NetworkManagement.Instance.urlRechargeCode != "")
				{
					RechargeCode.texture = NetworkManagement.Instance.Base64StringToTexture(NetworkManagement.Instance.urlRechargeCode);
				}
				else
				{
					ViewJumpCtl.Instance.ShowMsgTips("获取充值二维码失败!");
				}
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
