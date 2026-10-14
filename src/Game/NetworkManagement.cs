using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class NetworkManagement : MonoBehaviour
{
	public static NetworkManagement Instance;

	private string jsonstr = "";

	public bool IsYouKeModel;

	public string AESkey = Environment.GetEnvironmentVariable("GAME_SERVICE_AES_KEY") ?? "";

	public string AESiv = Environment.GetEnvironmentVariable("GAME_SERVICE_AES_IV") ?? "";

	private string urlGetQrCode = Environment.GetEnvironmentVariable("GAME_URL_QR_CODE") ?? "";

	private string urlGetToken = Environment.GetEnvironmentVariable("GAME_URL_TOKEN") ?? "";

	private string urlSendStart = Environment.GetEnvironmentVariable("GAME_URL_TRAINING_START") ?? "";

	private string urlSendError = Environment.GetEnvironmentVariable("GAME_URL_TRAINING_ERROR") ?? "";

	private string urlSendEnd = Environment.GetEnvironmentVariable("GAME_URL_TRAINING_END") ?? "";

	private string urlSendTiming = Environment.GetEnvironmentVariable("GAME_URL_TRAINING_TIMING") ?? "";

	private string urlGetRechargeCode = Environment.GetEnvironmentVariable("GAME_URL_RECHARGE") ?? "";

	private string urlGetBindingCode = Environment.GetEnvironmentVariable("GAME_URL_BINDING") ?? "";

	private string urlGetUserData = Environment.GetEnvironmentVariable("GAME_URL_USER_DATA") ?? "";

	public bool IsNetWork;

	public string StrGetQRCode;

	public string urlQRCode;

	public string sessionId;

	private string token;

	public string urlRechargeCode;

	public string urlBindingCode;

	public bool islogin;

	private string RecordId;

	private Thread SendErrorThread;

	public string errorid;

	public bool IsSendError;

	public bool IsStartTraining;

	public string BtnLevel;

	public bool TrainingIsPass;

	public bool KaoShiIsPass;

	private Thread SendTrainingHeartbeat;

	public bool IsHeartbeatRight;

	public string StrBinding;

	private void Awake()
	{
		AESkey = Environment.GetEnvironmentVariable("GAME_SERVICE_AES_KEY") ?? "";
		AESiv = Environment.GetEnvironmentVariable("GAME_SERVICE_AES_IV") ?? "";
		Instance = this;
		islogin = false;
		urlQRCode = "";
		sessionId = "";
		token = "";
		urlRechargeCode = "";
		urlBindingCode = "";
		IsYouKeModel = false;
		IsStartTraining = false;
		IsHeartbeatRight = true;
		StrBinding = "";
		StrGetQRCode = "";
	}

	private void Start()
	{
		SendErrorThread = new Thread(SendErrorId);
		SendErrorThread.Start();
		SendTrainingHeartbeat = new Thread(SendTimingHeartbeat);
		SendTrainingHeartbeat.Start();
	}

	private void Update()
	{
		IsConnectNetWork();
	}

	private void IsConnectNetWork()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable ||
			string.IsNullOrEmpty(urlGetQrCode) || string.IsNullOrEmpty(urlGetToken) ||
			Encoding.UTF8.GetByteCount(AESkey) != 32 || Encoding.UTF8.GetByteCount(AESiv) != 16)
		{
			IsNetWork = false;
			if (IsStartTraining)
			{
				IsHeartbeatRight = false;
			}
		}
		else
		{
			IsNetWork = true;
		}
	}

	public void GetQRCode(string softwareId, string numbering)
	{
		if (IsNetWork)
		{
			string value = Encrypt(AESkey, AESiv, GetTimeStampMilliSecond().ToString());
			Dictionary<string, string> header = new Dictionary<string, string> { { "signing", value } };
			string postdata = "softwareId=" + softwareId + "&numbering=" + numbering;
			jsonstr = RequestData.HttpGet(urlGetQrCode, postdata, header);
			JObject jObject = JObject.Parse(jsonstr);
			if ((string)jObject["code"] == "0")
			{
				urlQRCode = (string)jObject["codeUrl"];
				sessionId = "sessionId=" + (string)jObject["sessionId"];
			}
			else
			{
				StrGetQRCode = (string)jObject["msg"];
			}
			Debug.Log(jObject);
		}
	}

	public void GetToken()
	{
		if (IsNetWork && sessionId != "")
		{
			string value = Encrypt(AESkey, AESiv, GetTimeStampMilliSecond().ToString());
			Dictionary<string, string> header = new Dictionary<string, string> { { "signing", value } };
			jsonstr = RequestData.HttpPost(urlGetToken, sessionId, header);
			JObject jObject = JObject.Parse(jsonstr);
			if ((string)jObject["code"] == "0")
			{
				islogin = true;
				token = (string)jObject["token"];
			}
		}
	}

	public void GetRechargeCode(string numbering)
	{
		if (IsNetWork)
		{
			string value = Encrypt(AESkey, AESiv, GetTimeStampMilliSecond().ToString());
			Dictionary<string, string> header = new Dictionary<string, string> { { "signing", value } };
			string postdata = "numbering=" + numbering;
			jsonstr = RequestData.HttpGet(urlGetRechargeCode, postdata, header);
			JObject jObject = JObject.Parse(jsonstr);
			if ((string)jObject["code"] == "0")
			{
				urlRechargeCode = (string)jObject["codeUrl"];
			}
			else
			{
				urlRechargeCode = "";
			}
			Debug.Log(jObject);
		}
	}

	public void GetBindingCode(string numbering, string percentage, string mockType)
	{
		if (IsNetWork)
		{
			string value = Encrypt(AESkey, AESiv, GetTimeStampMilliSecond().ToString());
			Dictionary<string, string> header = new Dictionary<string, string> { { "signing", value } };
			string postdata = "numbering=" + numbering + "&percentage=" + percentage + "&mockType=" + mockType;
			jsonstr = RequestData.HttpGet(urlGetBindingCode, postdata, header);
			JObject jObject = JObject.Parse(jsonstr);
			string text = (string)jObject["code"];
			if (text == "0")
			{
				urlBindingCode = (string)jObject["codeUrl"];
			}
			else if (text == "1")
			{
				StrBinding = (string)jObject["msg"];
				urlRechargeCode = "";
			}
			else
			{
				urlRechargeCode = "";
			}
			Debug.Log(jObject);
		}
	}

	public float GetUserData()
	{
		float result = 0f;
		if (IsNetWork)
		{
			string value = Encrypt(AESkey, AESiv, GetTimeStampMilliSecond().ToString());
			Dictionary<string, string> header = new Dictionary<string, string>
			{
				{ "signing", value },
				{ "token", token }
			};
			jsonstr = RequestData.HttpGet(urlGetUserData, header);
			JObject jObject = JObject.Parse(jsonstr);
			if ((string)jObject["code"] == "0")
			{
				float num = (float)jObject["user"]["hours"];
				float num2 = (float)jObject["user"]["usedHours"];
				result = num - num2;
			}
			Debug.Log(jObject);
		}
		return result;
	}

	public void SendStartTraining(string codeName)
	{
		if (IsNetWork && !IsYouKeModel)
		{
			Dictionary<string, string> postdata = new Dictionary<string, string> { { "codeName", codeName } };
			string value = Encrypt(AESkey, AESiv, GetTimeStampMilliSecond().ToString());
			Dictionary<string, string> header = new Dictionary<string, string>
			{
				{ "signing", value },
				{ "token", token }
			};
			jsonstr = RequestData.HttpPost(urlSendStart, postdata, header);
			JObject jObject = JObject.Parse(jsonstr);
			RecordId = (string)jObject["result"]["recordId"];
			IsStartTraining = true;
			Debug.Log("开始训练" + jsonstr);
		}
	}

	public void SendSTrainingError(string errorId)
	{
		Dictionary<string, string> postdata = new Dictionary<string, string>
		{
			{ "errorId", errorId },
			{ "recordId", RecordId }
		};
		string value = Encrypt(AESkey, AESiv, GetTimeStampMilliSecond().ToString());
		Dictionary<string, string> header = new Dictionary<string, string>
		{
			{ "signing", value },
			{ "token", token }
		};
		jsonstr = RequestData.HttpPost(urlSendError, postdata, header);
		Debug.Log("训练错误" + jsonstr);
	}

	public void SendOverTraining(string chargeFlag, string fraction, string isPass)
	{
		if (IsNetWork && !IsYouKeModel && IsStartTraining)
		{
			Dictionary<string, string> postdata = new Dictionary<string, string>
			{
				{ "chargeFlag", chargeFlag },
				{ "fraction", fraction },
				{ "isPass", isPass },
				{ "recordId", RecordId }
			};
			string value = Encrypt(AESkey, AESiv, GetTimeStampMilliSecond().ToString());
			Dictionary<string, string> header = new Dictionary<string, string>
			{
				{ "signing", value },
				{ "token", token }
			};
			jsonstr = RequestData.HttpPost(urlSendEnd, postdata, header);
			IsStartTraining = false;
			Debug.Log("分数" + fraction + "通过" + isPass + "训练结束" + jsonstr);
		}
	}

	public void SendTiming()
	{
		if (IsNetWork && !IsYouKeModel)
		{
			string value = Encrypt(AESkey, AESiv, GetTimeStampMilliSecond().ToString());
			Dictionary<string, string> header = new Dictionary<string, string>
			{
				{ "signing", value },
				{ "token", token }
			};
			jsonstr = RequestData.HttpGet(urlSendTiming, header);
			if ((string)JObject.Parse(jsonstr)["code"] == "0")
			{
				IsHeartbeatRight = true;
			}
			else
			{
				IsHeartbeatRight = false;
			}
			Debug.Log("计时成功" + jsonstr);
		}
	}

	private void SendTimingHeartbeat()
	{
		while (true)
		{
			Thread.Sleep(30000);
			if (IsStartTraining)
			{
				SendTiming();
			}
		}
	}

	private void SendErrorId()
	{
		while (true)
		{
			if (IsSendError && IsNetWork && !IsYouKeModel)
			{
				SendSTrainingError(errorid);
				IsSendError = false;
			}
		}
	}

	public Texture2D Base64StringToTexture(string base64Str)
	{
		try
		{
			base64Str = base64Str.Replace("data:image/jpg;base64,", "");
			byte[] data = Convert.FromBase64String(base64Str);
			Texture2D texture2D = new Texture2D(10, 10);
			texture2D.LoadImage(data);
			return texture2D;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public long GetTimeStampMilliSecond()
	{
		TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
		try
		{
			return Convert.ToInt64(timeSpan.TotalMilliseconds);
		}
		catch (Exception arg)
		{
			Debug.Log($"GetTimeStampMilliSecond Error = {arg}");
			return 0L;
		}
	}

	public string Encrypt(string key, string iv, string sourceContent)
	{
		return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv), Encoding.UTF8.GetBytes(sourceContent)));
	}

	public byte[] Encrypt(byte[] key, byte[] iv, byte[] sourceContent)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		if (iv == null)
		{
			throw new ArgumentNullException("iv");
		}
		if (sourceContent == null)
		{
			throw new ArgumentNullException("sourceContent");
		}
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (Aes aes = Aes.Create())
			{
				using (ICryptoTransform cryptoTransform = aes.CreateEncryptor(key, iv))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
					{
						cryptoStream.Write(sourceContent, 0, sourceContent.Length);
						cryptoStream.FlushFinalBlock();
					}
				}
			}
			return memoryStream.ToArray();
		}
	}

	public void OutLogin()
	{
		islogin = false;
		urlQRCode = "";
		sessionId = "";
		token = "";
		urlRechargeCode = "";
		urlBindingCode = "";
		IsYouKeModel = false;
		IsStartTraining = false;
		IsHeartbeatRight = true;
		StrBinding = "";
		StrGetQRCode = "";
	}

	private void OnApplicationQuit()
	{
		if (SendErrorThread != null)
		{
			SendErrorThread.Interrupt();
			SendErrorThread.Abort();
		}
		if (SendTrainingHeartbeat != null)
		{
			SendTrainingHeartbeat.Interrupt();
			SendTrainingHeartbeat.Abort();
		}
	}
}
