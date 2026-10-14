using UnityEngine;

namespace Global
{
	public class MyDiagnosis : MonoBehaviour
	{
		public static MyDiagnosis Instance;

		private void Awake()
		{
			Instance = this;
		}

		public bool JudgeSoftCorrect()
		{
			DataCenter.Instance.SoftErrorCode = 0;
			return true;
		}

		public string GetErrorContent()
		{
			JudgeSoftCorrect();
			string result = "";
			switch (DataCenter.Instance.SoftErrorCode)
			{
			case 1:
				result = "没有检测到加密锁或者集成加密主板";
				break;
			case 2:
				result = "集成加密主板类型和软件不匹配";
				break;
			case 3:
				result = "三屏显示必须使用三屏的集成加密主板";
				break;
			}
			return result;
		}
	}
}
