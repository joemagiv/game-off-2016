using UnityEngine;
using System.Collections;

public class Socket : MonoBehaviour {
	
	private BoxCollider2D boxCollider;
	
	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponentInChildren<Plug>()){
			boxCollider.enabled = false;
		} else {
			boxCollider.enabled = true;
		}
	}
}
