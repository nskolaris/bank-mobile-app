using UnityEngine;
using System.Collections;

public class Pelota : MonoBehaviour {

	public Vector2 max_offset = new Vector2(10f,10f);

	public float rotation_speed = 10f;

	public float movement_speed = 1f;

	//Vector2 direction = Vector2.zero;

	Vector3 original_pos;

	// Use this for initialization
	void Start () {
		original_pos = GetComponent<RectTransform> ().localPosition;
		/*direction = new Vector2 (-1f, 1f);
		GetComponent<RectTransform> ().offsetMin = new Vector2(max_offset.x,0);
		GetComponent<RectTransform> ().offsetMax = new Vector2(max_offset.x,0);*/
	}
	
	// Update is called once per frame
	void Update () {
		/*Vector2 current_offset = GetComponent<RectTransform> ().offsetMin;

		if (direction.x > 0 && max_offset.x - current_offset.x < 3) {
			direction.x = -1;
		}
		if (direction.x < 0 && max_offset.x + current_offset.x < 3) {
			direction.x = 1;
		}

		if (direction.y > 0 && max_offset.y - current_offset.y < 3) {
			direction.y = -1;
		}
		if (direction.y < 0 && max_offset.y + current_offset.y < 3) {
			direction.y = 1;
		}

		float tParam = 0;			
		if (tParam < 1) {
			tParam += Time.deltaTime * movement_speed;
			current_offset = Vector2.Lerp (current_offset, new Vector2(max_offset.x * direction.x, max_offset.y * direction.y), tParam);
		}

		GetComponent<RectTransform> ().offsetMin = current_offset;
		GetComponent<RectTransform> ().offsetMax = current_offset;*/

		/*GetComponent<RectTransform> ().Rotate (new Vector3(0,0,rotation_speed));
		Debug.Log (GetComponent<RectTransform> ().localEulerAngles);
		GetComponent<RectTransform> ().localEulerAngles.Set (0,0,GetComponent<RectTransform> ().localEulerAngles.z + rotation_speed);*/

		Debug.Log (original_pos);

		GetComponent<RectTransform> ().RotateAround (Vector3.zero, new Vector3 (0, 0, 1), 10);

	}
}
