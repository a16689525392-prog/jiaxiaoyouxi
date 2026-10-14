using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using View;

namespace Scene
{
	public class NightLightCtl : MonoBehaviour
	{
		private class LightSettingInfo
		{
			public float actionTime;

			public bool canActionWhilePlaySound;

			public bool errorRemind = true;

			public bool random = true;

			public int randomNum;
		}

		private class LightQuestionInfo
		{
			public int Index;

			public bool isFirst;

			public bool isEnd;

			public string soundText = "";

			public int action;
		}

		private class NightLightInfo
		{
			public LightSettingInfo mSettingInfo = new LightSettingInfo();

			public List<LightQuestionInfo> mLightQuestionInfo = new List<LightQuestionInfo>();
		}

		public static string actionGuide;

		private GameObject lightGuideView;

		private LightExamTrainInfo mLightExamTrainInfo = new LightExamTrainInfo();

		private GameObject countDownPrefabs;

		private int errorType = -1;

		private float intervaltime;

		private bool isFirstJudge = true;

		private int questionIndex;

		private bool trueOperate;

		private bool falseOperate;

		private bool isJudgeEnd;

		private int countDownStep;

		private QuestionTrainInfo curQuestionTrainInfo;

		private int preLightState;

		private bool result;

		private bool isStart;

		private Coroutine cor;

		private List<LightQuestionInfo> randomQuestionList = new List<LightQuestionInfo>();

		private List<int> randomList = new List<int>();

		private bool isReadFile;

		private int firstIndex = -1;

		private int endIndex = -1;

		private NightLightInfo mNightLightInfo = new NightLightInfo();

		private void Start()
		{
			if (ViewJumpCtl.Instance.modelType == 1)
			{
				lightGuideView = ViewJumpCtl.Instance.CreateView(ViewType.LightGuideView);
			}
			StartCoroutine(ReadNightLightConfig());
			countDownPrefabs = ViewJumpCtl.Instance.CreateViewPrefab("CountDown", Vector3.zero);
			StartNightLightExam(4);
			mLightExamTrainInfo.examStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetAction = (UnityAction)Delegate.Combine(baseInstance.ResetAction, new UnityAction(Reset));
		}

		private void Update()
		{
			if (isReadFile)
			{
				isReadFile = false;
				if (mNightLightInfo.mSettingInfo.random)
				{
					GetRandomQuestionList();
				}
			}
			if (isStart)
			{
				JudgeNightLight();
			}
		}

