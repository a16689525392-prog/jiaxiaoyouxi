using UnityEngine;
using View;

namespace Scene
{
	public class HaiJingSceneModel : SceneModelBase
	{
		public GameObject wayPointsGroup;

		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			switch (itemType)
			{
			case 5050:
				itemStartPosition = startPositionGroup.Find("Night_StartPosition");
				break;
			case 400:
				itemStartPosition = startPositionGroup.Find("Common_StartPosition");
				break;
			}
			return itemStartPosition;
		}
	}
}
