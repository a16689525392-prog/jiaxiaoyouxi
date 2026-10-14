using Global;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Scene
{
	public class TiShiText : MonoBehaviour
	{
		public Text tiShiText;

		private void Start()
		{
			if (ViewJumpCtl.Instance.itemType == 601)
			{
				tiShiText.text = "遇行人时减速停车";
			}
			if (ViewJumpCtl.Instance.itemType == 602)
			{
				tiShiText.text = "握紧方向盘,轻踩脚刹缓慢降低车速至10公里/时以下,待车速降低后靠边停车,拉紧手刹,并开启双闪灯";
			}
			if (ViewJumpCtl.Instance.itemType == 603)
			{
				tiShiText.text = "酒驾或毒驾禁止启动车辆";
			}
			if (ViewJumpCtl.Instance.itemType == 604)
			{
				tiShiText.text = "发现车辆着火后,立刻停车,离开车辆";
			}
			if (ViewJumpCtl.Instance.itemType == 605)
			{
				tiShiText.text = "鸣喇叭,开启双闪灯,通过轻踩脚刹降低车速,车速降低后停车";
			}
			if (ViewJumpCtl.Instance.itemType == 606)
			{
				tiShiText.text = "轻踩脚刹控制车速,让车辆平稳通过";
			}
			if (ViewJumpCtl.Instance.itemType == 607)
			{
				tiShiText.text = "减速,靠边停车";
			}
			if (ViewJumpCtl.Instance.itemType == 608)
			{
				tiShiText.text = "距离前车100米停车,并开启危险警示灯提醒其他车辆";
			}
			if (ViewJumpCtl.Instance.itemType == 609)
			{
				tiShiText.text = "减速行驶,并在发生落石处前方50米外停车";
			}
			if (ViewJumpCtl.Instance.itemType == 611)
			{
				tiShiText.text = "鸣喇叭,开启双闪灯,逐级降档至一档,车速低于10公里/时后拉手刹停车";
			}
			if (ViewJumpCtl.Instance.itemType == 612)
			{
				tiShiText.text = "减速行驶,并在发生滑坡处前方50米外停车";
			}
			if (ViewJumpCtl.Instance.itemType == 403)
			{
				if (SettingInfoCtl.Instance.isManualGear)
				{
					tiShiText.GetComponent<Text>().text = "起步操作：离合器、脚刹踩到底，挂空挡，点火，挂一挡，松脚刹，缓慢松离合器";
				}
				else if (!SettingInfoCtl.Instance.isManualGear)
				{
					tiShiText.GetComponent<Text>().text = "起步操作：脚刹踩到底，挂N挡，点火，松手刹，挂D挡，缓慢松脚刹";
				}
			}
			if (ViewJumpCtl.Instance.itemType == 610)
			{
				tiShiText.text = "不要急踩刹车，开启双闪灯，慢慢靠边停车";
			}
			if (ViewJumpCtl.Instance.itemType == 613)
			{
				tiShiText.text = "通过公交盲区时，要提前减速行驶，预防突然从盲区出现的人和车";
			}
			if (ViewJumpCtl.Instance.itemType == 614)
			{
				tiShiText.text = "遇到非机动车抢道时，要提前减速，保持安全距离，让非机动车先行";
			}
			if (ViewJumpCtl.Instance.itemType == 615)
			{
				tiShiText.text = "场景中会遇到多种突发情况，保持冷静，谨慎驾驶";
			}
		}
	}
}
