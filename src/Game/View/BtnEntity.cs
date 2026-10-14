using UnityEngine;

namespace View
{
	public abstract class BtnEntity : MonoBehaviour
	{
		public int index;

		protected Color normalSelectTextColor = Color.green;

		protected Color normalSelectImageColor = Color.green;

		public abstract void Select();

		public abstract void Exit();

		public void SetBtnIndex(int index)
		{
			this.index = index;
		}
	}
}
