using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;

public class Configuracion : MonoBehaviour {

	GameObject general;
	bool initialized = false;

	public ComboBox combo_box;

	// Game Settings Transforms
	Transform juegos_settings;
	Transform settings_memotest;
	Transform settings_trivia;
	Transform settings_jumper;

	public bool unsaved_changes = false;

	void Start () {
		if (Main.logged_user ["id"] != null) {
			transform.Find ("StatusBar").Find ("ActiveUser").GetComponent<Text> ().text = Main.logged_user ["nombre_apellido"].ToString ();
		} else {
			transform.Find ("StatusBar").Find ("ActiveUser").GetComponent<Text> ().text = "";
		}
		general = transform.Find ("Tabs").Find ("General").gameObject;
		FillDataGeneral ();
		initialized = true;
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {Salir();}
	}

	public void Salir(){
		if (unsaved_changes) {
			GUItest.ConfirmWindow("Cambios sin guardar","Tiene cambios sin guardar, seguro desea salir?");
		} else {
			Main.Home();
		}
	}

	public void RegisterChanges(){
		unsaved_changes = true;
	}

	public void FillDataGeneral(){
		// Game Settings Transforms
		juegos_settings = general.transform.Find ("JuegoSettings");
		settings_memotest = juegos_settings.Find("Memotest");
		settings_trivia = juegos_settings.Find("Trivia");
		settings_jumper = juegos_settings.Find("Jumper");

		FillDataEvento ();
		FillDataPromotora ();

		string header_banco_id = Main.GetConfig("header_banco_id").ToString();
		general.transform.Find ("Header").Find ("ID").GetComponent<InputField> ().text = header_banco_id;
		if(int.Parse(header_banco_id) == 1){
			general.transform.Find ("Header").Find ("Activo").gameObject.GetComponent<Text> ().text = "Banco Macro";
		}else{
			general.transform.Find ("Header").Find ("Activo").gameObject.GetComponent<Text> ().text = "Banco Tucumán";
		}

		FillSettingsJuegos ();
		FillSettingsPremios ();
		FillSettingsFormulario ();

		if (!initialized) {
			unsaved_changes = false;
		}
	}

	public void FillDataEvento(){
		general.transform.Find ("Evento").Find ("Activo").GetComponent<Text> ().text = Evento.GetActivo () ["nombre"].ToString();
		general.transform.Find ("Evento").Find ("ID").GetComponent<InputField> ().text = Evento.GetActivo () ["code"].ToString();
	}

	public void FillDataPromotora(){
		general.transform.Find ("Promotora").Find ("Activo").GetComponent<Text> ().text = Promotora.GetActivo () ["nombre_apellido"].ToString();
		general.transform.Find ("Promotora").Find ("ID").GetComponent<InputField> ().text = Promotora.GetActivo () ["id"].ToString();
	}

	public void GuardarGeneral(){
		SaveSettingsJuegos();
		SaveSettingsPremios();
		SaveSettingsFormulario();
		unsaved_changes = false;
		GUItest.AlertMsg ("Cambios guardados","Los cambios fueron guardados correctamente","main");
	}

	//Funciones juegos

	void FillSettingsJuegos(){
		FillSettingsMemotest ();
		FillSettingsTrivia ();
		general.transform.Find("Juego").GetComponent<Toggle>().isOn = Convert.ToBoolean(Main.GetConfig("juego_activo"));
		string juego_nombre = Main.GetConfig("juego_nombre");
		if (juego_nombre == "jump") {
			juego_nombre = "Recorriendo el Pais";
		}
		general.transform.Find("Juego").Find("Activo").GetComponent<Text>().text = juego_nombre;
		general.transform.Find("Juego").Find("ID").GetComponent<InputField>().text = Main.GetConfig("juego_nombre");
	}

	void SaveSettingsJuegos(){
		Main.SaveConfig ("juego_activo", general.transform.Find ("Juego").GetComponent<Toggle>().isOn.ToString());
		Main.SaveConfig ("juego_nombre", general.transform.Find ("Juego").Find ("ID").GetComponent<InputField> ().text);
		SaveSettingsMemotest ();
		SaveSettingsTrivia ();
	}

	//Settings Memotest
	
