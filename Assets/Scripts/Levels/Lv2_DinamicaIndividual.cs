using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;
using UnityEngine.UI;


public class Lv2_DinamicaIndividual : MonoBehaviour {

	private TextMesh guideText;

	private float textSpeed = 0.1f;
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



	// Use this for initialization
	void Start () {
		blackFlash = GameObject.Find ("BlackFlash").GetComponent<Image> ();
		guideText = GetComponent<TextMesh> ();
		guideText.text = "";

		transparent = blackFlash.color;
		transparent.a = 0f;


	}
	
	// Update is called once per frame
	void Update () {
		if (InputArcade.Apertou (0, EControle.PRETO) || InputArcade.Apertou (1, EControle.PRETO)) {
			SceneManager.LoadScene ("Menu");
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
		currentTransp.a = Mathf.Lerp (currentTransp.a, transparent.a, 0.015f);
		blackFlash.color = currentTransp;
		if (blackFlash.color.a <= 0.05f) {
			destroyed = true;
			Destroy (blackFlash);
			startLv2 = true;
		}
	}

	private void StartLv2(){
		count += Time.deltaTime;
		if (count < 4) {
			ShowText ("Surpreso, ah? Sim, é a mesma sala. Não temos verba pra ficar esbanjando, então vamos logo. Estagiários, ataquem!!");
		}
		if (count > 4  && count < 6) {
			apagaTexto = true;
			finishedText = false;
			if (!doOnce) {
				//criar os esqueletos e desativar os scripts deles
			}

		}
		if (count > 6 && count < 12) {
			ShowText ("Um, dois, três, quatro… Pera, cadê o resto?... Aah, que seja, os que chegarem atrasados que dêem um jeito de tentar te matar como der.");
		}
		if (count > 12) {
			apagaTexto = true;
			finishedText = false;
			quest1 = true;
			startLv2 = false;
		}
	}

	private void Quest1(){
		//desativar caixa de texto (mesh)
		//ativar os scripts dos esqueletos
		//checkar se todos foram mortos
		//se todos foram mortos, ativar caixa de texto e mostrar texto
		//Ergh, a empresa precisava mesmo cortar gastos. Foice. Cortar. Hhhhhheheheh. PRÓXIMA ETAPA.
		//salvar que fase foi completada
		//load proxima fase
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
