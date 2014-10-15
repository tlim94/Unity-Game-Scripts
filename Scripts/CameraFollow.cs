using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private PlayerController controller;

	private playerID focus = playerID.P1;

	public float transitionSpeed = 200f;
	private Vector3 targePos;
	private Vector3 activePos;
	private Vector3 dist;

	void Awake(){
		//camera.orthographicSize = ((Screen.height/2.0f / 100f));
		controller = GameObject.Find("PlayerManager").GetComponent<PlayerController>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		activePos = controller.getPlayerByID(focus).transform.position;
		activePos.z = transform.position.z;

		if(focus != controller.isActive()){
			StopCoroutine("Transition");

			focus = controller.isActive();

			targePos = controller.getPlayerByID(focus).transform.position;
			targePos.z = transform.position.z;

			StartCoroutine("Transition");
			return;
		}

		transform.position = activePos;
	}


	IEnumerator Transition()
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		var dist = Vector3.Distance(startingPos, targePos);

		while (t < 1.0f)
		{
			t += (transitionSpeed *  Time.deltaTime) /dist; //* (Time.timeScale/transitionDuration);
			
			transform.position = Vector3.Lerp(startingPos, activePos, t);
			yield return 0;
		}

	}
}
