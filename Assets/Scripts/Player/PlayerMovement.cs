using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	public float movSpeed;
	public float jumpForce;
	//public float maxEnergy;
	//public float maxVelocity;

	private Vector3 movDirection;
	private Vector3 playerDirection;
	//private CharacterController controller;

	private Rigidbody rb;

	private bool isGrounded;
	private bool airborne;


	private bool firstButtonPressedF;	//Foward
	private bool firstButtonReleasedF;
	private bool resetF;
	private float timeOfFirstButtonF;

	private bool firstButtonPressedB;	//Back
	private bool firstButtonReleasedB;
	private bool resetB;
	private float timeOfFirstButtonB;

	private bool firstButtonPressedL;	//Left
	private bool firstButtonReleasedL;
	private bool resetL;
	private float timeOfFirstButtonL;

	private bool firstButtonPressedR;	//Right
	private bool firstButtonReleasedR;
	private bool resetR;
	private float timeOfFirstButtonR;

	private float range; 

	private Image dash;
	private Image healthBar;
	private Image energyBar;
	private Image clock;

	private bool delayAttack, delayAA;
	private float count, countAA;

	public Material black;
	public Material red;

//	private Renderer[] rend = new Renderer[9];

	private bool slowedTime;

	private GameObject backWall, frontWall, leftWall, rightWall, cam;


	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody> ();

		range = 1;
		dash = GameObject.Find ("DashF").GetComponent<Image> ();
		healthBar = GameObject.Find ("HealthBar").GetComponent<Image> ();
		energyBar = GameObject.Find ("EnergyBar").GetComponent<Image> ();
		clock = GameObject.Find ("Clock").GetComponent<Image> ();

		delayAttack = false;
		delayAA = false;
		countAA = 0;

		slowedTime = false;

