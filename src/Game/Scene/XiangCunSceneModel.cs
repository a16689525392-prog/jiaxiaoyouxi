using UnityEngine;
using View;

namespace Scene
{
	public class XiangCunSceneModel : SceneModelBase
	{
		public GameObject wayPointsGroup;

		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			switch (itemType)
			{
			case 51200:
				itemStartPosition = startPositionGroup.Find("Position_RainStart");
				break;
			case 401:
				itemStartPosition = startPositionGroup.Find("Position_CommonStart");
				break;
			}
			return itemStartPosition;
		}
	}
}
