using System.Collections;
using Global;
using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class GuideTipsViewCtl : MonoBehaviour
	{
		public static GuideTipsViewCtl Instance;

		public Text posText;

		public Transform operateList;

		public Scrollbar bar;

		public Transform actionAnimatiion;

		public GameObject OperateTitle;

		private Coroutine cor;

		private GuideStepInfo curShowGuideInfo;

		private int curActionIndex;

		private const int TipsCount = 27;

		private const int OnePageTipsCount = 6;

		private float oneTipsBarValue;

		private int endCount;

		private float curValue = 1f;

		private ActionAnimation penalAnimation;

		private ActionAnimation steerWheelAnimation;

		private ActionAnimation gearAnimation;

		private ActionAnimation handbreakAnimation;

		private ActionAnimation yuGuaAnimation;

		private ActionAnimation curAnimation;

		private float time;

		private void Awake()
		{
			Instance = this;
			oneTipsBarValue = 1f / 21f;
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
			{
				OperateTitle.SetActive(value: false);
			}
			if (ViewJumpCtl.Instance.trainType == TrainType.NightLight)
			{
				OperateTitle.SetActive(value: false);
			}
		}

		private void Start()
		{
			base.transform.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0.5f);
			base.transform.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0.5f);
			if (DataCenter.Instance.UsbVersion == 64 || DataCenter.Instance.UsbVersion == 96 || DataCenter.Instance.UsbVersion == 128)
			{
				if (Screen.width / Screen.height >= 2)
				{
					base.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-730f, 0f, 0f);
				}
				else if (Screen.width / Screen.height < 2)
				{
					base.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-550f, 0f, 0f);
				}
			}
			else
			{
				base.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-550f, 0f, 0f);
			}
			base.transform.localPosition -= new Vector3(0f, 20f, 20f);
		}

		public void InitGuideTipsView(GuideStepInfo guideInfo, bool selfJudge = false)
		{
			Reset();
			posText.text = guideInfo.titleText;
			for (int i = 0; i < guideInfo.actionList.Count; i++)
			{
				operateList.GetChild(i).GetComponent<OperateCtl>().InitOperateShow(guideInfo.actionList[i], i, selfJudge);
			}
			curShowGuideInfo = guideInfo;
			cor = StartCoroutine(ScrollBarLoop());
		}

		public void StartCurActionJudge()
		{
			if (curActionIndex < curShowGuideInfo.actionList.Count)
			{
				operateList.GetChild(curActionIndex).GetComponent<OperateCtl>().StartSelfJudge();
				_ = curShowGuideInfo.actionList[curActionIndex].isAction;
				curActionIndex++;
			}
		}

		public void SetOperateDone()
		{
			if (curActionIndex < 27)
			{
				operateList.GetChild(curActionIndex).Find("True").gameObject.SetActive(value: true);
				curActionIndex++;
				if (curActionIndex == curShowGuideInfo.actionList.Count)
				{
					Object.Destroy(base.gameObject, 3f);
				}
			}
			if (curActionIndex > 3 && curShowGuideInfo.actionList.Count - curActionIndex >= 3)
			{
				curValue = 1f - (float)(curActionIndex - 3) * oneTipsBarValue;
			}
		}

		public void SetFillAmount(float value)
		{
			if (curActionIndex < 27)
			{
				operateList.GetChild(curActionIndex).GetComponent<OperateCtl>().SetFillAmount(value);
			}
		}

		public void SetJudgeEnd()
		{
			endCount++;
			if (endCount > 3 && curShowGuideInfo.actionList.Count - endCount >= 3)
			{
				curValue = 1f - (float)(endCount - 3) * oneTipsBarValue;
			}
			if (endCount == curShowGuideInfo.actionList.Count)
			{
				switch (ViewJumpCtl.Instance.trainType)
				{
				case TrainType.CourseTwo:
				case TrainType.NanChangKaoChang:
					Object.Destroy(base.gameObject, 2f);
					break;
				case TrainType.CourseThree:
					Object.Destroy(base.gameObject, 3f);
					break;
				}
			}
			switch (ViewJumpCtl.Instance.trainType)
			{
			case TrainType.CourseTwo:
			case TrainType.NanChangKaoChang:
				if (curAnimation != null)
				{
					curAnimation.StopAnimation();
				}
				CourseTwoDriveGuide.Instance.JudgeNextAction();
				break;
			case TrainType.BasicTrain:
				if (curAnimation != null)
				{
					curAnimation.StopAnimation();
				}
				BasicTrainDriveGuide.Instance.JudgeNextAction();
				break;
			}
		}

		private void InitActionAnimationGo()
		{
			penalAnimation = actionAnimatiion.GetChild(0).GetComponent<ActionAnimation>();
			steerWheelAnimation = actionAnimatiion.GetChild(1).GetComponent<ActionAnimation>();
			gearAnimation = actionAnimatiion.GetChild(2).GetComponent<ActionAnimation>();
			handbreakAnimation = actionAnimatiion.GetChild(3).GetComponent<ActionAnimation>();
			yuGuaAnimation = actionAnimatiion.GetChild(4).GetComponent<ActionAnimation>();
			for (int i = 0; i < actionAnimatiion.childCount; i++)
			{
				actionAnimatiion.GetChild(i).gameObject.SetActive(value: false);
			}
		}

		private void PlayActionAnimation(string content)
		{
			if (curAnimation != null)
			{
				curAnimation.gameObject.SetActive(value: false);
			}
			curAnimation = null;
			int actionId = CarActionJudge.Instance.GetActionId(content);
			switch (actionId)
			{
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 23:
			case 24:
			case 25:
			case 26:
			case 41:
			case 68:
			case 111:
			case 112:
				curAnimation = penalAnimation;
				break;
			case 13:
			case 14:
			case 15:
			case 16:
			case 17:
			case 30:
			case 31:
			case 32:
			case 33:
			case 44:
			case 45:
				curAnimation = steerWheelAnimation;
				break;
			case 27:
			case 29:
				curAnimation = handbreakAnimation;
				break;
			case 54:
			case 55:
				curAnimation = yuGuaAnimation;
				break;
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
				curAnimation = gearAnimation;
				break;
			}
			if (curAnimation != null)
			{
				curAnimation.gameObject.SetActive(value: true);
				curAnimation.PlayAnimation(actionId);
			}
		}

		private void Reset()
		{
			curActionIndex = 0;
			bar.value = 1f;
			curValue = 1f;
			time = 0f;
			endCount = 0;
			for (int i = 0; i < 27; i++)
			{
				operateList.GetChild(i).GetComponent<OperateCtl>().Reset();
			}
			if (cor != null)
			{
				StopCoroutine(cor);
			}
		}

		private IEnumerator ScrollBarLoop()
		{
			bar.value = 0.99f;
			curValue = 1f;
			time = 0f;
			while (bar.value > 0f)
			{
				if (bar.value > curValue)
				{
					time += Time.deltaTime * 0.1f;
				}
				else
				{
					bar.value = curValue;
				}
				bar.value = Mathf.Lerp(1f, 0f, time);
				yield return 0;
			}
		}

		public void DestroyThis(float time = 0f)
		{
			if (time > 0f)
			{
				Object.Destroy(base.gameObject, time);
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}

		private void OnDestroy()
		{
			if (cor != null)
			{
				StopCoroutine(cor);
			}
		}
	}
}
