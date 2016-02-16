using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DbConnection;

public class Sync : MonoBehaviour {

	public string post_url;
	public string access_token;

	ArrayList nuevos_eventos = new ArrayList();

	void OnEnable(){
		GetSyncStats ();
	}

	void Start(){
		transform.Find ("DeviceID").Find ("Value").gameObject.GetComponent<Text>().text = SystemInfo.deviceUniqueIdentifier;
	}

	public void StartSync(){
		Debug.Log ("Comenzando sincronización");
		transform.Find ("Sync").GetComponent<Button> ().interactable = false;
		SyncEventos ();
	}

	/* Sincronización de Eventos */

	void SyncEventos(){
		Debug.Log ("Sincronizando eventos...");
		DB db = new DB(); db.Connect ();
		string sqlQuery = "SELECT * FROM eventos WHERE external_id is null";
		db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader();
		Hashtable eventos = new Hashtable ();
		int index = 0;
		while(db.reader.Read()){
			Hashtable evento = new Hashtable ();
			evento.Add ("code",db.reader.GetString(5));
			evento.Add ("nombre",db.reader.GetString(1));
			evento.Add ("ciudad",db.reader.GetString(2));
			evento.Add ("fecha_desde",db.reader.GetString(3));
			evento.Add ("fecha_hasta",db.reader.GetString(4));
			eventos.Add (index, evento);
			index ++;
		}
		db.reader.Close(); db.reader = null; db.Disconnect ();
		string json_eventos = JSON.JsonEncode(eventos);
		StartCoroutine(Post("eventos/sincronizar",json_eventos,SyncEventosResponse));
	}

	bool SyncEventosResponse(string response){
		DB db = new DB (); db.Connect ();
		Hashtable response_obj = JSON.JsonDecode(response) as Hashtable;
		ArrayList eventos_array = response_obj["eventos"] as ArrayList;
		nuevos_eventos = response_obj["nuevos_eventos"] as ArrayList;

		foreach(var evento in eventos_array){
			Hashtable evento_data = evento as Hashtable;
			Hashtable fields = evento_data["Evento"] as Hashtable;
			string sqlQuery = "SELECT COUNT(*) FROM eventos WHERE code = '"+fields["code"]+"'";
			db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader(); db.reader.Read();
			bool exists = db.reader.GetBoolean(0);
			db.reader.Close();
			if(exists){
				sqlQuery = "UPDATE eventos SET external_id = "+fields["id"]+" WHERE code = '"+fields["code"]+"'";
				db.dbcmd.CommandText = sqlQuery;
				if(db.dbcmd.ExecuteNonQuery()==1){}
			}else{
				sqlQuery = "INSERT INTO eventos(nombre,ciudad,code,fecha_inicio,fecha_final,external_id) VALUES "+"('"+fields["nombre"]+"','"+fields["ciudad"]+"','"+fields["code"]+"','"+fields["fecha_desde"]+"','"+fields["fecha_hasta"]+"','"+fields["id"]+"')";
				db.dbcmd.CommandText = sqlQuery;
				if(db.dbcmd.ExecuteNonQuery()==1){}
			}
		}

		/*foreach(DictionaryEntry evento in eventos_array){
			Hashtable evento_data = evento.Value as Hashtable;
			Hashtable fields = evento_data["Evento"] as Hashtable;
			string sqlQuery = "SELECT COUNT(*) FROM eventos WHERE code = '"+fields["code"]+"'";
			db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader(); db.reader.Read();
			bool exists = db.reader.GetBoolean(0);
			db.reader.Close();
			if(exists){
				sqlQuery = "UPDATE eventos SET external_id = "+fields["id"]+" WHERE code = '"+fields["code"]+"'";
				db.dbcmd.CommandText = sqlQuery;
				if(db.dbcmd.ExecuteNonQuery()==1){}
			}else{
				sqlQuery = "INSERT INTO eventos(nombre,ciudad,code,fecha_inicio,fecha_final,external_id) VALUES "+"('"+fields["nombre"]+"','"+fields["ciudad"]+"','"+fields["code"]+"','"+fields["fecha_desde"]+"','"+fields["fecha_hasta"]+"','"+fields["id"]+"')";
				db.dbcmd.CommandText = sqlQuery;
				if(db.dbcmd.ExecuteNonQuery()==1){}
			}
		}
		*/

		Debug.Log ("Eventos sincronizados");
		db.Disconnect ();
		SyncUsuarios ();
		return true;
	}

