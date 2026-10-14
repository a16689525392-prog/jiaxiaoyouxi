using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Global;
using Scene;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using View;

public class CourseThreeYanShiGuide : MonoBehaviour
{
	public static CourseThreeYanShiGuide Instance;

	public int CurYanshiTipsIndex;

	public bool YanShiPause;

	public GameObject YanShiTipsImage;

	private List<YanshiTipsEventInfo> YanshiTipsEventInfoList = new List<YanshiTipsEventInfo>();

	private void Awake()
	{
		Instance = this;
		TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
		baseInstance.ResetAction = (UnityAction)Delegate.Combine(baseInstance.ResetAction, new UnityAction(YanShiGuideReset));
	}

	private void Start()
	{
		CurYanshiTipsIndex = 0;
		YanShiPause = false;
	}

	private void Update()
	{
		if (!(DriveHistoryPlay.Instance != null) || YanshiTipsEventInfoList.Count <= 0 || CurYanshiTipsIndex >= YanshiTipsEventInfoList.Count)
		{
			return;
		}
		if (YanshiTipsEventInfoList[CurYanshiTipsIndex].isCreate)
		{
			if (DriveHistoryPlay.Instance.curIndexPlay >= YanshiTipsEventInfoList[CurYanshiTipsIndex].curYanShiIndexPlay && !YanShiPause)
			{
				YanShiPause = true;
				ViewJumpCtl.Instance.ShowYanShiTips(YanshiTipsEventInfoList[CurYanshiTipsIndex].YanshiTipsSound);
			}
		}
		else if (DriveHistoryPlay.Instance.curIndexPlay >= YanshiTipsEventInfoList[CurYanshiTipsIndex].curYanShiIndexPlay)
		{
			SoundPlayCtl.Instance.PlaySound(YanshiTipsEventInfoList[CurYanshiTipsIndex].YanshiTipsSound);
			CurYanshiTipsIndex++;
		}
	}

	public IEnumerator LoadYanShiGuideFile(string path)
	{
		string text = ((SceneJumpCtl.Instance.CoursethreeName != CoursethreeScenesName.NanChang) ? ((Application.platform == RuntimePlatform.Android) ? ((!SettingInfoCtl.Instance.isManualGear) ? ("file://" + Application.persistentDataPath + "/GuideAndRuleXml/CourseThree/AutoGear/") : ("file://" + Application.persistentDataPath + "/GuideAndRuleXml/CourseThree/ManualGear/")) : ((!SettingInfoCtl.Instance.isManualGear) ? DataCenter.Instance.GetWWWPath(Application.streamingAssetsPath + "/GuideAndRuleXml/CourseThree/AutoGear/") : DataCenter.Instance.GetWWWPath(Application.streamingAssetsPath + "/GuideAndRuleXml/CourseThree/ManualGear/"))) : ((!SettingInfoCtl.Instance.isManualGear) ? DataCenter.Instance.GetWWWPath(Application.streamingAssetsPath + "/GuideAndRuleXml/CourseThree/AutoGear/") : DataCenter.Instance.GetWWWPath(Application.streamingAssetsPath + "/GuideAndRuleXml/CourseThree/ManualGear/")));
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
		UnityWebRequest www = UnityWebRequest.Get(text + path);
		yield return www.SendWebRequest();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(www.downloadHandler.text);
		XmlNode yanshitipsEvent = xmlDocument.SelectSingleNode("Yanshi").SelectSingleNode("YanshiTips");
		ReadYanShiTipsEvent(yanshitipsEvent);
	}

	private void ReadYanShiTipsEvent(XmlNode yanshitipsEvent)
	{
		foreach (XmlElement childNode in yanshitipsEvent.ChildNodes)
		{
			YanshiTipsEventInfo yanshiTipsEventInfo = new YanshiTipsEventInfo();
			foreach (XmlElement item in childNode)
			{
				switch (item.Name)
				{
				case "index":
					yanshiTipsEventInfo.index = int.Parse(item.InnerText);
					break;
				case "curYanShiIndexPlay":
					yanshiTipsEventInfo.curYanShiIndexPlay = int.Parse(item.InnerText);
					break;
				case "YanshiTipsSoundText":
					yanshiTipsEventInfo.YanshiTipsSound = item.InnerText;
					break;
				case "isCreate":
					yanshiTipsEventInfo.isCreate = bool.Parse(item.InnerText);
					break;
				}
			}
			YanshiTipsEventInfoList.Add(yanshiTipsEventInfo);
		}
	}

	public void YanShiGuideReset()
	{
		CurYanshiTipsIndex = 0;
		YanShiPause = false;
	}

	private void OnDestroy()
	{
		TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
		baseInstance.ResetAction = (UnityAction)Delegate.Remove(baseInstance.ResetAction, new UnityAction(YanShiGuideReset));
	}
}
