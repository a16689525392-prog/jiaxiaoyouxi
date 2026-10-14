using UnityEngine;

namespace Scene
{
	public class People20 : MonoBehaviour
	{
		private static People20 instance;

		public float MoveSpeed;

		public float moveTime1;

		public static People20 Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void FixedUpdate()
		{
			if (CityRoadSceneCtl.Instance.isGreen)
			{
				moveTime1 += Time.deltaTime;
				MoveSpeed = 1.5f;
				base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
			}
			else if (!CityRoadSceneCtl.Instance.isGreen)
			{
				CityRoadSceneCtl.Instance.isInstanceMan20 = false;
				Object.Destroy(base.gameObject);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "AI_Car")
			{
				CityRoadSceneCtl.Instance.isInstanceMan20 = false;
				Object.Destroy(base.gameObject);
			}
		}
	}
}
