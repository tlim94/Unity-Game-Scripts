using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mine : MonoBehaviour {

	//public Transform mineStart;
	
	private RaycastHit2D hitInfo;
	private Vector2 mineEnd;
	private Vector2 mousePos;
	public float mineDistance = 15f;
	
	private PlayerController controller;
	private Player player;
	public bool drops = false;
	HashSet<BlockInfo> touchedBlock = new HashSet<BlockInfo>();

	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
		controller = GameObject.Find("PlayerManager").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

		if(controller.isActive() != player.ID){
			return;
		}

		if(controller.getLeftClick()){
			doMine();
        }
		else{
			resetTouched();
        }
        
    }

	private void resetTouched(){
		foreach (BlockInfo info in touchedBlock) {
			info.resetDMG();
        }

		touchedBlock.Clear();
	}

    
    private void doMine(){
		mineEnd = controller.getNormMousePos();

		RaycastHit2D[] hits = new RaycastHit2D[4];

		Physics2D.LinecastNonAlloc(transform.position, mineEnd, hits, 1 << LayerMask.NameToLayer("Ground"));
        //hitInfo = Physics2D.Linecast(transform.position, mineEnd, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawLine(transform.position, mineEnd, Color.green);

		foreach (RaycastHit2D hitInfo in hits) {
			if(hitInfo){
				if(hitInfo.distance < mineDistance && hitInfo.collider.gameObject.tag == ("Mine")){
					var blockInfo = hitInfo.collider.gameObject.GetComponent<BlockInfo>();
					if(blockInfo){
						blockInfo.incBlockDMG();
						touchedBlock.Add(blockInfo);
					}
					//Destroy(hitInfo.collider.gameObject);
					if(drops){
						hitInfo.collider.gameObject.GetComponent<CreateDrops>().instantiateDrops();
                    }
                }
            }
        }
        
    }
    
}
