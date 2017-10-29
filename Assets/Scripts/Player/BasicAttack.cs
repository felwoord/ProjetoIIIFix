using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour {
	private float count;
	public float timeOnScene;
	public float rotationSpeed;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;
		if (count > timeOnScene) {
			Destroy(gameObject);
		}
			
		transform.Rotate (-Vector3.up * (rotationSpeed * Time.deltaTime));
	}
		

	void OnCollisionEnter (Collision col)
	{

		if (col.collider.tag == "Enemy") {
			string nameObj = col.gameObject.name;

			if (col.collider.name == "Juansito") {
				col.gameObject.GetComponent<Juansito> ().Hit ();
			}
			if (nameObj == "Imp") {

			}
			if (nameObj == "Ghost") {

			}
			if (nameObj == "Encostus") {

			}

			Destroy (gameObject);
		}

		if (col.collider.tag == "Block") {
			col.gameObject.GetComponent<Juansito> ().Block ();
			Destroy (gameObject);
		}
	}
}

