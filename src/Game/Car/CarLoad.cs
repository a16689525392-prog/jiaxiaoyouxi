namespace Car
{
	public class CarLoad
	{
		public const string JieDa = "JieDa";

		public const string DongFengDuoLiKa = "DongFengDuoLiKa";

		public const string DaBaChe = "dabache";

		public const string YongShi = "YongShi";

		public const string JieFang1121 = "JieFang1121";

		public const string QiRuiE3 = "QiRuiE3";

		public const string KaLuoLa = "KaLuoLa";

		public const string NewSangTN = "NewSangTN";

		public const string TianJin = "TianJin";

		public const string JieFang1125 = "JieFang1125";

		public const string DFKangMingSi = "DFKangMingSi";

		public const string PiKa = "PiKa";

		public const string DFMengShi = "DFMengShi";

		public const string AiLiShe = "AiLiShe";

		public const string QianYinChe = "QianYinChe";

		public static string GetCarPath(CarType type)
		{
			string result = "";
			switch (type)
			{
			case CarType.JieDaManual:
			case CarType.JieDaAuto:
				result = "JieDa";
				break;
			case CarType.DongFengDuoLiKa:
				result = "DongFengDuoLiKa";
				break;
			case CarType.YongShi:
				result = "YongShi";
				break;
			case CarType.DaBaChe:
				result = "dabache";
				break;
			case CarType.JieFang1121:
				result = "JieFang1121";
				break;
			case CarType.QiRuiE3:
				result = "QiRuiE3";
				break;
			case CarType.KaLuoLa:
				result = "KaLuoLa";
				break;
			case CarType.NewSangTN:
				result = "NewSangTN";
				break;
			case CarType.TianJin:
				result = "TianJin";
				break;
			case CarType.JieFang1125:
				result = "JieFang1125";
				break;
			case CarType.DFKangMingSi:
				result = "DFKangMingSi";
				break;
			case CarType.PiKa:
				result = "PiKa";
				break;
			case CarType.DFMengShi:
				result = "DFMengShi";
				break;
			case CarType.AiLiSheManual:
			case CarType.AiLiSheAuto:
				result = "AiLiShe";
				break;
			case CarType.QianYinChe:
				result = "QianYinChe";
				break;
			case CarType.TestCar:
				result = "NewJieDa";
				break;
			}
			return result;
		}
	}
}
