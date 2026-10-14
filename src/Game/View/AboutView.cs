using Global;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class AboutView : ViewBase
	{
		public Text softVersionText;

		public Text serialNumberText;

		public static string Number;

		public override void Start()
		{
			base.Start();
			softVersionText.text = DataCenter.Instance.SoftVersion;
			serialNumberText.text = DataCenter.Instance.GetUsbVersionName() + Number + "\n" + DataCenter.Instance.SerialNumber;
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}

		public void Update()
		{
			softVersionText.text = DataCenter.Instance.SoftVersion;
			serialNumberText.text = DataCenter.Instance.GetUsbVersionName() + Number + "\n" + DataCenter.Instance.SerialNumber;
		}
	}
}
