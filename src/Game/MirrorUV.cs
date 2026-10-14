using UnityEngine;

public class MirrorUV : MonoBehaviour
{
	private MeshFilter mMeshFilter;

	private Vector2[] mUVs;

	private void Start()
	{
		mMeshFilter = GetComponent<MeshFilter>();
		if (mMeshFilter == null)
		{
			Object.Destroy(this);
			return;
		}
		mUVs = new Vector2[mMeshFilter.mesh.vertexCount];
		float x = mMeshFilter.mesh.bounds.max.x;
		float y = mMeshFilter.mesh.bounds.max.y;
		float x2 = mMeshFilter.mesh.bounds.size.x;
		float y2 = mMeshFilter.mesh.bounds.size.y;
		for (int i = 0; i < mMeshFilter.mesh.vertexCount; i++)
		{
			if (x2 > y2)
			{
				mUVs[i].x = (x - mMeshFilter.mesh.vertices[i].x) / x2;
				mUVs[i].y = 1f - (y - mMeshFilter.mesh.vertices[i].y + (x2 - y2) / 2f) / x2;
			}
			else
			{
				mUVs[i].x = (x - mMeshFilter.mesh.vertices[i].x + (y2 - x2) / 2f) / y2;
				mUVs[i].y = 1f - (y - mMeshFilter.mesh.vertices[i].y) / y2;
			}
		}
		mMeshFilter.mesh.uv = mUVs;
		Object.Destroy(this);
	}
}
