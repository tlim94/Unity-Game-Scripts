using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		Debug.Log("Collision");
		if(target.gameObject.tag == "Monster"){
			target.gameObject.GetComponent<Monster2D>().takeDamage(10f);
		}
	}
}
