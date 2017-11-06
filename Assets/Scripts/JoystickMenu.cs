using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickMenu : MonoBehaviour {
	public Sprite[] joystick = new Sprite[3];
	private Image img;


	private float count = 0;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;

		if (count < 0.5f) {
			img.sprite = joystick [0];
		}
		if (count > 0.5f && count < 1) {
			img.sprite = joystick [1];
		}
		if (count > 1 && count < 1.5f ){
			img.sprite = joystick [2];
		}
		if (count > 1.5f) {
			img.sprite = joystick [1];
		}
		if (count > 2) {
			count = 0;
		}
		
	}
}
