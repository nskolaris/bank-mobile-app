using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DbConnection;

public class Trivia : MonoBehaviour {

	int active_pregunta_index = 0;
	GameObject active_pregunta;
	int id_respuesta = 0;

	public bool can_click_respuesta = true;

	GameObject respuestas_container;
	float respuestas_container_movement;

	public float preguntas_margin = 0.1f;
	public PopupCanvas popup_canvas;

	// Use this for initialization
	void Start () {
		respuestas_container = transform.Find ("Respuestas").gameObject;
		InitializePreguntas ();
		NextPregunta ();
	}
	
	// Update is called once per frame
	void Update () {}

	void InitializePreguntas(){
		RemovePreguntas ();
		GameObject pregunta_template = transform.Find ("Pregunta").gameObject;
		Hashtable preguntas = Pregunta.GetAll ();
		int i = 1;
		foreach (DictionaryEntry pregunta in preguntas) {
			Hashtable pregunta_values = (Hashtable) pregunta.Value;
			pregunta_values["count"] = preguntas.Count;
			GameObject pregunta_object = Instantiate(pregunta_template) as GameObject;
			pregunta_object.transform.SetParent(transform);
			pregunta_object.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
			pregunta_object.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			pregunta_object.GetComponent<Pregunta>().SetData(pregunta_values,i);
			i++;
		}
		pregunta_template.SetActive (false);
	}

	public void NextPregunta(){
		//if (next_pregunta_available) {
			active_pregunta_index++;
			if (transform.Find ("Pregunta" + active_pregunta_index)) {
				TogglePostPregunta (false);
				
				if (transform.Find ("Pregunta" + (active_pregunta_index - 1))) {
					GameObject active_pregunta_anterior = transform.Find ("Pregunta" + (active_pregunta_index -1)).gameObject;
					HidePregunta(active_pregunta_anterior);
				}else{
					HidePreguntas();
				}
				
				active_pregunta = transform.Find ("Pregunta" + active_pregunta_index).gameObject;
				ShowPregunta(active_pregunta);
				showRespuestas ();
			} else {
				StartCoroutine(Fin ());
			}
		//}
	}

	IEnumerator Fin(){
		yield return new WaitForSeconds(0);
		popup_canvas.Show();
		popup_canvas.ShowChild("Fin");
		popup_canvas.ShowChild("Siguiente");
	}

	public void NextScene(){
		if (Main.GetConfig ("premios_activos").ToString () == "True") {
			Main.times_load_ruleta = 3;
			StartCoroutine (Main.LoadLevel ("ruleta", 0));
		} else {
			Main.Home();
		}
	}

	void ShowPregunta(GameObject pregunta){
		pregunta.SetActive (true);
		pregunta.gameObject.GetComponent<PanelMovement> ().Offset(new Vector2(-pregunta.gameObject.GetComponent<RectTransform>().rect.width,0));
		pregunta.gameObject.GetComponent<PanelMovement> ().move (
			new Vector2(0,0),
			7f
		);
	}

	void HidePregunta(GameObject pregunta){
		pregunta.gameObject.GetComponent<PanelMovement> ().Offset(new Vector2(0,0));
		pregunta.gameObject.GetComponent<PanelMovement> ().move (
			new Vector2(-pregunta.gameObject.GetComponent<RectTransform>().rect.width,0),
			7f
		);
	}

	void HidePreguntas(){
		Pregunta[] preguntas = GetComponentsInChildren<Pregunta> ();
		foreach (Pregunta pregunta in preguntas) {
			pregunta.gameObject.SetActive(false);
		}

	}

	void RemovePreguntas(){
		Pregunta[] preguntas = GetComponentsInChildren<Pregunta> ();
		foreach (Pregunta pregunta in preguntas) {
			if(pregunta.gameObject.name != "Pregunta"){
				GameObject.Destroy(pregunta.gameObject);
			}
		}
	}

	//Funciones objeto PostPregunta

	//bool next_pregunta_available = true;

	void TogglePostPregunta(bool on){
		GameObject post_pregunta = transform.Find ("PostPregunta").gameObject;

		Transform porcentaje = post_pregunta.transform.Find("Porcentaje");
		Transform siguiente = post_pregunta.transform.Find("Siguiente");

		if (on) {
			//next_pregunta_available = false;
			post_pregunta.SetActive (true);
			porcentaje.Find ("Number").gameObject.GetComponent<Text> ().text = GetPorcentajeRespuestasIguales ().ToString () + "%";

			porcentaje.gameObject.GetComponent<PanelMovement> ().Offset(new Vector2(0,porcentaje.gameObject.GetComponent<RectTransform>().rect.height));
			porcentaje.gameObject.GetComponent<PanelMovement> ().move (
				new Vector2(0,0),
				6f,
				afterShowPostPregunta
			);

			StartCoroutine(ShowNextButton());

		} else {
			porcentaje.gameObject.GetComponent<PanelMovement> ().Offset(new Vector2(0,0));
			porcentaje.gameObject.GetComponent<PanelMovement> ().move (
				new Vector2(porcentaje.gameObject.GetComponent<RectTransform>().rect.width,0),
				6f,
				afterHidePostPregunta
			);
			
			siguiente.gameObject.GetComponent<PanelMovement> ().Offset(new Vector2(0,0));
			siguiente.gameObject.GetComponent<PanelMovement> ().move (
				new Vector2(siguiente.gameObject.GetComponent<RectTransform>().rect.width,0),
				6f
			);
		}
	}

