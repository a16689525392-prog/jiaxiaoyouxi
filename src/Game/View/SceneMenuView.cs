using System;
using System.Collections;
using Car;
using Global;
using Scene;
using UnityEngine;
using UnityEngine.Events;

namespace View
{
	public class SceneMenuView : ViewBase
	{
		public GameObject DXTS;

		public static SceneMenuView Instance;

		public static bool isWeatherChoose;

		public static bool isCameraChange;

		public static bool isReset;

		public static bool isNotDriveCamera;

		public static bool KaoShiReset;

		public static string Read29;

		public GameObject[] subButtons;

		public Vector2 offset = new Vector2(-60f, 0f);

		public float animationDuration = 3f;

		public bool isOpenMenu;

		public GameObject MenuList;

		public int CurSelectBtnIndex;

		private float HeartbeatErrorTime;

		private int Errorindex;

		private float CloseMenuTime;

		public override void Awake()
		{
			Instance = this;
			base.Awake();
		}

		public override void Start()
		{
			base.Start();
			HeartbeatErrorTime = 0f;
			Errorindex = 0;
			if (ViewJumpCtl.Instance.itemType == 51200 || ViewJumpCtl.Instance.itemType == 51201 || ViewJumpCtl.Instance.itemType == 51202)
			{
				GetBtn(0).gameObject.SetActive(value: true);
			}
			else
			{
				GetBtn(0).gameObject.SetActive(value: false);
			}
			foreach (GameObject btn in btnList)
			{
				BtnClickBase btnClickBase = BtnClickBase.Get(btn);
				btnClickBase.OnBtnEnter = (UnityAction<GameObject>)Delegate.Combine(btnClickBase.OnBtnEnter, new UnityAction<GameObject>(ShowText));
				BtnClickBase btnClickBase2 = BtnClickBase.Get(btn);
				btnClickBase2.OnBtnExit = (UnityAction<GameObject>)Delegate.Combine(btnClickBase2.OnBtnExit, new UnityAction<GameObject>(HideText));
			}
			for (int i = 0; i < subButtons.Length; i++)
			{
				subButtons[i].transform.localPosition = GetBtn(4).transform.localPosition;
				subButtons[i].transform.GetChild(1).gameObject.SetActive(value: false);
			}
			MenuList.SetActive(value: false);
			mSelectBtnIndex = 5;
			CurSelectBtnIndexIsChange();
			GetBtn(5).GetComponent<NormalBtn>().Select();
		}

		private void Update()
		{
			CurSelectBtnIndexIsChange();
			if (!isOpenMenu)
			{
				CloseMenuTime += Time.deltaTime;
				if (CloseMenuTime > 2f && CloseMenuTime < 99f)
				{
					StopCoroutine("MenuListButtonsCoroutine");
					StartCoroutine("MenuListButtonsCoroutine", false);
					CloseMenuTime = 99f;
				}
			}
			else
			{
				CloseMenuTime = 0f;
			}
			if (Read29 != "c0")
			{
				SignalSourceCtl.Instance.SetWriteBuffer(29, 192);
				Read29 = "c0";
			}
			if (!NetworkManagement.Instance.IsHeartbeatRight)
			{
				HeartbeatErrorTime += Time.deltaTime;
				if (HeartbeatErrorTime > 0.2f && Errorindex == 0)
				{
					NetworkManagement.Instance.SendTiming();
					Errorindex = 1;
				}
				else if (HeartbeatErrorTime > 0.5f && Errorindex == 1)
				{
					ViewJumpCtl.Instance.ShowMsgTips("心跳检测错误,即将退出训练!");
					Errorindex = 2;
				}
				else if (HeartbeatErrorTime > 2f && Errorindex == 2)
				{
					BadWeatherView.scene = "null";
					SceneJumpCtl.Instance.EnterScene(SceneType.MainScene);
					ViewJumpCtl.Instance.modelType = 2;
					CarState.Instance.FireOn = false;
					MoNiCarDynamic.Instance.dynamicData.engineSpeed = 0f;
					StopAllCoroutines();
					ViewJumpCtl.Instance.ReturnPreView();
					isCameraChange = false;
					isWeatherChoose = false;
					GameGlobalCtl.PXvalue = 0f;
					GameGlobalCtl.PYvalue = 0f;
					GameGlobalCtl.PZvalue = 0f;
					GameGlobalCtl.RXvalue = 0f;
					GameGlobalCtl.RYvalue = 0f;
					GameGlobalCtl.RZvalue = 0f;
					CarCtl.IsNoOil = false;
					SendOver();
					NetworkManagement.Instance.TrainingIsPass = false;
					NetworkManagement.Instance.KaoShiIsPass = false;
					ViewJumpCtl.Instance.IsKaoShi = false;
					DataCenter.Instance.score = 100f;
					NetworkManagement.Instance.IsHeartbeatRight = true;
					Errorindex = 0;
				}
			}
			else
			{
				HeartbeatErrorTime = 0f;
				Errorindex = 0;
			}
		}

