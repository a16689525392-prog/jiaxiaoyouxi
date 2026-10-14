using UnityEngine;
using View;

namespace Scene
{
	public class NightTestSceneModel : SceneModelBase
	{
		public GameObject wayPointsGroup;

		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			if (itemType == 701)
			{
				itemStartPosition = startPositionGroup.Find("NightTest_StartPosition");
			}
			return itemStartPosition;
		}
	}
}
