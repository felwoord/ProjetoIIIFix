using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encostus : MonoBehaviour {
	private ParticleSystem part;
	private GameObject player;

	private float life = 15;
	// Use this for initialization
	void Start () {
		part = GetComponent<ParticleSystem> ();
		player = GameObject.Find ("Player");

		part.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hit (){
		player.GetComponent<PlayerMovement> ().AddEnergy ();

		if (life > 1) {
			ActiveParticle ();

			Invoke ("DeactiveParticle", 1f);
		}
		life--;
		if (life <= 0) {
			GameObject turret = GameObject.Find ("Turret");
			Destroy (turret);
			GameObject turret1 = GameObject.Find ("Turret1");
			Destroy (turret1);
			GameObject turret2 = GameObject.Find ("Turret2");
			Destroy (turret2);
			GameObject turret3 = GameObject.Find ("Turret3");
			Destroy (turret3);
			Destroy (gameObject);
		}
	}

	private void ActiveParticle(){
		part.Play ();
	}
	private void DeactiveParticle(){
		part.Stop ();
	}
}
