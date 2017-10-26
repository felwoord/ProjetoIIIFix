using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMov : MonoBehaviour {
	public float mov;
	public float amplitude;

	private bool slowedTime;

	//private Rigidbody rb;
	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody> ();
		slowedTime = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, amplitude *  Mathf.Sin (Time.time * mov));
	
		if(Input.GetKeyDown (KeyCode.Space)){
			if (!slowedTime) {
				mov /= 10;
			} else {
				mov *= 10;
			}
			slowedTime = !slowedTime;
		}

	}

//	public bool GetSlowedTime(){
//		return slowedTime;
//	}
}
