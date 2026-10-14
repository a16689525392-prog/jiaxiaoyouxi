using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.UI;
using View;

public class YouDaoKu : MonoBehaviour
{
	private GameObject Duidian;

	private GameObject TiShiLine;

	private GameObject Frame;

	private GameObject CarCamera;

	private GameObject Scenethirdduidian;

	private GameObject Scenezuohoushijingkuweixian;

	private GameObject Scenedaokuhouxian;

	private GameObject Mainzuohoushijingduidian;

	private GameObject YouView;

	private GameObject ZuoView;

	private GameObject ZuoDuiXianView;

	private GameObject DuiXianCamera;

	private GameObject LeftReviewCameraBig;

	private GameObject RightReviewCameraBig;

	private GameObject LeftReviewCamera;

	private GameObject RightReviewCamera;

	public bool flashingstate;

	private void Start()
	{
		Duidian = GameObject.Find("DuiDian");
		TiShiLine = GameObject.Find("TiShiLine");
		Frame = GameObject.Find("Frame");
		CarCamera = GameObject.Find("CarCamera");
		Scenethirdduidian = Duidian.transform.GetChild(2).gameObject;
		Scenezuohoushijingkuweixian = Duidian.transform.GetChild(3).gameObject;
		Scenedaokuhouxian = Duidian.transform.GetChild(4).gameObject;
		ZuoView = TiShiLine.transform.GetChild(3).gameObject;
		YouView = TiShiLine.transform.GetChild(4).gameObject;
		ZuoDuiXianView = TiShiLine.transform.GetChild(8).gameObject;
		LeftReviewCamera = CarCamera.transform.GetChild(0).gameObject;
		RightReviewCamera = CarCamera.transform.GetChild(1).gameObject;
		LeftReviewCameraBig = CarCamera.transform.GetChild(11).gameObject;
		RightReviewCameraBig = CarCamera.transform.GetChild(12).gameObject;
		DuiXianCamera = CarCamera.transform.GetChild(13).gameObject;
		Mainzuohoushijingduidian = Frame.transform.GetChild(4).gameObject;
		flashingstate = false;
		InvokeRepeating("FlashingState", 0.1f, 0.5f);
		SoundCltGai.Instance.SceneMenuViewDestroy = false;
	}

	private void FixedUpdate()
	{
		if (SoundCltGai.Instance.linshibianliang == 1)
		{
			YouDaoKuZong();
		}
	}

	public void YouDaoKu0()
	{
		if (SoundCltGai.Instance.index == -2f && SoundCltGai.Instance.chongzhi)
		{
			SoundPlayCtl.Instance.PlaySound("右打满,倒车,看右镜,对点停车");
			RightReviewCamera.gameObject.SetActive(value: false);
			RightReviewCameraBig.gameObject.SetActive(value: true);
			YouView.gameObject.SetActive(value: true);
			ZuoView.gameObject.SetActive(value: false);
			ZuoDuiXianView.gameObject.SetActive(value: false);
			DuiXianCamera.gameObject.SetActive(value: false);
			Scenethirdduidian.gameObject.SetActive(value: true);
			SoundCltGai.Instance.index = 0f;
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
		if (SoundCltGai.Instance.index != 0f)
		{
			return;
		}
		if ((double)SoundCltGai.Instance.car.transform.position.x < -1332.74 && (double)SoundCltGai.Instance.car.transform.position.x > -1332.95 && (double)SoundCltGai.Instance.car.transform.position.z < -120.95 && (double)SoundCltGai.Instance.car.transform.position.z > -121.25 && SoundCltGai.Instance.car.transform.eulerAngles.y >= 13f && SoundCltGai.Instance.car.transform.eulerAngles.y < 19f && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 1f;
			}
		}
		else if (!SoundCltGai.Instance.SceneMenuViewDestroy)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
	}

