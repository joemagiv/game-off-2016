using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	public Text dataText;
	
	//Pieces
	private int pieceCount;
	public Text pieceCountText;
	
	//Moves
	public int moveCount;
	public Text moveCountText;
	
	//Timer
	public bool levelStarted = false;
	public float startingTime;
	public float levelTime;
	public Text timerText;
	
	
	public bool circuitComplete;
	
	
	private Plug[] plugs;
	
	// Use this for initialization
	void Start () {
		dataText.text = "Test";
		levelTime = startingTime;
		plugs = FindObjectsOfType<Plug>();
		Invoke("CountActivePieces", 0.75f);
		Invoke("StartLevel", 2f);
		
	}
	
	void StartLevel(){
		levelStarted = true;
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
	
	


	
	
	// Update is called once per frame
	void Update () {
		if (circuitComplete){
			dataText.text = "Circuit Complete";
		} else {
			dataText.text = "Circuit InComplete"; 
		}
		CountActivePieces();
		
		if (levelStarted){
			levelTime = levelTime - Time.deltaTime;
			timerText.text = "Time: " + levelTime.ToString("#");
		} else {
			timerText.text = "";
		}
		
		moveCountText.text = "Moves: " + moveCount.ToString();
	}
}
