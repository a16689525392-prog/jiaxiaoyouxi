using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Global;
using Scene;
using UnityEngine;

namespace SoundAndTextShow
{
	public class SoundPlayCtl : MonoBehaviour
	{
		public class MyPlaySound
		{
			public int id;

			public string text;

			public int soundType;

			public float score;

			public MyPlaySound(int id, string text)
			{
				this.id = id;
				this.text = text;
			}

			public MyPlaySound()
			{
			}
		}

		private bool isPause;

		public SpeechSynthesis mSpeechSynthesis;

		private AssetBundle soundAssetBundle;

		private bool isNetWork;

		private List<MyPlaySound> playSoundList = new List<MyPlaySound>();

		private bool isFindSound;

		private string scoreText = "";

		public bool isSoundLoad;

		private bool isScoreSoundPlay = true;

		private float tempScore;

		private List<string> loadStringList = new List<string>();

		private bool isFirst;

		public List<MyPlaySound> soundList = new List<MyPlaySound>();

		public static SoundPlayCtl Instance { get; private set; }

		public string soundPath { get; private set; }

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			soundPath = Application.streamingAssetsPath + "/AssetBundles/sound";
			soundAssetBundle = AssetBundle.LoadFromFile(soundPath);
			ReadSoundMapping("mapping.xml");
		}

		private void Update()
		{
			if (AudioSourceCtl.Instance != null && AudioSourceCtl.Instance.soundPlayAudioSource != null && !AudioSourceCtl.Instance.soundPlayAudioSource.isPlaying && !isSoundLoad)
			{
				if (!isScoreSoundPlay)
				{
					scoreText = "扣" + tempScore + "分";
					PlaySound(scoreText, 0f, 2, 0f, isFirstPlay: false);
				}
				else if (playSoundList.Count > 0)
				{
					PlaySound(playSoundList[0].text, playSoundList[0].score, playSoundList[0].soundType, 0f, isFirstPlay: false);
					playSoundList.RemoveAt(0);
				}
			}
		}

		public void PlaySound(string soundText, float score = 0f, int soundType = 0, float time = 0f, bool isFirstPlay = true)
		{
			isPause = false;
			isFindSound = false;
			if (soundType == 0 && isFirstPlay)
			{
				if (score > 0f)
				{
					scoreText = "扣" + score + "分";
					SceneTextShow.Instance.ShowNormalText(soundText + ", " + scoreText);
				}
				else
				{
					SceneTextShow.Instance.ShowNormalText(soundText);
				}
			}
			if (AudioSourceCtl.Instance.soundPlayAudioSource.isPlaying || isSoundLoad)
			{
				MyPlaySound myPlaySound = new MyPlaySound();
				myPlaySound.text = soundText;
				myPlaySound.score = score;
				myPlaySound.soundType = soundType;
				playSoundList.Add(myPlaySound);
			}
			else
			{
				foreach (MyPlaySound sound in soundList)
				{
					if (!(sound.text == soundText))
					{
						continue;
					}
					isFindSound = true;
					switch (soundType)
					{
					case 0:
						LoadSound(sound.id);
						if (score > 0f)
						{
							isScoreSoundPlay = false;
							tempScore = score;
							DataCenter.Instance.score -= score;
							NetworkManagement.Instance.errorid = SoundToErrorID(soundText);
							NetworkManagement.Instance.IsSendError = true;
						}
						break;
					case 2:
						LoadSound(sound.id);
						isScoreSoundPlay = true;
						break;
					case 1:
						LoadSound(sound.id);
						SceneTextShow.Instance.ShowTitleText(soundText);
						break;
					case 3:
					{
						int actionId = CarActionJudge.Instance.GetActionId(soundText);
						if (!CarActionJudge.Instance.JudgeActionDone(actionId))
						{
							LoadSound(sound.id);
						}
						break;
					}
					case 4:
						LoadSound(sound.id);
						break;
					}
					break;
				}
				_ = isFindSound;
			}
			if (score > 0f)
			{
				SavetoDataFile(soundText, GameGlobalCtl.Instance.SignalQueueDataPool);
			}
		}

