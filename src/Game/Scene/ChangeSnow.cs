using Global;
using UnityEngine;

namespace Scene
{
	public class ChangeSnow : MonoBehaviour
	{
		private void Start()
		{
		}

		private void Update()
		{
			ParticleSystem.EmissionModule emission = base.transform.GetChild(0).GetComponent<ParticleSystem>().emission;
			ParticleSystem component = base.transform.GetChild(0).GetComponent<ParticleSystem>();
			AudioSource component2 = base.transform.GetChild(0).GetComponent<AudioSource>();
			switch (SettingInfoCtl.Instance.SnowIntensity)
			{
			case 0:
				emission.rateOverTime = 25f;
				component.startSpeed = 5f;
				component2.volume = 0.01f;
				component2.pitch = 1f;
				break;
			case 1:
				emission.rateOverTime = 100f;
				component.startSpeed = 6f;
				component2.volume = 0.3f;
				component2.pitch = 1f;
				break;
			case 2:
				emission.rateOverTime = 500f;
				component.startSpeed = 8f;
				component2.volume = 1f;
				component2.pitch = 1f;
				break;
			}
		}
	}
}
