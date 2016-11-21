using UnityEngine;
using System.Collections;

public class EndBlock : MonoBehaviour {
	
	private GameController gameController;
	
	
	// Use this for initialization
	void Start () {
		gameController = FindObjectOfType<GameController>().GetComponent<GameController>();
	}
	
	void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<Connector>()){
			Connector attachedConnector = other.GetComponent<Connector>();
			if(attachedConnector.ParentHasPower){
				gameController.circuitComplete = true;
			} else {
				gameController.circuitComplete = false;
				
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<Connector>()){
			gameController.circuitComplete = false;
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
