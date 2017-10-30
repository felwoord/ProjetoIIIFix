using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
	private GameObject player;
	private Vector3 movDirection;

	private bool slowedTime;
	private Rigidbody rb;

	private float mov;
	//	private float speedRot;
	private float movST;
	//	private float speedRotST;
	private Behaviour halo;

	private int life = 8;

	private float count = 0;
	private bool movement;

	private GameObject backWall, frontWall, leftWall, rightWall;

	// Use this for initialization
	void Start () {
		backWall = GameObject.Find ("BackWall");
		frontWall = GameObject.Find ("FrontWall");
		leftWall = GameObject.Find ("LeftWall");
		rightWall = GameObject.Find ("RightWall");

		player = GameObject.Find ("Player");
		rb = GetComponent<Rigidbody> ();

		mov = Random.Range (350, 450);
		movST = mov / 3;
		//		speedRot = 10;
		//		speedRotST = speedRot / 3;

		halo = (Behaviour)GetComponent ("Halo");
		halo.enabled = false;

	}

	// Update is called once per frame
	void FixedUpdate () {
		slowedTime = GameObject.Find ("Player").GetComponent<PlayerMovement> ().GetSlowedTime ();

		count += Time.deltaTime;
		if (count > 3) {
			float rand = Random.Range (1, 10);
			if (rand < 7) {
				movement = true;
			} else {
				movement = false;
			}
			count = 0;
		}

		if (movement) {
			Mov ();
		} else {
			rb.velocity = Vector3.zero;
		}

		AntiBugWall ();
	}


	private void Mov(){
		Vector3 differenceVector = new Vector3 (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z);
		Vector3 diffVectorNormalized = differenceVector.normalized;

		movDirection = -diffVectorNormalized;
		movDirection.y = 0;


		if (slowedTime) {
			rb.velocity = diffVectorNormalized * movST * Time.deltaTime;

		} else {
			rb.velocity = diffVectorNormalized * mov * Time.deltaTime;
			if (movDirection != Vector3.zero) {
			}
		}

		transform.rotation = Quaternion.LookRotation (-movDirection);

	}

	public void Hit(){
		Vector3 differenceVector = new Vector3 (transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y, transform.position.z - player.transform.position.z);
		Vector3 diffVectorNormalized = differenceVector.normalized;
		transform.position = new Vector3 (transform.position.x - diffVectorNormalized.x * -15, 6.80f, transform.position.z - diffVectorNormalized.z * -15);
		player.GetComponent<PlayerMovement> ().AddEnergy ();


		ActiveHalo ();

		Invoke ("DeactiveHalo", 0.15f);


		this.life--;

		if (this.life <= 0)
			Destroy (gameObject);
	}
		

	private void DeactiveHalo(){
		halo.enabled = false;
	}

	private void ActiveHalo(){
		halo.enabled = true;
	}

	private void AntiBugWall(){
		if (transform.position.z < backWall.transform.position.z) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, backWall.transform.position.z + 10);
		}
		if (transform.position.z > frontWall.transform.position.z) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, frontWall.transform.position.z - 10);
		}
		if (transform.position.x < leftWall.transform.position.x) {
			transform.position = new Vector3 (leftWall.transform.position.x + 10, transform.position.y, transform.position.z);
		}
		if (transform.position.x > rightWall.transform.position.x) {
			transform.position = new Vector3 (rightWall.transform.position.x - 10, transform.position.y, transform.position.z);
		}
	}

}
