namespace Car
{
	public class DynamicData
	{
		public float steerWheel;

		public float clutch;

		public float oil;

		public float footBrake;

		public float handBrake;

		public float carSpeed;

		public float curIg;

		public float mass;

		public float engineSpeed;

		public bool engineIsFired;

		public byte fire;

		public byte power;

		public byte gear;

		public byte torqueModel;

		public byte flIsGround;

		public byte frIsGround;

		public byte rlIsGround;

		public byte rrIsGround;

		public float drag;

		public float angularDrag;

		public float driveForce;

		public float pullForce;

		public float fWind;

		public float fRoll;

		public float fBrake;

		public float fAdhere;

		public float fGrade;

		public float fResist;

		public float fResist01;

		public float speedX;

		public float speedY;

		public float speedZ;

		public float forwardX;

		public float forwardY;

		public float forwardZ;

		public const float CAR_GRAVITY = 9.8f;

		public byte stopCar;

		public static float[] m_C1Ig = new float[7] { 0f, 4.59f, 3.12008f, 2.1209f, 1.44169f, 0.898f, 4.131f };

		public const float m_C1AA = -1E-06f;

		public const float m_C1BB = 0.005729f;

		public const float m_C1CC = 129.973f;

		public const float m_C1Np = 5600f;

		public const float m_C1TtqMax = 140f;

		public const float m_C1Rt = 0.311651f;

		public const float m_C1I0 = 3.83649f;

		public const float m_C1EtaT = 0.92f;

		public const float m_C1MM = 1504f;

		public const float m_C1Cd = 0.35f;

		public const float m_C1AirA = 2.0134f;

		public const float m_C1RollF1 = 0.02f;

		public const float m_C1RollF2 = 5.6E-05f;

		public const float m_C1RollF3 = 0f;

		public const float m_C1MaxFootBrake = 15000f;

		public const float m_C1MaxHandbrake = 7500f;

		public const float m_C1OilMin = 0.15f;

		public const float m_Adhere = 0.7f;

		public const float m_Grade = 0f;
	}
}
