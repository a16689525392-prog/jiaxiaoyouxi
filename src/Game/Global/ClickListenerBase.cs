using UnityEngine;
using UnityEngine.EventSystems;

namespace Global
{
	public class ClickListenerBase : EventTrigger
	{
		public delegate void VoidDelegate(GameObject go);

		public VoidDelegate onClick;

		public VoidDelegate onDown;

		public VoidDelegate onEnter;

		public VoidDelegate onExit;

		public VoidDelegate onUp;

		public VoidDelegate onSelect;

		public VoidDelegate onUpdataSelect;

		public static ClickListenerBase Get(GameObject go)
		{
			if (null == go)
			{
				return null;
			}
			ClickListenerBase clickListenerBase = go.GetComponent<ClickListenerBase>();
			if (clickListenerBase == null)
			{
				clickListenerBase = go.AddComponent<ClickListenerBase>();
			}
			return clickListenerBase;
		}

		public override void OnPointerClick(PointerEventData eventData)
		{
			if (onClick != null)
			{
				onClick(base.gameObject);
			}
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			if (onDown != null)
			{
				onDown(base.gameObject);
			}
		}

		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (onEnter != null)
			{
				onEnter(base.gameObject);
			}
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			if (onExit != null)
			{
				onExit(base.gameObject);
			}
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			if (onUp != null)
			{
				onUp(base.gameObject);
			}
		}

		public override void OnSelect(BaseEventData eventData)
		{
			if (onSelect != null)
			{
				onSelect(base.gameObject);
			}
		}

		public override void OnUpdateSelected(BaseEventData eventData)
		{
			if (onUpdataSelect != null)
			{
				onUpdataSelect(base.gameObject);
			}
		}
	}
}
