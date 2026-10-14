using UnityEngine;
using View;

namespace Scene
{
	public class DaChe_CourseTwoSceneModel : SceneModelBase
	{
		public override Transform GetStartPosition(TrainType trainType, int itemType)
		{
			return GetCourseTwoStartPosition(itemType);
		}

		public Transform GetCourseTwoStartPosition(int itemType)
		{
			switch (itemType)
			{
			case 1100:
				itemStartPosition = startPositionGroup.Find("Position_KaoShi");
				break;
			case 1:
				itemStartPosition = startPositionGroup.Find("Position_CeFangTingChe");
				break;
			case 2:
				itemStartPosition = startPositionGroup.Find("Position_PoDaoDingDian");
				break;
			case 3:
				itemStartPosition = startPositionGroup.Find("Position_QuXianDrive");
				break;
			case 4:
				itemStartPosition = startPositionGroup.Find("Position_ZhiJiao");
				break;
			case 5:
				itemStartPosition = startPositionGroup.Find("Position_ZhuangKao");
				break;
			case 6:
				itemStartPosition = startPositionGroup.Find("Position_LianXuZhangAi");
				break;
			case 7:
				itemStartPosition = startPositionGroup.Find("Position_DanBianQiao");
				break;
			case 8:
				itemStartPosition = startPositionGroup.Find("Position_QiFuLu");
				break;
			case 9:
				itemStartPosition = startPositionGroup.Find("Position_XianKuanMen");
				break;
			case 10:
				itemStartPosition = startPositionGroup.Find("Position_ZhaiLuDiaoTou");
				break;
			case 11:
				itemStartPosition = startPositionGroup.Find("Position_ShiHuaLuMian");
				break;
			case 12:
				itemStartPosition = startPositionGroup.Find("Position_YuTian");
				break;
			case 13:
				itemStartPosition = startPositionGroup.Find("Position_WuTian");
				break;
			case 14:
				itemStartPosition = startPositionGroup.Find("Position_SuiDao");
				break;
			case 15:
				itemStartPosition = startPositionGroup.Find("Position_LianXuJiWan");
				break;
			case 16:
				itemStartPosition = startPositionGroup.Find("Position_GaoSuGongLu");
				break;
			}
			return itemStartPosition;
		}
	}
}
