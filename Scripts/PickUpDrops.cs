﻿using UnityEngine;
using System.Collections;

public class PickUpDrops : MonoBehaviour {

	public AudioClip pickupSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if(target.gameObject.tag == "Player"){
			Destroy(gameObject);
		}
	}
}
