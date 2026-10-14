using System.Collections.Generic;
using System.Xml;

internal class XmlItemInfoRead
{
	public List<ItemInfo> GetItemInfo(string text)
	{
		List<ItemInfo> list = new List<ItemInfo>();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(text);
		foreach (XmlElement childNode in xmlDocument.SelectSingleNode("ItemInfo").ChildNodes)
		{
			ItemInfo itemInfo = new ItemInfo();
			itemInfo.itemId = int.Parse(childNode.GetAttribute("id"));
			foreach (XmlElement childNode2 in childNode.ChildNodes)
			{
				switch (childNode2.Name)
				{
				case "itemFlag":
					itemInfo.itemFlag = childNode2.InnerText;
					break;
				case "isPass":
					itemInfo.isPass = bool.Parse(childNode2.InnerText);
					break;
				case "isUnable":
					itemInfo.isUnable = bool.Parse(childNode2.InnerText);
					break;
				case "score":
					itemInfo.score = float.Parse(childNode2.InnerText);
					break;
				case "otherFlagInfo":
					itemInfo.otherFlagInfo = childNode2.InnerText;
					break;
				case "error":
				{
					string[] collection = childNode2.InnerText.Split(';');
					itemInfo.errorInfoList.AddRange(collection);
					break;
				}
				}
			}
			list.Add(itemInfo);
		}
		return list;
	}
}
