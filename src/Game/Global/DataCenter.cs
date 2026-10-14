using System.Collections.Generic;
using UnityEngine;
using View;

namespace Global
{
	public class DataCenter : MonoBehaviour
	{
		public byte UsbVersion = 240;

		public string SoftVersion = "V6.5.12";

		public float score = 100f;

		public bool isShowKeTengBiaoDingView;

		public int coreNumber;

		public bool isNetworking;

		public float mAdhere = 0.7f;

		public int tripleState = 3;

		public int SoftErrorCode;

		public string SerialNumber = "";

		public byte[] rsaKey = new byte[16];

		public byte[] TtqDesData = new byte[800];

		public byte[] TtqDesKey = new byte[128];

		public float[] TtqData = new float[200];

		public static ViewJumpCtl selectstate;

		public List<ItemPosition> itemPositionList = new List<ItemPosition>();

		public static DataCenter Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
			InitGameData();
			Invoke("JudgeMatch", 2f);
		}

		private void Update()
		{
		}

		private void JudgeResolutionMatch()
		{
			if ((float)(Screen.width / Screen.height) > 2.4f && UsbVersion != 240)
			{
				if (UsbVersion == 64 || UsbVersion == 96 || UsbVersion == 128)
				{
					tripleState = 0;
					return;
				}
				tripleState = 2;
				Camera[] array = Object.FindObjectsOfType<Camera>();
				for (int i = 0; i < array.Length; i++)
				{
					_ = array[i];
				}
			}
			else if ((float)(Screen.width / Screen.height) <= 2.8f && UsbVersion != 240)
			{
				tripleState = 1;
			}
		}

		private void JudgeMatch()
		{
			if ((float)(Screen.width / Screen.height) > 2.8f && UsbVersion == 240)
			{
				Camera[] array = Object.FindObjectsOfType<Camera>();
				for (int i = 0; i < array.Length; i++)
				{
					_ = array[i];
				}
			}
		}

		public void InitGameData()
		{
			base.gameObject.AddComponent<SettingInfoCtl>();
		}

		public string GetUsbVersionName()
		{
			string text = "";
			switch (UsbVersion)
			{
			case 0:
				return "集成加密家庭版";
			case 16:
				return "专业单屏小板";
			case 32:
				return "其他";
			case 48:
				return "专业单屏";
			case 64:
				return "专业三屏";
			case 80:
				return "企业单屏";
			case 96:
				return "企业三屏";
			case 112:
				return "动感单屏";
			case 128:
				return "动感三屏";
			case 224:
				return "加密锁家庭版";
			case 240:
				return "未检测到电路板";
			default:
				return $"{UsbVersion:X4}";
			}
		}

		public string GetFilePath(int fileType)
		{
			string text = "";
			text = ((!SettingInfoCtl.Instance.isManualGear) ? "ConfigurationFiles/AutoGear/" : "ConfigurationFiles/ManualGear/");
			switch (fileType)
			{
			case 0:
				text += "GuideFile/";
				break;
			case 1:
				text += "HistoryFile/";
				break;
			case 2:
				text += "DriveHistory/";
				break;
			case 3:
				text += "Other/";
				break;
			}
			return text;
		}

		public string GetWWWPath(string path)
		{
			path = "File://" + path;
			return path;
		}

		public string GetPlatformPath()
		{
			string text = "";
			if (Application.platform == RuntimePlatform.Android)
			{
				return Application.persistentDataPath + "/";
			}
			return Application.streamingAssetsPath + "/";
		}
	}
}
