using Scene;
using UnityEngine;

public class CullCamera : MonoBehaviour
{
	private void Start()
	{
		float[] array = new float[32];
		if (SceneJumpCtl.Instance.CurSceneType == SceneType.CourseTwo || SceneJumpCtl.Instance.CurSceneType == SceneType.XunLianScene)
		{
			array[24] = 90f;
		}
		else
		{
			array[24] = 150f;
		}
		Camera.main.layerCullDistances = array;
	}
}
