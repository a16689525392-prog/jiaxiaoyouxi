using System.Collections;
using System.Collections.Generic;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;

namespace Scene
{
	public class ColliderDetection : MonoBehaviour
	{
		public static ColliderDetection instance;

		public List<GameObject> wheelPointGroup;

		public List<Transform> wheelLeftFrontPoint;

		public List<Transform> wheelLeftRearPoint;

		public List<Transform> wheelRightFrontPoint;

		public List<Transform> wheelRightRearPoint;

		public List<Transform> carFrontPoint;

		public List<Transform> carRearPoint;

		public List<Transform> carMidPoint;

		private Dictionary<DetectionType, Coroutine> detectonDic = new Dictionary<DetectionType, Coroutine>();

		public UnityAction<Collider> TriggerAreaStayCallback;

		public UnityAction<Collider> TriggerAreaCallback;

		public UnityAction<Collision> BumpGuardBarCallback;

		public UnityAction<Collision> BumpGanZiCallback;

		public UnityAction<Collision> BumpAiCarCallback;

		public UnityAction<RaycastHit> PressEdgeLineCallback;

		public UnityAction<RaycastHit> CarOutsideCallback;

		public UnityAction OnShakeStatusChange;

		public UnityAction<int, int> roadIndexChangeCallBack;

		private RaycastHit hitInfo;

		private int preId;

		private int preFrameId;

		private float notShakeTime;

		private bool isShake1;

		private bool isDunk;

		private float stopShakeTime;

		private bool preShakeStatus;

		private bool isShake;

		private int curRoadIndex = -1;

		private int preRoadIndex = -1;

		private bool isFirstRoadIndexDetetion = true;

		private void Awake()
		{
			instance = this;
			StartDetectionJudge(DetectionType.CarOutside);
			StartDetectionJudge(DetectionType.PressEdgeLine);
			StartDetectionJudge(DetectionType.RoadIndex);
			StartDetectionJudge(DetectionType.Shake);
		}

		public void StartDetectionJudge(DetectionType type)
		{
			if (detectonDic.ContainsKey(type))
			{
				Debug.Log(string.Concat("对应类型[", type, "]的检测已开启,请勿重复开启"));
				return;
			}
			switch (type)
			{
			case DetectionType.CarOutside:
			{
				Coroutine value = StartCoroutine(DetectionCarOutside());
				detectonDic.Add(type, value);
				break;
			}
			case DetectionType.PressEdgeLine:
			{
				Coroutine value = StartCoroutine(DetectionPressEdgeLine());
				detectonDic.Add(type, value);
				break;
			}
			case DetectionType.RoadIndex:
			{
				Coroutine value = StartCoroutine(DetectionRoadIndex());
				detectonDic.Add(type, value);
				break;
			}
			case DetectionType.Shake:
			{
				Coroutine value = StartCoroutine(DetectionShake());
				detectonDic.Add(type, value);
				break;
			}
			}
		}

		public void Reset()
		{
			preId = 0;
			preFrameId = 0;
			isFirstRoadIndexDetetion = true;
			preRoadIndex = (curRoadIndex = -1);
		}

		public void StopDetectionJudge(DetectionType type)
		{
			if (detectonDic.ContainsKey(type) && detectonDic[type] != null)
			{
				StopCoroutine(detectonDic[type]);
			}
		}

		private IEnumerator DetectionPressEdgeLine()
		{
			while (true)
			{
				for (int i = 0; i < wheelPointGroup.Count; i += 3)
				{
					if (GetTriggerRaycastInfo(wheelPointGroup[i].transform.position, out hitInfo, 26) && preId != hitInfo.collider.GetInstanceID() && PressEdgeLineCallback != null)
					{
						PressEdgeLineCallback(hitInfo);
						preId = hitInfo.collider.GetInstanceID();
					}
					if (GetTriggerRaycastInfo(wheelPointGroup[i].transform.position, out hitInfo, 25) && preId != hitInfo.collider.GetInstanceID() && PressEdgeLineCallback != null)
					{
						PressEdgeLineCallback(hitInfo);
						preId = hitInfo.collider.GetInstanceID();
					}
					if (GetTriggerRaycastInfo(wheelPointGroup[i].transform.position, out hitInfo, 10) && preId != hitInfo.collider.GetInstanceID() && PressEdgeLineCallback != null)
					{
						PressEdgeLineCallback(hitInfo);
						preId = hitInfo.collider.GetInstanceID();
					}
					if (GetTriggerRaycastInfo(wheelPointGroup[i].transform.position, out hitInfo, 19) && preId != hitInfo.collider.GetInstanceID() && PressEdgeLineCallback != null)
					{
						PressEdgeLineCallback(hitInfo);
						preId = hitInfo.collider.GetInstanceID();
					}
				}
				yield return new WaitForSeconds(0.1f);
			}
		}

