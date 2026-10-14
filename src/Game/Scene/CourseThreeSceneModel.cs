using Global;
using UnityEngine;
using View;

namespace Scene
{
	public class CourseThreeSceneModel : SceneModelBase
	{
		public GameObject wayPointsGroup;

		public GameObject OutOfRangeArea;

		public GameObject TriggerArea;

		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			switch (trainType)
			{
			case TrainType.CourseThree:
				if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
				{
					itemStartPosition = startPositionGroup.transform.Find("InitPositionWay" + SceneJumpCtl.Instance.SceneWay);
				}
				else
				{
					GetCourseThreeStartPosition(itemType);
				}
				if (SceneJumpCtl.Instance.CoursethreeName != CoursethreeScenesName.NanChang && TriggerArea != null && ViewJumpCtl.Instance.modelType != 0 && ViewJumpCtl.Instance.modelType != 1 && ViewJumpCtl.Instance.modelType != 5)
				{
					TriggerArea.transform.GetChild(SceneJumpCtl.Instance.SceneWay - 1).gameObject.SetActive(value: true);
				}
				if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang && TriggerArea != null && ViewJumpCtl.Instance.modelType == 0)
				{
					TriggerArea.gameObject.SetActive(value: false);
				}
				if (ViewJumpCtl.Instance.itemType == 1300 && OutOfRangeArea != null && ViewJumpCtl.Instance.modelType != 5)
				{
					OutOfRangeArea.transform.GetChild(SceneJumpCtl.Instance.SceneWay - 1).gameObject.SetActive(value: true);
				}
				break;
			case TrainType.NightLight:
				if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.RenCai || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.ChengDong || SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.BaiYun)
				{
					itemStartPosition = startPositionGroup.transform.Find("InitPositionWay" + SceneJumpCtl.Instance.SceneWay);
				}
				else
				{
					GetCourseThreeStartPosition(200);
				}
				break;
			case TrainType.BasicTrain:
				itemStartPosition = startPositionGroup.Find("Position_BasicTrain");
				break;
			case TrainType.DriveSkill:
				GetCourseThreeStartPosition(201);
				break;
			}
			return itemStartPosition;
		}

		public void GetCourseThreeStartPosition(int itemType)
		{
			foreach (ItemPosition itemPosition in DataCenter.Instance.itemPositionList)
			{
				if (itemPosition.itemType == itemType)
				{
					itemStartPosition = startPositionGroup.transform.Find(itemPosition.initPositionName);
					if (itemStartPosition == null)
					{
						Debug.Log("未找到 " + itemPosition.initPositionName);
					}
					break;
				}
			}
		}
	}
}
