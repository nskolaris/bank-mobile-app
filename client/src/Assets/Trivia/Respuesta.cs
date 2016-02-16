using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using DbConnection;

public class Respuesta : MonoBehaviour, IPointerClickHandler {

	int id = 0;
	string respuesta = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerClick (PointerEventData eventData) {
		if (transform.parent.parent.gameObject.GetComponent<Trivia> ().can_click_respuesta) {
			transform.parent.parent.gameObject.GetComponent<Trivia> ().can_click_respuesta = false;
			transform.parent.parent.gameObject.GetComponent<Trivia> ().SelectRespuesta (id);
			GetComponent<Image> ().color = new Color32 (255, 255, 255, 146);
			transform.localScale = new Vector3 (1.1f, 1.1f, 1f);
		}
	}

	public void SetData(Hashtable data){
		id = (int)data["id"];
		gameObject.name = "Respuesta" + id.ToString ();
		respuesta = data["respuesta"].ToString();
		GetComponentInChildren<Text> ().text = respuesta;
	}
	
	public static Hashtable GetAllByPreguntaId(int pregunta_id){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT * FROM respuestas WHERE pregunta_id = "+pregunta_id.ToString();
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable respuestas = new Hashtable ();
		int index = 0;
		while (db.reader.Read()) {
			Hashtable respuesta = new Hashtable ();
			respuesta.Add ("id",db.reader.GetInt32(0));
			respuesta.Add ("respuesta",db.reader.GetString(1));
			respuestas.Add (index, respuesta);
			index ++;
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return respuestas;
	}
}
