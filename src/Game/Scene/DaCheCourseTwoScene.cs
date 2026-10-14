using System;
using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Scene
{
	public class DaCheCourseTwoScene : TrainSceneBase
	{
		private static DaCheCourseTwoScene instance;

		private int tempItemType;

		public Material shuMat;

		public Material diMianMat;

		public Material hua0Mat;

		public Material hua1Mat;

		public Material hua2Mat;

		private CourseTwoItemCtlBase curItemCtl;

		public static DaCheCourseTwoScene Instance => instance;

		protected override void Awake()
		{
			base.Awake();
			instance = this;
			diMianMat.shader = Shader.Find("Standard");
			if (ViewJumpCtl.Instance.itemType == 13)
			{
				shuMat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				hua0Mat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				hua1Mat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
				hua2Mat.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
			}
			else
			{
				shuMat.shader = Shader.Find("Sprites/Default");
				hua0Mat.shader = Shader.Find("Sprites/Default");
				hua1Mat.shader = Shader.Find("Sprites/Default");
				hua2Mat.shader = Shader.Find("Sprites/Default");
			}
		}

		protected override void Start()
		{
			base.Start();
			ColliderDetection colliderDetection = ColliderDetection.instance;
			colliderDetection.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Combine(colliderDetection.roadIndexChangeCallBack, new UnityAction<int, int>(JudgeTurnLight));
			ColliderDetection colliderDetection2 = ColliderDetection.instance;
			colliderDetection2.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Combine(colliderDetection2.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			tempItemType = -1;
			base.ResetAction = (UnityAction)Delegate.Combine(base.ResetAction, new UnityAction(TrainItemReset));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Combine(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
		}

		private void TrainItemReset()
		{
			tempItemType = -1;
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
			if (ViewJumpCtl.Instance.itemType == 13)
			{
				SkyBoxCtl.Instance.ChangeSkyBox(ViewJumpCtl.Instance.otherType = 5010);
			}
			if (ViewJumpCtl.Instance.itemType == 12)
			{
				UnityEngine.Object.Destroy(YuTian.rain);
			}
		}

		private void ResetTimePlay()
		{
			CarState.Instance.FireOn = false;
			tempItemType = -1;
			JudgeItemReset(ViewJumpCtl.Instance.itemType);
			if (ViewJumpCtl.Instance.itemType == 13)
			{
				SkyBoxCtl.Instance.ChangeSkyBox(ViewJumpCtl.Instance.otherType = 5010);
			}
			if (ViewJumpCtl.Instance.itemType == 12)
			{
				UnityEngine.Object.Destroy(YuTian.rain);
			}
		}

		private void TriggerAreaEvent(Collider other)
		{
			JudgeItemReset(other.name);
			if (curItemCtl != null)
			{
				curItemCtl.TriggerEvent(other);
			}
		}

		private void JudgeItemReset(int itemType)
		{
			switch (itemType)
			{
			case 1100:
				JudgeItemReset("Start_KaoShi");
				break;
			case 1:
				JudgeItemReset("Start_CeFang");
				break;
			case 5:
				JudgeItemReset("Start_DaoZhuang");
				break;
			case 2:
				JudgeItemReset("Start_PoDao");
				break;
			case 7:
				JudgeItemReset("Start_Qiao");
				break;
			case 8:
				JudgeItemReset("Start_QiFu");
				break;
			case 4:
				JudgeItemReset("Start_ZhiJiao");
				break;
			case 6:
				JudgeItemReset("Start_ZhangAi");
				break;
			case 3:
				JudgeItemReset("Start_QuXian");
				break;
			case 9:
				JudgeItemReset("Start_XianKuan");
				break;
			case 13:
				JudgeItemReset("Start_WuTian");
				break;
			case 12:
				JudgeItemReset("Start_YuTian");
				break;
			case 11:
				JudgeItemReset("Start_ShiHua");
				break;
			case 14:
				JudgeItemReset("Start_SuiDao");
				break;
			case 15:
				JudgeItemReset("Start_ZhuanWan");
				break;
			case 16:
				JudgeItemReset("Start_GaoSu");
				break;
			case 10:
				JudgeItemReset("Start_ZhaiLu");
				break;
			}
		}

		private void JudgeItemReset(string other)
		{
			switch (other)
			{
			case "Start_KaoShi":
				if (tempItemType != 1100)
				{
					tempItemType = 1100;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
					}
					ItemStart();
				}
				break;
			case "Start_CeFang":
				if (tempItemType != 1)
				{
					tempItemType = 1;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "DFCeFang"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<DFCeFangTingChe>();
					}
					ItemStart();
				}
				break;
			case "Start_DaoZhuang":
				if (tempItemType != 5)
				{
					tempItemType = 5;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "DaoZhuang"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<DaoZhuang>();
					}
					ItemStart();
				}
				break;
			case "Start_PoDao":
				if (tempItemType != 2)
				{
					tempItemType = 2;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "DFPoDao"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<DFPoDaoDingDian>();
					}
					ItemStart();
				}
				break;
			case "Start_Qiao":
				if (tempItemType != 7)
				{
					tempItemType = 7;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "DanBianQiao"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<DanBianQiao>();
					}
					ItemStart();
				}
				break;
			case "Start_QiFu":
				if (tempItemType != 8)
				{
					tempItemType = 8;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "QiFuLu"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<QiFuLu>();
					}
					ItemStart();
				}
				break;
			case "Start_ZhiJiao":
				if (tempItemType != 4)
				{
					tempItemType = 4;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "DFZhiJiao"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<DFZhiJiaoZhuanWan>();
					}
					ItemStart();
				}
				break;
			case "Start_ZhangAi":
				if (tempItemType != 6)
				{
					tempItemType = 6;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "LianXuZhangAi"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<ZhangAi>();
					}
					ItemStart();
				}
				break;
			case "Start_QuXian":
				if (tempItemType != 3)
				{
					tempItemType = 3;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "DFQuXian"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<DFQuXianXingShi>();
					}
					ItemStart();
				}
				break;
			case "Start_XianKuan":
				if (tempItemType != 9)
				{
					tempItemType = 9;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "XianKuanDoor"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<XianKuan>();
					}
					ItemStart();
				}
				break;
			case "Start_WuTian":
				if (tempItemType != 13)
				{
					tempItemType = 13;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "MoNiFog"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<WuTian>();
					}
					ItemStart();
				}
				break;
			case "Start_YuTian":
				if (tempItemType != 12)
				{
					tempItemType = 12;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<YuTian>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "MoNiRain"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<YuTian>();
					}
					ItemStart();
				}
				break;
			case "Start_ShiHua":
				if (tempItemType != 11)
				{
					tempItemType = 11;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "ShiHuaLuMian"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<ShiHua>();
					}
					ItemStart();
				}
				break;
			case "Start_SuiDao":
				if (tempItemType != 14)
				{
					tempItemType = 14;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "MoNiSuiDao"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<MoNiSuiDao>();
					}
					ItemStart();
				}
				break;
			case "Start_ZhuanWan":
				if (tempItemType != 15)
				{
					tempItemType = 15;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "LianXuZhuanWan"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<MoNiZhuanWan>();
					}
					ItemStart();
				}
				break;
			case "Start_GaoSu":
				if (tempItemType != 16)
				{
					tempItemType = 16;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "MoNiGaoSu"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<MoNiGaoSu>();
					}
					ItemStart();
				}
				break;
			case "Start_ZhaiLu":
				if (tempItemType != 10)
				{
					tempItemType = 10;
					if (curItemCtl != null)
					{
						UnityEngine.Object.Destroy(curItemCtl);
					}
					if (ViewJumpCtl.Instance.modelType == 0)
					{
						curItemCtl = base.gameObject.AddComponent<DFCourseTwoYanShi>();
						StartCoroutine(DriveHistoryPlay.Instance.LoadHistoryRecordFiles(DataCenter.Instance.GetFilePath(1) + "ZhaiLuDiaoTou"));
					}
					else if (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 6)
					{
						curItemCtl = base.gameObject.AddComponent<ZhaiLu>();
					}
					ItemStart();
				}
				break;
			}
		}

		private void ItemStart()
		{
		}

		private void JudgeTurnLight(int preIndex, int curIndex)
		{
			if (ViewJumpCtl.Instance.itemType == 16)
			{
				if (curIndex > preIndex)
				{
					CarActionJudge.Instance.JudgeTurnLight3s(109);
				}
				else
				{
					CarActionJudge.Instance.JudgeTurnLight3s(110);
				}
			}
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			SoundPlayCtl.Instance.ClearClip();
			ColliderDetection colliderDetection = ColliderDetection.instance;
			colliderDetection.roadIndexChangeCallBack = (UnityAction<int, int>)Delegate.Remove(colliderDetection.roadIndexChangeCallBack, new UnityAction<int, int>(JudgeTurnLight));
			ColliderDetection colliderDetection2 = ColliderDetection.instance;
			colliderDetection2.TriggerAreaCallback = (UnityAction<Collider>)Delegate.Remove(colliderDetection2.TriggerAreaCallback, new UnityAction<Collider>(TriggerAreaEvent));
			TrainSceneBase baseInstance = TrainSceneBase.BaseInstance;
			baseInstance.ResetTime = (UnityAction)Delegate.Remove(baseInstance.ResetTime, new UnityAction(ResetTimePlay));
			base.ResetAction = (UnityAction)Delegate.Remove(base.ResetAction, new UnityAction(TrainItemReset));
			if (ViewJumpCtl.Instance.itemType == 13)
			{
				SkyBoxCtl.Instance.ChangeSkyBox(ViewJumpCtl.Instance.otherType = 5010);
			}
		}
	}
}
