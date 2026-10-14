using System;
using System.Collections;
using System.IO;
using System.Xml;
using BestHttpLitJson;
using UnityEngine;
using UnityEngine.Networking;

namespace SoundAndTextShow
{
	public class SpeechSynthesis
	{
		private string grant_Type = "client_credentials";

		private string client_ID = Environment.GetEnvironmentVariable("GAME_TTS_CLIENT_ID") ?? "";

		private string client_Secret = Environment.GetEnvironmentVariable("GAME_TTS_CLIENT_SECRET") ?? "";

		private string baiduAPI = "http://tsn.baidu.com/text2audio";

		private string getTokenAPIPath = "https://openapi.baidu.com/oauth/2.0/token";

		public string token = "";

		private string cuid = "default";

		private int ctp = 1;

		private string lan = "zh";

		private int spd = 5;

		private int pit = 5;

		private int vol = 5;

		private int per;

		private int aue = 6;

		public IEnumerator GetToken()
		{
			if (string.IsNullOrEmpty(client_ID) || string.IsNullOrEmpty(client_Secret))
			{
				yield break;
			}
			WWWForm wWWForm = new WWWForm();
			wWWForm.AddField("grant_type", grant_Type);
			wWWForm.AddField("client_id", client_ID);
			wWWForm.AddField("client_secret", client_Secret);
			UnityWebRequest webRequest = UnityWebRequest.Post(getTokenAPIPath, wWWForm);
			yield return webRequest.SendWebRequest();
			if (webRequest.error != null)
			{
				Debug.LogError(webRequest.error);
			}
			else
			{
				token = JsonMapper.ToObject(webRequest.downloadHandler.text)["access_token"].ToString();
			}
		}

		public IEnumerator LoadSpeech(string soundText, bool isPlay = false, float score = 0f)
		{
			if (string.IsNullOrEmpty(token))
			{
				yield break;
			}
			Debug.Log(soundText);
			SoundPlayCtl.Instance.isSoundLoad = true;
			WWWForm wWWForm = new WWWForm();
			wWWForm.AddField("lan", lan);
			wWWForm.AddField("ctp", ctp);
			wWWForm.AddField("cuid", cuid);
			wWWForm.AddField("tok", token);
			wWWForm.AddField("tex", soundText);
			wWWForm.AddField("vol", vol);
			wWWForm.AddField("per", per);
			wWWForm.AddField("spd", spd);
			wWWForm.AddField("pit", pit);
			wWWForm.AddField("aue", aue);
			UnityWebRequest webRequest = UnityWebRequest.Post(baiduAPI, wWWForm);
			yield return webRequest.SendWebRequest();
			if (webRequest.error != null)
			{
				Debug.LogError(webRequest.error);
			}
			else
			{
				int newSoundId = GetNewSoundId();
				FileStream fileStream = new FileInfo(SoundPlayCtl.Instance.soundPath + newSoundId + ".wav").Create();
				fileStream.Write(webRequest.downloadHandler.data, 0, webRequest.downloadHandler.data.Length);
				fileStream.Close();
				fileStream.Dispose();
				SoundPlayCtl.MyPlaySound myPlaySound = new SoundPlayCtl.MyPlaySound(newSoundId, soundText);
				AddSoundNode(myPlaySound);
				SoundPlayCtl.Instance.soundList.Add(myPlaySound);
				if (isPlay)
				{
					SoundPlayCtl.Instance.PlaySound(soundText, score);
				}
			}
			SoundPlayCtl.Instance.isSoundLoad = false;
		}

		private int GetNewSoundId()
		{
			int i = 0;
			bool flag = true;
			for (; i < 10000; i++)
			{
				flag = true;
				foreach (SoundPlayCtl.MyPlaySound sound in SoundPlayCtl.Instance.soundList)
				{
					if (sound.id == i)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return i;
				}
			}
			return 0;
		}

		private void AddSoundNode(SoundPlayCtl.MyPlaySound sound)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(SoundPlayCtl.Instance.soundPath + "mapping.xml");
			XmlNode xmlNode = xmlDocument.SelectSingleNode("datas");
			XmlElement xmlElement = xmlDocument.CreateElement("sound");
			XmlElement xmlElement2 = xmlDocument.CreateElement("id");
			xmlElement2.InnerText = sound.id.ToString();
			XmlElement xmlElement3 = xmlDocument.CreateElement("text");
			xmlElement3.InnerText = sound.text;
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			xmlNode.AppendChild(xmlElement);
			xmlDocument.Save(SoundPlayCtl.Instance.soundPath + "mapping.xml");
		}
	}
}
