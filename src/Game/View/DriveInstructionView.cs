using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class DriveInstructionView : MonoBehaviour
	{
		public static DriveInstructionView Instance;

		public Text itemNameText;

		public Text instructionText;

		public Text tiShiText;

		public Image bg;

		public Image kuangImg;

		public Image bookImage;

		public Text daoJiShiText;

		public float timeClose;

		private void Awake()
		{
			Instance = this;
		}

		public void Update()
		{
			timeClose += Time.deltaTime;
			if (SceneJumpCtl.Instance.CurSceneType == SceneType.MainScene)
			{
				base.transform.GetChild(0).gameObject.SetActive(value: false);
			}
			else
			{
				base.transform.GetChild(0).gameObject.SetActive(value: true);
			}
			if (timeClose >= 6f)
			{
				bg.gameObject.SetActive(value: false);
				kuangImg.gameObject.SetActive(value: false);
				bookImage.gameObject.SetActive(value: false);
				itemNameText.text = "";
				tiShiText.text = "";
				instructionText.text = "";
				timeClose = 0f;
			}
		}

		private void GetItemInstructionText()
		{
			if (ViewJumpCtl.Instance.itemType == 401)
			{
				itemNameText.text = "乡村公路";
				instructionText.text = "在乡村公路中，当看到路口有“禁止直行标识”时，不能违禁直行；当看到路口有“直行标识”时，不能违禁左转弯或违禁右转弯；当看到前方有铁道路口时，不能违禁驶入。";
			}
			else if (ViewJumpCtl.Instance.itemType == 402)
			{
				itemNameText.text = "山区道路";
				instructionText.text = "在山区道路中，当看到路口有“禁行标识”时，可在其他道路上行驶，不能违反禁行规定通行。";
			}
			else if (ViewJumpCtl.Instance.itemType == 400)
			{
				itemNameText.text = "海景公路";
				instructionText.text = "在海景公路中，当看到路口有“直行标识”时只能直行，不能违禁左转弯或违禁右转弯。";
			}
			else if (ViewJumpCtl.Instance.itemType == 403)
			{
				itemNameText.text = "城市道路";
				instructionText.text = "在城市道路中，根据提示进行起步操作，抓好时机汇入车流。";
			}
			else if (ViewJumpCtl.Instance.itemType == 404)
			{
				itemNameText.text = "高速公路";
				instructionText.text = "在高速公路收费站入口前，要减速行驶，要与前车保持安全车距，不得超越前方车辆。";
			}
			else if (ViewJumpCtl.Instance.itemType == 405)
			{
				itemNameText.text = "深圳实景";
				instructionText.text = "在深圳实景道路中，当看到路口有“禁鸣喇叭标识”时，不能违禁鸣喇叭。在行驶过程中不能占用应急车道，不能逆行。";
			}
			else if (ViewJumpCtl.Instance.itemType == 51200)
			{
				itemNameText.text = "雨天驾驶";
				instructionText.text = "雨天驾驶时，界面中有可调节天气强度的按钮（强中弱），雨量、雷雨声大小、场景模糊程度随着天气强度等级的升高而增强，在雨天环境下要开启雨刮器，以保持车辆前方玻璃清晰、不遮挡视线；驾驶过程中保持车速低于30km/h。";
			}
			else if (ViewJumpCtl.Instance.itemType == 51201)
			{
				itemNameText.text = "雾天驾驶";
				instructionText.text = "雾天驾驶时，界面中有可调节天气强度的按钮（强中弱），雾天能见度随着天气强度等级的升高而降低，在雾天环境下要开启前照灯和危险警报闪光灯，以保持视野清晰、提醒其他车辆保持安全车距；驾驶过程中保持车速在30km/h以下。";
			}
			else if (ViewJumpCtl.Instance.itemType == 51202)
			{
				itemNameText.text = "雪天驾驶";
				instructionText.text = "雪天驾驶时，界面中有可调节天气强度的按钮（强中弱），雪天的雪量、风声大小、场景模糊程度随着天气强度等级的升高而增强，在雪天环境下要使用低速档（一档）行驶，不急刹车，不急加速，以保持车辆平稳行驶。";
			}
			else if (ViewJumpCtl.Instance.itemType == 503)
			{
				itemNameText.text = "泥泞路驾驶";
				instructionText.text = "泥泞路段驾驶时，使用低速档（一档）行驶，不急刹车，不急加速，以保持车辆不打滑，平稳通过。";
			}
			else if (ViewJumpCtl.Instance.itemType == 510)
			{
				itemNameText.text = "山路跟车/超车";
				instructionText.text = "山路跟车/超车正确操作：跟车时，车辆应与前车保持安全车距；超车时，应该在直线路段完成超车。";
			}
			else if (ViewJumpCtl.Instance.itemType == 5100)
			{
				itemNameText.text = "山路会车";
				instructionText.text = "山路会车正确操作：会车时要保持安全车距，要让不靠山体的一方先行，直线路段会车时，要让上坡车辆先行。";
			}
			else if (ViewJumpCtl.Instance.itemType == 509)
			{
				itemNameText.text = "山路坡道";
				instructionText.text = "山路坡道正确操作：上坡时应踩油门加速上坡，快要驶抵坡顶时要松开油门，下坡时要减速行驶，将车速控制在30km/小时以下。";
			}
			else if (ViewJumpCtl.Instance.itemType == 5090)
			{
				itemNameText.text = "山路弯道";
				instructionText.text = "山路弯道正确操作：山路通过连续急弯时，在进入弯道前要鸣喇叭提醒其他车辆，同时降低车速至30km/小时以下，弯道内不能触压道路中心线、不能占用对方车道。";
			}
			else if (ViewJumpCtl.Instance.itemType == 5050)
			{
				itemNameText.text = "夜间驾驶";
				instructionText.text = "夜间驾驶正确操作：夜间驾驶是训练驾驶员在夜间环境下的驾驶技能，在夜间驾驶时需要开启前照灯，以保持安全行驶。";
			}
			else if (ViewJumpCtl.Instance.itemType == 511)
			{
				itemNameText.text = "城市红绿灯";
				instructionText.text = "通过城市道路的红绿灯路口时，需要遵守“红灯停、绿灯行、黄灯等”的规则，红灯亮时，车辆应该在斑马线前停车，待绿灯亮，方可通过路口。";
			}
			else if (ViewJumpCtl.Instance.itemType == 5110)
			{
				itemNameText.text = "城市让超车";
				instructionText.text = "当发现后方有车辆将要超车时，要进行让超车，将车速控制在30km/小时以下行驶，不能变道，不能占用应急车道。";
			}
			else if (ViewJumpCtl.Instance.itemType == 5111)
			{
				itemNameText.text = "城市跟车";
				instructionText.text = "跟车行驶时，要与前车保持安全车距，不能在城市道路跟车时违法超车，不能占用应急车道。";
			}
			else if (ViewJumpCtl.Instance.itemType == 506)
			{
				itemNameText.text = "高速公路隧道";
				instructionText.text = "（1）进入隧道前要鸣喇叭提醒其他车辆，开启前照灯，并降低车速至35km/小时以下；（2）在隧道中行驶时，要按规定车道（右数第二条）行驶，不能变道，不能占用应急车道，车速保持在35km / 小时以下；（3）在隧道中遇到其他车辆时，不能超车，同时要保持安全车距；（4）驶抵隧道出口时，也要鸣喇叭提醒其他车辆，同时关闭前照灯。";
			}
			else if (ViewJumpCtl.Instance.itemType == 507)
			{
				itemNameText.text = "高速公路停车";
				instructionText.text = "根据语音引导从匝道口进入高速公路，在高速公路上行驶时，不能占用道路最右侧的应急车道；当车辆遇到故障时，需将车辆停靠在规定的安全停车区，停车后，拉起手刹，开启危险警示灯，下车检查车辆故障。";
			}
		}

		public void GetTextInfo()
		{
			bg.gameObject.SetActive(value: true);
			kuangImg.gameObject.SetActive(value: true);
			bookImage.gameObject.SetActive(value: true);
			itemNameText.text = "驾驶说明书";
			tiShiText.text = "提示：进行踩离合、脚刹、油门等操作将隐藏驾驶说明书";
			switch (ViewJumpCtl.Instance.itemType)
			{
			case 401:
				instructionText.text = "在乡村公路中，当看到路口有“禁止直行标识”时，不能违禁直行；当看到路口有“直行标识”时，不能违禁左转弯或违禁右转弯；当看到前方有铁道路口时，不能违禁驶入。";
				return;
			case 402:
				instructionText.text = "在山区道路中，当看到路口有“禁行标识”时，可在其他道路上行驶，不能违反禁行规定通行。";
				return;
			case 400:
				instructionText.text = "在海景公路中，当看到路口有“直行标识”时只能直行，不能违禁左转弯或违禁右转弯。";
				return;
			case 403:
				instructionText.text = "在城市道路中，根据提示进行起步操作，抓好时机汇入车流。";
				return;
			case 404:
				instructionText.text = "在高速公路收费站入口前，要减速行驶，要与前车保持安全车距，不得超越前方车辆。";
				return;
			case 405:
				instructionText.text = "在深圳实景道路中，当看到路口有“禁鸣喇叭标识”时，不能违禁鸣喇叭。在行驶过程中不能占用应急车道，不能逆行。";
				return;
			case 407:
				instructionText.text = "台州长潭水库路段复杂，适当降低车速，小心驾驶。";
				return;
			case 51200:
				instructionText.text = "在雨天环境下要开启雨刮器，以保持车辆前方玻璃清晰、不遮挡视线；驾驶过程中保持车速低于30km/h。";
				return;
			case 51201:
				instructionText.text = "在雾天环境下要开启雾灯、近光灯、示宽灯和危险警报闪光灯，以保持视野清晰、提醒其他车辆保持安全车距；驾驶过程中保持车速在30km/h以下。";
				return;
			case 51202:
				instructionText.text = "在雪天环境下要使用低速档（一档）行驶，要开启雾灯、近光灯和示宽灯，不急刹车，不急加速，以保持车辆平稳行驶。";
				return;
			case 503:
				instructionText.text = "泥泞路段驾驶时，使用低速档（一档）行驶，不急刹车，不急加速，以保持车辆不打滑，平稳通过。";
				return;
			case 510:
				instructionText.text = "山路跟车/超车正确操作：跟车时，车辆应与前车保持安全车距；超车时，应该在直线路段完成超车。";
				return;
			case 5100:
				instructionText.text = "山路会车正确操作：会车时要保持安全车距，要让不靠山体的一方先行，直线路段会车时，要让上坡车辆先行。";
				return;
			case 509:
				instructionText.text = "山路坡道正确操作：上坡时应踩油门加速上坡，快要驶抵坡顶时要松开油门，下坡时要减速行驶，将车速控制在30km/小时以下。";
				return;
			case 5090:
				instructionText.text = "山路弯道正确操作：山路通过连续急弯时，在进入弯道前要鸣喇叭提醒其他车辆，同时降低车速至30km/小时以下，弯道内不能触压道路中心线、不能占用对方车道。";
				return;
			case 5050:
				instructionText.text = "夜间驾驶正确操作：夜间驾驶是训练驾驶员在夜间环境下的驾驶技能，在夜间驾驶时需要开启前照灯，以保持安全行驶。";
				return;
			case 511:
				instructionText.text = "通过城市道路的红绿灯路口时，需要遵守“红灯停、绿灯行、黄灯等”的规则，红灯亮时，车辆应该在斑马线前停车，待绿灯亮，方可通过路口。";
				return;
			case 5110:
				instructionText.text = "当发现后方有车辆将要超车时，要进行让超车，将车速控制在30km/小时以下行驶，不能变道，不能占用应急车道。";
				return;
			case 5111:
				instructionText.text = "跟车行驶时，要与前车保持安全车距，不能在城市道路跟车时违法超车，不能占用应急车道。";
				return;
			case 506:
				instructionText.text = "（1）进入隧道前要开启前照灯，驶抵隧道出口时要关闭前照灯，并降低车速至80km/小时以下；（2）高速公路上要挂三档行驶，保持车速高于60km/小时，在隧道中行驶时车速要低于80km/小时；（3）在隧道中行驶时，要按规定车道（右数第二条）行驶，不能变道，不能占用应急车道；（4）在隧道中遇到其他车辆时，不能超车，同时要保持安全车距；";
				return;
			case 507:
				instructionText.text = "根据语音引导从匝道口进入高速公路，在高速公路上行驶时，不能占用道路最右侧的应急车道；当车辆遇到故障时，需将车辆停靠在规定的安全停车区，停车后，拉起手刹，开启危险警示灯，下车检查车辆故障。";
				return;
			}
			itemNameText.text = "";
			tiShiText.text = "";
			instructionText.text = "";
		}

		public void Exit()
		{
			if (bg != null)
			{
				bg.gameObject.SetActive(value: false);
				kuangImg.gameObject.SetActive(value: false);
				bookImage.gameObject.SetActive(value: false);
				itemNameText.text = "";
				tiShiText.text = "";
				instructionText.text = "";
			}
		}
	}
}