	void FillSettingsMemotest(){
		bool disable_after = true;
		if (settings_memotest.gameObject.activeSelf) {
			disable_after = false;
		}
		settings_memotest.gameObject.SetActive (true);
		settings_memotest.Find ("Tiempo").Find ("Button").gameObject.GetComponentInChildren<Text> ().text = Main.GetConfig ("memotest_tiempo") + " segundos";
		settings_memotest.Find ("Tiempo").Find ("Value").gameObject.GetComponent<Text> ().text = Main.GetConfig ("memotest_tiempo");
		settings_memotest.Find ("GrupoID").Find ("Button").gameObject.GetComponentInChildren<Text> ().text = Memotest.GetGroupNameById(int.Parse(Main.GetConfig ("memotest_grupo_id")));
		settings_memotest.Find ("GrupoID").Find ("Value").gameObject.GetComponent<Text> ().text = Main.GetConfig ("memotest_grupo_id");
		if (disable_after) {
			settings_memotest.gameObject.SetActive (false);
		}
	}
	
	public void CambiarTiempoMemotest(){
		combo_box.related_field = settings_memotest.Find("Tiempo").gameObject;
		combo_box.title = "Memotest: Tiempo límite";
		combo_box.FillFromInts(new int[] {30,45,60}, " segundos");
	}
	
	public void CambiarGrupoMemotest(){
		combo_box.related_field = settings_memotest.Find("GrupoID").gameObject;
		combo_box.title = "Memotest: Grupo de imagenes activo";
		combo_box.Fill(Memotest.AllGroupsCombo());
	}
	
	void SaveSettingsMemotest(){
		Main.SaveConfig ("memotest_tiempo", settings_memotest.Find ("Tiempo").Find ("Value").gameObject.GetComponent<Text> ().text);
		Main.SaveConfig ("memotest_grupo_id", settings_memotest.Find ("GrupoID").Find ("Value").gameObject.GetComponent<Text> ().text);
	}
	
	//Settings Trivia

	void FillSettingsTrivia(){
		bool disable_after = true;
		if (settings_trivia.gameObject.activeSelf) {
			disable_after = false;
		}
		settings_trivia.gameObject.SetActive (true);
		settings_trivia.Find ("GrupoID").Find ("Button").gameObject.GetComponentInChildren<Text> ().text = Pregunta.GetGroupNameById(int.Parse(Main.GetConfig ("trivia_grupo_id")));
		settings_trivia.Find ("GrupoID").Find ("Value").gameObject.GetComponent<Text> ().text = Main.GetConfig ("trivia_grupo_id");
		if (disable_after) {
			settings_trivia.gameObject.SetActive (false);
		}
	}

	public void CambiarTiempoTrivia(){
		combo_box.related_field = settings_trivia.Find("Tiempo").gameObject;
		combo_box.title = "Trivia: Tiempo por pregunta";
		combo_box.FillFromInts(new int[] {25,35,45}, " segundos");
	}

	public void CambiarGrupoTrivia(){
		combo_box.related_field = settings_trivia.Find("GrupoID").gameObject;
		combo_box.title = "Trivia: Grupo de preguntas activo";
		combo_box.Fill(Pregunta.AllGroupsCombo());
	}

	void SaveSettingsTrivia(){
		Main.SaveConfig ("trivia_grupo_id", settings_trivia.Find ("GrupoID").Find ("Value").gameObject.GetComponent<Text> ().text);
	}
	
	public void onJuegosChangeValue(){
		unsaved_changes = true;
		bool enabled = general.transform.Find ("Juego").GetComponent<Toggle> ().isOn;
		general.transform.Find ("CambiarJuego").GetComponent<Button> ().interactable = enabled;
		InputField[] fields = juegos_settings.gameObject.GetComponentsInChildren<InputField> ();
		foreach (InputField field in fields) {
			field.interactable = enabled;
		}
		Button[] buttons = juegos_settings.gameObject.GetComponentsInChildren<Button> ();
		foreach (Button button in buttons) {
			button.interactable = enabled;
		}
	}

