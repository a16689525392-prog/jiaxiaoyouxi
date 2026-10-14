using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
	public class LineRendererCtl : MonoBehaviour
	{
		private List<Vector3> posList = new List<Vector3>();

		private LineRenderer lineRenderer;

		public BiaoDingView biaoDingView;

		public GameObject point;

		private List<int> wheelDataList = new List<int>();

		private int curShowIndex;

		private float intervalValue;

		private Vector3 initPos;

		private Coroutine cor;

		private void Start()
		{
			if (ViewJumpCtl.Instance.itemType != 0)
			{
				base.gameObject.SetActive(value: false);
				return;
			}
			lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.positionCount = 0;
		}

		private void Update()
		{
			wheelDataList = biaoDingView.GetWheelDatas();
			if (wheelDataList.Count == 2 && intervalValue == 0f)
			{
				intervalValue = Mathf.Abs(wheelDataList[0] - wheelDataList[1]);
				if (wheelDataList[0] > wheelDataList[1])
				{
					initPos = new Vector3(-360f, -180f);
				}
				else
				{
					initPos = new Vector3(-360f, -280f);
				}
			}
			if (curShowIndex >= wheelDataList.Count || cor != null)
			{
				return;
			}
			if (curShowIndex == 0)
			{
				curShowIndex++;
				return;
			}
			float y = (float)(wheelDataList[curShowIndex - 1] - wheelDataList[0]) / intervalValue * 15f + initPos.y;
			Vector3 vector = new Vector3(-450 + 90 * curShowIndex, y);
			y = (float)(wheelDataList[curShowIndex] - wheelDataList[0]) / intervalValue * 15f + initPos.y;
			Vector3 targetPos = new Vector3(-450 + 90 * (curShowIndex + 1), y);
			if (curShowIndex == 1)
			{
				GameObject obj = Object.Instantiate(point, base.transform);
				obj.transform.localPosition = initPos;
				obj.GetComponent<LineRenderPoint>().SetValue(wheelDataList[0]);
			}
			curShowIndex++;
			lineRenderer.positionCount = curShowIndex;
			lineRenderer.SetPosition(curShowIndex - 2, vector);
			cor = StartCoroutine(MoveToNext(vector, targetPos));
		}

		private IEnumerator MoveToNext(Vector3 oriPos, Vector3 targetPos)
		{
			float timeCount = 0f;
			while (timeCount < 1f)
			{
				timeCount += Time.deltaTime;
				Vector3 position = Vector3.Lerp(oriPos, targetPos, timeCount);
				lineRenderer.SetPosition(curShowIndex - 1, position);
				yield return 0;
			}
			GameObject obj = Object.Instantiate(point, base.transform);
			obj.transform.localPosition = targetPos;
			obj.GetComponent<LineRenderPoint>().SetValue(wheelDataList[curShowIndex - 1]);
			yield return 0;
			cor = null;
		}
	}
}
