using UnityEngine;
using System.Collections;

public class Monster2D : MonoBehaviour {
	
	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3, 5);
	public float jumpSpeed = 15f;
	public float airSpeedMultiplier = .3f;
	private Transform groundCheck;
	
	private bool grounded;
	private bool walled;
	private bool jump = false;
	private Vector2 movement;

	public float scale = .1f;

	public float health = 100;
	
	void Start(){
		//controller = GameObject.Find("PlayerManager").GetComponent<PlayerController>();
		groundCheck = transform.Find("groundCheck");
	}
	
	void Update(){
	//	grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
		if(health <= 0){
			Destroy(gameObject);
		}

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(movement.y > 0 && (grounded || walled)){
			jump = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		var forceX = 0f;
		var forceY = 0f;
		
		var absVelX = Mathf.Abs(rigidbody2D.velocity.x);
		var absVelY = Mathf.Abs(rigidbody2D.velocity.y);

		/*movement = controller.getMovement();
		
		if (movement.x != 0) {
			if (absVelX < maxVelocity.x) {
				forceX = grounded ? speed * movement.x : (speed * movement.x * airSpeedMultiplier);
				transform.localScale = new Vector3 (forceX > 0 ? -scale : scale, transform.localScale.y, transform.localScale.z);
			}
		}

		if (movement.y > 0) {
			if(absVelY < maxVelocity.y && jump){
				if(walled){
					forceX = speed * 4 * -transform.localScale.x;
					forceY = jumpSpeed * movement.y;
					jump = false;
				}else if(grounded){
					forceY = jumpSpeed * movement.y;
					jump = false;
				}
			}
		}*/
		
		rigidbody2D.AddForce (new Vector2 (forceX, forceY));
	}

	public void takeDamage(float dmg){
		health -= dmg;
	}
}
