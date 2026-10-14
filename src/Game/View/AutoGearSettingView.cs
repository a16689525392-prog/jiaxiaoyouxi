using Car;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class AutoGearSettingView : ViewBase
	{
		private bool ismanualgear;

		public Text MGear;

		public Text AGear;

		public override void Awake()
		{
			base.Awake();
			ismanualgear = SettingInfoCtl.Instance.isManualGear;
			if (!SettingInfoCtl.Instance.isManualGear)
			{
				SettingInfoCtl.Instance.isManualGear = !SettingInfoCtl.Instance.isManualGear;
			}
		}

		private string AGearToString(int agear)
		{
			switch (agear)
			{
			case 101:
				return "P";
			case 102:
				return "R";
			case 104:
				return "N";
			case 100:
				return "D";
			default:
				return "N";
			}
		}

		private void ChangeManualToAutoGear(int gear)
		{
			switch (CarState.Instance.CarSignal.gear)
			{
			case 1:
				SettingInfoCtl.Instance.ManualToAutoGear1 = gear;
				break;
			case 2:
				SettingInfoCtl.Instance.ManualToAutoGear2 = gear;
				break;
			case 3:
				SettingInfoCtl.Instance.ManualToAutoGear3 = gear;
				break;
			case 4:
				SettingInfoCtl.Instance.ManualToAutoGear4 = gear;
				break;
			case 5:
				SettingInfoCtl.Instance.ManualToAutoGear5 = gear;
				break;
			case 6:
				SettingInfoCtl.Instance.ManualToAutoGearR = gear;
				break;
			}
			SettingInfoCtl.Instance.SaveSetingInfos();
		}

		private void Update()
		{
			switch (CarState.Instance.CarSignal.gear)
			{
			case 1:
				MGear.text = "1";
				AGear.text = AGearToString(SettingInfoCtl.Instance.ManualToAutoGear1);
				break;
			case 2:
				MGear.text = "2";
				AGear.text = AGearToString(SettingInfoCtl.Instance.ManualToAutoGear2);
				break;
			case 3:
				MGear.text = "3";
				AGear.text = AGearToString(SettingInfoCtl.Instance.ManualToAutoGear3);
				break;
			case 4:
				MGear.text = "4";
				AGear.text = AGearToString(SettingInfoCtl.Instance.ManualToAutoGear4);
				break;
			case 5:
				MGear.text = "5";
				AGear.text = AGearToString(SettingInfoCtl.Instance.ManualToAutoGear5);
				break;
			case 0:
				MGear.text = "N";
				AGear.text = "N";
				break;
			case 6:
				MGear.text = "R";
				AGear.text = AGearToString(SettingInfoCtl.Instance.ManualToAutoGearR);
				break;
			default:
				MGear.text = "N";
				AGear.text = "N";
				break;
			}
			if (CarState.Instance.CarSignal.gear == 0)
			{
				GetBtn(0).SetActive(value: false);
				GetBtn(1).SetActive(value: false);
				GetBtn(2).SetActive(value: false);
				GetBtn(3).SetActive(value: false);
			}
			else
			{
				GetBtn(0).SetActive(value: true);
				GetBtn(1).SetActive(value: true);
				GetBtn(2).SetActive(value: true);
				GetBtn(3).SetActive(value: true);
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0) && go.activeInHierarchy)
			{
				ChangeManualToAutoGear(101);
			}
			else if (go == GetBtn(1) && go.activeInHierarchy)
			{
				ChangeManualToAutoGear(102);
			}
			else if (go == GetBtn(2) && go.activeInHierarchy)
			{
				ChangeManualToAutoGear(104);
			}
			else if (go == GetBtn(3) && go.activeInHierarchy)
			{
				ChangeManualToAutoGear(100);
			}
			else if (go == GetBtn(4))
			{
				if (!ismanualgear)
				{
					SettingInfoCtl.Instance.isManualGear = !SettingInfoCtl.Instance.isManualGear;
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
