using Scene;
using UnityEngine;

namespace View
{
	public class ActionAnimation : MonoBehaviour
	{
		[HideInInspector]
		public bool isPlayEnd;

		[HideInInspector]
		public int curAction = -1;

		protected float intervalTime;

		protected virtual void Update()
		{
			if (isPlayEnd && curAction != -1)
			{
				intervalTime += Time.deltaTime;
				if (intervalTime > 1.5f && !CarActionJudge.Instance.JudgeActionDone(curAction))
				{
					PlayAnimation(curAction);
					intervalTime = 0f;
				}
			}
		}

		public virtual void PlayAnimation(int action)
		{
			Debug.Log("调用了基础的空动画");
		}

		public virtual void StopAnimation()
		{
			StopAllCoroutines();
			base.gameObject.SetActive(value: false);
			isPlayEnd = true;
			intervalTime = 0f;
			curAction = -1;
		}
	}
}