	public void CambiarJuego(){
		Hashtable juegos_combo = new Hashtable ();
		Hashtable memotest = new Hashtable ();
		memotest.Add("id", "memotest");
		memotest.Add ("nombre", "Memotest");
		juegos_combo.Add (0, memotest);
		Hashtable jumper = new Hashtable ();
		jumper.Add("id", "jump");
		jumper.Add ("nombre", "Recorriendo el Pais");
		juegos_combo.Add (1, jumper);
		Hashtable trivia = new Hashtable ();
		trivia.Add("id", "trivia");
		trivia.Add ("nombre", "Trivia");
		juegos_combo.Add (2, trivia);
		combo_box.related_field = general.transform.Find("Juego").gameObject;
		combo_box.title = "Cambiar juego activo";
		combo_box.Fill(juegos_combo);
		combo_box.SetSelected (general.transform.Find ("Juego").Find ("ID").GetComponent<InputField> ().text);
	}
	
	public void CambiarJuegoCallback(){
		string nombre_juego = general.transform.Find ("Juego").Find ("ID").GetComponent<InputField> ().text;
		general.transform.Find ("JuegoSettings").Find ("Memotest").gameObject.SetActive (false);
		general.transform.Find ("JuegoSettings").Find ("Trivia").gameObject.SetActive (false);
		general.transform.Find ("JuegoSettings").Find ("Jumper").gameObject.SetActive (false);
		switch (nombre_juego) {
		case "memotest":
			general.transform.Find ("JuegoSettings").Find ("Memotest").gameObject.SetActive (true);
			break;
		case "trivia":
			general.transform.Find ("JuegoSettings").Find ("Trivia").gameObject.SetActive (true);
			break;
		case "jump":
			general.transform.Find ("JuegoSettings").Find ("Jumper").gameObject.SetActive (true);
			break;
		}
	}

	//Funciones premios

	void FillSettingsPremios(){
		general.transform.Find("Premios").GetComponent<Toggle>().isOn = Convert.ToBoolean(Main.GetConfig("premios_activos"));
		general.transform.Find("CantidadPremios").GetComponent<InputField>().text = Main.GetConfig("cantidad_premios");
		string fecha_desde = Main.GetConfig ("premios_start_date");
		string fecha_hasta = Main.GetConfig ("premios_end_date");
		char[] delimiters = new char[] {'-',':',' '};
		string[] fecha_desde_exploded = fecha_desde.Split (delimiters);
		if (fecha_desde_exploded.Length > 1) {
			general.transform.Find ("FechaPremiosDesde").Find ("Fecha").Find ("Ano").gameObject.GetComponent<InputField> ().text = fecha_desde_exploded [0];
			general.transform.Find ("FechaPremiosDesde").Find ("Fecha").Find ("Mes").gameObject.GetComponent<InputField> ().text = fecha_desde_exploded [1];
			general.transform.Find ("FechaPremiosDesde").Find ("Fecha").Find ("Dia").gameObject.GetComponent<InputField> ().text = fecha_desde_exploded [2];
			general.transform.Find ("FechaPremiosDesde").Find ("Hora").Find ("Hora").gameObject.GetComponent<InputField> ().text = fecha_desde_exploded [3];
			general.transform.Find ("FechaPremiosDesde").Find ("Hora").Find ("Minuto").gameObject.GetComponent<InputField> ().text = fecha_desde_exploded [4];
		}
		string[] fecha_hasta_exploded = fecha_hasta.Split (delimiters);
		if (fecha_hasta_exploded.Length > 1) {
			general.transform.Find ("FechaPremiosHasta").Find ("Fecha").Find ("Ano").gameObject.GetComponent<InputField> ().text = fecha_hasta_exploded [0];
			general.transform.Find ("FechaPremiosHasta").Find ("Fecha").Find ("Mes").gameObject.GetComponent<InputField> ().text = fecha_hasta_exploded [1];
			general.transform.Find ("FechaPremiosHasta").Find ("Fecha").Find ("Dia").gameObject.GetComponent<InputField> ().text = fecha_hasta_exploded [2];
			general.transform.Find ("FechaPremiosHasta").Find ("Hora").Find ("Hora").gameObject.GetComponent<InputField> ().text = fecha_hasta_exploded [3];
			general.transform.Find ("FechaPremiosHasta").Find ("Hora").Find ("Minuto").gameObject.GetComponent<InputField> ().text = fecha_hasta_exploded [4];
		}
	}

