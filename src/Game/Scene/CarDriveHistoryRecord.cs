using System.Collections.Generic;
using System.IO;
using Car;
using UnityEngine;

namespace Scene
{
	public class CarDriveHistoryRecord : MonoBehaviour
	{
		public static CarDriveHistoryRecord Instance;

		private List<DriveRecord> carSignalList = new List<DriveRecord>();

		private bool isRecord;

		private float length;

		private void Awake()
		{
			Instance = this;
		}

		private void FixedUpdate()
		{
			length += Time.deltaTime * CarState.Instance.CurSpeed;
			if (isRecord)
			{
				DriveRecord driveRecord = new DriveRecord();
				driveRecord.pos = CarState.Instance.CurCar.transform.position;
				driveRecord.localEuler = CarState.Instance.CurCar.transform.localEulerAngles;
				driveRecord.speed = CarState.Instance.CurSpeed;
				driveRecord.engineSpeed = CarState.Instance.CurEngineSpeed;
				driveRecord.carSignal = CarState.Instance.CarSignal;
				carSignalList.Add(driveRecord);
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space) && !isRecord)
			{
				isRecord = true;
				Debug.Log("开始保存");
			}
			if (Input.GetKeyDown(KeyCode.A) && isRecord)
			{
				Debug.Log("取消保存");
				isRecord = false;
				carSignalList.Clear();
			}
			if (Input.GetKeyDown(KeyCode.S) && isRecord)
			{
				isRecord = false;
				SaveInfo();
				Debug.Log("保存完成");
			}
		}

		private void SaveInfo()
		{
			FileStream fileStream = new FileInfo(Application.streamingAssetsPath + "/history1.bytes").Create();
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(carSignalList.Count);
			foreach (DriveRecord carSignal in carSignalList)
			{
				binaryWriter.Write(carSignal.speed);
				binaryWriter.Write(carSignal.engineSpeed);
				binaryWriter.Write(carSignal.pos.x);
				binaryWriter.Write(carSignal.pos.y);
				binaryWriter.Write(carSignal.pos.z);
				binaryWriter.Write(carSignal.localEuler.x);
				binaryWriter.Write(carSignal.localEuler.y);
				binaryWriter.Write(carSignal.localEuler.z);
				binaryWriter.Write(carSignal.carSignal.oil);
				binaryWriter.Write(carSignal.carSignal.footBrake);
				binaryWriter.Write(carSignal.carSignal.handBrake);
				binaryWriter.Write(carSignal.carSignal.cluth);
				binaryWriter.Write(carSignal.carSignal.steerWheel);
				binaryWriter.Write(carSignal.carSignal.gear);
				binaryWriter.Write(carSignal.carSignal.turnLight);
				binaryWriter.Write(carSignal.carSignal.horn);
				binaryWriter.Write(carSignal.carSignal.fire);
				binaryWriter.Write(carSignal.carSignal.power);
				binaryWriter.Write(carSignal.carSignal.safeBelt);
				binaryWriter.Write(carSignal.carSignal.door);
				binaryWriter.Write(carSignal.carSignal.rainBrush);
				binaryWriter.Write(carSignal.carSignal.frontLight);
				binaryWriter.Write(carSignal.carSignal.widthLight);
				binaryWriter.Write(carSignal.carSignal.fogLight);
				binaryWriter.Write(carSignal.carSignal.sprayWater);
			}
			binaryWriter.Flush();
			binaryWriter.Close();
			fileStream.Close();
		}
	}
}
