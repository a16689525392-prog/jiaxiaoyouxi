using UnityEngine;

public class SetWayPoint : MonoBehaviour
{
	private void Awake()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (i == base.transform.childCount - 1)
			{
				base.transform.GetChild(i).GetComponent<WayPoint>().NextWayPoint.Add(base.transform.GetChild(0).GetComponent<WayPoint>());
			}
			else
			{
				base.transform.GetChild(i).GetComponent<WayPoint>().NextWayPoint.Add(base.transform.GetChild(i + 1).GetComponent<WayPoint>());
			}
		}
	}
}
