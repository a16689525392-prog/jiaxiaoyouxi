using UnityEngine;
using UnityEngine.UI;

namespace Car
{
	public class AddCameraRenderTexture : MonoBehaviour
	{
		public CarCameraType type;

		private void Start()
		{
			SetCameraTexture();
		}

		public void SetCarCameraType(CarCameraType t)
		{
			type = t;
			SetCameraTexture();
		}

		public void SetCameraTexture()
		{
			if (base.transform.GetComponent<RawImage>() != null)
			{
				switch (type)
				{
				case CarCameraType.LeftCamera:
					base.transform.GetComponent<RawImage>().texture = CarCameraCtl.Instance.leftCameraRenderTexture;
					break;
				case CarCameraType.RightCamera:
					base.transform.GetComponent<RawImage>().texture = CarCameraCtl.Instance.rightCameraRenderTexture;
					break;
				case CarCameraType.TopCamera:
					base.transform.GetComponent<RawImage>().texture = CarCameraCtl.Instance.topCameraRenderTexture;
					break;
				case CarCameraType.WheelCamera:
					base.transform.GetComponent<RawImage>().texture = CarCameraCtl.Instance.wheelCameraRenderTexture;
					break;
				case CarCameraType.MainCamera:
					Debug.LogError("没有设置主摄像机的RenderTexture");
					break;
				}
			}
			else if (base.transform.GetComponent<MeshRenderer>() != null)
			{
				switch (type)
				{
				case CarCameraType.LeftCamera:
					base.transform.GetComponent<MeshRenderer>().material.mainTexture = CarCameraCtl.Instance.leftCameraRenderTexture;
					break;
				case CarCameraType.RightCamera:
					base.transform.GetComponent<MeshRenderer>().material.mainTexture = CarCameraCtl.Instance.rightCameraRenderTexture;
					break;
				case CarCameraType.TopCamera:
					base.transform.GetComponent<MeshRenderer>().material.mainTexture = CarCameraCtl.Instance.topCameraRenderTexture;
					break;
				case CarCameraType.WheelCamera:
					base.transform.GetComponent<MeshRenderer>().material.mainTexture = CarCameraCtl.Instance.wheelCameraRenderTexture;
					break;
				case CarCameraType.MainCamera:
					Debug.LogError("没有设置主摄像机的RenderTexture");
					break;
				}
			}
		}
	}
}
