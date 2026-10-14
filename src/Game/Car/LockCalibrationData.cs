namespace Car
{
	public class LockCalibrationData
	{
		public int type = 9;

		public int btnIndex;

		public int preValue;

		public int curValue;

		public bool isPersistentSignal;

		public void Reset()
		{
			type = -1;
			btnIndex = -1;
			preValue = 0;
			curValue = 0;
			isPersistentSignal = false;
		}
	}
}
