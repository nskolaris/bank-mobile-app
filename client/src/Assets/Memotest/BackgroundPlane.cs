using UnityEngine;
using System.Collections;

public class BackgroundPlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float height = Camera.main.orthographicSize * 2.0f;
		float width = height * Screen.width / Screen.height;
		gameObject.transform.localScale = new Vector3(width, height, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
