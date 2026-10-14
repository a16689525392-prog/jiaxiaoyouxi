using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Car;
using Global;
using Scene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SoundAndTextShow
{
	public class SceneTextShow : MonoBehaviour
	{
		public Text titleText;

		public GameObject normalShowTextGroup;

		public GameObject QRCodeImage;

		public Text scoreText;

		public Text fpsText;

		public Image Bg;

		public Image FrameNumberImage;

		public float FrameNumber;

		private float FrameTime;

		private float Frame;

		public bool IsFrameChange;

		private bool isPrintErrorData;

		public bool isCollectionData;

		public static int VideoNumber;

		private int PrintI;

		private float savedatatime;

		private int frameCount;

		private float timeCount;

		private int preFps;

		private Coroutine titleCor;

		private float normalTime = 2f;

		private float showNormalTextIntervalTime = 0.5f;

		private Queue<string> normalTextQueue = new Queue<string>();

		public int ShowTextCount;

		public static SceneTextShow Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
			SceneJumpCtl instance = SceneJumpCtl.Instance;
			instance.EnterSceneAction = (UnityAction<SceneType>)Delegate.Combine(instance.EnterSceneAction, new UnityAction<SceneType>(JudgeShowScoreText));
			isCollectionData = SettingInfoCtl.Instance.isSaveData;
			InquireAndDelete();
			SavetoDataFile("开始采集");
		}

		private void Start()
		{
			titleText.text = "";
			Frame = -1f;
			IsFrameChange = true;
			isPrintErrorData = false;
			PrintI = 0;
		}

		private void InquireAndDelete()
		{
			if (!SettingInfoCtl.Instance.isSaveData)
			{
				return;
			}
			if (File.Exists(Application.streamingAssetsPath + "/DataCollectionLog/"))
			{
				FileInfo[] files = new DirectoryInfo(Application.streamingAssetsPath + "/DataCollectionLog/").GetFiles("*", SearchOption.AllDirectories);
				if (files.Length >= 4000)
				{
					for (int i = 0; i < 600; i++)
					{
						File.Delete(Application.streamingAssetsPath + "/DataCollectionLog/" + files[i].Name.ToString());
					}
				}
			}
			if (!File.Exists(Application.streamingAssetsPath + "/ErrorLog/"))
			{
				return;
			}
			FileInfo[] files2 = new DirectoryInfo(Application.streamingAssetsPath + "/ErrorLog/").GetFiles("*", SearchOption.AllDirectories);
			if (files2.Length >= 1000)
			{
				for (int j = 0; j < 400; j++)
				{
					File.Delete(Application.streamingAssetsPath + "/ErrorLog/" + files2[j].Name.ToString());
				}
			}
		}

		public void SavetoDataFile(string state, bool savedata = false)
		{
			if (SettingInfoCtl.Instance.isSaveData || savedata)
			{
				string[] contents = new string[1] { state + ":" + DateTime.Now.ToString("yyyy年MM月dd日-HH:mm:ss") };
				if (!File.Exists(Application.streamingAssetsPath + "/DataCollectionLog/"))
				{
					Directory.CreateDirectory(Application.streamingAssetsPath + "/DataCollectionLog/");
				}
				File.WriteAllLines(Application.streamingAssetsPath + "/DataCollectionLog/" + DateTime.Now.ToString("yyyyMMddHHmmss") + state + ".txt", contents);
			}
		}

		private void Update()
		{
			if (scoreText.gameObject.activeSelf)
			{
				QRCodeImage.SetActive(value: true);
			}
			else
			{
				QRCodeImage.SetActive(value: false);
			}
			savedatatime += Time.deltaTime;
			if (savedatatime >= 300f && IsFrameChange)
			{
				SavetoDataFile("正常采集");
				savedatatime = 0f;
			}
			if (GetComponent<SignalSourceCtl>() != null)
			{
				if (!GetComponent<SignalSourceCtl>().cbSignalReader.isDescryptOver)
				{
					FrameNumberImage.color = Color.yellow;
				}
				else
				{
					FrameNumberImage.color = Color.green;
				}
			}
			FrameNumberImage.transform.localEulerAngles = new Vector3(0f, 0f, (0f - FrameNumber) % 360f * 3f);
			if (SceneManager.GetActiveScene().name == SceneType.Loading.ToString())
			{
				Bg.sprite = Resources.Load("LoadingPage", typeof(Sprite)) as Sprite;
			}
			else
			{
				Bg.sprite = Resources.Load("Bg1", typeof(Sprite)) as Sprite;
			}
			if (SceneJumpCtl.Instance.CurSceneType != SceneType.MainScene && SceneManager.GetActiveScene().name != SceneType.Loading.ToString())
			{
				scoreText.gameObject.SetActive(value: true);
			}
			scoreText.text = DataCenter.Instance.score.ToString();
			showNormalTextIntervalTime += Time.deltaTime;
			if (showNormalTextIntervalTime > 0.5f && normalTextQueue.Count > 0)
			{
				ShowNormalText(normalTextQueue.Dequeue());
			}
			fpsText.gameObject.SetActive(value: false);
		}

		private void ShowFpsText()
		{
			fpsText.gameObject.SetActive(value: true);
			timeCount += Time.deltaTime;
			frameCount++;
			if (timeCount > 1f)
			{
				if (preFps == 0)
				{
					fpsText.text = frameCount.ToString();
				}
				else
				{
					fpsText.text = Mathf.FloorToInt((float)frameCount * 0.7f + (float)preFps * 0.3f).ToString();
				}
				preFps = frameCount;
				timeCount = 0f;
				frameCount = 0;
			}
		}

		private void JudgeShowScoreText(SceneType sceneType)
		{
			switch (sceneType)
			{
			case SceneType.CourseThree:
			case SceneType.HuNanYZCourseThree:
			case SceneType.RenCaiCourseThree:
			case SceneType.ChengDongCourseThree:
			case SceneType.BaiYunCourseThree:
			case SceneType.CourseTwo:
			case SceneType.WenZhouCourseTwo:
			case SceneType.HeBeiCourseTwo:
			case SceneType.QingHuCourseTwo:
			case SceneType.HuNanYZCourseTwo:
			case SceneType.ShiJiaZhuangCourseTwo:
			case SceneType.DFCourseTwo:
			case SceneType.LightExam:
			case SceneType.HaiJing:
			case SceneType.XueJing:
			case SceneType.ShanQu:
			case SceneType.XiangCun:
			case SceneType.TurnDrive:
			case SceneType.City:
			case SceneType.HighRoad:
			case SceneType.ShenZhen:
			case SceneType.YingjiXunlian:
			case SceneType.ChengDong:
			case SceneType.TaiZhouChangTan:
				scoreText.gameObject.SetActive(value: true);
				scoreText.text = DataCenter.Instance.score.ToString();
				break;
			default:
				scoreText.gameObject.SetActive(value: false);
				break;
			}
			if (sceneType == SceneType.MainScene)
			{
				DataCenter.Instance.score = 100f;
			}
		}

		public void ShowNormalText(string content, float time = 0f)
		{
			if (content == "")
			{
				return;
			}
			if (showNormalTextIntervalTime > 0.5f)
			{
				for (int i = 0; i < normalShowTextGroup.transform.childCount; i++)
				{
					if (!normalShowTextGroup.transform.GetChild(i).gameObject.activeSelf)
					{
						normalShowTextGroup.transform.GetChild(i).gameObject.SetActive(value: true);
						normalShowTextGroup.transform.GetChild(i).GetComponent<NormalText>().ShowText(content);
						normalShowTextGroup.transform.GetChild(i).SetAsLastSibling();
						ShowTextCount++;
						break;
					}
				}
				showNormalTextIntervalTime = 0f;
			}
			else
			{
				normalTextQueue.Enqueue(content);
			}
		}

		public void SetTextDisable(GameObject go)
		{
			go.SetActive(value: false);
			if (ShowTextCount > 0)
			{
				ShowTextCount--;
			}
		}

		public void BackToMainScene()
		{
			Instance.ShowTitleText("");
			ResetNormalText();
			scoreText.gameObject.SetActive(value: false);
		}

		public void ShowTitleText(string content, float time = 99f)
		{
			titleText.text = content;
			if (time == 0f)
			{
				time = normalTime + (float)content.Length * 0.2f;
			}
			if (titleCor != null)
			{
				StopCoroutine(titleCor);
			}
			titleCor = StartCoroutine(ResetTitleText(time));
		}

		private IEnumerator ResetNormalText(float time)
		{
			yield return new WaitForSeconds(time);
			ResetNormalText();
		}

		private IEnumerator ResetTitleText(float time)
		{
			yield return new WaitForSeconds(time);
			ResetTitleText();
		}

		public void ResetNormalText()
		{
			for (int i = 0; i < normalShowTextGroup.transform.childCount; i++)
			{
				normalShowTextGroup.transform.GetChild(i).gameObject.SetActive(value: false);
			}
		}

		public void ResetTitleText()
		{
			titleText.text = "";
			if (titleCor != null)
			{
				StopCoroutine(titleCor);
			}
		}

		public void OnDestroy()
		{
			SavetoDataFile("结束采集");
			StopAllCoroutines();
		}
	}
}
