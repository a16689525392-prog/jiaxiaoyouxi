using UnityEngine;

namespace Scene
{
	public class People3 : MonoBehaviour
	{
		private static People3 instance;

		public float MoveSpeed;

		public float moveTime1;

		public static People3 Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void FixedUpdate()
		{
			moveTime1 += Time.deltaTime;
			if (moveTime1 < 50f)
			{
				MoveSpeed = 1.5f;
				base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				CityRoadSceneCtl.Instance.InstancePeople3.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
			}
			else
			{
				MoveSpeed = 1.5f;
				base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				CityRoadSceneCtl.Instance.InstancePeople3.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
			}
			if (moveTime1 > 100f)
			{
				moveTime1 = 0f;
			}
		}
	}
}
