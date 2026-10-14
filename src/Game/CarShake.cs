using Car;
using Global;
using UnityEngine;

public class CarShake : MonoBehaviour
{
	public static CarShake Instance;

	public GameObject shakeGo;

	private int count;

	public bool isShake;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (SettingInfoCtl.Instance.carType == CarType.DongFengDuoLiKa)
		{
			shakeGo.transform.localPosition = new Vector3(120f, -10f, 0f);
		}
		else if (SettingInfoCtl.Instance.carType == CarType.DaBaChe)
		{
			shakeGo.transform.localPosition = new Vector3(100f, -10f, 0f);
		}
		else if (SettingInfoCtl.Instance.carType == CarType.KaLuoLa)
		{
			shakeGo.transform.localPosition = new Vector3(100f, -10f, 0f);
		}
		else if (SettingInfoCtl.Instance.carType == CarType.JieDaManual || SettingInfoCtl.Instance.carType == CarType.JieDaAuto)
		{
			shakeGo.transform.localPosition = new Vector3(280f, -115f, 0f);
		}
		else if (SettingInfoCtl.Instance.carType == CarType.AiLiSheManual || SettingInfoCtl.Instance.carType == CarType.AiLiSheAuto)
		{
			shakeGo.transform.localPosition = new Vector3(42f, -22f, 0f);
		}
		else if (SettingInfoCtl.Instance.carType == CarType.PiKa)
		{
			if (Screen.width / Screen.height >= 3)
			{
				shakeGo.transform.localPosition = new Vector3(66f, -10f, 0f);
			}
			else
			{
				shakeGo.transform.localPosition = new Vector3(55f, -10f, 0f);
			}
		}
		else if (SettingInfoCtl.Instance.carType == CarType.TianJin)
		{
			if (Screen.width / Screen.height >= 3)
			{
				shakeGo.transform.localPosition = new Vector3(-50f, -10f, 0f);
			}
			else
			{
				shakeGo.transform.localPosition = new Vector3(0f, -10f, 0f);
			}
		}
		else if (SettingInfoCtl.Instance.carType == CarType.JieFang1125)
		{
			if (Screen.width / Screen.height >= 3)
			{
				shakeGo.transform.localPosition = new Vector3(60f, -10f, 0f);
			}
			else
			{
				shakeGo.transform.localPosition = new Vector3(0f, -10f, 0f);
			}
		}
		else if (SettingInfoCtl.Instance.carType == CarType.YongShi)
		{
			shakeGo.transform.localPosition = new Vector3(78f, -10f, 0f);
		}
		else
		{
			shakeGo.transform.localPosition = new Vector3(0f, -10f, 0f);
		}
		if (isShake)
		{
			count++;
			if (count % 2 == 0)
			{
				shakeGo.transform.localPosition -= new Vector3(0f, 2f, 0f);
			}
			else
			{
				shakeGo.transform.localPosition += new Vector3(0f, 2f, 0f);
			}
		}
	}

	public void StartCarShake()
	{
		isShake = true;
		count = 0;
	}

	public void StopCarShake()
	{
		isShake = false;
	}
}
