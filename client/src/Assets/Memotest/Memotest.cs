using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DbConnection;

public class Memotest : MonoBehaviour {

	public GameObject Ficha;
	public GameObject Ficha3D;

	//Database config
	public int rows = 4;
	public int columns = 4;
	float seconds_limit;

	public float vertical_padding_percentage = 10;
	public float fichas_margin_percentage = 10;
	public float seconds_after_fail = 1;

	public bool allow_show = false;
	int group_showing = 0;
	int matches = 0;

	public GameObject timer;

	public PopupCanvas popup_canvas;

	public Text titulo;
	
	void Start () {
		int conjunto_id = int.Parse(Main.GetConfig("memotest_grupo_id"));
		switch (conjunto_id) {
		case 1:
			titulo.text = "Conocé los artistas que acompañamos";
			break;
		case 2:
			titulo.text = "Conocé nuestros productos";
			break;
		case 3:
			titulo.text = "Conocé la sustentabilidad";
			break;
		}
		seconds_limit = float.Parse(Main.GetConfig("memotest_tiempo"));
		if (seconds_limit > 0) {
			timer.SetActive (true);
			timer.GetComponentInChildren<Text> ().text = seconds_limit.ToString ("F0");
		} else {
			timer.SetActive (false);
		}
		instantiateFichas3D ();
	}

	/*void instantiateFichas(){
		float canvas_height = gameObject.GetComponent<RectTransform> ().rect.height;
		
		float vertical_padding = (vertical_padding_percentage * canvas_height) / 100;
		canvas_height -= vertical_padding * 2;
		
		float fichas_margin = (fichas_margin_percentage * canvas_height) / 100;
		float fichas_height = (canvas_height - (fichas_margin * (rows - 1))) / rows;
		float fichas_width = fichas_height;
		
		float total_fichas_width = (fichas_width * columns) + (fichas_margin * (columns - 1));
		float xo = (fichas_width / 2) - (total_fichas_width / 2);
		float yo = canvas_height/2 - (fichas_height / 2);
		
		List<int> groups = new List<int>();
		groups.Add(0);
		for (int group_id = 1; group_id <= (rows*columns)/2; group_id++) {
			groups.Add(0);
		}
		
		for (int c = 0; c < columns; c++) {
			for(int r = 0; r<rows; r++){
				float x = xo + ((fichas_width+fichas_margin)*c);
				float y =  yo - ((fichas_height+fichas_margin)*r);
				
				GameObject ficha = Instantiate(Ficha) as GameObject;
				
				ficha.transform.SetParent(gameObject.transform);
				ficha.transform.localPosition = new Vector3(x, y, 0);
				ficha.GetComponent<RectTransform>().sizeDelta = new Vector2(fichas_width, fichas_height);
				
				int group = 0;
				while(group == 0){
					int group_id = Random.Range(1, groups.Count);
					if(groups[group_id] < 2){
						group = group_id;
						groups[group_id] += 1;
					}
				}
				
				ficha.GetComponent<Ficha>().group = group;
			}
		}
	}*/

	public RectTransform gui_container;
	public float top_offset = 20f;

	void instantiateFichas3D(){
		float canvas_height = gui_container.rect.height;
		canvas_height = 500;
		Debug.Log (canvas_height);

		float vertical_padding = (vertical_padding_percentage * canvas_height) / 100;
		canvas_height -= vertical_padding * 2;
		
		float fichas_margin = (fichas_margin_percentage * canvas_height) / 100;
		float fichas_height = (canvas_height - (fichas_margin * (rows - 1))) / rows;
		float fichas_width = fichas_height;
		
		float total_fichas_width = (fichas_width * columns) + (fichas_margin * (columns - 1));
		float xo = (fichas_width / 2) - (total_fichas_width / 2);
		float yo = (canvas_height/2 - (fichas_height / 2))-top_offset;
		
		List<int> groups = new List<int>();
		groups.Add(0);
		for (int group_id = 1; group_id <= (rows*columns)/2; group_id++) {
			groups.Add(0);
		}
		
		for (int c = 0; c < columns; c++) {
			for(int r = 0; r<rows; r++){
				float x = xo + ((fichas_width+fichas_margin)*c);
				float y =  yo - ((fichas_height+fichas_margin)*r);
				
				GameObject ficha = Instantiate(Ficha3D) as GameObject;

				ficha.transform.SetParent(gameObject.transform);
				ficha.transform.localScale = new Vector3(fichas_width,fichas_height,fichas_width/2);
				ficha.transform.localPosition = new Vector3(x, y, 35);
				
				int group = 0;
				while(group == 0){
					int group_id = Random.Range(1, groups.Count);
					if(groups[group_id] < 2){
						group = group_id;
						groups[group_id] += 1;
					}
				}
				ficha.GetComponent<Ficha3D>().SetGroup(group);
			}
		}
	}
	
