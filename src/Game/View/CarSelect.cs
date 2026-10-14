using Car;
using Global;
using UnityEngine;

namespace View
{
	public class CarSelect : ViewBase
	{
		public static bool isGoBiaoDing;

		private GameObject curSelection;

		public Transform selectIcon;

		private string selectName = "SelectIcon";

		public override void Start()
		{
			base.Start();
			switch (SettingInfoCtl.Instance.carType)
			{
			case CarType.JieDaManual:
				curSelection = GetBtn(0).transform.Find(selectName).gameObject;
				break;
			case CarType.JieDaAuto:
				curSelection = GetBtn(1).transform.Find(selectName).gameObject;
				break;
			case CarType.DongFengDuoLiKa:
				curSelection = GetBtn(2).transform.Find(selectName).gameObject;
				break;
			case CarType.DaBaChe:
				curSelection = GetBtn(3).transform.Find(selectName).gameObject;
				break;
			case CarType.KaLuoLa:
				curSelection = GetBtn(4).transform.Find(selectName).gameObject;
				break;
			case CarType.QiRuiE3:
				curSelection = GetBtn(5).transform.Find(selectName).gameObject;
				break;
			case CarType.JieFang1121:
				curSelection = GetBtn(6).transform.Find(selectName).gameObject;
				break;
			case CarType.TianJin:
				curSelection = GetBtn(7).transform.Find(selectName).gameObject;
				break;
			case CarType.JieFang1125:
				curSelection = GetBtn(8).transform.Find(selectName).gameObject;
				break;
			case CarType.DFKangMingSi:
				curSelection = GetBtn(9).transform.Find(selectName).gameObject;
				break;
			case CarType.PiKa:
				curSelection = GetBtn(10).transform.Find(selectName).gameObject;
				break;
			case CarType.AiLiSheManual:
				curSelection = GetBtn(11).transform.Find(selectName).gameObject;
				break;
			case CarType.AiLiSheAuto:
				curSelection = GetBtn(12).transform.Find(selectName).gameObject;
				break;
			case CarType.QianYinChe:
				curSelection = GetBtn(13).transform.Find(selectName).gameObject;
				break;
			case CarType.YongShi:
				curSelection = GetBtn(14).transform.Find(selectName).gameObject;
				break;
			default:
				selectIcon.position = GetBtn(0).transform.position;
				break;
			}
			curSelection.SetActive(value: true);
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			curSelection.SetActive(value: false);
			if (go == GetBtn(0))
			{
				SettingInfoCtl.Instance.carType = CarType.JieDaManual;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(0).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(1))
			{
				SettingInfoCtl.Instance.carType = CarType.JieDaAuto;
				SettingInfoCtl.Instance.isManualGear = false;
				curSelection = GetBtn(1).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(2))
			{
				SettingInfoCtl.Instance.carType = CarType.DongFengDuoLiKa;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(2).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(3))
			{
				SettingInfoCtl.Instance.carType = CarType.DaBaChe;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(3).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(4))
			{
				SettingInfoCtl.Instance.carType = CarType.KaLuoLa;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(4).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(5))
			{
				SettingInfoCtl.Instance.carType = CarType.QiRuiE3;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(5).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(6))
			{
				SettingInfoCtl.Instance.carType = CarType.JieFang1121;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(6).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(7))
			{
				SettingInfoCtl.Instance.carType = CarType.TianJin;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(7).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(8))
			{
				SettingInfoCtl.Instance.carType = CarType.JieFang1125;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(8).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(9))
			{
				SettingInfoCtl.Instance.carType = CarType.DFKangMingSi;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(9).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(10))
			{
				SettingInfoCtl.Instance.carType = CarType.PiKa;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(10).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(11))
			{
				SettingInfoCtl.Instance.carType = CarType.AiLiSheManual;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(11).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(12))
			{
				SettingInfoCtl.Instance.carType = CarType.AiLiSheAuto;
				SettingInfoCtl.Instance.isManualGear = false;
				curSelection = GetBtn(12).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(13))
			{
				SettingInfoCtl.Instance.carType = CarType.QianYinChe;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(13).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(14))
			{
				SettingInfoCtl.Instance.carType = CarType.YongShi;
				SettingInfoCtl.Instance.isManualGear = true;
				curSelection = GetBtn(14).transform.Find(selectName).gameObject;
				if (isGoBiaoDing)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.LockBiaoDingView, isDestroyCurView: true, isSave: false);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(15))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			SettingInfoCtl.Instance.SaveSetingInfos();
			isGoBiaoDing = false;
			curSelection.SetActive(value: true);
		}
	}
}
