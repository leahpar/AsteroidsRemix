using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public int score = 100;
	private GameController gameController;

	void Start()
	{
		gameController = GameObject.FindWithTag("GameController").gameObject.GetComponent<GameController>();
	}

	void OnDeath()
	{
		gameController.AddScore(score);
	}
	
}