	public void YouDaoKu1()
	{
		if (SoundCltGai.Instance.index == 1f && SoundCltGai.Instance.chongzhi)
		{
			RightReviewCamera.gameObject.SetActive(value: true);
			RightReviewCameraBig.gameObject.SetActive(value: false);
			LeftReviewCamera.gameObject.SetActive(value: false);
			LeftReviewCameraBig.gameObject.SetActive(value: true);
			YouView.gameObject.SetActive(value: false);
			ZuoView.gameObject.SetActive(value: true);
			Scenethirdduidian.gameObject.SetActive(value: false);
			Scenedaokuhouxian.gameObject.SetActive(value: true);
			SoundPlayCtl.Instance.PlaySound("回半圈,继续倒车,看左镜,当车库底边线露出一个黄线的宽度时,停车");
			SoundCltGai.Instance.index = 1.1f;
		}
		if (SoundCltGai.Instance.index != 1.1f)
		{
			return;
		}
		if ((double)SoundCltGai.Instance.car.transform.position.x < -1332.95 && (double)SoundCltGai.Instance.car.transform.position.x > -1334.05 && (double)SoundCltGai.Instance.car.transform.position.z < -121.95 && (double)SoundCltGai.Instance.car.transform.position.z > -123.05 && ((SoundCltGai.Instance.car.transform.eulerAngles.y >= 0f && (double)SoundCltGai.Instance.car.transform.eulerAngles.y < 1.5) || (double)SoundCltGai.Instance.car.transform.eulerAngles.y >= 358.5) && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 2f;
			}
		}
		else
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
	}

	public void YouDaoKu2()
	{
		if (SoundCltGai.Instance.index == 2f && SoundCltGai.Instance.chongzhi)
		{
			LeftReviewCameraBig.gameObject.SetActive(value: false);
			YouView.gameObject.SetActive(value: false);
			ZuoView.gameObject.SetActive(value: false);
			Scenedaokuhouxian.gameObject.SetActive(value: false);
			SoundPlayCtl.Instance.PlaySound("回正方向,继续倒车,调整车身居中,看左镜底边,对点停车");
			SoundCltGai.Instance.index = 2.1f;
		}
		if (SoundCltGai.Instance.index != 2.1f)
		{
			return;
		}
		flashing(Scenezuohoushijingkuweixian, flashingstate);
		DuiXianCamera.gameObject.SetActive(value: true);
		DuiXianCamera.transform.localPosition = new Vector3(-0.5f, 1.21f, 0.45f);
		DuiXianCamera.transform.localEulerAngles = new Vector3(34f, -49.68f, 0f);
		ZuoDuiXianView.gameObject.SetActive(value: true);
		flashing(Mainzuohoushijingduidian, flashingstate);
		if ((double)SoundCltGai.Instance.car.transform.position.x < -1332.95 && (double)SoundCltGai.Instance.car.transform.position.x > -1334.05 && (double)SoundCltGai.Instance.car.transform.position.z < -125.43 && (double)SoundCltGai.Instance.car.transform.position.z > -125.65 && ((SoundCltGai.Instance.car.transform.eulerAngles.y >= 0f && (double)SoundCltGai.Instance.car.transform.eulerAngles.y < 2.5) || (double)SoundCltGai.Instance.car.transform.eulerAngles.y >= 357.5) && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 3f;
				flashing(Scenezuohoushijingkuweixian, open: false);
				flashing(Mainzuohoushijingduidian, open: false);
				DuiXianCamera.gameObject.SetActive(value: false);
				ZuoDuiXianView.gameObject.SetActive(value: false);
				LeftReviewCamera.gameObject.SetActive(value: true);
				SoundCltGai.Instance.linshibianliang = 2;
				SoundCltGai.Instance.chongzhi = true;
				SoundCltGai.Instance.index = -2f;
				SoundCltGai.Instance.INDEX = 0;
			}
		}
		else
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
	}

	public void YouDaoKuZong()
	{
		YouDaoKu0();
		YouDaoKu1();
		YouDaoKu2();
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
