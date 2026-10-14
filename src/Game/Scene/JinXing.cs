using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class JinXing : ShanQuSceneBase
	{
		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "BanXing")
			{
				SoundPlayCtl.Instance.PlaySound("违反禁行规定通行", 100f);
				itemStep = 1;
			}
		}
	}
}