		private string SoundToErrorID(string Sound)
		{
			string result = "0";
			switch (Sound)
			{
			case "撞护栏":
				result = "0001";
				break;
			case "点火时未将离合器踩到底":
				result = "0002";
				break;
			case "点火时未将脚刹踩到底":
				result = "0003";
				break;
			case "点火时挡位未挂在空挡":
				result = "0004";
				break;
			case "起步前未系上安全带":
				result = "0005";
				break;
			case "起步时猛踩油门":
				result = "0006";
				break;
			case "换挡时未将离合器踩到底":
				result = "0007";
				break;
			case "操作不当造成发动机熄火":
				result = "0008";
				break;
			case "车身出线":
				result = "0009";
				break;
			case "倒库不入":
				result = "0010";
				break;
			case "未将车辆两前轮均驶过控制线":
				result = "0011";
				break;
			case "未按规定进行倒车入库操作":
				result = "0012";
				break;
			case "倒车时，未正确开启右转向灯":
				result = "0013";
				break;
			case "倒车时，未开启右转向灯":
				result = "0014";
				break;
			case "出库时，未正确开启左转向灯":
				result = "0015";
				break;
			case "出库时，未开启左转向灯":
				result = "0016";
				break;
			case "未按规定进行侧方停车操作":
				result = "0017";
				break;
			case "未及时关闭转向灯":
				result = "0018";
				break;
			case "未正确开启转向灯":
				result = "0019";
				break;
			case "未开启左转向灯":
				result = "0020";
				break;
			case "转向结束后未及时关闭转向灯":
				result = "0021";
				break;
			case "上坡时，车辆后溜距离大于30厘米":
				result = "0022";
				break;
			case "上坡时，车辆后溜距离大于50厘米":
				result = "0023";
				break;
			case "停车后，汽车前保险杠距离桩杆线超过30厘米":
				result = "0024";
				break;
			case "停车后，汽车前保险杠距离桩杆线超过50厘米":
				result = "0025";
				break;
			case "车辆停止后，车身距离路边线超过30厘米":
				result = "0026";
				break;
			case "车辆停止后，车身距离路边线超过50厘米":
				result = "0027";
				break;
			case "停车后，未将档位挂为空挡":
				result = "0028";
				break;
			case "停车后，未拉紧手刹":
				result = "0029";
				break;
			case "起步时，未正确开启转向灯":
				result = "0030";
				break;
			case "起步时，未正确开启左转向灯":
				result = "0031";
				break;
			case "未按规定进行坡道定点停车操作":
				result = "0032";
				break;
			case "灯光考试开始前未关闭所有灯光":
				result = "0033";
				break;
			case "未播报完指令即进行操作":
				result = "0034";
				break;
			case "进行了正确灯光操作外的其他操作":
				result = "0035";
				break;
			case "只进行了一次远近灯交替":
				result = "0036";
				break;
			case "未开启近光灯或远光灯":
				result = "0037";
				break;
			case "未开启近光灯":
				result = "0038";
				break;
			case "未开启远光灯":
				result = "0039";
				break;
			case "未开启示宽灯":
				result = "0040";
				break;
			case "未远近光灯交替一次":
				result = "0041";
				break;
			case "未将档位加至三挡":
				result = "0042";
				break;
			case "未按规定进行超车操作":
				result = "0043";
				break;
			case "超车后，未变更回原车道":
				result = "0044";
				break;
			case "会车时，未按规定进行减速操作":
				result = "0045";
				break;
			case "会车时，未将车速降低至30km/h以下":
				result = "0046";
				break;
			case "未按规定进行减速操作":
				result = "0047";
				break;
			case "未将车速降低至30km/h以下":
				result = "0048";
				break;
			case "未按规定进行变更车道操作":
				result = "0049";
				break;
			case "转向前，未将车速降低至30km/h以下":
				result = "0050";
				break;
			case "掉头前，未将车速降低至30km/h以下":
				result = "0051";
				break;
			case "未进行减档操作":
				result = "0052";
				break;
			case "未按规定进行加减档操作":
				result = "0053";
				break;
			case "方向控制不稳，不能保持车辆直线行驶":
				result = "0054";
				break;
			case "直线行驶时，车速未达到30km/h以上":
				result = "0055";
				break;
			case "靠边停车时，未正确开启右转向灯":
				result = "0056";
				break;
			case "靠边停车时，未开启右转向灯":
				result = "0057";
				break;
			case "停车后，车辆距离道路边缘线超过规定距离":
				result = "0058";
				break;
			case "停车后，未及时关闭转向灯":
				result = "0059";
				break;
			case "停车后，未及时拉紧手刹":
				result = "0060";
				break;
			case "停车后，未及时熄火":
				result = "0061";
				break;
			case "未在规定距离内完成靠边停车操作":
				result = "0062";
				break;
			case "夜间会车未切换成近光灯":
				result = "0063";
				break;
			case "夜间同方向近距离跟车行驶，未使用近光灯":
				result = "0064";
				break;
			case "夜间在窄路与非机动车会车，未使用近光灯":
				result = "0065";
				break;
			case "逆向行驶":
				result = "0066";
				break;
			case "行驶中未松油门减速避让非机动车":
				result = "0067";
				break;
			case "行驶中未踩制动减速避让非机动车":
				result = "0068";
				break;
			case "行驶中未鸣喇叭示意非机动车":
				result = "0069";
				break;
			case "行驶中未打转向盘避让非机动车":
				result = "0070";
				break;
			case "行驶中未松油门减速避让行人":
				result = "0071";
				break;
			case "行驶中未踩制动减速避让行人":
				result = "0072";
				break;
			case "行驶中未鸣喇叭示意行人":
				result = "0073";
				break;
			case "行驶中未打转向盘避让行人":
				result = "0074";
				break;
			case "行驶中未松油门减速避让车辆":
				result = "0075";
				break;
			case "行驶中未踩制动减速避让车辆":
				result = "0076";
				break;
			case "行驶中未打转向盘避让车辆":
				result = "0077";
				break;
			case "夜间在没有路灯，照明不良条件下行驶，未开启远光灯":
				result = "0078";
				break;
			case "未交替使用远近光灯":
				result = "0079";
				break;
			case "道路上发生故障妨碍交通难移动时,不开启示宽灯和危险报警灯":
				result = "0080";
				break;
			case "雨天未开启雨刮器":
				result = "0081";
				break;
			case "恶劣天气驾驶机动车不按规定使用灯光":
				result = "0082";
				break;
			case "恶劣天气未减速慢行":
				result = "0083";
				break;
			case "会车时,未与其他车辆保持横向安全距离":
				result = "0084";
				break;
			case "恶劣天气未与前车保持安全距离":
				result = "0085";
				break;
			case "冰雪道路超车或变更车道":
				result = "0086";
				break;
			case "未与前车保持安全车距":
				result = "0087";
				break;
			case "未保证安全距离情况下抢道":
				result = "0088";
				break;
			case "弯道内超车":
				result = "0089";
				break;
			case "进入弯道前,未鸣喇叭":
				result = "0090";
				break;
			case "进入弯道前,未减速至通过弯道所需的速度":
				result = "0091";
				break;
			case "弯道内占用对方车道":
				result = "0092";
				break;
			case "驾驶机动车在高速公路上倒车":
				result = "0093";
				break;
			case "高速公路低于最低时速行驶":
				result = "0094";
				break;
			case "高速公路超速行驶":
				result = "0095";
				break;
			case "行驶中,占用应急车道":
				result = "0096";
				break;
			case "停车后,未开危险警示灯提醒其他车辆":
				result = "0097";
				break;
			case "停车后,未拉起手刹":
				result = "0098";
				break;
			case "未将机动车停靠在安全停车区":
				result = "0099";
				break;
			case "未按规定路线行驶":
				result = "0100";
				break;
			case "高速公路车辆发生故障,未及时平稳靠边停车":
				result = "0101";
				break;
			case "驶抵隧道入口时,未鸣喇叭":
				result = "0102";
				break;
			case "驶抵隧道时,未减速":
				result = "0103";
				break;
			case "隧道内驾驶时,车速过快":
				result = "0104";
				break;
			case "驶抵隧道出口时,未鸣喇叭":
				result = "0105";
				break;
			case "不按规定车道行驶":
				result = "0106";
				break;
			case "变更车道":
				result = "0107";
				break;
			case "驶抵隧道时,未开启前照灯":
				result = "0108";
				break;
			case "驶出隧道后,未关闭前照灯":
				result = "0109";
				break;
			case "行驶中猛转方向盘":
				result = "0110";
				break;
			case "撞行人":
				result = "0111";
				break;
			case "撞车":
				result = "0112";
				break;
			case "行驶中熄火":
				result = "0113";
				break;
			case "触压道路边缘线":
				result = "0114";
				break;
			}
			return result;
		}

