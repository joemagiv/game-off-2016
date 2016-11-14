using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	public Text dataText;
	
	private int pieceCount;
	public Text pieceCountText;
	
	
	public bool circuitComplete;
	
	// Use this for initialization
	void Start () {
		dataText.text = "Test";
		
	}
	
	public void CircuitComplete(){
		circuitComplete = true;
		dataText.text =  "Circuit Complete!";
	}
	
	public void CircuitBroke(){
		circuitComplete = false;
		dataText.text = "Circuit is broken";
	}
	
	public void CountActivePieces(){
		
	}
	
	public void PowerDownDownstreamPlugs(Plug severedPlug){
		if (severedPlug.nextPlug != null){
		bool lastPlug = false;
		Plug nextPlug = severedPlug.nextPlug;
		while (!lastPlug){
			Debug.Log ("Looping at " + nextPlug.name);
			if(nextPlug.nextPlug == null){
				lastPlug = true;
			} 
				nextPlug.previousPlug = null;
				nextPlug.connector1.IsTouchingPower = false;
				Debug.Log ("Powering Down " + nextPlug.name);
				nextPlug.isPowered = false;
				nextPlug = nextPlug.nextPlug;
				
			}
		}
	}
	
	public void PowerUpDownStreamPlugs(Plug reconnectedPlug){
		if(reconnectedPlug.nextPlug!= null){
			bool lastPlug = false;
			Plug nextPlug = reconnectedPlug.nextPlug;
			while (!lastPlug){
				Debug.Log ("Looping at " + nextPlug.name);
				if(nextPlug.nextPlug == null){
					lastPlug = true;
				} 
				nextPlug.previousPlug = reconnectedPlug;
				nextPlug.connector2.IsTouchingPower = true;
				Debug.Log ("Powering Up " + nextPlug.name);
				
				nextPlug.isPowered = true;
				nextPlug = nextPlug.nextPlug;
				
				}
			}
		}

	
	
	// Update is called once per frame
	void Update () {
	
	}
}
