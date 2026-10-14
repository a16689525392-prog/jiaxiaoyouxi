using System.Collections.Generic;

namespace Scene
{
	public class QuestionTrainInfo
	{
		public float questionStartTime;

		public string content;

		public string answer;

		public int result;

		public int oriState;

		public float questionEndTime;

		public List<LightOperate> operateList = new List<LightOperate>();
	}
}
