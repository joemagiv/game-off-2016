using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
	public Text dataText;
	
	//Pieces
	public int pieceCount;
	public Text pieceCountText;
	public int originalLevelPieceCount;
	
	//Moves
	public int moveCount;
	public Text moveCountText;
	
	//Timer
	public bool levelStarted = false;
	public float startingTime;
	public float levelTime;
	public Text timerText;
	
	//Submission button
	public Button submitButton;
	
	//EndofGame
	public Animator scoreScreenAnimator;
	public Text scoreText;
	
	public Button nextLevelButton;
	public Button restartLevelButton;
	
	public bool circuitComplete;
	
	private bool gameOverTriggered = false;
	
	//AudioCues
	private SFXManager sfxManager;
	public AudioClip winningSound;
	public AudioClip losingSound;
	
	private Plug[] plugs;
	
	// Use this for initialization
	void Start () {
		submitButton.gameObject.SetActive(false);
		restartLevelButton.gameObject.SetActive(false);
		sfxManager = FindObjectOfType<SFXManager>().GetComponent<SFXManager>();
		pieceCountText.text = "";
		dataText.text = "";
		levelTime = startingTime;
		plugs = FindObjectsOfType<Plug>();
		nextLevelButton.interactable = false;
		//Invoke("CountActivePieces", 0.75f);
		Invoke("StartLevel", 8f);
		
	}
	
	void StartLevel(){
		CountActivePieces();
		levelStarted = true;
		submitButton.gameObject.SetActive(true);
		
	}
	

	
	public void CountActivePieces(){
		int activePieces = 0;
		if(circuitComplete){
			Plug[] plugs = FindObjectsOfType<Plug>();
			foreach (Plug activePlug in plugs){
				if(activePlug.isPowered){
					activePieces++;
				}
			}
		}
		pieceCountText.text = "Components: " + activePieces.ToString();
		
	}
	
	public int ReturnActivePieces(){
		int activePieces = 0;
		if(circuitComplete){
			Plug[] plugs = FindObjectsOfType<Plug>();
			foreach (Plug activePlug in plugs){
				if(activePlug.isPowered){
					activePieces++;
				}
			}
		}
		return activePieces;
	}
	
	public void CheckAllConnectors(){
		foreach (Plug currentPlug in plugs){
			currentPlug.CheckPreviousAndNextConnectors();
		}
	}
	
	
	public void PowerDownDownstreamPlugs(Plug severedPlug){
		if (severedPlug.nextPlug != null){
			bool lastPlug = false;
			int breakPoint = 20;
			Plug currentLoopPlug = severedPlug.nextPlug;
			while (!lastPlug){
				breakPoint--;
				currentLoopPlug.previousPlug = null;

				
				if (breakPoint <= 0 ){
					Debug.Log ("Breakpoint Reached");
					break;
				}
				//				Debug.Log ("Looping at " + nextPlug.name);
				if(currentLoopPlug.nextPlug == null){
					lastPlug = true;
					
				} 
				
				currentLoopPlug.connector1.IsTouchingPower = false;
				Debug.Log ("Powering Down " + currentLoopPlug.name);
				currentLoopPlug.isPowered = false;
				Plug previousLoopPlug = currentLoopPlug;
				currentLoopPlug = currentLoopPlug.nextPlug;
				previousLoopPlug.nextPlug = null;
				
			}
			severedPlug.connector2.connectingPlug = null;
		}
		
		//CheckAllConnectors();
	}
	
	public void PowerUpDownStreamPlugs(Plug reconnectedPlug){
		CheckAllConnectors();
		if(reconnectedPlug.nextPlug!= null){
			bool lastPlug = false;
			int breakPoint = 20;
			Plug currentLoopPlug = reconnectedPlug.nextPlug;
			while (!lastPlug){
				breakPoint--;
				if (breakPoint <= 0 ){
					Debug.Log ("Breakpoint Reached");
					break;
				}
				//				Debug.Log ("Looping at " + nextPlug.name);
				if(currentLoopPlug.nextPlug == null){
					lastPlug = true;
				} 
				currentLoopPlug.previousPlug = reconnectedPlug;
				currentLoopPlug.connector2.IsTouchingPower = true;
				Debug.Log ("Powering Up " + currentLoopPlug.name);
				
				currentLoopPlug.isPowered = true;
				currentLoopPlug = currentLoopPlug.nextPlug;
				

			}
			
		}
		
	}
	
	public void callScoreScreen(){
		levelStarted = false;
		scoreText.text = "";
		scoreScreenAnimator.SetBool("OnScreen", true);
		scoreText.text = "Time: " + levelTime.ToString("#");
		sfxManager.PlaySFX(winningSound);
		
		Invoke("AddComponentScore", 1.5f);
		
	}
	
	private void AddComponentScore(){
		scoreText.text = "Time: " + levelTime.ToString("#") + "\n - Comp: " + ReturnActivePieces().ToString() + " x2";
		Invoke("TotalScore", 1.5f);
	}
	
	private void AddMovesScore(){
		scoreText.text = "Time: " + levelTime.ToString("#") + "\n - Comp: " + ReturnActivePieces().ToString() + " x2" +
			"\n - Moves: " + moveCount.ToString();
		Invoke("TotalScore", 1.5f);
		
		
	}
	
	private void TotalScore(){
		int endScore = Mathf.FloorToInt(levelTime) - (ReturnActivePieces()*2);
		Debug.Log("Endscore is" + endScore);
		scoreText.text = "Time: " + levelTime.ToString("#") + "\n - Comp: " + ReturnActivePieces().ToString() + " x2" +
		 "\n Score: " + endScore.ToString();
		
		Invoke("EnableNextLevel", 1.5f);
		
	}
	
	private void EnableNextLevel(){
		nextLevelButton.interactable = true;
	}
	
	public void LoadNextLevel(){
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.buildIndex+1);
	}
	
	public void restartCurrentScene()
	{
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}
	
	private void GameOver(){
		levelStarted = false;
		scoreText.text = "You ran out of time!";
		scoreScreenAnimator.SetBool("OnScreen", true);
		restartLevelButton.gameObject.SetActive(true);
		sfxManager.PlaySFX(losingSound);
	}

	
	// Update is called once per frame
	void Update () {

		
		if (levelStarted){
			levelTime = levelTime - Time.deltaTime;
			timerText.text = "Time: " + levelTime.ToString("#");
			if (circuitComplete){
				dataText.text = "Circuit Complete";
				if(ReturnActivePieces() < originalLevelPieceCount){
					submitButton.interactable = true;
				} else {
					submitButton.interactable = false;
				}
			} else {
				dataText.text = "Circuit InComplete"; 
				submitButton.interactable = false;
			}
			CountActivePieces();
		} else {
			timerText.text = "";
		}
		
		if(levelTime<0){
			if(!gameOverTriggered){
				GameOver();
				gameOverTriggered = true;
			}
		}
		
		//moveCountText.text = "Moves: " + moveCount.ToString();
	}
}
