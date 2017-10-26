using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkill : MonoBehaviour {
	private float count;
	public float timeOnScene;

	private bool slowedTime;

	private Rigidbody rb;

	private Vector3 vel,velST;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		vel = rb.velocity;
		velST = vel / 3;
	}
	
	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;
		if (count > timeOnScene) {
			Destroy(gameObject);
		}

		slowedTime = GameObject.Find ("Player").GetComponent<PlayerMovement> ().GetSlowedTime ();

		if (slowedTime) {
			rb.velocity = velST;
		} else {
			rb.velocity = vel;
		}
	}
		
}
