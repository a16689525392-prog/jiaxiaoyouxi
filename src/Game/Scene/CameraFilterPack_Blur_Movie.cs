using UnityEngine;
using View;

namespace Scene
{
	[ExecuteInEditMode]
	[AddComponentMenu("Camera Filter Pack/Blur/Movie")]
	public class CameraFilterPack_Blur_Movie : MonoBehaviour
	{
		public Shader SCShader;

		private float TimeX = 1f;

		private Vector4 ScreenResolution;

		private Material SCMaterial;

		[Range(0f, 1000f)]
		public float Radius;

		[Range(0f, 1000f)]
		public float Factor = 200f;

		[Range(1f, 8f)]
		public int FastFilter = 2;

		public static float ChangeRadius;

		public static float ChangeFactor;

		public static int ChangeFastFilter;

		private bool isMoHu;

		private Material material
		{
			get
			{
				if (SCMaterial == null)
				{
					SCMaterial = new Material(SCShader);
					SCMaterial.hideFlags = HideFlags.HideAndDontSave;
				}
				return SCMaterial;
			}
		}

		private void Start()
		{
			ChangeRadius = Radius;
			ChangeFactor = Factor;
			ChangeFastFilter = FastFilter;
			SCShader = Shader.Find("CameraFilterPack/Blur_Movie");
			if (!SystemInfo.supportsImageEffects)
			{
				base.enabled = false;
			}
		}

		private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
		{
			if (SCShader != null)
			{
				int fastFilter = FastFilter;
				TimeX += Time.deltaTime;
				if (TimeX > 100f)
				{
					TimeX = 0f;
				}
				material.SetFloat("_TimeX", TimeX);
				material.SetFloat("_Radius", Radius / (float)fastFilter);
				material.SetFloat("_Factor", Factor);
				material.SetVector("_ScreenResolution", new Vector2(Screen.width / fastFilter, Screen.height / fastFilter));
				int width = sourceTexture.width / fastFilter;
				int height = sourceTexture.height / fastFilter;
				if (FastFilter > 1)
				{
					RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
					Graphics.Blit(sourceTexture, temporary, material);
					Graphics.Blit(temporary, destTexture);
					RenderTexture.ReleaseTemporary(temporary);
				}
				else
				{
					Graphics.Blit(sourceTexture, destTexture, material);
				}
			}
			else
			{
				Graphics.Blit(sourceTexture, destTexture);
			}
		}

		private void OnValidate()
		{
			ChangeRadius = Radius;
			ChangeFactor = Factor;
			ChangeFastFilter = FastFilter;
		}

		private void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (ViewJumpCtl.Instance.itemType == 603)
			{
				if (Radius > 800f)
				{
					isMoHu = true;
				}
				else if (Radius < 10f)
				{
					isMoHu = false;
				}
				if (isMoHu)
				{
					Radius -= 5f;
				}
				if (!isMoHu)
				{
					Radius += 5f;
				}
				Factor = ChangeFactor;
				FastFilter = ChangeFastFilter;
			}
			else if (ViewJumpCtl.Instance.itemType == 607 && ViewJumpCtl.Instance.modelType == 2)
			{
				if (!TiredDrive.Instance.startMohu)
				{
					GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
				}
				if (TiredDrive.Instance.startMohu)
				{
					GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = true;
					if (Radius > 400f)
					{
						isMoHu = true;
					}
					else if (Radius < 3f)
					{
						isMoHu = false;
					}
					if (isMoHu)
					{
						Radius -= 2f;
					}
					if (!isMoHu)
					{
						Radius += 1f;
					}
					Factor = ChangeFactor;
					FastFilter = ChangeFastFilter;
				}
			}
			else if (ViewJumpCtl.Instance.itemType == 615 && ViewJumpCtl.Instance.modelType == 2)
			{
				GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = true;
				if (Radius > 400f)
				{
					isMoHu = true;
				}
				else if (Radius < 3f)
				{
					isMoHu = false;
				}
				if (isMoHu)
				{
					Radius -= 2f;
				}
				if (!isMoHu)
				{
					Radius += 1f;
				}
				Factor = ChangeFactor;
				FastFilter = ChangeFastFilter;
			}
			else if (ViewJumpCtl.Instance.itemType == 607 && (ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5))
			{
				if (!ShanQuSceneCtl.Instance.yanShiMoHu)
				{
					GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = false;
				}
				if (ShanQuSceneCtl.Instance.yanShiMoHu)
				{
					GameObject.Find("Camera").GetComponent<CameraFilterPack_Blur_Movie>().enabled = true;
					if (Radius > 400f)
					{
						isMoHu = true;
					}
					else if (Radius < 3f)
					{
						isMoHu = false;
					}
					if (isMoHu)
					{
						Radius -= 2f;
					}
					if (!isMoHu)
					{
						Radius += 1f;
					}
					Factor = ChangeFactor;
					FastFilter = ChangeFastFilter;
				}
			}
			else
			{
				base.enabled = false;
			}
		}

		private void OnDisable()
		{
			if ((bool)SCMaterial)
			{
				Object.DestroyImmediate(SCMaterial);
			}
		}
	}
}