	/* Sincronización de Usuarios */

	void SyncUsuarios(){
		Debug.Log ("Sincronizando usuarios...");
		DB db = new DB(); db.Connect ();
		string sqlQuery = "SELECT * FROM usuarios WHERE admin = 0 AND external_id is null";
		db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader();

		Hashtable usuarios = new Hashtable ();
		int index = 0;

		while (db.reader.Read()) {
			Hashtable usuario = new Hashtable ();
			usuario.Add ("username",db.reader.GetString(1));
			usuario.Add ("password",db.reader.GetString(2));
			usuario.Add ("password_tablet",db.reader.GetString(2));
			usuario.Add ("nombre",db.reader.GetString(4));
			usuario.Add ("apellido",db.reader.GetString(5));
			usuarios.Add (index, usuario);
			index ++;
		}

		db.reader.Close(); db.reader = null; db.Disconnect ();
		string json_usuarios = JSON.JsonEncode(usuarios);
		StartCoroutine(Post("usuarios/sincronizar",json_usuarios,SyncUsuariosResponse));
	}
	
	bool SyncUsuariosResponse(string response){
		DB db = new DB (); db.Connect ();
		//Hashtable usuarios = JSON.JsonDecode(response) as Hashtable;
		ArrayList usuarios = JSON.JsonDecode(response) as ArrayList;

		foreach (var usuario in usuarios) {
			Hashtable usuario_data = usuario as Hashtable;
			Hashtable fields = usuario_data["Usuario"] as Hashtable;
			string sqlQuery = "SELECT COUNT(*) FROM usuarios WHERE username = '"+fields["username"]+"'";
			db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader(); db.reader.Read();
			bool exists = db.reader.GetBoolean(0);
			db.reader.Close();
			if(exists){
				sqlQuery = "UPDATE usuarios SET external_id = "+fields["id"]+", password = '"+fields["password_tablet"]+"' WHERE username = '"+fields["username"]+"'";
				db.dbcmd.CommandText = sqlQuery;
				if(db.dbcmd.ExecuteNonQuery()==1){}
			}else{
				sqlQuery = "INSERT INTO usuarios(username,password,admin,nombre,apellido,external_id) VALUES "+"('"+fields["username"]+"','"+fields["password_tablet"]+"','"+fields["admin"]+"','"+fields["nombre"]+"','"+fields["apellido"]+"','"+fields["id"]+"')";
				db.dbcmd.CommandText = sqlQuery;
				if(db.dbcmd.ExecuteNonQuery()==1){}
			}
		}

		/*foreach(DictionaryEntry usuario in usuarios){
			Hashtable usuario_data = usuario.Value as Hashtable;
			Hashtable fields = usuario_data["Usuario"] as Hashtable;
			string sqlQuery = "SELECT COUNT(*) FROM usuarios WHERE username = '"+fields["username"]+"'";
			db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader(); db.reader.Read();
			bool exists = db.reader.GetBoolean(0);
			db.reader.Close();
			if(exists){
				sqlQuery = "UPDATE usuarios SET external_id = "+fields["id"]+", password = '"+fields["password_tablet"]+"' WHERE username = '"+fields["username"]+"'";
				db.dbcmd.CommandText = sqlQuery;
				if(db.dbcmd.ExecuteNonQuery()==1){}
			}else{
				sqlQuery = "INSERT INTO usuarios(username,password,admin,nombre,apellido,external_id) VALUES "+"('"+fields["username"]+"','"+fields["password_tablet"]+"','"+fields["admin"]+"','"+fields["nombre"]+"','"+fields["apellido"]+"','"+fields["id"]+"')";
				db.dbcmd.CommandText = sqlQuery;
				if(db.dbcmd.ExecuteNonQuery()==1){}
			}
		}*/

		Debug.Log ("Usuarios sincronizados");
		db.Disconnect ();
		SyncConfig ();
		return true;
	}

	/* Sincronización de Configuraciones */
	
