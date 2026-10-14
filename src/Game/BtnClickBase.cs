using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BtnClickBase : EventTrigger
{
	public UnityAction<GameObject> OnBtnClick;

	public UnityAction<GameObject> OnBtnEnter;

	public UnityAction<GameObject> OnBtnExit;

	public static BtnClickBase Get(GameObject go)
	{
		if (go.GetComponent<BtnClickBase>() == null)
		{
			go.AddComponent<BtnClickBase>();
		}
		return go.GetComponent<BtnClickBase>();
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
		if (OnBtnClick != null)
		{
			OnBtnClick(base.gameObject);
		}
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		if (OnBtnEnter != null)
		{
			OnBtnEnter(base.gameObject);
		}
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		if (OnBtnExit != null)
		{
			OnBtnExit(base.gameObject);
		}
	}
}
