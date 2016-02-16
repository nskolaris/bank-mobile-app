using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DbConnection;
//using Restifizer;

public class Export : MonoBehaviour {

	public string post_url;
	public string access_token;

	IEnumerator Post(string json) {

		WWWForm form = new WWWForm();

		form.AddField("data",json);
		Dictionary<string,string> headers = form.headers;
		byte[] rawData = form.data;

		headers["Authorization"] = "Bearer " + access_token + SystemInfo.deviceUniqueIdentifier;

		WWW download = new WWW(post_url, rawData, headers);

		yield return download;
		
		if(!string.IsNullOrEmpty(download.error)) {
			Debug.Log( "Error downloading: " + download.error );
		} else {
			ProcessResponse(download.text);
		}
	}

	void ProcessResponse(string response){
		if (response != "[]") {
			response = response.Trim (new Char[] { '{', '}' });
			string[] response_exploded = response.Split (',');

			DB db = new DB ();
			db.Connect ();

			foreach (string pair in response_exploded) {
				string[] key_value = pair.Split (':');
				string ext_id = key_value [1].Trim (new Char[]{'"'});
				string id = key_value [0].Trim (new Char[]{'"'});
				string query = "UPDATE participantes SET external_id = " + ext_id + " WHERE id = " + id;
				db.dbcmd.CommandText = query;
				if (db.dbcmd.ExecuteNonQuery () == 1) {
					Debug.Log ("Exportado participante id local " + id + " a la id externa " + ext_id);
				}
			}
			
			db.Disconnect ();
		} else {
			Debug.Log ("No se exportó ningun participante nuevo");
		}

		GameObject.Find ("Button Export").GetComponent<Button> ().interactable = true;
	}

	public void ExportData(){

		GameObject.Find ("Button Export").GetComponent<Button> ().interactable = false;
		
		DB db = new DB();
		db.Connect ();
		
		//Busco todos los participantes
		string sqlQuery = "SELECT * FROM participantes WHERE external_id IS NULL";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		
		Hashtable participantes = new Hashtable ();
		int index = 0;
		
		while (db.reader.Read()) {
			
			Hashtable participante = new Hashtable ();
			
			participante.Add ("local_id",db.reader.GetInt32(0));
			participante.Add ("nombre",db.reader.GetString(1));
			participante.Add ("apellido",db.reader.GetString(2));
			participante.Add ("email",db.reader.GetString(3));
			participante.Add ("telefono",db.reader.GetString(4));
			
			participantes.Add (index, participante);
			index ++;
		}
		
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		
		
		string json_participantes = JSON.JsonEncode(participantes);
		StartCoroutine(Post (json_participantes)); 
	}
}