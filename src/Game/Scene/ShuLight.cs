using UnityEngine;
using View;

namespace Scene
{
	public class ShuLight : MonoBehaviour
	{
		private void Start()
		{
			if (ViewJumpCtl.Instance.otherType != 505)
			{
				return;
			}
			foreach (Transform item in base.transform)
			{
				item.GetComponent<MeshRenderer>().material.color = Color.gray;
			}
		}
	}
}