		public IEnumerator DetectionCarOutside()
		{
			while (true)
			{
				for (int i = 0; i < carFrontPoint.Count; i++)
				{
					if (GetTriggerRaycastInfo(carFrontPoint[i].transform.position, out hitInfo, 10) && preFrameId != hitInfo.collider.GetInstanceID() && CarOutsideCallback != null)
					{
						CarOutsideCallback(hitInfo);
						preFrameId = hitInfo.collider.GetInstanceID();
					}
					if (GetTriggerRaycastInfo(carFrontPoint[i].transform.position, out hitInfo, 19) && preFrameId != hitInfo.collider.GetInstanceID() && CarOutsideCallback != null)
					{
						CarOutsideCallback(hitInfo);
						preFrameId = hitInfo.collider.GetInstanceID();
					}
				}
				for (int j = 0; j < carRearPoint.Count; j++)
				{
					if (GetTriggerRaycastInfo(carRearPoint[j].transform.position, out hitInfo, 10) && preFrameId != hitInfo.collider.GetInstanceID() && CarOutsideCallback != null)
					{
						CarOutsideCallback(hitInfo);
						preFrameId = hitInfo.collider.GetInstanceID();
					}
					if (GetTriggerRaycastInfo(carRearPoint[j].transform.position, out hitInfo, 19) && preFrameId != hitInfo.collider.GetInstanceID() && CarOutsideCallback != null)
					{
						CarOutsideCallback(hitInfo);
						preFrameId = hitInfo.collider.GetInstanceID();
					}
				}
				for (int k = 0; k < wheelLeftFrontPoint.Count; k++)
				{
					if (GetTriggerRaycastInfo(wheelLeftFrontPoint[k].transform.position, out hitInfo, 10) && preFrameId != hitInfo.collider.GetInstanceID() && CarOutsideCallback != null)
					{
						CarOutsideCallback(hitInfo);
						preFrameId = hitInfo.collider.GetInstanceID();
					}
				}
				for (int l = 0; l < wheelLeftRearPoint.Count; l++)
				{
					if (GetTriggerRaycastInfo(wheelLeftRearPoint[l].transform.position, out hitInfo, 10) && preFrameId != hitInfo.collider.GetInstanceID() && CarOutsideCallback != null)
					{
						CarOutsideCallback(hitInfo);
						preFrameId = hitInfo.collider.GetInstanceID();
					}
				}
				for (int m = 0; m < wheelRightFrontPoint.Count; m++)
				{
					if (GetTriggerRaycastInfo(wheelRightFrontPoint[m].transform.position, out hitInfo, 10) && preFrameId != hitInfo.collider.GetInstanceID() && CarOutsideCallback != null)
					{
						CarOutsideCallback(hitInfo);
						preFrameId = hitInfo.collider.GetInstanceID();
					}
				}
				for (int n = 0; n < wheelRightRearPoint.Count; n++)
				{
					if (GetTriggerRaycastInfo(wheelRightRearPoint[n].transform.position, out hitInfo, 10) && preFrameId != hitInfo.collider.GetInstanceID() && CarOutsideCallback != null)
					{
						CarOutsideCallback(hitInfo);
						preFrameId = hitInfo.collider.GetInstanceID();
					}
				}
				yield return new WaitForSeconds(0.1f);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer == 8)
			{
				TriggerAreaCallback?.Invoke(other);
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.gameObject.layer == 8)
			{
				TriggerAreaStayCallback?.Invoke(other);
			}
		}

