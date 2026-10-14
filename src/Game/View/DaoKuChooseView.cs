using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	public class DaoKuChooseView : ViewBase
	{
		public Image DaoKuLeft;

		public Image DaoKuRight;

		public override void Start()
		{
			base.Start();
			switch (SceneJumpCtl.Instance.CoursetwoName)
			{
			case CoursetwoScenesName.YiAnDa:
				DaoKuLeft.sprite = Resources.Load("YiAnDaCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				DaoKuRight.sprite = Resources.Load("YiAnDaCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				break;
			case CoursetwoScenesName.FoShan:
				DaoKuLeft.sprite = Resources.Load("FoShanCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				DaoKuRight.sprite = Resources.Load("FoShanCourseTwoImage/daocheruku", typeof(Sprite)) as Sprite;
				break;
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(0))
			{
				ViewJumpCtl.Instance.itemType = 34;
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(1))
			{
				ViewJumpCtl.Instance.itemType = 0;
				ViewJumpCtl.Instance.EnterView(ViewType.ModelChooseView);
			}
			else if (go == GetBtn(2))
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
			ViewJumpCtl.Instance.Enter = false;
		}
	}
}
