using UnityEngine;
using System.Collections;

public class Plug : MonoBehaviour {
	
	public bool isOverSocket;
	public GameObject currentSocket;
	private BoxCollider2D socketCollider;
	
	public GameObject secondSocket;
	private BoxCollider2D secondSocketCollider;
	
	public bool isLongBlock;
	public bool isLBlock;
	
	public bool isPowered;
	
	public bool isPlaced;
	
	public bool isDragging = false;
	
	private Vector2 previousLocation;
	
	private Connector[] connectors;
	
	private SpriteRenderer spriteRenderer;
	private GameController gameController;
	
	public Plug previousPlug;
	public Plug nextPlug;
	
	public Connector connector1;
	public Connector connector2;
	
	public int countOfColliders = 0;

	// Use this for initialization
	void Start () {
		connectors = GetComponentsInChildren<Connector>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		gameController = FindObjectOfType<GameController>().GetComponent<GameController>();
		
		if (isPlaced){
			Socket tempSocket;
			tempSocket = GetComponentInParent<Socket>();
			currentSocket = tempSocket.gameObject;
			Invoke("CheckConnectors",0.25f);
		}
	}
	
	private void disableConnectors(){
		foreach (Connector connector in connectors){
			connector.DisableBoxCollider();
		}
	}
	
	private void enableConnectors(){
		foreach (Connector connector in connectors){
			connector.EnableBoxCollider();
		}
	}
	
	public void MouseDown(){
		isPlaced = false;
		isPowered = false;
		connector1.IsTouchingPower = false;
		connector2.IsTouchingPower = false;
		connector1.connectingPlug = null;
//		connector2.connectingPlug = null;
		previousPlug = null;
		
		previousLocation = transform.position;
		//disableConnectors();
		isPowered = false;
		if (socketCollider != null){
			//	socketCollider.enabled = true;
			if (isLongBlock){
				if (secondSocketCollider != null){
					secondSocketCollider.enabled = true;
				}
			}
		}
	}
	
	public void MouseDrag(){
		// isPowered = false;
		isDragging = true;
		
		
		transform.position = Input.mousePosition;
		
		Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		
		Vector2 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		transform.position = curPosition;
	}
	
	public void MouseUp(){
		if (isOverSocket){
			Debug.Log ("Attempting to place");
			transform.position = currentSocket.transform.position;
			//socketCollider.enabled = false;
			transform.parent = currentSocket.transform;
			isPlaced = true;
			Invoke ("CheckConnectors", 0.25f);
			

		} else {
			transform.position = previousLocation;
		}
		
		isDragging = false;
		//enableConnectors();
		
	}
	
	public void CheckConnectors(){
		Debug.Log("Checking connectors for " + transform.name);
		if (connector1.connectingPlug != null){
			previousPlug = connector1.connectingPlug;
			connector1.connectingPlug.nextPlug = this;
			if (previousPlug.isPowered){
				isPowered = true;
			}
			
		} else {
			previousPlug = null;
		}
		if (connector2.connectingPlug != null){
			nextPlug = connector2.connectingPlug;
			if (isPowered){
				gameController.PowerUpDownStreamPlugs(this);
			} else {
				gameController.PowerDownDownstreamPlugs(this);
			}
		} else {
			nextPlug = null;
		}
	}
	
	
	//public bool checkPowered(){
	//	if (isPowered){
	//		return true;
	//	} else {
	//		return false;
	//	}
	//}
	
	//public bool checkPlaced(){
	//	if (isPlaced){
	//		return true;
	//	} else {
	//		return false;
	//	}
	//}
	
	void OnTriggerEnter2D(Collider2D other){
		//		Debug.Log ("Detected a Trigger: " + other);
		
		if (other.tag == "Socket"){
			isOverSocket = true;
			currentSocket = other.gameObject;
			socketCollider = other.gameObject.GetComponent<BoxCollider2D>();
		}
	}
	
	void OnTriggerStay2D(Collider2D other){
		if (isLongBlock){
			if (isPlaced){
				if (other.tag == "Socket"){
					isOverSocket = true;
					secondSocket = other.gameObject;
					secondSocketCollider = other.gameObject.GetComponent<BoxCollider2D>();
					secondSocketCollider.enabled =false;
				}
			}
		}
		if (other.tag == "Socket"){
			isOverSocket = true;
			currentSocket = other.gameObject;
			socketCollider = other.gameObject.GetComponent<BoxCollider2D>();
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		
		
		if (other.tag == "Socket"){
			isOverSocket = false;
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		if (isPowered){
			spriteRenderer.color = Color.yellow;
			foreach (Connector connector in connectors){
				connector.ParentHasPower = true;
			}
		} else {
			spriteRenderer.color = Color.red;
			
		}
		if (isDragging){
			if(Input.GetButtonDown("Jump")||Input.GetKeyDown(KeyCode.LeftShift)){
					transform.Rotate(0,0,90);
				//float currentRotation = transform.localRotation.z;
				//	Debug.Log("Current rotation is " + currentRotation);
				//if (isLBlock){
				//	if(currentRotation == 1||currentRotation==0){
				//	Debug.Log ("Flipping connectors");	
				//	Connector tempConnector = connector2;
				//	connector2 = connector1;
				//	connector1 = tempConnector;
				//	}
				//}
				//else {
				//		Connector tempConnector = connector2;
				//		connector2 = connector1;
				//		connector1 = tempConnector;
				//	}
				//}
			}
		}

	

		
		//if (isPlaced){
		//	bool connectedToPower = false;
		//	foreach (Connector connector in connectors){
		//		if (connector.IsTouchingPower){
		//			Debug.Log ("connector " + connector.name + " is powered");
		//				connectedToPower = true;
		//				break;
		//			}
		//			if (!connectedToPower){
		//				isPowered = false;
		//				//	Debug.Log ("Lost connection, disabling power for " + transform.name);
		//			} else {
		//				isPowered = true;
				
		//			}
		//	}
			
			
			
			//if (countOfColliders == 0){
			//	isPowered = false;
			//} 
			//if (countOfColliders == 1||countOfColliders == 2){
			//	bool connectedToPower = false;
			//	foreach (Connector connector in connectors){
			//		if (connector.IsTouchingPower){
			//			connectedToPower = true;
			//			break;
			//		}
			//		if (!connectedToPower){
			//			isPowered = false;
			//			Debug.Log ("Lost connection, disabling power for " + transform.name);
			//		} else {
			//			isPowered = true;
						
			//		}
			//	}
			//}
	//}
	}
}
