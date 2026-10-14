using System;
using Car;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class CtiyDriveSkill : MonoBehaviour
	{
		private static CtiyDriveSkill instance;

		public bool isReset;

		public static CtiyDriveSkill Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			switch (ViewJumpCtl.Instance.itemType)
			{
			case 5110:
				base.gameObject.AddComponent<RangChaoChe>();
				break;
			case 5111:
				base.gameObject.AddComponent<CityGenChe>();
				break;
			}
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetAction = (UnityAction)Delegate.Combine(baseInstance.ResetAction, new UnityAction(Reset));
			TrainSceneBase baseInstance2 = TrainSceneBase.BaseInstance;
			baseInstance2.ResetTime = (UnityAction)Delegate.Combine(baseInstance2.ResetTime, new UnityAction(ResetTimePlay));
		}

		private void Update()
		{
			if (ViewJumpCtl.Instance.itemType == 5111 && !AICarMove.isInitCar)
			{
				base.gameObject.AddComponent<AiCarManager>();
				CourseThreeSceneModel courseThreeSceneModel = (CourseThreeSceneModel)SceneModelBase.Instance;
				base.gameObject.GetComponent<AiCarManager>().Init(courseThreeSceneModel.wayPointsGroup.transform);
				AICarMove.isInitCar = true;
				isReset = false;
			}
		}

		private void ResetTimePlay()
		{
			UnityEngine.Object.Destroy(AICarMove.aicar);
			AICarMove.index = 0;
			AICarMove.isInitCar = false;
			RangChaoChe.isChangRoad = false;
			RangChaoChe.isOverSpeed = false;
			RangChaoChe.errorShoudTime = 0f;
			AICarMove.start = false;
			AICarMove.isSound = false;
			AICarMove.isSound1 = false;
			AICarMove.isSound2 = false;
			AICarMove.isSound3 = false;
			AICarMove.isEndOver = false;
			AICarMove.soundTime = 0f;
			CarState.Instance.FireOn = false;
			if (!isReset)
			{
				isReset = true;
			}
		}

		private void Reset()
		{
			UnityEngine.Object.Destroy(AICarMove.aicar);
			AICarMove.index = 0;
			AICarMove.isInitCar = false;
			RangChaoChe.isChangRoad = false;
			RangChaoChe.isOverSpeed = false;
			RangChaoChe.errorShoudTime = 0f;
			AICarMove.start = false;
			AICarMove.isSound = false;
			AICarMove.isSound1 = false;
			AICarMove.isSound2 = false;
			AICarMove.isSound3 = false;
			AICarMove.isEndOver = false;
			AICarMove.soundTime = 0f;
			CarState.Instance.FireOn = false;
			if (!isReset)
			{
				isReset = true;
			}
		}

		private void OnDestroy()
		{
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			TrainSceneBase baseInstance2 = TrainSceneBase.BaseInstance;
			baseInstance2.ResetAction = (UnityAction)Delegate.Remove(baseInstance2.ResetAction, new UnityAction(Reset));
			SoundPlayCtl.Instance.ClearClip();
		}
	}
}
