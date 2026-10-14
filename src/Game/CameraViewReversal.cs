using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraViewReversal : MonoBehaviour
{
	private Camera mCamera;

	private void Start()
	{
		mCamera = GetComponent<Camera>();
		mCamera.ResetWorldToCameraMatrix();
		mCamera.ResetProjectionMatrix();
		mCamera.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1f, 1f, 1f));
	}
}
