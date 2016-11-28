using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {
	
	private AudioSource audioSource;
	
	public AudioClip socketGrab;
	public AudioClip socketPlace;
	
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	public void PlaySFX(AudioClip activeClip){
		audioSource.clip = activeClip;
		audioSource.Play();
	}
	
	public void PlaySocketGrabSFX(){
		audioSource.clip = socketGrab;
		audioSource.Play();
		
	}
	
	public void PlaySocketPlaceSFX(){
		audioSource.clip = socketPlace;
		audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
