using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ficha : MonoBehaviour {

	public int group;
	public Sprite default_image;
	public bool showing = false;

	float rotation_speed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.localEulerAngles = new Vector3 (0, gameObject.transform.localEulerAngles.y + rotation_speed, 0);

		if (rotation_speed > 0) {
			if (gameObject.transform.localEulerAngles.y > 90) {
				rotation_speed = -rotation_speed;

				if (showing) {
					ShowPicture ();
				} else {
					ShowDefaultPicture ();
				}
			}
		} else if (rotation_speed < 0) {
			if(gameObject.transform.localEulerAngles.y > 180){

				rotation_speed = 0;
				gameObject.transform.localEulerAngles = new Vector3 (0, 0, 0);

				if(showing){
					gameObject.GetComponentInParent<Memotest> ().reportShow (group);
				}
			}
		}
	}

	public void HandleClick(){
		if (!showing) {
			if (gameObject.GetComponentInParent<Memotest> ().allow_show) {
				Show();
			}
		}
	}

	void Show(){
		if (!showing) {
			showing = true;
			rotation_speed = 15;
		}
	}

	public void Hide(){
		if (showing) {
			showing = false;
			rotation_speed = 15;
		}
	}

	void ShowPicture(){
		Sprite sprite =  Resources.Load <Sprite>("Memotest/"+group.ToString());
		if (sprite){
			gameObject.GetComponent<Image> ().sprite = sprite;
		} else {
			Debug.LogError("Sprite not found", this);
		}
	}

	void ShowDefaultPicture(){
		gameObject.GetComponent<Image> ().sprite = default_image;
	}
}