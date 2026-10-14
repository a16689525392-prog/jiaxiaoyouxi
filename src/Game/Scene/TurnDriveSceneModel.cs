using UnityEngine;
using View;

namespace Scene
{
	public class TurnDriveSceneModel : SceneModelBase
	{
		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			switch (itemType)
			{
			case 306:
				itemStartPosition = startPositionGroup.Find("DaS");
				break;
			case 304:
				itemStartPosition = startPositionGroup.Find("XiaoS");
				break;
			case 305:
				itemStartPosition = startPositionGroup.Find("ZhiJiao");
				break;
			}
			return itemStartPosition;
		}
	}
}
