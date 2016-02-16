using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using System.Collections;

public class CreateBox : MonoBehaviour {
	
	public int state = 0;
	public string action = "";
	Vector3 initial_scale;
	GameObject overlay;
	GameObject container;

	ComboBox combo_box;
	
	public string title = "Titulo";
	
	void Start () {
		overlay = transform.parent.Find("OverlayCreate").gameObject;
		EventTrigger eventTrigger = overlay.GetComponent<EventTrigger>();
		EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
		trigger.AddListener((PointerEventData) => {Toggle();});
		EventTrigger.Entry entry = new EventTrigger.Entry()
		{ callback = trigger, eventID = EventTriggerType.PointerClick };
		eventTrigger.triggers.Clear ();
		eventTrigger.triggers.Add(entry);

		combo_box = GameObject.FindGameObjectWithTag ("ComboBox").GetComponent<ComboBox>();
		container = gameObject.transform.Find ("Container").gameObject;
		initial_scale = GetComponent<RectTransform> ().localScale;
		gameObject.SetActive (false);
	}
	
	void Update () {
		if (state == 1) {
			float y = GetComponent<RectTransform> ().localScale.y;
			GetComponent<RectTransform> ().localScale = new Vector3 (initial_scale.x,Mathf.Lerp(y, initial_scale.y, 0.25f),initial_scale.z);
			if(GetComponent<RectTransform> ().localScale.y > initial_scale.y - 0.05f){
				GetComponent<RectTransform> ().localScale = initial_scale;
				state = 2;
			}
		}else if(state == 3) {
			float y = GetComponent<RectTransform> ().localScale.y;
			GetComponent<RectTransform> ().localScale = new Vector3 (initial_scale.x,Mathf.Lerp(y, 0f, 0.25f),initial_scale.z);
			if(GetComponent<RectTransform> ().localScale.y < 0.05f){
				GetComponent<RectTransform> ().localScale = new Vector3 (initial_scale.x,0f,initial_scale.z);
				gameObject.SetActive (false);
				state = 0;
			}
		}
	}
	
	void Toggle(){
		if (state == 0) {
			GetComponent<RectTransform> ().localScale = new Vector3 (initial_scale.x,0f,initial_scale.z);
			gameObject.SetActive (true);
			transform.Find ("Titulo").gameObject.GetComponent<Text>().text = title;
			overlay.SetActive(true);
			state = 1;
		}else if (state == 2) {
			Empty();
			overlay.SetActive(false);
			state = 3;
		}
	}
	
