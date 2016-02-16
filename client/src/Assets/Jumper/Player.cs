using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float horizontalAccelMultiplier = 20f;	
	public float jump_force = 10f;
	public float spawn_height = 5f;
	public float boost_force = 10f;
	public float max_speed = 20f;

	bool initialized = false;
	bool is_boosting = false;

	public Sprite sprite_jumping_right;
	public Sprite sprite_falling_right;
	public Sprite sprite_jumping_left;
	public Sprite sprite_falling_left;
	public Sprite sprite_boosting;

	float boost_percent = 0;
	public float boost_deplete_rate = 1f;

	GameObject gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameController");
		Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main.GetComponent<CameraController> ().binded && !initialized) {
			Initialize();
		}
		if (initialized) {
			Vector2 velocity = GetComponent<Rigidbody2D> ().velocity;

			//GetComponent<Rigidbody2D> ().AddForce(new Vector2(Input.acceleration.x * horizontalAccelMultiplier,0f));

			velocity.x = Input.acceleration.x * horizontalAccelMultiplier;

			GetComponent<Rigidbody2D> ().velocity = velocity;

			transform.rotation = Quaternion.Euler(new Vector3(0f,0f,Input.acceleration.x * -90));

			if(can_boost()){
				GetComponent<Rigidbody2D> ().AddForce(transform.up * boost_force);
			}

			if(GetComponent<Rigidbody2D> ().velocity.y > max_speed){
				GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, max_speed);
			}

			if(GetComponent<Rigidbody2D> ().velocity.x > max_speed){
				GetComponent<Rigidbody2D> ().velocity = new Vector2(max_speed, GetComponent<Rigidbody2D> ().velocity.y);
			}
			if(GetComponent<Rigidbody2D> ().velocity.x < -max_speed){
				GetComponent<Rigidbody2D> ().velocity = new Vector2(-max_speed, GetComponent<Rigidbody2D> ().velocity.y);
			}

			handleSprite();
		}
		if (can_boost()) {
			boost_percent -= Time.deltaTime * boost_deplete_rate;
		}
		updateBoostGraphic ();
	}

	void OnDestroy() {
		if(GameObject.FindGameObjectWithTag ("GameController") != null){
			gm.GetComponent<GameManager> ().GameOver (false);
		}
	}

	void Initialize(){
		float initial_height = Camera.main.gameObject.GetComponent<CameraController> ().GetInitialBottom () + spawn_height;
		Vector3 position = transform.position;
		position.y = initial_height;
		transform.position = position;
		gameObject.SetActive (true);
		initialized = true;
	}

	void handleSprite(){
		SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
		if (GetComponent<Rigidbody2D> ().velocity.y >= 0) {
			if (GetComponent<Rigidbody2D> ().velocity.x >= 0) {
				sprite_renderer.sprite =  sprite_jumping_right;
			}else{
				sprite_renderer.sprite =  sprite_jumping_left;
			}
		} else {
			if (GetComponent<Rigidbody2D> ().velocity.x >= 0) {
				sprite_renderer.sprite =  sprite_falling_right;
			}else{
				sprite_renderer.sprite =  sprite_falling_left;
			}
		}
		if (can_boost()) {
			sprite_renderer.sprite =  sprite_boosting;
		}
	}

	bool can_boost(){
		if (is_boosting && boost_percent > 0) {
			return true;
		}
		return false;
	}

	public void Jump(float block_multiplier){
		float bounce_speed = -GetComponent<Rigidbody2D>().velocity.y;
		float jump_speed;
		if(bounce_speed < jump_force){
			jump_speed = jump_force * block_multiplier;
		}else{
			jump_speed = bounce_speed * block_multiplier;
		}
		Vector3 speed = GetComponent<Rigidbody2D> ().velocity;
		speed.y = jump_speed;
		GetComponent<Rigidbody2D>().velocity = speed;
	}

	public void receivePowerUp(){
		boost_percent = 100;
	}

	void updateBoostGraphic(){
		gm.GetComponent<GameManager> ().Boost.transform.Find ("Amount").localScale = new Vector3 (boost_percent/100f,boost_percent/100f,1);
	}

	public void UseBoost(){
		if (is_boosting) {
			is_boosting = false;
		} else {
			is_boosting = true;
		}
	}
}
