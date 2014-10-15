using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	
	private Transform target;
	public float speed = 10f;
	public float followDist = 10f;
	public Vector2 maxVelocity = new Vector2(10, 50);
	private Vector2 direction;
	private PlayerController controller;
	private Player thisPlayer;
	
	void Start(){
		this.enabled = false;
		thisPlayer = GetComponent<Player>();
		//controller = GameObject.Find("PlayerManager").GetComponent<PlayerController>();
		controller = PlayerController.current;
	}

	public void setTarget(Transform info){
		target = info;
	}

	public void setFollowDist(float followDist){
		this.followDist = followDist;
	}

	void Update(){

		if(controller.isActive() == thisPlayer.ID){
			return;
		}

		var forceX = 0f;
		var forceY = 0f;

		var absVelX = Mathf.Abs(rigidbody.velocity.x);
		var absVelY = Mathf.Abs(rigidbody.velocity.y);

		var dir = target.position - transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		if(angle < 90 && angle > -90)
		{
			direction.x = 1;
		}
		else{
			direction.x = -1;
		}

		transform.localScale = new Vector3 (direction.x, transform.localScale.y, transform.localScale.z);

		var dist = Vector3.Distance(transform.position, target.position);
		if(absVelX < maxVelocity.x && dist > followDist){
			forceX = thisPlayer.speed * direction.x;
		}

		rigidbody.AddForce (new Vector2 (forceX, forceY));
	}
}
