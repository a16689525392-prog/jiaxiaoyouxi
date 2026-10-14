using System.Collections.Generic;
using System.Xml;
using Car;
using Global;
using UnityEngine;
using View;

namespace Scene
{
	public class ViewPointCtl : MonoBehaviour
	{
		private class ViewPoint
		{
			public TrainType mTrainType;

			public int mItemType;

			public List<CameraTransformInfo> cameraInfoList = new List<CameraTransformInfo>();
		}

		private class CameraTransformInfo
		{
			public Vector3 mPositon;

			public Vector3 eulerAngle;

			public float fieldOfView;
		}

		private List<ViewPoint> mViewPointList = new List<ViewPoint>();

		private ViewPoint curViewPoint;

		public static ViewPointCtl Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
			ReadViewPointFile();
		}

		private void Start()
		{
			SetCameraPosition(Camera.main, 0);
		}

		private void ReadViewPointFile()
		{
			XmlDocument xmlDocument = new XmlDocument();
			TextAsset textAsset = Resources.Load<TextAsset>(DataCenter.Instance.GetFilePath(3) + "ViewPoint");
			xmlDocument.LoadXml(textAsset.text);
			foreach (XmlElement childNode in xmlDocument.SelectSingleNode("viewPoints").ChildNodes)
			{
				ViewPoint viewPoint = new ViewPoint();
				foreach (XmlElement childNode2 in childNode.ChildNodes)
				{
					switch (childNode2.Name)
					{
					case "trainType":
						viewPoint.mTrainType = GetTrainType(childNode2.InnerText);
						break;
					case "itemType":
						viewPoint.mItemType = int.Parse(childNode2.InnerText);
						break;
					case "camera":
						viewPoint.cameraInfoList.Add(ReadCameraInfo(childNode2));
						break;
					default:
						Debug.LogError(childNode2.Name + "  节点名称错误");
						break;
					}
				}
				mViewPointList.Add(viewPoint);
			}
		}

		private CameraTransformInfo ReadCameraInfo(XmlElement cameraInfo)
		{
			CameraTransformInfo cameraTransformInfo = new CameraTransformInfo();
			foreach (XmlElement childNode in cameraInfo.ChildNodes)
			{
				switch (childNode.Name)
				{
				case "position":
				{
					string[] array2 = childNode.InnerText.Split(';');
					cameraTransformInfo.mPositon = new Vector3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]));
					break;
				}
				case "eulerAngle":
				{
					string[] array = childNode.InnerText.Split(';');
					cameraTransformInfo.eulerAngle = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
					break;
				}
				case "fieldOfView":
					cameraTransformInfo.fieldOfView = float.Parse(childNode.InnerText);
					break;
				default:
					Debug.LogError(childNode.Name + "  节点名称错误");
					break;
				}
			}
			return cameraTransformInfo;
		}

		private TrainType GetTrainType(string name)
		{
			TrainType result = TrainType.None;
			if (!(name == "NightLight"))
			{
				if (name == "Normal")
				{
					result = TrainType.None;
				}
				else
				{
					Debug.LogError(name + "  节点名称错误");
				}
			}
			else
			{
				result = TrainType.NightLight;
			}
			return result;
		}

		private void SetCameraPosition(Camera mCamera, int index)
		{
			bool flag = false;
			foreach (ViewPoint mViewPoint in mViewPointList)
			{
				if (mViewPoint.mTrainType == ViewJumpCtl.Instance.trainType && mViewPoint.mItemType == ViewJumpCtl.Instance.itemType)
				{
					curViewPoint = mViewPoint;
					mCamera.fieldOfView = mViewPoint.cameraInfoList[index].fieldOfView;
					mCamera.transform.position = CarState.Instance.CurCar.transform.position + curViewPoint.cameraInfoList[index].mPositon;
					mCamera.transform.localEulerAngles = CarState.Instance.CurCar.transform.localEulerAngles + curViewPoint.cameraInfoList[index].eulerAngle;
					flag = true;
					break;
				}
			}
			if (flag)
			{
				return;
			}
			foreach (ViewPoint mViewPoint2 in mViewPointList)
			{
				if (mViewPoint2.mTrainType == TrainType.None)
				{
					Camera.main.fieldOfView = mViewPoint2.cameraInfoList[index].fieldOfView;
					Camera.main.transform.position = CarState.Instance.CurCar.transform.position + curViewPoint.cameraInfoList[index].mPositon;
					Camera.main.transform.localEulerAngles = CarState.Instance.CurCar.transform.localEulerAngles + curViewPoint.cameraInfoList[index].eulerAngle;
					curViewPoint = mViewPoint2;
					break;
				}
			}
		}
	}
}
