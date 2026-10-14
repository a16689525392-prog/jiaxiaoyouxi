using Car;
using Global;
using Scene;
using SoundAndTextShow;
using UnityEngine;
using View;

public class HuNanYZCourseThreeKaoShi : MonoBehaviour
{
	public static HuNanYZCourseThreeKaoShi Instance;

	private GameObject TriggerArea;

	public bool isLastItem;

	public bool isOverTest;

	private NightLightCtl curItemCtl;

	public bool NightLightIsOver;

	private int itemStep;

	private GameObject countDownPrefabs;

	private float countdowntime;

	public void ChongZhi()
	{
		SoundPlayCtl.Instance.ClearClip();
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
		SoundPlayCtl.Instance.PlaySound("请开始考试，监管认证通过，请继续考试");
		TriggerArea.gameObject.SetActive(value: true);
		itemStep = 0;
		countdowntime = 0f;
	}

	private void Start()
	{
		Instance = this;
		TriggerArea = GameObject.Find("TriggerArea");
		isOverTest = false;
		isLastItem = false;
		SoundPlayCtl.Instance.PlaySound("请开始考试，监管认证通过，请继续考试");
		itemStep = 0;
		countdowntime = 0f;
	}

	private void Update()
	{
		Debug.Log("步骤" + itemStep + "时间" + countdowntime);
		if (itemStep == 0)
		{
			if (!SoundPlayCtl.Instance.IsPlaying())
			{
				countDownPrefabs = ViewJumpCtl.Instance.CreateViewPrefab("CountDown", Vector3.zero);
				countDownPrefabs.GetComponent<CountDownCtl>().SetCountDownTime(11);
				itemStep++;
			}
		}
		else if (itemStep == 1)
		{
			countdowntime += Time.deltaTime;
			if (countdowntime > 10f)
			{
				itemStep++;
				countdowntime = 0f;
			}
		}
		else if (itemStep == 2)
		{
			Object.Destroy(countDownPrefabs);
			SoundPlayCtl.Instance.PlaySound("上车准备已完成,请启动发动机准备开始模拟夜考");
			itemStep++;
		}
		else if (itemStep == 3)
		{
			if (!CarState.Instance.FireOn)
			{
				countdowntime += Time.deltaTime;
				if (countdowntime > 10f)
				{
					SoundPlayCtl.Instance.PlaySound("上车准备已完成,请启动发动机准备开始模拟夜考");
					countdowntime = 0f;
				}
			}
			else
			{
				itemStep++;
			}
		}
		else if (itemStep == 4)
		{
			if (CarState.Instance.FireOn && curItemCtl == null && !NightLightIsOver)
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
		else if (itemStep == 5)
		{
			countdowntime += Time.deltaTime;
			if (countdowntime > 5f)
			{
				SoundPlayCtl.Instance.PlaySound("请立即起步");
				countdowntime = 0f;
				itemStep = 99;
			}
		}
		if (DataCenter.Instance.score >= 90f && isLastItem && !isOverTest)
		{
			SoundPlayCtl.Instance.PlaySound("考试合格，科三考试结束");
			isOverTest = true;
			NetworkManagement.Instance.KaoShiIsPass = true;
		}
		if (DataCenter.Instance.score < 90f && !isOverTest)
		{
			SoundPlayCtl.Instance.PlaySound("考试不合格，科三考试结束");
			isOverTest = true;
		}
		_ = isOverTest;
		if (SceneMenuView.KaoShiReset)
		{
			ChongZhi();
			SceneMenuView.KaoShiReset = false;
		}
	}

	private void OnDestroy()
	{
		Object.Destroy(countDownPrefabs);
		SoundPlayCtl.Instance.ClearClip();
	}
}
