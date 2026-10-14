using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class LoadingScene : SceneBase
	{
		public static bool isCreateGameGlobalCtl;

		public static bool isFirstCheck;

		protected override void Awake()
		{
			base.Awake();
			if (!isCreateGameGlobalCtl)
			{
				Object.Instantiate(Resources.Load("Prefabs/GameGlobalCtl"));
				isCreateGameGlobalCtl = true;
			}
		}

		protected override void Start()
		{
			base.Start();
			if (ViewJumpCtl.Instance.curView != null && ViewJumpCtl.Instance.curView.GetComponent<ViewBase>().mViewType == ViewType.SceneMenuView)
			{
				ViewJumpCtl.Instance.EnterView(ViewType.LoadingView, isDestroyCurView: true, isSave: false);
			}
			else
			{
				ViewJumpCtl.Instance.EnterView(ViewType.LoadingView);
			}
			ViewJumpCtl.Instance.SetBackgroundActive(active: true);
			SceneTextShow.Instance.ShowNormalText("");
			SceneTextShow.Instance.ShowTitleText("");
			SoundPlayCtl.Instance.SoundListReset();
			AudioSourceCtl.Instance.StopAllAudio();
			StartCoroutine(SceneJumpCtl.Instance.LoadEncryptScene());
			if (SceneJumpCtl.Instance.CurSceneType == SceneType.MainScene)
			{
				SceneTextShow.Instance.scoreText.gameObject.SetActive(value: false);
			}
			else
			{
				SceneTextShow.Instance.scoreText.gameObject.SetActive(value: true);
			}
		}
	}
}
