using Car;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class ShanLuHuiChe : ShanQuSceneBase
	{
		private static ShanLuHuiChe instance;

		private bool upHillGo;

		private bool waiDaoGo;

		private bool zhaiLuGo;

		private bool isSafeDistance0;

		private bool isSafeDistance1;

		private bool isSafeDistance2;

		private bool isInitCar;

		public float CarNum;

		public static ShanLuHuiChe Instance => instance;

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (!isInitCar)
			{
				base.gameObject.AddComponent<AiCarManager>();
				ShanQuSceneModel shanQuSceneModel = (ShanQuSceneModel)SceneModelBase.Instance;
				base.gameObject.GetComponent<AiCarManager>().Init(shanQuSceneModel.wayPointsGroup.transform);
				CarNum = 1f;
				ShanQuSceneCtl.isReset = false;
				isInitCar = true;
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.itemType == 5100)
			{
				if (itemStep == 0 && other.name == "HuiChe_StaightLu")
				{
					SoundPlayCtl.Instance.PlaySound("山路会车,请注意让行");
					CarNum = 0f;
					itemStep = 1;
				}
				else if (itemStep == 1 && other.name == "StaightLu_PoDao")
				{
					itemStep = 2;
				}
				else if (itemStep == 2 && !upHillGo && other.name == "SafeHuiChe0")
				{
					SoundPlayCtl.Instance.PlaySound("未让上坡车辆先行", 10f);
					upHillGo = true;
				}
				else if (itemStep != 3 && other.name == "HuiChe_WanLu")
				{
					base.gameObject.AddComponent<AiCarManager>();
					ShanQuSceneModel shanQuSceneModel = (ShanQuSceneModel)SceneModelBase.Instance;
					base.gameObject.GetComponent<AiCarManager>().Init(shanQuSceneModel.wayPointsGroup.transform);
					CarNum = 2f;
					SoundPlayCtl.Instance.PlaySound("弯道会车");
					itemStep = 3;
				}
				else if (itemStep == 3 && other.name == "WanDao")
				{
					itemStep = 4;
				}
				else if (itemStep == 4 && !waiDaoGo && other.name == "SafeHuiChe1")
				{
					SoundPlayCtl.Instance.PlaySound("未让不靠山体一方先行", 10f);
					waiDaoGo = true;
				}
				else if (itemStep != 5 && other.name == "HuiChe_ZhaiLu")
				{
					base.gameObject.AddComponent<AiCarManager>();
					ShanQuSceneModel shanQuSceneModel2 = (ShanQuSceneModel)SceneModelBase.Instance;
					base.gameObject.GetComponent<AiCarManager>().Init(shanQuSceneModel2.wayPointsGroup.transform);
					CarNum = 3f;
					SoundPlayCtl.Instance.PlaySound("窄路会车");
					itemStep = 5;
				}
				else if (itemStep == 5 && other.name == "ZhaiLu")
				{
					itemStep = 6;
				}
				else if (itemStep == 6 && !zhaiLuGo && other.name == "SafeHuiChe2")
				{
					SoundPlayCtl.Instance.PlaySound("未让不靠山体一方先行", 10f);
					zhaiLuGo = true;
				}
				if (!isSafeDistance0 && other.name == "SafeDistance0")
				{
					SoundPlayCtl.Instance.PlaySound("会车时,未保持安全车距", 100f);
					isSafeDistance0 = true;
				}
				else if (!isSafeDistance1 && other.name == "SafeDistance1")
				{
					SoundPlayCtl.Instance.PlaySound("会车时,未保持安全车距", 100f);
					isSafeDistance1 = true;
				}
				else if (!isSafeDistance2 && other.name == "SafeDistance2")
				{
					SoundPlayCtl.Instance.PlaySound("会车时,未保持安全车距", 100f);
					isSafeDistance2 = true;
				}
				else if (itemStep != 99 && other.name == "End")
				{
					ItemEnd();
					SoundPlayCtl.Instance.PlaySound("项目结束");
					itemStep = 99;
				}
			}
		}
	}
}
