using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LongBlock : MonoBehaviour {
	
	public int socketsTouching;
	private Plug parentPlug;
	
	public List<GameObject> socketList;
	
	// Use this for initialization
	void Start () {
		parentPlug = GetComponentInParent<Plug>();
		socketList = new List<GameObject>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(!parentPlug.isPlaced){
			if (other.GetComponent<Socket>()){
				socketsTouching++;
				socketList.Add(other.gameObject);
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(!parentPlug.isPlaced){
			
			if (other.GetComponent<Socket>()){
				socketsTouching--;
				socketList.Remove(other.gameObject);
			}
		}
	}
	
	public void ClearList(){
		socketList.Clear();
	}
	
	public GameObject CheckHigherLowerSocket(){
		//first check on x axis
		float currentRotation = parentPlug.transform.rotation.z;
		Debug.Log("currentRotation in LongBlock script is " + currentRotation);
		if(currentRotation==1f||currentRotation<-0.6f){
			Debug.Log("in the 1f or 0.6f thing");
			if(currentRotation<-0.6f){
				Debug.Log("Detected a -0.7 rotation");
			} else {
				Debug.Log("Not detecting, rotation is " + currentRotation);
				
			}
			if(socketList[0].transform.position.x < socketList[1].transform.position.x){
				//is it further to the left
				return socketList[0].gameObject;
				} else {
						if (socketList[0].transform.position.x == socketList[1].transform.position.x){
							//is it the same? then check y
							
							if (socketList[0].transform.position.y > socketList[1].transform.position.y){
								//is it the higher one?
								return socketList[0].gameObject;
								
							} else {
								//if it's not grab the other one
								return socketList[1].gameObject;
							}
				
					}
				
				
			
				else {
					//if it's not further left and not the same it must be the other one
					return socketList[1].gameObject;
				}
			}
		} else {
			Debug.Log("Not detecting, rotation is " + currentRotation);
			
			if(socketList[0].transform.position.x > socketList[1].transform.position.x){
				//is it further to the left
				return socketList[1].gameObject;
			} else {
				if (socketList[0].transform.position.x == socketList[1].transform.position.x){
							//is it the same? then check y
					
					if (socketList[0].transform.position.y < socketList[1].transform.position.y){
								//is it the higher one?
						return socketList[0].gameObject;
						
					} else {
								//if it's not grab the other one
						return socketList[1].gameObject;
					}
					
				}
				
				
				
				else {
					//if it's not further left and not the same it must be the other one
					return socketList[0].gameObject;
				}
			}
			
		}
		
		
	}
	
	
	// Update is called once per frame
	void Update () {
		if (socketList.Count>2){
			socketList.Clear();
		}
	}
}
