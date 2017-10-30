using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkill : MonoBehaviour {
	private float count;
	public float timeOnScene;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;
		if (count > timeOnScene) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Ground") {
			GameObject bullet = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet.transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
			bullet.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 100);
			GameObject bullet2 = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet2.transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
			bullet2.GetComponent<Rigidbody> ().velocity = new Vector3 (70.72f, 0, 70.72f);
			GameObject bullet3 = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet3.transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
			bullet3.GetComponent<Rigidbody> ().velocity = new Vector3 (100, 0, 0);
			GameObject bullet4 = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet4.transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
			bullet4.GetComponent<Rigidbody> ().velocity = new Vector3 (70.72f, 0, -70.72f);
			GameObject bullet5 = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet5.transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
			bullet5.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, -100);
			GameObject bullet6 = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet6.transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
			bullet6.GetComponent<Rigidbody> ().velocity = new Vector3 (-70.72f, 0, -70.72f);
			GameObject bullet7 = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet7.transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
			bullet7.GetComponent<Rigidbody> ().velocity = new Vector3 (-100, 0, 0);
			GameObject bullet8 = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet8.transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
			bullet8.GetComponent<Rigidbody> ().velocity = new Vector3 (-70.72f, 0, 70.72f);
			Destroy (gameObject);
		}

		if (col.collider.tag == "Enemy") {
			string nameObj = col.gameObject.name;

			if (nameObj == "Juasinto") {
				col.gameObject.GetComponent<Juansito> ().Hit ();
			}
			if (nameObj == "Imp") {
			
			}
			if (nameObj == "Ghost") {
				col.gameObject.GetComponent<Ghost> ().Hit();
			}
			if (nameObj == "Encostus") {
				col.gameObject.GetComponent<Encostus> ().Hit ();
			}
			if (nameObj != "Encostus") {
				GameObject bullet = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
				bullet.transform.position = new Vector3 (transform.position.x + 5, 3, transform.position.z + 5);
				bullet.GetComponent<Rigidbody> ().velocity = new Vector3 (70.72f, 0, 70.72f);

				GameObject bullet2 = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
				bullet2.transform.position = new Vector3 (transform.position.x + 5, 3, transform.position.z - 5);
				bullet2.GetComponent<Rigidbody> ().velocity = new Vector3 (70.72f, 0, -70.72f);

				GameObject bullet3 = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
				bullet3.transform.position = new Vector3 (transform.position.x - 5, 3, transform.position.z + 5);
				bullet3.GetComponent<Rigidbody> ().velocity = new Vector3 (-70.72f, 0, 70.72f);

				GameObject bullet4 = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
				bullet4.transform.position = new Vector3 (transform.position.x - 5, 3, transform.position.z - 5);
				bullet4.GetComponent<Rigidbody> ().velocity = new Vector3 (-70.72f, 0, -70.72f);
			}
			Destroy (gameObject);

		}
		if (col.collider.tag == "Column") {
			rb.constraints = RigidbodyConstraints.FreezePositionY;
		}
		if (col.collider.tag == "Block") {
			Destroy (gameObject);
		}
	}
}
