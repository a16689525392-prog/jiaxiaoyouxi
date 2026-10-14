using UnityEngine;
using View;

namespace Scene
{
	public class XueJingSceneModel : SceneModelBase
	{
		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			switch (itemType)
			{
			case 503:
				itemStartPosition = startPositionGroup.Find("NiNingLu");
				break;
			case 51202:
				itemStartPosition = startPositionGroup.Find("CommonStart");
				break;
			}
			return itemStartPosition;
		}
	}
}
