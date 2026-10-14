using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View
{
	public class MsgView : ViewBase
	{
		public Image mask;

		public Text contentText;

		private UnityAction action0;

		private UnityAction action1;

		public override void Start()
		{
			base.Start();
			mask.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.ReturnPreView();
				if (action0 != null)
				{
					action0();
				}
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.ReturnPreView();
				if (action1 != null)
				{
					action1();
				}
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.ReturnPreView();
				if (action0 != null)
				{
					action0();
				}
			}
		}

		public void InitMsgView(string content, string btnName0, UnityAction action0, string btnName1 = "", UnityAction action1 = null)
		{
			contentText.text = content;
			if (btnName1 == "")
			{
				GetBtn(2).GetComponent<NormalBtn>().btnText.text = btnName0;
				this.action0 = action0;
				GetBtn(0).SetActive(value: false);
				GetBtn(1).SetActive(value: false);
			}
			else
			{
				GetBtn(0).GetComponent<NormalBtn>().btnText.text = btnName0;
				GetBtn(1).GetComponent<NormalBtn>().btnText.text = btnName1;
				GetBtn(2).SetActive(value: false);
				this.action0 = action0;
				this.action1 = action1;
			}
		}
	}
}
