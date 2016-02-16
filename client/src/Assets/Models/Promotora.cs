using UnityEngine;
using System.Collections;
using DbConnection;

public class Promotora : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static int GetActivoID(){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT valor FROM configuraciones WHERE denominacion = 'promotora_id'";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		int id = 0;
		while (db.reader.Read()) {
			id = int.Parse(db.reader.GetString(0));
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return id;
	}

	public static int GetLastID(){
		DB db = new DB(); db.Connect ();
		string sqlQuery = "SELECT id FROM usuarios ORDER BY id DESC LIMIT 1";
		db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader();
		int id = 0;
		while (db.reader.Read()) {
			id = db.reader.GetInt32(0);
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return id;
	}

	public static string GetActivoUsername(){
		Hashtable promotora = GetActivo ();
		return promotora["username"] as string;
	}

	public static Hashtable GetActivo(){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT id,nombre,apellido,username FROM usuarios WHERE id = "+GetActivoID();
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable promotora = new Hashtable ();
		while (db.reader.Read()) {
			promotora.Add("id",db.reader.GetInt32(0));
			promotora.Add("nombre_apellido",db.reader.GetString(1)+" "+db.reader.GetString(2));
			promotora.Add("username",db.reader.GetString(3));
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return promotora;
	}

	public static void CambiarActivo(string id){
		DB db = new DB ();
		db.Connect ();
		string query = "UPDATE configuraciones SET valor = '" + id + "' WHERE denominacion = 'promotora_id'";
		db.dbcmd.CommandText = query;
		if (db.dbcmd.ExecuteNonQuery () == 1) {
			Debug.Log ("Promotora id '" + id + "' activada");
		}
		db.Disconnect ();
	}
	
	public static Hashtable GetAll(){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT id,nombre,apellido FROM usuarios WHERE admin = 0 ORDER BY apellido COLLATE NOCASE";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable promotoras = new Hashtable ();
		int index = 0;
		while (db.reader.Read()) {
			Hashtable promotora = new Hashtable ();
			promotora.Add("id",db.reader.GetInt32(0));
			promotora.Add("nombre_apellido",db.reader.GetString(2)+" "+db.reader.GetString(1));
			promotoras.Add (index, promotora);
			index ++;
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return promotoras;
	}

	public static bool Save(Hashtable data){
		DB db = new DB();
		db.Connect ();
		
		string fields = "nombre,apellido,username,password";
		string values = "";
		
		foreach (DictionaryEntry value in data) {
			values += "'" + value.Value + "',";
		}
		char[] remove = {','};
		values = values.TrimEnd(remove);
		
		string sqlQuery = "INSERT INTO usuarios(" + fields + ") VALUES (" + values + ")";
		
		db.dbcmd.CommandText = sqlQuery;
		if (db.dbcmd.ExecuteNonQuery () == 1) {
			db.Disconnect ();
			return true;
		}
		db.Disconnect ();
		return false;
	}
}