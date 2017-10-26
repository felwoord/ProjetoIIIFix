using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;
using UnityEngine.UI;

public class Lv4_Psicotecnico : MonoBehaviour {
	private TextMesh guideText;

	private float textSpeed = 0.1f;
	private float timePassed = 0;
	private int textPosition = 0;
	private bool apagaTexto = false;
	private bool finishedText;

	private bool doOnce = false;

	private float count = 0;
	private float count2 = 0;

	// Use this for initialization
	void Start () {
		guideText = GetComponent<TextMesh> ();
		guideText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (InputArcade.Apertou (0, EControle.PRETO) || InputArcade.Apertou (1, EControle.PRETO)) {
			SceneManager.LoadScene ("Menu");
		}

		count += Time.deltaTime;
		if (count < 15){
			ShowText ("Esses são os supervisores gerais e… Mais alguns atrasados. Quê? O trânsito aqui é… Hhhhahahhah um inferno~");
		}
		if (count > 15 && count < 17) {
			if (!doOnce) {
				apagaTexto = true;
				finishedText = false;
				gameObject.GetComponent<MeshRenderer> ().enabled = false;
				SpawnEnemies ();
				doOnce = true;
			}

		}if (count > 17) {
			int enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			if (enemyCount == 0) {
				gameObject.GetComponent<MeshRenderer> ().enabled = true;
				ShowText ("Aaaaaargh. Ok, vamos fazer uma seletiva final pra decidir logo com quem vamos ficar, antes que você mate o departamento inteiro.");
				count2 += Time.deltaTime;
				if (count2 > 5) {
					PlayerPrefs.SetInt ("5SFopen", 1);
					PlayerPrefs.Save ();
					SceneManager.LoadScene ("5SF");
				}
			}

		
		}
		
	}

	private void SpawnEnemies(){
	}


	private void ShowText(string originalText){
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
