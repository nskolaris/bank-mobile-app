using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ComboBox : MonoBehaviour {

	public int state = 0;
	Vector3 initial_scale;
	GameObject overlay;
	GameObject scroll_rect;

	public string title = "Titulo";
	public GameObject related_field;

	float panel_height;

	void Start () {
		/*overlay = transform.parent.parent.Find("Overlay").gameObject;
		EventTrigger eventTrigger = overlay.GetComponent<EventTrigger>();
		EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
		trigger.AddListener((PointerEventData) => {Toggle();});
		EventTrigger.Entry entry = new EventTrigger.Entry()
		{ callback = trigger, eventID = EventTriggerType.PointerClick };
		eventTrigger.triggers.Clear ();
		eventTrigger.triggers.Add(entry);*/

		scroll_rect = transform.Find ("ScrollRect").gameObject;
		initial_scale = GetComponent<RectTransform> ().localScale;
		transform.parent.parent.gameObject.SetActive (false);
		GameObject options_panel = scroll_rect.transform.Find ("Options").gameObject;
		panel_height = options_panel.GetComponent<RectTransform> ().rect.height;
	}

	void Update () {
		if (state == 1) {
			float y = GetComponent<RectTransform> ().localScale.y;
			GetComponent<RectTransform> ().localScale = new Vector3 (initial_scale.x,Mathf.Lerp(y, initial_scale.y, 0.25f),initial_scale.z);
			if(GetComponent<RectTransform> ().localScale.y > initial_scale.y - 0.05f){
				GetComponent<RectTransform> ().localScale = initial_scale;
				state = 2;
				AutoSetSelected ();
			}
		}else if(state == 3) {
			float y = GetComponent<RectTransform> ().localScale.y;
			GetComponent<RectTransform> ().localScale = new Vector3 (initial_scale.x,Mathf.Lerp(y, 0f, 0.25f),initial_scale.z);
			if(GetComponent<RectTransform> ().localScale.y < 0.05f){
				GetComponent<RectTransform> ().localScale = new Vector3 (initial_scale.x,0f,initial_scale.z);
				gameObject.SetActive (false);
				transform.parent.parent.gameObject.SetActive (false);
				state = 0;
			}
		}
	}

	void Toggle(){
		if (state == 0) {
			GetComponent<RectTransform> ().localScale = new Vector3 (initial_scale.x,0f,initial_scale.z);
			gameObject.SetActive (true);
			transform.parent.parent.gameObject.SetActive (true);
			transform.Find ("Titulo").gameObject.GetComponent<Text>().text = title;
			state = 1;
		}else if (state == 2) {
			Empty();
			state = 3;
		}
	}

	public void FillFromInts(int[] ints, string string_attach){
		Hashtable combo = new Hashtable ();
		int i = 0;
		foreach(int numero in ints) {
			Hashtable combo_item = new Hashtable ();
			combo_item.Add("id",numero);
			string nombre = numero + string_attach;
			if(string_attach == "" && numero < 10){
				nombre = "0"+numero;
			}
			combo_item.Add("nombre",nombre);
			combo.Add (i,combo_item);
			i++;
		}
		Fill (combo);
	}

	public void Fill(Hashtable options){
		GameObject options_panel = scroll_rect.transform.Find ("Options").gameObject;

		GameObject option_template = scroll_rect.transform.Find("Options").Find ("Option").gameObject;
		option_template.SetActive (false);
		
		float text_height = option_template.GetComponent<RectTransform> ().rect.height;
		float height_sum = 0;
		
		foreach (DictionaryEntry option in options) {
			Hashtable option_values = (Hashtable) option.Value;
			GameObject option_object = Instantiate(option_template.gameObject) as GameObject;
			
			option_object.transform.SetParent(option_template.transform.parent);
			option_object.GetComponent<RectTransform>().sizeDelta = new Vector2(0f,text_height);
			option_object.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f,-(text_height/2)-(text_height * option.Key.GetHashCode()));
			option_object.GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);
			option_object.transform.Find("Text").gameObject.GetComponent<Text>().text = option_values["nombre"].ToString();
			//option_object.GetComponentInChildren<Text>().text = option_values["nombre"].ToString();
			option_object.GetComponent<Option>().id = option_values["id"].ToString();
			option_object.GetComponent<Option>().nombre = option_values["nombre"].ToString();
			option_object.SetActive(true);
			
			height_sum += text_height;
		}

		if (height_sum > panel_height) {
			options_panel.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0f, height_sum);
		} else {
			options_panel.GetComponent<RectTransform> ().offsetMax = new Vector2(0,-((panel_height-height_sum)/2)+20f);
		}
		Toggle ();
	}
	
	void Empty(){
		GameObject options_panel = scroll_rect.transform.Find ("Options").gameObject;
		Option[] options = options_panel.GetComponentsInChildren<Option>();
		foreach (Option target in options) {
			if(target.id != "null"){
				GameObject.Destroy(target.gameObject);
			}
		}
	}

	string selected_id;
	string selected_nombre;

	void AutoSetSelected(){
		string id = "";
		if (related_field.GetComponentInChildren<InputField> ()) {
			id = related_field.GetComponentInChildren<InputField> ().text;
		} else if(related_field.transform.Find ("ID")){
			id = related_field.transform.Find ("ID").gameObject.GetComponent<InputField>().text;
		} else if(related_field.transform.Find ("Value")){
			id = related_field.transform.Find ("Value").gameObject.GetComponent<Text>().text;
		}
		if(id != "" && id != null){
			SetSelected (id.TrimStart('0'));
		}
	}

	public void SetSelected(string id){
		GameObject options_panel = scroll_rect.transform.Find ("Options").gameObject;
		Option[] options = options_panel.GetComponentsInChildren<Option>();
		foreach (Option target in options) {
			if(target.id == id){
				target.Select();
			}
		}
	}

	public void ReportClick(string id, string nombre){
		selected_id = id;
		selected_nombre = nombre;
		/*if (related_field.GetComponentInChildren<InputField> ()) {
			related_field.GetComponentInChildren<InputField> ().text = nombre;
		} else if(related_field.transform.Find ("ID")){
			related_field.transform.Find ("Activo").gameObject.GetComponent<Text>().text = nombre;
			related_field.transform.Find ("ID").gameObject.GetComponent<InputField>().text = id;
		} else if(related_field.transform.Find ("Value")){
			related_field.transform.Find ("Button").gameObject.GetComponentInChildren<Text>().text = nombre;
			related_field.transform.Find ("Value").gameObject.GetComponent<Text>().text = id;
		}
		Toggle ();*/
	}

	public void Accept(){
		Configuracion config = related_field.GetComponentInParent<Configuracion> ();
		if (related_field.GetComponentInChildren<InputField> ()) {
			if(related_field.GetComponentInChildren<InputField> ().text != selected_nombre){
				if(config!=null){config.unsaved_changes = true;}
				related_field.GetComponentInChildren<InputField> ().text = selected_nombre;
			}
		} else if(related_field.transform.Find ("ID")){
			if(related_field.transform.Find ("Activo").gameObject.GetComponent<Text>().text != selected_nombre){
				if(config!=null){config.unsaved_changes = true;}
				related_field.transform.Find ("Activo").gameObject.GetComponent<Text>().text = selected_nombre;
			}
			if(related_field.transform.Find ("ID").gameObject.GetComponent<InputField>().text != selected_id){
				if(config!=null){config.unsaved_changes = true;}
				related_field.transform.Find ("ID").gameObject.GetComponent<InputField>().text = selected_id;
			}
		} else if(related_field.transform.Find ("Value")){
			if(related_field.transform.Find ("Button").gameObject.GetComponentInChildren<Text>().text != selected_nombre){
				if(config!=null){config.unsaved_changes = true;}
				related_field.transform.Find ("Button").gameObject.GetComponentInChildren<Text>().text = selected_nombre;
			}
			if(related_field.transform.Find ("Value").gameObject.GetComponent<Text>().text != selected_id){
				if(config!=null){config.unsaved_changes = true;}
				related_field.transform.Find ("Value").gameObject.GetComponent<Text>().text = selected_id;
			}
		}
		Toggle ();
	}
}
