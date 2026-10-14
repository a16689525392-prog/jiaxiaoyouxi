using UnityEngine;

public class ViewTest : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			float axis = Input.GetAxis("Mouse X");
			if (axis != 0f)
			{
				Debug.Log(axis);
			}
		}
	}
}
