using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;
using UnityEngine.UI;


public class Lv5_SeletivaFinal : MonoBehaviour {
	private Text guideText;
	private Image textBox;

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
		textBox = GameObject.Find ("TextBox").GetComponent<Image> ();
		guideText = GetComponent<Text> ();
		guideText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (InputArcade.Apertou (0, EControle.PRETO) || InputArcade.Apertou (1, EControle.PRETO)) {
			SceneManager.LoadScene ("Menu");
		}

		count += Time.deltaTime;
		if (count < 15){
			ShowText ("Esse é Encostus. Ele tem indicação de alguém que você não matou, então é normal que a gente fique mais pro lado dele. Boa sorte.");
		}
		if (count > 15 && count < 17) {
			if (!doOnce) {
				textBox.enabled = false;
				guideText.text = "";
				apagaTexto = true;
				finishedText = false;
				SpawnEnemies ();
				doOnce = true;
			}

		}if (count > 17) {
			int enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			if (enemyCount == 0) {
				textBox.enabled = true;
				ShowText ("Aaahcabou. A vaga é sua, parabéns. Passa no RH pra pegar a relação de documentos e começa na próxima segunda. Obrigado.");
				count2 += Time.deltaTime;
				if (count2 > 5) {
					SceneManager.LoadScene ("Menu");
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
