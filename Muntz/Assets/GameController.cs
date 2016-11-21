using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	public Text dataText;
	
	private int pieceCount;
	public Text pieceCountText;
	
	public int moveCount;
	public Text moveCountText;
	
	public float levelTime;
	
	public bool circuitComplete;
	
	private Plug[] plugs;
	
	// Use this for initialization
	void Start () {
		dataText.text = "Test";
		plugs = FindObjectsOfType<Plug>();
		Invoke("CountActivePieces", 0.75f);
		
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
	
	


	
	
	// Update is called once per frame
	void Update () {
		if (circuitComplete){
			dataText.text = "Circuit Complete";
		} else {
			dataText.text = "Circuit InComplete"; 
		}
		CountActivePieces();
	}
}
