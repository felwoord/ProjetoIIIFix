using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour {
	private bool quest1, quest2, quest3, quest4, quest5;
	private bool w, a, s, d, left, right, up, down;
	private bool aux, doOnce;
	private bool finalizou;

	private float count, count2, killCount;
	private TextMesh tm;

	private GameObject[] ghost = new GameObject[9];

	// Use this for initialization
	void Start () {
		tm = GetComponent<TextMesh> ();
		quest1 = true;
		quest2 = false;
		quest3 = false;
		quest4 = false;
		quest5 = false;
		w = false;
		a = false;
		s = false;
		d = false;
		left = false;
		right = false;
		up = false;
		down = false;
		aux = false;
		doOnce = false;
		finalizou = false;

		ghost[0] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost[0].transform.position = new Vector3 (-75, 2.82f, 1);

		ghost[1] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost[1].transform.position = new Vector3 (7, 2.82f, 1);

		ghost[2] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost[2].transform.position = new Vector3 (75, 2.82f, 1);

		ghost[3] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost[3].transform.position = new Vector3 (-100, 2.82f, 1);

		ghost[4] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost[4].transform.position = new Vector3 (100, 2.82f, 1);

		ghost[5] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost[5].transform.position = new Vector3 (50, 2.82f, 1);

		ghost[6] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost[6].transform.position = new Vector3 (50, 2.82f, 1);

		ghost[7] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost[7].transform.position = new Vector3 (25, 2.82f, 1);

		ghost[8] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost[8].transform.position = new Vector3 (-25, 2.82f, 1);


		for (int i = 0; i < 9; i++) {
			ghost[i].SetActive (false);
		}

		killCount = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("Menu");
		}
		bool isGrounded = GameObject.Find ("Player").GetComponent<PlayerMovement> ().GetIsGrounded ();
		bool airborne = GameObject.Find ("Player").GetComponent<PlayerMovement> ().GetAirborne ();
		bool slowedTime = GameObject.Find ("Player").GetComponent<PlayerMovement> ().GetSlowedTime ();
		count += Time.deltaTime;
		count2 += Time.deltaTime;

		if (quest1) {
			if (Input.GetKeyDown (KeyCode.W)) {
				w = true;
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				s = true;
			}
			if (Input.GetKeyDown (KeyCode.A)) {
				a = true;
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				d = true;
			}

			if (w && a && s && d) {
				count = 0;
				tm.text = "Voce pode apertar \nD,D/A,A/S,S/W,W \npara se deslocar rapidamente";
				quest1 = false;
				quest2 = true;
			}
		}

		if (quest2) {
			//count += Time.deltaTime;
			if (count > 8) {
				if (!up) {
					tm.text = "Agora, utilize a \nseta pra cima para pular";
				}
				if (Input.GetKeyDown (KeyCode.UpArrow)) {
					up = true;
					tm.text = "Com a seta pra direita \nvocê usa ataque de perto";
				}
				if (isGrounded) {
					if (Input.GetKeyDown (KeyCode.RightArrow) && up) {
						right = true;
						tm.text = "Seta pra esquerda  \nusará ataque à distancia";
					}
					if (Input.GetKeyDown (KeyCode.LeftArrow) && right) {
						left = true;
						tm.text = "E seta pra baixo  \npara criar uma parede";
					}
					if (Input.GetKeyDown (KeyCode.DownArrow) && left) {
						down = true;
					}
					if (up && down && left && right) {
						tm.text = "Muito bem!";
						count = 0;
						quest2 = false;
						quest3 = true;
					}
				}

			}
		}

		if (quest3) {
			//count += Time.deltaTime;
			//count2 += Time.deltaTime;

			if (count > 3) {
				if (!aux) {
					tm.text = "Voce pode flutuar ao \napertar a seta pra cima \ndurante o pulo";
				}
				if (airborne) {
					aux = true;
					tm.text = "Enquanto flutua, voce nao \npode se mover. Mas pode usar as \nhabilidades com diferencial";

					if (!doOnce) {
						count2 = 0;
						doOnce = true;
					}
				}
			}
			if (count2 > 10 && doOnce) {
				count = 0;
				quest3 = false;
				quest4 = true;
				doOnce = false;
			}
					
				
		}
		
		if (quest4) {
			if (!doOnce) {
				tm.text = "Ao apertar Space,\nvocê irá diminuir o tempo";
				doOnce = true;
			}
			if (Input.GetKeyDown (KeyCode.Space) && slowedTime && !finalizou) {
				tm.text = "Para voltar ao normal, \nuse Space novamente";
				count = 0;
			}
			if (Input.GetKeyDown (KeyCode.Space) && !slowedTime) {
				finalizou = true;
			}
			if (finalizou) {
				tm.text = "Parabens, agora \nmate os inimigos \npara prosseguir";
				quest4 = false;
				quest5 = true;
				doOnce = false;

				for (int i = 0; i < 9; i++) {
					ghost[i].SetActive (true);
				}
			}
		}

		if (quest5) {

			if (killCount >= 9) {
				SceneManager.LoadScene ("DI");
				PlayerPrefs.SetInt ("DIopen", 1);
			}
		
		}
	}

	public void AddKillCount(){
		killCount++;
	}

}
