using UnityEngine;
using View;

namespace Scene
{
	public class CityRoadSceneModel : SceneModelBase
	{
		public GameObject wayPointsGroup;

		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			switch (itemType)
			{
			case 403:
				itemStartPosition = startPositionGroup.Find("Position_CommonStart");
				break;
			case 511:
				itemStartPosition = startPositionGroup.Find("Position_RedGreenLight");
				break;
			}
			return itemStartPosition;
		}
	}
}
