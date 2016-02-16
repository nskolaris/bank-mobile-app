using UnityEngine;
using System.Collections;

public class Ruleta : MonoBehaviour {

	Vector2 mouse_pos = Vector2.zero;

	public float minimum_velocity = 100f;
	public float brake_rate = 100f;

	bool is_enabled = true;

	float spin_speed;
	float braking_accel;

	bool gonna_win;
	int state = 0;

	public Collider2D pointer;
	Collider2D jackpot;

	PopupCanvas popup_canvas;

	void Start () {
		jackpot = transform.Find("Jackpot").GetComponent<Collider2D>();
		popup_canvas = GameObject.FindGameObjectWithTag ("PopupCanvas").GetComponent<PopupCanvas> ();
		popup_canvas.gameObject.SetActive (false);
		gonna_win = Main.CalculateWin ();
		StartCoroutine (checkOK ());
	}

	IEnumerator checkOK(){
		yield return new WaitForSeconds(0);
		if (transform.position != Vector3.zero) {
			if (GameObject.FindGameObjectWithTag ("OverlayNegro") != null) {
				GameObject.FindGameObjectWithTag ("OverlayNegro").SetActive (false);
			}
		}
	}

	public void checkData(){
		Debug.Log (transform.position);
	}



	void Update () {
		if (transform.position == Vector3.zero) {
			Main.load_ruleta = true;
			Main.Home ();
		}
		/*if (Main.times_load_ruleta > 0) {
			Main.times_load_ruleta--;
			Main.load_ruleta = true;
			Main.Home ();
		}*/
		if (state == 0 || state == 3) {
			if(is_enabled){
				HandleMouseEvents();
			}else if (angularVelocity == 0 && !popup_canvas.gameObject.activeSelf) {
				HandleMouseEvents();
			}
		}else if (angularVelocity == 0 && !popup_canvas.gameObject.activeSelf) {
			HandleMouseEvents();
		}

		if (state == 1) {
			HandleAccel ();
		}
		if (state == 2) {
			CheckWinner();
		}
		HandleVel ();
	}

	bool hit_ruleta = false;

	float angularVelocity = 0f;

	void HandleMouseEvents(){
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
		
			if (hit.collider != null && hit.collider.transform == gameObject.transform) {

				hit_ruleta = true;
				float diffx = gameObject.transform.position.x - hit.point.x;
				float diffy = gameObject.transform.position.y - hit.point.y;
				Vector2 mouse_pos_actual = new Vector2 (diffx, diffy);
			
				if (mouse_pos != Vector2.zero) {
					float o = Vector2.Distance (mouse_pos, mouse_pos_actual);
					float a = Vector2.Distance (Vector2.zero, mouse_pos_actual);
					float oa = o / a;
					float angle = Mathf.Atan (oa);
					gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, gameObject.transform.localEulerAngles.z - (angle * Mathf.Rad2Deg));
					spin_speed = angle * Mathf.Rad2Deg * 50;
				}
			
				mouse_pos = mouse_pos_actual;
			}
		
		} else if (Input.GetMouseButtonUp (0)) {

			if(hit_ruleta){

				bool rotate = true;

				if (spin_speed < minimum_velocity){
					rotate = false;
					if (pointer.IsTouching (jackpot)) {
						rotate = true;
					}
				}

				if (rotate) {

					//GetComponent<Rigidbody2D> ().angularVelocity = -spin_speed;

					if(spin_speed < minimum_velocity){
						spin_speed = minimum_velocity;
					}

					angularVelocity = -spin_speed;

					float angular_distance;
				
					if (gonna_win) {
						angular_distance = gameObject.transform.localEulerAngles.z;
					} else {
						angular_distance = gameObject.transform.localEulerAngles.z + Random.Range (50f, 310f);
					}
				
					//float spin_count = Mathf.Round (-GetComponent<Rigidbody2D> ().angularVelocity / brake_rate);
					float spin_count = Mathf.Round (-angularVelocity / brake_rate);

					//braking_accel = (GetComponent<Rigidbody2D> ().angularVelocity * GetComponent<Rigidbody2D> ().angularVelocity) / (2 * (angular_distance + (spin_count * 360f)));
					braking_accel = (angularVelocity * angularVelocity) / (2 * (angular_distance + (spin_count * 360f)));
					state = 1;
					is_enabled = false;
				}
			}
		
		} else {
			mouse_pos = Vector2.zero;
		}
	}

	void HandleVel(){
		if (angularVelocity != 0) {
			gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, gameObject.transform.localEulerAngles.z + (angularVelocity*Time.deltaTime));
		}
	}

	void HandleAccel(){
		//if (gameObject.GetComponent<Rigidbody2D> ().angularVelocity < 0) {
		if (angularVelocity < 0) {
			//GetComponent<Rigidbody2D> ().angularVelocity = GetComponent<Rigidbody2D> ().angularVelocity + (braking_accel * Time.deltaTime);
			angularVelocity = angularVelocity + (braking_accel * Time.deltaTime);
		}
		//if (gameObject.GetComponent<Rigidbody2D> ().angularVelocity > 0) {
		if (angularVelocity > 0) {
			//GetComponent<Rigidbody2D> ().angularVelocity = 0;
			angularVelocity = 0;
			state = 2;
		}
	}

	void CheckWinner(){
		state = 3;
		if (pointer.IsTouching (jackpot)) {
			if(gonna_win){
				StartCoroutine(MostrarPanelFinal(true));
				Premio.Registrar();
			}else{
				StartCoroutine(MostrarPanelFinal(false));
			}
		} else {
			StartCoroutine(MostrarPanelFinal(false));
		}
	}

	IEnumerator MostrarPanelFinal(bool victoria){
		yield return new WaitForSeconds(1f);
		popup_canvas.Show ();
		popup_canvas.ShowChild ("Finalizar");
		is_enabled = false;
		if (victoria) {
			popup_canvas.ShowChild ("Ganaste");
		} else {
			popup_canvas.ShowChild ("Perdiste");
		}
	}
}
