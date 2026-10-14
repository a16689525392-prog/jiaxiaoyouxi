using UnityEngine;
using View;

public class ElectricCarandMan : MonoBehaviour
{
	public static ElectricCarandMan intance;

	public GameObject luntai_1;

	public GameObject luntai_2;

	public float speed;

	private void Start()
	{
		intance = this;
		speed = 1f;
		if (ViewJumpCtl.Instance.itemType == 701 || ViewJumpCtl.Instance.itemType == 706)
		{
			speed = 3f;
		}
	}

	private void FixedUpdate()
	{
		luntai_1.transform.Rotate(new Vector3(-12f, 0f, 0f));
		luntai_2.transform.Rotate(new Vector3(-12f, 0f, 0f));
		base.transform.Translate(Vector3.down * 0.03f * speed, Space.Self);
	}
}