		private void JudgeNightLight()
		{
			if (ViewJumpCtl.Instance.modelType == 1)
			{
				if (!SoundPlayCtl.Instance.IsPlaying() && !SoundPlayCtl.Instance.IsPause())
				{
					intervaltime += Time.deltaTime;
					if (!isJudgeEnd)
					{
						actionGuide = CarActionJudge.Instance.GetActionText(randomQuestionList[questionIndex].action);
					}
				}
				else
				{
					actionGuide = "请播报完指令再操作";
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 2 && !SoundPlayCtl.Instance.IsPlaying() && !SoundPlayCtl.Instance.IsPause())
			{
				intervaltime += Time.deltaTime;
			}
			if (isFirstJudge)
			{
				curQuestionTrainInfo = new QuestionTrainInfo();
				curQuestionTrainInfo.content = randomQuestionList[questionIndex].soundText;
				curQuestionTrainInfo.oriState = GetLightState();
				curQuestionTrainInfo.answer = CarActionJudge.Instance.GetActionText(randomQuestionList[questionIndex].action);
				if (CarState.Instance.CarSignal.frontLight != 0 || CarState.Instance.CarSignal.widthLight != 0 || CarState.Instance.CarSignal.fogLight != 0)
				{
					errorType = 0;
				}
				SoundPlayCtl.Instance.PlaySound(randomQuestionList[questionIndex].soundText);
				preLightState = GetLightState();
				isFirstJudge = false;
			}
			if (preLightState != GetLightState())
			{
				curQuestionTrainInfo.operateList.Add(new LightOperate(GetLightState(), intervaltime));
				preLightState = GetLightState();
			}
			if (intervaltime < mNightLightInfo.mSettingInfo.actionTime)
			{
				if (!trueOperate && CarActionJudge.Instance.JudgeActionDone(randomQuestionList[questionIndex].action))
				{
					trueOperate = true;
				}
				if (!falseOperate && JudgeLightOperateError(randomQuestionList[questionIndex].action))
				{
					falseOperate = true;
				}
				if (SoundPlayCtl.Instance.IsPlaying() && countDownStep == 0)
				{
					countDownStep = 1;
				}
				else if (!SoundPlayCtl.Instance.IsPlaying() && countDownStep == 1)
				{
					countDownStep = 2;
					countDownPrefabs.GetComponent<CountDownCtl>().SetCountDownTime(6);
					intervaltime = 0f;
				}
			}
			else if (intervaltime >= mNightLightInfo.mSettingInfo.actionTime && !isJudgeEnd)
			{
				if (randomQuestionList[questionIndex].action == 6 || randomQuestionList[questionIndex].action == 7)
				{
					switch (CarActionJudge.Instance.GetFarAndNearActionDone())
					{
					case 0:
						trueOperate = false;
						break;
					case 1:
						trueOperate = false;
						errorType = 4;
						break;
					case 2:
						trueOperate = true;
						break;
					}
				}
				if (errorType == -1)
				{
					if (trueOperate && falseOperate)
					{
						errorType = 2;
					}
					else if (!trueOperate)
					{
						errorType = 3;
					}
				}
				string actionText = CarActionJudge.Instance.GetActionText(randomQuestionList[questionIndex].action);
				if (errorType != -1)
				{
					SoundPlayCtl.Instance.PlaySound(ParseErrorType(errorType, actionText), 100f);
				}
				isJudgeEnd = true;
			}
			else if (intervaltime > mNightLightInfo.mSettingInfo.actionTime + 1f)
			{
				curQuestionTrainInfo.questionEndTime = intervaltime;
				curQuestionTrainInfo.result = errorType;
				mLightExamTrainInfo.questionTrainInfoList.Add(curQuestionTrainInfo);
				errorType = -1;
				trueOperate = false;
				falseOperate = false;
				intervaltime = 0f;
				isJudgeEnd = false;
				countDownStep = 0;
				if (questionIndex < randomQuestionList.Count - 1)
				{
					SoundPlayCtl.Instance.PlaySound(randomQuestionList[++questionIndex].soundText);
					curQuestionTrainInfo = new QuestionTrainInfo();
					curQuestionTrainInfo.content = randomQuestionList[questionIndex].soundText;
					curQuestionTrainInfo.oriState = GetLightState();
					curQuestionTrainInfo.answer = CarActionJudge.Instance.GetActionText(randomQuestionList[questionIndex].action);
				}
				else if (ViewJumpCtl.Instance.IsKaoShi)
				{
					HuNanYZCourseThreeKaoShi.Instance.NightLightIsOver = true;
				}
				else
				{
					isStart = false;
					ViewJumpCtl.Instance.EnterView(ViewType.LightResultView, isDestroyCurView: false);
					ViewJumpCtl.Instance.curView.GetComponent<LightResultView>().SetExamResultInfo(mLightExamTrainInfo);
					SoundPlayCtl.Instance.ClearClip();
				}
			}
		}

		private int GetLightState()
		{
			int num = -1;
			if (CarActionJudge.Instance.JudgeActionDone(0))
			{
				return 3;
			}
			if (CarActionJudge.Instance.JudgeActionDone(2))
			{
				return 2;
			}
			if (CarActionJudge.Instance.JudgeActionDone(4))
			{
				return 1;
			}
			return 0;
		}

		public bool JudgeLightOperateError(int action)
		{
			result = false;
			switch (action)
			{
			case 0:
				if (GetLightState() < preLightState)
				{
					result = true;
				}
				else if (trueOperate && CarActionJudge.Instance.JudgeActionDone(2))
				{
					result = true;
				}
				break;
			case 2:
				if (GetLightState() < preLightState && GetLightState() != 2)
				{
					result = true;
				}
				else if (trueOperate && CarActionJudge.Instance.JudgeActionDone(0))
				{
					result = true;
				}
				break;
			case 4:
				if (GetLightState() < preLightState && preLightState == 1)
				{
					result = true;
				}
				else if (trueOperate && GetLightState() != 1)
				{
					result = true;
				}
				break;
			case 8:
				if (GetLightState() > preLightState)
				{
					result = true;
				}
				else if (trueOperate && GetLightState() > preLightState)
				{
					result = true;
				}
				break;
			case 6:
			case 7:
				if (GetLightState() < preLightState && GetLightState() != 2)
				{
					result = true;
				}
				break;
			}
			return result;
		}

		public void StartNightLightExam(int time)
		{
			if (cor != null)
			{
				StopCoroutine(cor);
			}
			cor = StartCoroutine(WaitTime(time));
			countDownPrefabs.GetComponent<CountDownCtl>().SetCountDownTime(time);
		}

		private IEnumerator WaitTime(float time)
		{
			if (time > 0f)
			{
				yield return new WaitForSeconds(time);
			}
			isStart = true;
			yield return 0;
		}

		private void Reset()
		{
			errorType = -1;
			trueOperate = false;
			falseOperate = false;
			intervaltime = 0f;
			isJudgeEnd = false;
			isFirstJudge = true;
			countDownStep = 0;
			questionIndex = 0;
			isStart = false;
			mLightExamTrainInfo.questionTrainInfoList.Clear();
			GetRandomQuestionList();
			StartNightLightExam(4);
		}

		private void GetRandomQuestionList()
		{
			randomList.Clear();
			randomQuestionList.Clear();
			randomList.Add(firstIndex);
			randomList.Add(endIndex);
			randomQuestionList.Add(mNightLightInfo.mLightQuestionInfo[firstIndex]);
			int num = -1;
			int num2 = 0;
			while (randomQuestionList.Count < mNightLightInfo.mSettingInfo.randomNum - 1 && num2 < 500)
			{
				num2 = 0;
				do
				{
					num = UnityEngine.Random.Range(0, mNightLightInfo.mLightQuestionInfo.Count - 1);
					num2++;
				}
				while (JudgeIndexExist(num, randomList) && num2 < 500);
				foreach (LightQuestionInfo item in mNightLightInfo.mLightQuestionInfo)
				{
					if (item.Index == num)
					{
						randomList.Add(item.Index);
						randomQuestionList.Add(item);
					}
				}
			}
			if (num2 >= 500)
			{
				Debug.LogError("题库index 数据有误");
			}
			randomQuestionList.Add(mNightLightInfo.mLightQuestionInfo[endIndex]);
		}

		private bool JudgeIndexExist(int index, List<int> intList)
		{
			foreach (int @int in intList)
			{
				if (index == @int)
				{
					return true;
				}
			}
			return false;
		}

		private string ParseErrorType(int errorType, string answer = "")
		{
			string text = "";
			switch (errorType)
			{
			case 0:
				text = "灯光考试开始前未关闭所有灯光";
				break;
			case 1:
				text = "未播报完指令即进行操作";
				break;
			case 2:
				text = "进行了正确灯光操作外的其他操作";
				break;
			case 3:
				text = "未" + answer;
				break;
			case 4:
				text = "只进行了一次远近光灯交替";
				break;
			}
			return text;
		}

		private IEnumerator ReadNightLightConfig()
		{
			string text = Application.streamingAssetsPath + "/NightLight/";
			switch (SceneJumpCtl.Instance.CoursethreeName)
			{
			case CoursethreeScenesName.NanChang:
				text += "LanTian.xml";
				break;
			case CoursethreeScenesName.HuNanYZ:
				text += "JiuYi.xml";
				break;
			case CoursethreeScenesName.RenCai:
				text += "RenCai.xml";
				break;
			case CoursethreeScenesName.ChengDong:
				text += "ChengDong.xml";
				break;
			case CoursethreeScenesName.BaiYun:
				text += "BaiYun.xml";
				break;
			default:
				text += "LanTian.xml";
				break;
			}
			UnityWebRequest www = UnityWebRequest.Get(DataCenter.Instance.GetWWWPath(text));
			yield return www.SendWebRequest();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(www.downloadHandler.text);
			foreach (XmlElement childNode in xmlDocument.SelectSingleNode("nightLight").SelectSingleNode("setting").ChildNodes)
			{
				switch (childNode.Name)
				{
				case "actionTime":
					mNightLightInfo.mSettingInfo.actionTime = float.Parse(childNode.InnerText);
					break;
				case "canActionWhilePlaySound":
					mNightLightInfo.mSettingInfo.canActionWhilePlaySound = bool.Parse(childNode.InnerText);
					break;
				case "errorRemind":
					mNightLightInfo.mSettingInfo.errorRemind = bool.Parse(childNode.InnerText);
					break;
				case "random":
					mNightLightInfo.mSettingInfo.random = bool.Parse(childNode.InnerText);
					break;
				case "randomNum":
					mNightLightInfo.mSettingInfo.randomNum = int.Parse(childNode.InnerText);
					break;
				}
			}
			foreach (XmlElement childNode2 in xmlDocument.SelectSingleNode("nightLight").SelectSingleNode("questionList").ChildNodes)
			{
				LightQuestionInfo lightQuestionInfo = new LightQuestionInfo();
				lightQuestionInfo.Index = int.Parse(childNode2.GetAttribute("index"));
				foreach (XmlElement item in childNode2)
				{
					switch (item.Name)
					{
					case "soundText":
						lightQuestionInfo.soundText = item.InnerText;
						break;
					case "action":
						lightQuestionInfo.action = int.Parse(item.InnerText);
						break;
					case "isFirst":
						lightQuestionInfo.isFirst = bool.Parse(item.InnerText);
						if (lightQuestionInfo.isFirst)
						{
							firstIndex = lightQuestionInfo.Index;
						}
						break;
					case "isEnd":
						lightQuestionInfo.isEnd = bool.Parse(item.InnerText);
						if (lightQuestionInfo.isEnd)
						{
							endIndex = lightQuestionInfo.Index;
						}
						break;
					}
				}
				mNightLightInfo.mLightQuestionInfo.Add(lightQuestionInfo);
			}
			if (mNightLightInfo.mSettingInfo.randomNum > mNightLightInfo.mLightQuestionInfo.Count)
			{
				mNightLightInfo.mSettingInfo.randomNum = mNightLightInfo.mLightQuestionInfo.Count;
			}
			isReadFile = true;
		}

		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(countDownPrefabs);
			actionGuide = "";
			UnityEngine.Object.Destroy(lightGuideView);
			SoundPlayCtl.Instance.ClearClip();
		}
	}
}
