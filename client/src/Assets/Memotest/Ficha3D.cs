using UnityEngine;
using System.Collections;

public class Ficha3D : MonoBehaviour {

	public int group;
	int conjunto_id; 
	public Texture2D default_image;
	public bool showing = false;

	public float default_rotation_speed = 10;
	float rotation_speed = 0;

	void Start () {
		GameObject front = gameObject.transform.Find ("Front").gameObject;
		front.GetComponent<Renderer> ().material.mainTexture = default_image;

		GameObject bottom = gameObject.transform.Find ("Bottom").gameObject;
		bottom.GetComponent<Renderer> ().material.color = new Color32(29, 147, 255, 255);
		GameObject left = gameObject.transform.Find ("Left").gameObject;
		left.GetComponent<Renderer> ().material.color = new Color32(29, 147, 255, 255);
		GameObject top = gameObject.transform.Find ("Top").gameObject;
		top.GetComponent<Renderer> ().material.color = new Color32(29, 147, 255, 255);
		GameObject right = gameObject.transform.Find ("Right").gameObject;
		right.GetComponent<Renderer> ().material.color = new Color32(29, 147, 255, 255);
		
	}

	public void SetGroup(int group_id){
		group = group_id;
		conjunto_id = int.Parse(Main.GetConfig("memotest_grupo_id"));
		GameObject back = gameObject.transform.Find ("Back").gameObject;
		Texture2D tex = Resources.Load("Memotest/grupo-"+conjunto_id+"/"+group.ToString()) as Texture2D;
		if (tex){
			back.GetComponent<Renderer> ().material.mainTexture = tex;
		} else {
			Debug.LogError("Sprite not found", this);
		}
	}

	void Update () {
		if (rotation_speed > 0) {
			gameObject.transform.localEulerAngles = new Vector3 (0, gameObject.transform.localEulerAngles.y + rotation_speed, 0);
			if(showing){
				if (gameObject.transform.localEulerAngles.y > 180) {
					rotation_speed = 0;
					gameObject.transform.localEulerAngles = new Vector3 (0, 180, 0);
				}
			}else{
				if (gameObject.transform.localEulerAngles.y < 180) {
					rotation_speed = 0;
					gameObject.transform.localEulerAngles = new Vector3 (0, 0, 0);
				}
			}
		}
	}

	void OnMouseDown(){
		if (!showing) {
			if (gameObject.GetComponentInParent<Memotest> ().allow_show && !gameObject.GetComponentInParent<Memotest> ().paused) {
				Show();
			}
		}
	}

	void Show(){
		if (!showing) {
			gameObject.GetComponentInParent<Memotest> ().reportShow (group);
			showing = true;
			rotation_speed = default_rotation_speed;
		}
	}
	
	public void Hide(){
		if (showing) {
			showing = false;
			rotation_speed = default_rotation_speed;
		}
	}
}
