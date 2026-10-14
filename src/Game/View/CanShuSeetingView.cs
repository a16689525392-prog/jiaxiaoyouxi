using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;

namespace View
{
	public class CanShuSeetingView : ViewBase
	{
		private int oriHuiZheng;

		private int oriZhenDong;

		private int oriRecordTime;

		private bool oriIsSaveData;

		private bool isChange;

		public override void Start()
		{
			base.Start();
			oriHuiZheng = SettingInfoCtl.Instance.huiZhengForce;
			oriZhenDong = SettingInfoCtl.Instance.zhengDongForce;
			oriRecordTime = SettingInfoCtl.Instance.historyRecordTime;
			oriIsSaveData = SettingInfoCtl.Instance.isSaveData;
			InitSettingInfo();
			SettingInfoCtl.Instance.BiaoDingModel = MyBiaoDing.Board;
		}

		private void Update()
		{
			if (mSelectBtnIndex != 1)
			{
				SignalSourceCtl.Instance.SetCarShakeStatus(isShake: false);
			}
		}

		private void InitSettingInfo()
		{
			GetBtn(0).GetComponent<NumberAddBtn>().SetNumberText(SettingInfoCtl.Instance.huiZhengForce);
			GetBtn(2).GetComponent<NumberAddBtn>().SetNumberText(SettingInfoCtl.Instance.zhengDongForce);
			GetBtn(3).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isShowTopView);
			GetBtn(4).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isJudgeDoor);
			GetBtn(5).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isJudgeSafetyBelt);
			GetBtn(6).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isJudgeFogLight);
			GetBtn(7).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isCeFangJudgeLight);
			GetBtn(8).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.IsFangXiangPan);
			GetBtn(9).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isReverseSafetyBelt);
			GetBtn(10).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isOpenDongGan);
			GetBtn(11).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isSaveData);
			GetBtn(12).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.BigCarlaps == 4);
			GetBtn(13).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.BigCarlaps == 5);
			GetBtn(14).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isShowBiaoDing);
			GetBtn(15).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.qualityLevel == MyQualityLevel.Fastest);
			GetBtn(16).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.qualityLevel == MyQualityLevel.Fast);
			GetBtn(17).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.qualityLevel == MyQualityLevel.Simple);
			GetBtn(18).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.qualityLevel == MyQualityLevel.Good);
		}

		private void IsSavedata()
		{
			if (SceneTextShow.Instance.isCollectionData != SettingInfoCtl.Instance.isSaveData)
			{
				if (SettingInfoCtl.Instance.isSaveData)
				{
					SceneTextShow.Instance.SavetoDataFile("开始采集");
				}
				else
				{
					SceneTextShow.Instance.SavetoDataFile("结束采集", savedata: true);
				}
				SceneTextShow.Instance.isCollectionData = SettingInfoCtl.Instance.isSaveData;
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				SettingInfoCtl.Instance.huiZhengForce++;
				if (SettingInfoCtl.Instance.huiZhengForce > 100)
				{
					SettingInfoCtl.Instance.huiZhengForce = 0;
				}
				go.GetComponent<NumberAddBtn>().SetNumberText(SettingInfoCtl.Instance.huiZhengForce);
				isChange = true;
			}
			else if (go == GetBtn(1))
			{
				SettingInfoCtl.Instance.huiZhengForce--;
				if (SettingInfoCtl.Instance.huiZhengForce < 0)
				{
					SettingInfoCtl.Instance.huiZhengForce = 100;
				}
				GetBtn(0).GetComponent<NumberAddBtn>().SetNumberText(SettingInfoCtl.Instance.huiZhengForce);
				isChange = true;
			}
			else if (go == GetBtn(2))
			{
				SettingInfoCtl.Instance.zhengDongForce += 10;
				if (SettingInfoCtl.Instance.zhengDongForce > 100)
				{
					SettingInfoCtl.Instance.zhengDongForce = 0;
				}
				go.GetComponent<NumberAddBtn>().SetNumberText(SettingInfoCtl.Instance.zhengDongForce);
				SignalSourceCtl.Instance.SetCarShakeStatus(isShake: true);
				isChange = true;
			}
			else if (go == GetBtn(3))
			{
				SettingInfoCtl.Instance.isShowTopView = !SettingInfoCtl.Instance.isShowTopView;
				GetBtn(3).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isShowTopView);
				isChange = true;
			}
			else if (go == GetBtn(4))
			{
				SettingInfoCtl.Instance.isJudgeDoor = !SettingInfoCtl.Instance.isJudgeDoor;
				GetBtn(4).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isJudgeDoor);
				isChange = true;
			}
			else if (go == GetBtn(5))
			{
				SettingInfoCtl.Instance.isJudgeSafetyBelt = !SettingInfoCtl.Instance.isJudgeSafetyBelt;
				GetBtn(5).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isJudgeSafetyBelt);
				isChange = true;
			}
			else if (go == GetBtn(6))
			{
				SettingInfoCtl.Instance.isJudgeFogLight = !SettingInfoCtl.Instance.isJudgeFogLight;
				GetBtn(6).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isJudgeFogLight);
				isChange = true;
			}
			else if (go == GetBtn(7))
			{
				SettingInfoCtl.Instance.isCeFangJudgeLight = !SettingInfoCtl.Instance.isCeFangJudgeLight;
				GetBtn(7).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isCeFangJudgeLight);
				isChange = true;
			}
			else if (go == GetBtn(8))
			{
				SettingInfoCtl.Instance.IsFangXiangPan = !SettingInfoCtl.Instance.IsFangXiangPan;
				GetBtn(8).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.IsFangXiangPan);
				isChange = true;
			}
			else if (go == GetBtn(9))
			{
				SettingInfoCtl.Instance.isReverseSafetyBelt = !SettingInfoCtl.Instance.isReverseSafetyBelt;
				GetBtn(9).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isReverseSafetyBelt);
				isChange = true;
			}
			else if (go == GetBtn(10))
			{
				SettingInfoCtl.Instance.isOpenDongGan = !SettingInfoCtl.Instance.isOpenDongGan;
				GetBtn(10).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isOpenDongGan);
				isChange = true;
			}
			else if (go == GetBtn(11))
			{
				SettingInfoCtl.Instance.isSaveData = !SettingInfoCtl.Instance.isSaveData;
				GetBtn(11).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isSaveData);
				isChange = true;
			}
			else if (go == GetBtn(12))
			{
				if (SettingInfoCtl.Instance.BigCarlaps != 4)
				{
					SettingInfoCtl.Instance.BigCarlaps = 4;
					GetBtn(12).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: true);
					GetBtn(13).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					isChange = true;
				}
			}
			else if (go == GetBtn(13))
			{
				if (SettingInfoCtl.Instance.BigCarlaps != 5)
				{
					SettingInfoCtl.Instance.BigCarlaps = 5;
					GetBtn(12).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					GetBtn(13).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: true);
					isChange = true;
				}
			}
			else if (go == GetBtn(14))
			{
				SettingInfoCtl.Instance.isShowBiaoDing = !SettingInfoCtl.Instance.isShowBiaoDing;
				GetBtn(14).GetComponent<ToggleBtn>().SetToggleImageActive(SettingInfoCtl.Instance.isShowBiaoDing);
				isChange = true;
			}
			else if (go == GetBtn(15))
			{
				if (SettingInfoCtl.Instance.qualityLevel != MyQualityLevel.Fastest)
				{
					SettingInfoCtl.Instance.qualityLevel = MyQualityLevel.Fastest;
					QualitySettings.SetQualityLevel((int)SettingInfoCtl.Instance.qualityLevel);
					GetBtn(15).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: true);
					GetBtn(16).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					GetBtn(17).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					GetBtn(18).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					isChange = true;
				}
			}
			else if (go == GetBtn(16))
			{
				if (SettingInfoCtl.Instance.qualityLevel != MyQualityLevel.Fast)
				{
					SettingInfoCtl.Instance.qualityLevel = MyQualityLevel.Fast;
					QualitySettings.SetQualityLevel((int)SettingInfoCtl.Instance.qualityLevel);
					GetBtn(15).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					GetBtn(16).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: true);
					GetBtn(17).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					GetBtn(18).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					isChange = true;
				}
			}
			else if (go == GetBtn(17))
			{
				if (SettingInfoCtl.Instance.qualityLevel != MyQualityLevel.Simple)
				{
					SettingInfoCtl.Instance.qualityLevel = MyQualityLevel.Simple;
					QualitySettings.SetQualityLevel((int)SettingInfoCtl.Instance.qualityLevel);
					GetBtn(15).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					GetBtn(16).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					GetBtn(17).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: true);
					GetBtn(18).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					isChange = true;
				}
			}
			else if (go == GetBtn(18))
			{
				if (SettingInfoCtl.Instance.qualityLevel != MyQualityLevel.Good)
				{
					SettingInfoCtl.Instance.qualityLevel = MyQualityLevel.Good;
					QualitySettings.SetQualityLevel((int)SettingInfoCtl.Instance.qualityLevel);
					GetBtn(15).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					GetBtn(16).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					GetBtn(17).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: false);
					GetBtn(18).GetComponent<ToggleBtn>().SetToggleImageActive(isActive: true);
					isChange = true;
				}
			}
			else if (go == GetBtn(19))
			{
				ViewJumpCtl.Instance.ShowMsgTips("保存成功");
				SettingInfoCtl.Instance.SaveSetingInfos();
				isChange = false;
				IsSavedata();
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else
			{
				if (!(go == GetBtn(20)))
				{
					return;
				}
				if (isChange)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.MsgView, isDestroyCurView: false);
					ViewJumpCtl.Instance.curView.GetComponent<MsgView>().InitMsgView("修改的标定信息还未保存\n是否需要保存?", "保存", delegate
					{
						SettingInfoCtl.Instance.SaveSetingInfos();
						ViewJumpCtl.Instance.ShowMsgTips("保存成功");
						IsSavedata();
						ViewJumpCtl.Instance.ReturnPreView();
					}, "不保存", delegate
					{
						SettingInfoCtl.Instance.huiZhengForce = oriHuiZheng;
						SettingInfoCtl.Instance.zhengDongForce = oriZhenDong;
						SettingInfoCtl.Instance.historyRecordTime = oriRecordTime;
						SettingInfoCtl.Instance.isSaveData = oriIsSaveData;
						IsSavedata();
						ViewJumpCtl.Instance.ReturnPreView();
					});
				}
				else
				{
					ViewJumpCtl.Instance.ReturnPreView();
					SettingInfoCtl.Instance.huiZhengForce = oriHuiZheng;
					SettingInfoCtl.Instance.zhengDongForce = oriZhenDong;
					SettingInfoCtl.Instance.historyRecordTime = oriRecordTime;
				}
				SignalSourceCtl.Instance.SetCarShakeStatus(isShake: false);
			}
		}
	}
}
