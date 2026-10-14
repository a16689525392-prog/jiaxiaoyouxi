using Scene;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Car
{
	public class AiCar : MonoBehaviour
	{
		private static AiCar instance;

		public GameObject[] wheelList;

		private WayPoint curWayPoint;

		private WayPoint nextWayPoint;

		private AiCarManager carManager;

		public float speed = 10f;

		public float speedGenChe = 4f;

		public float speedHuiChe = 6.5f;

		public float speedSuiDao = 15f;

		private float time;

		private float targetTime;

		private Quaternion oriQua;

		private Quaternion targetQua;

		public bool isHit;

		public static AiCar Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void FixedUpdate()
		{
			if (curWayPoint != null && !Physics.Raycast(base.transform.position, base.transform.forward, 5f, LayerMask.GetMask("Car", "AiCar")))
			{
				time += Time.deltaTime;
				base.transform.position = Vector3.Lerp(curWayPoint.transform.position, nextWayPoint.transform.position, time / targetTime);
				base.transform.rotation = Quaternion.Lerp(oriQua, targetQua, time / targetTime);
				if (time > targetTime)
				{
					curWayPoint = nextWayPoint;
					GetNextWayPoint();
					time = 0f;
				}
			}
			GameObject[] array = wheelList;
			foreach (GameObject gameObject in array)
			{
				if (ViewJumpCtl.Instance.itemType == 510 || ViewJumpCtl.Instance.itemType == 703)
				{
					gameObject.transform.Rotate(new Vector3(speedGenChe * 10f, 0f));
				}
				else if (ViewJumpCtl.Instance.itemType == 5100 || ViewJumpCtl.Instance.itemType == 404 || ViewJumpCtl.Instance.itemType == 5111)
				{
					gameObject.transform.Rotate(new Vector3(speedHuiChe * 10f, 0f));
				}
				else if (ViewJumpCtl.Instance.itemType == 506)
				{
					gameObject.transform.Rotate(new Vector3(speedSuiDao * 10f, 0f));
				}
				else
				{
					gameObject.transform.Rotate(new Vector3(speed * 10f, 0f));
				}
			}
		}

		public void SetWayPoint(WayPoint wayPoint, AiCarManager carManager)
		{
			curWayPoint = wayPoint;
			GetNextWayPoint();
			this.carManager = carManager;
		}

		private void GetNextWayPoint()
		{
			if (curWayPoint.NextWayPoint.Count != 0)
			{
				int index = 0;
				nextWayPoint = curWayPoint.NextWayPoint[index];
				oriQua = base.transform.rotation;
				targetQua = Quaternion.LookRotation(nextWayPoint.transform.position - curWayPoint.transform.position);
				if (ViewJumpCtl.Instance.itemType == 510 || ViewJumpCtl.Instance.itemType == 703)
				{
					targetTime = Vector3.Distance(nextWayPoint.transform.position, curWayPoint.transform.position) / speedGenChe;
				}
				else if (ViewJumpCtl.Instance.itemType == 5100 || ViewJumpCtl.Instance.itemType == 404 || ViewJumpCtl.Instance.itemType == 5111)
				{
					targetTime = Vector3.Distance(nextWayPoint.transform.position, curWayPoint.transform.position) / speedHuiChe;
				}
				else if (ViewJumpCtl.Instance.itemType == 506)
				{
					targetTime = Vector3.Distance(nextWayPoint.transform.position, curWayPoint.transform.position) / speedSuiDao;
				}
				else
				{
					targetTime = Vector3.Distance(nextWayPoint.transform.position, curWayPoint.transform.position) / speed;
				}
			}
			else
			{
				carManager.ResetCarPosition(base.gameObject);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "ZhangAi")
			{
				ShanLuGenChe.wanDaoOverStart = true;
				Debug.Log(ShanLuGenChe.wanDaoOverStart);
			}
			if (other.tag == "AiCarRemove")
			{
				carManager.ResetCarPosition(base.gameObject);
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			carManager.ResetCarPosition(base.gameObject);
			if (!isHit)
			{
				SoundPlayCtl.Instance.PlaySound("撞车", 100f);
				AudioSourceCtl.Instance.PlayAudio("CarHit");
				CarState.Instance.FireOn = false;
				isHit = true;
			}
		}
	}
}
