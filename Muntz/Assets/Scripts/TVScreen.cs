using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TVScreen : MonoBehaviour {
	
	public TextMesh levelTextMesh;
	
	// Use this for initialization
	void Start () {
		levelTextMesh.text = "";
	}
	
	
	public void UpdateLevelText(){
		Scene scene = SceneManager.GetActiveScene();
		levelTextMesh.text = "Level " + (scene.buildIndex).ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
