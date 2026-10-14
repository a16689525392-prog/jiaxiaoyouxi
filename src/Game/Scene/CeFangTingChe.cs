using System;
using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class CeFangTingChe : CourseTwoItemCtlBase
	{
		private GameObject ViewParent;

		private GameObject WindowViewR;

		public int viewStep;

		public bool Findgameobject;

		public bool CloseWindowViewR;

		private float CloseTime;

		private float intervalTime = 10f;

		protected override void Start()
		{
			viewStep = 0;
			ViewParent = GameObject.Find("ViewParent");
			Findgameobject = true;
			ColliderDetection instance = ColliderDetection.instance;
			instance.CarOutsideCallback = (UnityAction<RaycastHit>)Delegate.Combine(instance.CarOutsideCallback, new UnityAction<RaycastHit>(CarFrameHitEvent));
			StartCoroutine(ColliderDetection.instance.DetectionCarOutside());
		}

		private void ChongZhi()
		{
			if (itemStep == 0)
			{
				viewStep = 0;
				if (ViewParent.transform.GetChild(0).gameObject.activeSelf && ViewParent.transform.GetChild(0).name == "NewMainCameraView(Clone)")
				{
					WindowViewR.SetActive(value: false);
				}
				CourseTwoScene.Instance.SetCameraPosition("ChongZhiCamera");
				CloseWindowViewR = false;
			}
		}

		private void Update()
		{
			if (ViewParent.transform.GetChild(0).name == "NewMainCameraView(Clone)" && Findgameobject)
			{
				WindowViewR = ViewParent.transform.GetChild(0).GetChild(2).GetChild(5)
					.GetChild(9)
					.gameObject;
				Findgameobject = false;
			}
			intervalTime += Time.deltaTime;
			UpdateJudgeCeFang();
			ChongZhi();
			if (viewStep == 1 && CarState.Instance.CarSignal.footBrake > 0.75f)
			{
				CloseWindowViewR = true;
			}
			if (!CloseWindowViewR)
			{
				return;
			}
			CloseTime += Time.deltaTime;
			if (CloseTime > 1f)
			{
				if (CourseTwoScene.Instance.GetIsExecute("WindowViewRCloseCamera"))
				{
					WindowViewR.SetActive(value: false);
				}
				CourseTwoScene.Instance.SetCameraPosition("WindowViewRCloseCamera");
				viewStep++;
				CloseWindowViewR = false;
				CloseTime = 0f;
			}
		}

		private void UpdateJudgeCeFang()
		{
			if (itemStep == 1)
			{
				if (ViewJumpCtl.Instance.modelType == 2 && CarActionJudge.Instance.JudgeActionDone(114) && CarState.Instance.CurSpeed > 0.25f && CarState.Instance.FireOn)
				{
					itemStep++;
				}
				else if (ViewJumpCtl.Instance.modelType == 0 && CarActionJudge.Instance.JudgeActionDone(114) && DriveHistoryPlay.Instance.curCalculateSpeedH > 0.15f)
				{
					if ((DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 1 || !SettingInfoCtl.Instance.isCeFangJudgeLight) && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 0)
					{
						_ = SettingInfoCtl.Instance.isCeFangJudgeLight;
					}
					itemStep++;
				}
			}
			else
			{
				if (itemStep != 3)
				{
					return;
				}
				if (ViewJumpCtl.Instance.modelType == 2 && !CarActionJudge.Instance.JudgeActionDone(114) && CarState.Instance.CurSpeed > 0.15f)
				{
					if (CarState.Instance.CarSignal.turnLight == 2 && SettingInfoCtl.Instance.isCeFangJudgeLight)
					{
						SoundPlayCtl.Instance.PlaySound("出库时,未正确开启左转向灯", 10f);
					}
					else if (CarState.Instance.CarSignal.turnLight == 0 && SettingInfoCtl.Instance.isCeFangJudgeLight)
					{
						SoundPlayCtl.Instance.PlaySound("出库时,未开启左转向灯", 10f);
					}
					itemStep++;
				}
				else if (ViewJumpCtl.Instance.modelType == 0 && !CarActionJudge.Instance.JudgeActionDone(114) && DriveHistoryPlay.Instance.curCalculateSpeedH > 0.15f)
				{
					if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 2 && SettingInfoCtl.Instance.isCeFangJudgeLight)
					{
						SoundPlayCtl.Instance.PlaySound("出库时,未正确开启左转向灯", 10f);
					}
					else if (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 0 && SettingInfoCtl.Instance.isCeFangJudgeLight)
					{
						SoundPlayCtl.Instance.PlaySound("出库时,未开启左转向灯", 10f);
					}
					itemStep++;
				}
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
			if (itemStep == 0 && other.name == "Start_CeFang")
			{
				SoundPlayCtl.Instance.PlaySound("侧方停车项目开始");
				itemStep = 1;
			}
			else if (itemStep != 3 && other.name == "Step1")
			{
				itemStep = 3;
			}
			else if (itemStep != 99 && itemStep != 0 && other.name == "End")
			{
				if (itemStep < 3)
				{
					SoundPlayCtl.Instance.PlaySound("未按规定进行侧方停车操作", 100f);
				}
				else if (ViewJumpCtl.Instance.modelType == 2 && CarState.Instance.CarSignal.turnLight != 0 && SettingInfoCtl.Instance.isCeFangJudgeLight)
				{
					SoundPlayCtl.Instance.PlaySound("未及时关闭转向灯", 10f);
				}
				else if (ViewJumpCtl.Instance.modelType == 0 && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 0 && SettingInfoCtl.Instance.isCeFangJudgeLight)
				{
					SoundPlayCtl.Instance.PlaySound("未及时关闭转向灯", 10f);
				}
				ItemEnd();
				SoundPlayCtl.Instance.PlaySound("侧方停车项目结束");
				itemStep = 99;
				NetworkManagement.Instance.TrainingIsPass = true;
				if (ViewJumpCtl.Instance.IsKaoShi && CourseTwoKaoShi.KaoShiStep == CourseTwoScene.Instance.indexID - 1)
				{
					CourseTwoKaoShi.KaoShiStep = CourseTwoScene.Instance.indexID;
				}
			}
			if (viewStep == 0 && other.name == "WindowViewR")
			{
				CourseTwoScene.Instance.SetCameraPosition("WindowViewROpenCamera");
				if (CourseTwoScene.Instance.GetIsExecute("WindowViewROpenCamera"))
				{
					WindowViewR.SetActive(value: true);
				}
				viewStep++;
			}
			if (other.name == "OutLine")
			{
				WindowViewR.SetActive(value: false);
				CourseTwoScene.Instance.SetCameraPosition("WindowViewROutLineCamera");
			}
		}

		protected override void OnDestroy()
		{
			if (WindowViewR != null)
			{
				WindowViewR.SetActive(value: false);
				CourseTwoScene.Instance.SetCameraPosition("OnDestroyCamera");
			}
		}
	}
}
