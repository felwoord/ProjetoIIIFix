using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;
using UnityEngine.UI;

public class Lv3_TestePratico : MonoBehaviour {
	private Text guideText;
	private Image textBox;

//	private float textSpeed = 0.1f;
	private float timePassed = 0;
	private int textPosition = 0;
	private bool apagaTexto = false;
	private bool finishedText;

	private float count = 0;
	private float count2 = 0;

	private bool doOnce = false;

	private GameObject[] juan = new GameObject[4];
	private GameObject[] ghost = new GameObject[5];

	public AudioClip[] vampeta = new AudioClip[2];
	private AudioSource speaker;
	private bool playAudioOnce = false;

	void Start () {
		textBox = GameObject.Find ("TextBox").GetComponent<Image> ();
		guideText = GetComponent<Text> ();
		guideText.text = "";

		speaker = GameObject.Find ("Speaker").GetComponent<AudioSource> ();
		
	}
	

	void Update () {
		if (InputArcade.Apertou (0, EControle.PRETO) || InputArcade.Apertou (1, EControle.PRETO)) {
			SceneManager.LoadScene ("Menu");
		}
		if(Input.GetKeyDown(KeyCode.Keypad9) || InputArcade.Apertou(0,EControle.BRANCO)){
			GameObject kill = GameObject.FindWithTag ("Enemy");
			Destroy (kill);
		}

		count += Time.deltaTime;
		if (count < 12){
			PlayAudio (vampeta [0]);
			ShowText ("Agora, ah… Precisamos cortar alguns funcionários júnior. Não necessariamente cortar, você pode ser criativo aí hahahaha. ", 0.07f);
		}
		if (count > 12 && count < 13) {
			if (!doOnce) {
				guideText.text = "";
				Reset ();
				textBox.enabled = false;
				SpawnEnemies ();
				doOnce = true;
			}

		}if (count > 13) {
			int enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			if (enemyCount == 0) {
				textBox.enabled = true;
				PlayAudio (vampeta [1]);
				ShowText ("Ah? Você ainda tá vivo?... Aaah… Próxima fase.",0.06f);
				count2 += Time.deltaTime;
				if (count2 > 5) {
					PlayerPrefs.SetInt ("4PTopen", 1);
					PlayerPrefs.Save ();

					//GameObject.Find("DataBase").GetComponent<conexaobd>().OpenLevel(4);
					SceneManager.LoadScene ("4PT");
				}
			}

		}
	}

	private void SpawnEnemies(){
		juan [0] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan [0].transform.position = new Vector3 (-75, 7.83f, 5);
		juan [0].name = "Juansito";

		juan [1] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan [1].transform.position = new Vector3 (0, 7.83f, 5);
		juan [1].name = "Juansito";

		juan [2] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan [2].transform.position = new Vector3 (75, 7.83f, 5);
		juan [2].name = "Juansito";

		juan [3] = Instantiate (Resources.Load ("Juansito")) as GameObject;
		juan [3].transform.position = new Vector3 (-75, 7.83f, 30);
		juan [3].name = "Juansito";

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
