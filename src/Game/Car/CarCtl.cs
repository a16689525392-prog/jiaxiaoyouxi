using Global;
using Scene;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Car
{
	public class CarCtl : MonoBehaviour
	{
		public static CarCtl instance;

		[SerializeField]
		private WheelCollider[] m_WheelColliders = new WheelCollider[4];

		[SerializeField]
		private GameObject[] m_WheelMeshes = new GameObject[4];

		[SerializeField]
		private WheelEffects[] m_WheelEffects = new WheelEffects[4];

		[SerializeField]
		private Vector3 m_CentreOfMassOffset = Vector3.zero;

		[SerializeField]
		private float m_MaximumSteerAngle = 35f;

		[SerializeField]
		private float m_Topspeed = 200f;

		[SerializeField]
		private float m_SlipLimit = 0.4f;

		[SerializeField]
		private float m_BrakeTorque = 500f;

		private Quaternion[] m_WheelMeshLocalRotations;

		private Vector3 m_Prevpos;

		private Vector3 m_Pos;

		private float m_GearFactor;

		private float m_OldRotation;

		public Rigidbody m_Rigidbody;

		private const float k_ReversingThreshold = 0.01f;

		public GameObject SnowBrushGameObject;

		public float M;

		private GameObject CarSignOnMap;

		private GameObject SmallMap;

		public static float RX;

		private float RY;

		public static float RZ;

		private float RYtime;

		private float Ry;

		private float Ratime;

		private float V0;

		private float Ra;

		public static float Rcarx;

		public static float RXTest;

		public static bool IsNoOil;

		private Vector3 lastPosition;

		private float fixeddeltaTime;

		public float TransformVelocity;

		private AudioSource fireOnAudio;

		private AudioSource fireOffAudio;

		private float fireOnIntervalTime = 1f;

		public bool isFireOn;

		public bool isFireOff;

		private float maxEngineSpeed = 5600f;

		private float curShowEngineSpeed;

		private float targetEngineSpeed;

		private float[] maxSpeed = new float[7] { 0f, 37f, 60f, 95f, 100f, 140f, 40f };

		private float tempXAngle;

		private const float DOWN_FORCE = -100f;

		public bool Skidding { get; private set; }

		public float MaxSpeed => m_Topspeed;

		public float Revs { get; private set; }

		public float CurrentSpeed
		{
			get
			{
				if (m_Rigidbody == null)
				{
					return 0f;
				}
				if (Vector3.Angle(m_Rigidbody.transform.forward, m_Rigidbody.velocity) > 90f)
				{
					return 0f - m_Rigidbody.velocity.magnitude;
				}
				return m_Rigidbody.velocity.magnitude;
			}
		}

		public float CurShowEngineSpeed => curShowEngineSpeed;

		public static CarCtl Instance => instance;

		private void Awake()
		{
			instance = this;
			if (ViewJumpCtl.Instance.otherType != 502 && SnowBrushGameObject != null)
			{
				SnowBrushGameObject.SetActive(value: false);
			}
		}

		private void Start()
		{
			m_WheelMeshLocalRotations = new Quaternion[4];
			for (int i = 0; i < 4; i++)
			{
				m_WheelMeshLocalRotations[i] = m_WheelMeshes[i].transform.localRotation;
			}
			m_WheelColliders[0].attachedRigidbody.centerOfMass = m_CentreOfMassOffset;
			m_Rigidbody = base.transform.GetComponent<Rigidbody>();
			if ((ViewJumpCtl.Instance.modelType == 1 || ViewJumpCtl.Instance.modelType == 0) && ViewJumpCtl.Instance.itemType != 615)
			{
				m_Rigidbody.isKinematic = true;
			}
			else
			{
				m_Rigidbody.isKinematic = false;
			}
			Ry = m_Rigidbody.transform.localEulerAngles.y;
			V0 = 0f;
			if (SceneJumpCtl.Instance.CoursetwoName == CoursetwoScenesName.FoShan || SceneJumpCtl.Instance.CoursetwoName == CoursetwoScenesName.HeBei)
			{
				m_MaximumSteerAngle = 45f;
			}
			lastPosition = base.transform.position;
		}

		private void OnDestroy()
		{
			if (SmallMap != null)
			{
				Object.Destroy(SmallMap);
			}
			SettingInfoCtl.Instance.Mileage += M;
			if (SettingInfoCtl.Instance.Mileage > 9999f)
			{
				SettingInfoCtl.Instance.Mileage = 0f;
			}
			SettingInfoCtl.Instance.SaveSetingInfos();
			GameGlobalCtl.PXvalue = 0f;
			GameGlobalCtl.PYvalue = 0f;
			GameGlobalCtl.PZvalue = 0f;
			GameGlobalCtl.RXvalue = 0f;
			GameGlobalCtl.RYvalue = 0f;
			GameGlobalCtl.RZvalue = 0f;
		}

		private void FixedUpdate()
		{
			fixeddeltaTime += Time.fixedDeltaTime;
			if (fixeddeltaTime > 0.1f)
			{
				TransformVelocity = ((base.transform.position - lastPosition) / fixeddeltaTime).magnitude * 3.6f;
				lastPosition = base.transform.position;
				fixeddeltaTime = 0f;
			}
			Ratime += Time.fixedDeltaTime;
			if (Ratime > 0.5f)
			{
				Ra = (CurrentSpeed - V0) / 0.5f;
				V0 = CurrentSpeed;
				Ratime = 0f;
			}
			if ((double)Mathf.Abs(Ra) <= 0.5)
			{
				Rcarx = Ra * 0.3f;
			}
			else if ((double)Mathf.Abs(Ra) > 0.5 && Mathf.Abs(Ra) <= 1f)
			{
				Rcarx = Ra * 0.8f;
			}
			else if (Mathf.Abs(Ra) > 1f && Mathf.Abs(Ra) <= 2f)
			{
				Rcarx = Ra * 1f;
			}
			else if (Mathf.Abs(Ra) > 2f && Mathf.Abs(Ra) <= 3f)
			{
				Rcarx = Ra * 1.2f;
			}
			else if (Mathf.Abs(Ra) > 3f)
			{
				Rcarx = Ra / Mathf.Abs(Ra) * 2.7f;
			}
			if (m_Rigidbody.transform.localEulerAngles.x > 270f && m_Rigidbody.transform.localEulerAngles.x < 360f)
			{
				RXTest = 360f - m_Rigidbody.transform.localEulerAngles.x;
				RX = 360f - m_Rigidbody.transform.localEulerAngles.x + Rcarx;
				GameGlobalCtl.RXvalue = RX / 90f;
			}
			else if (m_Rigidbody.transform.localEulerAngles.x > 0f && m_Rigidbody.transform.localEulerAngles.x < 90f)
			{
				RXTest = 0f - m_Rigidbody.transform.localEulerAngles.x;
				RX = 0f - m_Rigidbody.transform.localEulerAngles.x + Rcarx;
				GameGlobalCtl.RXvalue = RX / 90f;
			}
			if (m_Rigidbody.transform.localEulerAngles.z > 270f && m_Rigidbody.transform.localEulerAngles.z < 360f)
			{
				RZ = m_Rigidbody.transform.localEulerAngles.z - 360f;
				GameGlobalCtl.RZvalue = RZ / 90f;
			}
			else if (m_Rigidbody.transform.localEulerAngles.z > 0f && m_Rigidbody.transform.localEulerAngles.z < 90f)
			{
				RZ = m_Rigidbody.transform.localEulerAngles.z;
				GameGlobalCtl.RZvalue = RZ / 90f;
			}
			RYtime += Time.fixedDeltaTime;
			if (RYtime > 0.05f)
			{
				RY = m_Rigidbody.transform.localEulerAngles.y - Ry;
				GameGlobalCtl.RYvalue = RY / 90f;
				RYtime = 0f;
				Ry = m_Rigidbody.transform.localEulerAngles.y;
			}
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				M += m_Rigidbody.velocity.magnitude * Time.deltaTime;
			}
			if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1)
			{
				if (isFireOn && fireOnAudio != null)
				{
					fireOnAudio.pitch = 0.8f + 0.5f * (CarState.Instance.CurEngineSpeed - 800f) / 5200f;
				}
			}
			else if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && isFireOn && fireOnAudio != null)
			{
				fireOnAudio.pitch = 0.8f + 0.5f * (DriveHistoryPlay.Instance.curCalculateEngineSpeedH - 800f) / 5200f;
			}
		}

		public void Move(int fire, float steering, float oil, float cluth, float footbreak, float handbreak, int gear)
		{
			if (m_Rigidbody == null)
			{
				return;
			}
			JudgeFireOn(fire);
			SteerWheelCtl(steering);
			if (handbreak > 0.7f)
			{
				for (int i = 0; i < 4; i++)
				{
					m_WheelColliders[i].brakeTorque = (handbreak + footbreak) * m_BrakeTorque / 2f;
				}
			}
			else
			{
				for (int j = 0; j < 4; j++)
				{
					m_WheelColliders[j].brakeTorque = 0f;
				}
			}
			AddDriveForce();
			CheckForWheelSpin();
		}

		public void SetMotoTorque(float value)
		{
			WheelCollider[] wheelColliders = m_WheelColliders;
			for (int i = 0; i < wheelColliders.Length; i++)
			{
				wheelColliders[i].motorTorque = value;
			}
		}

		private void JudgeFireOn(int fire)
		{
			fireOnIntervalTime += Time.deltaTime;
			if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1)
			{
				if (fire == 1 && !isFireOn && fireOnIntervalTime > 1f)
				{
					if (ViewJumpCtl.Instance.trainType == TrainType.NightLight || (ViewJumpCtl.Instance.trainType == TrainType.BasicTrain && ViewJumpCtl.Instance.itemType == 300))
					{
						SoundPlayCtl.Instance.PlaySound("当前训练项目不能进行起步操作");
					}
					else
					{
						isFireOn = true;
					}
					fireOnIntervalTime = 0f;
				}
				else if (isFireOn && CarState.Instance.CarSignal.power == 0 && (SignalSourceCtl.Instance.Pid == "400A" || SignalSourceCtl.Instance.Pid == "4100") && fireOnIntervalTime > 1f)
				{
					isFireOn = false;
				}
				else if (isFireOn && fire == 1 && SignalSourceCtl.Instance.Pid == "4200" && fireOnIntervalTime > 0.8f)
				{
					isFireOn = false;
					fireOnIntervalTime = 0f;
				}
			}
			else if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
			{
				if ((DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fire == 1 && !isFireOn && fireOnIntervalTime > 1f) || (DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].engineSpeed > 10f && !isFireOn))
				{
					if (ViewJumpCtl.Instance.trainType == TrainType.NightLight || (ViewJumpCtl.Instance.trainType == TrainType.BasicTrain && ViewJumpCtl.Instance.itemType == 300))
					{
						SoundPlayCtl.Instance.PlaySound("当前训练项目不能进行起步操作");
					}
					else
					{
						isFireOn = true;
					}
					fireOnIntervalTime = 0f;
				}
				else if (isFireOn && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fire == 1 && (SignalSourceCtl.Instance.Pid == "400A" || SignalSourceCtl.Instance.Pid == "4100") && fireOnIntervalTime > 1f)
				{
					isFireOn = false;
				}
				else if (isFireOn && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fire == 1 && SignalSourceCtl.Instance.Pid == "4200" && fireOnIntervalTime > 0.8f)
				{
					isFireOn = false;
					fireOnIntervalTime = 0f;
				}
			}
			if (!(TrainSceneBase.BaseInstance != null) || !(CarState.Instance.CurCar != null))
			{
				return;
			}
			if (isFireOn && fireOnAudio == null)
			{
				fireOnAudio = AudioSourceCtl.Instance.PlayAudio("CarEngine", isLoop: true);
				if (fireOnAudio != null)
				{
					fireOnAudio.volume = 0.8f;
				}
				if (ViewJumpCtl.Instance.itemType == 401 || ViewJumpCtl.Instance.itemType == 403 || ViewJumpCtl.Instance.itemType == 511 || ViewJumpCtl.Instance.itemType == 404 || ViewJumpCtl.Instance.itemType == 5111 || ViewJumpCtl.Instance.itemType == 5110 || ViewJumpCtl.Instance.itemType == 5100)
				{
					AiCar.Instance.isHit = false;
				}
			}
			else if (!isFireOn && fireOnAudio != null)
			{
				AudioSourceCtl.Instance.StopPlayAudio(fireOnAudio);
				fireOnAudio = null;
				isFireOff = true;
			}
			else if (!isFireOn && isFireOff)
			{
				if (fireOffAudio != null)
				{
					fireOffAudio.volume = 0.5f;
					fireOffAudio.loop = false;
					CarState.Instance.StopCar();
				}
				isFireOff = false;
			}
		}

		public float GetTargetSpeed(float oil, float cluth, int gear)
		{
			float num = 0f;
			float a = 0.15f;
			if (gear == 5)
			{
				a = 0.26f;
			}
			if (SettingInfoCtl.Instance.isManualGear)
			{
				num = Mathf.Lerp(a, 1f, oil) * (1f - cluth) * maxSpeed[gear];
			}
			else
			{
				float num2 = Mathf.Lerp(a, 1f, oil);
				num = num2 * 30f + num2 * num2 * 60f;
			}
			return (float)(int)num + 0.5f;
		}

		public float GetCurEngineSpeed(float oil)
		{
			oil = ((!isFireOn) ? 0f : Mathf.Lerp(0.16f, 1f, oil));
			targetEngineSpeed = maxEngineSpeed * oil;
			if (Mathf.Abs(curShowEngineSpeed - targetEngineSpeed) < 50f)
			{
				curShowEngineSpeed = targetEngineSpeed;
			}
			else
			{
				curShowEngineSpeed = Mathf.Lerp(curShowEngineSpeed, targetEngineSpeed, 0.1f);
			}
			return curShowEngineSpeed;
		}

		private void SteerWheelCtl(float steering)
		{
			tempXAngle = steering * m_MaximumSteerAngle;
			Vector3 localEulerAngles = m_WheelMeshes[0].transform.parent.localEulerAngles;
			localEulerAngles.y = tempXAngle;
			m_WheelMeshes[0].transform.parent.localEulerAngles = localEulerAngles;
			m_WheelMeshes[1].transform.parent.localEulerAngles = localEulerAngles;
			m_WheelColliders[0].steerAngle = tempXAngle;
			m_WheelColliders[1].steerAngle = tempXAngle;
			float x = CurrentSpeed * 5f;
			if (ViewJumpCtl.Instance.modelType == 1 && ViewJumpCtl.Instance.trainType != TrainType.NightLight)
			{
				x = CarDriveHistoryPlay.Instance.curCalculateSpeed * 5f;
			}
			if (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5)
			{
				x = ((ViewJumpCtl.Instance.trainType != TrainType.CourseThree || !(CourseThreeYanShiGuide.Instance != null)) ? (DriveHistoryPlay.Instance.curCalculateSpeedH * 5f) : ((!CourseThreeYanShiGuide.Instance.YanShiPause) ? 15f : 0f));
			}
			if (CarDriveHistoryPlay.Instance == null || CarDriveHistoryPlay.Instance.IsPlaying())
			{
				for (int i = 0; i < 4; i++)
				{
					m_WheelMeshes[i].transform.Rotate(new Vector3(x, 0f, 0f));
					tempXAngle = m_WheelMeshes[i].transform.localEulerAngles.x;
				}
			}
		}

		private void AddDriveForce()
		{
			m_Rigidbody.angularDrag = MoNiCarDynamic.Instance.dynamicData.angularDrag;
			if (CarState.Instance.CarSignal.footBrake > 0f)
			{
				m_Rigidbody.drag = MoNiCarDynamic.Instance.dynamicData.drag;
			}
			else if (CarState.Instance.CarSignal.cluth >= 0.2f)
			{
				m_Rigidbody.drag = MoNiCarDynamic.Instance.dynamicData.drag + CarState.Instance.CarSignal.cluth * 0.2f;
			}
			else
			{
				m_Rigidbody.drag = MoNiCarDynamic.Instance.dynamicData.drag;
			}
			Vector3 direction = new Vector3(0f, -100f, MoNiCarDynamic.Instance.dynamicData.driveForce);
			m_Rigidbody.AddForce(base.transform.TransformDirection(direction));
			if (Mathf.Abs(MoNiCarDynamic.Instance.dynamicData.driveForce) > 10f && Mathf.Abs(CarState.Instance.CurSpeed) < 1f && CarState.Instance.CarSignal.handBrake < 0.4f)
			{
				float num = MoNiCarDynamic.Instance.dynamicData.driveForce / 5000f;
				SetMotoTorque(80f * num);
			}
			else if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo && ViewJumpCtl.Instance.itemType == 2)
			{
				SetMotoTorque(-1f);
			}
			else
			{
				SetMotoTorque(0f);
			}
		}

		private void CheckForWheelSpin()
		{
			for (int i = 0; i < 4; i++)
			{
				m_WheelColliders[i].GetGroundHit(out var hit);
				if (!(Mathf.Abs(hit.forwardSlip) >= m_SlipLimit) && !(Mathf.Abs(hit.sidewaysSlip) >= m_SlipLimit))
				{
					m_WheelEffects[i].EndSkidTrail();
				}
			}
		}
	}
}
