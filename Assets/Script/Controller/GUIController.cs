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

	private int screenWidth;

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

	public Texture texture22;


	void Start()
	{
		screenWidth  = Screen.width;

		scoreList = new List<ScoreItem>();


		// define fontSize according to the screen resolution
		fontSize = (int)((float)screenWidth * fontSizeRatio);
		textTopLeft.fontSize = fontSize;
		textTopRight.fontSize = fontSize;
		textBottomLeft.fontSize = fontSize;
		textBottomRight.fontSize = fontSize;
	}

	void DisplayScore()
	{
		textTopLeft.text = "Score: " + scoreLabel + "\n"
			+ "Bonus: " + (int)GameController.playerCoeff2 + "%";
	}

	void DisplayLife()
	{
		textTopRight.text = System.String.Format ("{0:F1}", PlayerController.health);
	}

	void DisplayCoeff()
	{
		textBottomLeft.text = System.String.Format ("{0:F2}", GameController.globalCoeff)
                              + " / " +
							  System.String.Format ("{0:F2}", GameController.playerCoeff);
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
