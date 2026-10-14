using System;
using System.Collections.Generic;
using System.Xml;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class BasicTrainDriveGuide : MonoBehaviour
	{
		public static BasicTrainDriveGuide Instance;

		private GameObject guideTipsView;

		private List<GuideStepInfo> guideStepInfoList = new List<GuideStepInfo>();

		private int guideActionIndex;

		private bool oneStepJudgeEnd = true;

		private int stepIndex;

		private GameObject countDownPrefabs;

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			SoundPlayCtl.Instance.PlaySound(ViewJumpCtl.Instance.GetItemTypeName() + "训练开始");
			switch (ViewJumpCtl.Instance.itemType)
			{
			case 300:
				LoadGuideFile(DataCenter.Instance.GetFilePath(0) + "WheelTrainGuide");
				break;
			case 301:
				LoadGuideFile(DataCenter.Instance.GetFilePath(0) + "CluthTrainGuide");
				break;
			case 302:
				LoadGuideFile(DataCenter.Instance.GetFilePath(0) + "GearTrainGuide");
				break;
			case 303:
				LoadGuideFile(DataCenter.Instance.GetFilePath(0) + "StartTrainGuide");
				break;
			case 307:
				LoadGuideFile(DataCenter.Instance.GetFilePath(0) + "YuGuaQiGuide");
				break;
			case 308:
				LoadGuideFile(DataCenter.Instance.GetFilePath(0) + "AddSpeedGuide");
				break;
			}
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetAction = (UnityAction)Delegate.Combine(baseInstance.ResetAction, new UnityAction(Reset));
			guideTipsView = ViewJumpCtl.Instance.CreateView(ViewType.GuideTipsView);
			BasicGuide();
		}

		public void LoadGuideFile(string path)
		{
			string text = Resources.Load<TextAsset>(path).text;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(text);
			foreach (XmlElement childNode in xmlDocument.SelectSingleNode("guide").SelectSingleNode("guideOperate").ChildNodes)
			{
				GuideStepInfo guideStepInfo = new GuideStepInfo();
				foreach (XmlElement item in childNode)
				{
					switch (item.Name)
					{
					case "index":
						guideStepInfo.index = int.Parse(item.InnerText);
						break;
					case "titleText":
						guideStepInfo.titleText = item.InnerText;
						break;
					case "action":
					{
						ActionInfo actionInfo = new ActionInfo();
						foreach (XmlElement childNode2 in item.ChildNodes)
						{
							switch (childNode2.Name)
							{
							case "content":
								actionInfo.content = childNode2.InnerText;
								break;
							case "isAction":
								actionInfo.isAction = bool.Parse(childNode2.InnerText);
								break;
							case "duringTime":
								actionInfo.duringTime = float.Parse(childNode2.InnerText);
								break;
							}
						}
						guideStepInfo.actionList.Add(actionInfo);
						break;
					}
					}
				}
				guideStepInfoList.Add(guideStepInfo);
			}
		}

		private void BasicGuide()
		{
			if (guideTipsView == null)
			{
				guideTipsView = ViewJumpCtl.Instance.CreateView(ViewType.GuideTipsView);
			}
			if (stepIndex < guideStepInfoList.Count && oneStepJudgeEnd)
			{
				if (guideStepInfoList[stepIndex].titleText != "")
				{
					SoundPlayCtl.Instance.PlaySound(guideStepInfoList[stepIndex].titleText);
				}
				guideTipsView.GetComponent<GuideTipsViewCtl>().InitGuideTipsView(guideStepInfoList[stepIndex]);
				guideTipsView.GetComponent<GuideTipsViewCtl>().StartCurActionJudge();
				guideActionIndex = 0;
				oneStepJudgeEnd = false;
			}
			else if (stepIndex >= guideStepInfoList.Count)
			{
				switch (ViewJumpCtl.Instance.itemType)
				{
				case 300:
				case 302:
					CountDownReset(5);
					break;
				}
				if (guideTipsView != null)
				{
					UnityEngine.Object.Destroy(guideTipsView, 3f);
				}
				SoundPlayCtl.Instance.PlaySound(ViewJumpCtl.Instance.GetItemTypeName() + "训练结束");
			}
		}

		public void JudgeNextAction()
		{
			guideActionIndex++;
			if (guideActionIndex < guideStepInfoList[stepIndex].actionList.Count)
			{
				guideTipsView.GetComponent<GuideTipsViewCtl>().StartCurActionJudge();
				return;
			}
			oneStepJudgeEnd = true;
			stepIndex++;
			BasicGuide();
		}

		private void CountDownReset(int time)
		{
			countDownPrefabs = ViewJumpCtl.Instance.CreateViewPrefab("CountDown", Vector3.zero);
			countDownPrefabs.GetComponent<CountDownCtl>().SetCountDownTime(time);
			CountDownCtl component = countDownPrefabs.GetComponent<CountDownCtl>();
			component.CountDownEndAction = (UnityAction)Delegate.Combine(component.CountDownEndAction, (UnityAction)delegate
			{
				oneStepJudgeEnd = true;
				stepIndex = 0;
				guideTipsView = ViewJumpCtl.Instance.CreateView(ViewType.GuideTipsView);
				BasicGuide();
				UnityEngine.Object.Destroy(countDownPrefabs);
			});
		}

		private void Reset()
		{
			if (countDownPrefabs == null)
			{
				stepIndex = 0;
				oneStepJudgeEnd = true;
				BasicGuide();
			}
		}

		private void OnDestroy()
		{
			if (guideTipsView != null)
			{
				UnityEngine.Object.Destroy(guideTipsView);
			}
			if (countDownPrefabs != null)
			{
				UnityEngine.Object.Destroy(countDownPrefabs);
			}
		}
	}
}
