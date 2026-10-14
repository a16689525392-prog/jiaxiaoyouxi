using UnityEngine;
using View;

namespace Scene
{
	public class HighRoadSceneModel : SceneModelBase
	{
		public GameObject wayPointsGroup;

		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			switch (itemType)
			{
			case 404:
				itemStartPosition = startPositionGroup.Find("Position_CommonStart");
				break;
			case 608:
				itemStartPosition = startPositionGroup.Find("Position_FrontFlash");
				break;
			case 506:
				itemStartPosition = startPositionGroup.Find("Position_SuiDao");
				break;
			case 507:
				itemStartPosition = startPositionGroup.Find("Position_StopCar");
				break;
			case 704:
				itemStartPosition = startPositionGroup.Find("Position_HighRoadTest");
				break;
			case 705:
				itemStartPosition = startPositionGroup.Find("Position_SuiDaoTest");
				break;
			}
			return itemStartPosition;
		}
	}
}
