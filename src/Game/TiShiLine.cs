using Car;
using Scene;
using UnityEngine;
using UnityEngine.UI;
using View;

public class TiShiLine : MonoBehaviour
{
	public GameObject QXchetouright;

	public GameObject QXchetouleft;

	public GameObject chetoucenter;

	public GameObject chetouleft;

	public GameObject yibiaopanduidian;

	public GameObject WindowView;

	public GameObject MirrorLeft;

	public GameObject WindowJianBangView;

	public GameObject WindowViewR;

	public GameObject MirrorLeftCenter;

	public GameObject PDyugua;

	public GameObject WindowViewRHandle;

	public GameObject SJZcarlefttuqi;

	public RawImage WindowViewRawImage;

	public RawImage ZuoViewRawImage;

	public RawImage YouViewRawImage;

	public RawImage WindowViewRRawImage;

	public RawImage WindowViewRHandleRawImage;

	public RawImage WindowJianBangViewRawImage;

	private float flashTime;

	private void Start()
	{
		WindowViewRawImage.texture = CarCameraCtl.Instance.leftCameraRenderTexture;
		ZuoViewRawImage.texture = CarCameraCtl.Instance.leftCameraRenderTexture;
		YouViewRawImage.texture = CarCameraCtl.Instance.rightCameraRenderTexture;
		WindowViewRRawImage.texture = CarCameraCtl.Instance.rightCameraRenderTexture;
		WindowViewRHandleRawImage.texture = CarCameraCtl.Instance.rightCameraRenderTexture;
		WindowJianBangViewRawImage.texture = CarCameraCtl.Instance.leftCameraRenderTexture;
	}

	private void Update()
	{
		if (SceneJumpCtl.Instance.CoursetwoName == CoursetwoScenesName.ShiJiaZhuang)
		{
			chetoucenter.transform.localPosition = new Vector3(-26.45f, 392f, 0f);
		}
		if (SceneJumpCtl.Instance.CoursetwoName == CoursetwoScenesName.NanFeng)
		{
			WindowView.transform.GetChild(3).localPosition = new Vector3(122f, -85f, 0f);
		}
		if (SceneJumpCtl.Instance.CoursetwoName == CoursetwoScenesName.FoShan)
		{
			if (ViewJumpCtl.Instance.itemType == 0)
			{
				WindowJianBangView.transform.GetChild(3).localPosition = new Vector3(50f, -85f, 0f);
			}
			else if (ViewJumpCtl.Instance.itemType == 4)
			{
				WindowView.transform.GetChild(3).localPosition = new Vector3(122f, -85f, 0f);
			}
			else if (ViewJumpCtl.Instance.itemType == 34)
			{
				WindowJianBangView.transform.GetChild(3).localPosition = new Vector3(35.7f, -85f, 0f);
			}
		}
		flashTime += Time.deltaTime;
		if (flashTime >= 1f)
		{
			flashTime = 0f;
		}
		if (QXchetouright.activeSelf)
		{
			QXchetouright.transform.GetChild(0).transform.Rotate(0f, 1f, 0f);
		}
		if (QXchetouleft.activeSelf)
		{
			QXchetouleft.transform.GetChild(0).transform.Rotate(0f, 1f, 0f);
		}
		if (SJZcarlefttuqi.activeSelf)
		{
			SJZcarlefttuqi.transform.GetChild(0).transform.Rotate(0f, 1f, 0f);
		}
		if (chetoucenter.activeSelf)
		{
			chetoucenter.transform.GetChild(0).transform.Rotate(0f, 1f, 0f);
		}
		if (PDyugua.activeSelf)
		{
			PDyugua.transform.GetChild(0).transform.Rotate(0f, 1f, 0f);
		}
		if (chetouleft.activeSelf)
		{
			chetouleft.transform.GetChild(0).transform.Rotate(0f, 1f, 0f);
		}
		if (yibiaopanduidian.activeSelf)
		{
			if (flashTime < 0.5f)
			{
				yibiaopanduidian.transform.GetChild(0).GetComponent<Image>().color = Color.red;
			}
			else if (flashTime >= 0.5f && flashTime < 1f)
			{
				yibiaopanduidian.transform.GetChild(0).GetComponent<Image>().color = Color.blue;
			}
		}
		if (MirrorLeftCenter.activeSelf)
		{
			if (flashTime < 0.5f)
			{
				MirrorLeftCenter.transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 84f / 85f, 1f);
			}
			else if (flashTime >= 0.5f && flashTime < 1f)
			{
				MirrorLeftCenter.transform.GetChild(0).GetComponent<Image>().color = Color.green;
			}
		}
		if (WindowView.activeSelf)
		{
			if (flashTime < 0.5f)
			{
				WindowView.transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 84f / 85f, 1f);
				WindowView.transform.GetChild(3).GetComponent<Image>().color = Color.red;
			}
			else if (flashTime >= 0.5f && flashTime < 1f)
			{
				WindowView.transform.GetChild(0).GetComponent<Image>().color = Color.green;
				WindowView.transform.GetChild(3).GetComponent<Image>().color = Color.blue;
			}
		}
		if (WindowJianBangView.activeSelf)
		{
			if (flashTime < 0.5f)
			{
				WindowJianBangView.transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 84f / 85f, 1f);
				WindowJianBangView.transform.GetChild(3).GetComponent<Image>().color = Color.red;
			}
			else if (flashTime >= 0.5f && flashTime < 1f)
			{
				WindowJianBangView.transform.GetChild(0).GetComponent<Image>().color = Color.green;
				WindowJianBangView.transform.GetChild(3).GetComponent<Image>().color = Color.blue;
			}
		}
		if (WindowViewR.activeSelf)
		{
			if (flashTime < 0.5f)
			{
				WindowViewR.transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 84f / 85f, 1f);
				WindowViewR.transform.GetChild(3).GetComponent<Image>().color = Color.red;
			}
			else if (flashTime >= 0.5f && flashTime < 1f)
			{
				WindowViewR.transform.GetChild(0).GetComponent<Image>().color = Color.green;
				WindowViewR.transform.GetChild(3).GetComponent<Image>().color = Color.blue;
			}
		}
		if (WindowViewRHandle.activeSelf)
		{
			if (flashTime < 0.5f)
			{
				WindowViewRHandle.transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 84f / 85f, 1f);
				WindowViewRHandle.transform.GetChild(3).GetComponent<Image>().color = Color.red;
			}
			else if (flashTime >= 0.5f && flashTime < 1f)
			{
				WindowViewRHandle.transform.GetChild(0).GetComponent<Image>().color = Color.green;
				WindowViewRHandle.transform.GetChild(3).GetComponent<Image>().color = Color.blue;
			}
		}
		if (MirrorLeft.activeSelf)
		{
			if (flashTime < 0.5f)
			{
				MirrorLeft.GetComponent<Image>().color = Color.red;
			}
			else if (flashTime >= 0.5f && flashTime < 1f)
			{
				MirrorLeft.GetComponent<Image>().color = Color.yellow;
			}
		}
		if (Screen.width / Screen.height <= 1)
		{
			WindowView.transform.localPosition = new Vector3(-560f, 313f, 0f);
			base.transform.GetChild(1).localPosition = new Vector3(-12.5f, -87f, 0f);
			base.transform.GetChild(2).localPosition = new Vector3(-38f, -275f, 0f);
			WindowViewR.transform.localPosition = new Vector3(290f, 404f, 0f);
			WindowViewRHandle.transform.localPosition = new Vector3(290f, 404f, 0f);
			WindowJianBangView.transform.localPosition = new Vector3(-560f, 313f, 0f);
		}
	}
}
