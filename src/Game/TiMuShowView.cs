using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Global;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;
using View;

public class TiMuShowView : ViewBase
{
	public static TiMuShowView Instance;

	public GameObject goBack;

	public GameObject goLastStep;

	public GameObject goNextStep;

	public Text questionShow;

	public Text numOfQuestion;

	public GameObject ShowRight;

	public GameObject ShowWrong;

	public Text showRightSelectAnswer;

	public Text showWrongSelect;

	public Text showRightAnswer;

	public GameObject goAChoose;

	public GameObject goBChoose;

	public GameObject goCChoose;

	public GameObject goDChoose;

	public Text A_Answer;

	public Text B_Answer;

	public Text C_Answer;

	public Text D_Answer;

	public Text showAnswerExplain;

	public Image ShowImage;

	public GameObject ShowRawImage;

	public GameObject goTiJiaoDaAn;

	public GameObject goChongZhi;

	public GameObject goHandInBtn;

	public GameObject showTime;

	public Text showTimeText;

	public static List<QuestionBody> mQuestions;

	public static int num;

	public Text ShowRightNumber;

	public Text ShowWrongNumber;

	public Text ShowNotAnswerNumber;

	public int recordSelectRightNum;

	private int recordSelectWrongNum;

	public List<int> mErrorId;

	private bool ifClick_A;

	private bool ifClick_B;

	private bool ifClick_C;

	private bool ifClick_D;

	private byte sumClick;

	public static float moniTime;

	public int needQuestion;

	public GameObject ShowTestResult;

	public GameObject Quit;

	private bool showResultView;

	public Text ifPassText;

	public Text getScore;

	public Text usedTime;

	public bool CuoTiF;

	public GameObject CuoTiNull;

	public GameObject CuoTiNullBack;

