using System;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using View;

namespace Scene
{
	public class TurnScene : TrainSceneBase
	{
		private GameObject daSName;

		private GameObject xiaoSName;

		private GameObject zhiJiaoName;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
			if (ViewJumpCtl.Instance.itemType == 304)
			{
				xiaoSName = GameObject.Find("GameGlobalCtl(Clone)/Canvas/SceneText/xiaoSName");
				SoundPlayCtl.Instance.PlaySound("小S弯转向训练开始");
			}
			else if (ViewJumpCtl.Instance.itemType == 306)
			{
				daSName = GameObject.Find("GameGlobalCtl(Clone)/Canvas/SceneText/daSName");
				SoundPlayCtl.Instance.PlaySound("大S弯转向训练开始");
			}
			else if (ViewJumpCtl.Instance.itemType == 305)
			{
				zhiJiaoName = GameObject.Find("GameGlobalCtl(Clone)/Canvas/SceneText/zhiJiaoName");
				SoundPlayCtl.Instance.PlaySound("直角弯转向训练开始");
			}
			base.ResetAction = (UnityAction)Delegate.Combine(base.ResetAction, new UnityAction(TrainItemReset));
		}

		protected override void Update()
		{
			base.Update();
			if (ViewJumpCtl.Instance.itemType == 304)
			{
				xiaoSName.GetComponent<Text>().text = "小S弯转向训练";
			}
			else if (ViewJumpCtl.Instance.itemType == 306)
			{
				daSName.GetComponent<Text>().text = "大S弯转向训练";
			}
			else if (ViewJumpCtl.Instance.itemType == 305)
			{
				zhiJiaoName.GetComponent<Text>().text = "直角弯转向训练";
			}
		}

		private void TrainItemReset()
		{
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			if (ViewJumpCtl.Instance.itemType == 304)
			{
				xiaoSName.GetComponent<Text>().text = " ";
			}
			else if (ViewJumpCtl.Instance.itemType == 306)
			{
				daSName.GetComponent<Text>().text = " ";
			}
			else if (ViewJumpCtl.Instance.itemType == 305)
			{
				zhiJiaoName.GetComponent<Text>().text = " ";
			}
			SoundPlayCtl.Instance.ClearClip();
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainItemReset));
		}
	}
}
