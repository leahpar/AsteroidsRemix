using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{

	void Start()
	{
		//StartMusic();
	}

	public void StartMusic()
	{
		if (audio && (DataController.OptMusic == 1) && !audio.isPlaying) audio.Play();
	}

	public void StopMusic()
	{
		if (audio) audio.Stop();
	}

	void ToogleMusic()
	{
		if (DataController.OptMusic == 0)
		{
			DataController.OptMusic = 1;
			if (audio) audio.Play();
		}
		else
		{
			DataController.OptMusic = 0;
			if (audio) audio.Stop();
		}
	}

	void ToogleSound()
	{
		if (DataController.OptSound == 0)
		{
			DataController.OptSound = 1;
		}
		else
		{
			DataController.OptSound = 0;
		}
	}


	void OnEnable() 
	{
		MenuController.OnMusicToggle      += ToogleMusic;
		MenuController.OnSoundToggle      += ToogleSound;
	}

	void OnDisable()
	{
		MenuController.OnMusicToggle      -= ToogleMusic;
		MenuController.OnSoundToggle      -= ToogleSound;
	}

}
