using System;
using System.Runtime.InteropServices;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class SetResolutionView : ViewBase
	{
		private static int SM_W = 0;

		private static int SM_H = 1;

		public Text Textstate;

		public Text Textsetresolution;

		private string value = "";

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int GetSystemMetrics(int nIndex);

		public override void Awake()
		{
			base.Awake();
			Textstate.text = "当前分辨率为:" + SettingInfoCtl.Instance.SetResolution;
			value = "自适应";
			Textsetresolution.text = "需更改分辨率为:" + value;
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				value = "800 * 600";
			}
			else if (go == GetBtn(1))
			{
				value = "1920*1080";
			}
			else if (go == GetBtn(2))
			{
				value = "2560*1080";
			}
			else if (go == GetBtn(3))
			{
				value = "3840*1080";
			}
			else if (go == GetBtn(4))
			{
				value = "5760*1080";
			}
			else if (go == GetBtn(5))
			{
				value = "自适应";
			}
			else if (go == GetBtn(6))
			{
				SettingInfoCtl.Instance.SetResolution = value;
				SettingInfoCtl.Instance.SaveSetingInfos();
				if (value == "自适应")
				{
					if (Convert.ToSingle(GetSystemMetrics(0)) / Convert.ToSingle(GetSystemMetrics(1)) > 2.4f && (DataCenter.Instance.UsbVersion == 48 || DataCenter.Instance.UsbVersion == 80 || DataCenter.Instance.UsbVersion == 0 || DataCenter.Instance.UsbVersion == 16 || DataCenter.Instance.UsbVersion == 112 || DataCenter.Instance.UsbVersion == 224))
					{
						if (!(Convert.ToSingle(Screen.width) / Convert.ToSingle(Screen.height) < 2.4f))
						{
							Application.runInBackground = true;
							Screen.SetResolution(2560, 1080, fullscreen: true);
						}
					}
					else
					{
						Application.runInBackground = true;
						Screen.SetResolution(GetSystemMetrics(0), GetSystemMetrics(1), fullscreen: true);
					}
				}
				else
				{
					string[] array = value.Split('*');
					int width = Convert.ToInt32(array[0]);
					int height = Convert.ToInt32(array[1]);
					Application.runInBackground = true;
					Screen.SetResolution(width, height, fullscreen: true);
				}
				ViewJumpCtl.Instance.ReturnPreView();
			}
			else if (go == GetBtn(7))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			Textsetresolution.text = "需更改分辨率为:" + value;
		}
	}
}
