using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public static PlayerController current;

	private Vector2 moving = new Vector2();
	private Vector2 mousePos;
	private Vector2 normalMousePos;
	
	private GameObject[] playerObjs;
	private playerID activePlayer = playerID.P1;
	private FollowPlayer follow;
	private Dictionary<playerID, GameObject> playerList = new Dictionary<playerID, GameObject>();
	public float attractDistance = 5f;
	private float followDist;

	void Awake(){
		current = this;
	}

	// Use this for initialization
	void Start () {

		playerObjs = GameObject.FindGameObjectsWithTag("Player");

		foreach (GameObject player in playerObjs) {
			playerList.Add(player.GetComponent<Player2D>().ID, player);
		}
	}

	public playerID isActive(){
		return activePlayer;
	}

	public Vector2 getMovement(){
		return moving;
	}

	public bool getLeftClick(){
		return Input.GetMouseButton(0);
	}

	public bool getRightClick(){
		return Input.GetMouseButton(1);
	}
	
	public Vector2 getNormMousePos(){
		return normalMousePos;
	}
	

	public GameObject getPlayerByID(playerID ID){
		return playerList[ID];
	}

	// Update is called once per frame
	void Update () {

		mousePos = Input.mousePosition;
		normalMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
		
		moving.x = moving.y = 0;
		
		if (Input.GetKey("d")) {
			moving.x = 1;
		} 
		else if (Input.GetKey("a")) {
			moving.x = -1;
		}
		
		if (Input.GetKey("w")){
			moving.y = 1;
		} 
		else if (Input.GetKey("s")){
			moving.y = -1;
		}

		if(Input.GetKeyDown ("i")){
			Inventory.current.toggleInv();
		}

		if(Input.GetKeyDown("1")){
			activePlayer = playerID.P1;
		}

		if(Input.GetKeyDown("2")){
			activePlayer = playerID.P2;
			
		}

		if(Input.GetKeyDown("3")){
			activePlayer = playerID.P3;
			
		}

		if(Input.GetKey("4")){

			var activePlayerObj = playerList[activePlayer];
			followDist = 4.0f;

			foreach(playerID ID in playerID.GetValues(typeof(playerID))){
				if(ID != activePlayer){
					var dist = Vector3.Distance(playerList[activePlayer].transform.position, playerList[ID].transform.position);
					Debug.Log(dist);
					if(dist <= attractDistance){
						var follow = playerList[ID].GetComponent<FollowPlayer>();
						follow.setTarget(activePlayerObj.transform);
						follow.setFollowDist(followDist);
						follow.enabled = true;
						followDist += 4;
					}
				}
			}
		}

		if(Input.GetKey("5")){
			foreach(playerID ID in playerID.GetValues(typeof(playerID))){
				if(ID != activePlayer){
					var follow = playerList[ID].GetComponent<FollowPlayer>();
					follow.enabled = false;
				}
			}
		}

	}
}
