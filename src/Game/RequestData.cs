using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using LitJson;
using UnityEngine;

public class RequestData : MonoBehaviour
{
	public static string HttpPost(string Url, string postDataStr)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
		httpWebRequest.Method = "POST";
		httpWebRequest.ContentType = "application/x-www-form-urlencoded";
		byte[] bytes = Encoding.ASCII.GetBytes(postDataStr);
		httpWebRequest.ContentLength = bytes.Length;
		using (Stream stream = httpWebRequest.GetRequestStream())
		{
			stream.Write(bytes, 0, bytes.Length);
		}
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		string text = httpWebResponse.ContentEncoding;
		if (text == null || text.Length < 1)
		{
			text = "UTF-8";
		}
		StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(text));
		string result = streamReader.ReadToEnd();
		httpWebRequest.Abort();
		httpWebResponse.Close();
		streamReader.Close();
		return result;
	}

	public static string HttpPost(string Url, string postDataStr, Dictionary<string, string> Header)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
		httpWebRequest.Method = "POST";
		httpWebRequest.ContentType = "application/x-www-form-urlencoded";
		byte[] bytes = Encoding.ASCII.GetBytes(postDataStr);
		httpWebRequest.ContentLength = bytes.Length;
		foreach (KeyValuePair<string, string> item in Header)
		{
			httpWebRequest.Headers.Add(item.Key, item.Value);
		}
		using (Stream stream = httpWebRequest.GetRequestStream())
		{
			stream.Write(bytes, 0, bytes.Length);
		}
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		string text = httpWebResponse.ContentEncoding;
		if (text == null || text.Length < 1)
		{
			text = "UTF-8";
		}
		StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(text));
		string result = streamReader.ReadToEnd();
		httpWebRequest.Abort();
		httpWebResponse.Close();
		streamReader.Close();
		return result;
	}

	public static string HttpPost(string Url, Dictionary<string, string> Postdata)
	{
		JsonData jsonData = new JsonData();
		foreach (KeyValuePair<string, string> Postdatum in Postdata)
		{
			jsonData[Postdatum.Key] = Postdatum.Value;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(jsonData.ToJson());
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
		httpWebRequest.Method = "POST";
		httpWebRequest.ContentType = "application/json";
		httpWebRequest.ContentLength = bytes.Length;
		using (Stream stream = httpWebRequest.GetRequestStream())
		{
			stream.Write(bytes, 0, bytes.Length);
		}
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		string text = httpWebResponse.ContentEncoding;
		if (text == null || text.Length < 1)
		{
			text = "UTF-8";
		}
		StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(text));
		string result = streamReader.ReadToEnd();
		httpWebRequest.Abort();
		httpWebResponse.Close();
		streamReader.Close();
		return result;
	}

	public static string HttpPost(string Url, Dictionary<string, string> Postdata, Dictionary<string, string> Header)
	{
		JsonData jsonData = new JsonData();
		foreach (KeyValuePair<string, string> Postdatum in Postdata)
		{
			jsonData[Postdatum.Key] = Postdatum.Value;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(jsonData.ToJson());
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
		httpWebRequest.Method = "POST";
		httpWebRequest.ContentType = "application/json";
		httpWebRequest.ContentLength = bytes.Length;
		foreach (KeyValuePair<string, string> item in Header)
		{
			httpWebRequest.Headers.Add(item.Key, item.Value);
		}
		using (Stream stream = httpWebRequest.GetRequestStream())
		{
			stream.Write(bytes, 0, bytes.Length);
		}
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		string text = httpWebResponse.ContentEncoding;
		if (text == null || text.Length < 1)
		{
			text = "UTF-8";
		}
		StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(text));
		string result = streamReader.ReadToEnd();
		httpWebRequest.Abort();
		httpWebResponse.Close();
		streamReader.Close();
		return result;
	}

	public static string HttpGet(string Url, string Postdata)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url + "?" + Postdata);
		httpWebRequest.Method = "GET";
		httpWebRequest.ContentType = "application/json";
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		string text = httpWebResponse.ContentEncoding;
		if (text == null || text.Length < 1)
		{
			text = "UTF-8";
		}
		StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(text));
		string result = streamReader.ReadToEnd();
		httpWebRequest.Abort();
		httpWebResponse.Close();
		streamReader.Close();
		return result;
	}

	public static string HttpGet(string Url, string Postdata, Dictionary<string, string> Header)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url + "?" + Postdata);
		httpWebRequest.Method = "GET";
		httpWebRequest.ContentType = "application/json";
		foreach (KeyValuePair<string, string> item in Header)
		{
			httpWebRequest.Headers.Add(item.Key, item.Value);
		}
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		string text = httpWebResponse.ContentEncoding;
		if (text == null || text.Length < 1)
		{
			text = "UTF-8";
		}
		StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(text));
		string result = streamReader.ReadToEnd();
		httpWebRequest.Abort();
		httpWebResponse.Close();
		streamReader.Close();
		return result;
	}

	public static string HttpGet(string Url, Dictionary<string, string> Header)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
		httpWebRequest.Method = "GET";
		httpWebRequest.ContentType = "application/json";
		foreach (KeyValuePair<string, string> item in Header)
		{
			httpWebRequest.Headers.Add(item.Key, item.Value);
		}
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		string text = httpWebResponse.ContentEncoding;
		if (text == null || text.Length < 1)
		{
			text = "UTF-8";
		}
		StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(text));
		string result = streamReader.ReadToEnd();
		httpWebRequest.Abort();
		httpWebResponse.Close();
		streamReader.Close();
		return result;
	}
}
