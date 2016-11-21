using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	public Text dataText;
	
	private int pieceCount;
	public Text pieceCountText;
	
	
	public bool circuitComplete;
	
	private Plug[] plugs;
	
	// Use this for initialization
	void Start () {
		dataText.text = "Test";
		plugs = FindObjectsOfType<Plug>();
		
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
			int breakPoint = 20;
			Plug nextPlug = severedPlug.nextPlug;
			while (!lastPlug){
				breakPoint--;
				if (breakPoint <= 0 ){
					Debug.Log ("Breakpoint Reached");
					break;
				}
				//				Debug.Log ("Looping at " + nextPlug.name);
				if(nextPlug.nextPlug == null){
					lastPlug = true;
					
				} 
				nextPlug.previousPlug = null;
				nextPlug.connector1.IsTouchingPower = false;
				Debug.Log ("Powering Down " + nextPlug.name);
				nextPlug.isPowered = false;
				nextPlug = nextPlug.nextPlug;

				
			}
			severedPlug.connector2.connectingPlug = null;
		}
	}
	
	public void PowerUpDownStreamPlugs(Plug reconnectedPlug){
		if(reconnectedPlug.nextPlug!= null){
			bool lastPlug = false;
			int breakPoint = 20;
			Plug nextPlug = reconnectedPlug.nextPlug;
			while (!lastPlug){
				breakPoint--;
				if (breakPoint <= 0 ){
					Debug.Log ("Breakpoint Reached");
					break;
				}
				//				Debug.Log ("Looping at " + nextPlug.name);
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
	
	
	//public void PowerDownDownstreamPlugs(Plug severedPlug){
	//	if (severedPlug.nextPlug != null){
	//		bool lastPlug = false;
			
	//		//Plug nextPlug = severedPlug.nextPlug;
	//		//while (!lastPlug||breakPoint<20){
	//		foreach (Plug thisPlug in plugs){
	//			if(thisPlug.isPlaced){
	//				Debug.Log ("Looping at " + thisPlug.name);
	//				if(thisPlug.nextPlug == null){
	//					lastPlug = true;
	//				} 
	//				thisPlug.previousPlug = null;
	//				thisPlug.connector1.IsTouchingPower = false;
	//				Debug.Log ("Powering Down " + thisPlug.name);
	//				thisPlug.isPowered = false;
	//				//thisPlug = thisPlug.nextPlug;
	//			}
				
	//		}
	//	}
	//}


	
	//public void PowerUpDownStreamPlugs(Plug reconnectedPlug){
	//	if(reconnectedPlug.nextPlug!= null){
	//		bool lastPlug = false;
	//		int breakPoint =0;
	//		Plug nextPlug = reconnectedPlug.nextPlug;
	//		foreach (Plug thisPlug in plugs){
				
				
	//			if(thisPlug.isPlaced){
					
	//				Debug.Log ("Looping at " + thisPlug.name + " Breakpoint at " + breakPoint );
	//				if(nextPlug.nextPlug == null){
	//					lastPlug = true;
	//				} 
	//				thisPlug.previousPlug = reconnectedPlug;
	//				thisPlug.connector2.IsTouchingPower = true;
	//				Debug.Log ("Powering Up " + thisPlug.name);
					
	//					thisPlug.isPowered = true;
	//				}
	//			}
	//		}
	//	}

	
	
	// Update is called once per frame
	void Update () {
	
	}
}
