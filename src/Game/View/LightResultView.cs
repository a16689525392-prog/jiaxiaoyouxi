using System.Collections.Generic;
using Scene;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class LightResultView : ViewBase
	{
		public GameObject contentList;

		public GameObject answerList;

		public GameObject resultList;

		public Sprite tSprite;

		public Sprite fSprite;

		private List<Text> contentTextList = new List<Text>();

		private List<Text> answerTextList = new List<Text>();

		private List<Image> resultImgeList = new List<Image>();

		public override void Awake()
		{
			base.Awake();
			InitLightResultView();
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				Reset();
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.ReturnPreView();
				SceneJumpCtl.Instance.EnterScene(SceneType.MainScene);
			}
		}

		private void Reset()
		{
			if (TrainSceneBase.BaseInstance.ResetAction != null)
			{
				TrainSceneBase.BaseInstance.ResetAction();
			}
			SoundPlayCtl.Instance.SoundPause();
			SceneTextShow.Instance.ShowNormalText("");
			ViewJumpCtl.Instance.ReturnPreView();
		}

		public void InitLightResultView()
		{
			for (int i = 0; i < contentList.transform.childCount; i++)
			{
				contentTextList.Add(contentList.transform.GetChild(i).GetComponentInChildren<Text>());
			}
			for (int j = 0; j < answerList.transform.childCount; j++)
			{
				answerTextList.Add(answerList.transform.GetChild(j).GetComponentInChildren<Text>());
			}
			for (int k = 0; k < resultList.transform.childCount; k++)
			{
				resultImgeList.Add(resultList.transform.GetChild(k).GetChild(0).GetComponent<Image>());
			}
		}

		public void SetExamResultInfo(LightExamTrainInfo mLightExamTrainInfo)
		{
			int count = mLightExamTrainInfo.questionTrainInfoList.Count;
			if (contentTextList.Count < mLightExamTrainInfo.questionTrainInfoList.Count)
			{
				count = contentTextList.Count;
			}
			else if (contentTextList.Count > mLightExamTrainInfo.questionTrainInfoList.Count)
			{
				for (int i = mLightExamTrainInfo.questionTrainInfoList.Count; i < contentTextList.Count; i++)
				{
					contentTextList[i].gameObject.SetActive(value: false);
					answerTextList[i].gameObject.SetActive(value: false);
					resultImgeList[i].gameObject.SetActive(value: false);
				}
			}
			for (int j = 0; j < count; j++)
			{
				contentTextList[j].text = mLightExamTrainInfo.questionTrainInfoList[j].content;
				answerTextList[j].text = mLightExamTrainInfo.questionTrainInfoList[j].answer;
				if (mLightExamTrainInfo.questionTrainInfoList[j].result == -1)
				{
					resultImgeList[j].sprite = tSprite;
				}
				else
				{
					resultImgeList[j].sprite = fSprite;
				}
			}
		}
	}
}
