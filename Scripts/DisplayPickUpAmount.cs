using UnityEngine;
using System.Collections;

public class DisplayPickUpAmount : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		int amount = player.GetComponent<Inventory>().getAmountDirt();
		GUI.Box(new Rect(50, 25, 100, 25), amount.ToString());
	}
}
