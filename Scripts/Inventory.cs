using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public static Inventory current;

	private Canvas screen;
	private bool display = false;

	public int dirtBlocks = 0;
	private List<Item> itemList = new List<Item>();
	private GameObject[] inventoryFields;

	// Use this for initialization
	void Awake(){
		current = this;
		screen = GetComponent<Canvas>();
		screen.enabled = display;

		inventoryFields = GameObject.FindGameObjectsWithTag("InventorySlot");

		itemList.Add(new Item(0));
		itemList.Add(new Item(1));
		itemList.Add(new Item(2));
		itemList.Add(new Item(3));
		itemList.Add(new Item(4));
		itemList.Add(new Item(5));
		itemList.Add(new Item(6));
		itemList.Add(new Item(7));

		var i = 0;
		foreach (Item item in itemList) {
			inventoryFields[i].GetComponent<Text>().text = item.name;
			i++;
		}

	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if(target.gameObject.tag == "Dirt"){
			incrDirt();
		}
	}

	public int getAmountDirt(){
		return dirtBlocks;
	}

	public void incrDirt(){
		dirtBlocks++;
	}

	public void toggleInv(){
		display = !display;
		screen.enabled = display;
	}
}