	void SyncConfig(){
		Debug.Log ("Sincronizando configuraciones...");
		DB db = new DB(); db.Connect ();
		string nuevos_eventos_ids = string.Join (",", (string[])Evento.GetIdsByCodes (nuevos_eventos).ToArray (Type.GetType ("System.String")));
		string sqlQuery = "SELECT * FROM configuraciones WHERE evento_id IN ("+nuevos_eventos_ids+")";
		db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader();
		Hashtable configuraciones = new Hashtable ();
		int index = 0;
		while (db.reader.Read()) {
			Hashtable configuracion = new Hashtable ();
			configuracion.Add ("denominacion",db.reader.GetString(1));
			configuracion.Add ("valor",db.reader.GetString(2));
			configuracion.Add ("evento_id",db.reader.GetInt32(3));
			configuraciones.Add (index, configuracion);
			index ++;
		}
		db.reader.Close(); db.reader = null; db.Disconnect ();
		string json_configuraciones = JSON.JsonEncode(configuraciones);
		StartCoroutine(Post("configuraciones/sincronizar",json_configuraciones,SyncConfigResponse));
	}
	
	bool SyncConfigResponse(string response){
		DB db = new DB (); db.Connect ();
		Hashtable configuraciones_array = JSON.JsonDecode(response) as Hashtable;
		foreach (DictionaryEntry evento in configuraciones_array) {
			String evento_id = evento.Key as String;
			Hashtable configuraciones = evento.Value as Hashtable;
			foreach(DictionaryEntry configuracion in configuraciones){
				string denominacion = configuracion.Key as String;
				string value = configuracion.Value as String;
				string sqlQuery = "SELECT COUNT(*) FROM configuraciones WHERE denominacion = '"+denominacion+"' AND evento_id = "+evento_id;
				db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader(); db.reader.Read();
				bool exists = db.reader.GetBoolean(0);
				db.reader.Close();
				if(exists){
					sqlQuery = "UPDATE configuraciones SET valor = '"+value+"' WHERE denominacion = '"+denominacion+"' AND evento_id = "+evento_id;
					db.dbcmd.CommandText = sqlQuery;
					if(db.dbcmd.ExecuteNonQuery()==1){}
				}else{
					sqlQuery = "INSERT INTO configuraciones(denominacion,valor,evento_id) VALUES "+"('"+denominacion+"','"+value+"',"+evento_id+")";
					db.dbcmd.CommandText = sqlQuery;
					if(db.dbcmd.ExecuteNonQuery()==1){}
				}
			}
		}
		Debug.Log ("Configuraciones sincronizadas");
		db.Disconnect ();
		SyncParticipantes ();
		return true;
	}

	/* Sincronización de Participantes */

	void SyncParticipantes(){
		Debug.Log ("Sincronizando participantes...");
		DB db = new DB(); db.Connect ();
		string sqlQuery = "SELECT * FROM participantes WHERE external_id IS NULL";
		db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader();
		Hashtable participantes = new Hashtable ();
		int index = 0;
		while (db.reader.Read()) {
			Hashtable participante = new Hashtable ();
			participante.Add ("local_id",db.reader.GetInt32(0));
			participante.Add ("nombre",db.reader.GetString(1));
			participante.Add ("apellido",db.reader.GetString(2));
			participante.Add ("email",db.reader.GetString(3));
			participante.Add ("telefono",db.reader.GetString(4));
			participante.Add ("evento_code",db.reader.GetString(6));
			participante.Add ("dni",db.reader.GetString(7));
			participante.Add ("provincia",db.reader.GetString(8));
			participante.Add ("acepta_beneficios",db.reader.GetString(9));
			participante.Add ("promotora_username",db.reader.GetString(10));
			participante.Add ("juego_nombre",db.reader.GetString(11));
			participante.Add ("fecha_ingresado",db.reader.GetString(12));
			participantes.Add (index, participante);
			index ++;
		}
		db.reader.Close(); db.reader = null; db.Disconnect ();
		string json_participantes = JSON.JsonEncode(participantes);
		StartCoroutine(Post("participantes/sincronizar",json_participantes,SyncParticipantesResponse));
	}

	bool SyncParticipantesResponse(string response){
		if(response != "[]"){
			DB db = new DB (); db.Connect ();
			Hashtable participantes = JSON.JsonDecode(response) as Hashtable;
			foreach(DictionaryEntry participante in participantes){
				string query = "UPDATE participantes SET external_id = " + participante.Value + " WHERE id = " + participante.Key;
				db.dbcmd.CommandText = query;
				if (db.dbcmd.ExecuteNonQuery () == 1){}
			}
			db.Disconnect ();
		}
		Debug.Log ("Participantes sincronizados");
		PostExport (true);
		return true;
	}

