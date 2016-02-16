using UnityEngine;
using System.Collections;

public class RuletaJackpot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Debug.Log ("Start");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D colInfo) {
		if(colInfo.tag == "Pointer"){
			//Debug.Log ("Enter");
		}
	}

	void OnTriggerExit2D(Collider2D colInfo) {
		if(colInfo.tag == "Pointer"){
			//Debug.Log ("Exit");
		}
	}
}
