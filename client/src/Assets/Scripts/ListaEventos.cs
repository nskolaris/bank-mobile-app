using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using DbConnection;

public class ListaEventos : MonoBehaviour {

	public GameObject eventos_panel;

	int current_event_id = 0;
	
	void Start () {}

	void OnEnable() {
		LlenarLista (Evento.GetAll());
	}

	public CreateBox create_box;

	public void Crear(){
		Hashtable fields = new Hashtable ();
		fields.Add (0, "Nombre"); fields.Add (1, "Ciudad");
		fields.Add (2, "Codigo");
		fields.Add (3,"Fecha Inicio"); fields.Add (4,"Fecha Final");
		create_box.action = "evento";
		create_box.title = "Crear nuevo evento";
		create_box.Fill(fields);
	}

	public void refreshList(){
		vaciarLista ();
		LlenarLista (Evento.GetAll());
	}

	void LlenarLista(Hashtable eventos){
		vaciarLista ();
		GameObject evento_template = eventos_panel.transform.Find("Evento").gameObject;

		evento_template.SetActive (false);
		float text_height = evento_template.GetComponent<RectTransform> ().rect.height;

		float height_sum = 0;

		foreach (DictionaryEntry evento in eventos) {
			Hashtable evento_values = (Hashtable) evento.Value;
			GameObject evento_object = Instantiate(evento_template.gameObject) as GameObject;

			evento_object.transform.SetParent(evento_template.transform.parent);
			evento_object.GetComponent<RectTransform>().sizeDelta = new Vector2(0f,text_height);
			evento_object.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f,-(text_height/2)-(text_height * evento.Key.GetHashCode()));

			evento_object.transform.Find("Text").gameObject.GetComponent<Text>().text = evento_values["nombre"].ToString();
			evento_object.GetComponent<Evento>().id = (int)evento_values["local_id"];
			evento_object.SetActive(true);

			height_sum += text_height;
		}

		float panel_height = eventos_panel.GetComponent<RectTransform> ().rect.height;
		if(height_sum > panel_height){
			eventos_panel.GetComponent<RectTransform> ().sizeDelta = new Vector2(0f, height_sum);
		}
	}

	void vaciarLista(){
		Evento[] eventos = eventos_panel.GetComponentsInChildren<Evento>();
		foreach (Evento target in eventos) {
			if(target.id != 0){
				GameObject.Destroy(target.gameObject);
			}
		}
	}

	public void SelectEvento(int id){
		transform.Find ("HacerActual").GetComponent<Button> ().interactable = true;
		current_event_id = id;
	}

	public void HacerActual(){
		Hashtable evento = Evento.Get (current_event_id);
		Evento.CambiarActivo (evento["code"].ToString());
		Configuracion config = transform.parent.GetComponentInParent<Configuracion>();
		if(config != null){
			config.FillDataEvento();
		}
	}
}