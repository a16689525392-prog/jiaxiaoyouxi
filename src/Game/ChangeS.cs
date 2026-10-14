using UnityEngine;

public class ChangeS : MonoBehaviour
{
	public GameObject caodi;

	public GameObject ziti;

	public GameObject changdi;

	public GameObject quxiancaodi;

	public GameObject welan;

	private void Start()
	{
		caodi.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Mobile/Diffuse");
		ziti.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Mobile/Diffuse");
		quxiancaodi.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Mobile/Diffuse");
		changdi.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Mobile/Diffuse");
		welan.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Mobile/Diffuse");
	}

	private void Update()
	{
	}
}
