using Car;
using Global;
using Scene;
using SoundAndTextShow;
using UnityEngine;
using View;

public class VipCourseThreeKaoShi : MonoBehaviour
{
	public static VipCourseThreeKaoShi Instance;

	private GameObject TriggerArea;

	public bool isLastItem;

	public bool isOverTest;

	private NightLightCtl curItemCtl;

	public bool NightLightIsOver;

	private int itemStep;

	private GameObject countDownPrefabs;

	private float countdowntime;

	private int Startindex;

	public void ChongZhi()
	{
		SoundPlayCtl.Instance.ClearClip();
		if (CarState.Instance.CarSignal.gear == 0 || CarState.Instance.CarSignal.gear == 104 || CarState.Instance.CarSignal.gear == 101)
		{
			CarState.Instance.FireOn = true;
		}
		if (curItemCtl != null)
		{
			Object.Destroy(curItemCtl);
		}
		if (countDownPrefabs != null)
		{
			Object.Destroy(countDownPrefabs);
		}
		isOverTest = false;
		isLastItem = false;
		SoundPlayCtl.Instance.PlaySound("正在申请视频认证，请等待");
		TriggerArea.gameObject.SetActive(value: true);
		itemStep = 0;
		countdowntime = 0f;
		Startindex = 0;
		NightLightIsOver = false;
		itemStep = 3;
	}

	private void Start()
	{
		Instance = this;
		if (CarState.Instance.CarSignal.gear == 0 || CarState.Instance.CarSignal.gear == 104 || CarState.Instance.CarSignal.gear == 101)
		{
			CarState.Instance.FireOn = true;
		}
		TriggerArea = GameObject.Find("TriggerArea");
		isOverTest = false;
		isLastItem = false;
		SoundPlayCtl.Instance.PlaySound("正在申请视频认证，请等待");
		itemStep = 0;
		countdowntime = 0f;
		Startindex = 0;
		itemStep = 3;
	}

	private void Update()
	{
		if (SceneMenuView.KaoShiReset)
		{
			ChongZhi();
			SceneMenuView.KaoShiReset = false;
		}
		if (itemStep == 0)
		{
			if (!SoundPlayCtl.Instance.IsPlaying())
			{
				countDownPrefabs = ViewJumpCtl.Instance.CreateViewPrefab("CountDown", Vector3.zero);
				countDownPrefabs.GetComponent<CountDownCtl>().SetCountDownTime(6);
				itemStep++;
			}
		}
		else if (itemStep == 1)
		{
			countdowntime += Time.deltaTime;
			if (countdowntime > 6f && Startindex == 0)
			{
				switch (SceneJumpCtl.Instance.SceneWay)
				{
				case 1:
					SoundPlayCtl.Instance.PlaySound("本次考试开始，请走一号考试线路");
					break;
				case 2:
					SoundPlayCtl.Instance.PlaySound("本次考试开始，请走二号考试线路");
					break;
				case 3:
					SoundPlayCtl.Instance.PlaySound("本次考试开始，请走三号考试线路");
					break;
				}
				Startindex++;
			}
			else if (countdowntime > 11f && Startindex == 1)
			{
				SoundPlayCtl.Instance.PlaySound("请开始上车准备");
				Startindex++;
			}
			else if (countdowntime > 16f && Startindex == 2)
			{
				SoundPlayCtl.Instance.PlaySound("上车1");
				Startindex++;
			}
			else if (countdowntime > 20f && Startindex == 3)
			{
				SoundPlayCtl.Instance.PlaySound("上车2");
				Startindex++;
			}
			if (countdowntime > 22f)
			{
				itemStep++;
				countdowntime = 0f;
				Object.Destroy(countDownPrefabs);
			}
		}
		else if (itemStep == 2)
		{
			if (curItemCtl == null && !NightLightIsOver)
			{
				curItemCtl = base.gameObject.AddComponent<NightLightCtl>();
			}
			if (NightLightIsOver && curItemCtl != null)
			{
				Object.Destroy(curItemCtl);
				if (countDownPrefabs != null)
				{
					Object.Destroy(countDownPrefabs);
				}
				itemStep++;
			}
		}
		if (DataCenter.Instance.score >= 90f && isLastItem && !isOverTest)
		{
			SoundPlayCtl.Instance.PlaySound("考试合格，科三考试结束");
			isOverTest = true;
		}
		if (DataCenter.Instance.score < 90f && !isOverTest)
		{
			SoundPlayCtl.Instance.PlaySound("考试不合格，科三考试结束");
			isOverTest = true;
		}
		_ = isOverTest;
	}

	private void OnDestroy()
	{
		Object.Destroy(countDownPrefabs);
		SoundPlayCtl.Instance.ClearClip();
	}
}
