using UnityEngine;
using System.Collections;

public class Player2D : MonoBehaviour {
	
	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3, 5);
	public float jumpSpeed = 15f;
	public float airSpeedMultiplier = .3f;
	private Transform groundCheck;
	private Transform wallCheck;
	
	private bool grounded;
	private bool walled;
	private bool jump = false;
	private Vector2 movement;

	private PlayerController controller;
	private Animator animator;

	public playerID ID = playerID.P1;

	public float scale = .1f;
	private float dmg = 10f;
	
	void Start(){
		animator = GetComponent<Animator>();
		//controller = GameObject.Find("PlayerManager").GetComponent<PlayerController>();
		controller = PlayerController.current;
		groundCheck = transform.Find("groundCheck");
		wallCheck = transform.Find("wallCheck");
	}

	void Update(){
	//	grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
	//	walled = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

//		Debug.Log (grounded);
		// If the jump button is pressed and the player is grounded then the player should jump.
		if(movement.y > 0 && (grounded || walled)){
			jump = true;
		}

		if(controller.getLeftClick()){
			animator.SetInteger("animState", 1);
			dmg = 10f;
		}
		else if(controller.getRightClick()){
			animator.SetInteger("animState", 2);
			dmg = 20f;
		}
		else{
			animator.SetInteger("animState", 0);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(controller.isActive() != ID){
			return;
		}

		var forceX = 0f;
		var forceY = 0f;
		
		var absVelX = Mathf.Abs(rigidbody2D.velocity.x);
		var absVelY = Mathf.Abs(rigidbody2D.velocity.y);

		movement = controller.getMovement();
		
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
		}
		
		rigidbody2D.AddForce (new Vector2 (forceX, forceY));
	}
}
