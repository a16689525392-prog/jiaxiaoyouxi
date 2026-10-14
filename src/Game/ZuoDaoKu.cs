using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.UI;
using View;

public class ZuoDaoKu : MonoBehaviour
{
	private GameObject Duidian;

	private GameObject TiShiLine;

	private GameObject Frame;

	private GameObject CarCamera;

	private GameObject YouView;

	private GameObject ZuoView;

	private GameObject ZuoDuiXianView;

	private GameObject DuiXianCamera;

	private GameObject LeftReviewCameraBig;

	private GameObject RightReviewCameraBig;

	private GameObject LeftReviewCamera;

	private GameObject RightReviewCamera;

	private GameObject Mainzuochuangjiao;

	private GameObject Scenezuohoushijingkuweixian;

	private GameObject Mainzuohoushijingduidian;

	private GameObject Scenechuanguozuoyixian;

	private GameObject Mainzuoyixian;

	private GameObject Scenezhijiaodian;

	private GameObject Scenezhijiaohoudian;

	private GameObject Scenehuangxiankuandian;

	public bool flashingstate;

	private void Start()
	{
		Duidian = GameObject.Find("DuiDian");
		TiShiLine = GameObject.Find("TiShiLine");
		Frame = GameObject.Find("Frame");
		CarCamera = GameObject.Find("CarCamera");
		Mainzuohoushijingduidian = Frame.transform.GetChild(4).gameObject;
		Mainzuochuangjiao = Frame.transform.GetChild(5).gameObject;
		Mainzuoyixian = Frame.transform.GetChild(6).gameObject;
		Scenezuohoushijingkuweixian = Duidian.transform.GetChild(3).gameObject;
		Scenechuanguozuoyixian = Duidian.transform.GetChild(11).gameObject;
		Scenezhijiaodian = Duidian.transform.GetChild(12).gameObject;
		Scenezhijiaohoudian = Duidian.transform.GetChild(13).gameObject;
		Scenehuangxiankuandian = Duidian.transform.GetChild(14).gameObject;
		ZuoView = TiShiLine.transform.GetChild(3).gameObject;
		YouView = TiShiLine.transform.GetChild(4).gameObject;
		ZuoDuiXianView = TiShiLine.transform.GetChild(8).gameObject;
		LeftReviewCamera = CarCamera.transform.GetChild(0).gameObject;
		RightReviewCamera = CarCamera.transform.GetChild(1).gameObject;
		LeftReviewCameraBig = CarCamera.transform.GetChild(11).gameObject;
		RightReviewCameraBig = CarCamera.transform.GetChild(12).gameObject;
		DuiXianCamera = CarCamera.transform.GetChild(13).gameObject;
		flashingstate = false;
		InvokeRepeating("FlashingState", 0.1f, 0.5f);
		SoundCltGai.Instance.SceneMenuViewDestroy = false;
		RightReviewCameraBig.transform.localPosition = new Vector3(0.79f, 1.14f, 0.72f);
		RightReviewCameraBig.transform.localEulerAngles = new Vector3(19.1f, 176.4f, 0f);
	}

	private void FixedUpdate()
	{
		if (SoundCltGai.Instance.linshibianliang == 2)
		{
			ZuoDaoKuZong();
		}
	}

	private void chongzhibutton()
	{
		flashing(Scenechuanguozuoyixian, open: false);
		flashing(Mainzuoyixian, open: false);
		ZuoView.gameObject.SetActive(value: false);
		flashing(Scenezhijiaodian, open: false);
		flashing(Scenezhijiaohoudian, open: false);
		YouView.gameObject.SetActive(value: false);
		flashing(Scenehuangxiankuandian, open: false);
		flashing(Scenezuohoushijingkuweixian, open: false);
		flashing(Mainzuohoushijingduidian, open: false);
	}

