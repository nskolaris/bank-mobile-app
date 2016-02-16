#pragma strict

private var _GM : Transform;
private var player : GameObject;

private var followPlayer : boolean = false;

function Start () {
	_GM = GameObject.FindGameObjectWithTag("GameController").transform;
}

function Update () {
	if(followPlayer){
		if(GameObject.FindGameObjectWithTag("Player") != null){
			player = GameObject.FindGameObjectWithTag("Player");
			var box = GetComponent(BoxCollider2D) as BoxCollider2D;
			var boundarySize = box.size.y / 2;
			var newPos = Vector3.Lerp(transform.parent.transform.position,player.transform.position,0.075).y;
			if(transform.parent.transform.position.y < newPos){
				transform.parent.transform.position.y = newPos;
			}
		}
	}
}

function OnTriggerEnter2D (colInfo : Collider2D) {
	if(colInfo.tag == "Player"){
		player = GameObject.FindGameObjectWithTag("Player");
		var box = GetComponent(BoxCollider2D) as BoxCollider2D;
		switch(gameObject.tag){
			case "TopBoundary":
			if(player.GetComponent.<Rigidbody2D>().velocity.y > 0){
				followPlayer = true;
			}
			break;
			case "BottomBoundary":
			followPlayer = false;
			Destroy(player.gameObject);
			break;
			case "LeftBoundary":
			if(player.gameObject.GetComponent.<Rigidbody2D>().velocity.x < 0){
				player.transform.position.x = -(box.offset.x+5f);
			}
			break;
			case "RightBoundary":
			if(player.gameObject.GetComponent.<Rigidbody2D>().velocity.x > 0){
				player.transform.position.x = -(box.offset.x-5f);
			}
			break;
		}
	}
}

function OnTriggerExit2D (colInfo : Collider2D) {
	if(colInfo.tag == "Player"){
		player = GameObject.FindGameObjectWithTag("Player");
		switch(gameObject.tag){
			case "TopBoundary":
			if(player.GetComponent.<Rigidbody2D>().velocity.y < 0){
				followPlayer = false;
			}
			break;
		}
	}
}