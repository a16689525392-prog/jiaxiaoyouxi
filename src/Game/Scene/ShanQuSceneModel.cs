using UnityEngine;
using View;

namespace Scene
{
	public class ShanQuSceneModel : SceneModelBase
	{
		public GameObject wayPointsGroup;

		public GameObject OtherAiCarwayPointsGroup;

		public GameObject ShanQuRoadTest;

		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			switch (itemType)
			{
			case 402:
				itemStartPosition = startPositionGroup.Find("Position_ShanQu");
				break;
			case 604:
			case 607:
			case 51201:
				itemStartPosition = startPositionGroup.Find("Position_CommonStart");
				break;
			case 612:
				itemStartPosition = startPositionGroup.Find("Position_FrontShanTiHuaPo");
				break;
			case 605:
				itemStartPosition = startPositionGroup.Find("Position_StreeWheelBad");
				break;
			case 606:
				itemStartPosition = startPositionGroup.Find("Position_KengWaLuMian");
				break;
			case 609:
				itemStartPosition = startPositionGroup.Find("Position_StoneFall");
				break;
			case 510:
				itemStartPosition = startPositionGroup.Find("Position_GenChe");
				break;
			case 5100:
				itemStartPosition = startPositionGroup.Find("Position_HuiChe");
				break;
			case 509:
				itemStartPosition = startPositionGroup.Find("Position_PoStart");
				break;
			case 5090:
				itemStartPosition = startPositionGroup.Find("Position_WanStart");
				break;
			case 703:
				itemStartPosition = startPositionGroup.Find("Position_ShanQuRoadTest");
				ShanQuRoadTest.SetActive(value: true);
				break;
			}
			return itemStartPosition;
		}
	}
}