	public void ShowFechaSelector(string field){
		Transform contenedor;
		bool enabled = general.transform.Find ("Premios").GetComponent<Toggle> ().isOn;
		if (enabled) {
			string[] field_exploded = field.Split (new string[] {"-"}, StringSplitOptions.None);
			string type = field_exploded [0];
			string field_location = field_exploded [1];

			if (field_location == "desde") {
				contenedor = general.transform.Find ("FechaPremiosDesde");
			} else {
				contenedor = general.transform.Find ("FechaPremiosHasta");
			}

			switch (type) {
			case "day":
				combo_box.related_field = contenedor.Find ("Fecha").Find ("Dia").gameObject;
				combo_box.title = "Día";
				int[] dias = Enumerable.Range (1, 31).ToArray ();
				combo_box.FillFromInts (dias, "");
				break;
			case "month":
				combo_box.related_field = contenedor.Find ("Fecha").Find ("Mes").gameObject;
				combo_box.title = "Mes";
				int[] meses = Enumerable.Range (1, 12).ToArray ();
				combo_box.FillFromInts (meses, "");
				break;
			case "year":
				combo_box.related_field = contenedor.Find ("Fecha").Find ("Ano").gameObject;
				combo_box.title = "Año";
				int[] years = new int[] {2015,2016,2017,2018,2019,2020};
				combo_box.FillFromInts (years, "");
				break;
			case "hour":
				combo_box.related_field = contenedor.Find ("Hora").Find ("Hora").gameObject;
				combo_box.title = "Hora";
				int[] hours = Enumerable.Range (0, 24).ToArray ();
				combo_box.FillFromInts (hours, "");
				break;
			case "minute":
				combo_box.related_field = contenedor.Find ("Hora").Find ("Minuto").gameObject;
				combo_box.title = "Minuto";
				int[] minutes = Enumerable.Range (0, 60).ToArray ();
				combo_box.FillFromInts (minutes, "");
				break;
			}
		}
	}

	public void onPremiosChangeValue(){
		unsaved_changes = true;
		bool enabled = general.transform.Find ("Premios").GetComponent<Toggle> ().isOn;
		general.transform.Find ("CantidadPremios").GetComponent<InputField> ().interactable = enabled;
		/*InputField[] fields = general.transform.Find ("FechaPremiosDesde").GetComponentsInChildren<InputField> ();
		foreach (InputField field in fields) {
			field.interactable = enabled;
		}
		fields = general.transform.Find ("FechaPremiosHasta").GetComponentsInChildren<InputField> ();
		foreach (InputField field in fields) {
			field.interactable = enabled;
		}*/
	}

	void SaveSettingsPremios(){
		Main.SaveConfig ("premios_activos", general.transform.Find("Premios").GetComponent<Toggle>().isOn.ToString());
		Main.SaveConfig ("cantidad_premios", general.transform.Find("CantidadPremios").GetComponent<InputField>().text);
		string fecha_desde = 
			general.transform.Find ("FechaPremiosDesde").Find ("Fecha").Find("Ano").gameObject.GetComponent<InputField>().text+"-"+
			general.transform.Find ("FechaPremiosDesde").Find ("Fecha").Find("Mes").gameObject.GetComponent<InputField>().text+"-"+
			general.transform.Find ("FechaPremiosDesde").Find ("Fecha").Find("Dia").gameObject.GetComponent<InputField>().text+" "+
			general.transform.Find ("FechaPremiosDesde").Find ("Hora").Find("Hora").gameObject.GetComponent<InputField>().text+":"+
			general.transform.Find ("FechaPremiosDesde").Find ("Hora").Find("Minuto").gameObject.GetComponent<InputField>().text+":00";
		Main.SaveConfig ("premios_start_date", fecha_desde);
		string fecha_hasta = 
			general.transform.Find ("FechaPremiosHasta").Find ("Fecha").Find("Ano").gameObject.GetComponent<InputField>().text+"-"+
			general.transform.Find ("FechaPremiosHasta").Find ("Fecha").Find("Mes").gameObject.GetComponent<InputField>().text+"-"+
			general.transform.Find ("FechaPremiosHasta").Find ("Fecha").Find("Dia").gameObject.GetComponent<InputField>().text+" "+
			general.transform.Find ("FechaPremiosHasta").Find ("Hora").Find("Hora").gameObject.GetComponent<InputField>().text+":"+
			general.transform.Find ("FechaPremiosHasta").Find ("Hora").Find("Minuto").gameObject.GetComponent<InputField>().text+":00";
		Main.SaveConfig ("premios_end_date", fecha_hasta);
	}