		public void SavetoDataFile(string error, Queue<string> queue)
		{
			if (!SettingInfoCtl.Instance.isSaveData)
			{
				return;
			}
			string[] array = new string[60];
			for (int i = 0; i < 60; i++)
			{
				if (queue.Count > 0)
				{
					array[i] = queue.Dequeue();
				}
			}
			if (!File.Exists(Application.streamingAssetsPath + "/ErrorLog/"))
			{
				Directory.CreateDirectory(Application.streamingAssetsPath + "/ErrorLog/");
			}
			File.WriteAllLines(Application.streamingAssetsPath + "/ErrorLog/" + DateTime.Now.ToString("yyyyMMddHHmmss") + error + ".txt", array);
		}

		public void RemoveSound(string soundText)
		{
			foreach (MyPlaySound playSound in playSoundList)
			{
				if (playSound.text == soundText && playSound.soundType == 3)
				{
					playSoundList.Remove(playSound);
					break;
				}
			}
		}

		public void SoundListReset()
		{
			playSoundList.Clear();
			SoundPause();
			isScoreSoundPlay = true;
			SceneTextShow.Instance.ResetNormalText();
		}

		public void SoundPause()
		{
			AudioSourceCtl.Instance.soundPlayAudioSource.Pause();
			isPause = true;
		}

