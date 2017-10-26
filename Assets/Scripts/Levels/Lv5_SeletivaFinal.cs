using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;
using UnityEngine.UI;


public class Lv5_SeletivaFinal : MonoBehaviour {
	private TextMesh guideText;

	private float textSpeed = 0.1f;
	private float timePassed = 0;
	private int textPosition = 0;
	private bool apagaTexto = false;
	private bool finishedText;

	private float count = 0;

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

		count++;
		if (count < 5){
			ShowText ("Esse é Encostus. Ele tem indicação de alguém que você não matou, então é normal que a gente fique mais pro lado dele. Boa sorte.");
		}
		if (count > 5 && count < 7) {
			//desativar caixa de texto (mesh)
			//spawnar inimigos (uma vez)

		}if (count > 7) {
			//checkar se todos foram mortos
			//se todos foram mortos, ativar caixa de texto e mostrar texto
			//Aaahcabou. A vaga é sua, parabéns. Passa no RH pra pegar a relação de documentos e começa na próxima segunda. Obrigado.
			//salvar que fase foi completada
			//load win game scene
		}
		
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
