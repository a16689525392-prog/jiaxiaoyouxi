using UnityEngine;

namespace Scene
{
	public class People30 : MonoBehaviour
	{
		private static People30 instance;

		public float MoveSpeed;

		public float moveTime1;

		public static People30 Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void FixedUpdate()
		{
			moveTime1 += Time.deltaTime;
			if (!CityRoadSceneCtl.Instance.isRed2 && moveTime1 < 17f)
			{
				MoveSpeed = 1.5f;
				base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
			}
			else if (!CityRoadSceneCtl.Instance.isRed2 && moveTime1 > 17f)
			{
				MoveSpeed = 0f;
				base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
			}
			else if (CityRoadSceneCtl.Instance.isRed2)
			{
				CityRoadSceneCtl.Instance.isInstanceMan30 = false;
				Object.Destroy(base.gameObject);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "AI_Car")
			{
				CityRoadSceneCtl.Instance.isInstanceMan30 = false;
				Object.Destroy(base.gameObject);
			}
		}
	}
}
