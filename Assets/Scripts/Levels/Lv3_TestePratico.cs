using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;
using UnityEngine.UI;

public class Lv3_TestePratico : MonoBehaviour {
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
		ShowText ("Agora, ah… Precisamos cortar alguns funcionários júnior. Não necessariamente cortar, você pode ser criativo aí hhhhhahah. ");
		}
		if (count > 5 && count < 7) {
			//desativar caixa de texto (mesh)
			//spawnar inimigos (uma vez)

		}if (count > 7) {
			//checkar se todos foram mortos
			//se todos foram mortos, ativar caixa de texto e mostrar texto
			//Ah? Você ainda tá vivo?... Aaah… Próxima fase.
			//salvar que fase foi completada
			//load proxima fase
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
