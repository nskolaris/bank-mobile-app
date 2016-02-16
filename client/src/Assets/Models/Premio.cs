using UnityEngine;
using System;
using System.Collections;
using DbConnection;

public class Premio : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static int GetCountByEvento(int evento_id){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT COUNT(*) FROM premios WHERE evento_id = " + evento_id;
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		int count = 0;
		while (db.reader.Read()) {
			count = db.reader.GetInt32(0);
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return count;
	}

	public static int GetRestantes(){
		int premios_restantes = int.Parse (Main.GetConfig ("cantidad_premios")) - GetCountByEvento (Evento.GetActivoID ());
		return premios_restantes;
	}

	public static Hashtable GetLastByEvento(int evento_id){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT * FROM premios WHERE evento_id = " + evento_id + " ORDER BY id DESC LIMIT 1";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable premio = new Hashtable();
		while (db.reader.Read()) {
			premio.Add ("id",db.reader.GetInt32(0));
			premio.Add ("fecha_entregado",db.reader.GetString(1));
			premio.Add ("evento_id",db.reader.GetInt32(2));
			premio.Add ("participante_id",db.reader.GetInt32(3));
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return premio;
	}

	public static void Registrar(){
		DB db = new DB ();
		db.Connect ();
		string query = "INSERT INTO premios(fecha_entregado,evento_id,participante_id) VALUES ('"+System.DateTime.Now.ToString()+"',"+Evento.GetActivoID()+","+Main.participante_id+");";
		db.dbcmd.CommandText = query;
		if (db.dbcmd.ExecuteNonQuery () == 1) {
			int restantes = int.Parse(Main.GetConfig ("cantidad_premios"));
			Main.SaveConfig("cantidad_premios",(restantes-1).ToString());
		}
		db.Disconnect ();
	}
}
