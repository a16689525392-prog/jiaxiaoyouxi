using UnityEngine;
using View;

public class SoundCltGai : MonoBehaviour
{
	public static SoundCltGai Instance;

	public int INDEX;

	public float index;

	public bool chongzhi;

	public string CWTS;

	public Rigidbody car;

	public float s;

	public bool fire;

	public bool YaXian;

	public bool SceneMenuViewDestroy;

	public int linshibianliang;

	private void Awake()
	{
		Instance = this;
		if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo && ViewJumpCtl.Instance.modelType == 2)
		{
			_ = ViewJumpCtl.Instance.itemType;
		}
		car = GetComponent<Rigidbody>();
		INDEX = 0;
		index = -2f;
		s = 0f;
		fire = false;
		YaXian = false;
		chongzhi = true;
		CWTS = "";
		SceneMenuViewDestroy = false;
	}
}