		private void Update()
		{
			if (isDunk)
			{
				isShake1 = true;
				SignalSourceCtl.Instance.SetCarShakeStatus(isShake1);
				OnShakeStatusChange?.Invoke();
			}
			else if (!isDunk && isShake1)
			{
				isShake1 = false;
				SignalSourceCtl.Instance.SetCarShakeStatus(isShake1);
				OnShakeStatusChange?.Invoke();
			}
			if (isShake1)
			{
				notShakeTime += Time.deltaTime;
				if (notShakeTime > 2f)
				{
					isDunk = false;
					notShakeTime = 0f;
				}
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.layer == 9)
			{
				BumpGuardBarCallback?.Invoke(collision);
				isDunk = true;
			}
			if (collision.gameObject.layer == 20)
			{
				BumpGanZiCallback?.Invoke(collision);
			}
			if (collision.collider.gameObject.layer != 12 && collision.collider.gameObject.layer != 13)
			{
				AudioSourceCtl.Instance.PlayAudio("CarHit");
			}
		}

		public bool GetShakeStatus()
		{
			return isShake;
		}

		public IEnumerator DetectionShake()
		{
			while (true)
			{
				if (GetTriggerRaycastInfo(carMidPoint[1].position, out hitInfo, 13) && CarState.Instance.CurSpeed > 0.1f)
				{
					isShake = true;
					stopShakeTime = 0f;
				}
				else if (CarState.Instance.FireOn && CarState.Instance.CarSignal.gear != 0 && CarState.Instance.CarSignal.cluth < 0.7f && CarState.Instance.CarSignal.cluth > 0.3f && (CarState.Instance.CarSignal.footBrake > 0.6f || CarState.Instance.CarSignal.handBrake > 0.6f))
				{
					isShake = true;
					stopShakeTime = 0f;
				}
				else
				{
					stopShakeTime += Time.deltaTime;
					if (stopShakeTime > 0.1f && isShake)
					{
						isShake = false;
						stopShakeTime = 0f;
					}
				}
				if (preShakeStatus != isShake)
				{
					SignalSourceCtl.Instance.SetCarShakeStatus(isShake);
					preShakeStatus = isShake;
					OnShakeStatusChange?.Invoke();
				}
				yield return 0;
			}
		}

		public int GetCurRoadIndex()
		{
			return curRoadIndex;
		}

		private IEnumerator DetectionRoadIndex()
		{
			while (true)
			{
				if (GetTriggerRaycastInfo(carMidPoint[1].position, out hitInfo, 11))
				{
					string text = hitInfo.collider.tag;
					if (!(text == "RoadIndex0"))
					{
						_ = text == "RoadIndex1";
					}
				}
				if (GetTriggerRaycastInfo(carFrontPoint[1].position, out hitInfo, 11))
				{
					switch (hitInfo.collider.tag)
					{
					case "RoadIndex0":
						curRoadIndex = 0;
						break;
					case "RoadIndex1":
						curRoadIndex = 1;
						break;
					case "RoadIndex2":
						curRoadIndex = 2;
						break;
					default:
						curRoadIndex = -1;
						break;
					}
				}
				else
				{
					curRoadIndex = -1;
				}
				if (isFirstRoadIndexDetetion)
				{
					isFirstRoadIndexDetetion = false;
				}
				else if (preRoadIndex != curRoadIndex && curRoadIndex != -1 && preRoadIndex != -1)
				{
					Debug.Log(preRoadIndex + " || " + curRoadIndex);
					roadIndexChangeCallBack(preRoadIndex, curRoadIndex);
				}
				preRoadIndex = curRoadIndex;
				yield return new WaitForSeconds(0.1f);
			}
		}

		public bool GetTriggerRaycastInfo(Vector3 oriPosition, out RaycastHit hitinfo, int layerIndex)
		{
			Vector3 direction = base.transform.TransformDirection(Vector3.down);
			if (Physics.Raycast(oriPosition, direction, out hitinfo, 10f, LayerMask.GetMask(LayerMask.LayerToName(layerIndex))))
			{
				return true;
			}
			return false;
		}

		public string GetTriggerRaycastInfoName(Vector3 oriPosition, out RaycastHit hitinfo, int layerIndex)
		{
			string result = "";
			Vector3 direction = base.transform.TransformDirection(Vector3.down);
			if (Physics.Raycast(oriPosition, direction, out hitinfo, 10f, LayerMask.GetMask(LayerMask.LayerToName(layerIndex))))
			{
				return hitinfo.collider.name;
			}
			return result;
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
			SignalSourceCtl.Instance.SetCarShakeStatus(isShake: false);
		}
	}
}
