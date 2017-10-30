using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : MonoBehaviour {
	private float turretCount;
	private float impCount;

	private float turretCD;
	private float count = 0;

	// Use this for initialization
	void Start () {
		turretCD = 8;
	}
	
	// Update is called once per frame
	void Update () {
		turretCount = GameObject.FindGameObjectsWithTag ("Turret").Length;
		if (turretCount < 4) {
			count += Time.deltaTime;
		}

		if (count > turretCD) {
			GameObject turret = Instantiate (Resources.Load ("Turret")) as GameObject;
			turret.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			count = 0;
		}

	}
}
