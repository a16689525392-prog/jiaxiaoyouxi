using UnityEngine;

public class FlashCarSignal : MonoBehaviour
{
	public GameObject signalLight;

	private float flashTime;

	private GameObject turnLeftLightShow;

	private GameObject turnRightLightShow;

	private void Update()
	{
		flashTime += Time.deltaTime;
		if (flashTime > 0f && flashTime < 0.6f)
		{
			turnLeftLightShow.SetActive(value: true);
			turnRightLightShow.SetActive(value: true);
		}
		else if (flashTime >= 0.6f && flashTime <= 1.2f)
		{
			turnLeftLightShow.SetActive(value: false);
			turnRightLightShow.SetActive(value: false);
		}
		else
		{
			flashTime = 0f;
		}
	}

	private void Start()
	{
		InitSignalLightGo();
	}

	private void InitSignalLightGo()
	{
		turnLeftLightShow = signalLight.transform.Find("TurnLeft").gameObject;
		turnRightLightShow = signalLight.transform.Find("TurnRight").gameObject;
	}
}
