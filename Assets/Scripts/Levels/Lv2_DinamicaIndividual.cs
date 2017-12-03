using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;
using UnityEngine.UI;


public class Lv2_DinamicaIndividual : MonoBehaviour {
	private GameObject player;

	private Image textBox;
	private Text guideText;

	//private float textSpeed = 0.1f;
	private float timePassed = 0;
	private int textPosition = 0;
	private bool apagaTexto = false;
	private bool finishedText;

	private bool destroyed = false;
	private Image blackFlash;
	private Color currentTransp;
	private Color transparent;

	private bool startLv2 = false;
	private bool quest1 = false;
	private bool doOnce = false;
	private float count = 0;
	private float count2 = 0;

	private GameObject[] ghost = new GameObject[9];

	public AudioClip[] vampeta = new AudioClip[3];
	private AudioSource speaker;
	private bool playAudioOnce = false;



	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		player.GetComponent<PlayerMovement> ().enabled = false;
		textBox = GameObject.Find ("TextBox").GetComponent<Image> ();
		blackFlash = GameObject.Find ("BlackFlash").GetComponent<Image> ();
		guideText = GetComponent<Text> ();
		guideText.text = "";

		blackFlash.enabled = true;
		transparent = blackFlash.color;
		transparent.a = 0f;

		speaker = GameObject.Find ("Speaker").GetComponent<AudioSource> ();



	}
	
	// Update is called once per frame
	void Update () {
		if (InputArcade.Apertou (0, EControle.PRETO) || InputArcade.Apertou (1, EControle.PRETO)) {
			SceneManager.LoadScene ("Menu");
		}

		if(Input.GetKeyDown(KeyCode.Keypad9) || InputArcade.Apertou(0,EControle.BRANCO)){
			GameObject kill = GameObject.FindWithTag ("Enemy");
			Destroy (kill);
		}

		if (!destroyed) {
			FlashIn ();
		}

		if (startLv2) {
			StartLv2 ();
		}
		if (quest1) {
			Quest1 ();
		}


	}

	private void FlashIn(){
		currentTransp = blackFlash.color;
		currentTransp.a = Mathf.Lerp (currentTransp.a, transparent.a, 0.01f);
		blackFlash.color = currentTransp;
		if (blackFlash.color.a <= 0.05f) {
			destroyed = true;
			Destroy (blackFlash);
			startLv2 = true;
		}
	}

	private void StartLv2(){
		count += Time.deltaTime;
		if (count < 11) {
			PlayAudio (vampeta [0]);
			ShowText ("Surpreso? É, é a mesma sala. Não, a gente não tem verba pra ficar esbanjando, então vamo logo com isso. Estagiários, ataquem!!", 0.075f);
		}
		if (count > 11  && count < 12) {
			Reset ();
			if (!doOnce) {
				SpawnEnemies ();
				doOnce = true;
			}

		}
		if (count > 12 && count < 23) {
			PlayAudio (vampeta [1]);
			ShowText ("Um, dois, três, quatro… Pera, cadê o resto?... Aah, que seja, os que chegarem atrasados que dêem um jeito de te matar.", 0.07f);
		}
		if (count > 23) {
			for (int i = 0; i <= 8; i++) {
				ghost[i].GetComponent<Ghost> ().enabled = true;
			}
			player.GetComponent<PlayerMovement> ().enabled = true;
			textBox.enabled = false;
			guideText.text = "";
			Reset ();
			quest1 = true;
			startLv2 = false;
		}
	}

	private void Quest1(){
		int enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if (enemyCount == 0) {
			textBox.enabled = true;
			PlayAudio (vampeta [2]);
			ShowText ("Ergh, a empresa precisava mesmo cortar gastos. \nFoice. \nCortar. \nHeheheheheh. \nPRÓXIMA ETAPA.", 0.08f);
			count2 += Time.deltaTime;
			if (count2 > 10) {
				PlayerPrefs.SetInt ("3TPopen", 1);
				PlayerPrefs.Save ();
				//GameObject.Find("DataBase").GetComponent<conexaobd>().OpenLevel(3);
				SceneManager.LoadScene ("3TP");
			}
		}
			
	}

	private void SpawnEnemies(){

		for (int i = 0; i <= 8; i++) {
			ghost[i] = Instantiate (Resources.Load ("Ghost")) as GameObject;
			ghost[i].name = "Ghost";
			ghost[i].GetComponent<Ghost> ().enabled = false;
		}


		ghost [0].transform.position = new Vector3 (0, 6.8f, 0);
		ghost [1].transform.position = new Vector3 (90, 6.8f, 0);
		ghost [2].transform.position = new Vector3 (25, 6.8f, 25);
		ghost [3].transform.position = new Vector3 (-25, 6.8f, 25);
		ghost [4].transform.position = new Vector3 (50, 6.8f, 25);
		ghost [5].transform.position = new Vector3 (-50, 6.8f, 25);
		ghost [6].transform.position = new Vector3 (50, 6.8f, 0);
		ghost [7].transform.position = new Vector3 (25, 6.8f, 0);
		ghost [8].transform.position = new Vector3 (75, 6.8f, 0);

	}

	private void PlayAudio(AudioClip aud){
		if (!playAudioOnce) {
			speaker.clip = aud;
			speaker.Play ();
			playAudioOnce = true;
		}
	}

	private void Reset(){
		apagaTexto = true;
		finishedText = false;
		playAudioOnce = false;
	}

	private void ShowText(string originalText, float textSpeed){
		timePassed += Time.deltaTime;

		if (!finishedText) {
			if (apagaTexto) {
				guideText.text = "";
				textPosition = 0;
				apagaTexto = false;
			}
			if (timePassed > textSpeed) {
				timePassed = 0;
				guideText.text += originalText [textPosition];
				textPosition++;

				if (textPosition >= originalText.Length) {
					finishedText = true;
					//this.enabled = false;

				}
			}
		}
	}
}
