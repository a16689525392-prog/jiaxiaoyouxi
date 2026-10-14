using Car;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class TiShiView : ViewBase
	{
		public Text contentText;

		public bool isConnect;

		public override void Start()
		{
			base.Start();
			contentText.text = MyDiagnosis.Instance.GetErrorContent() + ",请退出软件重新进入或联系驾驶模拟器生产厂家";
		}

		private void Update()
		{
			if (SignalSourceCtl.Instance.connectStatus == 1 && !isConnect)
			{
				isConnect = true;
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
