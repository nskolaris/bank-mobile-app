using UnityEngine;
using System.Collections;

public class PopupCanvas : MonoBehaviour {

	float disappear_time = 20f;
	bool hiding = false;
	bool showing = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hiding || showing) {
			Vector3 scale = transform.Find ("BG").gameObject.GetComponent<RectTransform> ().localScale;

			float tParam = 0;
			float target_value = 0;
			if(showing){target_value = 1;}
			if (tParam < 1) {
				tParam += Time.deltaTime * disappear_time;
				scale.y = Mathf.Lerp(scale.y, target_value, tParam);
			}

			if(hiding){
				if(scale.y > 0.01){
					transform.Find("BG").gameObject.GetComponent<RectTransform>().localScale = scale;
				}else{
					transform.Find("BG").gameObject.GetComponent<RectTransform>().localScale = new Vector3(scale.x,0f,scale.z);
					hiding = false;
					HideAllChildren ();
					gameObject.SetActive(false);
				}
			}else{
				if(scale.y < 0.99){
					transform.Find("BG").gameObject.GetComponent<RectTransform>().localScale = scale;
				}else{
					transform.Find("BG").gameObject.GetComponent<RectTransform>().localScale = new Vector3(scale.x,1f,scale.z);
					showing = false;
				}
			}
		}
	}

	public void Show(){
		if (transform.Find ("BG") != null) {
			transform.Find ("BG").gameObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 0, 1);
			gameObject.SetActive (true);
			showing = true;
		}
	}

	public void Hide(){
		hiding = true;
	}

	public void ShowChild(string name){
		transform.Find ("BG").Find (name).gameObject.SetActive (true);
	}

	void HideAllChildren(){
		foreach(Transform child in transform.Find("BG"))
		{
			child.gameObject.SetActive(false);
		}
	}
}
