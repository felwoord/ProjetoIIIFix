using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juansito : MonoBehaviour {
	private GameObject player;
	private Vector3 movDirection;

	private bool slowedTime;
	private Rigidbody rb;

	private float mov, speedRot;
	private float movST, speedRotST;

	private Behaviour halo;

	private int life = 3;

	private float count = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		rb = GetComponent<Rigidbody> ();

		mov = Random.Range (250, 350);
		movST = mov / 3;
		speedRot = 10;
		speedRotST = speedRot / 3;

		halo = (Behaviour)GetComponent ("Halo");
		halo.enabled = false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		slowedTime = GameObject.Find ("Player").GetComponent<PlayerMovement> ().GetSlowedTime ();


		Mov ();
	}

	private void Mov(){
		Vector3 differenceVector = new Vector3 (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z);
		Vector3 diffVectorNormalized = differenceVector.normalized;

		movDirection = -diffVectorNormalized;
		movDirection.y = 0;

//		Quaternion newRotation = Quaternion.LookRotation (differenceVector, Vector3.up);
//		newRotation.x = 0;
//		newRotation.z = 0;

		
		if (slowedTime) {
			rb.velocity = diffVectorNormalized * movST * Time.deltaTime;
//			transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedRotST * Time.deltaTime);
		} else {
			rb.velocity = diffVectorNormalized * mov * Time.deltaTime;
//			transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedRot * Time.deltaTime);
		}

		
		if (movDirection != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation (movDirection);
		}



	}
		
	public void Hit(){
		Vector3 differenceVector = new Vector3 (transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y, transform.position.z - player.transform.position.z);
		Vector3 diffVectorNormalized = differenceVector.normalized;
		transform.position = new Vector3 (transform.position.x - diffVectorNormalized.x * -15, 7.83f, transform.position.z - diffVectorNormalized.z * -15);
		player.GetComponent<PlayerMovement> ().AddEnergy ();


		ActiveHalo ();

		Invoke ("DeactiveHalo", 0.15f);


		this.life--;

		if (this.life <= 0)
			Destroy (gameObject);
	}

	public void Block()
	{
		Vector3 differenceVector = new Vector3 (transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y, transform.position.z - player.transform.position.z);
		Vector3 diffVectorNormalized = differenceVector.normalized;
		transform.position = new Vector3 (transform.position.x - diffVectorNormalized.x * -7.5f, 7.83f, transform.position.z - diffVectorNormalized.z * -7.5f);
	}

	private void DeactiveHalo(){
		halo.enabled = false;
	}

	private void ActiveHalo(){
		halo.enabled = true;
	}

}
