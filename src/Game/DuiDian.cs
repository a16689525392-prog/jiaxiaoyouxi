using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.UI;
using View;

public class DuiDian : MonoBehaviour
{
	private GameObject Duidian;

	private GameObject TiShiLine;

	private GameObject Frame;

	private GameObject CarCamera;

	private GameObject SceneTwoAndThreeCenter;

	private GameObject Scenezuohoushijingduidian;

	private GameObject Scenethirdduidian;

	private GameObject Scenezuohoushijingkuweixian;

	private GameObject Mainyibiaopanduidian;

	private GameObject Mainchetouduidian;

	private GameObject Mainzuohoushijingduidian;

	private GameObject zuojianxian;

	private GameObject ZuoDuiXianView;

	private GameObject DuiXianCamera;

	private GameObject LeftReviewCamera;

	public bool flashingstate;

	private void Start()
	{
		SoundCltGai.Instance.linshibianliang = 0;
		Duidian = GameObject.Find("DuiDian");
		TiShiLine = GameObject.Find("TiShiLine");
		Frame = GameObject.Find("Frame");
		CarCamera = GameObject.Find("CarCamera");
		SceneTwoAndThreeCenter = Duidian.transform.GetChild(0).gameObject;
		Scenezuohoushijingduidian = Duidian.transform.GetChild(1).gameObject;
		Scenethirdduidian = Duidian.transform.GetChild(2).gameObject;
		Scenezuohoushijingkuweixian = Duidian.transform.GetChild(3).gameObject;
		Mainyibiaopanduidian = TiShiLine.transform.GetChild(0).gameObject;
		Mainchetouduidian = TiShiLine.transform.GetChild(1).gameObject;
		ZuoDuiXianView = TiShiLine.transform.GetChild(8).gameObject;
		DuiXianCamera = CarCamera.transform.GetChild(13).gameObject;
		LeftReviewCamera = CarCamera.transform.GetChild(0).gameObject;
		zuojianxian = Frame.transform.GetChild(3).gameObject;
		Mainzuohoushijingduidian = Frame.transform.GetChild(4).gameObject;
		zuojianxian.gameObject.SetActive(value: false);
		flashingstate = false;
		InvokeRepeating("FlashingState", 0.1f, 0.5f);
	}

	private void FixedUpdate()
	{
		if (SoundCltGai.Instance.linshibianliang == 0)
		{
			DuiDianZong();
		}
	}

	private void chongzhibutton()
	{
		zuojianxian.gameObject.SetActive(value: false);
		flashing(Scenezuohoushijingduidian, open: false);
		flashing(Mainzuohoushijingduidian, open: false);
		SettingInfoCtl.Instance.curViewPoint = 0;
		CarCameraCtl.Instance.ShowViewPoint(SettingInfoCtl.Instance.curViewPoint);
		SettingInfoCtl.Instance.SaveSetingInfos();
	}

