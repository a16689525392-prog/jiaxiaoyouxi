using Global;
using Scene;
using SoundAndTextShow;
using UnityEngine;
using View;

public class CourseTwoKaoShi : CourseTwoItemCtlBase
{
	public static CourseTwoKaoShi Instance;

	private GameObject TriggerArea;

	public static int KaoShiStep;

	private bool isOverTest;

	private float dirveAllTime;

	public void ChongZhi()
	{
		TriggerArea.SetActive(value: true);
		KaoShiStep = 0;
		isOverTest = false;
		dirveAllTime = 0f;
		SoundPlayCtl.Instance.PlaySound("调整桌椅，系好安全带，本次考试开始");
		if (CourseTwoScene.Instance.curItemCtl != null)
		{
			Object.Destroy(CourseTwoScene.Instance.curItemCtl);
		}
	}

	protected override void Start()
	{
		Instance = this;
		TriggerArea = GameObject.Find("TriggerArea");
		KaoShiStep = 0;
		isOverTest = false;
		dirveAllTime = 0f;
		SoundPlayCtl.Instance.PlaySound("调整桌椅，系好安全带，本次考试开始");
	}

	private void Update()
	{
		dirveAllTime += Time.deltaTime;
		if (ViewJumpCtl.Instance.itemType == 1200)
		{
			if (KaoShiStep == 5 && DataCenter.Instance.score >= 90f && !isOverTest)
			{
				SoundPlayCtl.Instance.PlaySound("考试通过，请考生将车开回起点");
				isOverTest = true;
				NetworkManagement.Instance.KaoShiIsPass = true;
			}
			if ((DataCenter.Instance.score < 90f || dirveAllTime > 10200f) && !isOverTest)
			{
				SoundPlayCtl.Instance.PlaySound("考试不合格，请考生将车开回起点");
				isOverTest = true;
			}
		}
		_ = isOverTest;
		if (SceneMenuView.KaoShiReset)
		{
			ChongZhi();
			SceneMenuView.KaoShiReset = false;
		}
	}
}
