using UnityEngine;
using View;

namespace Scene
{
	public class ShenZhenSceneModel : SceneModelBase
	{
		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			if (itemType == 706)
			{
				itemStartPosition = startPositionGroup.Find("DefenseTest_StartPosition");
			}
			return itemStartPosition;
		}
	}
}
