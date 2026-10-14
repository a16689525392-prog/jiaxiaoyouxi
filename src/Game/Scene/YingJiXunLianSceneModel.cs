using UnityEngine;
using View;

namespace Scene
{
	public class YingJiXunLianSceneModel : SceneModelBase
	{
		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			return GetYingJiXunLianStartPosition(itemType);
		}

		public Transform GetYingJiXunLianStartPosition(int itemType)
		{
			switch (itemType)
			{
			case 601:
				itemStartPosition = startPositionGroup.Find("AppearPerson_Pos");
				break;
			case 602:
				itemStartPosition = startPositionGroup.Find("BoomWheel_Pos");
				break;
			case 603:
				itemStartPosition = startPositionGroup.Find("DrunkDriving_Pos");
				break;
			case 611:
				itemStartPosition = startPositionGroup.Find("BrakeBad_Pos");
				break;
			}
			return itemStartPosition;
		}
	}
}
