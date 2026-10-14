using Global;
using UnityEngine;

namespace View
{
	public class TianqiQiangduView : ViewBase
	{
		private int Intensity;

		public override void Start()
		{
			base.Start();
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				Intensity = 0;
			}
			else if (go == GetBtn(1))
			{
				Intensity = 1;
			}
			else if (go == GetBtn(2))
			{
				Intensity = 2;
			}
			switch (BadWeatherView.scene)
			{
			case "rain":
				SettingInfoCtl.Instance.RainIntensity = Intensity;
				SettingInfoCtl.Instance.SaveSetingInfos();
				break;
			case "snow":
				SettingInfoCtl.Instance.SnowIntensity = Intensity;
				SettingInfoCtl.Instance.SaveSetingInfos();
				break;
			case "fog":
				SettingInfoCtl.Instance.FogIntensity = Intensity;
				SettingInfoCtl.Instance.SaveSetingInfos();
				break;
			}
			SceneMenuView.isWeatherChoose = false;
			ViewJumpCtl.Instance.ReturnPreView();
		}
	}
}
