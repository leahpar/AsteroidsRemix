using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {


	void StopMusic()
	{
		audio.Stop();
	}

	void OnEnable()  { PlayerController.OnGameOverAction += StopMusic; }
	void OnDisable() { PlayerController.OnGameOverAction -= StopMusic; }

}
