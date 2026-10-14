using View;

namespace Scene
{
	public class MainScene : SceneBase
	{
		private static bool isFirst = true;

		protected override void Start()
		{
			base.Start();
			if (isFirst)
			{
				isFirst = false;
				ViewJumpCtl.Instance.EnterView(ViewType.MainView);
				NetworkManagement.Instance.IsYouKeModel = true;
			}
			else
			{
				ViewJumpCtl.Instance.ReturnPreView();
			}
		}
	}
}
