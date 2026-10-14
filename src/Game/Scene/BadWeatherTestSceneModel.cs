using UnityEngine;
using View;

namespace Scene
{
	public class BadWeatherTestSceneModel : SceneModelBase
	{
		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			if (itemType == 702)
			{
				itemStartPosition = startPositionGroup.Find("BadWeatherTest_StartPosition");
			}
			return itemStartPosition;
		}
	}
}
