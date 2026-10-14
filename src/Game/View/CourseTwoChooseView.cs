using Global;
using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class CourseTwoChooseView : ViewBase
	{
		public static int curClickBtnIndex;

		public Image DaoKu;

		public Image CeFang;

		public Image QuXian;

		public Image ZhiJiao;

		public Image PoDao;

		public Image CoursetwoKaoShi;

		public override void Start()
		{
			base.Start();
			switch (SceneJumpCtl.Instance.CoursetwoName)
			{
			case CoursetwoScenesName.YiAnDa:
				DaoKu.sprite = Resources.Load("YiAnDaCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				CeFang.sprite = Resources.Load("YiAnDaCourseTwoImage/cefangtingche", typeof(Sprite)) as Sprite;
				QuXian.sprite = Resources.Load("YiAnDaCourseTwoImage/quxianxingshi", typeof(Sprite)) as Sprite;
				ZhiJiao.sprite = Resources.Load("YiAnDaCourseTwoImage/zhijiaozhuanwan", typeof(Sprite)) as Sprite;
				PoDao.sprite = Resources.Load("YiAnDaCourseTwoImage/podaodingdian", typeof(Sprite)) as Sprite;
				CoursetwoKaoShi.sprite = Resources.Load("YiAnDaCourseTwoImage/CoursetwoKaoShi", typeof(Sprite)) as Sprite;
				break;
			case CoursetwoScenesName.QingHu:
				DaoKu.sprite = Resources.Load("QingHuCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				CeFang.sprite = Resources.Load("QingHuCourseTwoImage/cefangtingche", typeof(Sprite)) as Sprite;
				QuXian.sprite = Resources.Load("QingHuCourseTwoImage/quxianxingshi", typeof(Sprite)) as Sprite;
				ZhiJiao.sprite = Resources.Load("QingHuCourseTwoImage/zhijiaozhuanwan", typeof(Sprite)) as Sprite;
				PoDao.sprite = Resources.Load("QingHuCourseTwoImage/podaodingdian", typeof(Sprite)) as Sprite;
				CoursetwoKaoShi.sprite = Resources.Load("QingHuCourseTwoImage/CoursetwoKaoShi", typeof(Sprite)) as Sprite;
				break;
			case CoursetwoScenesName.WenZhou:
				DaoKu.sprite = Resources.Load("WenZhouCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				CeFang.sprite = Resources.Load("WenZhouCourseTwoImage/cefangtingche", typeof(Sprite)) as Sprite;
				QuXian.sprite = Resources.Load("WenZhouCourseTwoImage/quxianxingshi", typeof(Sprite)) as Sprite;
				ZhiJiao.sprite = Resources.Load("WenZhouCourseTwoImage/zhijiaozhuanwan", typeof(Sprite)) as Sprite;
				PoDao.sprite = Resources.Load("WenZhouCourseTwoImage/podaodingdian", typeof(Sprite)) as Sprite;
				CoursetwoKaoShi.sprite = Resources.Load("WenZhouCourseTwoImage/CoursetwoKaoShi", typeof(Sprite)) as Sprite;
				break;
			case CoursetwoScenesName.HeBei:
				DaoKu.sprite = Resources.Load("HeBeiCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				CeFang.sprite = Resources.Load("HeBeiCourseTwoImage/cefangtingche", typeof(Sprite)) as Sprite;
				QuXian.sprite = Resources.Load("HeBeiCourseTwoImage/quxianxingshi", typeof(Sprite)) as Sprite;
				ZhiJiao.sprite = Resources.Load("HeBeiCourseTwoImage/zhijiaozhuanwan", typeof(Sprite)) as Sprite;
				PoDao.sprite = Resources.Load("HeBeiCourseTwoImage/podaodingdian", typeof(Sprite)) as Sprite;
				CoursetwoKaoShi.sprite = Resources.Load("HeBeiCourseTwoImage/CoursetwoKaoShi", typeof(Sprite)) as Sprite;
				break;
			case CoursetwoScenesName.HuNanYZ:
				DaoKu.sprite = Resources.Load("HuNanYZCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				CeFang.sprite = Resources.Load("HuNanYZCourseTwoImage/cefangtingche", typeof(Sprite)) as Sprite;
				QuXian.sprite = Resources.Load("HuNanYZCourseTwoImage/quxianxingshi", typeof(Sprite)) as Sprite;
				ZhiJiao.sprite = Resources.Load("HuNanYZCourseTwoImage/zhijiaozhuanwan", typeof(Sprite)) as Sprite;
				PoDao.sprite = Resources.Load("HuNanYZCourseTwoImage/podaodingdian", typeof(Sprite)) as Sprite;
				CoursetwoKaoShi.sprite = Resources.Load("HuNanYZCourseTwoImage/CoursetwoKaoShi", typeof(Sprite)) as Sprite;
				break;
			case CoursetwoScenesName.ShiJiaZhuang:
				DaoKu.sprite = Resources.Load("ShiJiaZhuangCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				CeFang.sprite = Resources.Load("ShiJiaZhuangCourseTwoImage/cefangtingche", typeof(Sprite)) as Sprite;
				QuXian.sprite = Resources.Load("ShiJiaZhuangCourseTwoImage/quxianxingshi", typeof(Sprite)) as Sprite;
				ZhiJiao.sprite = Resources.Load("ShiJiaZhuangCourseTwoImage/zhijiaozhuanwan", typeof(Sprite)) as Sprite;
				PoDao.sprite = Resources.Load("ShiJiaZhuangCourseTwoImage/podaodingdian", typeof(Sprite)) as Sprite;
				CoursetwoKaoShi.sprite = Resources.Load("ShiJiaZhuangCourseTwoImage/CoursetwoKaoShi", typeof(Sprite)) as Sprite;
				break;
			case CoursetwoScenesName.FoShan:
				DaoKu.sprite = Resources.Load("FoShanCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				CeFang.sprite = Resources.Load("FoShanCourseTwoImage/cefangtingche", typeof(Sprite)) as Sprite;
				QuXian.sprite = Resources.Load("FoShanCourseTwoImage/quxianxingshi", typeof(Sprite)) as Sprite;
				ZhiJiao.sprite = Resources.Load("FoShanCourseTwoImage/zhijiaozhuanwan", typeof(Sprite)) as Sprite;
				PoDao.sprite = Resources.Load("FoShanCourseTwoImage/podaodingdian", typeof(Sprite)) as Sprite;
				CoursetwoKaoShi.sprite = Resources.Load("FoShanCourseTwoImage/CoursetwoKaoShi", typeof(Sprite)) as Sprite;
				break;
			case CoursetwoScenesName.NanFeng:
				DaoKu.sprite = Resources.Load("NanFengCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				CeFang.sprite = Resources.Load("NanFengCourseTwoImage/cefangtingche", typeof(Sprite)) as Sprite;
				QuXian.sprite = Resources.Load("NanFengCourseTwoImage/quxianxingshi", typeof(Sprite)) as Sprite;
				ZhiJiao.sprite = Resources.Load("NanFengCourseTwoImage/zhijiaozhuanwan", typeof(Sprite)) as Sprite;
				PoDao.sprite = Resources.Load("NanFengCourseTwoImage/podaodingdian", typeof(Sprite)) as Sprite;
				CoursetwoKaoShi.sprite = Resources.Load("NanFengCourseTwoImage/CoursetwoKaoShi", typeof(Sprite)) as Sprite;
				break;
			default:
				DaoKu.sprite = Resources.Load("YiAnDaCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				CeFang.sprite = Resources.Load("YiAnDaCourseTwoImage/cefangtingche", typeof(Sprite)) as Sprite;
				QuXian.sprite = Resources.Load("YiAnDaCourseTwoImage/quxianxingshi", typeof(Sprite)) as Sprite;
				ZhiJiao.sprite = Resources.Load("YiAnDaCourseTwoImage/zhijiaozhuanwan", typeof(Sprite)) as Sprite;
				PoDao.sprite = Resources.Load("YiAnDaCourseTwoImage/podaodingdian", typeof(Sprite)) as Sprite;
				CoursetwoKaoShi.sprite = Resources.Load("YiAnDaCourseTwoImage/CoursetwoKaoShi", typeof(Sprite)) as Sprite;
				break;
			}
			if (!SettingInfoCtl.Instance.isManualGear)
			{
				GetBtn(4).SetActive(value: false);
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				if (SceneJumpCtl.Instance.CoursetwoName == CoursetwoScenesName.YiAnDa || SceneJumpCtl.Instance.CoursetwoName == CoursetwoScenesName.FoShan)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.DaoKuChooseView);
				}
				else
				{
					ViewJumpCtl.Instance.itemType = 0;
					ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
				}
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.itemType = 1;
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.itemType = 3;
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(3))
			{
				ViewJumpCtl.Instance.itemType = 4;
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(4))
			{
				ViewJumpCtl.Instance.itemType = 2;
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(5))
			{
				ViewJumpCtl.Instance.modelType = 2;
				ViewJumpCtl.Instance.itemType = 1200;
				SendBtnLevel(SceneJumpCtl.Instance.CoursetwoName);
				ViewJumpCtl.Instance.EnterScene();
			}
			else if (go == GetBtn(6))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			curClickBtnIndex = mSelectBtnIndex;
		}

		private void SendBtnLevel(CoursetwoScenesName CoursetwoName)
		{
			switch (CoursetwoName)
			{
			case CoursetwoScenesName.YiAnDa:
				NetworkManagement.Instance.BtnLevel = "A6";
				break;
			case CoursetwoScenesName.QingHu:
				NetworkManagement.Instance.BtnLevel = "A12";
				break;
			case CoursetwoScenesName.WenZhou:
				NetworkManagement.Instance.BtnLevel = "A18";
				break;
			case CoursetwoScenesName.HuNanYZ:
				NetworkManagement.Instance.BtnLevel = "A24";
				break;
			case CoursetwoScenesName.ShiJiaZhuang:
				NetworkManagement.Instance.BtnLevel = "A30";
				break;
			case CoursetwoScenesName.FoShan:
				NetworkManagement.Instance.BtnLevel = "A36";
				break;
			case CoursetwoScenesName.HeBei:
				NetworkManagement.Instance.BtnLevel = "A42";
				break;
			}
			NetworkManagement.Instance.SendStartTraining(NetworkManagement.Instance.BtnLevel);
		}
	}
}
