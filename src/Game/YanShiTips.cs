using SoundAndTextShow;
using UnityEngine;
using UnityEngine.UI;

public class YanShiTips : MonoBehaviour
{
	public Text SoundTips;

	public bool IsSoundStart;

	public bool IsSoundOver;

	private float DestroyTiem;

	private void Start()
	{
		IsSoundOver = false;
		DestroyTiem = 0f;
	}

	private void Update()
	{
		if (IsSoundStart && AudioSourceCtl.Instance != null && AudioSourceCtl.Instance.soundPlayAudioSource != null && !AudioSourceCtl.Instance.soundPlayAudioSource.isPlaying && !SoundPlayCtl.Instance.isSoundLoad)
		{
			IsSoundOver = true;
		}
		if (!IsSoundOver)
		{
			return;
		}
		DestroyTiem += Time.deltaTime;
		if (DestroyTiem > 1f)
		{
			if (CourseThreeYanShiGuide.Instance != null)
			{
				CourseThreeYanShiGuide.Instance.CurYanshiTipsIndex++;
				CourseThreeYanShiGuide.Instance.YanShiPause = false;
			}
			Object.Destroy(base.gameObject);
		}
	}
}