//		for(int i = 0; i < 9; i++){
//			rend [i] = GameObject.Find ("b" + i).GetComponent<Renderer> ();
//		}

		backWall = GameObject.Find ("BackWall");
		frontWall = GameObject.Find ("FrontWall");
		leftWall = GameObject.Find ("LeftWall");
		rightWall = GameObject.Find ("RightWall");
		cam = GameObject.Find ("Main Camera");

	}
	
	// Update is called once per frame
	void Update () {

		if(InputArcade.Apertou(0,EControle.AZUL) || InputArcade.Apertou(1,EControle.AZUL)){
			slowedTime = !slowedTime;
		}

		movDirection = new Vector3 (InputArcade.Eixo (0, EEixo.HORIZONTAL) * range, 0, InputArcade.Eixo (0, EEixo.VERTICAL) * range);

		rb.AddForce (movDirection * movSpeed * Time.deltaTime * 1000);

		if (movDirection != Vector3.zero) {
			playerDirection = movDirection.normalized;
			transform.rotation = Quaternion.LookRotation (movDirection);
		}

		if (delayAttack) {
			if (InputArcade.Eixo (1, EEixo.VERTICAL) > 0) {
				Jump ();
			}
			if (InputArcade.Eixo (1, EEixo.HORIZONTAL) < 0) {
				if (energyBar.fillAmount >= 0.2f) {
					ShotAttack ();
				}
			}
			if (delayAA) {
				if (InputArcade.Eixo (1, EEixo.HORIZONTAL) > 0) {
					BasicAttack ();
					delayAA = false;
				}
			}
			if (InputArcade.Eixo (1, EEixo.VERTICAL) < 0) {
				if (energyBar.fillAmount >= 0.35f) {
					SpecialAttack ();
				}
			}
			delayAttack = false;
		}

		if (!delayAA) {
			countAA += Time.deltaTime;
			if (countAA >= 0.5f) {
				countAA = 0;
				delayAA = true;
			}
		}

		if (delayAttack == false) {
			if (InputArcade.Eixo (1, EEixo.VERTICAL) == 0 && InputArcade.Eixo (1, EEixo.HORIZONTAL) == 0) {
				delayAttack = true;
			}

		}

		if (dash.fillAmount >= 1) {
			// ||||||||||||||||||||||||||||||||||||||||| Dash Foward |||||||||||||||||||||||||||||||||||||||||
			if (InputArcade.Eixo (0, EEixo.VERTICAL) > 0 && firstButtonPressedF && firstButtonReleasedF) {
				if (Time.time - timeOfFirstButtonF < 0.5f) {
					transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 20);
					dash.fillAmount = 0;
				}
				resetF = true;
			}

			if (InputArcade.Eixo (0, EEixo.VERTICAL) > 0 && !firstButtonPressedF) {
				firstButtonPressedF = true;
				timeOfFirstButtonF = Time.time;
			}
			if (InputArcade.Eixo (0, EEixo.VERTICAL) == 0 && firstButtonPressedF) {
				firstButtonReleasedF = true;
			}

			if (resetF) {
				firstButtonPressedF = false;
				firstButtonReleasedF = false;
				resetF = false;
			}

			// ||||||||||||||||||||||||||||||||||||||||| Dash Backward |||||||||||||||||||||||||||||||||||||||||
			if (InputArcade.Eixo (0, EEixo.VERTICAL) < 0 && firstButtonPressedB && firstButtonReleasedB) {
				if (Time.time - timeOfFirstButtonB < 0.5f) {
					transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 20);
					dash.fillAmount = 0;
				}
				resetB = true;
			}

			if (InputArcade.Eixo (0, EEixo.VERTICAL) < 0 && !firstButtonPressedB) {
				firstButtonPressedB = true;
				timeOfFirstButtonB = Time.time;
			}
			if (InputArcade.Eixo (0, EEixo.VERTICAL) == 0 && firstButtonPressedB) {
				firstButtonReleasedB = true;
			}

			if (resetB) {
				firstButtonPressedB = false;
				firstButtonReleasedB = false;
				resetB = false;
			}

			// ||||||||||||||||||||||||||||||||||||||||| Dash Right |||||||||||||||||||||||||||||||||||||||||
			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) > 0 && firstButtonPressedR && firstButtonReleasedR) {
				if (Time.time - timeOfFirstButtonR < 0.5f) {
					transform.position = new Vector3 (transform.position.x + 20, transform.position.y, transform.position.z);
					dash.fillAmount = 0;
				}
				resetR = true;
			}

			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) > 0 && !firstButtonPressedR) {
				firstButtonPressedR = true;
				timeOfFirstButtonR = Time.time;
			}
			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) == 0 && firstButtonPressedR) {
				firstButtonReleasedR = true;
			}


			if (resetR) {
				firstButtonPressedR = false;
				firstButtonReleasedR = false;
				resetR = false;
			}

			// ||||||||||||||||||||||||||||||||||||||||| Dash Left |||||||||||||||||||||||||||||||||||||||||
			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) < 0 && firstButtonPressedL && firstButtonReleasedL) {
				if (Time.time - timeOfFirstButtonL < 0.5f) {
					transform.position = new Vector3 (transform.position.x - 20, transform.position.y, transform.position.z);
					dash.fillAmount = 0;
				}
				resetL = true;
			}

			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) < 0 && !firstButtonPressedL) {
				firstButtonPressedL = true;
				timeOfFirstButtonL = Time.time;
			}

			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) == 0 && firstButtonPressedL) {
				firstButtonReleasedL = true;
			}

			if (resetL) {
				firstButtonPressedL = false;
				firstButtonReleasedL = false;
				resetL = false;
			}
		}

		if (backWall.transform.position.z > cam.transform.position.z) {
			backWall.GetComponent<MeshRenderer> ().enabled = false;
		} else {
			backWall.GetComponent<MeshRenderer> ().enabled = true;
		}

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


		if (dash.fillAmount < 1) {
			dash.fillAmount += Time.deltaTime / 5;
		}
		if (energyBar.fillAmount < 1 && !airborne && !slowedTime) {
			energyBar.fillAmount += Time.deltaTime / 10;
		}
		if (airborne) {
			energyBar.fillAmount -= Time.deltaTime / 5;
		}
		if (energyBar.fillAmount <= 0.001f) {
			airborne = false;
			slowedTime = false;
			rb.constraints = ~RigidbodyConstraints.FreezePosition;
		}

		if (slowedTime) {
			energyBar.fillAmount -= Time.deltaTime / 20;
			clock.enabled = true;
		} else {
			clock.enabled = false;
		}
	}
	

	private void Jump(){
		if (isGrounded) {
			rb.AddForce (transform.up * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		} else {
			if (!airborne) {
				rb.constraints = RigidbodyConstraints.FreezeAll;
				airborne = true;
			} else {
				rb.constraints = ~RigidbodyConstraints.FreezePosition;
				airborne = false;
			}
		}
	}

	private void BasicAttack(){
		if (isGrounded) {
			GameObject basicAttack = Instantiate (Resources.Load ("BasicAttack")) as GameObject;
			basicAttack.transform.position = new Vector3 (transform.position.x + playerDirection.x * 7.0f, transform.position.y + playerDirection.y * 7.0f, transform.position.z + playerDirection.z * 7.0f);
			basicAttack.transform.Rotate (playerDirection);
		} else {
			GameObject basicAttack = Instantiate (Resources.Load ("BasicAttack")) as GameObject;
			basicAttack.transform.position = new Vector3 (transform.position.x + playerDirection.x * 2.0f, 0, transform.position.z + playerDirection.z * 2.0f);
			basicAttack.transform.Rotate (playerDirection);
			rb.constraints = ~RigidbodyConstraints.FreezePosition;
			airborne = false;
		}

	}

	private void ShotAttack(){
		if (isGrounded) {
			GameObject bullet = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet.transform.position = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
			bullet.GetComponent<Rigidbody> ().velocity = playerDirection * 100;
			energyBar.fillAmount -= 0.2f;
		} else if (!isGrounded && airborne) {
			GameObject bullet = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			bullet.GetComponent<Rigidbody> ().velocity = new Vector3 (playerDirection.x * 100, playerDirection.y - 25, playerDirection.z * 100);
			energyBar.fillAmount -= 0.2f;
			rb.constraints = ~RigidbodyConstraints.FreezePosition;
			airborne = false;
		} else if (!isGrounded && !airborne) {
		}
	}

	private void SpecialAttack(){
		if (isGrounded) {
			GameObject specialSkill = Instantiate (Resources.Load ("SpecialSkill")) as GameObject;
			specialSkill.transform.position = new Vector3 (transform.position.x + playerDirection.x * 20, transform.position.y + playerDirection.y * 20, transform.position.z + playerDirection.z * 20);
			specialSkill.transform.rotation = Quaternion.LookRotation (playerDirection);
			energyBar.fillAmount -= 0.35f;
		} else if (!isGrounded && airborne) {
			transform.position = new Vector3 (transform.position.x, 2.185f, transform.position.z);
			GameObject specialSkill = Instantiate (Resources.Load ("SpecialSkill")) as GameObject;
			specialSkill.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 2);
			specialSkill.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 50.0f);
			GameObject specialSkill2 = Instantiate (Resources.Load ("SpecialSkill")) as GameObject;
			specialSkill2.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 2);
			specialSkill2.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, -50.0f);
			GameObject specialSkill3 = Instantiate (Resources.Load ("SpecialSkill")) as GameObject;
			specialSkill3.transform.position = new Vector3 (transform.position.x + 2, transform.position.y, transform.position.z);
			specialSkill3.transform.Rotate (Vector3.up * 90);
			specialSkill3.GetComponent<Rigidbody> ().velocity = new Vector3 (50.0f, 0, 0);
			GameObject specialSkill4 = Instantiate (Resources.Load ("SpecialSkill")) as GameObject;
			specialSkill4.transform.position = new Vector3 (transform.position.x - 2, transform.position.y, transform.position.z);
			specialSkill4.transform.Rotate (Vector3.up * 90);
			specialSkill4.GetComponent<Rigidbody> ().velocity = new Vector3 (-50.0f, 0, 0);
			energyBar.fillAmount -= 0.35f;
			rb.constraints = ~RigidbodyConstraints.FreezePosition;
			airborne = false;
		} else if (!isGrounded && !airborne) {
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Ground") {
			isGrounded = true;
		}
		if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Block" || collision.gameObject.tag == "Sword") {
			healthBar.fillAmount -= 0.15f;
			Vector3 differenceVector = new Vector3 (collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y, collision.transform.position.z - transform.position.z);
			Vector3 diffVectorNormalized = differenceVector.normalized;
			transform.position = new Vector3 (transform.position.x - diffVectorNormalized.x * 15, 2.2f, transform.position.z - diffVectorNormalized.z * 15);
//			for (int i = 0; i <= 8; i++) {
//				rend [i].material = red;
//			}

			Invoke ("ChangeToBlack", 0.05f);

		}

	}

	private void ChangeToBlack(){
//		for (int i = 0; i <= 8; i++) {
//			rend [i].material = black;
//		}
	}

	public bool GetIsGrounded(){
		return isGrounded;
	}
	public bool GetAirborne(){
		return airborne;
	}
	public void AddEnergy(){
		if (!slowedTime) {
			energyBar.fillAmount += 0.1f;
		} else {
			energyBar.fillAmount += 0.01f;
		}
	}
	public bool GetSlowedTime(){
		return slowedTime;
	}
}
