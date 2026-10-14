using Global;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Scene
{
	public class SkyBoxCtl : MonoBehaviour
	{
		private static SkyBoxCtl instance;

		public GameObject weatherTypeChoose;

		public Dropdown intensity;

		public Material sunnySkyBox;

		public Material rainSkyBox;

		public Material fogSkyBox;

		public Material nightSkyBox;

		public static SkyBoxCtl Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		public void ChangeSkyBox(int weatherType)
		{
			switch (weatherType)
			{
			case 504:
				RenderSettings.skybox = sunnySkyBox;
				RenderSettings.fog = false;
				DataCenter.Instance.mAdhere = 0.7f;
				break;
			case 500:
				RenderSettings.skybox = rainSkyBox;
				RenderSettings.fog = true;
				RenderSettings.fogMode = FogMode.ExponentialSquared;
				DataCenter.Instance.mAdhere = 0.7f;
				break;
			case 502:
				RenderSettings.skybox = fogSkyBox;
				RenderSettings.fog = true;
				RenderSettings.fogColor = new Color(40f / 51f, 40f / 51f, 40f / 51f);
				RenderSettings.fogMode = FogMode.ExponentialSquared;
				DataCenter.Instance.mAdhere = 0.3f;
				break;
			case 501:
				RenderSettings.skybox = fogSkyBox;
				RenderSettings.fog = true;
				RenderSettings.fogDensity = 0.01f;
				RenderSettings.fogMode = FogMode.ExponentialSquared;
				DataCenter.Instance.mAdhere = 0.7f;
				break;
			case 505:
				RenderSettings.skybox = nightSkyBox;
				RenderSettings.fog = false;
				DataCenter.Instance.mAdhere = 0.7f;
				break;
			case 701:
				RenderSettings.skybox = nightSkyBox;
				RenderSettings.fog = false;
				DataCenter.Instance.mAdhere = 0.7f;
				break;
			default:
				RenderSettings.skybox = fogSkyBox;
				RenderSettings.fog = false;
				break;
			}
		}

		private void Update()
		{
			string scene = BadWeatherView.scene;
			switch (scene)
			{
			default:
				_ = scene == "null";
				break;
			case "rain":
				if (SettingInfoCtl.Instance.RainIntensity == 0)
				{
					RenderSettings.fogDensity = 0.001f;
				}
				else if (SettingInfoCtl.Instance.RainIntensity == 1)
				{
					RenderSettings.fogDensity = 0.002f;
				}
				else if (SettingInfoCtl.Instance.RainIntensity == 2)
				{
					RenderSettings.fogDensity = 0.005f;
				}
				break;
			case "snow":
				if (SettingInfoCtl.Instance.SnowIntensity == 0)
				{
					RenderSettings.fogDensity = 0.001f;
				}
				else if (SettingInfoCtl.Instance.SnowIntensity == 1)
				{
					RenderSettings.fogDensity = 0.003f;
				}
				else if (SettingInfoCtl.Instance.SnowIntensity == 2)
				{
					RenderSettings.fogDensity = 0.01f;
				}
				break;
			case "fog":
				if (SettingInfoCtl.Instance.FogIntensity == 0)
				{
					RenderSettings.fogDensity = 0.01f;
				}
				else if (SettingInfoCtl.Instance.FogIntensity == 1)
				{
					RenderSettings.fogDensity = 0.02f;
				}
				else if (SettingInfoCtl.Instance.FogIntensity == 2)
				{
					RenderSettings.fogDensity = 0.1f;
				}
				break;
			}
		}
	}
}
