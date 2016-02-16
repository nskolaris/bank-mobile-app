using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Text.RegularExpressions;
using DbConnection;

public class Registration : MonoBehaviour {
	
	bool acepta_recibir = true;
	bool acepta_bases = false;
	bool mayor_18 = false;

	Hashtable input_options = new Hashtable ();

	//Variables combo box
	public ComboBox combo_box;

	void Start () {
		LoadFields();
		//combo_box = transform.Find ("ComboBox").gameObject.GetComponent<ComboBox>();
	}

	void LoadFields(){
		GameObject field_template = transform.Find("Fields").Find("Field").gameObject;
		float height = field_template.GetComponent<RectTransform>().anchorMax.y - field_template.GetComponent<RectTransform>().anchorMin.y;
		float width = field_template.GetComponent<RectTransform>().anchorMax.x - field_template.GetComponent<RectTransform>().anchorMin.x;
		float margin_y = height/2;

		DB db = new DB(); db.Connect ();
		field_template.SetActive (false);
		
		//Leo los campos a usar
		string sqlQuery = "SELECT * FROM form_fields WHERE enabled = 1 ORDER BY orden ASC";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();

		GameObject last_field = new GameObject();

		int input_count = 0;

		while (db.reader.Read()) {
			string name = db.reader.GetString(1);
			string type = db.reader.GetString(2);
			string placeholder = db.reader.GetString(3);
			string validation = db.reader.GetString(6);

			GameObject field = Instantiate(field_template) as GameObject;
			field.transform.SetParent(transform.Find("Fields"));

			field.transform.Find("Label").Find ("Text").gameObject.GetComponent<Text>().text = placeholder;
			field.transform.Find("Input").gameObject.GetComponent<InputField>().placeholder.GetComponent<Text>().text = placeholder;

			switch(validation){
			case "email":
				field.transform.Find("Input").gameObject.GetComponent<InputField>().contentType = InputField.ContentType.EmailAddress;

				Navigation nav2 = field.transform.Find("Input").gameObject.GetComponent<InputField>().navigation;
				nav2.mode = Navigation.Mode.Explicit;
				//nav.selectOnDown = field.transform.Find("Input").gameObject.GetComponent<Selectable>();
				field.transform.Find("Input").gameObject.GetComponent<InputField>().navigation = nav2;
				break;
			case "phone":
				field.transform.Find("Input").gameObject.GetComponent<InputField>().contentType = InputField.ContentType.IntegerNumber;

				if(last_field != null){
					Navigation nav = last_field.transform.Find("Input").gameObject.GetComponent<InputField>().navigation;
					nav.mode = Navigation.Mode.Explicit;
					nav.selectOnDown = field.transform.Find("Input").gameObject.GetComponent<Selectable>();
					last_field.transform.Find("Input").gameObject.GetComponent<InputField>().navigation = nav;
				}
				break;
			case "dni":
				field.transform.Find("Input").gameObject.GetComponent<InputField>().contentType = InputField.ContentType.IntegerNumber;
				break;
			}

			field.transform.Find("ID").gameObject.GetComponent<Text>().text = name;
			field.transform.Find("Validation").gameObject.GetComponent<Text>().text = validation;

			field.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
			field.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

			float posy;
			float posx;
			if(input_count > 2){
				posy = 1f - ((input_count-3)*height) - (margin_y*(input_count-3));
				posx = 0.525f;
			}else{
				posy = 1f - (input_count*height) - (margin_y*input_count);
				posx = 0.025f;
			}
			field.GetComponent<RectTransform>().anchorMax = new Vector2(posx+width,posy);
			field.GetComponent<RectTransform>().anchorMin = new Vector2(posx,posy-height);

			field.tag = "RegisterFormInput";
			field.SetActive(true);
			input_count++;

			if(type == "select"){
				Hashtable options = GetSelectOptions(name);
				input_options.Add (name,options);
				EventTrigger eventTrigger = field.GetComponentInChildren<EventTrigger>();
				EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
				combo_box.title = placeholder;
				trigger.AddListener((PointerEventData) => {combo_box.related_field = field;combo_box.Fill(options);});
				EventTrigger.Entry entry = new EventTrigger.Entry()
				{ callback = trigger, eventID = EventTriggerType.PointerClick };
				eventTrigger.triggers.Add(entry);
				field.transform.Find("Input").gameObject.GetComponent<InputField>().interactable = false;
			}

			/*EventTrigger eventTrigger2 = field.GetComponentInChildren<EventTrigger>();
			EventTrigger.TriggerEvent trigger2 = new EventTrigger.TriggerEvent();
			trigger2.AddListener((PointerEventData) => {Debug.Log ("sas");});
			EventTrigger.Entry entry2 = new EventTrigger.Entry()
			{ callback = trigger2, eventID = EventTriggerType.PointerClick };
			eventTrigger2.triggers.Add(entry2);*/

			last_field = field;
		}
		
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
	}
	
