using UnityEngine;

public class NewCameraMirror : MonoBehaviour
{
	private void Start()
	{
		Mesh sharedMesh = GetComponent<MeshFilter>().sharedMesh;
		Vector2[] array = new Vector2[sharedMesh.vertexCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].x = 1f - sharedMesh.uv[i].x;
			array[i].y = sharedMesh.uv[i].y;
		}
		sharedMesh.uv = array;
	}
}
