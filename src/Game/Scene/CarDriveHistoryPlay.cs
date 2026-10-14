using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Car;
using Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using View;

namespace Scene
{
	public class CarDriveHistoryPlay : MonoBehaviour
	{
		public static CarDriveHistoryPlay Instance;

		public int playHistoryState;

		public float curCalculateSpeed;

		public float curCalculateEngineSpeed;

		public bool isAuto;

		public int curPlayIndex;

		private float value;

		private float cluthValue;

		private int curShowPathGear;

		private int curShowPathIndex;

		private List<Vector3> posList = new List<Vector3>();

		private LineRenderer lineRender;

		private float indexCount;

		public bool isLoadHistoryEnd;

		public List<DriveRecord> carDriveRecordList = new List<DriveRecord>();

		public int CurPlayIndex => curPlayIndex;

		private void Awake()
		{
			Instance = this;
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetAction = (UnityAction)Delegate.Combine(baseInstance.ResetAction, new UnityAction(Reset));
		}

		private void FixedUpdate()
		{
			if (isLoadHistoryEnd && curPlayIndex < carDriveRecordList.Count - 1)
			{
				CalculateSpeed();
				if (ViewJumpCtl.Instance.modelType == 1)
				{
					curCalculateEngineSpeed = CarCtl.Instance.GetCurEngineSpeed(CarState.Instance.CarSignal.oil);
				}
				else if (ViewJumpCtl.Instance.modelType == 0)
				{
					curCalculateEngineSpeed = carDriveRecordList[CurPlayIndex].engineSpeed;
				}
				SetCarPosition();
				ShowCarDrivePath();
			}
			else if (curPlayIndex >= carDriveRecordList.Count - 1 && CarState.Instance.FireOn)
			{
				CarState.Instance.StopCar();
				curCalculateSpeed = 0f;
				curCalculateEngineSpeed = 0f;
			}
			else if (GuideTipsViewCtl.Instance != null && ViewJumpCtl.Instance.modelType == 1)
			{
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang)
				{
					CourseTwoDriveGuide.Instance.JudgeGuide(curPlayIndex);
				}
				else if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
				{
					CourseThreeDriveGuide.Instance.JudgeGuide(curPlayIndex);
				}
			}
			if (Input.GetKeyDown(KeyCode.I))
			{
				Debug.Log(curPlayIndex);
			}
		}

		private void ShowCarDrivePath()
		{
			if ((ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang) && ViewJumpCtl.Instance.modelType == 1)
			{
				if (lineRender == null)
				{
					lineRender = CreateLineRenderer();
				}
				if (curPlayIndex >= curShowPathIndex)
				{
					ShowDirvePath();
				}
			}
			else if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
			{
				if (lineRender == null)
				{
					lineRender = CreateLineRenderer();
				}
				ShowCourseThreePath();
			}
		}

