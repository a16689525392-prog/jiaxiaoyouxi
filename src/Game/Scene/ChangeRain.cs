using System;
using Global;
using UnityEngine;

namespace Scene
{
	public class ChangeRain : MonoBehaviour
	{
		private GameObject ParticleSystem;

		private ParticleSystem.EmissionModule lizinumber;

		private ParticleSystem lizispeed;

		private AudioSource soure;

		public GameObject Mcamera;

		private void Start()
		{
			ParticleSystem = base.transform.GetChild(1).gameObject;
			ParticleSystem.GetComponent<ParticleSystem>().Play();
			lizinumber = ParticleSystem.GetComponent<ParticleSystem>().emission;
			lizispeed = ParticleSystem.GetComponent<ParticleSystem>();
			soure = ParticleSystem.GetComponent<AudioSource>();
		}

		[Obsolete]
		private void Update()
		{
			if (SettingInfoCtl.Instance.curViewPoint == 0)
			{
				Mcamera.gameObject.SetActive(value: true);
			}
			else
			{
				Mcamera.gameObject.SetActive(value: false);
			}
			ParticleSystem.GetComponent<ParticleSystem>().Play();
			switch (SettingInfoCtl.Instance.RainIntensity)
			{
			case 0:
				lizinumber.rateOverTime = 240f;
				lizispeed.startSpeed = 13f;
				soure.volume = 0.2f;
				soure.pitch = 1f;
				break;
			case 1:
				lizinumber.rateOverTime = 500f;
				lizispeed.startSpeed = 20f;
				soure.volume = 0.5f;
				soure.pitch = 1f;
				break;
			case 2:
				lizinumber.rateOverTime = 1000f;
				lizispeed.startSpeed = 30f;
				soure.volume = 1f;
				soure.pitch = 1f;
				break;
			}
		}
	}
}
