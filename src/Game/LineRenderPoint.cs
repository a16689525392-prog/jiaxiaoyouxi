using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LineRenderPoint : MonoBehaviour
{
	private Image image;

	private Text valueText;

	private float timeCount;

	private void Awake()
	{
		image = base.transform.Find("Image").GetComponent<Image>();
		valueText = base.transform.Find("Value").GetComponent<Text>();
	}

	private void Start()
	{
		StartCoroutine(DoLocalScale());
	}

	public void SetValue(float value)
	{
		valueText.text = value.ToString();
	}

	private void Update()
	{
	}

	private IEnumerator DoLocalScale()
	{
		while (timeCount < 1f)
		{
			timeCount += Time.deltaTime * 5f;
			if (timeCount > 1f)
			{
				timeCount = 1f;
			}
			image.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timeCount);
			yield return 0;
		}
		yield return 0;
	}
}
