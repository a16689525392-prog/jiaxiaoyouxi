using System.Collections;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Scene
{
	public class OperateCtl : MonoBehaviour
	{
		private bool isJudgeEnd;

		public ActionInfo curActionInfo;

		private bool selfJudge;

		private Text content;

		private Image trueImage;

		private Image falseImage;

		private Image fillImage;

		private int startIndex = -1;

		private bool isStartJudge;

		private bool isWheelJudge;

		private bool isSendEndMsg;

		private Coroutine JudgeCor;

		private float targetWheelAngle;

		private float curDuringTime;

		private bool isWheelPause;

		private void Awake()
		{
			content = base.transform.Find("Content").GetComponent<Text>();
			trueImage = base.transform.Find("True").GetComponent<Image>();
			falseImage = base.transform.Find("False").GetComponent<Image>();
			fillImage = base.transform.Find("FillImage").GetComponent<Image>();
			fillImage.fillAmount = 0f;
			trueImage.gameObject.SetActive(value: false);
			falseImage.gameObject.SetActive(value: false);
		}

		public void InitOperateShow(ActionInfo info, int i, bool selfJudge = false)
		{
			Reset();
			this.selfJudge = selfJudge;
			curActionInfo = info;
			content.text = curActionInfo.content;
		}

		public void Reset()
		{
			isJudgeEnd = false;
			trueImage.gameObject.SetActive(value: false);
			falseImage.gameObject.SetActive(value: false);
			fillImage.fillAmount = 0f;
			isStartJudge = false;
			content.text = "";
			selfJudge = false;
			curActionInfo = null;
			isWheelJudge = false;
			isSendEndMsg = false;
		}

		public void StartSelfJudge()
		{
			if (!selfJudge)
			{
				selfJudge = true;
			}
		}

		private void Update()
		{
			if (selfJudge && !isJudgeEnd && curActionInfo != null && (ViewJumpCtl.Instance.trainType == TrainType.BasicTrain || ViewJumpCtl.Instance.trainType == TrainType.NightLight || CarDriveHistoryPlay.Instance.CurPlayIndex > curActionInfo.startIndex) && !isWheelJudge)
			{
				PlayContentSound();
				JudgeActionDone();
			}
			if (isSendEndMsg && !isJudgeEnd)
			{
				isSendEndMsg = false;
			}
			if (!isSendEndMsg && isJudgeEnd)
			{
				GuideTipsViewCtl.Instance.SetJudgeEnd();
				SoundPlayCtl.Instance.RemoveSound(content.text);
				isSendEndMsg = true;
			}
			if (isWheelPause && CarActionJudge.Instance.JudgeActionDone(curActionInfo.content))
			{
				CarDriveHistoryPlay.Instance.Play();
				isWheelPause = false;
			}
		}

		private void PlayContentSound()
		{
			if (!isStartJudge)
			{
				isStartJudge = true;
				if (curActionInfo.isAction)
				{
					SoundPlayCtl.Instance.PlaySound(content.text, 0f, 3);
				}
				else
				{
					SoundPlayCtl.Instance.PlaySound(content.text, 0f, 4);
				}
			}
		}

		private void JudgeActionDone()
		{
			if (!curActionInfo.isAction)
			{
				isJudgeEnd = true;
				trueImage.gameObject.SetActive(value: true);
			}
			else if (curActionInfo.duringIndex > 0)
			{
				DuringIndexJudge();
			}
			else if (curActionInfo.duringTime > 0f)
			{
				if (CarActionJudge.Instance.JudgeActionDone(curActionInfo.content))
				{
					curDuringTime += Time.deltaTime;
					if (curDuringTime > curActionInfo.duringTime)
					{
						isJudgeEnd = true;
						trueImage.gameObject.SetActive(value: true);
					}
					fillImage.fillAmount = curDuringTime / curActionInfo.duringTime;
				}
				else
				{
					curDuringTime = 0f;
					fillImage.fillAmount = 0f;
				}
			}
			else if (ViewJumpCtl.Instance.trainType == TrainType.NightLight || ViewJumpCtl.Instance.trainType == TrainType.BasicTrain || CarDriveHistoryPlay.Instance.CurPlayIndex < curActionInfo.endIndex)
			{
				if (CarActionJudge.Instance.JudgeActionDone(curActionInfo.content))
				{
					isJudgeEnd = true;
					trueImage.gameObject.SetActive(value: true);
				}
			}
			else if (CarDriveHistoryPlay.Instance.CurPlayIndex > curActionInfo.endIndex)
			{
				isJudgeEnd = true;
				falseImage.gameObject.SetActive(value: true);
			}
		}

		private void DuringIndexJudge()
		{
			if (startIndex == -1 && CarActionJudge.Instance.JudgeActionDone(curActionInfo.content))
			{
				startIndex = CarDriveHistoryPlay.Instance.CurPlayIndex;
			}
			else if (startIndex != -1 && !CarActionJudge.Instance.JudgeActionDone(curActionInfo.content))
			{
				startIndex = -1;
			}
			if (startIndex != -1 && CarDriveHistoryPlay.Instance.CurPlayIndex - startIndex > curActionInfo.duringIndex)
			{
				isJudgeEnd = true;
				trueImage.gameObject.SetActive(value: true);
			}
			else if (CarDriveHistoryPlay.Instance.CurPlayIndex > curActionInfo.endIndex)
			{
				isJudgeEnd = true;
				falseImage.gameObject.SetActive(value: true);
			}
			if (startIndex != -1)
			{
				fillImage.fillAmount = (float)(CarDriveHistoryPlay.Instance.CurPlayIndex - startIndex) / (float)curActionInfo.duringIndex;
			}
			else
			{
				fillImage.fillAmount = 0f;
			}
		}

		private IEnumerator JudgeWheelOver(float time)
		{
			yield return new WaitForSeconds(time);
			float curSteerWheelScale = GetCurSteerWheelScale(0.8f, 1.2f);
			CourseTwoDriveGuide.Instance.CurSteerWheelAngle = targetWheelAngle;
			if (curSteerWheelScale != 1f && curSteerWheelScale != 0f)
			{
				trueImage.gameObject.SetActive(value: true);
			}
			else
			{
				falseImage.gameObject.SetActive(value: false);
				CarDriveHistoryPlay.Instance.Pause();
				isWheelPause = true;
			}
			isJudgeEnd = true;
			JudgeCor = null;
		}

		private float GetCurSteerWheelScale(float min, float max)
		{
			float a = MyLerp(CourseTwoDriveGuide.Instance.CurSteerWheelAngle, targetWheelAngle, min);
			float b = MyLerp(CourseTwoDriveGuide.Instance.CurSteerWheelAngle, targetWheelAngle, max);
			return Mathf.InverseLerp(a, b, CarState.Instance.CarSignal.steerWheel);
		}

		private float MyLerp(float min, float max, float value)
		{
			return min + (max - min) * value;
		}

		public void SetFillAmount(float value)
		{
			fillImage.gameObject.SetActive(value: true);
			value = Mathf.Clamp(value, 0f, 1f);
			fillImage.fillAmount = value;
		}

		private void OnDestroy()
		{
			if (JudgeCor != null)
			{
				CourseTwoDriveGuide.Instance.CurSteerWheelAngle = targetWheelAngle;
				StopCoroutine(JudgeCor);
			}
		}
	}
}
