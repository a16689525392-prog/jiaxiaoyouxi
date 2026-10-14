using System;
using Global;
using Scene;
using UnityEngine;
using View;

namespace Car
{
	public class MoNiCarDynamic : MonoBehaviour
	{
		public static MoNiCarDynamic Instance;

		public DynamicData dynamicData = new DynamicData();

		private float targetEngineSpeed;

		private float eSpeedChangeDeltaTime;

		private int curgear = 1;

		private int changeCount;

		private void Awake()
		{
			Instance = this;
		}

		public DynamicData TransfromGeneralFormat(CarState carState)
		{
			dynamicData.steerWheel = carState.CarSignal.steerWheel;
			dynamicData.oil = Mathf.Lerp(0.15f, 1f, carState.CarSignal.oil);
			dynamicData.clutch = carState.CarSignal.cluth;
			if (ViewJumpCtl.Instance.itemType != 611)
			{
				dynamicData.footBrake = Mathf.Lerp(0f, 1f, carState.CarSignal.footBrake - 0.08f);
			}
			else if (ViewJumpCtl.Instance.itemType == 611 && ViewJumpCtl.Instance.modelType == 2)
			{
				if (!BrakeBad.Instance.isBrakeBad)
				{
					dynamicData.footBrake = Mathf.Lerp(0f, 1f, carState.CarSignal.footBrake - 0.08f);
				}
				else
				{
					dynamicData.footBrake = 0f;
				}
			}
			dynamicData.handBrake = Mathf.Clamp(carState.CarSignal.handBrake - 0.08f, 0f, 1f);
			dynamicData.carSpeed = carState.CurSpeed;
			if (carState.CurSpeed < 0.1f)
			{
				dynamicData.carSpeed = 0f;
			}
			if (Vector3.Dot(carState.CurCar.GetComponent<Rigidbody>().velocity, carState.CurCar.transform.forward) < 0f)
			{
				dynamicData.carSpeed = 0f - dynamicData.carSpeed;
			}
			dynamicData.curIg = 0f;
			dynamicData.mass = 0f;
			dynamicData.engineIsFired = carState.FireOn;
			dynamicData.fire = (byte)carState.CarSignal.fire;
			dynamicData.power = (byte)carState.CarSignal.power;
			dynamicData.gear = (byte)carState.CarSignal.gear;
			dynamicData.flIsGround = 0;
			dynamicData.frIsGround = 0;
			dynamicData.rlIsGround = 0;
			dynamicData.rrIsGround = 0;
			if (!SettingInfoCtl.Instance.isManualGear)
			{
				AutoToManGear();
			}
			if (!carState.FireOn || dynamicData.carSpeed < 0.01f)
			{
				dynamicData.drag = dynamicData.footBrake * 50f + dynamicData.handBrake * 30f;
				if (dynamicData.drag < 0.01f)
				{
					dynamicData.drag = dynamicData.clutch * 0.01f;
				}
				dynamicData.angularDrag = dynamicData.drag;
			}
			else
			{
				dynamicData.drag = 0f;
				dynamicData.angularDrag = 0f;
			}
			dynamicData.curIg = DynamicData.m_C1Ig[dynamicData.gear];
			CalculatEngineSpeed();
			return dynamicData;
		}

		private void CalculatEngineSpeed()
		{
			eSpeedChangeDeltaTime += Time.deltaTime;
			if (!dynamicData.engineIsFired && dynamicData.fire == 1)
			{
				dynamicData.engineSpeed = 200f;
			}
			else if (dynamicData.engineIsFired)
			{
				if (dynamicData.clutch > 0.25f || dynamicData.gear == 0)
				{
					targetEngineSpeed = 5600f * dynamicData.oil;
				}
				else
				{
					targetEngineSpeed = Math.Abs(dynamicData.carSpeed * 3.83649f * dynamicData.curIg / 0.11749242f);
				}
			}
			else
			{
				targetEngineSpeed = 0f;
			}
			if (Mathf.Abs(dynamicData.engineSpeed - targetEngineSpeed) > 30f)
			{
				if (dynamicData.engineSpeed < targetEngineSpeed)
				{
					dynamicData.engineSpeed += 40f;
				}
				else if (dynamicData.engineSpeed > targetEngineSpeed)
				{
					dynamicData.engineSpeed -= 40f;
				}
			}
			else if (eSpeedChangeDeltaTime > 1f)
			{
				dynamicData.engineSpeed = targetEngineSpeed;
				eSpeedChangeDeltaTime = 0f;
			}
		}