		public override void BtnClick(GameObject go)
		{
			base.BtnClick(go);
			if (go == GetBtn(5))
			{
				SendOver();
				BadWeatherView.scene = "null";
				SceneJumpCtl.Instance.EnterScene(SceneType.MainScene);
				ViewJumpCtl.Instance.modelType = 2;
				CarState.Instance.FireOn = false;
				MoNiCarDynamic.Instance.dynamicData.engineSpeed = 0f;
				StopAllCoroutines();
				ViewJumpCtl.Instance.ReturnPreView();
				isCameraChange = false;
				isWeatherChoose = false;
				GameGlobalCtl.PXvalue = 0f;
				GameGlobalCtl.PYvalue = 0f;
				GameGlobalCtl.PZvalue = 0f;
				GameGlobalCtl.RXvalue = 0f;
				GameGlobalCtl.RYvalue = 0f;
				GameGlobalCtl.RZvalue = 0f;
				CarCtl.IsNoOil = false;
				NetworkManagement.Instance.TrainingIsPass = false;
				NetworkManagement.Instance.KaoShiIsPass = false;
				ViewJumpCtl.Instance.IsKaoShi = false;
				DataCenter.Instance.score = 100f;
			}
			if (go == GetBtn(4))
			{
				return;
			}
			if (go == GetBtn(3) && go.activeInHierarchy)
			{
				isReset = true;
				KaoShiReset = true;
				TrainSceneBase.BaseInstance.ResetAction();
				CarCtl.IsNoOil = false;
				if (NetworkManagement.Instance.IsStartTraining)
				{
					NetworkManagement.Instance.errorid = "1000";
					NetworkManagement.Instance.IsSendError = true;
				}
			}
			else if (go == GetBtn(2) && go.activeInHierarchy)
			{
				CarCameraCtl.Instance.ShowNextViewPoint();
				SettingInfoCtl.Instance.SaveSetingInfos();
			}
			else if (go == GetBtn(1) && go.activeInHierarchy)
			{
				if (AdjustMirrorView.instance == null)
				{
					ViewJumpCtl.Instance.EnterView(ViewType.AdjustMirrorView, isDestroyCurView: false);
				}
			}
			else if (go == GetBtn(0) && go.activeInHierarchy && !isWeatherChoose)
			{
				ViewJumpCtl.Instance.EnterView(ViewType.TianQiQiangDuView, isDestroyCurView: false);
				isWeatherChoose = true;
			}
		}

		private void CurSelectBtnIndexIsChange()
		{
			if (CurSelectBtnIndex == mSelectBtnIndex)
			{
				return;
			}
			foreach (GameObject btn in btnList)
			{
				if (btn == GetBtn(mSelectBtnIndex))
				{
					btn.transform.GetChild(1).gameObject.SetActive(value: true);
					if (btn == GetBtn(4))
					{
						MenuList.SetActive(value: true);
						StopCoroutine("MenuListButtonsCoroutine");
						StartCoroutine("MenuListButtonsCoroutine", true);
						isOpenMenu = true;
					}
					if (btn == GetBtn(5))
					{
						StopCoroutine("MenuListButtonsCoroutine");
						StartCoroutine("MenuListButtonsCoroutine", false);
					}
				}
				else if (btn != GetBtn(4) && btn != GetBtn(5))
				{
					btn.transform.GetChild(1).gameObject.SetActive(value: false);
				}
			}
			CurSelectBtnIndex = mSelectBtnIndex;
		}

