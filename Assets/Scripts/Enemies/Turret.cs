using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	private GameObject player;
	private Color col;

	private Vector3 newPos;
	private float count = 0;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");	
		col = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;

		if (count > 3) {
			//CheckPosition ();
			DrawLine (transform.position, player.transform.position, col); 
			count = 0;
		}
	}

	void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 2f)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.startColor = color;
		lr.startWidth = 1f;
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		GameObject.Destroy(myLine, duration);
	}

	void CheckPosition(){
//		if (transform.position.x - player.transform.position.x > 0) {
//			newPos.x = player.transform.position.x * 5;
//		} else {
//			newPos.x = player.transform.position.x * 5;		
//		}
//
//		if (transform.position.z - player.transform.position.z > 0) {
//			newPos.z = player.transform.position.z * 5;
//		} else {
//			newPos.z = player.transform.position.z * 5;		
//		}
		newPos.x = player.transform.position.x * 10;
		newPos.y = transform.position.y;
		newPos.z = player.transform.position.z * 10;

	}
}