		public void SoundPlay()
		{
			if (!AudioSourceCtl.Instance.soundPlayAudioSource.isPlaying)
			{
				AudioSourceCtl.Instance.soundPlayAudioSource.Play();
				isPause = false;
			}
		}

		public bool IsPlaying()
		{
			return AudioSourceCtl.Instance.soundPlayAudioSource.isPlaying;
		}

		public bool IsPause()
		{
			return isPause;
		}

		public void ClearClip()
		{
			if (AudioSourceCtl.Instance != null)
			{
				AudioSourceCtl.Instance.soundPlayAudioSource.clip = null;
			}
		}

		private void LoadSound(int id)
		{
			if (AudioSourceCtl.Instance != null)
			{
				AudioSourceCtl.Instance.soundPlayAudioSource.clip = soundAssetBundle.LoadAsset<AudioClip>(id + ".mp3");
				AudioSourceCtl.Instance.soundPlayAudioSource.Play();
			}
		}

		private void ReadSoundMapping(string path)
		{
			TextAsset textAsset = soundAssetBundle.LoadAsset<TextAsset>(path);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(textAsset.text);
			foreach (XmlElement childNode in xmlDocument.SelectSingleNode("datas").ChildNodes)
			{
				MyPlaySound myPlaySound = new MyPlaySound();
				foreach (XmlElement childNode2 in childNode.ChildNodes)
				{
					string text = childNode2.Name;
					if (!(text == "id"))
					{
						if (text == "text")
						{
							myPlaySound.text = childNode2.InnerText;
						}
					}
					else
					{
						myPlaySound.id = int.Parse(childNode2.InnerText);
					}
				}
				soundList.Add(myPlaySound);
			}
		}
	}
}
