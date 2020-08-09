using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXController : MonoBehaviour
{
    public AudioSource audioSource;
	
	public AudioClip FootStep1;
	public AudioClip FootStep2;
	public AudioClip FootStep3;
	public AudioClip FootStep4;
	public AudioClip SplashStep;
	public AudioClip LadderJump;
	public AudioClip RiverFall;
	public AudioClip ButtonClick;
	
	public void PlaySoundFX(int fxNum){
		audioSource.Stop ();
		switch (fxNum){
			case 1:
			audioSource.clip = ButtonClick;
			break;
			case 2:
			audioSource.clip = FootStep1;
			break;
			case 3:
			audioSource.clip = FootStep2;
			break;
			case 4:
			audioSource.clip = FootStep3;
			break;
			case 5:
			audioSource.clip = FootStep4;
			break;
			case 6:
			audioSource.clip = SplashStep;
			break;
			case 7:
			audioSource.clip = LadderJump;
			break;
			case 8:
			audioSource.clip = RiverFall;
			break;
		}
		audioSource.Play();
	}
}
