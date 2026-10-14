using System.Collections.Generic;
using UnityEngine;

namespace SoundAndTextShow
{
	public class AudioSourceCtl : MonoBehaviour
	{
		private static AudioSourceCtl instance;

		public List<AudioSource> mAudioSourceList = new List<AudioSource>();

		public AudioSource soundPlayAudioSource;

		public static AudioSourceCtl Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		public AudioSource PlayAudio(string audioName, bool isLoop = false)
		{
			AudioClip audioClip = Resources.Load<AudioClip>("Audio/" + audioName);
			AudioSource result = null;
			if (audioClip != null)
			{
				foreach (AudioSource mAudioSource in mAudioSourceList)
				{
					if (!mAudioSource.isPlaying)
					{
						mAudioSource.clip = audioClip;
						mAudioSource.loop = isLoop;
						mAudioSource.Play();
						result = mAudioSource;
						break;
					}
				}
			}
			else
			{
				Debug.LogError(audioName + "  声音文件未找到");
			}
			return result;
		}

		public void StopPlayAudio(AudioSource audioSource)
		{
			foreach (AudioSource mAudioSource in mAudioSourceList)
			{
				if (mAudioSource == audioSource)
				{
					audioSource.Stop();
					audioSource.loop = false;
					audioSource.clip = null;
					audioSource.volume = 1f;
					audioSource.pitch = 1f;
					break;
				}
			}
		}

		public void StopAllAudio()
		{
			foreach (AudioSource mAudioSource in mAudioSourceList)
			{
				mAudioSource.Stop();
				mAudioSource.loop = false;
				mAudioSource.clip = null;
				mAudioSource.volume = 1f;
				mAudioSource.pitch = 1f;
			}
		}
	}
}
