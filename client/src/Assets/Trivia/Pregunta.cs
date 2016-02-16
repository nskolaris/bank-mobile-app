using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DbConnection;

public class Pregunta : MonoBehaviour {

	public int id = 0;
	string pregunta = "";
	Sprite image;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetData(Hashtable data, int index){
		id = (int)data["id"];
		gameObject.name = "Pregunta" + index.ToString ();
		pregunta = data["pregunta"].ToString();
		transform.Find("Text").gameObject.GetComponent<Text>().text = pregunta;
		transform.Find("Counter").gameObject.GetComponent<Text>().text = index.ToString()+"/"+data["count"].ToString();
		image =  Resources.Load <Sprite>("Trivia/preguntas/"+id.ToString ());
		if (image){
			transform.Find ("Image").gameObject.GetComponent<Image> ().sprite = image;
		} else {
			Debug.LogError("Sprite not found", this);
		}
		gameObject.SetActive (false);
	}

	public static Hashtable GetAll(){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT * FROM preguntas WHERE grupo_id = "+int.Parse(Main.GetConfig("trivia_grupo_id"));
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable preguntas = new Hashtable ();
		int index = 0;
		while (db.reader.Read()) {
			Hashtable pregunta = new Hashtable ();
			pregunta.Add ("id",db.reader.GetInt32(0));
			pregunta.Add ("pregunta",db.reader.GetString(1));
			preguntas.Add (index, pregunta);
			index ++;
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return preguntas;
	}

	// Funciones Grupos

	public static Hashtable GetAllGroups(){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT * FROM grupospreguntas";
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
		string sqlQuery = "SELECT * FROM grupospreguntas WHERE id = " + id;
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
