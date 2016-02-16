using UnityEngine;
using System.Collections;

public class GUItest : MonoBehaviour {
	
	// 200x300 px window will apear in the center of the screen.
	private Rect windowRect = new Rect ((Screen.width - 600)/2, (Screen.height - 150)/2, 600, 150);
	// Only show it if needed.
	private static bool show = false;

	//private static string msg_title = "";
	private static string msg = "";
	private static string msg_type = "";
	private static string next_scene = "";

	private static GUIStyle guiStyle = new GUIStyle();
	
	void OnGUI () {
		guiStyle.fontSize = 20;
		guiStyle.normal.textColor = Color.white;


		GUIStyle window_style = new GUIStyle();
		window_style.normal.background = MakeTex(600, 1, new Color32(0, 147, 208, 255));
		window_style.fontSize = 20;
		window_style.normal.textColor = Color.white;
		if (show) {
			windowRect = GUI.Window (0, windowRect, DialogWindow, "",window_style);
		}
	}

	Texture2D MakeTex(int width, int height, Color col){
		Color[] pix = new Color[width*height];
		
		for(int i = 0; i < pix.Length; i++)
			pix[i] = col;
		
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();
		
		return result;
	}

	// This is the actual window.
	void DialogWindow (int windowID)
	{
		float y = 30;

		if (msg_type == "Alert") {
			GUI.Label(new Rect(20,y, windowRect.width, 30), msg,guiStyle);
			if(GUI.Button(new Rect(20,y+35, windowRect.width - 40, 60), "Aceptar"))	{
				show = false;
				if(next_scene!=""){
					StartCoroutine(Main.LoadLevel(next_scene));
				}
			}
		} else if (msg_type == "Confirm") {
			GUI.Label(new Rect(20,y, windowRect.width, 30), msg,guiStyle);
			
			if(GUI.Button(new Rect(20,y+35, (windowRect.width/2) - 40, 60), "Aceptar"))	{
				Main.Home();
				show = false;
			}
			
			if(GUI.Button(new Rect(20 + (windowRect.width/2) - 10,y+35, (windowRect.width/2) - 40, 60), "Cancelar")){
				show = false;
			}
		}
	}

	public static void ConfirmWindow(string title, string message){
		msg_type = "Confirm";
		//msg_title = title;
		msg = message;
		show = true;
	}
	
	// To open the dialogue from outside of the script.
	public static void Open()
	{
		show = true;
	}

	public static void AlertMsg(string title, string message, string scene_after_accept = ""){
		msg_type = "Alert";
		//msg_title = title;
		msg = message;
		next_scene = scene_after_accept;
		show = true;
	}
	
}