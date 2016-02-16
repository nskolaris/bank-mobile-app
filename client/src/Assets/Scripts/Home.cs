using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using DbConnection;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour {

	Transform login;

	void Start () {

		if (Main.load_ruleta) {
			Main.load_ruleta = false;
			SceneManager.LoadScene("ruleta");
		} else {
			if(GameObject.FindGameObjectWithTag("OverlayNegro") != null){
				GameObject.FindGameObjectWithTag("OverlayNegro").SetActive(false);
			}
		}

		if (!Convert.ToBoolean (Main.GetConfig ("juego_activo"))) {
			transform.Find ("Button Play").gameObject.GetComponentInChildren<Text> ().text = "Participar";
		} else {
			transform.Find ("Button Play").gameObject.GetComponentInChildren<Text> ().text = "Jugar";
		}
		
		if (!Convert.ToBoolean (Main.GetConfig ("formulario_activo")) && !Convert.ToBoolean (Main.GetConfig ("juego_activo")) && !Convert.ToBoolean (Main.GetConfig ("premios_activos"))) {
			transform.Find ("Button Play").gameObject.GetComponent<Button>().interactable = false;
		}

		login = transform.Find ("Login");
	}

	void Update(){
		if (transform.Find ("Slider").gameObject.GetComponent<Slider> ().value != 1) {
			time_since_last_slider_change += Time.deltaTime;
			if (time_since_last_slider_change > 0.25f) {
				transform.Find ("Slider").gameObject.GetComponent<Slider> ().value = 1f;
				transform.Find ("Slider_Vertical").gameObject.GetComponent<Slider> ().value = 0f;
				current_value_horizontal = 1f;
			}
		}

		if (admin_click_timer > 0 || click_to_admin > 0) {
			admin_click_timer -= Time.deltaTime;
			if(admin_click_timer <= 0){
				click_to_admin = 0;
			}
		}
	}

	public void Jugar(){
		if (Convert.ToBoolean (Main.GetConfig ("formulario_activo"))) {
			SceneManager.LoadScene("formulario");
		} else if (Convert.ToBoolean (Main.GetConfig ("juego_activo"))) {
			Main.StartGame ();
		} else {
			SceneManager.LoadScene("ruleta");
		}
	}

	float time_since_last_slider_change = 0f;
	float current_value_horizontal = 1f;

	public void HandleSlider(){
		Vector3 mPos = Input.mousePosition;
		float relX = mPos.x / transform.Find ("Slider").gameObject.GetComponent<RectTransform>().rect.width;
		if (relX > current_value_horizontal - 0.1f && relX < current_value_horizontal + 0.1f) {
			time_since_last_slider_change = 0;
		}
		current_value_horizontal = transform.Find ("Slider").gameObject.GetComponent<Slider> ().value;
	}

	public void ShowHandle(bool horizontal){
		Image handle_horizontal = transform.Find ("Slider").Find ("Handle Slide Area").GetComponentInChildren<Image>();
		Color32 handle_horizontal_color = handle_horizontal.color;
		Color32 new_handle_horizontal_color;
		if (horizontal) {
			new_handle_horizontal_color = new Color32(handle_horizontal_color.r,handle_horizontal_color.g,handle_horizontal_color.b,255);
		} else {
			new_handle_horizontal_color = new Color32(handle_horizontal_color.r,handle_horizontal_color.g,handle_horizontal_color.b,1);
		}
		transform.Find ("Slider").Find ("Handle Slide Area").GetComponentInChildren<Image>().color = new_handle_horizontal_color;
	}

	int click_to_admin = 0;
	float admin_click_timer = 0;

	public void ClickToAdmin(){
		admin_click_timer = 1;
		click_to_admin++;
		if (click_to_admin >= 5) {
			click_to_admin = 0;
			ShowLoginWindow();
		}
	}

	public void ShowLoginWindow(){
		login.Find ("Evento").Find ("Activo").gameObject.GetComponent<Text> ().text = Evento.GetActivo () ["nombre"].ToString();
		login.Find ("Promotora").Find ("Activo").gameObject.GetComponent<Text> ().text = Promotora.GetActivo () ["nombre_apellido"].ToString();
		if (Convert.ToBoolean (Main.GetConfig ("juego_activo"))) {
			if(Main.GetConfig ("juego_nombre") == "jump"){
				login.Find ("Juego").Find ("Activo").gameObject.GetComponent<Text> ().text = "Recorriendo el Pais";
			}else{
				login.Find ("Juego").Find ("Activo").gameObject.GetComponent<Text> ().text = Main.GetConfig ("juego_nombre");
			}
		} else {
			login.Find ("Juego").Find ("Text").gameObject.GetComponent<Text> ().color = new Color32(171,171,171,255);
			login.Find ("Juego").Find ("Activo").gameObject.GetComponent<Text> ().color = new Color32(171,171,171,255);
			login.Find ("Juego").Find ("Activo").gameObject.GetComponent<Text> ().text = "Inactivo";
		}
		if (Convert.ToBoolean (Main.GetConfig ("premios_activos"))) {
			login.Find ("Premios").Find ("Restantes").gameObject.GetComponent<Text> ().text = Main.premiosRemaining ().ToString();
		} else {
			login.Find ("Premios").Find ("Text").gameObject.GetComponent<Text> ().color = new Color32(171,171,171,255);
			login.Find ("Premios").Find ("Restantes").gameObject.GetComponent<Text> ().color = new Color32(171,171,171,255);
			login.Find ("Premios").Find ("Restantes").gameObject.GetComponent<Text> ().text = "Inactivo";
		}
		if (Convert.ToBoolean (Main.GetConfig ("formulario_activo"))) {
			login.Find ("Formulario").Find ("Value").gameObject.GetComponent<Text> ().text = "Si";
		} else {
			login.Find ("Formulario").Find ("Value").gameObject.GetComponent<Text> ().text = "No";
		}

		FillDates ();

		login.gameObject.SetActive (true);
	}

	void FillDates(){
		string fecha_desde = Main.GetConfig ("premios_start_date");
		string fecha_hasta = Main.GetConfig ("premios_end_date");
		char[] delimiters = new char[] {'-',':',' '};

		string[] fecha_desde_exploded = fecha_desde.Split (delimiters);
		if (fecha_desde_exploded.Length > 1) {
			string fecha = fecha_desde_exploded[2]+"/"+fecha_desde_exploded[1]+"/"+fecha_desde_exploded[0]+" "+fecha_desde_exploded[3]+":"+fecha_desde_exploded [4];
			login.Find ("FechaDesde").Find ("Fecha").gameObject.GetComponent<Text> ().text = fecha;
		}

		string[] fecha_hasta_exploded = fecha_hasta.Split (delimiters);
		if (fecha_hasta_exploded.Length > 1) {
			string fecha = fecha_hasta_exploded[2]+"/"+fecha_hasta_exploded[1]+"/"+fecha_hasta_exploded[0]+" "+fecha_hasta_exploded[3]+":"+fecha_hasta_exploded [4];
			login.Find ("FechaHasta").Find ("Fecha").gameObject.GetComponent<Text> ().text = fecha;
		}

		if(Main.premiosRemaining() <= 0){
			GUItest.AlertMsg("Alerta","Atención, no quedan mas premios para repartir");
			login.Find ("FechaDesde").Find ("Fecha").gameObject.GetComponent<Text> ().color = Color.red;
			login.Find ("FechaHasta").Find ("Fecha").gameObject.GetComponent<Text> ().color = Color.red;
		}

		if (Main.GetConfig ("premios_end_date") != "") {
			DateTime end = DateTime.Parse (Main.GetConfig ("premios_end_date"));
			if (end < System.DateTime.Now) {
				GUItest.AlertMsg ("Alerta", "Atención, la fecha de reparto de los premios ha expirado");
				login.Find ("FechaDesde").Find ("Fecha").gameObject.GetComponent<Text> ().color = Color.red;
				login.Find ("FechaHasta").Find ("Fecha").gameObject.GetComponent<Text> ().color = Color.red;
			}
		}
	}

	public void Login(){
		string username = login.Find("Username").gameObject.GetComponent<InputField>().text;
		string password = login.Find("Password").gameObject.GetComponent<InputField>().text;
		if (username != "" && password != "") {
			DB db = new DB(); db.Connect();
			string sqlQuery = "SELECT id,admin,nombre,apellido FROM usuarios WHERE username = '" + username + "' AND password = '" + password + "'";
			db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader();
			if(db.reader.Read()){
				Main.logged_user = new Hashtable();
				Main.logged_user.Add("id",db.reader.GetInt32(0));
				Main.logged_user.Add("admin",db.reader.GetBoolean(1));
				Main.logged_user.Add("nombre",db.reader.GetString(2)+" "+db.reader.GetString(3));
				if(!db.reader.IsDBNull(2)){
					Main.logged_user.Add("nombre_apellido",db.reader.GetString(2)+" "+db.reader.GetString(3));
				}else{
					Main.logged_user.Add("nombre_apellido"," ");
				}
				
				db.reader.Close(); db.reader = null; db.Disconnect();
				if(Convert.ToBoolean (Main.logged_user["admin"])){
					SceneManager.LoadScene("configuracion");
				}else{
					Promotora.CambiarActivo(Main.logged_user["id"].ToString());
					//Application.LoadLevel(Application.loadedLevel);
					GUItest.AlertMsg ("OK","Login correcto, promotora "+Main.logged_user["nombre"]+" activa");
					ShowLoginWindow();
				}
			}else{
				db.reader.Close();
				db.reader = null;
				db.Disconnect();
				GUItest.AlertMsg ("Error","Usuario o contraseña incorrectos");
			}
		}
	}
}