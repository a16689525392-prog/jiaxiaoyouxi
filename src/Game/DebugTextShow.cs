using UnityEngine;
using UnityEngine.UI;

public class DebugTextShow : MonoBehaviour
{
	public static DebugTextShow instance;

	private Text mytext;

	private void Awake()
	{
		instance = this;
		mytext = base.transform.GetComponent<Text>();
	}

	public void ShowText(string content)
	{
		mytext.text = content;
	}
}
