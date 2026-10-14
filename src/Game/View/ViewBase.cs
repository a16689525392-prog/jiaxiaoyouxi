using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace View
{
	public class ViewBase : MonoBehaviour
	{
		public ViewType mViewType;

		public List<GameObject> btnList = new List<GameObject>();

		public GameObject backBtn;

		[HideInInspector]
		public int mSelectBtnIndex = -1;

		protected int viewBtnCount;

		public Dictionary<int, GameObject> btnDic = new Dictionary<int, GameObject>();

		public virtual void Awake()
		{
			InitBtngroup();
		}

		public virtual void Start()
		{
			if (viewBtnCount > 0)
			{
				switch (mViewType)
				{
				case ViewType.MainView:
					BtnEnter(GetBtn(MainView.curClickBtnIndex).gameObject);
					break;
				case ViewType.SoftSettingView:
					BtnEnter(GetBtn(SettingView.curClickBtnIndex).gameObject);
					break;
				case ViewType.CourseTwoView:
					BtnEnter(GetBtn(CourseTwoChooseView.curClickBtnIndex).gameObject);
					break;
				case ViewType.SceneMenuView:
					BtnEnter(GetBtn(0).gameObject);
					break;
				default:
					BtnEnter(GetBtn(0).gameObject);
					break;
				}
			}
		}

		public virtual void BtnClick(GameObject go)
		{
		}

		public virtual void BtnEnter(GameObject go)
		{
			if (mSelectBtnIndex != go.GetComponent<BtnEntity>().index)
			{
				if (!btnDic.ContainsKey(mSelectBtnIndex))
				{
					mSelectBtnIndex = go.GetComponent<BtnEntity>().index;
				}
				if (mSelectBtnIndex != -1)
				{
					GetBtn(mSelectBtnIndex).GetComponent<BtnEntity>().Exit();
				}
				go.GetComponent<BtnEntity>().Select();
				mSelectBtnIndex = go.GetComponent<BtnEntity>().index;
			}
		}

		public virtual void SelectNextBtn()
		{
			SelectNextBtn(mSelectBtnIndex);
		}

		public virtual void SelectPreBtn()
		{
			SelectPreBtn(mSelectBtnIndex);
		}

		public virtual void SelectNextBtn(int curIndex)
		{
			if (viewBtnCount > 0)
			{
				int num = curIndex + 1;
				if (num > viewBtnCount - 1)
				{
					num = 0;
				}
				if (GetBtn(num) == null || !GetBtn(num).activeSelf)
				{
					SelectNextBtn(num);
				}
				else
				{
					BtnEnter(GetBtn(num).gameObject);
				}
			}
		}

		public virtual void SelectPreBtn(int curIndex)
		{
			if (viewBtnCount > 0)
			{
				int num = curIndex - 1;
				if (num < 0)
				{
					num = viewBtnCount - 1;
				}
				if (GetBtn(num) == null || !GetBtn(num).activeSelf)
				{
					SelectPreBtn(num);
				}
				else
				{
					BtnEnter(GetBtn(num).gameObject);
				}
			}
		}

		public virtual void CurBtnClick()
		{
			BtnClick(GetBtn(mSelectBtnIndex));
		}

		public void BackBtnClick()
		{
			if (backBtn != null)
			{
				BtnClick(backBtn);
			}
		}

		private void InitBtngroup()
		{
			if (btnList.Count <= 0)
			{
				return;
			}
			foreach (GameObject btn in btnList)
			{
				if (btn.transform.GetComponent<BtnEntity>() != null && btn.activeSelf)
				{
					BtnClickBase btnClickBase = BtnClickBase.Get(btn);
					btnClickBase.OnBtnClick = (UnityAction<GameObject>)Delegate.Combine(btnClickBase.OnBtnClick, new UnityAction<GameObject>(BtnClick));
					BtnClickBase btnClickBase2 = BtnClickBase.Get(btn);
					btnClickBase2.OnBtnEnter = (UnityAction<GameObject>)Delegate.Combine(btnClickBase2.OnBtnEnter, new UnityAction<GameObject>(BtnEnter));
					int index = btn.GetComponent<BtnEntity>().index;
					btnDic.Add(index, btn);
					viewBtnCount++;
				}
				else if (btn.activeSelf)
				{
					Debug.LogError(btn.name + "没有添加BtnEntity组件");
				}
			}
		}

		public virtual void ClearBtnFangFa()
		{
			if (btnList.Count <= 0)
			{
				return;
			}
			foreach (GameObject btn in btnList)
			{
				if (btn.transform.GetComponent<BtnEntity>() != null && btn.activeSelf)
				{
					BtnClickBase btnClickBase = BtnClickBase.Get(btn);
					btnClickBase.OnBtnClick = (UnityAction<GameObject>)Delegate.Remove(btnClickBase.OnBtnClick, new UnityAction<GameObject>(BtnClick));
				}
				else if (btn.activeSelf)
				{
					Debug.LogError(btn.name + "没有添加BtnEntity组件");
				}
			}
		}

		protected virtual GameObject GetBtn(int index)
		{
			if (btnDic.ContainsKey(index))
			{
				return btnDic[index];
			}
			Debug.LogError(base.gameObject.name + "没有下标为" + index + "的按钮");
			return null;
		}
	}
}
