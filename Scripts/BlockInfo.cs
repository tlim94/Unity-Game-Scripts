using UnityEngine;
using System.Collections;

public class BlockInfo : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	private blockState state = blockState.FULL;
	public blockType type = blockType.DIRT;
	public blockLayer layer = blockLayer.TOP;

	private float blockDMG = 0f;
	private Color startColor;
	public float mineRate = 0.01f;
	private Transform underCheck;
	private Transform leftCheck;
	private Transform rightCheck;

	public void updateBlockState(){
		state++;
	}

	public void incBlockDMG(){
		blockDMG += mineRate;
	}

	public void resetDMG(){
		blockDMG = 0f;
		if(spriteRenderer){
			spriteRenderer.color = startColor;
		}
	}

	public void checkState(){
		if(blockDMG >= 1f){
			state = blockState.DESTROYED;
		}
	}

	public bool checkUnder(){
		RaycastHit2D[] hits = new RaycastHit2D[1];

		var startPos = transform.position;
		startPos.y += 2.95f;

		Physics2D.LinecastNonAlloc(startPos, underCheck.position, hits, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawLine(startPos, underCheck.position, Color.green);
		
		foreach (RaycastHit2D hitInfo in hits) {
			if(hitInfo){
				//Debug.Log("Underground");
				return true;
			}
		}

		return false;
	}

	public bool checkLeft(){
		RaycastHit2D[] hits = new RaycastHit2D[1];
		
		var startPos = transform.position;
		startPos.x -= 3.05f;
		
		Physics2D.LinecastNonAlloc(startPos, leftCheck.position, hits, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawLine(startPos, leftCheck.position, Color.green);
		
		foreach (RaycastHit2D hitInfo in hits) {
			if(hitInfo){
				//Debug.Log("Underground");
				return true;
			}
		}
		
		return false;
	}

	public bool checkRight(){
		RaycastHit2D[] hits = new RaycastHit2D[1];
		
		var startPos = transform.position;
		startPos.x += 3.05f;
		
		Physics2D.LinecastNonAlloc(startPos, rightCheck.position, hits, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawLine(startPos, rightCheck.position, Color.green);
		
		foreach (RaycastHit2D hitInfo in hits) {
			if(hitInfo){
				//Debug.Log("Underground");
				return true;
			}
		}
		
		return false;
	}

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		startColor = spriteRenderer.color;
		underCheck = transform.Find("underCheck");
		leftCheck = transform.Find("leftCheck");
		rightCheck = transform.Find("rightCheck");
	}

	// Update is called once per frame
	void Update () {

		checkState();

		if(layer == blockLayer.TOP){
			if(!checkUnder()){
				var left = checkLeft();
				var right = checkRight();

				//Base
				if(left && right){
					animator.SetInteger("topState", 1);
				}
				//Right
				else if(left && !right){
					animator.SetInteger("topState", 2);
				}
				//Left
				else if(right && !left){
					animator.SetInteger("topState", 3);
				}
				//Full
				else if(!left && !right){
					animator.SetInteger("topState", 4);
				}

				//checkRight();
			}
			/*if(!checkUnder() || !checkLeft() || !checkRight(){
				animator.SetInteger("blockState", 1);
			}*/
		}

		var newColor = spriteRenderer.color;
		spriteRenderer.color = new Color(newColor.r, newColor.g, newColor.b, 1 - blockDMG);

		if(state == blockState.DESTROYED){
			Destroy(gameObject);
		}
	}
}
