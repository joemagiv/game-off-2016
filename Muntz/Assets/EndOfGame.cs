using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour {
	
	public GameObject gameStartUI;
	
	// Use this for initialization
	void Start () {
		gameStartUI.SetActive(false);
		Invoke("GameStartUIActive",5f);
	}
	
	void GameStartUIActive(){
		gameStartUI.SetActive(true);
		
	}
	
	public void RestartGame(){
		SceneManager.LoadScene(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
