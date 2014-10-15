using UnityEngine;
using System.Collections;

public class CreateDrops : MonoBehaviour {

	public Drop drop;
	public int totalDrops = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void instantiateDrops(){
		var t = transform;

		for(int i = 0; i < totalDrops; i++){
			//t.TransformPoint(0, -100, 0);
			Drop clone = Instantiate(drop, t.position, Quaternion.identity) as Drop;
			clone.rigidbody.AddForce(Vector2.right * Random.Range(-100, 100));
			clone.rigidbody.AddForce(Vector3.up * Random.Range(100, 300));
		}
	}
}
