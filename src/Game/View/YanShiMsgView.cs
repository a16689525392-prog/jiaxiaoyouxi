using Car;
using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class YanShiMsgView : ViewBase
	{
		public Image mask;

		public Text contentText;

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
				TrainSceneBase.BaseInstance.ResetAction();
				if (ViewJumpCtl.Instance.itemType == 601 || ViewJumpCtl.Instance.itemType == 602 || ViewJumpCtl.Instance.itemType == 603 || ViewJumpCtl.Instance.itemType == 611)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.YingjiXunlian);
				}
				else if (ViewJumpCtl.Instance.itemType == 604 || ViewJumpCtl.Instance.itemType == 605 || ViewJumpCtl.Instance.itemType == 606 || ViewJumpCtl.Instance.itemType == 607 || ViewJumpCtl.Instance.itemType == 609 || ViewJumpCtl.Instance.itemType == 612)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ShanQu);
				}
				else if (ViewJumpCtl.Instance.itemType == 608)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.HighRoad);
				}
				else if (ViewJumpCtl.Instance.itemType == 610 || ViewJumpCtl.Instance.itemType == 613 || ViewJumpCtl.Instance.itemType == 614 || ViewJumpCtl.Instance.itemType == 615)
				{
					SceneJumpCtl.Instance.EnterScene(SceneType.ChengDong);
				}
			}
			else if (go == GetBtn(1))
			{
				SceneJumpCtl.Instance.EnterScene(SceneType.MainScene);
				CarState.Instance.FireOn = false;
				MoNiCarDynamic.Instance.dynamicData.engineSpeed = 0f;
				StopAllCoroutines();
			}
		}
	}
}