	//Funciones formulario

	void FillSettingsFormulario(){
		general.transform.Find("Formulario").GetComponent<Toggle>().isOn = Convert.ToBoolean(Main.GetConfig("formulario_activo"));
	}

	void SaveSettingsFormulario(){
		Main.SaveConfig ("formulario_activo", general.transform.Find("Formulario").GetComponent<Toggle>().isOn.ToString());
	}

	//Funciones eventos

	public void CambiarEvento(){
		Hashtable eventos = Evento.GetAll ();
		Hashtable eventos_combo = new Hashtable ();
		int i = 0;
		foreach(DictionaryEntry evento in eventos) {
			Hashtable evento_values = (Hashtable) evento.Value;
			Hashtable evento_item = new Hashtable ();
			evento_item.Add("id",evento_values["code"].ToString());
			evento_item.Add("nombre",evento_values["nombre"].ToString());
			eventos_combo.Add (i,evento_item);
			i++;
		}
		combo_box.related_field = general.transform.Find("Evento").gameObject;
		combo_box.title = "Cambiar evento activo";
		combo_box.Fill(eventos_combo);
		combo_box.SetSelected (general.transform.Find ("Evento").Find ("ID").GetComponent<InputField> ().text);
	}

	public void CambiarEventoCallback(){
		if (initialized) {
			string evento_code = general.transform.Find ("Evento").Find ("ID").GetComponent<InputField> ().text;
			Evento.CambiarActivo (evento_code);
			//FillDataGeneral ();
		}
	}

	//Funciones promotoras

	public void NuevaPromotora(){
		CreateBox create_box = transform.Find ("CreateBox").gameObject.GetComponent<CreateBox>();
		Hashtable fields = new Hashtable ();
		fields.Add (0, "Nombre"); fields.Add (1, "Apellido");
		fields.Add (2, "Nombre de usuario"); fields.Add (3, "Contraseña"); fields.Add (4, "Repetir Contraseña");
		create_box.action = "promotora";
		create_box.title = "Crear nueva promotora";
		create_box.Fill(fields);
	}

	public void CambiarPromotora(){
		Hashtable promotoras = Promotora.GetAll ();
		Hashtable promotoras_combo = new Hashtable ();
		int i = 0;
		foreach (DictionaryEntry promotora in promotoras) {
			Hashtable evento_values = (Hashtable) promotora.Value;
			Hashtable evento_item = new Hashtable ();
			evento_item.Add("id",evento_values["id"].ToString());
			evento_item.Add("nombre",evento_values["nombre_apellido"].ToString());
			promotoras_combo.Add (i,evento_item);
			i++;
		}
		combo_box.related_field = general.transform.Find("Promotora").gameObject;
		combo_box.title = "Cambiar promotora activa";
		combo_box.Fill(promotoras_combo);
		combo_box.SetSelected (general.transform.Find ("Promotora").Find ("ID").GetComponent<InputField> ().text);
	}

	public void CambiarPromotoraCallback(){
		if (initialized) {
			string promotora_id = general.transform.Find ("Promotora").Find ("ID").GetComponent<InputField> ().text;
			Promotora.CambiarActivo (promotora_id);
		}
	}

	//Funciones Header

	public void CambiarHeader(){
		Hashtable headers_combo = new Hashtable ();
		Hashtable header_macro = new Hashtable ();
		header_macro.Add("id","1");
		header_macro.Add("nombre","Banco Macro");
		Hashtable header_tucuman = new Hashtable ();
		header_tucuman.Add("id","2");
		header_tucuman.Add("nombre","Banco Tucuman");
		headers_combo.Add(1,header_macro);
		headers_combo.Add(2,header_tucuman);
		combo_box.related_field = general.transform.Find("Header").gameObject;
		combo_box.title = "Cambiar encabezado activo";
		combo_box.Fill(headers_combo);
		combo_box.SetSelected (general.transform.Find ("Header").Find ("ID").GetComponent<InputField> ().text);
	}
	
	public void CambiarHeaderCallback(){
		if (initialized) {
			string header_banco_id = general.transform.Find ("Header").Find ("ID").GetComponent<InputField> ().text;
			Main.SaveConfig ("header_banco_id", header_banco_id);
			//FillDataGeneral ();
		}
	}
}