	public void Enviar(){
		if (ValidatesRegistration ()) {
			GameObject.Find ("Siguiente").GetComponent<Button> ().interactable = false;

			DB db = new DB ();
			db.Connect ();

			GameObject[] inputs = GameObject.FindGameObjectsWithTag ("RegisterFormInput");

			string fields = "evento_code,acepta_beneficios,fecha_ingresado,";
			string values = "'" + Evento.GetActivoCode () + "','" + acepta_recibir + "','" + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")+ "',";

			foreach (GameObject input in inputs) {

				fields += input.transform.Find ("ID").gameObject.GetComponent<Text> ().text + ",";
				values += "'" + input.transform.Find ("Input").gameObject.GetComponent<InputField> ().text + "',";
			}

			char[] remove = {','};

			fields = fields.TrimEnd (remove);
			values = values.TrimEnd (remove);

			string sqlQuery = "INSERT INTO participantes(" + fields + ",promotora_username,juego_nombre) VALUES (" + values + ",'"+Promotora.GetActivoUsername()+"','"+Main.GetConfig("juego_nombre")+"')";
			db.dbcmd.CommandText = sqlQuery;
			if (db.dbcmd.ExecuteNonQuery () == 1) {
				Main.AfterRegister ();
			} else {
				Debug.Log ("error en la registracion");
			}

			GameObject.Find ("Siguiente").GetComponent<Button> ().interactable = true;
			db.Disconnect ();
		}
	}

	Hashtable GetSelectOptions(string field){
		DB db = new DB();
		db.Connect ();
		string sqlQuery = "SELECT * FROM " + field + "s ORDER BY nombre ASC";
		db.dbcmd.CommandText = sqlQuery;
		db.reader = db.dbcmd.ExecuteReader();
		Hashtable options = new Hashtable ();
		int i = 0;
		while (db.reader.Read()) {
			Hashtable option = new Hashtable ();
			option.Add("id",db.reader.GetInt32(0));
			option.Add("nombre",db.reader.GetString(1));
			options.Add(i,option);
			i++;
		}
		db.reader.Close();
		db.reader = null;
		db.Disconnect ();
		return options;
	}

	public void ToggleAceptaInfo(){
		if (acepta_recibir) {
			acepta_recibir = false;
		} else {
			acepta_recibir = true;
		}
	}

	public void ToggleAceptaBases(){
		if (acepta_bases) {
			acepta_bases = false;
		} else {
			acepta_bases = true;
		}
	}

	public void ToggleMayor(){
		if (mayor_18) {
			mayor_18 = false;
		} else {
			mayor_18 = true;
		}
	}

	bool ValidatesRegistration(){
		bool validates = true;

		if (!mayor_18) {
			validates = false;
			GUItest.AlertMsg("error","Debe ser mayor de 18 años");
		}

		if (!acepta_bases) {
			validates = false;
			GUItest.AlertMsg("error","Debe aceptar las bases y condiciones");
		}

		GameObject[] inputs = GameObject.FindGameObjectsWithTag ("RegisterFormInput");
		foreach (GameObject input in inputs) {
			input.transform.Find ("Input").gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
			string validation = input.transform.Find ("Validation").gameObject.GetComponent<Text> ().text;
			string value = input.transform.Find ("Input").gameObject.GetComponent<InputField> ().text;
			string nombre = input.transform.Find("Label").Find ("Text").gameObject.GetComponent<Text>().text;
			bool this_validates = true;

			if(!NotEmpty(value)){
				this_validates = false;
				GUItest.AlertMsg("error","El " + nombre + " no puede estar vacio");
			}

			switch(validation){
			case "text":
				if(!IsText(value)){
					this_validates = false;
					GUItest.AlertMsg("error","El " + nombre + " es invalido");
				}
				break;
			case "email":
				if(!IsValidEmail(value)){
					this_validates = false;
					GUItest.AlertMsg("error","El " + nombre + " es invalido");
				}
				break;
			case "phone":
				if(!IsValidPhone(value)){
					this_validates = false;
					GUItest.AlertMsg("error","El teléfono es invalido, 9 - 20 números");
				}
				break;
			case "dni":
				if(!IsValidDni(value)){
					this_validates = false;
					GUItest.AlertMsg("error","El DNI es invalido, 7-8 números");
				}
				break;
			}

			if(!this_validates){
				validates = false;
				input.transform.Find ("Input").gameObject.GetComponent<Image>().color = new Color32(255,174,174,255);
			}

		}

		return validates;
	}

	bool NotEmpty(string str){
		return (str != "");
	}

	bool IsText(string str){
		return Regex.IsMatch(str, @"^[a-zA-Z ]+$");
	}

	bool IsValidPhone(string phone){
		if (phone.Length >= 9 && phone.Length <= 20) {
			return true;
		} else {
			return false;
		}
	}

	bool IsValidDni(string dni){
		if (dni.Length >= 7 && dni.Length <= 8) {
			return true;
		} else {
			return false;
		}
	}

	bool IsValidEmail(string email)	{
		Regex rgx = new Regex(@"^[^@\s]+@[^@\s]+(\.[^@\s]+)+$");
		return rgx.IsMatch (email);
	}

	bool IsNumeric(string str){
		foreach (char c in str){
			if (c < '0' || c > '9')
				return false;
		}
		return true;
	}
}
