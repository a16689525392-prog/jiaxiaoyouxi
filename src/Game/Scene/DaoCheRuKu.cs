using System;
using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class DaoCheRuKu : CourseTwoItemCtlBase
	{
		private GameObject ViewParent;

		private GameObject WindowJianBangView;

		public int viewStep;

		public bool Findgameobject;

		private float intervalTime = 10f;

		protected override void Start()
		{
			if (SettingInfoCtl.Instance.carType == CarType.JieDaManual || SettingInfoCtl.Instance.carType == CarType.JieDaAuto)
			{
				viewStep = 0;
				ViewParent = GameObject.Find("ViewParent");
				Findgameobject = true;
			}
			ColliderDetection instance = ColliderDetection.instance;
			instance.CarOutsideCallback = (UnityAction<RaycastHit>)Delegate.Combine(instance.CarOutsideCallback, new UnityAction<RaycastHit>(CarFrameHitEvent));
			StartCoroutine(ColliderDetection.instance.DetectionCarOutside());
			SoundPlayCtl.Instance.PlaySound("请将车辆向前行驶至项目起始位置");
		}

		private void ChongZhi()
		{
			if (itemStep == 0)
			{
				viewStep = 0;
				if (ViewParent.transform.GetChild(0).gameObject.activeSelf && ViewParent.transform.GetChild(0).name == "NewMainCameraView(Clone)")
				{
					WindowJianBangView.SetActive(value: false);
				}
				CourseTwoScene.Instance.SetCameraPosition("ChongZhiCamera");
			}
		}

		private void Update()
		{
			if (SettingInfoCtl.Instance.carType == CarType.JieDaManual || SettingInfoCtl.Instance.carType == CarType.JieDaAuto)
			{
				if (ViewParent.transform.GetChild(0).name == "NewMainCameraView(Clone)" && Findgameobject)
				{
					WindowJianBangView = ViewParent.transform.GetChild(0).GetChild(2).GetChild(5)
						.GetChild(10)
						.gameObject;
					Findgameobject = false;
				}
				if (WindowJianBangView.activeSelf && CarActionJudge.Instance.JudgeActionDone(114) && CarState.Instance.CurSpeed > 0.1f)
				{
					CourseTwoScene.Instance.SetCameraPosition("WindowView1CloseCamera");
					WindowJianBangView.SetActive(value: false);
				}
				ChongZhi();
			}
			intervalTime += Time.deltaTime;
			UpdateJudgeDaoKu();
		}

		private void UpdateJudgeDaoKu()
		{
			if (itemStep == 2)
			{
				if (!CarActionJudge.Instance.JudgeActionDone(114) && !CarActionJudge.Instance.JudgeActionDone(115))
				{
					SoundPlayCtl.Instance.PlaySound("倒库不入", 100f);
					itemStep++;
				}
			}
			else if (itemStep == 3)
			{
				if (!CarActionJudge.Instance.JudgeActionDone(114) && CarState.Instance.CurSpeed > 0.1f)
				{
					itemStep++;
				}
			}
			else if (itemStep == 4)
			{
				if (CarActionJudge.Instance.JudgeActionDone(114))
				{
					SoundPlayCtl.Instance.PlaySound("未将车辆两前轮均驶过控制线", 100f);
					itemStep++;
				}
			}
			else if (itemStep == 6 && !CarActionJudge.Instance.JudgeActionDone(114) && !CarActionJudge.Instance.JudgeActionDone(115))
			{
				SoundPlayCtl.Instance.PlaySound("倒库不入", 100f);
				itemStep++;
			}
		}

		private void CarFrameHitEvent(RaycastHit hitInfo)
		{
			if (intervalTime > 10f)
			{
				SoundPlayCtl.Instance.PlaySound("车身出线", 100f);
				intervalTime = 0f;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			switch (itemStep)
			{
			case -1:
				if (other.name == "Start_DaoKu")
				{
					SoundPlayCtl.Instance.PlaySound("倒车入库项目开始");
					itemStep += 2;
				}
				break;
			case 1:
				if (other.name == "Step1")
				{
					itemStep++;
				}
				break;
			case 2:
				if (other.name == "Step2")
				{
					itemStep++;
				}
				break;
			case 4:
				if (other.name == "Step3")
				{
					itemStep++;
				}
				break;
			case 5:
				if (other.name == "Step1")
				{
					itemStep++;
				}
				break;
			case 6:
				if (other.name == "Step2")
				{
					itemStep++;
				}
				break;
			case 7:
				if (other.name == "End")
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("倒车入库项目结束");
					itemStep = 99;
					NetworkManagement.Instance.TrainingIsPass = true;
					if (ViewJumpCtl.Instance.IsKaoShi && CourseTwoKaoShi.KaoShiStep == CourseTwoScene.Instance.indexID - 1)
					{
						CourseTwoKaoShi.KaoShiStep = CourseTwoScene.Instance.indexID;
					}
				}
				break;
			}
			if (SettingInfoCtl.Instance.carType == CarType.JieDaManual || SettingInfoCtl.Instance.carType == CarType.JieDaAuto)
			{
				switch (viewStep)
				{
				case 0:
					if (other.name == "WindowView1")
					{
						CourseTwoScene.Instance.SetCameraPosition("WindowView1OpenCamera");
						if (CourseTwoScene.Instance.GetIsExecute("WindowView1OpenCamera"))
						{
							WindowJianBangView.SetActive(value: true);
						}
						viewStep++;
						itemStep = -1;
					}
					break;
				case 1:
					if (other.name == "Start_DaoKu")
					{
						viewStep++;
					}
					break;
				case 2:
					if (other.name == "WindowView1")
					{
						CourseTwoScene.Instance.SetCameraPosition("WindowView1CloseCamera");
						if (CourseTwoScene.Instance.GetIsExecute("WindowView1CloseCamera"))
						{
							WindowJianBangView.SetActive(value: false);
						}
						viewStep++;
					}
					break;
				case 3:
					if (other.name == "WindowView2")
					{
						CourseTwoScene.Instance.SetCameraPosition("WindowView2OpenCamera");
						if (CourseTwoScene.Instance.GetIsExecute("WindowView2OpenCamera"))
						{
							WindowJianBangView.SetActive(value: true);
						}
						viewStep++;
					}
					break;
				case 4:
					if (other.name == "Step3")
					{
						viewStep++;
					}
					break;
				case 5:
					if (other.name == "WindowView2")
					{
						CourseTwoScene.Instance.SetCameraPosition("WindowView2CloseCamera");
						if (CourseTwoScene.Instance.GetIsExecute("WindowView2CloseCamera"))
						{
							WindowJianBangView.SetActive(value: false);
						}
						viewStep++;
					}
					break;
				}
				if (other.name == "OutLine" && WindowJianBangView != null)
				{
					WindowJianBangView.SetActive(value: false);
				}
			}
			if (other.name == "End" && itemStep < 7)
			{
				SoundPlayCtl.Instance.PlaySound("未按规定进行倒车入库操作", 100f);
				itemStep = 99;
			}
		}

		protected override void OnDestroy()
		{
			if (SettingInfoCtl.Instance.carType != CarType.JieDaManual)
			{
				_ = SettingInfoCtl.Instance.carType;
				_ = 1;
			}
			if (WindowJianBangView != null)
			{
				WindowJianBangView.SetActive(value: false);
			}
			if (CarCameraCtl.Instance.leftReviewCamera != null)
			{
				CourseTwoScene.Instance.SetCameraPosition("OnDestroyCamera");
			}
		}
	}
}
