using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	
	public int block_type = 0;
	public GameObject block;

	// Use this for initialization
	void Start () {
		int width = Random.Range(3,6);
		Vector3 pos = Vector3.zero;
		Quaternion rot = Quaternion.identity;
		
		for(int i = 0; i <= width; i++){
			GameObject newBlock = Instantiate(block, pos, rot) as GameObject;
			newBlock.transform.parent = transform;
			newBlock.GetComponent<Block>().type = block_type;
			newBlock.GetComponent<Block>().index = i;
		}
		BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
		boxCollider.size = new Vector2(100f, 1f);
		boxCollider.offset = new Vector2(0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D (Collider2D colInfo) {
		if(colInfo.tag == "BottomBoundary"){
			Destroy(gameObject);
		}
	}
}