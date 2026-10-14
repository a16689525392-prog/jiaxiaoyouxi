using UnityEngine;
using UnityEngine.UI;
using View;

public class BiaoDingItem : MonoBehaviour
{
	public Text showText;

	public GameObject sureBtn;

	public void SetShowText(string value)
	{
		showText.text = value;
	}

	public void SetSureBtnName(string name)
	{
		sureBtn.GetComponentInChildren<Text>().text = name;
	}

	public void SetSureBtnIndex(int index)
	{
		sureBtn.GetComponent<NormalBtn>().SetBtnIndex(index);
	}
}
