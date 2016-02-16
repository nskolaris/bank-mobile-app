using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	//public Sprite regular_sprite;
	//public Sprite powered_sprite;
	public float powered_block_jump_multiplier = 10f;
	public int type = 0;
	/* 0-Normal 1-Powered */

	public int index;

	// Use this for initialization
	void Start () {
	
		SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
		/*switch(type){
		case 0: //Normal
			sprite_renderer.sprite =  regular_sprite;
			break;
		case 1: //Powered
			sprite_renderer.sprite =  powered_sprite;
			break;
		}*/

		/*Position setup*/
		float width = sprite_renderer.bounds.extents.x * 2;
		transform.localPosition = new Vector3(width*index,0,0); 

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D colInfo) {
		if (colInfo.tag == "Player") {
			if (GameManager.player.GetComponent<Rigidbody2D> ().velocity.y < 0) {
				float jump_multiplier = 0f;
				switch (type) {
				case 0: //Normal
					jump_multiplier = 1f;
					break;
				case 1: //Powered
					jump_multiplier = powered_block_jump_multiplier;
					break;
				}
				GameManager.player.GetComponent<Player> ().Jump (jump_multiplier);
			}
		} else if (colInfo.tag == "BottomBoundary") {

		}
	}
}
