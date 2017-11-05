using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;
using UnityEngine.UI;


public class Lv5_SeletivaFinal : MonoBehaviour {
	private Text guideText;
	private Image textBox;

	//private float textSpeed = 0.1f;
	private float timePassed = 0;
	private int textPosition = 0;
	private bool apagaTexto = false;
	private bool finishedText;

	private bool doOnce = false;

	private float count = 0;
	private float count2 = 0;

	private GameObject encostus;

	public AudioClip[] vampeta = new AudioClip[3];
	private AudioSource speaker;
	private bool playAudioOnce = false;

	private bool finished = false;

	// Use this for initialization
	void Start () {
		textBox = GameObject.Find ("TextBox").GetComponent<Image> ();
		guideText = GetComponent<Text> ();
		guideText.text = "";
		encostus = GameObject.Find ("Encostus");
		encostus.GetComponent<Encostus> ().enabled = false;

		speaker = GameObject.Find ("Speaker").GetComponent<AudioSource> ();


	}
	
	// Update is called once per frame
	void Update () {
		if (InputArcade.Apertou (0, EControle.PRETO) || InputArcade.Apertou (1, EControle.PRETO)) {
			SceneManager.LoadScene ("Menu");
		}
		if(Input.GetKeyDown(KeyCode.Keypad9) || InputArcade.Apertado(0,EControle.BRANCO)){
			GameObject kill = GameObject.FindWithTag ("Enemy");
			Destroy (kill);
		}

		count += Time.deltaTime;
		if (!finished) {
			if (count < 13) {
				PlayAudio (vampeta [0]);
				ShowText ("Bom, esse é Encostus. Ele tem indicação de alguém que você não matou aqui, e, bom, assim é normal que a gente queira ficar mais pro lado dele. Boa sorte.", 0.06f);
			}
			if (count > 13 && count < 14) {
				if (!doOnce) {
					textBox.enabled = false;
					guideText.text = "";
					Reset ();
					SpawnEnemies ();
					doOnce = true;
				}

			}
			if (count > 14) {
				int enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
				if (enemyCount == 0) {
					textBox.enabled = true;
					PlayAudio (vampeta [1]);
					ShowText ("Aaahcabou. A vaga é sua, parabéns. Passa no RH pra gente pegar a relação de documentos,e você começa na próxima segunda.", 0.07f);
					count2 += Time.deltaTime;
					if (count2 > 15 && count2 < 16) {
						Reset ();
						count = 0;
						finished = true;
					}
				}
			}
		} else {
			if (count > 10) {
				PlayAudio (vampeta [2]);
				ShowText ("Tava esperando o que? Cena de créditos e agradecimentos? Não, seu inferno vai começar agora!", 0.08f);
			}
			if (count > 20) {
				SceneManager.LoadScene ("Menu");
			}
		}
		
	}

	private void SpawnEnemies(){
//		encostus = Instantiate (Resources.Load ("Encostus")) as GameObject;
//		encostus.name = "Encostus";
		encostus.GetComponent<Encostus> ().enabled = true;
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
