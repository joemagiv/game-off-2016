using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {
	
	public AudioClip mainGameMusic;
	
	private AudioSource audioSource;
	
	
	private static bool created = false;
	
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		
	}
	
	public void StartMainGameMusic(){
		audioSource.clip = mainGameMusic;
		audioSource.volume = 0.75f;
		audioSource.Play();
		audioSource.loop = true;
	}
	
	void Awake() {
		if (!created) {
			DontDestroyOnLoad (transform.gameObject);
			created = true;
		} else {
			//destroys clones
			Destroy (this.gameObject);
		}
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
	void OnSceneLoaded(Scene scene, LoadSceneMode m){

	}
}
