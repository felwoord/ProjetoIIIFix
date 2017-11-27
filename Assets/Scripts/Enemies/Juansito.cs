using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juansito : MonoBehaviour {
	private GameObject player;
	private Vector3 movDirection;

	private bool slowedTime;
	private Rigidbody rb;

	private float mov;
//	private float speedRot;
//	private float movST;
//	private float speedRotST;
	private Behaviour halo;

	private int life = 3;

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

		mov = Random.Range (250, 350);
//		movST = mov / 3;
//		speedRot = 10;
//		speedRotST = speedRot / 3

		halo = (Behaviour)GetComponent ("Halo");
		halo.enabled = false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		slowedTime = GameObject.Find ("Player").GetComponent<PlayerMovement> ().GetSlowedTime ();

		count += Time.deltaTime;
		if (count > 8) {
			float rand = Random.Range (1, 10);
			if (rand < 7) {
				movement = true;
			} else {
				movement = false;
			}
			count = 0;
		}
		if (!slowedTime) {
			if (movement) {
				rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
				Mov ();
			} else {
				rb.velocity = Vector3.zero;
				rb.constraints = RigidbodyConstraints.FreezeAll;
			}
		} else {
			rb.velocity = Vector3.zero;
			rb.constraints = RigidbodyConstraints.FreezeAll;
		}
		AntiBugWall ();
	}


	private void Mov(){
		Vector3 differenceVector = new Vector3 (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z);
		Vector3 diffVectorNormalized = differenceVector.normalized;

		movDirection = -diffVectorNormalized;
		movDirection.y = 0;



		rb.velocity = diffVectorNormalized * mov * Time.deltaTime;
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

	private void AntiBugWall(){
		if (transform.position.z < backWall.transform.position.z) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, backWall.transform.position.z + 3);
		}
		if (transform.position.z > frontWall.transform.position.z) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, frontWall.transform.position.z - 3);
		}
		if (transform.position.x < leftWall.transform.position.x) {
			transform.position = new Vector3 (leftWall.transform.position.x + 3, transform.position.y, transform.position.z);
		}
		if (transform.position.x > rightWall.transform.position.x) {
			transform.position = new Vector3 (rightWall.transform.position.x - 3, transform.position.y, transform.position.z);
		}
		if (transform.position.y < 7f) {
			transform.position = new Vector3 (transform.position.x, 7.83f, transform.position.z);
		}
	}
}
