using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
	public GUISkin skin;

	// buttons
	public float buttonXMargin, buttonYMargin;
	public float buttonXSize, buttonYSize;

	public float fontSizeRatio = 0.02f;

	public int state = 0;
	private int back = 0;
	public const int MENU_STATE_STOP  = 0;
	public const int MENU_STATE_RUN   = 1;
	public const int MENU_STATE_PAUSE = 2;
	public const int MENU_STATE_OVER  = 3;
	public const int MENU_STATE_OPTS  = 4;
	public const int MENU_STATE_UPGR  = 5;


	/* EVENTS */

	public delegate void musicToggle();
	public static event musicToggle OnMusicToggle;

	public delegate void soundToggle();
	public static event soundToggle OnSoundToggle;

	/* END EVENTS */

	private GameController gameController;
	private CaracController carac;

	void Start ()
	{
		gameController = gameObject.GetComponent<GameController>();
		carac = gameObject.GetComponent<CaracController>();
		

		// define fontSize according to the screen resolution
		skin.button.fontSize = (int)((float)Screen.width * fontSizeRatio);
		skin.box.fontSize = (int)((float)Screen.width * fontSizeRatio);

	}

	void OnGUI()
	{
		GUI.skin = skin;

		switch (state)
		{
		case MENU_STATE_STOP:
			if (AddButton(1, 2, "Start"))
			{
				gameController.StartGame();
				state = MENU_STATE_RUN;
			}
			if (AddButton(2, 2, "Upgrades"))
			{
				state = MENU_STATE_UPGR;
			}
			if (AddButton(3, 2, "Options"))
			{
				state = MENU_STATE_OPTS;
				back = MENU_STATE_STOP;
			}
#if (!UNITY_WEBPLAYER)
			if (AddButton(2, 3, "Quit"))
			{
				Application.Quit();
			}
#endif
			break;
		case MENU_STATE_RUN:
			if (AddButton2("||"))
			{
				Time.timeScale = 0;
				state = MENU_STATE_PAUSE;
			}
			break;
		case MENU_STATE_PAUSE:
			if (AddButton(2, 1, "Resume"))
			{
				Time.timeScale = 1;
				state = MENU_STATE_RUN;
			}
			if (AddButton(2, 2, "Options"))
			{
				state = MENU_STATE_OPTS;
				back = MENU_STATE_PAUSE;
			}
			if (AddButton(2, 3, "End game"))
			{
				gameController.GameOver();
				state = MENU_STATE_OVER;
			}

			break;
		case MENU_STATE_OVER:
			//AddText(2, 1, "<b>GAME OVER</b>");
			AddText(1, 2, "<b>Score</b>\n" + gameController.score.ToString("0,0"));
			AddText(2, 2, "<b>Total points</b>\n" + DataController.Points.ToString("0,0"));
			AddText(3, 2, "<b>Time</b>\n" + gameController.timePlayed + " sec");

			if (AddButton(2, 3, "Main menu"))
			{
				state = MENU_STATE_STOP;
			}
			break;
		case MENU_STATE_OPTS:
			if (AddButton(2, 1, "Music : " + (DataController.OptMusic == 0 ? "OFF" : "ON")))
			{
				OnMusicToggle();
			}
			if (AddButton(2, 2, "Sound : " + (DataController.OptSound == 0 ? "OFF" : "ON")))
			{
				OnSoundToggle();
			}
			if (AddButton(2, 3, "Back"))
			{
				state = back;
			}
			//AddTextArea(20, 20, 60, 60, DataController.GetStatistics());
			break;
		case MENU_STATE_UPGR:
			if (AddButton(1, 1, carac.GetLabelLife()))
			{
				carac.UpdateLife();
			}
			if (AddButton(2, 1, carac.GetLabelRegen()))
			{
				carac.UpdateRegen();
			}
			if (AddButton(3, 1, carac.GetLabelBonus()))
			{
				carac.UpdateBonus();
			}
			if (AddButton(1, 2, carac.GetLabelDamage()))
			{
				carac.UpdateDamage();
			}
			if (AddButton(2, 2, carac.GetLabelFireRate()))
			{
				carac.UpdateFireRate();
			}
			if (DataController.UpShot < 11)
			{
				if (AddButton(3, 2, carac.GetLabelExtraShot())) carac.UpdateShot();
			}
			else
			{
				if (AddButton(3, 2, carac.GetLabelExtraShot2())) {};
			}
			if (AddButton(2, 3, "Back"))
			{
				state = MENU_STATE_STOP;
			}
			break;
		}
	}

	bool AddButton(int x, int y, string label)
	{
		int px = (int)(((x-1)*buttonXSize + x*buttonXMargin) / 100.0f * (float)Screen.width);
		int py = (int)(((y-1)*buttonYSize + y*buttonYMargin) / 100.0f * (float)Screen.height);
		int dx = (int)(buttonXSize / 100.0f * (float)Screen.width);
		int dy = (int)(buttonYSize / 100.0f * (float)Screen.height);
		return GUI.Button(new Rect(px, py, dx, dy), label);
	}

	bool AddButton2(string label)
	{
		int px = (int)(0.47f * (float)Screen.width);
		int py = (int)(0.05f * (float)Screen.height);
		int dx = (int)(0.06f * (float)Screen.width);
		int dy = (int)(0.10f * (float)Screen.height);
		return GUI.Button(new Rect(px, py, dx, dy), label);
	}

	void AddText(int x, int y, string label)
	{
		int px = (int)(((x-1)*buttonXSize + x*buttonXMargin) / 100.0f * (float)Screen.width);
		int py = (int)(((y-1)*buttonYSize + y*buttonYMargin) / 100.0f * (float)Screen.height);
		int dx = (int)(buttonXSize / 100.0f * (float)Screen.width);
		int dy = (int)(buttonYSize / 100.0f * (float)Screen.height);
		GUI.Box(new Rect(px, py, dx, dy), label);
	}

	void AddTextArea(float x, float y, float w, float h, string label)
	{
		int px = (int)(x/100.0f * (float)Screen.width);
		int py = (int)(y/100.0f * (float)Screen.height);
		int dx = (int)(w/100.0f * (float)Screen.width);
		int dy = (int)(h/100.0f * (float)Screen.height);
		GUI.Box(new Rect(px, py, dx, dy), label);
	}


}