	public void reportShow(int group){
		if (!popup_canvas.gameObject.activeSelf) {
			if (group_showing == 0) {
				group_showing = group;
			} else {
				if (group == group_showing) {
					matches ++;
					if (matches == (rows * columns) / 2) {
						StartCoroutine (AfterGame(true,2));
					}
				} else {
					allow_show = false;
					Ficha3D[] fichas = gameObject.GetComponentsInChildren<Ficha3D> ();
					foreach (Ficha3D ficha in fichas) {
						if (ficha.group == group_showing || ficha.group == group) {
							StartCoroutine (HideFichas3D (ficha));
						}
					}

					/*Ficha[] fichas = gameObject.GetComponentsInChildren<Ficha>();
					foreach (Ficha ficha in fichas){
						if(ficha.group == group_showing || ficha.group == group){
							StartCoroutine(HideFichas(ficha));
						}
					}*/

				}
				group_showing = 0;
			}
		}
	}

	/*IEnumerator HideFichas(Ficha ficha){
		yield return new WaitForSeconds(seconds_after_fail);
		ficha.Hide ();
		allow_show = true;
	}*/

	IEnumerator HideFichas3D(Ficha3D ficha){
		yield return new WaitForSeconds(seconds_after_fail);
		ficha.Hide ();
		allow_show = true;
	}

	public bool paused = true;

	public void Pause(bool state){
		paused = state;
	}

	void Update () {
		if (!paused) {
			if (seconds_limit > 0) {
				seconds_limit -= Time.deltaTime;
				if (seconds_limit > 0) { 
					timer.GetComponentInChildren<Text> ().text = seconds_limit.ToString ("F0");
				} else {
					StartCoroutine (AfterGame(false));
				}
			}
		}
	}

	bool won;

	IEnumerator AfterGame(bool win, int seconds = 0){
		Pause (true);
		won = win;
		yield return new WaitForSeconds(seconds);
		popup_canvas.Show();
		if (win) {
			popup_canvas.ShowChild ("Ganaste");
		} else {
			popup_canvas.ShowChild ("Perdiste");
		}
		//popup_canvas.ShowChild("Fin");
		popup_canvas.ShowChild("Volver");
		popup_canvas.ShowChild("Siguiente");
	}

	public void NextScene(){
		if (won) {
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

	// Funciones Grupos
	
	public static Hashtable GetAllGroups(){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT * FROM gruposmemotest";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable grupos = new Hashtable ();
		int index = 0;
		while (db.reader.Read()) {
			Hashtable grupo = new Hashtable ();
			grupo.Add ("id",db.reader.GetInt32(0));
			grupo.Add ("denominacion",db.reader.GetString(1));
			grupos.Add (index, grupo);
			index ++;
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return grupos;
	}
	
	public static Hashtable AllGroupsCombo(){
		Hashtable grupos = GetAllGroups ();
		Hashtable grupos_combo = new Hashtable ();
		int i = 0;
		foreach(DictionaryEntry grupo in grupos) {
			Hashtable grupo_values = (Hashtable) grupo.Value;
			Hashtable grupo_item = new Hashtable ();
			grupo_item.Add("id",grupo_values["id"].ToString());
			grupo_item.Add("nombre",grupo_values["denominacion"].ToString());
			grupos_combo.Add (i,grupo_item);
			i++;
		}
		return grupos_combo;
	}
	
	public static Hashtable GetGroupById(int id){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT * FROM gruposmemotest WHERE id = " + id;
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable grupo = new Hashtable ();
		while (db.reader.Read()) {
			grupo.Add ("id",db.reader.GetInt32(0));
			grupo.Add ("denominacion",db.reader.GetString(1));
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return grupo;
	}
	
	public static string GetGroupNameById(int id){
		Hashtable grupo = GetGroupById (id);
		return grupo["denominacion"].ToString();
	}
}