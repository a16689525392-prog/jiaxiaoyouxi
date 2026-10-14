using UnityEngine;
using View;

namespace Scene
{
	public class SceneModelBase : MonoBehaviour
	{
		public static SceneModelBase Instance;

		public Transform startPositionGroup;

		protected Transform itemStartPosition;

		public virtual void Awake()
		{
			Instance = this;
			itemStartPosition = startPositionGroup;
		}

		public virtual Transform GetStartPosition(TrainType trainType, int itemType)
		{
			return itemStartPosition;
		}

		public Transform GetStartPosition()
		{
			return GetStartPosition(ViewJumpCtl.Instance.trainType, ViewJumpCtl.Instance.itemType);
		}
	}
}
