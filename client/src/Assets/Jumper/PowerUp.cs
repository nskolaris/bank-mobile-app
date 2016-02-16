using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public float rotationMax = 1.0f;
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(-rotationMax,rotationMax));

		SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>() as SpriteRenderer;
		int sprite_number = Random.Range(1,7);
		Sprite sprite = Resources.Load<Sprite>("Jumper/descuentos/"+sprite_number.ToString());
		if (sprite){
			sprite_renderer.sprite =  sprite;
			GetComponent<SpriteRenderer> ().sprite = sprite;
		} else {
			Debug.LogError("Sprite not found", this);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D colInfo) {
		if (colInfo.tag == "Player") {
			GameManager.player.GetComponent<Player> ().receivePowerUp ();
			Destroy(gameObject);
		} else if (colInfo.tag == "BottomBoundary") {
			Destroy(gameObject);
		}
	}
}