		private void AutoToManGear()
		{
			dynamicData.clutch = dynamicData.footBrake * 1.5f;
			if (dynamicData.footBrake < 0.5f)
			{
				dynamicData.footBrake = 0f;
			}
			Debug.Log("转换前挡位:" + dynamicData.gear);
			switch (dynamicData.gear)
			{
			case 104:
				dynamicData.gear = 0;
				break;
			case 102:
				dynamicData.gear = 6;
				break;
			case 101:
				dynamicData.gear = 0;
				break;
			case 103:
				dynamicData.gear = 1;
				break;
			case 100:
				if (CarCtl.instance.m_Rigidbody.transform.localEulerAngles.x > 300f && CarCtl.instance.m_Rigidbody.transform.localEulerAngles.x < 355f)
				{
					dynamicData.gear = 1;
					Debug.Log("自动转化为一档行驶");
				}
				else
				{
					dynamicData.gear = CalculateAutoGear(CarState.Instance.CarSignal.oil, CarState.Instance.CurEngineSpeed);
				}
				break;
			}
			Debug.Log("转换后挡位:" + dynamicData.gear);
		}

		private byte CalculateAutoGear(float oil, float engineSpeed)
		{
			int num = GearFromOil(oil);
			if (curgear < num && engineSpeed > 1500f)
			{
				if (changeCount < 0)
				{
					changeCount = 0;
				}
				changeCount++;
				if (changeCount > 10)
				{
					changeCount = 0;
					if (CarState.Instance.CurSpeed <= 90f && curgear < 3)
					{
						curgear++;
					}
					else if (CarState.Instance.CurSpeed > 90f && curgear < 4)
					{
						curgear++;
					}
				}
			}
			else if (curgear > num && engineSpeed < 1500f)
			{
				if (changeCount > 0)
				{
					changeCount = 0;
				}
				changeCount--;
				if (changeCount < -100)
				{
					changeCount = 0;
					if (curgear > 1)
					{
						curgear--;
					}
				}
			}
			return (byte)curgear;
		}

		private int GearFromOil(float oil)
		{
			int num = 1;
			if (oil < 0.1f)
			{
				return 1;
			}
			if (oil < 0.2f)
			{
				return 2;
			}
			if (oil < 0.3f)
			{
				return 3;
			}
			if (oil < 0.4f)
			{
				return 4;
			}
			return 5;
		}

		public void DynamicCaculateLocal()
		{
			float ttq = DynamicsEngineTorque(dynamicData.engineSpeed, dynamicData.carSpeed, dynamicData.oil, dynamicData.gear, dynamicData.clutch, dynamicData.engineIsFired, -1E-06f, 0.005729f, 129.973f, 5600f, 140f, 0.311651f, 3.83649f, dynamicData.curIg);
			if (dynamicData.gear == 0 || dynamicData.clutch > 0.8f)
			{
				dynamicData.pullForce = 0f;
			}
			else
			{
				dynamicData.pullForce = DynamicsTorqueAfterClutch(ttq, dynamicData.clutch) * 3.83649f * dynamicData.curIg * 0.92f / 0.311651f;
			}
			if (IsCurrMinEngineTorque(dynamicData.gear, dynamicData.carSpeed, dynamicData.oil, dynamicData.curIg, dynamicData.clutch) && 0f > dynamicData.pullForce)
			{
				dynamicData.pullForce = 0.3f * dynamicData.pullForce;
			}
			dynamicData.fWind = DynamicWindForce(dynamicData.carSpeed, 0.35f, 2.0134f);
			dynamicData.fRoll = DynamicsRollForce(dynamicData.carSpeed, 0f, 0.02f, 5.6E-05f, 0f, 1504f);
			if (dynamicData.gear == 0 || dynamicData.clutch > 0.8f)
			{
				dynamicData.fRoll *= 0.1f;
			}
			dynamicData.fGrade = 0f;
			dynamicData.fBrake = DynamicsBrake(dynamicData.footBrake, dynamicData.handBrake, dynamicData.carSpeed, dynamicData.pullForce, (dynamicData.fWind + dynamicData.fRoll + dynamicData.fGrade) * DataCenter.Instance.mAdhere, 15000f, 7500f);
			dynamicData.driveForce = dynamicData.pullForce + dynamicData.fWind + dynamicData.fBrake + dynamicData.fRoll + dynamicData.fGrade;
			dynamicData.fAdhere = DataCenter.Instance.mAdhere * 1504f * 9.8f;
			if (dynamicData.driveForce > dynamicData.fAdhere)
			{
				dynamicData.driveForce = dynamicData.fAdhere;
			}
			else if (dynamicData.driveForce < 0f - dynamicData.fAdhere)
			{
				dynamicData.driveForce = 0f - dynamicData.fAdhere;
			}
		}

