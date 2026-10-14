using Global;
using SoundAndTextShow;
using UnityEngine;
using View;

public class CourseThreeKaoShi : MonoBehaviour
{
	public static CourseThreeKaoShi Instance;

	private GameObject TriggerArea;

	public bool isLastItem;

	public bool isOverTest;

	public void ChongZhi()
	{
		isOverTest = false;
		isLastItem = false;
		SoundPlayCtl.Instance.PlaySound("科三考试开始");
	}

	private void Start()
	{
		Instance = this;
		TriggerArea = GameObject.Find("TriggerArea");
		isOverTest = false;
		isLastItem = false;
		SoundPlayCtl.Instance.PlaySound("科三考试开始");
	}

	private void Update()
	{
		if (DataCenter.Instance.score >= 80f && isLastItem && !isOverTest)
		{
			SoundPlayCtl.Instance.PlaySound("考试合格，科三考试结束");
			isOverTest = true;
			NetworkManagement.Instance.KaoShiIsPass = true;
		}
		if (DataCenter.Instance.score < 80f && !isOverTest)
		{
			SoundPlayCtl.Instance.PlaySound("考试不合格，科三考试结束");
			isOverTest = true;
		}
		if (isOverTest)
		{
			TriggerArea.gameObject.SetActive(value: false);
		}
		if (SceneMenuView.KaoShiReset)
		{
			ChongZhi();
			SceneMenuView.KaoShiReset = false;
		}
	}
}
