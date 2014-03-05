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

public class HUDController : MonoBehaviour
{
	public GUIText textTopLeft;
	public GUIText textTopRight;
	public GUIText textBottomLeft;
	public GUIText textBottomRight;
	public GUIText textBottomCenter;
	public GUIText textTitle;

	private MenuController menu;
	
	// global
	public  float fontSizeRatio = 0.020f;
	private int   fontSize;

	private List<ScoreItem> scoreList;
	private int score = 0;
	private string scoreLabel;
	private float scoreTime = 0;


	void Start()
	{
		scoreList = new List<ScoreItem>();

		menu = gameObject.GetComponent<MenuController>();

		// define fontSize according to the screen resolution
		fontSize = (int)((float)Screen.width * fontSizeRatio);
		textTopLeft.fontSize     = fontSize;
		textTopRight.fontSize    = fontSize;
		textBottomLeft.fontSize  = fontSize;
		textBottomRight.fontSize = fontSize;
		textBottomCenter.fontSize = fontSize;

		textTitle.fontSize = (int)(fontSize * 3.0f);
		textTitle.text = DataController.Title;
	}

	void DisplayTitle()
	{
		textTitle.text = DataController.Title;
	}

	void DisplayGameOver()
	{
		textTitle.text = "Game Over";
	}

	void DisplayScore()
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
					scoreLabel = scoreLabel + " + " + scoreList[i].value.ToString("0,0");
				}
			}
			scoreLabel = score.ToString("0,0") + scoreLabel;
			scoreTime = Time.time + 1.0f;
		}
		else if (Time.time > scoreTime)
		{
			scoreLabel = score.ToString("0,0");
		}

		textTopLeft.text = "Score: " + scoreLabel + "\n"
			+ "Bonus: " + (int)GameController.playerCoeff2 + "%";
	}

	void DisplayPoints()
	{
		textTopLeft.text = "Points: " + DataController.Points.ToString("0,0");
	}

	void DisplayLife()
	{
		textTopRight.text = "Life: " + System.String.Format ("{0:F0}", PlayerController.health);
	}

	void DisplayCoeff()
	{
		textBottomLeft.text = System.String.Format ("{0:F2}", GameController.globalCoeff)
                              + " / " +
							  System.String.Format ("{0:F2}", GameController.playerCoeff);
	}
	

	void DisplayVersion()
	{
		textBottomCenter.text = "v" + DataController.Version.ToString();
	}

	public void AddScore(int points)
	{
		scoreList.Add(new ScoreItem(points));
	}

	void ResetDisplay()
	{
		textTopLeft.text     = "";
		textTopRight.text    = "";
		textBottomLeft.text  = "";
		textBottomRight.text = "";
		textBottomCenter.text = "";
		textTitle.text       = "";
	}

	public void ResetScore()
	{
		scoreList.Clear();
		score = 0;
	}

	void FixedUpdate()
	{
		ResetDisplay();
		switch (menu.state)
		{
		case MenuController.MENU_STATE_STOP:
			DisplayTitle();
			DisplayVersion();
			if (DataController.Points > 0) DisplayPoints();
			// reset current score
			break;
		case MenuController.MENU_STATE_OVER:
			DisplayGameOver();
			break;
		case MenuController.MENU_STATE_OPTS:
			break;
		case MenuController.MENU_STATE_UPGR:
			DisplayPoints();
			break;
		case MenuController.MENU_STATE_RUN:
		case MenuController.MENU_STATE_PAUSE:
			DisplayScore();
			DisplayLife();
			DisplayCoeff();
			break;
		}
	}

}