	IEnumerator ShowNextButton(){
		yield return new WaitForSeconds(1);
		GameObject post_pregunta = transform.Find ("PostPregunta").gameObject;
		Transform siguiente = post_pregunta.transform.Find("Siguiente");
		siguiente.gameObject.GetComponent<PanelMovement> ().Offset(new Vector2(0,-siguiente.gameObject.GetComponent<RectTransform>().rect.height));
		siguiente.gameObject.GetComponent<PanelMovement> ().move (
			new Vector2(0,0),
			6f
		);
	}

	void afterShowPostPregunta(){
		//next_pregunta_available = true;
	}

	void afterHidePostPregunta(){
		GameObject post_pregunta = transform.Find ("PostPregunta").gameObject;
		post_pregunta.SetActive (false);
	}

	//Funciones objeto respuestas
	
	void showRespuestas(){
		InitializeRespuestas();
		can_click_respuesta = true;
		respuestas_container.gameObject.GetComponent<PanelMovement> ().Offset(new Vector2(respuestas_container.gameObject.GetComponent<RectTransform>().rect.width,0));
		respuestas_container.gameObject.GetComponent<PanelMovement> ().move (
			new Vector2(0,0),
			7f,
			afterShowRespuestas
		);
	}

	void afterShowRespuestas(){
		//can_click_respuesta = true;
	}

	IEnumerator hideRespuestas(){
		yield return new WaitForSeconds(1);
		respuestas_container.gameObject.GetComponent<PanelMovement> ().Offset(new Vector2(0,0));
		respuestas_container.gameObject.GetComponent<PanelMovement> ().move (
			new Vector2(respuestas_container.gameObject.GetComponent<RectTransform>().rect.width,0),
			10f//,
			//RemoveRespuestas
		);
		StartCoroutine (RemoveRespuestasForSure ());
		TogglePostPregunta (true);
	}

	void InitializeRespuestas(){
		GameObject respuesta_template = respuestas_container.transform.Find ("Respuesta").gameObject;
		respuesta_template.SetActive (true);
		float respuesta_template_anchor_height = respuesta_template.GetComponent<RectTransform> ().anchorMax.y - respuesta_template.GetComponent<RectTransform> ().anchorMin.y;
		Hashtable respuestas = Respuesta.GetAllByPreguntaId (active_pregunta.GetComponent<Pregunta>().id);
		float total_height = ((int)respuestas.Count * respuesta_template_anchor_height) + ((int)respuestas.Count * preguntas_margin);
		float top = 0.5f + total_height / 2;
		foreach (DictionaryEntry respuesta in respuestas) {
			Hashtable respuesta_values = (Hashtable) respuesta.Value;
			GameObject respuesta_object = Instantiate(respuesta_template) as GameObject;
			respuesta_object.transform.SetParent(respuestas_container.transform);
			respuesta_object.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
			respuesta_object.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			float posy = top - (float.Parse(respuesta.Key.ToString())*respuesta_template_anchor_height) - (preguntas_margin*float.Parse(respuesta.Key.ToString()));
			respuesta_object.GetComponent<RectTransform>().anchorMax = new Vector2(respuesta_template.GetComponent<RectTransform> ().anchorMax.x,posy);
			respuesta_object.GetComponent<RectTransform>().anchorMin = new Vector2(respuesta_template.GetComponent<RectTransform> ().anchorMin.x,posy-respuesta_template_anchor_height);
			respuesta_object.GetComponent<Respuesta>().SetData(respuesta_values);
		}
		respuesta_template.SetActive (false);
	}

	void RemoveRespuestas(){
		Respuesta[] respuestas = respuestas_container.GetComponentsInChildren<Respuesta> ();
		foreach (Respuesta respuesta in respuestas) {
			if(respuesta.gameObject.name != "Respuesta"){
				GameObject.Destroy(respuesta.gameObject);
			}
		}
	}

	IEnumerator RemoveRespuestasForSure(){
		yield return new WaitForSeconds(1);
		Respuesta[] respuestas = respuestas_container.GetComponentsInChildren<Respuesta> ();
		foreach (Respuesta respuesta in respuestas) {
			if(respuesta.gameObject.name != "Respuesta"){
				GameObject.Destroy(respuesta.gameObject);
			}
		}
	}
	
	public void SelectRespuesta(int id){
		id_respuesta = id; DB db = new DB (); db.Connect ();
		string fields = "participante_id,pregunta_id,respuesta_id";
		string values = "" + Main.participante_id + "," + active_pregunta.GetComponent<Pregunta>().id + "," + id_respuesta;
		string sqlQuery = "INSERT INTO respuestas_participantes(" + fields + ") VALUES (" + values + ")";
		db.dbcmd.CommandText = sqlQuery;
		if(db.dbcmd.ExecuteNonQuery () == 1){
			StartCoroutine(hideRespuestas());
		}
		db.Disconnect ();
	}

	float GetPorcentajeRespuestasIguales(){
		DB db = new DB(); db.Connect ();
		string sqlQuery = "SELECT COUNT(*) FROM respuestas_participantes WHERE respuesta_id = "+id_respuesta;
		db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader();
		int cantidad_iguales = 0;
		while (db.reader.Read()) {
			if(!db.reader.IsDBNull(0)){
				cantidad_iguales = db.reader.GetInt32(0);
			}
		}
		db.reader.Close();
		sqlQuery = "SELECT COUNT(*) FROM respuestas_participantes WHERE pregunta_id = "+active_pregunta.GetComponent<Pregunta>().id;
		db.dbcmd.CommandText = sqlQuery; db.reader = db.dbcmd.ExecuteReader();
		int cantidad_totales = 0;
		while (db.reader.Read()) {
			if(!db.reader.IsDBNull(0)){
				cantidad_totales = db.reader.GetInt32(0);
			}
		}
		db.reader.Close(); db.reader = null; db.Disconnect ();
		return ((cantidad_iguales * 100) / cantidad_totales);
	}
}
