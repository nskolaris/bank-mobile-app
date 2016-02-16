using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using DbConnection;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	EventSystem system;
	void Start () {
		system = EventSystem.current;
	}
	bool open_keyboard = false;
	void Update(){
		if (!open_keyboard) {
			if (TouchScreenKeyboard.visible) {
				open_keyboard = true;
			}
		} else {
			if (!TouchScreenKeyboard.visible) { //Keyboard was closed
				open_keyboard = false;
				/*if(system.currentSelectedGameObject.transform.Find("Placeholder").GetComponent<Text>().text == "DNI"){
					InputField[] inputs = system.currentSelectedGameObject.transform.parent.parent.GetComponentsInChildren<InputField> ();
					foreach (InputField input in inputs) {
						if (input.transform.Find ("Placeholder").GetComponent<Text> ().text == "Tel") {
							input.OnPointerClick (new PointerEventData (system));
							system.SetSelectedGameObject (input.gameObject, new BaseEventData (system));
						}
					}
				}else{*/
				StartCoroutine(NextField());
				//}
			}
		}
	}

	IEnumerator NextField(){
		yield return new WaitForSeconds(0.5f);
		InputField current_input = system.currentSelectedGameObject.GetComponent<InputField> ();
		Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
		if (next != null && current_input.text != ""){
			InputField inputfield = next.GetComponent<InputField>();
			if (inputfield != null){
				inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret
				system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
			}
		}
	}

	public void NextFiel(){
		/*if (system.currentSelectedGameObject.transform.Find ("Placeholder").GetComponent<Text> ().text == "DNI") {
			InputField[] inputs = system.currentSelectedGameObject.transform.parent.parent.GetComponentsInChildren<InputField> ();
			foreach (InputField input in inputs) {
				if (input.transform.Find ("Placeholder").GetComponent<Text> ().text == "Tel") {
					input.OnPointerClick (new PointerEventData (system));
					system.SetSelectedGameObject (input.gameObject, new BaseEventData (system));
				}
			}
		} else {*/
			Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
			if (next != null){
				InputField inputfield = next.GetComponent<InputField>();
				if (inputfield != null){
					inputfield.OnPointerClick(new PointerEventData(system));
					system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
				}
			}
		//}
	}

	public static Hashtable logged_user = new Hashtable();
	public static int participante_id = 0;

	public void Loadlevel(string level){
		/*if (level == "ruleta") {
			load_ruleta = true;
			level = "main";
		}*/
		SceneManager.LoadScene(level);
	}

	public static IEnumerator LoadLevel(string level, float seconds = 0){
		yield return new WaitForSeconds(seconds);
		/*if (level == "ruleta") {
			load_ruleta = true;
			level = "main";
		}*/
		Time.timeScale = 1;
		SceneManager.LoadScene(level);
	}

	public static void Home(){
		SceneManager.LoadScene("main");
	}

	public static bool load_ruleta = false;
	public static int times_load_ruleta = 3;

	public void reloadLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public static void StartGame(){
		SceneManager.LoadScene(Main.GetConfig("juego_nombre"));
	}

	public static void AfterRegister(){
		DB db = new DB(); db.Connect ();
		
		string sqlQuery = "SELECT id FROM participantes ORDER BY id DESC LIMIT 1";
		db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader();
		while (db.reader.Read()) {
			if(!db.reader.IsDBNull(0)){
				participante_id = db.reader.GetInt32(0);
			}
		}
		db.reader.Close(); db.reader = null; db.Disconnect ();

		if (Convert.ToBoolean (Main.GetConfig ("juego_activo"))) {
			StartGame ();
		} else {
			Home ();
		}
	}

	public static string GetConfig(string param_name){
		DB db = new DB();
		db.Connect ();

		string sqlQuery = "SELECT valor FROM configuraciones WHERE evento_id = "+Evento.GetActivoID()+" AND denominacion = '"+param_name+"'";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		string value = "";
		while (db.reader.Read()) {
			if(!db.reader.IsDBNull(0)){
				value = db.reader.GetString(0);
			}
		}
		db.reader.Close();

		if (value == "") {
			sqlQuery = "SELECT valor FROM configuraciones WHERE evento_id = 0 AND denominacion = '"+param_name+"'";
			db.dbcmd.CommandText = sqlQuery;
			db.reader = db.dbcmd.ExecuteReader();
			while (db.reader.Read()) {
				value = db.reader.GetString(0);
			}
			db.reader.Close();
		}

		db.reader = null;
		db.Disconnect ();

		return value;
	}

	public static bool SaveConfig(string param_name, string value){
		DB db = new DB();
		db.Connect ();

		bool result = false;

		string sqlQuery = "SELECT id FROM configuraciones WHERE evento_id = "+Evento.GetActivoID()+" AND denominacion = '"+param_name+"'";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		int id = 0;
		while (db.reader.Read()) {
			if(!db.reader.IsDBNull(0)){
				id = db.reader.GetInt32(0);
			}
		}
		db.reader.Close();
		db.reader = null;

		if (id == 0) {
			sqlQuery = "INSERT INTO configuraciones(denominacion,valor,evento_id) VALUES ('"+param_name+"','"+value+"',"+Evento.GetActivoID()+")";
			db.dbcmd.CommandText = sqlQuery;
			result = Convert.ToBoolean(db.dbcmd.ExecuteNonQuery());
		} else {
			sqlQuery = "UPDATE configuraciones SET valor = '"+value+"' WHERE id = "+id;
			db.dbcmd.CommandText = sqlQuery;
			result = Convert.ToBoolean(db.dbcmd.ExecuteNonQuery());
		}

		db.Disconnect ();
		return result;
	}

	public static int premiosRemaining(){
		return int.Parse(Main.GetConfig ("cantidad_premios"));
	}

	public static bool CalculateWin(){
		//int premios_restantes = Premio.GetRestantes ();
		int premios_restantes = int.Parse(Main.GetConfig ("cantidad_premios"));

		if (premios_restantes > 0) {
			Hashtable last_premio = Premio.GetLastByEvento (Evento.GetActivoID ());

			DateTime last_date = new DateTime();
			if(last_premio["id"] != null){
				last_date = DateTime.Parse(last_premio["fecha_entregado"].ToString());
			}else{
				last_date = DateTime.Parse(GetConfig ("premios_start_date"));
			}

			DateTime end = DateTime.Parse(GetConfig ("premios_end_date"));
			TimeSpan duration = end - last_date;

			DateTime hora_proxima_entrega = last_date.AddMinutes(duration.TotalMinutes / premios_restantes);

			if(System.DateTime.Now > hora_proxima_entrega){
				return true;
			}
		}
		return false;
	}

	public static string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
		
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}
}
