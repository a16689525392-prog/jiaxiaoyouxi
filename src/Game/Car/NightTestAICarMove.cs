using SoundAndTextShow;
using UnityEngine;

namespace Car
{
	public class NightTestAICarMove : MonoBehaviour
	{
		public GameObject[] wheelList;

		public float speed = 5f;

		public bool isHit;

		private void Update()
		{
			GameObject[] array = wheelList;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].transform.Rotate(new Vector3(speed * 10f, 0f));
			}
		}

		private void OnTriggerEnter(Collider other)
		{
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (!isHit)
			{
				SoundPlayCtl.Instance.PlaySound("撞车", 100f);
				AudioSourceCtl.Instance.PlayAudio("CarHit");
				CarState.Instance.FireOn = false;
				isHit = true;
				Object.Destroy(base.gameObject);
			}
		}
	}
}