	public void ZuoDaoKu0()
	{
		if (SoundCltGai.Instance.index < 1f)
		{
			flashing(Mainzuochuangjiao, flashingstate);
			flashing(Scenezuohoushijingkuweixian, flashingstate);
		}
		if (SoundCltGai.Instance.index == -2f && SoundCltGai.Instance.chongzhi)
		{
			chongzhibutton();
			SoundPlayCtl.Instance.PlaySound("扶正方向,前进,看左窗外,黄线过窗角时,停车");
			LeftReviewCamera.gameObject.SetActive(value: false);
			DuiXianCamera.gameObject.SetActive(value: true);
			DuiXianCamera.transform.localPosition = new Vector3(-0.4f, 1.23f, 0.03f);
			DuiXianCamera.transform.localEulerAngles = new Vector3(28f, -98f, 0f);
			ZuoDuiXianView.gameObject.SetActive(value: true);
			SoundCltGai.Instance.index = 0f;
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
		if (SoundCltGai.Instance.index != 0f)
		{
			return;
		}
		if ((double)SoundCltGai.Instance.car.transform.position.x < -1333.2 && (double)SoundCltGai.Instance.car.transform.position.x > -1333.9 && (double)SoundCltGai.Instance.car.transform.position.z < -122.85 && (double)SoundCltGai.Instance.car.transform.position.z > -123.05 && ((SoundCltGai.Instance.car.transform.eulerAngles.y >= 0f && (double)SoundCltGai.Instance.car.transform.eulerAngles.y < 2.5) || (double)SoundCltGai.Instance.car.transform.eulerAngles.y >= 357.5) && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 1f;
				flashing(Mainzuochuangjiao, open: false);
				flashing(Scenezuohoushijingkuweixian, open: false);
			}
		}
		else if (!SoundCltGai.Instance.SceneMenuViewDestroy)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
	}

