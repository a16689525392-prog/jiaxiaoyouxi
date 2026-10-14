using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class WeiJinZhiXing : XiangCunSceneBase
	{
		private GameObject driveInstructionView;

		private new void Start()
		{
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "IfZhiXing")
			{
				itemStep = 1;
			}
			else if (itemStep == 1 && other.name == "BanZhiXing")
			{
				SoundPlayCtl.Instance.PlaySound("违禁直行", 100f);
				itemStep = 2;
			}
			else if (itemStep != 3 && other.name == "IfYouZhuan")
			{
				itemStep = 3;
			}
			else if (itemStep == 3 && other.name == "BanYouZhuan")
			{
				SoundPlayCtl.Instance.PlaySound("违禁右转弯", 100f);
				itemStep = 4;
			}
			else if (itemStep != 5 && other.name == "IfZuoZhuan")
			{
				itemStep = 5;
			}
			else if (itemStep == 5 && other.name == "BanZuoZhuan")
			{
				SoundPlayCtl.Instance.PlaySound("违禁左转弯", 100f);
				itemStep = 6;
			}
		}
	}
}