	public override void Awake()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			StartCoroutine(ReadCreateQuestion(GetCuoTiName(), GetTagCuoTiName()));
			if (File.Exists(GetTagCuoTiName()))
			{
				FileStream fileStream = new FileInfo(GetTagCuoTiName()).OpenRead();
				if (fileStream.Length != 0L)
				{
					CuoTiF = true;
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					mErrorId = (List<int>)binaryFormatter.Deserialize(fileStream);
				}
				else
				{
					CuoTiF = false;
				}
				fileStream.Close();
			}
		}
		else if (File.Exists(GetCuoTiName()))
		{
			FileStream fileStream2 = new FileInfo(GetCuoTiName()).OpenRead();
			if (fileStream2.Length != 0L)
			{
				CuoTiF = true;
				BinaryFormatter binaryFormatter2 = new BinaryFormatter();
				mErrorId = (List<int>)binaryFormatter2.Deserialize(fileStream2);
			}
			else
			{
				CuoTiF = false;
			}
			fileStream2.Close();
		}
		if (ViewJumpCtl.Instance.trainType == TrainType.CourseOne)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				StartCoroutine(ReadCreateQuestion(Application.streamingAssetsPath + "/KeMuYi/KeMuYi_ShunXuLianXi/KeMuYi_ShunXuLianXi", Application.persistentDataPath + "/KeMuYi/KeMuYi_ShunXuLianXi"));
				ReadQuestion(Application.persistentDataPath + "/KeMuYi/KeMuYi_ShunXuLianXi", ref mQuestions);
			}
			else
			{
				ReadQuestion(Application.streamingAssetsPath + "/KeMuYi/KeMuYi_ShunXuLianXi/KeMuYi_ShunXuLianXi", ref mQuestions);
			}
			if (ViewJumpCtl.Instance.itemType == 1002)
			{
				needQuestion = 100;
				ResetQuestionUnAnswered(needQuestion);
			}
			if (ViewJumpCtl.Instance.itemType == 1003 && CuoTiF && mErrorId != null && mErrorId.Count != 0)
			{
				for (int i = 0; i < mQuestions.Count; i++)
				{
					if (!mErrorId.Contains(mQuestions[i].id))
					{
						mQuestions.Remove(mQuestions[i]);
						i--;
					}
				}
				if (mQuestions == null || mQuestions.Count == 0)
				{
					return;
				}
				foreach (QuestionBody mQuestion in mQuestions)
				{
					mQuestion.ifAnswer = false;
					mQuestion.RecordSelectAnswer = 0;
				}
			}
		}
		else if (ViewJumpCtl.Instance.trainType == TrainType.CourseFour)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				StartCoroutine(ReadCreateQuestion(Application.streamingAssetsPath + "/KeMuSi/KeMuSi_ShunXuLianXi/kemusi", Application.persistentDataPath + "/KeMuSi/KeMuSi_ShunXuLianXi"));
				ReadQuestion(Application.persistentDataPath + "/KeMuSi/KeMuSi_ShunXuLianXi", ref mQuestions);
			}
			else
			{
				ReadQuestion(Application.streamingAssetsPath + "/KeMuSi/KeMuSi_ShunXuLianXi/kemusi", ref mQuestions);
			}
			if (ViewJumpCtl.Instance.itemType == 1002)
			{
				needQuestion = 50;
				ResetQuestionUnAnswered(needQuestion);
			}
			if (ViewJumpCtl.Instance.itemType == 1003 && CuoTiF && mErrorId != null && mErrorId.Count != 0)
			{
				for (int j = 0; j < mQuestions.Count; j++)
				{
					if (!mErrorId.Contains(mQuestions[j].id))
					{
						mQuestions.Remove(mQuestions[j]);
						j--;
					}
				}
				if (mQuestions == null || mQuestions.Count == 0)
				{
					return;
				}
				foreach (QuestionBody mQuestion2 in mQuestions)
				{
					mQuestion2.ifAnswer = false;
					mQuestion2.RecordSelectAnswer = 0;
				}
			}
		}
		Instance = this;
		base.Awake();
	}

	private void ResetQuestionUnAnswered(int num)
	{
		System.Random random = new System.Random();
		while (mQuestions.Count > num)
		{
			mQuestions.RemoveAt(random.Next(mQuestions.Count));
		}
		if (mQuestions == null || mQuestions.Count == 0)
		{
			return;
		}
		foreach (QuestionBody mQuestion in mQuestions)
		{
			mQuestion.ifAnswer = false;
			mQuestion.RecordSelectAnswer = 0;
		}
	}

	private void TestResult()
	{
		ShowTestResult.SetActive(value: true);
		SetViewContent();
		btnList.Clear();
		btnDic.Clear();
		viewBtnCount = 0;
		btnList.Add(Quit);
		Quit.GetComponent<NormalBtn>().index = 0;
		base.ClearBtnFangFa();
		base.Awake();
	}

	public void SetViewContent()
	{
		if (ViewJumpCtl.Instance.trainType == TrainType.CourseOne)
		{
			if (recordSelectRightNum > 90 && moniTime >= 0f)
			{
				ifPassText.text = "恭喜你通过了科目一理论考试！";
				getScore.text = string.Concat(recordSelectRightNum);
				usedTime.text = GetFormatTime("{0:00}:{1:00}:{2:00}", 2700f - moniTime);
			}
			else
			{
				ifPassText.text = "很遗憾，科目一考试不通过！";
				getScore.text = string.Concat(recordSelectRightNum);
				usedTime.text = GetFormatTime("{0:00}:{1:00}:{2:00}", 2700f - moniTime);
			}
		}
		else if (ViewJumpCtl.Instance.trainType == TrainType.CourseFour)
		{
			if (recordSelectRightNum > 45 && moniTime >= 0f)
			{
				ifPassText.text = "恭喜你通过了科目四理论考试！";
				getScore.text = string.Concat(recordSelectRightNum * 2);
				usedTime.text = GetFormatTime("{0:00}:{1:00}:{2:00}", 2700f - moniTime);
			}
			else
			{
				ifPassText.text = "很遗憾，科目四考试不通过！";
				getScore.text = string.Concat(recordSelectRightNum * 2);
				usedTime.text = GetFormatTime("{0:00}:{1:00}:{2:00}", 2700f - moniTime);
			}
		}
	}

	public override void Start()
	{
		if (ViewJumpCtl.Instance.itemType == 1003 && !CuoTiF)
		{
			CuoTiNull.SetActive(value: true);
			btnList.Clear();
			btnDic.Clear();
			viewBtnCount = 0;
			btnList.Add(CuoTiNullBack);
			CuoTiNullBack.GetComponent<NormalBtn>().index = 0;
			base.ClearBtnFangFa();
			base.Awake();
			return;
		}
		base.Start();
		num = 0;
		recordSelectRightNum = 0;
		recordSelectWrongNum = 0;
		showResultView = true;
		ShowContent();
		ButtonPaiLie();
		if (ViewJumpCtl.Instance.itemType == 1002)
		{
			goHandInBtn.gameObject.SetActive(value: true);
			showTime.gameObject.SetActive(value: true);
			moniTime = 2700f;
		}
		else
		{
			goHandInBtn.gameObject.SetActive(value: false);
			showTime.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (ViewJumpCtl.Instance.itemType == 1002)
		{
			SetShowTime();
		}
	}

	private void SetShowTime()
	{
		if (moniTime > 0f)
		{
			moniTime -= Time.deltaTime;
			showTimeText.text = GetFormatTime("{0:00}:{1:00}:{2:00}", moniTime);
		}
		else
		{
			moniTime = 0f;
			showTimeText.text = GetFormatTime("{0:00}:{1:00}:{2:00}", moniTime);
		}
		if (moniTime <= 0f && showResultView)
		{
			TestResult();
			showResultView = false;
		}
	}

	public static string GetFormatTime(string formatStr, float time)
	{
		int num = (int)time / 3600;
		int num2 = (int)(time - (float)(num * 3600)) / 60;
		int num3 = (int)(time - (float)(num * 3600) - (float)(num2 * 60));
		return string.Format(formatStr, num, num2, num3);
	}

	private void ShowNumbers()
	{
		ShowRightNumber.text = "对：" + recordSelectRightNum;
		ShowWrongNumber.text = "错：" + recordSelectWrongNum;
		ShowNotAnswerNumber.text = "未答：" + (mQuestions.Count - recordSelectRightNum - recordSelectWrongNum);
	}

	public override void BtnClick(GameObject go)
	{
		if (go.name == "Back")
		{
			ViewJumpCtl.Instance.ReturnPreView();
		}
		else if (go.name == "CuoTiNullBack")
		{
			ViewJumpCtl.Instance.ReturnPreView();
		}
		else if (go.name == "Quit")
		{
			ViewJumpCtl.Instance.ReturnPreView();
		}
		else if (go.name == "LastStepArrow")
		{
			num--;
			if (num < 0)
			{
				num = mQuestions.Count - 1;
			}
			ShowContent();
			ButtonPaiLie();
			RefreshLastAndNext();
		}
		else if (go.name == "NextStepArrow")
		{
			num++;
			if (num > mQuestions.Count - 1)
			{
				num = 0;
			}
			ShowContent();
			ButtonPaiLie();
			RefreshLastAndNext();
		}
		else if (go.name == "chongzhi")
		{
			num = 0;
			recordSelectRightNum = 0;
			recordSelectWrongNum = 0;
			foreach (QuestionBody mQuestion in mQuestions)
			{
				mQuestion.ifAnswer = false;
				mQuestion.RecordSelectAnswer = 0;
			}
			ShowContent();
			ButtonPaiLie();
			RefreshLastAndNext();
		}
		else if (go.name == "AChoose" && !mQuestions[num].ifAnswer)
		{
			ChooseClick(ifClick_A, go);
			if (mQuestions[num].type == 3 && !mQuestions[num].ifAnswer)
			{
				if (!ifClick_A)
				{
					sumClick--;
				}
				else if (ifClick_A)
				{
					sumClick++;
				}
			}
			else if ((mQuestions[num].type == 2 || mQuestions[num].type == 1) && !mQuestions[num].ifAnswer)
			{
				JudgeAnswer(1, goDChoose);
			}
			ShowContent();
		}
		else if (go.name == "BChoose" && !mQuestions[num].ifAnswer)
		{
			ChooseClick(ifClick_B, go);
			if (mQuestions[num].type == 3 && !mQuestions[num].ifAnswer)
			{
				if (!ifClick_B)
				{
					sumClick -= 2;
				}
				else if (ifClick_B)
				{
					sumClick += 2;
				}
			}
			else if ((mQuestions[num].type == 2 || mQuestions[num].type == 1) && !mQuestions[num].ifAnswer)
			{
				JudgeAnswer(2, goDChoose);
			}
			ShowContent();
		}
		else if (go.name == "CChoose" && !mQuestions[num].ifAnswer)
		{
			ChooseClick(ifClick_C, go);
			if (mQuestions[num].type == 3 && !mQuestions[num].ifAnswer)
			{
				if (!ifClick_C)
				{
					sumClick -= 4;
				}
				else if (ifClick_C)
				{
					sumClick += 4;
				}
			}
			else if ((mQuestions[num].type == 2 || mQuestions[num].type == 1) && !mQuestions[num].ifAnswer)
			{
				JudgeAnswer(4, goDChoose);
			}
			ShowContent();
		}
		else if (go.name == "DChoose" && !mQuestions[num].ifAnswer)
		{
			ChooseClick(ifClick_D, go);
			if (mQuestions[num].type == 3 && !mQuestions[num].ifAnswer)
			{
				if (!ifClick_D)
				{
					sumClick -= 8;
				}
				else if (ifClick_D)
				{
					sumClick += 8;
				}
			}
			else if ((mQuestions[num].type == 2 || mQuestions[num].type == 1) && !mQuestions[num].ifAnswer)
			{
				JudgeAnswer(8, goDChoose);
			}
			ShowContent();
		}
		else if (go.name == "TiJiaoDaAn")
		{
			JudgeAnswer(sumClick, goTiJiaoDaAn);
			ShowContent();
		}
		else if (go.name == "HandIn")
		{
			TestResult();
		}
	}

	private void ReadQuestion(string filePath, ref List<QuestionBody> qustion)
	{
		if (File.Exists(filePath))
		{
			FileStream fileStream = new FileInfo(filePath).OpenRead();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			qustion = (List<QuestionBody>)binaryFormatter.Deserialize(fileStream);
			fileStream.Close();
		}
	}

	private IEnumerator ReadCreateQuestion(string filePath, string tagfilePath)
	{
		if (!File.Exists(tagfilePath))
		{
			UnityWebRequest www = UnityWebRequest.Get(DataCenter.Instance.GetWWWPath(filePath));
			DownloadHandlerFile downloadHandler = new DownloadHandlerFile(tagfilePath);
			www.downloadHandler = downloadHandler;
			yield return www.SendWebRequest();
			while (!www.isDone)
			{
				yield return null;
			}
		}
	}

	private void ShowContent()
	{
		ShowRight.SetActive(value: false);
		ShowWrong.SetActive(value: false);
		showRightSelectAnswer.text = "";
		showWrongSelect.text = "";
		showRightAnswer.text = "";
		if (mQuestions[num].ifAnswer)
		{
			if (mQuestions[num].answerRight)
			{
				ShowRight.SetActive(value: true);
				showRightSelectAnswer.text = "您的回答：" + TurnAnswerToZiMu(mQuestions[num].RecordSelectAnswer) + "  回答正确";
			}
			else
			{
				ShowWrong.SetActive(value: true);
				showWrongSelect.text = "您的回答：" + TurnAnswerToZiMu(mQuestions[num].RecordSelectAnswer);
				showRightAnswer.text = "正确答案是：" + TurnAnswerToZiMu(mQuestions[num].rightAnswer);
			}
			showAnswerExplain.gameObject.SetActive(value: true);
			ShowAnswerExplain();
		}
		else if (!mQuestions[num].ifAnswer)
		{
			showAnswerExplain.gameObject.SetActive(value: false);
		}
		SetContent();
	}

	private void ButtonPaiLie()
	{
		if (mQuestions[num].type == 1)
		{
			if (ViewJumpCtl.Instance.itemType == 1002)
			{
				goChongZhi.SetActive(value: false);
				btnList.Clear();
				btnDic.Clear();
				viewBtnCount = 0;
				btnList.Add(goBack);
				btnList.Add(goLastStep);
				btnList.Add(goNextStep);
				btnList.Add(goHandInBtn);
				btnList.Add(goAChoose);
				btnList.Add(goBChoose);
				goBack.GetComponent<NormalBtn>().index = 0;
				goLastStep.GetComponent<NormalBtn>().index = 1;
				goNextStep.GetComponent<NormalBtn>().index = 2;
				goAChoose.GetComponent<NormalBtn>().index = 3;
				goBChoose.GetComponent<NormalBtn>().index = 4;
				goHandInBtn.GetComponent<NormalBtn>().index = 5;
			}
			else
			{
				btnList.Clear();
				btnDic.Clear();
				viewBtnCount = 0;
				btnList.Add(goBack);
				btnList.Add(goLastStep);
				btnList.Add(goNextStep);
				btnList.Add(goChongZhi);
				btnList.Add(goAChoose);
				btnList.Add(goBChoose);
				goBack.GetComponent<NormalBtn>().index = 0;
				goLastStep.GetComponent<NormalBtn>().index = 1;
				goNextStep.GetComponent<NormalBtn>().index = 2;
				goAChoose.GetComponent<NormalBtn>().index = 3;
				goBChoose.GetComponent<NormalBtn>().index = 4;
				goChongZhi.GetComponent<NormalBtn>().index = 5;
			}
		}
		else if (mQuestions[num].type == 2)
		{
			if (ViewJumpCtl.Instance.itemType == 1002)
			{
				goChongZhi.SetActive(value: false);
				btnList.Clear();
				btnDic.Clear();
				viewBtnCount = 0;
				btnList.Add(goBack);
				btnList.Add(goLastStep);
				btnList.Add(goNextStep);
				btnList.Add(goHandInBtn);
				btnList.Add(goAChoose);
				btnList.Add(goBChoose);
				btnList.Add(goCChoose);
				btnList.Add(goDChoose);
				goBack.GetComponent<NormalBtn>().index = 0;
				goLastStep.GetComponent<NormalBtn>().index = 1;
				goNextStep.GetComponent<NormalBtn>().index = 2;
				goAChoose.GetComponent<NormalBtn>().index = 3;
				goBChoose.GetComponent<NormalBtn>().index = 4;
				goCChoose.GetComponent<NormalBtn>().index = 5;
				goDChoose.GetComponent<NormalBtn>().index = 6;
				goHandInBtn.GetComponent<NormalBtn>().index = 7;
			}
			else
			{
				btnList.Clear();
				btnDic.Clear();
				viewBtnCount = 0;
				btnList.Add(goBack);
				btnList.Add(goLastStep);
				btnList.Add(goNextStep);
				btnList.Add(goChongZhi);
				btnList.Add(goAChoose);
				btnList.Add(goBChoose);
				btnList.Add(goCChoose);
				btnList.Add(goDChoose);
				goBack.GetComponent<NormalBtn>().index = 0;
				goLastStep.GetComponent<NormalBtn>().index = 1;
				goNextStep.GetComponent<NormalBtn>().index = 2;
				goAChoose.GetComponent<NormalBtn>().index = 3;
				goBChoose.GetComponent<NormalBtn>().index = 4;
				goCChoose.GetComponent<NormalBtn>().index = 5;
				goDChoose.GetComponent<NormalBtn>().index = 6;
				goChongZhi.GetComponent<NormalBtn>().index = 7;
			}
		}
		else if (mQuestions[num].type == 3)
		{
			if (ViewJumpCtl.Instance.itemType == 1002)
			{
				goChongZhi.SetActive(value: false);
				if (!mQuestions[num].ifAnswer)
				{
					goTiJiaoDaAn.SetActive(value: true);
					btnList.Clear();
					btnDic.Clear();
					viewBtnCount = 0;
					btnList.Add(goBack);
					btnList.Add(goLastStep);
					btnList.Add(goNextStep);
					btnList.Add(goHandInBtn);
					btnList.Add(goAChoose);
					btnList.Add(goBChoose);
					btnList.Add(goCChoose);
					btnList.Add(goDChoose);
					btnList.Add(goTiJiaoDaAn);
					goBack.GetComponent<NormalBtn>().index = 0;
					goLastStep.GetComponent<NormalBtn>().index = 1;
					goNextStep.GetComponent<NormalBtn>().index = 2;
					goAChoose.GetComponent<NormalBtn>().index = 3;
					goBChoose.GetComponent<NormalBtn>().index = 4;
					goCChoose.GetComponent<NormalBtn>().index = 5;
					goDChoose.GetComponent<NormalBtn>().index = 6;
					goTiJiaoDaAn.GetComponent<NormalBtn>().index = 7;
					goHandInBtn.GetComponent<NormalBtn>().index = 8;
				}
				else
				{
					goTiJiaoDaAn.SetActive(value: false);
					btnList.Clear();
					btnDic.Clear();
					viewBtnCount = 0;
					btnList.Add(goBack);
					btnList.Add(goLastStep);
					btnList.Add(goNextStep);
					btnList.Add(goHandInBtn);
					btnList.Add(goAChoose);
					btnList.Add(goBChoose);
					btnList.Add(goCChoose);
					btnList.Add(goDChoose);
					goBack.GetComponent<NormalBtn>().index = 0;
					goLastStep.GetComponent<NormalBtn>().index = 1;
					goNextStep.GetComponent<NormalBtn>().index = 2;
					goAChoose.GetComponent<NormalBtn>().index = 3;
					goBChoose.GetComponent<NormalBtn>().index = 4;
					goCChoose.GetComponent<NormalBtn>().index = 5;
					goDChoose.GetComponent<NormalBtn>().index = 6;
					goHandInBtn.GetComponent<NormalBtn>().index = 7;
				}
			}
			else if (!mQuestions[num].ifAnswer)
			{
				goTiJiaoDaAn.SetActive(value: true);
				btnList.Clear();
				btnDic.Clear();
				viewBtnCount = 0;
				btnList.Add(goBack);
				btnList.Add(goLastStep);
				btnList.Add(goNextStep);
				btnList.Add(goChongZhi);
				btnList.Add(goAChoose);
				btnList.Add(goBChoose);
				btnList.Add(goCChoose);
				btnList.Add(goDChoose);
				btnList.Add(goTiJiaoDaAn);
				goBack.GetComponent<NormalBtn>().index = 0;
				goLastStep.GetComponent<NormalBtn>().index = 1;
				goNextStep.GetComponent<NormalBtn>().index = 2;
				goAChoose.GetComponent<NormalBtn>().index = 3;
				goBChoose.GetComponent<NormalBtn>().index = 4;
				goCChoose.GetComponent<NormalBtn>().index = 5;
				goDChoose.GetComponent<NormalBtn>().index = 6;
				goTiJiaoDaAn.GetComponent<NormalBtn>().index = 7;
				goChongZhi.GetComponent<NormalBtn>().index = 8;
			}
			else
			{
				goTiJiaoDaAn.SetActive(value: false);
				btnList.Clear();
				btnDic.Clear();
				viewBtnCount = 0;
				btnList.Add(goBack);
				btnList.Add(goLastStep);
				btnList.Add(goNextStep);
				btnList.Add(goChongZhi);
				btnList.Add(goAChoose);
				btnList.Add(goBChoose);
				btnList.Add(goCChoose);
				btnList.Add(goDChoose);
				goBack.GetComponent<NormalBtn>().index = 0;
				goLastStep.GetComponent<NormalBtn>().index = 1;
				goNextStep.GetComponent<NormalBtn>().index = 2;
				goAChoose.GetComponent<NormalBtn>().index = 3;
				goBChoose.GetComponent<NormalBtn>().index = 4;
				goCChoose.GetComponent<NormalBtn>().index = 5;
				goDChoose.GetComponent<NormalBtn>().index = 6;
				goChongZhi.GetComponent<NormalBtn>().index = 7;
			}
		}
		base.ClearBtnFangFa();
		base.Awake();
	}

	private void ChooseClick(bool ifClick, GameObject go)
	{
		ifClick = !ifClick;
		switch (ifClick)
		{
		case true:
			go.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			break;
		case false:
			go.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			break;
		}
		switch (go.name)
		{
		case "AChoose":
			ifClick_A = ifClick;
			break;
		case "BChoose":
			ifClick_B = ifClick;
			break;
		case "CChoose":
			ifClick_C = ifClick;
			break;
		case "DChoose":
			ifClick_D = ifClick;
			break;
		}
		Debug.Log(go.name + ifClick);
	}

	private void JudgeAnswer(byte answer, GameObject go)
	{
		if (mQuestions[num].type == 2 || mQuestions[num].type == 1)
		{
			mQuestions[num].ifAnswer = true;
			if (answer == mQuestions[num].rightAnswer)
			{
				recordSelectRightNum++;
				mQuestions[num].answerRight = true;
			}
			else
			{
				recordSelectWrongNum++;
				mQuestions[num].answerRight = false;
				AddCuoTi(mQuestions[num].id);
			}
		}
		if (mQuestions[num].type == 3 && go == goTiJiaoDaAn.gameObject)
		{
			sumClick = 0;
			mQuestions[num].ifAnswer = true;
			ButtonPaiLie();
			if (answer == mQuestions[num].rightAnswer)
			{
				recordSelectRightNum++;
				mQuestions[num].answerRight = true;
			}
			else
			{
				recordSelectWrongNum++;
				mQuestions[num].answerRight = false;
				AddCuoTi(mQuestions[num].id);
			}
		}
		if (mQuestions[num].type == 2 || mQuestions[num].type == 1 || (mQuestions[num].type == 3 && go == goTiJiaoDaAn.gameObject))
		{
			showAnswerExplain.gameObject.SetActive(value: true);
			ShowAnswerExplain();
			mQuestions[num].ifAnswer = true;
			mQuestions[num].RecordSelectAnswer = answer;
		}
	}

	private string TurnAnswerToZiMu(byte answer)
	{
		string result = "";
		switch (answer)
		{
		case 1:
			result = "A";
			break;
		case 2:
			result = "B";
			break;
		case 3:
			result = "AB";
			break;
		case 4:
			result = "C";
			break;
		case 5:
			result = "AC";
			break;
		case 6:
			result = "BC";
			break;
		case 7:
			result = "ABC";
			break;
		case 8:
			result = "D";
			break;
		case 9:
			result = "AD";
			break;
		case 10:
			result = "BD";
			break;
		case 11:
			result = "ABD";
			break;
		case 12:
			result = "CD";
			break;
		case 13:
			result = "ACD";
			break;
		case 14:
			result = "BCD";
			break;
		case 15:
			result = "ABCD";
			break;
		}
		return result;
	}

	private void ShowAnswerExplain()
	{
		showAnswerExplain.text = mQuestions[num].details;
	}

	private void SetImage()
	{
		if (mQuestions[num].hasImage)
		{
			string path = "LiLun/images/" + mQuestions[num].id.ToString("0000");
			if (Resources.Load(path, typeof(Sprite)) as Sprite == null)
			{
				ShowImage.gameObject.SetActive(value: false);
				ShowRawImage.gameObject.SetActive(value: true);
				ShowRawImage.transform.GetChild(0).GetComponent<VideoPlayer>().clip = Resources.Load(path) as VideoClip;
				ShowRawImage.transform.GetChild(0).GetComponent<VideoPlayer>().isLooping = true;
			}
			else
			{
				ShowImage.gameObject.SetActive(value: true);
				ShowRawImage.gameObject.SetActive(value: false);
				ShowImage.overrideSprite = Resources.Load(path, typeof(Sprite)) as Sprite;
			}
		}
		else
		{
			ShowImage.gameObject.SetActive(value: false);
			ShowRawImage.gameObject.SetActive(value: false);
		}
	}

	private void SetContent()
	{
		if (mQuestions[num].type == 2)
		{
			goCChoose.SetActive(value: true);
			goDChoose.SetActive(value: true);
			goTiJiaoDaAn.SetActive(value: false);
			numOfQuestion.text = num + 1 + ".";
			questionShow.text = mQuestions[num].text;
			A_Answer.text = mQuestions[num].answers[0];
			B_Answer.text = mQuestions[num].answers[1];
			C_Answer.text = mQuestions[num].answers[2];
			D_Answer.text = mQuestions[num].answers[3];
		}
		else if (mQuestions[num].type == 1)
		{
			goCChoose.SetActive(value: false);
			goDChoose.SetActive(value: false);
			goTiJiaoDaAn.SetActive(value: false);
			numOfQuestion.text = num + 1 + ".";
			questionShow.text = mQuestions[num].text;
			A_Answer.text = "正确";
			B_Answer.text = "错误";
		}
		else if (mQuestions[num].type == 3)
		{
			goCChoose.SetActive(value: true);
			goDChoose.SetActive(value: true);
			numOfQuestion.text = num + 1 + ".";
			questionShow.text = mQuestions[num].text;
			A_Answer.text = mQuestions[num].answers[0];
			B_Answer.text = mQuestions[num].answers[1];
			C_Answer.text = mQuestions[num].answers[2];
			D_Answer.text = mQuestions[num].answers[3];
		}
		SetImage();
		ShowNumbers();
	}

	private void RefreshLastAndNext()
	{
		ifClick_A = false;
		ifClick_B = false;
		ifClick_C = false;
		ifClick_D = false;
		sumClick = 0;
		if (!mQuestions[num].ifAnswer)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
		}
		else if (mQuestions[num].RecordSelectAnswer == 1)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
		}
		else if (mQuestions[num].RecordSelectAnswer == 2)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
		}
		else if (mQuestions[num].RecordSelectAnswer == 3)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
		}
		else if (mQuestions[num].RecordSelectAnswer == 4)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
		}
		else if (mQuestions[num].RecordSelectAnswer == 5)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
		}
		else if (mQuestions[num].RecordSelectAnswer == 6)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
		}
		else if (mQuestions[num].RecordSelectAnswer == 7)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
		}
		else if (mQuestions[num].RecordSelectAnswer == 8)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
		}
		else if (mQuestions[num].RecordSelectAnswer == 9)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
		}
		else if (mQuestions[num].RecordSelectAnswer == 10)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
		}
		else if (mQuestions[num].RecordSelectAnswer == 11)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
		}
		else if (mQuestions[num].RecordSelectAnswer == 12)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
		}
		else if (mQuestions[num].RecordSelectAnswer == 13)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
		}
		else if (mQuestions[num].RecordSelectAnswer == 14)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: false);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
		}
		else if (mQuestions[num].RecordSelectAnswer == 15)
		{
			goAChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goBChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goCChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			goDChoose.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
		}
	}

	private void WriteQuestion(string filePath, List<QuestionBody> qustion)
	{
		FileStream fileStream = new FileInfo(filePath).OpenWrite();
		new BinaryFormatter().Serialize(fileStream, qustion);
		fileStream.Close();
	}

	private void AddCuoTi(int id)
	{
		if (mErrorId == null)
		{
			mErrorId = new List<int>();
		}
		else if (!mErrorId.Contains(id))
		{
			mErrorId.Add(id);
		}
	}

	public string GetCuoTiName()
	{
		if (ViewJumpCtl.Instance.trainType == TrainType.CourseOne)
		{
			return Application.streamingAssetsPath + "/KeMuYi/KeMuYi_CuoTiLianXi/KeMuYi_CuoTiLianXi";
		}
		return Application.streamingAssetsPath + "/KeMuSi/KeMuSi_CuoTiLianXi/KeMuSi_CuoTiLianXi";
	}

	public string GetTagCuoTiName()
	{
		if (ViewJumpCtl.Instance.trainType == TrainType.CourseOne)
		{
			return Application.persistentDataPath + "/KeMuYi/KeMuYi_CuoTiLianXi";
		}
		return Application.persistentDataPath + "/KeMuSi/KeMuSi_CuoTiLianXi";
	}

	private void OnDestroy()
	{
		if (mErrorId != null && mErrorId.Count != 0)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				FileStream fileStream = new FileInfo(GetTagCuoTiName()).OpenWrite();
				new BinaryFormatter().Serialize(fileStream, mErrorId);
				fileStream.Close();
			}
			else
			{
				FileStream fileStream2 = new FileInfo(GetCuoTiName()).OpenWrite();
				new BinaryFormatter().Serialize(fileStream2, mErrorId);
				fileStream2.Close();
			}
		}
	}
}