	public void DuiDian0()
	{
		if (SoundCltGai.Instance.index < 1f)
		{
			flashing(SceneTwoAndThreeCenter, flashingstate);
			flashing(Mainyibiaopanduidian, flashingstate);
		}
		if (SoundCltGai.Instance.index == -2f && SoundCltGai.Instance.chongzhi)
		{
			chongzhibutton();
			SoundPlayCtl.Instance.PlaySound("前进,仪表盘中心对点");
			SoundCltGai.Instance.index = 0f;
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
		if (SoundCltGai.Instance.index == 0f && SoundCltGai.Instance.car.transform.position.x < -1332f && (double)SoundCltGai.Instance.car.transform.position.x > -1332.95 && (double)SoundCltGai.Instance.car.transform.position.z < -119.35 && (double)SoundCltGai.Instance.car.transform.position.z > -119.85 && SoundCltGai.Instance.car.transform.eulerAngles.y >= 85f && SoundCltGai.Instance.car.transform.eulerAngles.y <= 95f && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			SoundCltGai.Instance.index = 1f;
		}
	}

	public void DuiDian1()
	{
		if (SoundCltGai.Instance.index >= 1f && (double)SoundCltGai.Instance.index <= 1.1)
		{
			flashing(Mainyibiaopanduidian, open: false);
			flashing(SceneTwoAndThreeCenter, flashingstate);
		}
		if (SoundCltGai.Instance.index == 1f && SoundCltGai.Instance.chongzhi)
		{
			SoundPlayCtl.Instance.PlaySound("前进,当车头挡住点位时,看左窗外,当肩对黄虚线时,停车");
			SoundCltGai.Instance.index = 1.1f;
		}
		if (SoundCltGai.Instance.index == 1.1f && (double)SoundCltGai.Instance.car.transform.position.x >= -1331.9 && (double)SoundCltGai.Instance.car.transform.position.z < -119.35 && (double)SoundCltGai.Instance.car.transform.position.z > -119.85 && SoundCltGai.Instance.car.transform.eulerAngles.y >= 87f && SoundCltGai.Instance.car.transform.eulerAngles.y <= 93f && SoundCltGai.Instance.chongzhi)
		{
			flashing(SceneTwoAndThreeCenter, open: false);
			zuojianxian.gameObject.SetActive(value: true);
			flashing(Scenezuohoushijingduidian, flashingstate);
			DuiXianCamera.gameObject.SetActive(value: true);
			LeftReviewCamera.gameObject.SetActive(value: false);
			DuiXianCamera.transform.localPosition = new Vector3(-0.5f, 1.21f, 0.45f);
			DuiXianCamera.transform.localEulerAngles = new Vector3(36f, -90f, 0f);
			ZuoDuiXianView.gameObject.SetActive(value: true);
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
			SoundCltGai.Instance.index = 1.2f;
		}
		if (SoundCltGai.Instance.index != 1.2f)
		{
			return;
		}
		flashing(Scenezuohoushijingduidian, flashingstate);
		if ((double)SoundCltGai.Instance.car.transform.position.x >= -1326.6 && (double)SoundCltGai.Instance.car.transform.position.x <= -1326.35 && (double)SoundCltGai.Instance.car.transform.position.z < -119.35 && (double)SoundCltGai.Instance.car.transform.position.z > -119.85 && SoundCltGai.Instance.car.transform.eulerAngles.y >= 88f && SoundCltGai.Instance.car.transform.eulerAngles.y < 92f && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 2f;
				DuiXianCamera.transform.localPosition = new Vector3(-0.5f, 1.21f, 0.45f);
				DuiXianCamera.transform.localEulerAngles = new Vector3(34f, -49.68f, 0f);
			}
		}
		else
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
		}
	}

	public void DuiDian2()
	{
		if (SoundCltGai.Instance.index >= 2f && SoundCltGai.Instance.index < 3f)
		{
			zuojianxian.gameObject.SetActive(value: false);
			flashing(Scenezuohoushijingduidian, flashingstate);
			flashing(Mainzuohoushijingduidian, flashingstate);
		}
		if (SoundCltGai.Instance.index == 2f && SoundCltGai.Instance.chongzhi)
		{
			SoundPlayCtl.Instance.PlaySound("倒车,看左镜底,对点停车");
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "还未对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.red;
			SoundCltGai.Instance.index = 2.1f;
		}
		if (SoundCltGai.Instance.index != 2.1f)
		{
			return;
		}
		if ((double)SoundCltGai.Instance.car.transform.position.x >= -1328.35 && (double)SoundCltGai.Instance.car.transform.position.x <= -1328.15 && (double)SoundCltGai.Instance.car.transform.position.z < -119.35 && (double)SoundCltGai.Instance.car.transform.position.z > -119.85 && SoundCltGai.Instance.car.transform.eulerAngles.y >= 87f && SoundCltGai.Instance.car.transform.eulerAngles.y <= 93f && SoundCltGai.Instance.chongzhi)
		{
			SceneMenuView.Instance.DXTS.GetComponent<Text>().text = "已对上";
			SceneMenuView.Instance.DXTS.GetComponent<Text>().color = Color.green;
			if (SoundCltGai.Instance.car.velocity.magnitude < 0.05f)
			{
				SoundCltGai.Instance.index = 3f;
				ZuoDuiXianView.gameObject.SetActive(value: false);
				DuiXianCamera.gameObject.SetActive(value: false);
				LeftReviewCamera.gameObject.SetActive(value: true);
				flashing(Mainzuohoushijingduidian, open: false);
				flashing(Scenezuohoushijingduidian, open: false);
				SoundCltGai.Instance.linshibianliang = 1;
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

	public void DuiDianZong()
	{
		DuiDian0();
		DuiDian1();
		DuiDian2();
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