		private void SetCarPosition()
		{
			if (playHistoryState == 0)
			{
				curPlayIndex = GetCurPlayIndex();
				if (curPlayIndex >= carDriveRecordList.Count)
				{
					curPlayIndex = carDriveRecordList.Count - 1;
				}
				if (isAuto)
				{
					CarState.Instance.CurCar.transform.localPosition = carDriveRecordList[curPlayIndex].pos;
				}
				else
				{
					CarState.Instance.CurCar.transform.position = carDriveRecordList[curPlayIndex].pos;
				}
				CarState.Instance.CurCar.transform.localEulerAngles = carDriveRecordList[curPlayIndex].localEuler;
				if (ViewJumpCtl.Instance.modelType == 1)
				{
					if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang)
					{
						CourseTwoDriveGuide.Instance.JudgeGuide(curPlayIndex);
					}
					else if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
					{
						CourseThreeDriveGuide.Instance.JudgeGuide(curPlayIndex);
					}
				}
			}
			else
			{
				if (isAuto)
				{
					CarState.Instance.CurCar.transform.localPosition = carDriveRecordList[curPlayIndex].pos;
				}
				else
				{
					CarState.Instance.CurCar.transform.position = carDriveRecordList[curPlayIndex].pos;
				}
				CarState.Instance.CurCar.transform.localEulerAngles = carDriveRecordList[curPlayIndex].localEuler;
			}
		}

		private void CalculateSpeed()
		{
			if (ViewJumpCtl.Instance.modelType != 1)
			{
				return;
			}
			if (CarState.Instance.FireOn && CarState.Instance.CarSignal.gear != 0 && CarState.Instance.CarSignal.gear != 104)
			{
				if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
				{
					cluthValue = CarState.Instance.CarSignal.cluth * 0.1f;
				}
				else
				{
					cluthValue = CarState.Instance.CarSignal.cluth;
				}
				if (curCalculateSpeed < 6f)
				{
					if (SettingInfoCtl.Instance.isManualGear)
					{
						curCalculateSpeed = Mathf.Lerp(curCalculateSpeed, CarCtl.Instance.GetTargetSpeed(CarState.Instance.CarSignal.oil, cluthValue, CarState.Instance.CarSignal.gear), 0.01f);
						curCalculateSpeed *= 1f - Mathf.Clamp(CarState.Instance.CarSignal.footBrake + CarState.Instance.CarSignal.handBrake - 0.1f, 0f, 0.5f) * 2f;
					}
					else
					{
						curCalculateSpeed = Mathf.Lerp(curCalculateSpeed, CarCtl.Instance.GetTargetSpeed(CarState.Instance.CarSignal.oil, cluthValue, CarState.Instance.CarSignal.gear), 0.01f);
						curCalculateSpeed *= 1f - CarState.Instance.CarSignal.footBrake - Mathf.Clamp(CarState.Instance.CarSignal.handBrake - 0.1f, 0f, 0.5f) * 2f;
					}
				}
				else if (SettingInfoCtl.Instance.isManualGear)
				{
					curCalculateSpeed = Mathf.Lerp(curCalculateSpeed, CarCtl.Instance.GetTargetSpeed(CarState.Instance.CarSignal.oil, cluthValue, CarState.Instance.CarSignal.gear), 0.002f);
					curCalculateSpeed *= 1f - Mathf.Clamp(CarState.Instance.CarSignal.footBrake + CarState.Instance.CarSignal.handBrake - 0.1f, 0f, 0.5f) * 2f;
				}
				else
				{
					curCalculateSpeed = Mathf.Lerp(curCalculateSpeed, CarCtl.Instance.GetTargetSpeed(CarState.Instance.CarSignal.oil, cluthValue, CarState.Instance.CarSignal.gear), 0.002f);
					curCalculateSpeed *= 1f - CarState.Instance.CarSignal.footBrake - Mathf.Clamp(CarState.Instance.CarSignal.handBrake - 0.1f, 0f, 0.5f) * 2f;
				}
			}
			else
			{
				curCalculateSpeed *= 1f - Mathf.Clamp(CarState.Instance.CarSignal.footBrake + CarState.Instance.CarSignal.handBrake - 0.1f, 0f, 0.5f) * 2f;
			}
		}

		private void ShowDirvePath()
		{
			if (curPlayIndex >= carDriveRecordList.Count)
			{
				if (lineRender != null)
				{
					lineRender.positionCount = 0;
				}
				return;
			}
			while (curShowPathIndex < carDriveRecordList.Count && (carDriveRecordList[curShowPathIndex].carSignal.gear == curShowPathGear || carDriveRecordList[curShowPathIndex].carSignal.gear == 0))
			{
				curShowPathIndex++;
			}
			if (curShowPathIndex < carDriveRecordList.Count)
			{
				curShowPathGear = carDriveRecordList[curShowPathIndex].carSignal.gear;
			}
			posList.Clear();
			posList.Add(carDriveRecordList[curPlayIndex].pos);
			while (curShowPathIndex < carDriveRecordList.Count && carDriveRecordList[curShowPathIndex].carSignal.gear == curShowPathGear)
			{
				curShowPathIndex++;
				if (curShowPathIndex % 50 == 0 && Vector3.Distance(carDriveRecordList[curShowPathIndex].pos, posList[posList.Count - 1]) > 0.2f)
				{
					posList.Add(carDriveRecordList[curShowPathIndex].pos);
				}
			}
			SetLineRenderPositions(posList);
		}

		private void ShowCourseThreePath()
		{
			if (curPlayIndex <= curShowPathIndex)
			{
				return;
			}
			posList.Add(carDriveRecordList[curShowPathIndex].pos);
			while (curShowPathIndex < carDriveRecordList.Count)
			{
				curShowPathIndex++;
				if (curShowPathIndex % 10 == 0 && Vector3.Distance(carDriveRecordList[curShowPathIndex].pos, posList[posList.Count - 1]) > 0.3f)
				{
					posList.Add(carDriveRecordList[curShowPathIndex].pos);
				}
			}
			SetLineRenderPositions(posList);
		}

		private void SetLineRenderPositions(List<Vector3> positions)
		{
			lineRender.positionCount = positions.Count;
			for (int i = 0; i < positions.Count; i++)
			{
				if (isAuto)
				{
					switch (ViewJumpCtl.Instance.itemType)
					{
					case 0:
						positions[i] = GameObject.Find("DaoKuGuideModeArea").transform.TransformPoint(positions[i]) + new Vector3(0f, 0.2f, 0f);
						break;
					case 1:
						positions[i] = GameObject.Find("CeFangGuideModeArea").transform.TransformPoint(positions[i]) + new Vector3(0f, 0.2f, 0f);
						break;
					case 2:
						positions[i] = GameObject.Find("PoDaoGuideModeArea").transform.TransformPoint(positions[i]) + new Vector3(0f, 0.2f, 0f);
						break;
					case 3:
						positions[i] = GameObject.Find("QuXianGuideModeArea").transform.TransformPoint(positions[i]) + new Vector3(0f, 0.2f, 0f);
						break;
					case 4:
						positions[i] = GameObject.Find("ZhiJiaoGuideModeArea").transform.TransformPoint(positions[i]) + new Vector3(0f, 0.2f, 0f);
						break;
					}
				}
				else
				{
					positions[i] += new Vector3(0f, 0.2f, 0f);
				}
			}
			lineRender.SetPositions(positions.ToArray());
			lineRender.material.mainTextureScale = new Vector2(positions.Count / 3, 1f);
		}

		private LineRenderer CreateLineRenderer()
		{
			GameObject obj = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("DriveLineRenderer"));
			obj.transform.SetParent(TrainSceneBase.BaseInstance.transform);
			return obj.GetComponent<LineRenderer>();
		}

		public bool IsPlayEnd()
		{
			if (curPlayIndex < carDriveRecordList.Count)
			{
				return false;
			}
			return true;
		}

		private int GetCurPlayIndex()
		{
			if (carDriveRecordList[curPlayIndex].speed < 0.1f)
			{
				indexCount += 5f;
				return (int)indexCount;
			}
			if (carDriveRecordList[curPlayIndex].speed < 2f)
			{
				if (curCalculateSpeed < 2f && curCalculateSpeed > 0.5f)
				{
					indexCount += 1f;
				}
				else if (curCalculateSpeed >= 2f)
				{
					indexCount += 2f;
				}
				else if (!(curCalculateSpeed <= 0.5f))
				{
				}
			}
			else if (carDriveRecordList[curPlayIndex].speed >= 2f && !(curCalculateSpeed < 0.1f))
			{
				value = curCalculateSpeed / carDriveRecordList[curPlayIndex].speed;
				indexCount += Mathf.Clamp(value, 0.5f, 2.5f);
			}
			return (int)indexCount;
		}

		public DriveRecord GetCurRecordState()
		{
			return carDriveRecordList[curPlayIndex];
		}

		public bool IsPlaying()
		{
			if (isLoadHistoryEnd)
			{
				return playHistoryState == 0;
			}
			return false;
		}

		public void Play()
		{
			playHistoryState = 0;
		}

		public void Pause()
		{
			playHistoryState = 1;
		}

		public IEnumerator LoadHistoryDriveFiles(string path)
		{
			carDriveRecordList.Clear();
			string text = "";
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
			{
				switch (SceneJumpCtl.Instance.CoursetwoName)
				{
				case CoursetwoScenesName.YiAnDa:
					text = "/HistoryData/CourseTwo/YiAnDa/";
					break;
				case CoursetwoScenesName.QingHu:
					text = "/HistoryData/CourseTwo/QingHu/";
					break;
				case CoursetwoScenesName.WenZhou:
					text = "/HistoryData/CourseTwo/WenZhou/";
					break;
				case CoursetwoScenesName.HeBei:
					text = "/HistoryData/CourseTwo/HeBei/";
					break;
				case CoursetwoScenesName.HuNanYZ:
					text = "/HistoryData/CourseTwo/HuNanYZ/";
					break;
				case CoursetwoScenesName.ShiJiaZhuang:
					text = "/HistoryData/CourseTwo/ShiJiaZhuang/";
					break;
				case CoursetwoScenesName.FoShan:
					text = "/HistoryData/CourseTwo/FoShan/";
					break;
				case CoursetwoScenesName.NanFeng:
					text = "/HistoryData/CourseTwo/NanFeng/";
					break;
				}
			}
			else if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
			{
				switch (SceneJumpCtl.Instance.CoursethreeName)
				{
				case CoursethreeScenesName.NanChang:
					text = "/HistoryData/CourseThree/NanChang/";
					break;
				case CoursethreeScenesName.HuNanYZ:
					text = "/HistoryData/CourseThree/HuNanYZ/";
					break;
				case CoursethreeScenesName.RenCai:
					text = "/HistoryData/CourseThree/RenCai/";
					break;
				case CoursethreeScenesName.ChengDong:
					text = "/HistoryData/CourseThree/ChengDong/";
					break;
				case CoursethreeScenesName.BaiYun:
					text = "/HistoryData/CourseThree/BaiYun/";
					break;
				}
			}
			UnityWebRequest www = UnityWebRequest.Get(DataCenter.Instance.GetWWWPath(Application.streamingAssetsPath + text + path));
			yield return www.SendWebRequest();
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(www.downloadHandler.data));
			int num = binaryReader.ReadInt32();
			int num2;
			if (num == -1)
			{
				isAuto = true;
				num2 = binaryReader.ReadInt32();
				switch (ViewJumpCtl.Instance.itemType)
				{
				case 0:
					if ((bool)GameObject.Find("DaoKuGuideModeArea"))
					{
						CarState.Instance.CurCar.transform.parent = GameObject.Find("DaoKuGuideModeArea").transform;
					}
					break;
				case 1:
					if ((bool)GameObject.Find("CeFangGuideModeArea"))
					{
						CarState.Instance.CurCar.transform.parent = GameObject.Find("CeFangGuideModeArea").transform;
					}
					break;
				case 2:
					if ((bool)GameObject.Find("PoDaoGuideModeArea"))
					{
						CarState.Instance.CurCar.transform.parent = GameObject.Find("PoDaoGuideModeArea").transform;
					}
					break;
				case 3:
					if ((bool)GameObject.Find("QuXianGuideModeArea"))
					{
						CarState.Instance.CurCar.transform.parent = GameObject.Find("QuXianGuideModeArea").transform;
					}
					break;
				case 4:
					if ((bool)GameObject.Find("ZhiJiaoGuideModeArea"))
					{
						CarState.Instance.CurCar.transform.parent = GameObject.Find("ZhiJiaoGuideModeArea").transform;
					}
					break;
				}
			}
			else
			{
				isAuto = false;
				num2 = num;
			}
			for (int i = 0; i < num2; i++)
			{
				DriveRecord driveRecord = new DriveRecord();
				driveRecord.speed = binaryReader.ReadSingle();
				driveRecord.engineSpeed = binaryReader.ReadSingle();
				driveRecord.pos.x = binaryReader.ReadSingle();
				driveRecord.pos.y = binaryReader.ReadSingle();
				driveRecord.pos.z = binaryReader.ReadSingle();
				driveRecord.localEuler.x = binaryReader.ReadSingle();
				driveRecord.localEuler.y = binaryReader.ReadSingle();
				driveRecord.localEuler.z = binaryReader.ReadSingle();
				driveRecord.carSignal.oil = binaryReader.ReadSingle();
				driveRecord.carSignal.footBrake = binaryReader.ReadSingle();
				driveRecord.carSignal.handBrake = binaryReader.ReadSingle();
				driveRecord.carSignal.cluth = binaryReader.ReadSingle();
				driveRecord.carSignal.steerWheel = binaryReader.ReadSingle();
				driveRecord.carSignal.gear = binaryReader.ReadInt32();
				driveRecord.carSignal.turnLight = binaryReader.ReadInt32();
				driveRecord.carSignal.horn = binaryReader.ReadInt32();
				driveRecord.carSignal.fire = binaryReader.ReadInt32();
				driveRecord.carSignal.power = binaryReader.ReadInt32();
				driveRecord.carSignal.safeBelt = binaryReader.ReadInt32();
				driveRecord.carSignal.door = binaryReader.ReadInt32();
				driveRecord.carSignal.rainBrush = binaryReader.ReadInt32();
				driveRecord.carSignal.frontLight = binaryReader.ReadInt32();
				driveRecord.carSignal.widthLight = binaryReader.ReadInt32();
				driveRecord.carSignal.fogLight = binaryReader.ReadInt32();
				driveRecord.carSignal.sprayWater = binaryReader.ReadInt32();
				carDriveRecordList.Add(driveRecord);
			}
			binaryReader.Close();
			isLoadHistoryEnd = true;
			yield return 0;
		}

		private void Reset()
		{
			playHistoryState = 0;
			curPlayIndex = 0;
			indexCount = 0f;
			curCalculateSpeed = 0f;
			curCalculateEngineSpeed = 0f;
			if (carDriveRecordList != null && carDriveRecordList.Count > 0)
			{
				CarState.Instance.CurCar.transform.position = carDriveRecordList[0].pos;
				CarState.Instance.CurCar.transform.localEulerAngles = carDriveRecordList[0].localEuler;
			}
			if (lineRender != null)
			{
				lineRender.positionCount = 0;
				curShowPathIndex = 0;
				curShowPathGear = 0;
				posList.Clear();
			}
		}

		private void OnDestroy()
		{
			playHistoryState = 3;
		}
	}
}