	public void Fill(Hashtable fields){
		GameObject input_template = container.transform.Find ("InputField").gameObject;
		GameObject input_template_fecha = container.transform.Find ("InputFecha").gameObject;
		input_template.SetActive (false);
		input_template_fecha.SetActive (false);
		
		float height_input_template = input_template.GetComponent<RectTransform> ().anchorMax.y - input_template.GetComponent<RectTransform> ().anchorMin.y;
		float height_input_template_fecha = input_template_fecha.GetComponent<RectTransform> ().anchorMax.y - input_template_fecha.GetComponent<RectTransform> ().anchorMin.y;
		int i = 0;

		float last_height = 0;

		foreach (DictionaryEntry field in fields) {

			GameObject input_object;
			bool is_fecha = false;

			float height;

			if(field.Value.ToString().Contains("Fecha")){
				input_object = Instantiate(input_template_fecha.gameObject) as GameObject;
				is_fecha = true;
				height = 0.2f;
			}else{
				input_object = Instantiate(input_template.gameObject) as GameObject;
				height = 0.1f;
			}

			input_object.transform.SetParent(input_template.transform.parent);
			input_object.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
			input_object.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			Vector2 anchor_max = input_object.GetComponent<RectTransform>().anchorMax;
			Vector2 anchor_min = input_object.GetComponent<RectTransform>().anchorMin;
			anchor_max.y = anchor_max.y - (last_height) - (0.01f * i);
			anchor_min.y = anchor_max.y - height;
			input_object.GetComponent<RectTransform>().anchorMax = anchor_max;
			input_object.GetComponent<RectTransform>().anchorMin = anchor_min;
			input_object.GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);

			if(is_fecha){
				input_object.transform.Find("Placeholder").gameObject.GetComponent<Text>().text = field.Value.ToString();

				EventTrigger eventTrigger_dia = input_object.transform.Find("Dia").gameObject.GetComponent<EventTrigger>();
				EventTrigger.TriggerEvent trigger_dia = new EventTrigger.TriggerEvent();
				trigger_dia.AddListener((PointerEventData) => {
					combo_box.related_field = input_object.transform.Find("Dia").gameObject;
					combo_box.title = "Día";
					int[] dias = Enumerable.Range (1, 31).ToArray ();
					combo_box.FillFromInts (dias, "");
				});
				EventTrigger.Entry entry_dia = new EventTrigger.Entry()
				{ callback = trigger_dia, eventID = EventTriggerType.PointerClick };
				eventTrigger_dia.triggers.Clear ();
				eventTrigger_dia.triggers.Add(entry_dia);

				EventTrigger eventTrigger_mes = input_object.transform.Find("Mes").gameObject.GetComponent<EventTrigger>();
				EventTrigger.TriggerEvent trigger_mes = new EventTrigger.TriggerEvent();
				trigger_mes.AddListener((PointerEventData) => {
					combo_box.related_field = input_object.transform.Find("Mes").gameObject;
					combo_box.title = "Mes";
					int[] meses = Enumerable.Range (1, 12).ToArray ();
					combo_box.FillFromInts (meses, "");;
				});
				EventTrigger.Entry entry_mes = new EventTrigger.Entry()
				{ callback = trigger_mes, eventID = EventTriggerType.PointerClick };
				eventTrigger_mes.triggers.Clear ();
				eventTrigger_mes.triggers.Add(entry_mes);

				EventTrigger eventTrigger_ano = input_object.transform.Find("Ano").gameObject.GetComponent<EventTrigger>();
				EventTrigger.TriggerEvent trigger_ano = new EventTrigger.TriggerEvent();
				trigger_ano.AddListener((PointerEventData) => {
					combo_box.related_field = input_object.transform.Find("Ano").gameObject;
					combo_box.title = "Año";
					int[] years = new int[] {2015,2016,2017,2018,2019,2020};
					combo_box.FillFromInts (years, "");
				});
				EventTrigger.Entry entry_ano = new EventTrigger.Entry()
				{ callback = trigger_ano, eventID = EventTriggerType.PointerClick };
				eventTrigger_ano.triggers.Clear ();
				eventTrigger_ano.triggers.Add(entry_ano);

				last_height += height_input_template_fecha;

			}else{
				input_object.GetComponent<InputField>().placeholder.GetComponent<Text>().text = field.Value.ToString();

				last_height += height_input_template;
			}

			input_object.SetActive(true);

			i ++;
		}
		Toggle ();
	}
	
	void Empty(){
		InputField[] inputs = container.GetComponentsInChildren<InputField>();
		foreach (InputField target in inputs) {
			GameObject.Destroy(target.gameObject);
		}
	}

	public void Save(){
		InputField[] fields = GetComponentsInChildren<InputField> ();
		Hashtable values = new Hashtable();
		int i = 0;
		string fecha_inicio = "";
		string fecha_final = "";
		foreach (InputField field in fields) {
			if(field.text != ""){
				switch (action) {
				case "promotora":
					if(i == 4){
						if(field.text != values[3].ToString()){
							GUItest.AlertMsg("Error","Las contraseñas no coinciden");
							return;
						}
					}else{
						values.Add(i,field.text);
						i++;
					}
					break;
				case "evento":
					if(i > 2 && i < 6){
						if(i < 5){fecha_inicio = "-" + field.text + fecha_inicio;}else{
							fecha_inicio = field.text + fecha_inicio;
						}
					}else if(i > 5){
						if(i < 8){fecha_final = "-" + field.text + fecha_final;}else{
							fecha_final = field.text + fecha_final;
						}
					}else{
						values.Add(i,field.text);
					}
					i++;
					break;
				}
			}else{
				GUItest.AlertMsg("Error","Debe llenar todos los campos");
				return;
			}
		}
		if (fecha_inicio != "") {
			values.Add(3,fecha_inicio);
		}
		if (fecha_final != "") {
			values.Add(4,fecha_final);
		}
		switch (action) {
		case "promotora":
			if (Promotora.Save (values)) {
				Configuracion config = transform.GetComponentInParent<Configuracion>();
				if(config != null){
					config.FillDataPromotora();
				}
				Toggle ();
				GUItest.AlertMsg("Éxito","La promotora fue agregada y activada correctamente");
			}
			break;
		case "evento":
			if (Evento.Save (values)) {
				ListaEventos lista = transform.parent.Find("Tabs").GetComponentInChildren<ListaEventos>();
				Debug.Log (lista);
				if(lista != null){
					lista.refreshList();
				}
				Toggle ();
				GUItest.AlertMsg("Éxito","El evento fue agregado correctamente");
			}
			break;
		}

	}
}
