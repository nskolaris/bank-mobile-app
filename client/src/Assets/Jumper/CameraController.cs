using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float distance_from_player = 100f;
	public float player_follow_threshold = 1f;
	public float kill_zone_threshold = 1f;
	public float edge_threshold = 0.5f;
	
	public bool binded = false;

	//Color initialColor;
	public float color_change_rate = 0.01f;
	public float starts_appear_height = 60f;
	public float starts_appear_rate = 0.01f;

	float initial_world_bottom;

	//Map variables
	GameObject map;
	float map_z;
	float map_offset;

	void Start () {
		//initialColor = GetComponent<Camera>().backgroundColor;
	}

	void BindToPlayer(){
		Vector3 new_position = transform.position;
		new_position.z = GameManager.player.transform.position.z - distance_from_player;
		transform.position = new_position;

		float width = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width*1.5f, 0f, distance_from_player)).x;
		Vector3 boundaries_new_position = new Vector3 (0f, 0f, distance_from_player);

		/*Top Trigger*/
		BoxCollider2D top_boundary = transform.Find ("BoundariesTop").gameObject.GetComponent<BoxCollider2D> ();
		top_boundary.transform.position = boundaries_new_position;
		float height = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, Screen.height * player_follow_threshold, distance_from_player)).y;
		top_boundary.size = new Vector2(width*1.5f, height);
		top_boundary.offset = new Vector2(0f,gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f,Screen.height,distance_from_player)).y);
		top_boundary.gameObject.SetActive (true);

		/*Bottom Trigger*/
		BoxCollider2D bottom_boundary = transform.Find ("BoundariesBottom").gameObject.GetComponent<BoxCollider2D> ();
		bottom_boundary.transform.position = boundaries_new_position;
		bottom_boundary.size = new Vector2(width*1.5f, 2f);
		bottom_boundary.offset = new Vector2(0f,gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f,0f,distance_from_player)).y-1f-kill_zone_threshold);
		bottom_boundary.gameObject.SetActive (true);

		height = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, Screen.height*2f, distance_from_player)).y;
		float xPos = gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width,0f,distance_from_player)).x + 1f + edge_threshold;
		
		/*Right Trigger*/
		BoxCollider2D right_boundary = transform.Find ("BoundariesRight").gameObject.GetComponent<BoxCollider2D> ();
		right_boundary.transform.position = boundaries_new_position;
		right_boundary.size = new Vector2(6f, height*1.5f);
		right_boundary.offset = new Vector2(xPos+6f,0f);
		right_boundary.gameObject.SetActive (true);

		/*Left Trigger*/
		BoxCollider2D left_boundary = transform.Find ("BoundariesLeft").gameObject.GetComponent<BoxCollider2D> ();
		left_boundary.transform.position = boundaries_new_position;
		left_boundary.size = new Vector2(6f, height*1.5f);
		left_boundary.offset = new Vector2(-xPos-6f,0f);
		left_boundary.gameObject.SetActive (true);

		/*Stars setup*/
		/*GameObject stars = transform.Find ("Stars").gameObject;
		float stars_new_z = distance_from_player + 10;
		stars.transform.localPosition = new Vector3 (stars.transform.localPosition.x,stars.transform.localPosition.y,stars_new_z);
		Vector3 full_screen_vector = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,stars_new_z)) - GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f,0f,stars_new_z));
		var newScale = full_screen_vector.x/stars.GetComponent<Renderer>().bounds.size.x;
		stars.transform.localScale = new Vector3(newScale,newScale,1.0f);
		stars.GetComponent<Renderer>().material.color = new Color(1f,1f,1f,0f);
		stars.GetComponent<Renderer>().enabled = false;*/

		/*Map setup*/
		map = transform.Find ("Mapa").gameObject;
		map_z = distance_from_player + 10;

		Vector3 full_screen_vector = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,map_z)) - GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f,0f,map_z));
		var newScale = full_screen_vector.x/map.GetComponent<Renderer>().bounds.size.x;
		map.transform.localScale = new Vector3(newScale,newScale,1.0f);

		map_offset = (map.GetComponent<Renderer>().bounds.size.y / 2) - (GetViewHeight(map_z)/2);
		map.transform.localPosition = new Vector3 (map.transform.localPosition.x,map.transform.localPosition.y + map_offset,map_z);

		//map.GetComponent<Renderer>().material.color = new Color(1f,1f,1f,0.5f);


		initial_world_bottom = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, 0f, distance_from_player)).y;

		binded = true;
	}

	void Update () {
		if (GameManager.player != null && !binded) {
			BindToPlayer();
		}

		/*float pos_y = transform.position.y;
		float factor = pos_y * color_change_rate;
		GetComponent<Camera>().backgroundColor = new Color(initialColor.r-factor,initialColor.g-factor,initialColor.b-factor,1);*/

		/*if(transform.position.y > starts_appear_height){
			GameObject stars = transform.Find ("Stars").gameObject;
			stars.GetComponent<Renderer>().enabled = true;
			stars.GetComponent<Renderer>().material.color = new Color(1f,1f,1f,(transform.position.y - starts_appear_height)*starts_appear_rate);
		}*/

		//Map handling
		float map_new_y = ((GameManager.completed_percentage * (((map.GetComponent<Renderer>().bounds.size.y+25f)/2))) / 100);
		map.transform.localPosition = new Vector3 (map.transform.localPosition.x,-map_new_y + map_offset,map.transform.localPosition.z);
	}

	public float GetViewHeight(float to_z = -1f){
		if (to_z == -1f) {
			to_z = distance_from_player;
		}
		float screen_top = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, Screen.height, to_z)).y;
		float screen_bottom = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, 0f, to_z)).y;
		return screen_top - screen_bottom;
	}

	public float GetInitialBottom(){
		return initial_world_bottom;
	}

	public float GetBottom(float to_z){
		return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, 0f, to_z)).y;
	}

	public float GetTop(float to_z){
		return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, Screen.height, to_z)).y;
	}

	public Vector4 GetBoundaries(float to_z){
		float screen_left = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, 0f, distance_from_player)).x;
		float screen_right = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, distance_from_player)).x;
		float screen_top = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, distance_from_player)).y;
		float screen_bottom = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, 0f, distance_from_player)).y;
		return new Vector4(screen_left,screen_right,screen_top,screen_bottom);
	}

	void OnTriggerEnter2D (Collider2D colInfo) {
		/*if(colInfo.tag == "Player"){
			player = GameObject.FindGameObjectWithTag("Player");
			var box = GetComponent(BoxCollider2D) as BoxCollider2D;
			switch(gameObject.tag){
			case "TopBoundary":
				if(player.GetComponent.<Rigidbody2D>().velocity.y > 0){
					followPlayer = true;
				}
				break;
			case "BottomBoundary":
				Destroy(player.gameObject);
				_GM.gameObject.SendMessage("gameOver");
				break;
			case "LeftBoundary":
				if(player.gameObject.GetComponent.<Rigidbody2D>().velocity.x < 0){
					player.transform.position.x = -box.offset.x;
				}
				break;
			case "RightBoundary":
				if(player.gameObject.GetComponent.<Rigidbody2D>().velocity.x > 0){
					player.transform.position.x = -box.offset.x;
				}
				break;
			}
		}*/
	}
}
