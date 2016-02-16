using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	bool initialized = false;

	//General
	public float level_size = 50f;
	float min_x;
	float max_x;
	float min_y;
	float max_y;

	GameObject floor;
	GameObject sun;
	public float sun_rotation_velocity = 100f;

	//Platforms
	public GameObject platform;
	public float distance_between_platforms = 1.5f;
	int platform_index = 0;
	public float initial_platform_height;

	//PowerUps
	public GameObject powerup;
	public int max_simultaneous_powerups = 2;
	public float powerup_drop_chance = 0.1f;

	//Clouds
	/*public GameObject cloud;
	public float cloud_min_z = 5f;
	public float cloud_max_z = 25f;
	public int max_clouds = 5;
	public float wind_speed = 1f;
	public float cloud_start_y = 0f;
	public float cloud_end_y = 40f;*/

	void Start () {
		floor = transform.Find ("Floor").gameObject;

		min_x = -level_size / 2;
		max_x = level_size / 2;
	}

	void Update () {
		if (Camera.main.GetComponent<CameraController> ().binded && !initialized) {
			InitializeLevel();
		}
		if (initialized) {
			ManagePlatforms ();
			//ManageClouds();
			ManagePowerUps();
		}
	}

	void InitializeLevel(){

		Camera main_camera = Camera.main;
		float camera_distance_to_player = main_camera.gameObject.GetComponent<CameraController> ().distance_from_player;

		/*General setup */


		/*Floor setup*/
		floor.GetComponent<BoxCollider2D>().size = new Vector2(main_camera.ScreenToWorldPoint(new Vector3(Screen.width*1.5f, 0f, camera_distance_to_player)).x,1f);
		floor.GetComponent<BoxCollider2D>().offset = new Vector2(0f,main_camera.ScreenToWorldPoint(new Vector3(0f,0f,camera_distance_to_player)).y-0.5f);
		
		/*Sun setup*/
		sun = transform.Find ("Sun").gameObject;
		sun.GetComponent<Rigidbody2D>().angularVelocity = sun_rotation_velocity;

		initialized = true;
	}

	void ManagePlatforms(){
		float max_platforms =  (int)Mathf.Round(Camera.main.gameObject.GetComponent<CameraController>().GetViewHeight() / distance_between_platforms);
		float initial_height = Camera.main.gameObject.GetComponent<CameraController> ().GetInitialBottom () + initial_platform_height;
		
		Platform[] platforms = GetComponentsInChildren<Platform>();
		if (platforms.Length < max_platforms) {
			for(var i = 0; i < max_platforms - platforms.Length; i++){
				Vector3 pos = new Vector3(Random.Range(min_x,max_x),initial_height+(distance_between_platforms*platform_index)-2f,0.5f);
				GameObject platform_clone = Instantiate(platform, pos, Quaternion.identity) as GameObject;
				platform_clone.transform.parent = transform;
				int blockType = Random.Range(0,100);
				if(blockType < 10){ //10% chance de que sea powered
					platform_clone.GetComponent<Platform>().block_type = 1;
				}
				platform_index++;
			}
		}
	}

	void ManagePowerUps(){
		PowerUp[] powerups = GetComponentsInChildren<PowerUp>();
		if (max_simultaneous_powerups > powerups.Length) {
			float spawn_chance = Mathf.Clamp(powerup_drop_chance, 0, 1);
			float rand = Random.Range(0f,1f);
			if(rand < spawn_chance){
				CameraController cam = Camera.main.gameObject.GetComponent<CameraController>();
				float y_pos = cam.GetTop(cam.distance_from_player);
				float x_pos = Random.Range(cam.GetBoundaries(cam.distance_from_player).x,cam.GetBoundaries(cam.distance_from_player).y);
				GameObject powerupClone = Instantiate(powerup, new Vector3(x_pos,y_pos,0f), Quaternion.identity) as GameObject;
				powerupClone.transform.parent = gameObject.transform;
			}
		}
	}

	/*void ManageClouds(){
		float camera_y = Camera.main.transform.position.y;
		if(camera_y > cloud_start_y && cloud_end_y > camera_y){
			Cloud[] clouds = GetComponentsInChildren<Cloud>();
			AddCloud(max_clouds-clouds.Length);
		}
	}

	void AddCloud(int amount){
		for(var i=0; i<amount; i++){
			float z_pos = Random.Range(cloud_min_z,cloud_max_z);
			//float x_pos = Random.Range(Camera.main.gameObject.GetComponent<CameraController>().GetBoundaries(z_pos).x,Camera.main.gameObject.GetComponent<CameraController>().GetBoundaries(z_pos).y);
			float y_pos = Random.Range(Camera.main.gameObject.GetComponent<CameraController>().GetBoundaries(z_pos).z,Camera.main.gameObject.GetComponent<CameraController>().GetBoundaries(z_pos).w); 

			GameObject cloudClone = Instantiate(cloud, new Vector3(0f,y_pos,z_pos), Quaternion.identity) as GameObject;
			cloudClone.transform.parent = gameObject.transform;
			cloudClone.GetComponent<Rigidbody2D>().velocity = new Vector3(wind_speed,0f,0f);

			//float x_pos;
			if(wind_speed > 0){
				if(y_pos < mainCam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, Screen.height*2, z_pos)).y){
					x_pos = minX-cloudClone.GetComponent.<Renderer>().bounds.extents.x;
				}else{
					posX = Random.Range(minX-cloudClone.GetComponent.<Renderer>().bounds.extents.x,maxX-((maxX-minX)/2));
				}
			}else{
				if(y_pos < mainCam.GetComponent.<Camera>().ScreenToWorldPoint(new Vector3(0f, Screen.height*2, 10f)).y){
					x_pos = maxX+cloudClone.GetComponent.<Renderer>().bounds.extents.x;
				}else{
					posX = Random.Range(minX+((maxX-minX)/2),maxX+cloudClone.GetComponent.<Renderer>().bounds.extents.x);
				}
			}
			//cloudClone.transform.position.x = x_pos;
		}
	}*/
}