		private void ShowText(GameObject go)
		{
			go.transform.GetChild(1).gameObject.SetActive(value: true);
			if (go == GetBtn(4))
			{
				MenuList.SetActive(value: true);
				StopCoroutine("MenuListButtonsCoroutine");
				StartCoroutine("MenuListButtonsCoroutine", true);
			}
			if (go != GetBtn(5))
			{
				isOpenMenu = true;
			}
		}

		private void HideText(GameObject go)
		{
			if (go != GetBtn(4) && go != GetBtn(5))
			{
				go.transform.GetChild(1).gameObject.SetActive(value: false);
			}
			isOpenMenu = false;
		}

		private IEnumerator MenuListButtonsCoroutine(bool isShow)
		{
			float elapsedTime = 0f;
			while (elapsedTime < animationDuration)
			{
				for (int i = 0; i < subButtons.Length; i++)
				{
					Vector3 b;
					if (isShow)
					{
						b = GetBtn(4).transform.localPosition + new Vector3(offset.x * (float)(i + 1), 0f, 0f);
						if (Mathf.Abs(b.x - subButtons[i].transform.localPosition.x) >= 1f)
						{
							if (subButtons[i] == GetBtn(0))
							{
								if (ViewJumpCtl.Instance.itemType == 51200 || ViewJumpCtl.Instance.itemType == 51201 || ViewJumpCtl.Instance.itemType == 51202)
								{
									GetBtn(0).gameObject.SetActive(value: true);
								}
								else
								{
									GetBtn(0).gameObject.SetActive(value: false);
								}
							}
							else
							{
								subButtons[i].SetActive(value: true);
							}
						}
					}
					else
					{
						b = GetBtn(4).transform.localPosition;
						if (Mathf.Abs(b.x - subButtons[i].transform.localPosition.x) < 1f)
						{
							subButtons[i].SetActive(value: false);
						}
					}
					subButtons[i].transform.localPosition = Vector3.Lerp(subButtons[i].transform.localPosition, b, elapsedTime / animationDuration);
				}
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			for (int j = 0; j < subButtons.Length; j++)
			{
				if (isShow)
				{
					subButtons[j].transform.localPosition = GetBtn(4).transform.localPosition + new Vector3(offset.x * (float)(j + 1), 0f, 0f);
					continue;
				}
				subButtons[j].transform.localPosition = GetBtn(4).transform.localPosition;
				MenuList.SetActive(value: false);
			}
		}

		private void SendOver()
		{
			string fraction = DataCenter.Instance.score.ToString();
			string isPass = "0";
			if (ViewJumpCtl.Instance.IsKaoShi)
			{
				if (NetworkManagement.Instance.KaoShiIsPass && DataCenter.Instance.score >= 80f)
				{
					isPass = "1";
				}
			}
			else if (NetworkManagement.Instance.TrainingIsPass && DataCenter.Instance.score >= 80f)
			{
				isPass = "1";
			}
			if (ViewJumpCtl.Instance.trainType == TrainType.CourseTwo)
			{
				NetworkManagement.Instance.SendOverTraining("0", fraction, isPass);
			}
			else if (ViewJumpCtl.Instance.trainType == TrainType.CourseThree)
			{
				if (SceneJumpCtl.Instance.CoursethreeName == CoursethreeScenesName.NanChang)
				{
					NetworkManagement.Instance.SendOverTraining("0", fraction, isPass);
				}
				else
				{
					NetworkManagement.Instance.SendOverTraining("1", fraction, isPass);
				}
			}
			else
			{
				NetworkManagement.Instance.SendOverTraining("0", fraction, isPass);
			}
		}
	}
}
