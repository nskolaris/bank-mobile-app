using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;

	float scale;
	public float min_scale = 1;
	public float max_scale = 5;

	float width;

	// Use this for initialization
	void Start () {
		//Setting scale
		scale = Random.Range(min_scale,max_scale);
		transform.localScale = new Vector3(scale,scale,scale);
		width = GetComponent<Renderer>().bounds.extents.x;

		//Setting sprite
		SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>() as SpriteRenderer;
		int sprite_number = Random.Range(1,3);
		switch(sprite_number){
		case 1:
			sprite_renderer.sprite =  sprite1;
			break;
		case 2:
			sprite_renderer.sprite =  sprite2;
			break;
		case 3:
			sprite_renderer.sprite =  sprite3;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float cam_distance = transform.position.z - Camera.main.transform.position.z;
		float min_x = Camera.main.gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, 0f, cam_distance)).x;
		float max_x = Camera.main.gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, 0f, cam_distance)).x;
		float min_y = Camera.main.gameObject.GetComponent<CameraController> ().GetBottom (cam_distance);

		if(transform.position.y + 2f < min_y){Destroy(gameObject);}
		if(GetComponent<Rigidbody2D>().velocity.x > 0 && transform.position.x - width > max_x){Destroy(gameObject);}
		if(GetComponent<Rigidbody2D>().velocity.x < 0 && transform.position.x + width < min_x){Destroy(gameObject);}
	}
}
