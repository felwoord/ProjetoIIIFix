using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackAnimation : MonoBehaviour {
	private Animator ani;
	float count = 0;

	// Use this for initialization
	void Start () {
		ani = GetComponent<Animator> ();
		Debug.Log (ani.hasRootMotion);
	}
	
	// Update is called once per frame
	void Update () {
		if (ani.GetCurrentAnimatorStateInfo (0).IsName ("AttackState")) {
			count += Time.deltaTime;

			if (count > 0.4f) {
				Destroy (gameObject);
			}
		}
	}

//	void OnCollisionEnter (Collision col)
//	{
//		Debug.Log ("ok");
//		if (col.collider.tag == "Enemy") {
//			string nameObj = col.gameObject.name;
//
//			if (col.collider.name == "Juansito") {
//				col.gameObject.GetComponent<Juansito> ().Hit ();
//			}
//			if (nameObj == "Imp") {
//				col.gameObject.GetComponent<Imp> ().Hit ();
//			}
//			if (nameObj == "Ghost") {
//				col.gameObject.GetComponent<Ghost> ().Hit();
//			}
//			if (nameObj == "Encostus") {
//				col.gameObject.GetComponent<Encostus> ().Hit ();
//			}
//
//			//Destroy (gameObject);
//		}
//
//		if (col.collider.tag == "Turret") {
//			Destroy (col.gameObject);
//			//Destroy (gameObject);
//		}
//
//		if (col.collider.tag == "Block") {
//			col.gameObject.GetComponent<Juansito> ().Block ();
//			Destroy (gameObject);
//		}
//	}

	void OnTriggerEnter (Collider col)
	{
		Debug.Log ("ok");
		if (col.tag == "Enemy") {
			string nameObj = col.gameObject.name;

			if (col.name == "Juansito") {
				col.gameObject.GetComponent<Juansito> ().Hit ();
			}
			if (nameObj == "Imp") {
				col.gameObject.GetComponent<Imp> ().Hit ();
			}
			if (nameObj == "Ghost") {
				col.gameObject.GetComponent<Ghost> ().Hit();
			}
			if (nameObj == "Encostus") {
				col.gameObject.GetComponent<Encostus> ().Hit ();
			}

			//Destroy (gameObject);
		}

		if (col.tag == "Turret") {
			Destroy (col.gameObject);
			//Destroy (gameObject);
		}

		if (col.tag == "Block") {
			col.gameObject.GetComponent<Juansito> ().Block ();
			Destroy (gameObject);
		}
	}
}
