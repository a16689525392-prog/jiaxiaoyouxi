using Car;
using SoundAndTextShow;
using UnityEngine;

namespace Scene
{
	public class GaoSuRoadOver : HighRoadSceneBase
	{
		private bool isInitCar;

		private bool isOverCar;

		private float SafeDisdanceTime = 10f;

		private void Update()
		{
			SafeDisdanceTime += Time.deltaTime;
			if (!isInitCar)
			{
				base.gameObject.AddComponent<AiCarManager>();
				HighRoadSceneModel highRoadSceneModel = (HighRoadSceneModel)SceneModelBase.Instance;
				base.gameObject.GetComponent<AiCarManager>().Init(highRoadSceneModel.wayPointsGroup.transform);
				HighRoadSceneCtl.Instance.isReset = false;
				isInitCar = true;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (itemStep == 0 && other.name == "Start")
			{
				SoundPlayCtl.Instance.PlaySound("前方收费站");
				itemStep = 1;
			}
			else if (!isOverCar && other.name == "OverCar")
			{
				SoundPlayCtl.Instance.PlaySound("高速公路收费站前超车", 100f);
				isOverCar = true;
			}
			else if (SafeDisdanceTime > 10f && other.name == "SafeDistance")
			{
				SoundPlayCtl.Instance.PlaySound("未与前车保持安全车距", 100f);
				SafeDisdanceTime = 0f;
			}
			else if (other.name == "End")
			{
				isOverCar = true;
				ItemEnd();
			}
		}
	}
}
