using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using DbConnection;

public class Evento : MonoBehaviour, IPointerClickHandler{

	public GameObject config_evento;
	public int id;
	public bool selected;
	public Color32 selected_color;

	public void OnPointerClick (PointerEventData eventData) {
		Evento[] eventos = transform.parent.gameObject.GetComponentsInChildren<Evento> ();
		foreach (Evento evento in eventos) {
			evento.ToggleSelect(false);
		}
		ToggleSelect(true);
		config_evento.GetComponent<ListaEventos> ().SelectEvento (id);
	}

	void ToggleSelect(bool value){
		selected = value;
		if (selected) {
			GetComponentInChildren<Image>().color = selected_color;
		} else {
			GetComponentInChildren<Image>().color = new Color32(255,255,255,255);
		}
	}

	/* Funciones modelo */

	public static int GetActivoID(){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT id FROM eventos WHERE code = '"+GetActivoCode()+"'";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		int id = 0;
		while (db.reader.Read()) {
			id = db.reader.GetInt32(0);
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return id;
	}

	public static string GetActivoCode(){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT valor FROM configuraciones WHERE denominacion = 'evento_code'";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		string evento_code = "";
		while (db.reader.Read()) {
			evento_code = db.reader.GetString(0);
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return evento_code;
	}

	public static void CambiarActivo(string code){
		DB db = new DB ();
		db.Connect ();
		string query = "UPDATE configuraciones SET valor = '" + code + "' WHERE denominacion = 'evento_code'";
		db.dbcmd.CommandText = query;
		if (db.dbcmd.ExecuteNonQuery () == 1) {
			GUItest.AlertMsg("agregado","Evento codigo '" + code + "' activado");
		}
		db.Disconnect ();
	}

	public static Hashtable GetActivo(){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT * FROM eventos WHERE code = '"+GetActivoCode()+"'";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable evento = new Hashtable ();
		while (db.reader.Read()) {
			evento.Add("id",db.reader.GetInt32(0));
			evento.Add("nombre",db.reader.GetString(1));
			evento.Add("code",db.reader.GetString(5));
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return evento;
	}
	
	public static Hashtable Get(int id){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT * FROM eventos WHERE id = " + id;
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable evento = new Hashtable ();
		while (db.reader.Read()) {
			evento.Add ("local_id",db.reader.GetInt32(0));
			evento.Add ("nombre",db.reader.GetString(1));
			evento.Add ("ciudad",db.reader.GetString(2));
			if(!db.reader.IsDBNull(3)){evento.Add ("fecha_desde",db.reader.GetString(3));}else{evento.Add ("fecha_desde","");}
			if(!db.reader.IsDBNull(4)){evento.Add ("fecha_hasta",db.reader.GetString(4));}else{evento.Add ("fecha_hasta","");}
			evento.Add ("code",db.reader.GetString(5));
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return evento;
	}

	public static ArrayList GetIdsByCodes(ArrayList codes){
		DB db = new DB();
		db.Connect ();
		string string_codes = string.Join ("','", (string[])codes.ToArray (Type.GetType ("System.String")));
		string sqlQuery = "SELECT id FROM eventos WHERE code IN ('"+string_codes+"')";
		db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader();
		ArrayList ids = new ArrayList ();
		while (db.reader.Read()) {
			ids.Add(db.reader.GetInt32(0).ToString());
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return ids;
	}

	public static Hashtable GetAll(){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT * FROM eventos ORDER BY nombre COLLATE NOCASE";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable eventos = new Hashtable ();
		int index = 0;
		while (db.reader.Read()) {
			Hashtable evento = new Hashtable ();
			evento.Add ("local_id",db.reader.GetInt32(0));
			evento.Add ("nombre",db.reader.GetString(1));
			if(!db.reader.IsDBNull(3)){evento.Add ("fecha_desde",db.reader.GetString(3));}else{evento.Add ("fecha_desde","");}
			if(!db.reader.IsDBNull(4)){evento.Add ("fecha_hasta",db.reader.GetString(4));}else{evento.Add ("fecha_hasta","");}
			evento.Add ("code",db.reader.GetString(5));
			eventos.Add (index, evento);
			index ++;
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return eventos;
	}

	public static bool Save(Hashtable data){
		DB db = new DB(); db.Connect ();

		string fields = "nombre,ciudad,code,fecha_inicio,fecha_final";
		string values = "";

		foreach (DictionaryEntry value in data) {
			values += "'" + value.Value + "',";
		}
		char[] remove = {','};
		values = values.TrimEnd(remove);
		
		string sqlQuery = "INSERT INTO eventos(" + fields + ") VALUES (" + values + ")";

		db.dbcmd.CommandText = sqlQuery;
		if (db.dbcmd.ExecuteNonQuery () == 1) {
			db.Disconnect ();
			return true;
		}
		db.Disconnect ();
		return false;
	}
}
