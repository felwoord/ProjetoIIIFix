using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;
using UnityEngine.UI;

public class Lv3_TestePratico : MonoBehaviour {
	private Text guideText;
	private Image textBox;

	private float textSpeed = 0.1f;
	private float timePassed = 0;
	private int textPosition = 0;
	private bool apagaTexto = false;
	private bool finishedText;

	private float count = 0;
	private float count2 = 0;

	private bool doOnce = false;

	private GameObject[] juan = new GameObject[9];

	void Start () {
		textBox = GameObject.Find ("TextBox").GetComponent<Image> ();
		guideText = GetComponent<Text> ();
		guideText.text = "";
		
	}
	

	void Update () {
		if (InputArcade.Apertou (0, EControle.PRETO) || InputArcade.Apertou (1, EControle.PRETO)) {
			SceneManager.LoadScene ("Menu");
		}
		if(Input.GetKeyDown(KeyCode.Keypad9)){
			for (int i = 0; i <= 8; i++) {
				Destroy (juan [i]);
			}
		}
		count += Time.deltaTime;
		if (count < 15){
		ShowText ("Agora, ah… Precisamos cortar alguns funcionários júnior. Não necessariamente cortar, você pode ser criativo aí hhhhhahah. ");
		}
		if (count > 15 && count < 17) {
			if (!doOnce) {
				apagaTexto = true;
				guideText.text = "";
				finishedText = false;
				textBox.enabled = false;
				SpawnEnemies ();
				doOnce = true;
			}

		}if (count > 17) {
			int enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			if (enemyCount == 0) {
				textBox.enabled = true;
				ShowText ("Ah? Você ainda tá vivo?... Aaah… Próxima fase.");
				count2 += Time.deltaTime;
				if (count2 > 5) {
					PlayerPrefs.SetInt ("4PTopen", 1);
					PlayerPrefs.Save ();
					SceneManager.LoadScene ("4PT");
				}
			}

		}
	}

	private void SpawnEnemies(){
		juan[0] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan[0].transform.position = new Vector3 (-75, 7.83f, 5);

		juan[1] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan[1].transform.position = new Vector3 (0, 7.83f, 5);

		juan[2] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan[2].transform.position = new Vector3 (75, 7.83f, 5);

		juan[3] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan[3].transform.position = new Vector3 (-75, 7.83f, 30);

		juan[4] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan[4].transform.position = new Vector3 (0, 7.83f, 30);

		juan[5] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan[5].transform.position = new Vector3 (75, 7.83f, 30);

		juan[6] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan[6].transform.position = new Vector3 (-75, 7.83f, 80);

		juan[7] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan[7].transform.position = new Vector3 (0, 7.83f, 80);

		juan[8] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan[8].transform.position = new Vector3 (75, 7.83f, 80);
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