	public void ZuoDaoKu1()
	{
		if (SoundCltGai.Instance.index == 1f && SoundCltGai.Instance.chongzhi)
		{
			DuiXianCamera.transform.localPosition = new Vector3(-0.35f, 1.23f, 0.26f);
			DuiXianCamera.transform.localEulerAngles = new Vector3(30.3f, -66.5f, 0f);
			SoundPlayCtl.Instance.PlaySound("停车,左一圈加90度,继续前进,当座位到黄虚线上方时,停车");
			SoundCltGai.Instance.index = 1.1f;
		}
		if (SoundCltGai.Instance.index != 1.1f)
		{
			return;
		}
		flashing(Scenechuanguozuoyixian, flashingstate);
		flashing(Mainzuoyixian, flashingstate);
		if ((double)SoundCltGai.Instance.car.transform.position.x < -1339.5 && (double)SoundCltGai.Instance.car.transform.position.x > -1341.5 && SoundCltGai.Instance.car.transform.position.z < -119f && SoundCltGai.Instance.car.transform.position.z > -121f && SoundCltGai.Instance.car.transform.eulerAngles.y >= 251f && SoundCltGai.Instance.car.transform.eulerAngles.y < 257f && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 2f;
				flashing(Scenechuanguozuoyixian, open: false);
				flashing(Mainzuoyixian, open: false);
			}
		}
		else
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
	}

	public void ZuoDaoKu2()
	{
		if (SoundCltGai.Instance.index == 2f && SoundCltGai.Instance.chongzhi)
		{
			ZuoDuiXianView.gameObject.SetActive(value: false);
			DuiXianCamera.gameObject.SetActive(value: false);
			LeftReviewCameraBig.gameObject.SetActive(value: true);
			ZuoView.gameObject.SetActive(value: true);
			SoundPlayCtl.Instance.PlaySound("停车,保持方向不动,挂倒挡,后退,看左镜,对点停车");
			SoundCltGai.Instance.index = 2.1f;
		}
		if (SoundCltGai.Instance.index != 2.1f)
		{
			return;
		}
		flashing(Scenezhijiaodian, flashingstate);
		if ((double)SoundCltGai.Instance.car.transform.position.x < -1335.8 && (double)SoundCltGai.Instance.car.transform.position.x > -1336.5 && (double)SoundCltGai.Instance.car.transform.position.z < -119.5 && (double)SoundCltGai.Instance.car.transform.position.z > -119.9 && (double)SoundCltGai.Instance.car.transform.eulerAngles.y >= 308.8 && (double)SoundCltGai.Instance.car.transform.eulerAngles.y < 311.8 && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 3f;
				flashing(Scenezhijiaodian, open: false);
			}
		}
		else if (!SoundCltGai.Instance.SceneMenuViewDestroy)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
	}

	public void ZuoDaoKu3()
	{
		if (SoundCltGai.Instance.index == 3f && SoundCltGai.Instance.chongzhi)
		{
			SoundPlayCtl.Instance.PlaySound("停车,左打满,继续倒车,看左镜,对点停车");
			SoundCltGai.Instance.index = 3.1f;
		}
		if (SoundCltGai.Instance.index != 3.1f)
		{
			return;
		}
		flashing(Scenezhijiaohoudian, flashingstate);
		if ((double)SoundCltGai.Instance.car.transform.position.x < -1334.1 && (double)SoundCltGai.Instance.car.transform.position.x > -1335.1 && (double)SoundCltGai.Instance.car.transform.position.z < -120.1 && (double)SoundCltGai.Instance.car.transform.position.z > -121.1 && (double)SoundCltGai.Instance.car.transform.eulerAngles.y >= 332.8 && (double)SoundCltGai.Instance.car.transform.eulerAngles.y < 338.5 && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 4f;
				flashing(Scenezhijiaohoudian, open: false);
			}
		}
		else if (!SoundCltGai.Instance.SceneMenuViewDestroy)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
	}

	public void ZuoDaoKu4()
	{
		if (SoundCltGai.Instance.index == 4f && SoundCltGai.Instance.chongzhi)
		{
			ZuoView.gameObject.SetActive(value: false);
			LeftReviewCamera.gameObject.SetActive(value: true);
			LeftReviewCameraBig.gameObject.SetActive(value: false);
			RightReviewCamera.gameObject.SetActive(value: false);
			RightReviewCameraBig.gameObject.SetActive(value: true);
			YouView.gameObject.SetActive(value: true);
			SoundPlayCtl.Instance.PlaySound("停车,回半圈,继续倒车,看右镜,车库底边线露出一个黄线宽度时,停车");
			SoundCltGai.Instance.index = 4.1f;
		}
		if (SoundCltGai.Instance.index != 4.1f)
		{
			return;
		}
		flashing(Scenehuangxiankuandian, flashingstate);
		if ((double)SoundCltGai.Instance.car.transform.position.x < -1333.2 && (double)SoundCltGai.Instance.car.transform.position.x > -1333.9 && SoundCltGai.Instance.car.transform.position.z < -123f && (double)SoundCltGai.Instance.car.transform.position.z > -123.9 && ((SoundCltGai.Instance.car.transform.eulerAngles.y >= 0f && (double)SoundCltGai.Instance.car.transform.eulerAngles.y < 1.5) || (double)SoundCltGai.Instance.car.transform.eulerAngles.y >= 358.5) && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 5f;
				flashing(Scenehuangxiankuandian, open: false);
				YouView.gameObject.SetActive(value: false);
				RightReviewCamera.gameObject.SetActive(value: true);
				RightReviewCameraBig.gameObject.SetActive(value: false);
			}
		}
		else if (!SoundCltGai.Instance.SceneMenuViewDestroy)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
	}

	public void ZuoDaoKu5()
	{
		if (SoundCltGai.Instance.index == 5f && SoundCltGai.Instance.chongzhi)
		{
			DuiXianCamera.gameObject.SetActive(value: true);
			LeftReviewCamera.gameObject.SetActive(value: false);
			DuiXianCamera.transform.localPosition = new Vector3(-0.5f, 1.21f, 0.45f);
			DuiXianCamera.transform.localEulerAngles = new Vector3(34f, -49.68f, 0f);
			ZuoDuiXianView.gameObject.SetActive(value: true);
			SoundPlayCtl.Instance.PlaySound("回正方向,继续倒车,调整车身居中,看左镜底边,对点停车");
			SoundCltGai.Instance.index = 5.1f;
		}
		if (SoundCltGai.Instance.index != 5.1f)
		{
			return;
		}
		flashing(Scenezuohoushijingkuweixian, flashingstate);
		flashing(Mainzuohoushijingduidian, flashingstate);
		if ((double)SoundCltGai.Instance.car.transform.position.x < -1332.95 && (double)SoundCltGai.Instance.car.transform.position.x > -1334.05 && (double)SoundCltGai.Instance.car.transform.position.z < -125.43 && (double)SoundCltGai.Instance.car.transform.position.z > -125.65 && ((SoundCltGai.Instance.car.transform.eulerAngles.y >= 0f && (double)SoundCltGai.Instance.car.transform.eulerAngles.y < 2.5) || (double)SoundCltGai.Instance.car.transform.eulerAngles.y >= 357.5) && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 6f;
				flashing(Scenezuohoushijingkuweixian, open: false);
				flashing(Mainzuohoushijingduidian, open: false);
				ZuoDuiXianView.gameObject.SetActive(value: false);
				DuiXianCamera.gameObject.SetActive(value: false);
				LeftReviewCamera.gameObject.SetActive(value: true);
				SoundCltGai.Instance.linshibianliang = 3;
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

	public void ZuoDaoKuZong()
	{
		ZuoDaoKu0();
		ZuoDaoKu1();
		ZuoDaoKu2();
		ZuoDaoKu3();
		ZuoDaoKu4();
		ZuoDaoKu5();
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
