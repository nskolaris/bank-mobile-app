using UnityEngine;
using System;

public class PanelMovement : MonoBehaviour {

	//int movement_state = 0;

	float time;
	//Vector3 target_position;
	Vector2 target_offset;
	Action callback;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	/*void Update () {
		Vector3 new_position = transform.localPosition;
		float distance = Vector3.Distance (new_position, target_position);
		if (distance != 0) {
			if(distance < 0.1 && Vector3.Distance(new_position, target_position) > -0.1){
				new_position = target_position;
				if(callback != null){
					callback();
				}
			}
			float tParam = 0;			
			if (tParam < 1) {
				tParam += Time.deltaTime * time;
				new_position = Vector3.Lerp (new_position, target_position, tParam);
			}
			transform.localPosition = new_position;
		}
	}*/

	void Update () {
		Vector2 new_offset = GetComponent<RectTransform> ().offsetMin;
		float distance = Vector2.Distance (new_offset, target_offset);

		if (distance != 0) {
			if(distance < 0.1 && distance > -0.1){
				new_offset = target_offset;
				if(callback != null){
					callback();
				}
			}
			float tParam = 0;			
			if (tParam < 1) {
				tParam += Time.deltaTime * time;
				new_offset = Vector2.Lerp (new_offset, target_offset, tParam);
			}
			GetComponent<RectTransform> ().offsetMin = new_offset;
			GetComponent<RectTransform> ().offsetMax = new_offset;
		}
	}

	/*public void move(Vector3 target, float seconds, Action callback_fn = null){
		target_position = target;
		time = seconds;
		callback = callback_fn;
	}*/

	public void move(Vector2 offset, float seconds, Action callback_fn = null){
		target_offset = offset;
		time = seconds;
		callback = callback_fn;
	}

	public void Offset(Vector2 rel_pos){
		GetComponent<RectTransform> ().offsetMin = rel_pos;
		GetComponent<RectTransform> ().offsetMax = rel_pos;
	}
}