	IEnumerator Post(string url, string json, Func<string,bool> callback) {
		string cert1 = @"-----BEGIN CERTIFICATE-----
MIIFaTCCBFGgAwIBAgIQa1Nm3qM+kHLYOWIS4eblOjANBgkqhkiG9w0BAQsFADCB
kDELMAkGA1UEBhMCR0IxGzAZBgNVBAgTEkdyZWF0ZXIgTWFuY2hlc3RlcjEQMA4G
A1UEBxMHU2FsZm9yZDEaMBgGA1UEChMRQ09NT0RPIENBIExpbWl0ZWQxNjA0BgNV
BAMTLUNPTU9ETyBSU0EgRG9tYWluIFZhbGlkYXRpb24gU2VjdXJlIFNlcnZlciBD
QTAeFw0xNTExMDIwMDAwMDBaFw0xODExMDEyMzU5NTlaMFsxITAfBgNVBAsTGERv
bWFpbiBDb250cm9sIFZhbGlkYXRlZDEUMBIGA1UECxMLUG9zaXRpdmVTU0wxIDAe
BgNVBAMTF2FwcHMtbGFuemFsbGFtYXMuY29tLmFyMIIBIjANBgkqhkiG9w0BAQEF
AAOCAQ8AMIIBCgKCAQEA7+v+eG+i4oTaXS7cj5XmHRftHyAfK8JH+zGnPvyzLDp3
QkWinW+DylbPj/hR70MnzJno0E9W7+P1sSFpiaqmo0h9mCEQVuEbi512i2RcRivZ
1VXFsn+LVkA+mTXSe4I8mSwA8TS6yVDZM4MR5HEAms2Pugf5Iq1mXCX2VVYEsj7v
usrV4obClm4t/I+2MTJYJyhxjrGooDbnJWdnEtPxmTRxnTezQlUqxz8y06EKLif5
4Ju9nai/htqoIwKNT9wEL7gHfT6yKO7ZHaaEHeRvKjEE+m6nMCzKBjJFfmORTf+t
gTEjbV+lHteIuik1d3FrnnDT1pwQHdR5VFQu0ETsVQIDAQABo4IB8TCCAe0wHwYD
VR0jBBgwFoAUkK9qOpRaC9iQ6hJWc99DtDoo2ucwHQYDVR0OBBYEFDcV/xxzq2eE
WQMQ0so/S2WbardiMA4GA1UdDwEB/wQEAwIFoDAMBgNVHRMBAf8EAjAAMB0GA1Ud
JQQWMBQGCCsGAQUFBwMBBggrBgEFBQcDAjBPBgNVHSAESDBGMDoGCysGAQQBsjEB
AgIHMCswKQYIKwYBBQUHAgEWHWh0dHBzOi8vc2VjdXJlLmNvbW9kby5jb20vQ1BT
MAgGBmeBDAECATBUBgNVHR8ETTBLMEmgR6BFhkNodHRwOi8vY3JsLmNvbW9kb2Nh
LmNvbS9DT01PRE9SU0FEb21haW5WYWxpZGF0aW9uU2VjdXJlU2VydmVyQ0EuY3Js
MIGFBggrBgEFBQcBAQR5MHcwTwYIKwYBBQUHMAKGQ2h0dHA6Ly9jcnQuY29tb2Rv
Y2EuY29tL0NPTU9ET1JTQURvbWFpblZhbGlkYXRpb25TZWN1cmVTZXJ2ZXJDQS5j
cnQwJAYIKwYBBQUHMAGGGGh0dHA6Ly9vY3NwLmNvbW9kb2NhLmNvbTA/BgNVHREE
ODA2ghdhcHBzLWxhbnphbGxhbWFzLmNvbS5hcoIbd3d3LmFwcHMtbGFuemFsbGFt
YXMuY29tLmFyMA0GCSqGSIb3DQEBCwUAA4IBAQALiZPuk5T62fHPnuEELWDtjy1n
iOqnr29DTUF++xwVVMmRJ1HZk+xIV/YaL6gnwIEXD7TJ2cCGKvzN9g7+/aNQCw3T
IugC5UB95S9i/Z6hpacT6Pon8yYI9l9CAJP5AFQb0KljJUcqqetVPqyknrp4L5Qd
ViPoTvPp3s1DWFmXt5UWmVBzFUfPfmpbF6WDOIGc81Ph4l6Cltyul52druHZM0dE
ZZC94aK3AWGhncfSMlG+LmiOTwPnSeM4I0y/hvAf8oxe9M27uaeZ8L7ew5mxwsyu
LmVJzvc0PybZOOgPTjawE6afKSO9r0qscsmsUFhvplg7cWHP/T0TA5cjQ2ES
-----END CERTIFICATE-----
    ";
		
		AndroidHttpsHelper.AddCertificate(cert1);

		WWWForm form = new WWWForm();
		form.AddField("data",json);
		Dictionary<string,string> headers = form.headers;
		byte[] rawData = form.data;
		headers["Authorization"] = "Bearer " + access_token + SystemInfo.deviceUniqueIdentifier;
		WWW download = new WWW(post_url + url, rawData, headers);
		yield return download;
		Debug.Log (download.text);
		if(!string.IsNullOrEmpty(download.error)) {
			Debug.Log( "Error downloading: " + download.error );
		} else {
			if(download.text != ""){
				callback(download.text);
			}else{
				PostExport(false,"respuesta vacia");
			}
		}
	}

