using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Logo : MonoBehaviour {
	
	Sprite spr;

	// Use this for initialization
	void Start () {
		int header_activo = int.Parse(Main.GetConfig ("header_banco_id"));
		if (header_activo == 1) {
			spr = Resources.Load<Sprite> ("img/logo-macro");
		} else {
			//spr = Resources.Load<Sprite> ("img/logo-tucuman");
			spr = Resources.Load<Sprite> ("img/logo-tucu2");
		}
		if (spr){
			GetComponent<Image> ().sprite = spr;
		} else {
			Debug.LogError("Sprite not found", this);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
