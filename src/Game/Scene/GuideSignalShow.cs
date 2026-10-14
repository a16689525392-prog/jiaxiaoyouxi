using Car;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace Scene
{
	public class GuideSignalShow : MonoBehaviour
	{
		public Transform cluthIcon;

		public Text cluthText;

		public Transform footbreakIcon;

		public Text footbreakText;

		public Transform oilIcon;

		public Text oilText;

		public Transform steer;

		public Text steerText;

		public GameObject gearIcon;

		public GameObject bg;

		private Vector3 UpVec3 = new Vector3(456f, 217f, 0f);

		private Vector3 DownVec3 = new Vector3(456f, 165f, 0f);

		private int preGear;

		private CarSignal signal;

		private Vector3[] gearPosition = new Vector3[7]
		{
			new Vector3(0f, 0f),
			new Vector3(-22f, 26f),
			new Vector3(-22f, -26f),
			new Vector3(0f, 26f),
			new Vector3(0f, -26f),
			new Vector3(22f, 26f),
			new Vector3(22f, -26f)
		};

		private Vector3 tempVec0;

		private Vector3 targetPositon;

		private int step = -1;

		private void Update()
		{
			UpdateSignal();
		}

		private void UpdateSignal()
		{
			if (CarDriveHistoryPlay.Instance != null)
			{
				signal = CarDriveHistoryPlay.Instance.GetCurRecordState().carSignal;
				if (SettingInfoCtl.Instance.isManualGear)
				{
					cluthIcon.localPosition = new Vector3(456f, 217f - 52f * signal.cluth, 0f);
					footbreakIcon.localPosition = new Vector3(456f, 217f - 52f * signal.footBrake, 0f);
					cluthText.text = (signal.cluth * 100f).ToString("f0");
					footbreakText.text = (signal.footBrake * 100f).ToString("f0");
					UpdateGearShow();
				}
				else
				{
					cluthIcon.parent.gameObject.SetActive(value: false);
					footbreakIcon.localPosition = new Vector3(456f, 217f - 52f * signal.cluth, 0f);
					footbreakText.text = (signal.cluth * 100f).ToString("f0");
					gearIcon.transform.parent.gameObject.SetActive(value: false);
				}
				oilIcon.localPosition = new Vector3(456f, 217f - 52f * signal.oil, 0f);
				oilText.text = (signal.oil * 100f).ToString("f0");
				steer.localEulerAngles = new Vector3(0f, 0f, (0f - signal.steerWheel) * CarSignalValue.SteerWheeMaxlAngle);
				steerText.text = (signal.steerWheel * CarSignalValue.SteerWheeMaxlAngle).ToString("f0");
			}
		}

		private void UpdateGearShow()
		{
			if (signal.gear != preGear)
			{
				if (signal.gear == 0)
				{
					tempVec0 = new Vector3(gearPosition[preGear].x, 0f);
					targetPositon = gearPosition[signal.gear];
				}
				else
				{
					tempVec0 = new Vector3(gearPosition[signal.gear].x, 0f);
					targetPositon = gearPosition[signal.gear];
				}
				step = 0;
				preGear = signal.gear;
			}
			if (step == 0)
			{
				gearIcon.transform.localPosition = Vector3.Lerp(gearIcon.transform.localPosition, tempVec0, Time.deltaTime * 10f);
				if ((double)(gearIcon.transform.localPosition - tempVec0).magnitude < 0.5)
				{
					step = 1;
					gearIcon.transform.localPosition = tempVec0;
				}
			}
			else if (step == 1)
			{
				gearIcon.transform.localPosition = Vector3.Lerp(gearIcon.transform.localPosition, targetPositon, Time.deltaTime * 10f);
				if ((double)(gearIcon.transform.localPosition - targetPositon).magnitude < 0.5)
				{
					step = -1;
					gearIcon.transform.localPosition = targetPositon;
				}
			}
		}
	}
}
