using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;
using UnityEngine.UI;

public class Lv4_Psicotecnico : MonoBehaviour {
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

	private GameObject[] imp = new GameObject[3];
	private GameObject[] juan = new GameObject[2];
	private GameObject[] ghost = new GameObject[5];

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
			ShowText ("Esses são os supervisores gerais e… Mais alguns atrasados. Quê? O trânsito aqui é… Hhhhahahhah um inferno~");
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
				ShowText ("Aaaaaargh. Ok, vamos fazer uma seletiva final pra decidir logo com quem vamos ficar, antes que você mate o departamento inteiro.");
				count2 += Time.deltaTime;
				if (count2 > 9) {
					PlayerPrefs.SetInt ("5SFopen", 1);
					PlayerPrefs.Save ();
					SceneManager.LoadScene ("5SF");
				}
			}

		
		}
		
	}

	private void SpawnEnemies(){
		imp [0] = Instantiate (Resources.Load ("Imp")) as GameObject;
		imp [0].transform.position = new Vector3 (Random.Range (-180, 180), transform.position.y, Random.Range (-180, 180));
		imp [0].name = "Imp";

		imp [1] = Instantiate (Resources.Load ("Imp")) as GameObject;
		imp [1].transform.position = new Vector3 (Random.Range (-180, 180), transform.position.y, Random.Range (-180, 180));
		imp [1].name = "Imp";

		imp [2] = Instantiate (Resources.Load ("Imp")) as GameObject;
		imp [2].transform.position = new Vector3 (Random.Range (-180, 180), transform.position.y, Random.Range (-180, 180));
		imp [2].name = "Imp";

		juan [0] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan [0].transform.position = new Vector3 (Random.Range (-180, 180), 7.83f, Random.Range (-180, 180));
		juan [0].name = "Juansito";

		juan [1] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan [1].transform.position = new Vector3 (Random.Range (-180, 180), 7.83f, Random.Range (-180, 180));
		juan [1].name = "Juansito";

		ghost [0] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost [0].transform.position = new Vector3 (Random.Range (-180, 180), 6.8f, Random.Range (-180, 180));
		ghost [0].name = "Ghost";

		ghost [1] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost [1].transform.position = new Vector3 (Random.Range (-180, 180), 6.8f, Random.Range (-180, 180));
		ghost [1].name = "Ghost";

		ghost [2] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost [2].transform.position = new Vector3 (Random.Range (-180, 180), 6.8f, Random.Range (-180, 180));
		ghost [2].name = "Ghost";

		ghost [3] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost [3].transform.position = new Vector3 (Random.Range (-180, 180), 6.8f, Random.Range (-180, 180));
		ghost [3].name = "Ghost";

		ghost [4] = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost [4].transform.position = new Vector3 (Random.Range (-180, 180), 6.8f, Random.Range (-180, 180));
		ghost [4].name = "Ghost";
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
