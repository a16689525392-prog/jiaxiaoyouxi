using Global;
using UnityEngine;

namespace Scene
{
	public class SnowCtl : MonoBehaviour
	{
		public GameObject particle0;

		public GameObject particle1;

		private void Start()
		{
			if (DataCenter.Instance.tripleState != 0)
			{
				particle0.SetActive(value: false);
				particle1.SetActive(value: false);
			}
			else
			{
				particle0.SetActive(value: true);
				particle1.SetActive(value: true);
			}
		}
	}
}