	void PostExport(bool status, string details = "") {
		transform.Find ("Sync").GetComponent<Button> ().interactable = true;
		if (status) {
			GUItest.AlertMsg ("Sincronización finalizada", "La sincronización finalizó con éxito");
			RegisterSync ();
		} else {
			GUItest.AlertMsg ("Sincronización finalizada", "Ocurrió un error con la sincronización ("+details+")");
		}
	}

	void RegisterSync(){
		DB db = new DB ();
		db.Connect ();
		//string query = "INSERT INTO sync (created,participantes_exportados,eventos_sincronizados,usuario_id) VALUES ('" + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',"+exported_users+","+synced_events+","+Main.logged_user["id"]+")";
		string logged_user = Main.logged_user ["id"].ToString();
		if (logged_user == "") {
			logged_user = "0";
		}
		string query = "INSERT INTO sync (created,participantes_exportados,eventos_sincronizados,usuario_id) VALUES ('" + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "',0,0,"+Main.logged_user["id"]+")";
		db.dbcmd.CommandText = query; db.dbcmd.ExecuteNonQuery (); db.Disconnect ();
		GetSyncStats ();
	}

	void GetSyncStats(){
		DB db = new DB();
		db.Connect ();

		string sqlQuery = "SELECT * FROM 'sync' ORDER BY id DESC LIMIT 1";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		while (db.reader.Read()) {
			transform.Find ("LastSync").Find ("Value").gameObject.GetComponent<Text>().text = db.reader.GetString(1);
			transform.Find ("SyncedUsers").Find ("Value").gameObject.GetComponent<Text>().text = db.reader.GetInt32(2).ToString();
			transform.Find ("SyncedEvents").Find ("Value").gameObject.GetComponent<Text>().text = db.reader.GetInt32(3).ToString();
		}
		db.reader.Close();

		sqlQuery = "SELECT COUNT(*) FROM participantes WHERE external_id IS NULL";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		while (db.reader.Read()) {
			if(!db.reader.IsDBNull(0)){
				transform.Find ("UnsyncedUsers").Find ("Value").gameObject.GetComponent<Text>().text = db.reader.GetInt32(0).ToString();
			}else{
				transform.Find ("UnsyncedUsers").Find ("Value").gameObject.GetComponent<Text>().text = "0";
			}
		}
		db.reader.Close();

		sqlQuery = "SELECT COUNT(*) FROM eventos WHERE external_id IS NULL";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		while (db.reader.Read()) {
			if(!db.reader.IsDBNull(0)){
				transform.Find ("UnsyncedEvents").Find ("Value").gameObject.GetComponent<Text>().text = db.reader.GetInt32(0).ToString();
			}else{
				transform.Find ("UnsyncedEvents").Find ("Value").gameObject.GetComponent<Text>().text = "0";
			}
		}
		db.reader.Close();

		db.reader = null;
		db.Disconnect ();
	}
}