		public bool JudgeFireOff()
		{
			bool result = false;
			if (SettingInfoCtl.Instance.isManualGear && dynamicData.engineIsFired && targetEngineSpeed < 300f && dynamicData.clutch < 0.25f && dynamicData.gear != 0)
			{
				result = true;
			}
			return result;
		}

		private float DynamicsRollForce(float CurV, float grade, float C1RollF1, float C1RollF2, float C1RollF3, float C1MM)
		{
			float num = Math.Abs(CurV);
			float num2 = (C1RollF1 + C1RollF2 * num + C1RollF3 * num * num) * C1MM * 9.8f * (float)Math.Cos(grade);
			if (CurV > 0f)
			{
				return 0f - num2;
			}
			if (CurV < 0f)
			{
				return num2;
			}
			return 0f;
		}

		private float DynamicWindForce(float CurV, float C1Cd, float C1AirA)
		{
			float num = C1Cd * C1AirA * CurV * CurV / 21.15f;
			if (CurV > 0f)
			{
				return 0f - num;
			}
			if (CurV < 0f)
			{
				return num;
			}
			return 0f;
		}

		private float DynamicsBrake(float footbrake, float handbrake, float CurV, float FDrive, float FEnvirment, float C1MaxFootBrake, float C1MaxHandbrake)
		{
			float num = footbrake * C1MaxFootBrake + handbrake * C1MaxHandbrake;
			if (CurV > 0f)
			{
				num = 0f - num;
			}
			else if (CurV < 0.05f)
			{
				if (FDrive + FEnvirment == 0f)
				{
					num = 0f;
				}
				else if (num > Math.Abs(FDrive + FEnvirment))
				{
					num = 0f - (FDrive + FEnvirment);
				}
				else if (FDrive + FEnvirment > 0f)
				{
					num = 0f - num;
				}
			}
			return num;
		}

