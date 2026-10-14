using UnityEngine;

namespace Scene
{
	public class DriveLineRendererCtl : MonoBehaviour
	{
		private LineRenderer lineRenderer;

		private float curIndex;

		private float oriIndex;

		private float offset;

		private void Start()
		{
			lineRenderer = base.transform.GetComponent<LineRenderer>();
			curIndex = lineRenderer.material.mainTextureScale.x;
			oriIndex = curIndex;
		}

		private void Update()
		{
			offset -= Time.deltaTime / 2f;
			if ((double)offset < 0.02)
			{
				offset = 1f;
			}
			lineRenderer.material.mainTextureOffset = new Vector2(offset, 0f);
		}
	}
}
