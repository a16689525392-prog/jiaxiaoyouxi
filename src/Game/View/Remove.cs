using System.Collections.Generic;
using System.IO;
using Scene;
using UnityEngine;

namespace View
{
	public class Remove : MonoBehaviour
	{
		private List<RecordItemData> recordItemDataList = new List<RecordItemData>();

		public static bool refresh;

		private void Start()
		{
			refresh = false;
		}

		private void Update()
		{
		}

		public string GetPath()
		{
			string text = "";
			text = ((Application.platform == RuntimePlatform.WindowsPlayer) ? (Application.streamingAssetsPath + "/RecordFiles/") : ((Application.platform != RuntimePlatform.Android) ? (Application.streamingAssetsPath + "/RecordFiles/") : (Application.persistentDataPath + "/RecordFiles/")));
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		public void RAll()
		{
			FileInfo fileInfo = new FileInfo(GetPath() + "historymap.map");
			recordItemDataList.Clear();
			FileStream fileStream = fileInfo.Create();
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(recordItemDataList.Count);
			binaryWriter.Flush();
			binaryWriter.Close();
			fileStream.Close();
			refresh = true;
			PlayerPrefs.SetInt("i", 0);
			if (Application.platform == RuntimePlatform.WindowsPlayer)
			{
				string fullpath = Application.streamingAssetsPath + "/RecordFiles/";
				DeleteAllFile(fullpath);
			}
			else if (Application.platform == RuntimePlatform.Android)
			{
				string fullpath2 = Application.persistentDataPath + "/RecordFiles/";
				DeleteAllFile(fullpath2);
			}
			else
			{
				string fullpath3 = Application.streamingAssetsPath + "/RecordFiles/";
				DeleteAllFile(fullpath3);
			}
		}

		public bool DeleteAllFile(string fullpath)
		{
			if (Directory.Exists(fullpath))
			{
				FileInfo[] files = new DirectoryInfo(fullpath).GetFiles("*", SearchOption.AllDirectories);
				Debug.Log(files.Length);
				for (int i = 0; i < files.Length; i++)
				{
					File.Delete(fullpath + "/" + files[i].Name);
				}
				return true;
			}
			return false;
		}
	}
}
