using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScoreItem
{
	public float time;
	public int   value;

	public ScoreItem(int v)
	{
		value = v;
		time = Time.time + 1.0f;
	}
}

public class GUIController : MonoBehaviour
{

	private int screenWidth, screenHeight;

	public GUIText textTopLeft;
	public GUIText textTopRight;
	public GUIText textBottomLeft;
	public GUIText textBottomRight;

	private List<ScoreItem> scoreList;
	private int score = 0;
	private string scoreLabel;
	private float scoreTime = 0;

	public  float fontSizeRatio = 0.020f;
	private int   fontSize;


	void Start()
	{
		screenWidth  = Screen.width;
		screenHeight = Screen.height;

		scoreList = new List<ScoreItem>();


		// define fontSize according to the screen resolution
		fontSize = (int)((float)screenWidth * fontSizeRatio);
		Debug.Log(screenWidth + " " + fontSizeRatio + " " + fontSize);
		textTopLeft.fontSize = fontSize;
		textTopRight.fontSize = fontSize;
		textBottomLeft.fontSize = fontSize;
		textBottomRight.fontSize = fontSize;
		//skin.button.fontSize = fontSize;
	}

	void DisplayScore()
	{
		textTopLeft.text = scoreLabel;
	}

	void DisplayLife()
	{
		textTopRight.text = System.String.Format ("{0:F1}", PlayerController.health);
	}

	void DisplayCoeff()
	{
		textBottomLeft.text = System.String.Format ("{0:F2}", WorldController.coeff)
                              + " / " +
                              System.String.Format ("{0:F2}", WorldController.playerCoeff);
	}

	public void AddScore(int points)
	{
		scoreList.Add(new ScoreItem(points));
	}

	void FixedUpdate()
	{
		int i;
		if (scoreList.Count > 0)
		{
			scoreLabel = "";
			for (i = scoreList.Count-1; i >= 0; i--)
			{
				if (Time.time > scoreList[i].time)
				{
					score += scoreList[i].value;
					scoreList.RemoveAt(i);
				}
				else
				{
					scoreLabel = scoreLabel + " + " + scoreList[i].value.ToString();
				}
			}
			scoreLabel = score.ToString() + scoreLabel;
			scoreTime = Time.time + 1.0f;
		}
		else if (Time.time > scoreTime)
		{
			scoreLabel = score.ToString();
		}

		DisplayScore();
		DisplayLife();
		DisplayCoeff();
	}

}
