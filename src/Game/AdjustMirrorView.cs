using System.IO;
using System.Xml;
using Car;
using Global;
using UnityEngine;
using View;

public class AdjustMirrorView : ViewBase
{
	public static AdjustMirrorView instance;

	public override void Awake()
	{
		instance = this;
		base.Awake();
	}

	public override void Start()
	{
		base.Start();
		base.transform.SetAsLastSibling();
	}

	private void Update()
	{
	}

	public override void BtnClick(GameObject go)
	{
		base.BtnClick(go);
		if (go == GetBtn(0))
		{
			ChangeMirrorX(CarCameraCtl.Instance.leftReviewCamera, -0.5f);
		}
		else if (go == GetBtn(1))
		{
			ChangeMirrorX(CarCameraCtl.Instance.leftReviewCamera, 0.5f);
		}
		else if (go == GetBtn(2))
		{
			ChangeMirrorY(CarCameraCtl.Instance.leftReviewCamera, 0.5f);
		}
		else if (go == GetBtn(3))
		{
			ChangeMirrorY(CarCameraCtl.Instance.leftReviewCamera, -0.5f);
		}
		else if (go == GetBtn(4))
		{
			ChangeMirrorX(CarCameraCtl.Instance.rightReviewCamera, -0.5f);
		}
		else if (go == GetBtn(5))
		{
			ChangeMirrorX(CarCameraCtl.Instance.rightReviewCamera, 0.5f);
		}
		else if (go == GetBtn(6))
		{
			ChangeMirrorY(CarCameraCtl.Instance.rightReviewCamera, 0.5f);
		}
		else if (go == GetBtn(7))
		{
			ChangeMirrorY(CarCameraCtl.Instance.rightReviewCamera, -0.5f);
		}
		else if (go == GetBtn(8))
		{
			SetMirrorDataToXml();
			ViewJumpCtl.Instance.ReturnPreView();
		}
	}

	private void ChangeMirrorX(Transform T, float Value)
	{
		float num = T.localEulerAngles.x + Value;
		if (T == CarCameraCtl.Instance.leftReviewCamera)
		{
			if (num > 350f || num < 35f)
			{
				T.localEulerAngles += new Vector3(Value, 0f, 0f);
			}
		}
		else if (T == CarCameraCtl.Instance.rightReviewCamera && (num > 350f || num < 35f))
		{
			T.localEulerAngles += new Vector3(Value, 0f, 0f);
		}
	}

	private void ChangeMirrorY(Transform T, float Value)
	{
		float num = T.localEulerAngles.y + Value;
		if (T == CarCameraCtl.Instance.leftReviewCamera)
		{
			if (num > 165f && num < 195f)
			{
				T.localEulerAngles += new Vector3(0f, Value, 0f);
			}
		}
		else if (T == CarCameraCtl.Instance.rightReviewCamera && num > 165f && num < 185f)
		{
			T.localEulerAngles += new Vector3(0f, Value, 0f);
		}
	}

