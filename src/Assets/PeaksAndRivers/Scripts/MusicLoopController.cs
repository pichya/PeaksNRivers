using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoopController : MonoBehaviour
{
    public AudioSource audioSource;
	
	public AudioClip TitleScreenLoop;
	public AudioClip StartGameLoop;
	public AudioClip MainGameLoop;
	public AudioClip WinGameLoop;
	public AudioClip LoseGameLoop;
	
	public void playTitleScreenLoop(){
		audioSource.Stop ();
        audioSource.loop = true;
        audioSource.clip = TitleScreenLoop;
        audioSource.volume = 0.9f;
        audioSource.Play();
	}
	
	public void playStartGameLoop(){
		audioSource.Stop ();
        audioSource.loop = true;
        audioSource.clip = StartGameLoop;
        audioSource.volume = 0.5f;
        audioSource.Play();
	}
	
	public void playMainLoop(){
		audioSource.Stop ();
        audioSource.loop = true;
        audioSource.clip = MainGameLoop;
        audioSource.volume = 0.5f;
        audioSource.Play();
	}
	
	public void playWinLoop(){
		Debug.Log("WIN LOOP");
		audioSource.Stop ();
        audioSource.loop = true;
        audioSource.clip = WinGameLoop;
        audioSource.volume = 0.5f;
        audioSource.Play();
	}
	

}
