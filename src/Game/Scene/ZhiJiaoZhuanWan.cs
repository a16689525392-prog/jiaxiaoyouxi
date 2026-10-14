using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class ZhiJiaoZhuanWan : CourseTwoItemCtlBase
	{
		private GameObject ViewParent;

		private GameObject WindowView;

		public int viewStep;

		public bool Findgameobject;

		protected override void Start()
		{
			viewStep = 0;
			ViewParent = GameObject.Find("ViewParent");
			Findgameobject = true;
		}

		private void ChongZhi()
		{
			if (itemStep == 0)
			{
				viewStep = 0;
				if (ViewParent.transform.GetChild(0).gameObject.activeSelf && ViewParent.transform.GetChild(0).name == "NewMainCameraView(Clone)")
				{
					WindowView.SetActive(value: false);
				}
				CourseTwoScene.Instance.SetCameraPosition("ChongZhiCamera");
			}
		}

		private void Update()
		{
			if (ViewParent.transform.GetChild(0).name == "NewMainCameraView(Clone)" && Findgameobject)
			{
				if (CourseTwoScene.Instance.directionID == 0)
				{
					WindowView = ViewParent.transform.GetChild(0).GetChild(2).GetChild(5)
						.GetChild(0)
						.gameObject;
				}
				else if (CourseTwoScene.Instance.directionID == 1)
				{
					if (SceneJumpCtl.Instance.CoursetwoName == CoursetwoScenesName.HuNanYZ)
					{
						WindowView = ViewParent.transform.GetChild(0).GetChild(2).GetChild(5)
							.GetChild(15)
							.gameObject;
					}
					else
					{
						WindowView = ViewParent.transform.GetChild(0).GetChild(2).GetChild(5)
							.GetChild(9)
							.gameObject;
					}
				}
				Findgameobject = false;
			}
			UpdateCarStop();
			ChongZhi();
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			switch (itemStep)
			{
			case 0:
				if (other.name == "Start_ZhiJiao")
				{
					SoundPlayCtl.Instance.PlaySound("直角转弯项目开始");
					itemStep++;
				}
				break;
			case 1:
				if (!(other.name == "OpenLight") || ViewJumpCtl.Instance.trainType != TrainType.CourseTwo)
				{
					break;
				}
				if (CourseTwoScene.Instance.directionID == 0)
				{
					if (CarState.Instance.CarSignal.turnLight == 2)
					{
						SoundPlayCtl.Instance.PlaySound("未正确开启转向灯", 100f);
					}
					else if (CarState.Instance.CarSignal.turnLight == 0)
					{
						SoundPlayCtl.Instance.PlaySound("未开启左转向灯", 100f);
					}
				}
				else if (CourseTwoScene.Instance.directionID == 1)
				{
					if (CarState.Instance.CarSignal.turnLight == 1)
					{
						SoundPlayCtl.Instance.PlaySound("未正确开启转向灯", 100f);
					}
					else if (CarState.Instance.CarSignal.turnLight == 0)
					{
						SoundPlayCtl.Instance.PlaySound("未开启右转向灯", 100f);
					}
				}
				itemStep++;
				break;
			case 2:
				if (other.name == "End_ZhiJiao")
				{
					if (CarState.Instance.CarSignal.turnLight != 0)
					{
						SoundPlayCtl.Instance.PlaySound("转向结束后未及时关闭转向灯", 100f);
					}
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("直角转弯项目结束");
					itemStep = 99;
					NetworkManagement.Instance.TrainingIsPass = true;
					if (ViewJumpCtl.Instance.IsKaoShi && CourseTwoKaoShi.KaoShiStep == CourseTwoScene.Instance.indexID - 1)
					{
						CourseTwoKaoShi.KaoShiStep = CourseTwoScene.Instance.indexID;
					}
				}
				break;
			}
			switch (viewStep)
			{
			case 0:
				if (other.name == "WindowViewOpen")
				{
					CourseTwoScene.Instance.SetCameraPosition("WindowViewOpenCamera");
					if (CourseTwoScene.Instance.GetIsExecute("WindowViewOpenCamera"))
					{
						WindowView.SetActive(value: true);
					}
					viewStep++;
				}
				break;
			case 1:
				if (other.name == "WindowViewClose")
				{
					CourseTwoScene.Instance.SetCameraPosition("WindowViewCloseCamera");
					if (CourseTwoScene.Instance.GetIsExecute("WindowViewCloseCamera"))
					{
						WindowView.SetActive(value: false);
					}
					viewStep++;
				}
				break;
			}
			if (other.name == "OutLine")
			{
				WindowView.SetActive(value: false);
			}
		}

		protected override void OnDestroy()
		{
			if (WindowView != null)
			{
				WindowView.SetActive(value: false);
			}
			if (CarCameraCtl.Instance.leftReviewCamera != null)
			{
				CourseTwoScene.Instance.SetCameraPosition("OnDestroyCamera");
			}
		}
	}
}
