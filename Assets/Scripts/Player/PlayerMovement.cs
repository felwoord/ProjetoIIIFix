using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	private Renderer rend;

	private bool slowedTime;

	private GameObject backWall, frontWall, leftWall, rightWall, cam;

	private AudioSource audioEffect;
	public AudioClip blade;
	public AudioClip shot;

	private Image basicIcon, jumpIcon, wallIcon, bulletIcon;

	private MeshCollider mC;

	void Start () {
		mC = GetComponent<MeshCollider> ();

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

		rend = GetComponent<Renderer> ();


		backWall = GameObject.Find ("BackWall");
		frontWall = GameObject.Find ("FrontWall");
		leftWall = GameObject.Find ("LeftWall");
		rightWall = GameObject.Find ("RightWall");
		cam = GameObject.Find ("Main Camera");

		audioEffect = GetComponent<AudioSource> ();

		basicIcon = GameObject.Find ("BasicAttackIcon").GetComponent<Image> ();
		jumpIcon = GameObject.Find ("JumpIcon").GetComponent<Image> ();
		wallIcon = GameObject.Find ("WallIcon").GetComponent<Image> ();
		bulletIcon = GameObject.Find ("BulletIcon").GetComponent<Image> ();

	}

	void Update () {

		if(InputArcade.Apertou(0,EControle.AZUL) || InputArcade.Apertou(1,EControle.AZUL)){
			slowedTime = !slowedTime;
		}

		Movement ();
		Attack ();
		Dash ();
		AntiBugWall ();
		EnergyBarInter ();


	}

	private void EnergyBarInter(){
		if (SceneManager.GetActiveScene ().name == "1Tutorial") {
			energyBar.fillAmount += Time.deltaTime / 5;
		} else {
			if (energyBar.fillAmount < 1 && !airborne && !slowedTime) {
				energyBar.fillAmount += Time.deltaTime / 50;
			}
		}
		if (airborne) {
			energyBar.fillAmount -= Time.deltaTime / 10;
		}
		if (energyBar.fillAmount <= 0.001f) {
			airborne = false;
			slowedTime = false;
			rb.constraints = ~RigidbodyConstraints.FreezePosition;
		}

		if (slowedTime) {
			energyBar.fillAmount -= Time.deltaTime / 10;
			clock.enabled = true;
		} else {
			clock.enabled = false;
		}
	}

	private void AntiBugWall(){
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
	}

	private void Movement(){
		movDirection = new Vector3 (InputArcade.Eixo (0, EEixo.HORIZONTAL) * range, 0, InputArcade.Eixo (0, EEixo.VERTICAL) * range);

		rb.AddForce (movDirection * movSpeed * Time.deltaTime * 1000);

		if (movDirection != Vector3.zero) {
			playerDirection = movDirection.normalized;
			transform.rotation = Quaternion.LookRotation (movDirection);
		}
	}

	private void Dash(){
		if (dash.fillAmount >= 1) {
			// ||||||||||||||||||||||||||||||||||||||||| Dash Foward |||||||||||||||||||||||||||||||||||||||||
			if (InputArcade.Eixo (0, EEixo.VERTICAL) > 0 && firstButtonPressedF && firstButtonReleasedF) {
				if (Time.time - timeOfFirstButtonF < 0.35f) {
					transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 20);
					dash.fillAmount = 0;
				}
				resetF = true;
			}

			if (InputArcade.Eixo (0, EEixo.VERTICAL) > 0 && !firstButtonPressedF) {
				firstButtonPressedF = true;
				timeOfFirstButtonF = Time.time;
				firstButtonPressedB = false;
				firstButtonReleasedB = false;
				firstButtonPressedL = false;
				firstButtonReleasedL = false;
				firstButtonPressedR = false;
				firstButtonReleasedR = false;

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
				if (Time.time - timeOfFirstButtonB < 0.35f) {
					transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 20);
					dash.fillAmount = 0;
				}
				resetB = true;
			}

			if (InputArcade.Eixo (0, EEixo.VERTICAL) < 0 && !firstButtonPressedB) {
				firstButtonPressedB = true;
				timeOfFirstButtonB = Time.time;
				firstButtonPressedF = false;
				firstButtonReleasedF = false;
				firstButtonPressedL = false;
				firstButtonReleasedL = false;
				firstButtonPressedR = false;
				firstButtonReleasedR = false;
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
				if (Time.time - timeOfFirstButtonR < 0.35f) {
					transform.position = new Vector3 (transform.position.x + 20, transform.position.y, transform.position.z);
					dash.fillAmount = 0;
				}
				resetR = true;
			}

			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) > 0 && !firstButtonPressedR) {
				firstButtonPressedR = true;
				timeOfFirstButtonR = Time.time;
				firstButtonPressedB = false;
				firstButtonReleasedB = false;
				firstButtonPressedL = false;
				firstButtonReleasedL = false;
				firstButtonPressedF = false;
				firstButtonReleasedF = false;
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
				if (Time.time - timeOfFirstButtonL < 0.35f) {
					transform.position = new Vector3 (transform.position.x - 20, transform.position.y, transform.position.z);
					dash.fillAmount = 0;
				}
				resetL = true;
			}

			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) < 0 && !firstButtonPressedL) {
				firstButtonPressedL = true;
				timeOfFirstButtonL = Time.time;
				firstButtonPressedB = false;
				firstButtonReleasedB = false;
				firstButtonPressedF = false;
				firstButtonReleasedF = false;
				firstButtonPressedR = false;
				firstButtonReleasedR = false;;
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


		if (dash.fillAmount < 1) {
			dash.fillAmount += Time.deltaTime / 5;
		}

	}

	private void Attack(){
		if (delayAttack) {
			if (InputArcade.Eixo (1, EEixo.VERTICAL) > 0) {
				Jump ();
			}
			if (InputArcade.Eixo (1, EEixo.HORIZONTAL) < 0) {
				if (energyBar.fillAmount >= 0.6f) {
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

		jumpIcon.enabled = false;
		Invoke ("IconJump", 0.1f);
	}


	private void BasicAttack(){
		if (isGrounded) {
//			GameObject basicAttack = Instantiate (Resources.Load ("BasicAttack")) as GameObject;
//			basicAttack.transform.position = new Vector3 (transform.position.x + playerDirection.x * 5.0f, 3.5f, transform.position.z + playerDirection.z * 5.0f);
//			basicAttack.transform.Rotate (playerDirection);
			GameObject attackAni = Instantiate (Resources.Load ("spine attack")) as GameObject;
			attackAni.transform.position = new Vector3 (transform.position.x + playerDirection.x * 1.0f, 2.5f, transform.position.z);
			attackAni.transform.rotation = Quaternion.LookRotation (playerDirection);
			attackAni.transform.parent = gameObject.transform;
		} else {
//			GameObject basicAttack = Instantiate (Resources.Load ("BasicAttack")) as GameObject;
//			basicAttack.transform.position = new Vector3 (transform.position.x + playerDirection.x * 2.0f, 0, transform.position.z + playerDirection.z * 2.0f);
//			basicAttack.transform.Rotate (playerDirection);
			GameObject attackAni = Instantiate (Resources.Load ("spine attack")) as GameObject;
			attackAni.transform.position = new Vector3 (transform.position.x, 2.5f, transform.position.z);
			attackAni.transform.rotation = Quaternion.LookRotation (playerDirection);
			rb.constraints = ~RigidbodyConstraints.FreezePosition;
			airborne = false;
		}

		audioEffect.clip = blade;
		audioEffect.Play ();

		basicIcon.enabled = false;
		Invoke ("IconBasic", 0.1f);

	}

	private void ShotAttack(){
		if (isGrounded) {
			GameObject bullet = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet.transform.position = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
			bullet.GetComponent<Rigidbody> ().velocity = playerDirection * 100;
			energyBar.fillAmount -= 0.6f;
		} else if (!isGrounded && airborne) {
			GameObject bullet = Instantiate (Resources.Load ("BulletSkill")) as GameObject;
			bullet.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			bullet.GetComponent<Rigidbody> ().velocity = new Vector3 (playerDirection.x * 100, playerDirection.y - 25, playerDirection.z * 100);
			energyBar.fillAmount -= 0.6f;
			rb.constraints = ~RigidbodyConstraints.FreezePosition;
			airborne = false;
		} else if (!isGrounded && !airborne) {
		}

		audioEffect.clip = shot;
		audioEffect.Play ();

		bulletIcon.enabled = false;
		Invoke ("IconBullet", 0.1f);
	}

	private void SpecialAttack(){
		if (isGrounded) {
			GameObject specialSkill = Instantiate (Resources.Load ("SpecialSkill")) as GameObject;
			specialSkill.transform.position = new Vector3 (transform.position.x + playerDirection.x * 20,  3f, transform.position.z + playerDirection.z * 20);
			specialSkill.transform.rotation = Quaternion.LookRotation (playerDirection);
			specialSkill.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			energyBar.fillAmount -= 0.35f;
		} else if (!isGrounded && airborne) {
			transform.position = new Vector3 (transform.position.x, 6.7f, transform.position.z);
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

		wallIcon.enabled = false;
		Invoke ("IconWall", 0.1f);
	}

	private void IconBasic (){
		basicIcon.enabled = true;
	}
	private void IconBullet (){
		bulletIcon.enabled = true;
	}
	private void IconJump (){
		jumpIcon.enabled = true;
	}
	private void IconWall (){
		wallIcon.enabled = true;
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
			transform.position = new Vector3 (transform.position.x - diffVectorNormalized.x * 15, 7.7f, transform.position.z - diffVectorNormalized.z * 15);

			mC.enabled = false;
			rb.useGravity = false;
			//rend.material = red;

			StartCoroutine(DoBlinks(0.35f, 0.2f));

			//Invoke ("ChangeToBlack", 0.08f);
			Invoke ("ActiveMeshCollider", 3);
			if(SceneManager.GetActiveScene ().name != "1Tutorial"){
				if (healthBar.fillAmount <= 0) {
					SceneManager.LoadScene ("Menu");
				}
			}

		}

	}
	private void ActiveMeshCollider(){
		mC.enabled = true;
		rb.useGravity = true;
	}

	IEnumerator DoBlinks(float duration, float blinkTime) {
		while (duration > 0f) {
			duration -= Time.deltaTime;

			//toggle renderer
			rend.enabled = !rend.enabled;

			//wait for a bit
			//Debug.Log(duration);
			yield return new WaitForSeconds(blinkTime);
		}

		//make sure renderer is enabled when we exit
		rend.enabled = true;
	}

	private void ChangeToBlack(){
		rend.material = black;
	}

	public bool GetIsGrounded(){
		return isGrounded;
	}

	public bool GetAirborne(){
		return airborne;
	}

	public void AddEnergy(){
		if (energyBar.fillAmount < 0.9f) {
			if (!slowedTime) {
				energyBar.fillAmount += 0.1f;
			} else {
				energyBar.fillAmount += 0.01f;
			}
		}
	}

	public bool GetSlowedTime(){
		return slowedTime;
	}

}
