using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Option : MonoBehaviour, IPointerClickHandler {

	public string id = "null";
	public string nombre;

	public void OnPointerClick (PointerEventData eventData) {
		Select ();
	}

	public void Select(){
		transform.parent.gameObject.GetComponentInParent<ComboBox> ().ReportClick (id, nombre);
		Option[] options = transform.parent.gameObject.GetComponentsInChildren<Option>();
		foreach (Option target in options) {
			target.gameObject.GetComponentInChildren<Image> ().color = new Color32 (255, 255, 255, 255);
		}
		gameObject.GetComponentInChildren<Image> ().color = new Color32 (0, 147, 208, 255);
	}
}
