using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfigTabs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MarkButton ("General");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowTab(string name){
		GameObject[] children = GameObject.FindGameObjectsWithTag ("ConfigTab");
		foreach (GameObject child in children) {
			child.gameObject.SetActive(false);
		}
		transform.FindChild(name).gameObject.SetActive (true);
		MarkButton (name);
	}

	void MarkButton(string name){
		GameObject status_bar = transform.parent.Find ("StatusBar").gameObject;
		Button[] buttons = status_bar.GetComponentsInChildren<Button> ();
		foreach (Button button in buttons) {
			ColorBlock cb = button.GetComponent<Button>().colors;
			cb.normalColor = Color.white;
			button.GetComponent<Button>().colors = cb;
		}
		Button btn = status_bar.transform.Find (name).gameObject.GetComponent<Button> ();
		ColorBlock cb2 = btn.colors;
		cb2.normalColor = new Color32(191,191,191,255);
		btn.GetComponent<Button>().colors = cb2;
	}
}
