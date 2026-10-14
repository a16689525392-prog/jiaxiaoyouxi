using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.UI;
using View;

public class YouChuKu : MonoBehaviour
{
	private GameObject Duidian;

	private GameObject TiShiLine;

	private GameObject Frame;

	private GameObject CarCamera;

	private GameObject YouDuiXianView;

	private GameObject DuiXianCamera;

	private GameObject RightReviewCamera;

	private GameObject Mainyouchuangjiao;

	private GameObject Sceneyouhoushijingkuweixian;

	public bool flashingstate;

	private void Start()
	{
		Duidian = GameObject.Find("DuiDian");
		TiShiLine = GameObject.Find("TiShiLine");
		Frame = GameObject.Find("Frame");
		CarCamera = GameObject.Find("CarCamera");
		YouDuiXianView = TiShiLine.transform.GetChild(9).gameObject;
		Mainyouchuangjiao = Frame.transform.GetChild(7).gameObject;
		RightReviewCamera = CarCamera.transform.GetChild(1).gameObject;
		DuiXianCamera = CarCamera.transform.GetChild(13).gameObject;
		Sceneyouhoushijingkuweixian = Duidian.transform.GetChild(15).gameObject;
		flashingstate = false;
		InvokeRepeating("FlashingState", 0.1f, 0.5f);
		SoundCltGai.Instance.SceneMenuViewDestroy = false;
	}

	private void FixedUpdate()
	{
		if (SoundCltGai.Instance.linshibianliang == 3)
		{
			YouChuKuZong();
		}
	}

	public void YouChuKu0()
	{
		if (SoundCltGai.Instance.index < 1f)
		{
			flashing(Mainyouchuangjiao, flashingstate);
			flashing(Sceneyouhoushijingkuweixian, flashingstate);
		}
		if (SoundCltGai.Instance.index == -2f && SoundCltGai.Instance.chongzhi)
		{
			SoundPlayCtl.Instance.PlaySound("扶正方向,前进,看右窗外,当黄线过右窗角时,停车");
			RightReviewCamera.gameObject.SetActive(value: false);
			DuiXianCamera.gameObject.SetActive(value: true);
			DuiXianCamera.transform.localPosition = new Vector3(0f, 1.25f, 0.1f);
			DuiXianCamera.transform.localEulerAngles = new Vector3(22.5f, 90f, 0f);
			YouDuiXianView.gameObject.SetActive(value: true);
			SoundCltGai.Instance.index = 0f;
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
		if (SoundCltGai.Instance.index != 0f)
		{
			return;
		}
		if ((double)SoundCltGai.Instance.car.transform.position.x < -1333.2 && (double)SoundCltGai.Instance.car.transform.position.x > -1333.9 && (double)SoundCltGai.Instance.car.transform.position.z < -122.6 && (double)SoundCltGai.Instance.car.transform.position.z > -123.05 && ((SoundCltGai.Instance.car.transform.eulerAngles.y >= 0f && (double)SoundCltGai.Instance.car.transform.eulerAngles.y < 2.5) || (double)SoundCltGai.Instance.car.transform.eulerAngles.y >= 357.5) && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 1f;
				flashing(Mainyouchuangjiao, open: false);
				flashing(Sceneyouhoushijingkuweixian, open: false);
				YouDuiXianView.gameObject.SetActive(value: false);
				RightReviewCamera.gameObject.SetActive(value: true);
				DuiXianCamera.gameObject.SetActive(value: false);
			}
		}
		else if (!SoundCltGai.Instance.SceneMenuViewDestroy)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
	}

	public void YouChuKu1()
	{
		if (SoundCltGai.Instance.index == 1f && SoundCltGai.Instance.chongzhi)
		{
			SoundPlayCtl.Instance.PlaySound("停车,右一圈加90度,继续前进,车身转正后方向盘回正");
			SoundCltGai.Instance.index = 1.1f;
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "";
		}
		if (SoundCltGai.Instance.index == 1.1f && SoundCltGai.Instance.car.transform.position.x > -1327f && SoundCltGai.Instance.car.transform.position.z < -117f && SoundCltGai.Instance.car.transform.position.z > -123f && SoundCltGai.Instance.car.transform.eulerAngles.y >= 85f && SoundCltGai.Instance.car.transform.eulerAngles.y < 95f && SoundCltGai.Instance.chongzhi)
		{
			SoundCltGai.Instance.index = 2f;
			SoundCltGai.Instance.linshibianliang = 4;
			SoundCltGai.Instance.chongzhi = true;
			SoundCltGai.Instance.index = -2f;
			SoundCltGai.Instance.INDEX = 0;
		}
	}

	public void YouChuKuZong()
	{
		YouChuKu0();
		YouChuKu1();
		if (MoNiCarDynamic.Instance.dynamicData.engineIsFired)
		{
			SoundCltGai.Instance.fire = true;
		}
		if (SoundCltGai.Instance.fire && SoundCltGai.Instance.chongzhi && !MoNiCarDynamic.Instance.dynamicData.engineIsFired)
		{
			SoundCltGai.Instance.chongzhi = false;
			SoundCltGai.Instance.CWTS = "熄火,请重置";
		}
		if (SoundCltGai.Instance.YaXian && SoundCltGai.Instance.chongzhi)
		{
			SoundCltGai.Instance.chongzhi = false;
			SoundCltGai.Instance.CWTS = "压线,请重置";
		}
	}

	public void flashing(GameObject go, bool open)
	{
		if (open)
		{
			go.gameObject.SetActive(value: true);
		}
		else
		{
			go.gameObject.SetActive(value: false);
		}
	}

	public void FlashingState()
	{
		if (flashingstate)
		{
			flashingstate = false;
		}
		else
		{
			flashingstate = true;
		}
	}

	private void OnDestroy()
	{
		SoundCltGai.Instance.fire = false;
	}
}
