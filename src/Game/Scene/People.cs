using UnityEngine;
using View;

namespace Scene
{
	public class People : MonoBehaviour
	{
		private static People instance;

		public float MoveSpeed = 1.001f;

		public float moveTime;

		public static People Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void FixedUpdate()
		{
			moveTime += Time.deltaTime;
			if (ViewJumpCtl.Instance.itemType != 601 && ViewJumpCtl.Instance.itemType != 613 && ViewJumpCtl.Instance.itemType != 615 && ViewJumpCtl.Instance.itemType != 706)
			{
				return;
			}
			if (ViewJumpCtl.Instance.modelType == 2)
			{
				if (moveTime < 5f)
				{
					MoveSpeed = 1.5f;
					base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				}
				else if (moveTime > 5f && moveTime < 10f)
				{
					MoveSpeed = 1.6f;
					base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				}
				else if (moveTime > 10f && moveTime < 16f)
				{
					MoveSpeed = 1.8f;
					base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				}
				else if (moveTime > 16f && moveTime < 18f)
				{
					MoveSpeed = 2f;
					base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				}
				else if (moveTime > 18f)
				{
					MoveSpeed = 0f;
					base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				}
			}
			else
			{
				if (ViewJumpCtl.Instance.modelType != 0 && ViewJumpCtl.Instance.modelType != 5)
				{
					return;
				}
				if (moveTime < 1.5f)
				{
					if (ViewJumpCtl.Instance.itemType == 613)
					{
						MoveSpeed = 8f;
					}
					else
					{
						MoveSpeed = 1.85f;
					}
					base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				}
				else if (moveTime > 1.5f && moveTime < 8f)
				{
					MoveSpeed = 1.9f;
					base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				}
				else if (moveTime > 8f && moveTime < 15f)
				{
					MoveSpeed = 2f;
					base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				}
				else if (moveTime > 15f)
				{
					MoveSpeed = 0f;
					base.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
				}
			}
		}
	}
}
