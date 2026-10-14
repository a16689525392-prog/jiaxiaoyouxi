using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Scene;
using UnityEngine;
using UnityEngine.Networking;

namespace Global
{
	public class CourseThreeItemPositionCtl
	{
		private List<ItemPosition> itemPositionList = new List<ItemPosition>();

		public IEnumerator ReadCourseThreeItemPostion(List<ItemPosition> itemPositionList)
		{
			this.itemPositionList = itemPositionList;
			string text = Application.persistentDataPath + "/UserCourseThreeItemPosition.xml";
			if (new FileInfo(text).Exists)
			{
				UnityWebRequest www = UnityWebRequest.Get("File:///" + text);
				yield return www.SendWebRequest();
				ReadItemPosition(www.downloadHandler.text);
			}
			else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
			{
				TextAsset textAsset = Resources.Load<TextAsset>(DataCenter.Instance.GetFilePath(3) + "CourseThreeItemPositionSetting");
				ReadItemPosition(textAsset.text);
			}
			else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.HuNanYZ)
			{
				TextAsset textAsset2 = Resources.Load<TextAsset>(DataCenter.Instance.GetFilePath(3) + "HuNanYZCourseThreeItemPositionSetting");
				ReadItemPosition(textAsset2.text);
			}
			else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai)
			{
				TextAsset textAsset3 = Resources.Load<TextAsset>(DataCenter.Instance.GetFilePath(3) + "RenCaiCourseThreeItemPositionSetting");
				ReadItemPosition(textAsset3.text);
			}
			else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong)
			{
				TextAsset textAsset4 = Resources.Load<TextAsset>(DataCenter.Instance.GetFilePath(3) + "ChengDongCourseThreeItemPositionSetting");
				ReadItemPosition(textAsset4.text);
			}
			else if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
			{
				TextAsset textAsset5 = Resources.Load<TextAsset>(DataCenter.Instance.GetFilePath(3) + "BaiYunCourseThreeItemPositionSetting");
				ReadItemPosition(textAsset5.text);
			}
		}

		private void ReadItemPosition(string text)
		{
			itemPositionList.Clear();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(text);
			foreach (XmlElement childNode in xmlDocument.SelectSingleNode("data").ChildNodes)
			{
				ItemPosition itemPosition = new ItemPosition();
				foreach (XmlElement childNode2 in childNode.ChildNodes)
				{
					string name = childNode2.Name;
					if (!(name == "itemIndex"))
					{
						if (name == "itemName")
						{
							itemPosition.itemName = childNode2.InnerText;
						}
					}
					else
					{
						itemPosition.itemIndex = int.Parse(childNode2.InnerText);
					}
				}
				itemPosition.initPositionName = "InitPosition" + itemPosition.itemIndex;
				itemPosition.itemStartName = "ItemStart" + itemPosition.itemIndex;
				itemPosition.itemType = GetItemType(itemPosition.itemName);
				itemPositionList.Add(itemPosition);
			}
		}

		public static int GetItemType(string itemName)
		{
			int num = -1;
			switch (itemName)
			{
			case "Starting":
				num = 200;
				break;
			case "Overtake":
				num = 201;
				break;
			case "ThroughSchool":
				num = 202;
				break;
			case "ChangeLane":
				num = 203;
				break;
			case "ThroughManLane":
				num = 204;
				break;
			case "TurnRight":
				num = 205;
				break;
			case "TurnRound":
				num = 206;
				break;
			case "ThroughBusStation":
				num = 207;
				break;
			case "TurnLeft":
				num = 208;
				break;
			case "AddAndSubGear":
				num = 210;
				break;
			case "StraightDrive":
				num = 211;
				break;
			case "Stopping":
				num = 212;
				break;
			case "Meeting":
				num = 209;
				break;
			case "DaoCar":
				num = 213;
				break;
			case "StraightCrossroads":
				num = 214;
				break;
			case "CourseThreeKaoShi":
				num = 1300;
				break;
			case "None":
				num = -1;
				break;
			default:
				num = 200;
				Debug.Log(itemName + " 未找到");
				break;
			}
			return num;
		}
	}
}
