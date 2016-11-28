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
	
	private BoxCollider2D connector1boxCollider;
	private BoxCollider2D connector2boxCollider;
	
	private DraggingPlug draggingPlug;
	
	public int countOfColliders = 0;
	
	private LongBlock longBlock;
	private SFXManager sfxManager;

	// Use this for initialization
	void Start () {
		connectors = GetComponentsInChildren<Connector>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		gameController = FindObjectOfType<GameController>().GetComponent<GameController>();
		connector1boxCollider = connector1.GetComponent<BoxCollider2D>();
		connector2boxCollider = connector2.GetComponent<BoxCollider2D>();
		sfxManager = FindObjectOfType<SFXManager>().GetComponent<SFXManager>();
		
		draggingPlug = FindObjectOfType<DraggingPlug>().GetComponent<DraggingPlug>();
		
		if (isPlaced){
			Socket tempSocket;
			tempSocket = GetComponentInParent<Socket>();
			currentSocket = tempSocket.gameObject;
			Invoke("CheckConnectors",0.15f);
		}
		
		if (isLongBlock){
			longBlock = GetComponentInChildren<LongBlock>();
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
		connector1.IsTouchingPower = false;
		connector2.IsTouchingPower = false;
		connector1.connectingPlug = null;
//		connector2.connectingPlug = null;
		previousPlug = null;
		gameController.PowerDownDownstreamPlugs(this);
		transform.parent = draggingPlug.transform;
		sfxManager.PlaySocketGrabSFX();
		
		
		previousLocation = transform.position;
		//disableConnectors();
		isPowered = false;
		if (socketCollider != null){
			//	socketCollider.enabled = true;
			if (isLongBlock){
				longBlock.ClearList();
				
				if (secondSocketCollider != null){
					secondSocketCollider.enabled = true;
				}
			}
		}
		
		connector1boxCollider.enabled = false;
		connector2boxCollider.enabled = false;
	}
	
	public void MouseDrag(){
		if (nextPlug!= null){
			nextPlug = null;
		}
		// isPowered = false;
		isDragging = true;

		
		
		transform.position = Input.mousePosition;
		
		Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		
		Vector2 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		transform.position = curPosition;
	}
	
	public void MouseUp(){
		connector1boxCollider.enabled = true;
		connector2boxCollider.enabled = true;
		if (isOverSocket){
			
			Debug.Log ("Attempting to place");
			
			if(isLongBlock){
				if (longBlock.socketList.Count < 2){
					transform.position = previousLocation;
				} else {
					transform.position = longBlock.CheckHigherLowerSocket().transform.position;
					transform.parent = currentSocket.transform;
					isPlaced = true;
					gameController.CheckAllConnectors();
					CheckConnectors();
					
					
					//Invoke ("CheckConnectors", 0.15f);
					gameController.moveCount = gameController.moveCount + 1;
					}
				}
			 
			
			else{
				transform.position = currentSocket.transform.position;
				transform.parent = currentSocket.transform;
				isPlaced = true;
				gameController.CheckAllConnectors();
				Invoke ("CheckConnectors", 0.15f);
					gameController.moveCount = gameController.moveCount + 1;
				}
			

			} else {
				transform.position = previousLocation;
		}
		isDragging = false;
		sfxManager.PlaySocketPlaceSFX();
		
		gameController.CountActivePieces();
		
	}
	
	public void CheckPreviousAndNextConnectors(){
		if(!isDragging){
			if (connector1.connectingPlug != null){
				previousPlug = connector1.connectingPlug;
				connector1.connectingPlug.nextPlug = this;
				}
				
			 else {
				previousPlug = null;
			 }
			
			
			if (connector2.connectingPlug != null){
				nextPlug = connector2.connectingPlug;
			} else {
				nextPlug = null;
			}
		}
		
	}
	
	public void CheckConnectors(){

		if(!isDragging){
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
	}
	
	
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
			spriteRenderer.color = Color.white;
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
				Debug.Log("Current rotation is " + transform.localRotation);
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
