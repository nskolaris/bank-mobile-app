using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine;
using System.Collections;
//using SqliteDatabase;

namespace DbConnection{

	public class DB{

		public IDbConnection dbconn;
		public IDbCommand dbcmd;
		public IDataReader reader;

		MonoBehaviour main;

		string db_name = "macro";
		string filepath = "";

		public void Connect(){

			main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>();

			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer) {
				filepath = Application.dataPath + "/StreamingAssets/" + db_name;
			} else {
				filepath = Application.persistentDataPath + "/" + db_name;
			}

			if (!System.IO.File.Exists (filepath)) {
				main.StartCoroutine (LoadDBMethod (db_name));
			} else {
				string conn = "URI=file:" + filepath;
				dbconn = new SqliteConnection(conn);
				dbconn.Open();
				dbcmd = dbconn.CreateCommand();
			}
		}

		public void Disconnect(){
			dbcmd.Dispose();
			dbcmd = null;
			dbconn.Close();
			dbconn = null;
		}

		IEnumerator LoadDBMethod(string db_name) {
			WWW loadDB = new WWW("file://" +Application.streamingAssetsPath + db_name);
			yield return loadDB;
			if(!string.IsNullOrEmpty(loadDB.error)){
				GUItest.AlertMsg ("Error","Error downloading:" + loadDB.error);
			}else{
				if (loadDB.text != "") {
					System.IO.File.WriteAllBytes (filepath, loadDB.bytes);
					string conn = "URI=file:" + filepath;
					dbconn = new SqliteConnection (conn);
					dbconn.Open ();
					dbcmd = dbconn.CreateCommand ();
				}
			}
		}
	}

}