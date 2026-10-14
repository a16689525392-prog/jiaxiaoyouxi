using SoundAndTextShow;
using UnityEngine;
using View;

public class AICarMove : MonoBehaviour
{
	public static GameObject aicar;

	public static bool start;

	public static bool isEndOver;

	public static int index;

	public float aispeed;

	public static float soundTime;

	public static bool isInitCar;

	public static bool isSound;

	public static bool isSound1;

	public static bool isSound2;

	public static bool isSound3;

	private void Start()
	{
		index = 0;
		aispeed = 1f;
	}

	private void Update()
	{
		if (ViewJumpCtl.Instance.itemType != 5110)
		{
			return;
		}
		if (!isInitCar)
		{
			aicar = Object.Instantiate(Resources.Load("AppearPerson/AICar11")) as GameObject;
			GameObject gameObject = GameObject.Find("CarBornPosition");
			aicar.transform.parent = gameObject.transform;
			aicar.transform.localPosition = new Vector3(0f, 0f, 0f);
			aicar.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
			isInitCar = true;
		}
		if (start)
		{
			aicar.transform.position += aicar.transform.forward * 0.27f * aispeed;
		}
		if (Vector3.Distance(base.transform.position, aicar.transform.position) < 10f && index == 0)
		{
			index = 1;
		}
		if (index == 1)
		{
			aicar.transform.eulerAngles += new Vector3(0f, -1.5f, 0f);
			aispeed = 0.8f;
			/*Error near IL_0161: Invalid field token*/;
		}
		if (index == 2)
		{
			aicar.transform.eulerAngles += new Vector3(0f, 1.5f, 0f);
			if (aicar.transform.eulerAngles.y > 184.5f)
			{
				aispeed = 1f;
				index = 3;
				aicar.transform.eulerAngles = new Vector3(0f, 184.5f, 0f);
			}
		}
		if (index == 3 && Vector3.Distance(base.transform.position, aicar.transform.position) > 10f)
		{
			index = 4;
		}
		if (index == 4 && base.transform.position.z > aicar.transform.position.z)
		{
			aispeed = 1.5f;
			aicar.transform.eulerAngles += new Vector3(0f, 1.5f, 0f);
			if (aicar.transform.eulerAngles.y > 226.5f)
			{
				index = 5;
			}
		}
		if (index == 5)
		{
			aicar.transform.eulerAngles += new Vector3(0f, -3f, 0f);
			if (aicar.transform.eulerAngles.y < 184.5f)
			{
				aispeed = 1f;
				index = 6;
				aicar.transform.eulerAngles = new Vector3(0f, 184.5f, 0f);
			}
		}
		if (index == 6)
		{
			soundTime += Time.deltaTime;
			if (soundTime > 3f && !isEndOver)
			{
				SoundPlayCtl.Instance.PlaySound("项目结束");
				isEndOver = true;
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (ViewJumpCtl.Instance.itemType == 5110)
		{
			if (other.name == "Start_ChaoChe" && !isSound)
			{
				SoundPlayCtl.Instance.PlaySound("让超车项目开始");
				isSound = true;
			}
			else if (other.name == "AiCarBorn" && !isSound1)
			{
				start = true;
				SoundPlayCtl.Instance.PlaySound("后方有车辆将要超车,请勿加速");
				isSound1 = true;
			}
		}
		if (ViewJumpCtl.Instance.itemType == 5111)
		{
			if (other.name == "Start_GenChe" && !isSound)
			{
				SoundPlayCtl.Instance.PlaySound("跟车项目开始,请保持安全车距");
				isSound = true;
			}
			else if (other.name == "SafeDistance" && !isSound1)
			{
				SoundPlayCtl.Instance.PlaySound("未与前车保持安全车距", 100f);
				isSound1 = true;
			}
			else if (other.name == "OverCar" && !isSound2)
			{
				SoundPlayCtl.Instance.PlaySound("违法超车", 100f);
				isSound2 = true;
			}
			else if (other.name == "End" && !isSound3)
			{
				SoundPlayCtl.Instance.PlaySound("项目结束");
				isSound3 = true;
			}
		}
	}

	public void OnDestroy()
	{
		start = false;
		isSound = false;
		isSound1 = false;
		isSound2 = false;
		isSound3 = false;
		isEndOver = false;
		soundTime = 0f;
		isInitCar = false;
		index = 0;
	}
}
