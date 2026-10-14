using UnityEngine;

public class SetRenderTexture : MonoBehaviour
{
	public int flag;

	private void Start()
	{
		switch (flag)
		{
		case 0:
			GetComponent<Renderer>().material.mainTexture = ResourceManager.leftMirrorTexture;
			break;
		case 1:
			GetComponent<Renderer>().material.mainTexture = ResourceManager.middleMirrorTexture;
			break;
		case 2:
			GetComponent<Renderer>().material.mainTexture = ResourceManager.rightMirrorTexture;
			break;
		case 3:
			GetComponent<Renderer>().material.mainTexture = ResourceManager.rearBackTexture;
			break;
		}
	}
}
