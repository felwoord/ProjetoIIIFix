using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost2 : MonoBehaviour {
	private GameObject player;

	private int life;

	private float mov;
	private float movST;

	private bool slowedTime;

	private Rigidbody rb;

	private Behaviour halo;

	private Vector3 movDirection;



	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");

		rb = GetComponent<Rigidbody> ();

		life = 4;

		mov = Random.Range (150, 250);
		movST = mov / 3;

		halo = (Behaviour)GetComponent ("Halo");
		halo.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		slowedTime = GameObject.Find ("Player").GetComponent<PlayerMovement> ().GetSlowedTime ();

		Vector3 differenceVector = new Vector3 (player.transform.position.x - transform.position.x,  player.transform.position.y - transform.position.y,  player.transform.position.z - transform.position.z);
		Vector3 diffVectorNormalized = differenceVector.normalized;


		movDirection = -diffVectorNormalized;
		movDirection.y = 0;

		if (slowedTime) {
			rb.velocity = diffVectorNormalized * movST * Time.deltaTime;
		} else {
			rb.velocity = diffVectorNormalized * mov * Time.deltaTime;
		}
		transform.position = new Vector3 (transform.position.x, 2.82f,  transform.position.z);

		if (movDirection != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation (movDirection);
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "BasicAttack") {
			Vector3 differenceVector = new Vector3 (transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y, transform.position.z - player.transform.position.z);
			Vector3 diffVectorNormalized = differenceVector.normalized;
			transform.position = new Vector3 (transform.position.x - diffVectorNormalized.x * -15, 2.83f, transform.position.z - diffVectorNormalized.z * -15);
			player.GetComponent<PlayerMovement> ().AddEnergy ();


			ActiveHalo ();

			Invoke ("DeactiveHalo", 0.15f);


			this.life--;

			if (this.life <= 0) {
				Destroy (gameObject);
				if (SceneManager.GetActiveScene ().name == "Tutorial") {
					GameObject.Find ("GuideText").GetComponent<Level1> ().AddKillCount ();
				}
			}
		}
			
	}

	void OnCollisionEnter (Collision col){

		if (col.collider.tag == "BulletSkill") {
		
			ActiveHalo ();

			Invoke ("DeactiveHalo", 0.15f);


			this.life--;

			if (this.life <= 0) {
				Destroy (gameObject);
				if (SceneManager.GetActiveScene ().name == "Tutorial") {
					GameObject.Find ("GuideText").GetComponent<Level1> ().AddKillCount ();
				}
			}
		}
	}

	private void DeactiveHalo(){
		halo.enabled = false;
	}

	private void ActiveHalo(){
		halo.enabled = true;
	}
}