	public void SetMirrorDataToXml()
	{
		string text = "";
		text = ((Application.platform != RuntimePlatform.Android) ? Path.Combine(Application.streamingAssetsPath, "SettingFile/MirrorData.xml") : Path.Combine(Application.persistentDataPath, "SettingFile/MirrorData.xml"));
		if (File.Exists(text))
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(text);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("MirrorData");
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("MirrorData/CarType");
			if (xmlNodeList.Count > 0)
			{
				bool flag = false;
				foreach (XmlNode item in xmlNodeList)
				{
					if (!(item.Attributes["name"].Value == SettingInfoCtl.Instance.carType.ToString()))
					{
						continue;
					}
					flag = true;
					if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
					{
						XmlNode xmlNode3 = item.SelectSingleNode("CourseTwo");
						if (xmlNode3 != null)
						{
							item.RemoveChild(xmlNode3);
						}
						WriteMirrorData(xmlDocument, "CourseTwo", item);
					}
					else
					{
						XmlNode xmlNode4 = item.SelectSingleNode("Other");
						if (xmlNode4 != null)
						{
							item.RemoveChild(xmlNode4);
						}
						WriteMirrorData(xmlDocument, "Other", item);
					}
					break;
				}
				if (!flag)
				{
					CreateCarTypeElement(xmlDocument, xmlNode);
				}
			}
			else if (xmlNode == null)
			{
				XmlNode xmlNode5 = xmlDocument.CreateElement("MirrorData");
				xmlDocument.AppendChild(xmlNode5);
				CreateCarTypeElement(xmlDocument, xmlNode5);
			}
			else
			{
				CreateCarTypeElement(xmlDocument, xmlNode);
			}
			xmlDocument.Save(text);
		}
		else
		{
			XmlDocument xmlDocument2 = new XmlDocument();
			XmlDeclaration newChild = xmlDocument2.CreateXmlDeclaration("1.0", "UTF-8", null);
			xmlDocument2.AppendChild(newChild);
			XmlNode xmlNode6 = xmlDocument2.CreateElement("MirrorData");
			xmlDocument2.AppendChild(xmlNode6);
			CreateCarTypeElement(xmlDocument2, xmlNode6);
			xmlDocument2.Save(text);
		}
	}

	private void CreateCarTypeElement(XmlDocument xmlDoc, XmlNode MirrorDataNode)
	{
		XmlNode xmlNode = xmlDoc.CreateElement("CarType");
		XmlAttribute xmlAttribute = xmlDoc.CreateAttribute("name");
		xmlAttribute.Value = SettingInfoCtl.Instance.carType.ToString();
		xmlNode.Attributes.Append(xmlAttribute);
		MirrorDataNode.AppendChild(xmlNode);
		if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
		{
			WriteMirrorData(xmlDoc, "CourseTwo", xmlNode);
		}
		else
		{
			WriteMirrorData(xmlDoc, "Other", xmlNode);
		}
	}

	private void WriteMirrorData(XmlDocument xmlDoc, string trainType, XmlNode newCarTypeNode)
	{
		XmlNode xmlNode = xmlDoc.CreateElement(trainType);
		newCarTypeNode.AppendChild(xmlNode);
		XmlNode xmlNode2 = xmlDoc.CreateElement("leftReviewCameraX");
		xmlNode2.InnerText = CarCameraCtl.Instance.leftReviewCamera.localEulerAngles.x.ToString();
		xmlNode.AppendChild(xmlNode2);
		XmlNode xmlNode3 = xmlDoc.CreateElement("leftReviewCameraY");
		xmlNode3.InnerText = CarCameraCtl.Instance.leftReviewCamera.localEulerAngles.y.ToString();
		xmlNode.AppendChild(xmlNode3);
		XmlNode xmlNode4 = xmlDoc.CreateElement("rightReviewCameraX");
		xmlNode4.InnerText = CarCameraCtl.Instance.rightReviewCamera.localEulerAngles.x.ToString();
		xmlNode.AppendChild(xmlNode4);
		XmlNode xmlNode5 = xmlDoc.CreateElement("rightReviewCameraY");
		xmlNode5.InnerText = CarCameraCtl.Instance.rightReviewCamera.localEulerAngles.y.ToString();
		xmlNode.AppendChild(xmlNode5);
	}

	private void ReadMirrorDataXml()
	{
		string text = Path.Combine(Application.streamingAssetsPath, "SettingFile/MirrorData.xml");
		if (!File.Exists(text))
		{
			return;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(text);
		foreach (XmlNode item in xmlDocument.SelectNodes("MirrorData/CarType"))
		{
			if (!(item.Attributes["name"].Value == SettingInfoCtl.Instance.carType.ToString()))
			{
				continue;
			}
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
			{
				XmlNode xmlNode2 = item.SelectSingleNode("CourseTwo/leftReviewCameraX");
				XmlNode xmlNode3 = item.SelectSingleNode("CourseTwo/leftReviewCameraY");
				XmlNode xmlNode4 = item.SelectSingleNode("CourseTwo/rightReviewCameraX");
				XmlNode xmlNode5 = item.SelectSingleNode("CourseTwo/rightReviewCameraY");
				if (xmlNode2 != null && xmlNode2 != null && xmlNode2 != null && xmlNode2 != null)
				{
					CarCameraCtl.Instance.leftReviewCamera.localEulerAngles = new Vector3(float.Parse(xmlNode2.InnerText), float.Parse(xmlNode3.InnerText));
					CarCameraCtl.Instance.rightReviewCamera.localEulerAngles = new Vector3(float.Parse(xmlNode4.InnerText), float.Parse(xmlNode5.InnerText));
				}
			}
			else
			{
				XmlNode xmlNode6 = item.SelectSingleNode("Other/leftReviewCameraX");
				XmlNode xmlNode7 = item.SelectSingleNode("Other/leftReviewCameraY");
				XmlNode xmlNode8 = item.SelectSingleNode("Other/rightReviewCameraX");
				XmlNode xmlNode9 = item.SelectSingleNode("Other/rightReviewCameraY");
				if (xmlNode6 != null && xmlNode6 != null && xmlNode6 != null && xmlNode6 != null)
				{
					CarCameraCtl.Instance.leftReviewCamera.localEulerAngles = new Vector3(float.Parse(xmlNode6.InnerText), float.Parse(xmlNode7.InnerText));
					CarCameraCtl.Instance.rightReviewCamera.localEulerAngles = new Vector3(float.Parse(xmlNode8.InnerText), float.Parse(xmlNode9.InnerText));
				}
			}
			break;
		}
	}
}
