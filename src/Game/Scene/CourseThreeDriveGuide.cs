using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using View;

namespace Scene
{
	public class CourseThreeDriveGuide : MonoBehaviour
	{
		public static CourseThreeDriveGuide Instance;

		private GuideStepInfo curGuideInfo;

		private GuideSignalShow guideSignalShow;

		private GameObject guideView;

		private int curItemIndex;

		private int curGuideActionIndex;

		private int curJudgeDoneIndex = -1;

		private List<GuideStepInfo> guideStepInfoList = new List<GuideStepInfo>();

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetAction = (UnityAction)Delegate.Combine(baseInstance.ResetAction, new UnityAction(Reset));
		}

		private void Update()
		{
			UpdateGuideAction();
		}

		public IEnumerator LoadGuideFile(string path)
		{
			string text = ((!SettingInfoCtl.Instance.isManualGear) ? "/GuideAndRuleXml/CourseThree/AutoGear/" : "/GuideAndRuleXml/CourseThree/ManualGear/");
			switch (SceneJumpCtl.Instance.CoursethreeName)
			{
			case CoursethreeScenesName.NanChang:
				text += "NanChang/";
				break;
			case CoursethreeScenesName.HuNanYZ:
				text += "HuNanYZ/";
				break;
			case CoursethreeScenesName.RenCai:
				text += "RenCai/";
				break;
			case CoursethreeScenesName.ChengDong:
				text += "ChengDong/";
				break;
			case CoursethreeScenesName.BaiYun:
				text += "BaiYun/";
				break;
			default:
				text += "NanChang/";
				break;
			}
			UnityWebRequest www = UnityWebRequest.Get(DataCenter.Instance.GetWWWPath(Application.streamingAssetsPath + text + path));
			yield return www.SendWebRequest();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(www.downloadHandler.text);
			XmlNode guideOperate = xmlDocument.SelectSingleNode("guide").SelectSingleNode("guideOperate");
			ReadGuideOperate(guideOperate);
		}

		public void JudgeGuide(int index)
		{
			if (curItemIndex >= guideStepInfoList.Count || index <= guideStepInfoList[curItemIndex].index)
			{
				return;
			}
			if (guideStepInfoList[curItemIndex].titleText == "起步" || guideStepInfoList[curItemIndex].titleText == "靠边停车" || guideStepInfoList[curItemIndex].titleText == "加减挡")
			{
				curGuideInfo = guideStepInfoList[curItemIndex];
				if (curGuideInfo.titleText != "")
				{
					SoundPlayCtl.Instance.PlaySound(guideStepInfoList[curItemIndex].titleText, 0f, 1);
				}
				if (curGuideInfo.actionList[0].content != "")
				{
					if (curGuideInfo.actionList[0].isAction)
					{
						SoundPlayCtl.Instance.PlaySound(curGuideInfo.actionList[0].content, 0f, 3);
					}
					else
					{
						SoundPlayCtl.Instance.PlaySound(curGuideInfo.actionList[0].content);
					}
				}
				if (guideView != null)
				{
					UnityEngine.Object.Destroy(guideView);
				}
				guideView = ViewJumpCtl.Instance.CreateView(ViewType.GuideTipsView);
				guideView.GetComponent<GuideTipsViewCtl>().InitGuideTipsView(curGuideInfo);
				curGuideActionIndex = 0;
				curJudgeDoneIndex = -1;
				curItemIndex++;
			}
			else
			{
				curGuideInfo = guideStepInfoList[curItemIndex];
				if (guideView != null)
				{
					UnityEngine.Object.Destroy(guideView);
				}
				if (curGuideInfo.titleText != "")
				{
					SoundPlayCtl.Instance.PlaySound(guideStepInfoList[curItemIndex].titleText, 0f, 1);
				}
				guideView = ViewJumpCtl.Instance.CreateView(ViewType.GuideTipsView);
				guideView.GetComponent<GuideTipsViewCtl>().InitGuideTipsView(curGuideInfo, selfJudge: true);
				curGuideInfo = null;
				curItemIndex++;
			}
		}

		private void UpdateGuideAction()
		{
			if (curGuideInfo != null && curJudgeDoneIndex < curGuideInfo.actionList.Count - 1)
			{
				if (curGuideActionIndex > curJudgeDoneIndex && (!curGuideInfo.actionList[curGuideActionIndex].isAction || CarActionJudge.Instance.JudgeActionDone(curGuideInfo.actionList[curGuideActionIndex].content)))
				{
					SoundPlayCtl.Instance.RemoveSound(curGuideInfo.actionList[curGuideActionIndex].content);
					guideView.GetComponent<GuideTipsViewCtl>().SetOperateDone();
					curJudgeDoneIndex++;
				}
				if (curGuideActionIndex != curJudgeDoneIndex || curGuideActionIndex >= curGuideInfo.actionList.Count - 1 || CarDriveHistoryPlay.Instance.CurPlayIndex <= curGuideInfo.actionList[curGuideActionIndex + 1].startIndex)
				{
					return;
				}
				curGuideActionIndex++;
				if (curGuideActionIndex < curGuideInfo.actionList.Count)
				{
					if (curGuideInfo.actionList[curGuideActionIndex].isAction)
					{
						SoundPlayCtl.Instance.PlaySound(curGuideInfo.actionList[curGuideActionIndex].content, 0f, 3);
					}
					else
					{
						SoundPlayCtl.Instance.PlaySound(curGuideInfo.actionList[curGuideActionIndex].content);
					}
				}
			}
			else if (curGuideInfo != null && curJudgeDoneIndex == curGuideInfo.actionList.Count - 1)
			{
				UnityEngine.Object.Destroy(guideView, 3f);
				curGuideInfo = null;
			}
		}

		private void UpdateJudgeGuideSignalShow()
		{
			if (guideSignalShow.gameObject.activeSelf && CarDriveHistoryPlay.Instance.IsPlayEnd())
			{
				guideSignalShow.gameObject.SetActive(value: false);
			}
			else if (!guideSignalShow.gameObject.activeSelf && !CarDriveHistoryPlay.Instance.IsPlayEnd())
			{
				guideSignalShow.gameObject.SetActive(value: true);
			}
		}

		private void ReadGuideOperate(XmlNode guideOperate)
		{
			guideStepInfoList.Clear();
			foreach (XmlElement childNode in guideOperate.ChildNodes)
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
							case "startIndex":
								actionInfo.startIndex = int.Parse(childNode2.InnerText);
								break;
							case "endIndex":
								actionInfo.endIndex = int.Parse(childNode2.InnerText);
								break;
							case "duringIndex":
								actionInfo.duringIndex = int.Parse(childNode2.InnerText);
								break;
							case "isAction":
								actionInfo.isAction = bool.Parse(childNode2.InnerText);
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

		private void Reset()
		{
			curItemIndex = 0;
			curJudgeDoneIndex = -1;
			curGuideInfo = null;
			curGuideActionIndex = 0;
			if (guideView != null)
			{
				UnityEngine.Object.Destroy(guideView);
			}
		}

		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(guideView);
			if (guideSignalShow != null)
			{
				UnityEngine.Object.Destroy(guideSignalShow.gameObject);
			}
		}
	}
}
