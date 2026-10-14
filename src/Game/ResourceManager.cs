using UnityEngine;

public class ResourceManager
{
	public static RenderTexture leftMirrorTexture = null;

	public static RenderTexture rightMirrorTexture = null;

	public static RenderTexture middleMirrorTexture = null;

	public static RenderTexture rearBackTexture = null;

	public static RenderTexture topTexture = null;

	private static ResourceManager instance = new ResourceManager();

	private ResourceManager()
	{
		leftMirrorTexture = new RenderTexture(512, 512, 16);
		rightMirrorTexture = new RenderTexture(512, 512, 16);
		middleMirrorTexture = new RenderTexture(512, 512, 16);
		rearBackTexture = new RenderTexture(512, 512, 16);
		topTexture = new RenderTexture(512, 512, 16);
	}
}