		private float DynamicsEngineTorque(float CurNe, float CurV, float Oil, int Gear, float Clutch, bool bEngineFired, float C1AA, float C1BB, float C1CC, float C1Np, float C1TtqMax, float C1Rt, float C1I0, float CurIg)
		{
			if (!bEngineFired)
			{
				return 0f;
			}
			float num = C1AA * CurNe * CurNe + C1BB * CurNe + C1CC;
			if (Gear == 0)
			{
				return 0f;
			}
			float num2 = C1Np * Oil;
			float ttq = 0.377f * num2 * C1Rt / C1I0 / CurIg;
			ttq = DynamicsTorqueAfterClutch(ttq, Clutch);
			float num3 = Math.Abs(CurV);
			float num4 = Math.Abs(ttq);
			switch (Gear)
			{
			case 1:
				if (num3 < num4 * 0.8f || CurV < 0f)
				{
					return num;
				}
				if (num3 < num4 * 1.1f)
				{
					return Mathf.Lerp(num * 0.3f, (0f - num) * 0.3f, Mathf.InverseLerp(num4 * 0.9f, num4 * 1.1f, num3));
				}
				return (0f - C1TtqMax) * 0.1f;
			case 6:
				if (num3 < num4 * 0.8f || CurV > 0f)
				{
					return 0f - num;
				}
				if (num3 < num4 * 1.1f)
				{
					return Mathf.Lerp((0f - num) * 0.3f, num * 0.3f, Mathf.InverseLerp(num4 * 0.9f, num4 * 1.1f, num3));
				}
				return C1TtqMax * 0.1f;
			case 2:
				if (num3 < num4 * 0.95f)
				{
					return num;
				}
				if (num3 < num4 * 1.35f)
				{
					return Mathf.Lerp(num, 0f - num, Mathf.InverseLerp(num4 * 0.95f, num4 * 1.35f, num3));
				}
				return 0f - C1TtqMax;
			case 3:
				if (num3 < num4 * 0.95f)
				{
					return num;
				}
				if (num3 < num4 * 1.65f)
				{
					return Mathf.Lerp(num, 0f - num, Mathf.InverseLerp(num4 * 0.95f, num4 * 1.65f, num3));
				}
				return 0f - C1TtqMax;
			case 4:
				if (num3 < num4 * 0.95f)
				{
					return num;
				}
				if (num3 < num4 * 1.95f)
				{
					return Mathf.Lerp(num, 0f - num, Mathf.InverseLerp(num4 * 0.95f, num4 * 1.95f, num3));
				}
				return 0f - C1TtqMax;
			case 5:
				if (num3 < num4 * 0.95f)
				{
					return num;
				}
				if (num3 < num4 * 2.25f)
				{
					return Mathf.Lerp(num, 0f - num, Mathf.InverseLerp(num4 * 0.95f, num4 * 2.25f, num3));
				}
				return 0f - C1TtqMax;
			default:
				if (num3 < num4 * 0.95f)
				{
					return num;
				}
				if (num3 < num4 * 2.55f)
				{
					return Mathf.Lerp(num, 0f - num, Mathf.InverseLerp(num4 * 0.95f, num4 * 2.55f, num3));
				}
				return 0f - C1TtqMax;
			}
		}

		public float DynamicCaculate()
		{
			if (dynamicData.engineIsFired)
			{
				if (IsCurrMinEngineTorque(dynamicData.gear, dynamicData.carSpeed, dynamicData.oil, dynamicData.curIg, dynamicData.clutch) && 0f > dynamicData.pullForce)
				{
					dynamicData.pullForce = 0.3f * dynamicData.pullForce;
				}
				dynamicData.driveForce = dynamicData.pullForce + dynamicData.fResist + dynamicData.fBrake;
				dynamicData.fAdhere = DataCenter.Instance.mAdhere * 1504f * 9.8f;
				if (dynamicData.driveForce > dynamicData.fAdhere)
				{
					dynamicData.driveForce = dynamicData.fAdhere;
				}
				else if (dynamicData.driveForce < 0f - dynamicData.fAdhere)
				{
					dynamicData.driveForce = 0f - dynamicData.fAdhere;
				}
				if (dynamicData.gear == 2 && dynamicData.carSpeed < 10f)
				{
					dynamicData.driveForce /= 2f;
				}
				if (dynamicData.gear == 1 && dynamicData.carSpeed < 10f)
				{
					dynamicData.driveForce /= 2f;
				}
			}
			else
			{
				dynamicData.driveForce = 0f;
			}
			return dynamicData.driveForce;
		}

		private bool IsCurrMinEngineTorque(int Gear, float currSpeed, float currOil, float currIg, float Clutch)
		{
			float num = 5600f * currOil;
			float ttq = 0.377f * num * 0.311651f / 3.83649f / currIg;
			ttq = DynamicsTorqueAfterClutch(ttq, Clutch);
			if (Gear != 6 && Math.Abs(currSpeed) >= ttq)
			{
				return true;
			}
			return false;
		}

		private float DynamicsTorqueAfterClutch(float Ttq, float Clutch)
		{
			if (Clutch < 0.1f)
			{
				return Ttq;
			}
			if (Clutch > 0.9f)
			{
				return 0.01f;
			}
			return Mathf.Lerp(0.01f, Ttq, Mathf.InverseLerp(0.9f, 0.1f, Clutch));
		}
	}
}
