using System.Collections.Generic;

public class ItemInfo
{
	public int itemId = -1;

	public string itemFlag = "-1";

	public bool isPass;

	public bool isUnable;

	public float score = 100f;

	public string otherFlagInfo;

	public List<string> errorInfoList = new List<string>();

	public ItemInfo(int itemId, string itemFlag, bool isPass = false, bool isUnable = false, int score = 100, string otherFlagInfo = "")
	{
		this.itemId = itemId;
		this.itemFlag = itemFlag;
		this.isPass = isPass;
		this.isUnable = isUnable;
		this.score = score;
		this.otherFlagInfo = otherFlagInfo;
	}

	public ItemInfo()
	{
	}
}
