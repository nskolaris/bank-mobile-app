using UnityEngine;
using System.Collections;

public class RuletaScreen : MonoBehaviour {

	public GameObject ruleta_prefab;

	// Use this for initialization
	void Start () {
		StartCoroutine (StartRuleta ());
	}

	IEnumerator StartRuleta(){
		yield return new WaitForSeconds(0);
		Instantiate (ruleta_prefab);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
