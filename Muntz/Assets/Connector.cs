using UnityEngine;
using System.Collections;

public class Connector : MonoBehaviour {
	
	private Plug parentPlug;
	private BoxCollider2D boxCollider;
	private GameController gameController;
	
	public bool ParentHasPower = false;
	
	public bool IsTouchingPower = false;
	
	public Connector siblingConnector;
	
	public Plug connectingPlug;

	
	// Use this for initialization
	void Start () {
		parentPlug = GetComponentInParent<Plug>();
		boxCollider = GetComponent<BoxCollider2D>();
		gameController = FindObjectOfType<GameController>().GetComponent<GameController>();
		
	
	}
	
	public void DisableBoxCollider(){
		boxCollider.enabled = false;
	}
	
	public void EnableBoxCollider(){
		boxCollider.enabled = true;
	}
	
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Connector"||other.tag == "Start"){
			parentPlug.countOfColliders++;
		}
		//if (other.GetComponentInParent<Plug>()){
		//	Plug otherPlug = other.GetComponentInParent<Plug>();
		//	if (otherPlug.isPlaced){
		//		if (otherPlug.isPowered){
		//			parentPlug.isPowered = true;
		//		}
		//	}
		//}
	}
	
	void OnTriggerStay2D(Collider2D other){
		if (parentPlug.isPlaced){
			if (other.GetComponent<Connector>()){
				connectingPlug = other.GetComponentInParent<Plug>();

				//Connector otherConnector = other.GetComponent<Connector>();
				//if (otherConnector.ParentHasPower){
				//		Debug.Log(other.name + " is powered, powering" + parentPlug.name);
				//		parentPlug.isPowered = true;
				//		otherConnector.IsTouchingPower = true;
				//		IsTouchingPower = true;
				//	} else {
				//		Debug.Log(other.transform.parent.name + " is not powered, disabling" + parentPlug.name);
				//		parentPlug.isPowered = false;
				//		IsTouchingPower = false;
				//	}
				//}
			}
			if (other.tag == "Start"){
				parentPlug.isPowered = true;
				IsTouchingPower = true;
			}
			if (other.tag == "End"){
				if (parentPlug.isPowered){
					gameController.CircuitComplete();
				}
			}
			
		}
	}

	
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Connector"||other.tag == "Start"){
			parentPlug.countOfColliders--;
		}
		
		if (other.tag == "Start"){
			parentPlug.isPowered = false;
			IsTouchingPower = false;
		}
		if (other.tag == "End"){
			if (parentPlug.isPowered){
				gameController.CircuitBroke();
				parentPlug.isPowered = false;
				IsTouchingPower = false;
			}
		} 
		//if (other.GetComponentInParent<Connector>()){
		//	if (IsTouchingPower){
		//		parentPlug.isPowered = false;
		//		ParentHasPower =false;
		//		IsTouchingPower = false;
				
		//		//siblingConnector.ParentHasPower = false;
		//		//	if (siblingConnector.connectingPlug != null){
		//		//		siblingConnector.connectingPlug.isPowered = false;
		//		//	}
		//		//}
		//	}
		//	gameController.PowerDownDownstreamPlugs(parentPlug);
		//}
	}

		
	
	// Update is called once per frame
	void Update () {
		//if (IsTouchingPower){
		//	parentPlug.isPowered = true;
		//}
	}
}
