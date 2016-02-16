using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public PopupCanvas popup_canvas;

	public bool player_won = false;

	//Player
	public GameObject player_prefab;
	public static GameObject player;
	public GameObject Boost;

	public void SpawnPlayer(){
		Vector3 pos = Vector3.zero;
		Quaternion rot = Quaternion.identity;
		player = Instantiate(player_prefab, pos, rot) as GameObject;
	}

	public void UseBoost(){
		player.GetComponent<Player> ().UseBoost ();
	}

	//Points
	public GameObject point_display;
	float latitud = 3779f;
	public static float level_height = 300f;
	public static float completed_percentage = 0f;
	int points;

	void UpdateScore(){
		int current_points = (int) Mathf.Round(player.transform.position.y);
		if (current_points > points) {
			points = current_points;
			completed_percentage = ((points*100)/level_height);
			float points_to_display = latitud - ((points*latitud)/level_height);
			if(points_to_display <= 0){
				points_to_display = 0;
				GameOver(true);
			}
			point_display.GetComponent<Text> ().text = points_to_display.ToString ("F0");
		}
	}

	//Timer
	public GameObject timer;
	float time_limit = 0f;
	bool run_timer = false;

	void RunTimer(){
		if (run_timer) {
			if (time_limit > 0) {
				time_limit -= Time.deltaTime;
				if (time_limit > 0) { 
					timer.GetComponentInChildren<Text> ().text = time_limit.ToString ("F0");
				} else {
					GameOver(false);
				}
			}
		}
	}

	bool paused = false;
	
	void Start () {
		Pause (true);
		time_limit = float.Parse(Main.GetConfig("jumper_tiempo"));
		time_limit = 60;
		if (time_limit > 0) {
			timer.SetActive (true);
			run_timer = true;
		} else {
			timer.SetActive (false);
		}

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		SpawnPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!paused) {
			UpdateScore ();
			RunTimer ();
		}
	}

	public void Pause(bool state){
		if (state) {
			paused = true;
			Time.timeScale = 0;
		} else {
			paused = false;
			Time.timeScale = 1;
		}
	}

	public IEnumerator PauseAfterSeconds(bool state, float seconds){
		paused = state;
		yield return new WaitForSeconds(seconds);
		Pause (state);
	}

	public void GameOver(bool won){
		popup_canvas.Show();
		popup_canvas.ShowChild("Finalizar");
		popup_canvas.ShowChild("Volver");
		StartCoroutine (PauseAfterSeconds (true,1.5f));
		player_won = won;
		if (won) {
			popup_canvas.ShowChild("Ganaste");
			/*if(Main.GetConfig("premios_activos").ToString() == "True"){
				StartCoroutine (Main.LoadLevel ("ruleta", 2));
			}else{
				Debug.Log ("mensaje gracias por jugar");
				Main.Home();
			}*/
		} else {
			popup_canvas.ShowChild("Perdiste");
			/*Debug.Log ("perdiste");
			Main.Home();*/
		}
	}

	public void NextScene(){
		if (player_won) {
			if(Main.GetConfig("premios_activos").ToString() == "True"){
				Main.times_load_ruleta = 3;
				StartCoroutine (Main.LoadLevel ("ruleta", 0));
			}else{
				Main.Home();
			}
		} else {
			Main.Home();
		}
	}
}