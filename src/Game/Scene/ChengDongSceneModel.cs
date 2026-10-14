using UnityEngine;
using View;

namespace Scene
{
	public class ChengDongSceneModel : SceneModelBase
	{
		public GameObject dabache;

		public GameObject ElectricCar;

		public static ChengDongSceneModel instance;

		private void Start()
		{
			instance = this;
		}

		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			return GetChengDongScenePosition(itemType);
		}

		public Transform GetChengDongScenePosition(int itemType)
		{
			switch (itemType)
			{
			case 610:
				itemStartPosition = startPositionGroup.Find("NoOil_Pos");
				break;
			case 613:
				itemStartPosition = startPositionGroup.Find("BusBlindArea_Pos");
				dabache.SetActive(value: true);
				break;
			case 614:
				itemStartPosition = startPositionGroup.Find("RobRoad_Pos");
				ElectricCar.SetActive(value: true);
				break;
			case 615:
				itemStartPosition = startPositionGroup.Find("AllYJXunLian_Pos");
				break;
			}
			return itemStartPosition;
		}
	}
}
