using System;

[Serializable]
public class QuestionBody
{
	public int id;

	public byte km;

	public byte cx;

	public byte zj;

	public string text;

	public string[] answers;

	public string details;

	public byte rightAnswer;

	public byte type;

	public bool hasImage;

	public bool ifAnswer;

	public bool answerRight;

	public byte RecordSelectAnswer;
}
