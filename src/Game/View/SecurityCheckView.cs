using System;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace View
{
	public class SecurityCheckView : ViewBase
	{
		private VideoPlayer mVideoPlayer;

		private long Nowframe;

		private ulong FrameCount;

		private bool SendStart;

		public Slider Slider;

		private int BtnGo;

		public override void Start()
		{
			base.Start();
			mVideoPlayer = GetComponent<VideoPlayer>();
			mVideoPlayer.targetTexture.Release();
			FrameCount = 99999999uL;
			SendStart = false;
			BtnGo = SceneTextShow.VideoNumber;
			PlayVideo(BtnGo.ToString());
			GetBtn(1).transform.GetChild(1).gameObject.SetActive(value: false);
			GetBtn(1).transform.GetChild(2).gameObject.SetActive(value: true);
		}

		public void Update()
		{
			Slider.value = Convert.ToSingle(mVideoPlayer.frame) / Convert.ToSingle(FrameCount);
			if (Nowframe >= (long)(FrameCount - 1) && SendStart)
			{
				SendStart = false;
				mVideoPlayer.frame = 0L;
				mVideoPlayer.Play();
				GetBtn(1).transform.GetChild(1).gameObject.SetActive(value: true);
				GetBtn(1).transform.GetChild(2).gameObject.SetActive(value: false);
			}
			UporDownBtnState();
			Nowframe = mVideoPlayer.frame;
		}

		private void PlayVideo(string BtnBatch)
		{
			string path = "SecurityCheck/" + BtnBatch;
			mVideoPlayer.clip = Resources.Load(path) as VideoClip;
			mVideoPlayer.frame = 0L;
			Nowframe = mVideoPlayer.frame;
			mVideoPlayer.Play();
			FrameCount = mVideoPlayer.frameCount;
			SendStart = true;
		}

		private void UporDownBtnState()
		{
			if (BtnGo == 1)
			{
				GetBtn(0).SetActive(value: false);
				GetBtn(2).SetActive(value: true);
			}
			else if (BtnGo > 1 && BtnGo < 17)
			{
				GetBtn(0).SetActive(value: true);
				GetBtn(2).SetActive(value: true);
			}
			else
			{
				GetBtn(0).SetActive(value: true);
				GetBtn(2).SetActive(value: false);
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0) && go.activeSelf)
			{
				BtnGo--;
				PlayVideo(BtnGo.ToString());
				GetBtn(1).transform.GetChild(1).gameObject.SetActive(value: false);
				GetBtn(1).transform.GetChild(2).gameObject.SetActive(value: true);
			}
			else if (go == GetBtn(1))
			{
				if (go.transform.GetChild(2).gameObject.activeSelf)
				{
					go.transform.GetChild(1).gameObject.SetActive(value: true);
					go.transform.GetChild(2).gameObject.SetActive(value: false);
					mVideoPlayer.Pause();
				}
				else
				{
					go.transform.GetChild(1).gameObject.SetActive(value: false);
					go.transform.GetChild(2).gameObject.SetActive(value: true);
					mVideoPlayer.Play();
				}
			}
			else if (go == GetBtn(2) && go.activeSelf)
			{
				BtnGo++;
				PlayVideo(BtnGo.ToString());
				GetBtn(1).transform.GetChild(1).gameObject.SetActive(value: false);
				GetBtn(1).transform.GetChild(2).gameObject.SetActive(value: true);
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
