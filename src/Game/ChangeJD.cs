using UnityEngine;

public class ChangeJD : MonoBehaviour
{
	public GameObject dimian;

	public GameObject fangzi1;

	public GameObject fangzi2;

	public GameObject car;

	public GameObject welan;

	public GameObject paizi;

	private void Start()
	{
		for (int num = dimian.transform.childCount - 1; num >= 0; num--)
		{
			dimian.transform.GetChild(num).GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Standard");
		}
		fangzi1.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Standard");
		car.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Standard");
		paizi.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Standard");
		fangzi2.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Standard");
		welan.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Standard");
	}
}
