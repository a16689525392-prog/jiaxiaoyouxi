using System;
using System.Collections;
using Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using View;

namespace Scene
{
	public class SceneJumpCtl : MonoBehaviour
	{
		public UnityAction<SceneType> EnterSceneAction;

		public CoursetwoScenesName CoursetwoName;

		public CoursethreeScenesName CoursethreeName;

		public int SceneWayNumber;

		public int SceneWay;

		private static AssetBundle sceneAssetBundle;

		private byte[] sceneBytes;

		private AsyncOperation asyncOperation;

		private byte[] tempBytes;

		private bool isUsePlayerSetting;

		public static SceneJumpCtl Instance { get; private set; }

		public SceneType CurSceneType { get; private set; }

		private void Awake()
		{
			Instance = this;
			CurSceneType = SceneType.MainScene;
		}

		private void Start()
		{
			EnterScene(SceneType.MainScene);
		}

		public void EnterScene(SceneType sceneType)
		{
			CurSceneType = sceneType;
			if (SceneManager.GetActiveScene().name != SceneType.Loading.ToString())
			{
				SceneManager.LoadSceneAsync(SceneType.Loading.ToString());
			}
			EnterSceneAction?.Invoke(CurSceneType);
		}

		internal void EnterScene(object co)
		{
			throw new NotImplementedException();
		}

		public IEnumerator LoadEncryptScene(SceneType sceneType = SceneType.Default)
		{
			if (sceneType == SceneType.Default)
			{
				sceneType = CurSceneType;
			}
			if (sceneAssetBundle != null)
			{
				sceneAssetBundle.Unload(unloadAllLoadedObjects: true);
			}
			if (Application.platform == RuntimePlatform.WindowsEditor && isUsePlayerSetting)
			{
				asyncOperation = SceneManager.LoadSceneAsync(sceneType.ToString());
			}
			else
			{
				string text = sceneType.ToString().ToLower();
				string path = Application.streamingAssetsPath + "/AssetBundles/" + text + ".scene";
				UnityWebRequest unityWebRequest = UnityWebRequest.Get(DataCenter.Instance.GetWWWPath(path));
				yield return unityWebRequest.SendWebRequest();
				if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
				{
					asyncOperation = SceneManager.LoadSceneAsync(SceneType.MainScene.ToString());
					Debug.Log(unityWebRequest.error);
					yield break;
				}
				byte b = 0;
				tempBytes = new byte[unityWebRequest.downloadHandler.data.Length];
				Buffer.BlockCopy(unityWebRequest.downloadHandler.data, 0, tempBytes, 0, tempBytes.Length);
				for (int i = 1; i < tempBytes.Length; i++)
				{
					b += tempBytes[i];
				}
				if (b == unityWebRequest.downloadHandler.data[0])
				{
					sceneBytes = new byte[unityWebRequest.downloadHandler.data.Length - 1];
					Buffer.BlockCopy(unityWebRequest.downloadHandler.data, 1, sceneBytes, 0, sceneBytes.Length);
					sceneAssetBundle = AssetBundle.LoadFromMemory(sceneBytes);
					asyncOperation = SceneManager.LoadSceneAsync(sceneType.ToString());
				}
				else
				{
					asyncOperation = SceneManager.LoadSceneAsync(SceneType.MainScene.ToString());
				}
			}
			if (ViewJumpCtl.Instance.curView.GetComponent<LoadingView>() != null)
			{
				ViewJumpCtl.Instance.curView.GetComponent<LoadingView>().ShowLoadingSceneProgress(asyncOperation);
			}
		}
	}
}
