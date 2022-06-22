using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerScript : MonoBehaviour{
	[SerializeField] private AudioSource audio; 

	[Header("Main sounds")]
	[SerializeField] AudioClip clipClickOnButton;
	public void PlaySoundClickOnButton(){
		audio.PlayOneShot(clipClickOnButton);
	}
	void Awake(){ instance = this; }
	private static AudioControllerScript instance;
	public static AudioControllerScript Instance{get => instance;}
}