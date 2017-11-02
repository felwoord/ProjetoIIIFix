using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ArcadePUCCampinas;

public class Lv1_Tutorial : MonoBehaviour {
	private Text guideText;

	private GameObject player;

	private float textSpeed = 0.16f;
	private float timePassed = 0;
	private int textPosition = 0;
	private bool apagaTexto = false;
	private bool finishedText;

	private bool isGrounded, airborne, slowedTime;
	private bool left = false, right = false, up = false, down = false;
	private bool basicSkill = false, bulletSkill = false, jumpSkill = false, specialSkill = false;
	private bool airborneSkill = false, slowedTimeSkill = false;
	private bool startTutorialFinished = false;
	private bool quest1 = false;
	private bool quest2 = false;
	private bool quest3 = false;

	private float count;

	private bool doOnce = false;
	private bool playerCreated = false;

	private bool ghostCreated = false;
	private GameObject ghost;

	private AudioSource speaker;
	public AudioClip[] vampeta = new AudioClip[17];
	public AudioClip[] fantasma = new AudioClip[3];
	private bool playAudioOnce = false;

	void Start () {
		guideText = GetComponent<Text> ();
		guideText.text = "";

		speaker = GameObject.Find ("Speaker").GetComponent<AudioSource> ();

		Invoke ("StarTutorial", 2.0f);	



	}

	void Update () {
		timePassed += Time.deltaTime;
		if (InputArcade.Apertou (0, EControle.PRETO) || InputArcade.Apertou (1, EControle.PRETO)) {
			SceneManager.LoadScene ("Menu");
		}

		if (playerCreated) {
			isGrounded = player.GetComponent<PlayerMovement> ().GetIsGrounded ();
			airborne = player.GetComponent<PlayerMovement> ().GetAirborne ();
			slowedTime = player.GetComponent<PlayerMovement> ().GetSlowedTime ();
		}


		if (startTutorialFinished) {
			count += Time.deltaTime;
			Introduction ();
		}

		if (quest1) {
			Quest1 ();
		}
		if (quest2) {
			Quest2 ();
		}
		if (quest3) {
			Quest3 ();
		}

		if (ghostCreated) {
			GhostCreated ();
		}
	}

	private void StarTutorial(){
//		fazer algum efeito enquanto o player aparece
		player = Instantiate (Resources.Load ("Player")) as GameObject;
		player.transform.position = new Vector3 (0, 7.7f, -65.0f);
		player.name = "Player";
		//player.GetComponent<PlayerMovement> ().enabled = false;

		startTutorialFinished = true;
		count = 0;
		playerCreated = true;
	}
		
	private void Introduction(){
		if (count < 18) {
			PlayAudio (vampeta [0]);
			ShowText ("Gh...Ghrriiim? Gh… Que seja, seu nome não é importante! Você quer a vaga de Anjo da Morte?... " +
			"Eehhr vamos lá. Nessa seleção você vai ter que ahn… Matar. É óbvio. É pra isso que você quer o emprego, afinal");
		}
		if (count > 18 && count < 19) {
			apagaTexto = true;
			finishedText = false;
		}
		if (count > 19 && count < 39) {
			ShowText ("Ironicamente nossos funcionários pra essa fase já estão mortos, mas é só você… Pegar o “espírito” da coisa heheh. \n" +
			"Vai ahn… Até ali pra eu começar a te avaliar.\n" +
			"(Imagem Manete) para andar pra frente");
		}
//		if (count > 16 && count < 18) {
//			apagaTexto = true;
//			finishedText = false;
//		}
//		if (count > 18 && count < 23) {
//			ShowText ("É pra isso que você quer o\n" +
//				"emprego, afinal hehehehe…\n");
//		}
//		if (count > 23 && count < 25) {
//			apagaTexto = true;
//			finishedText = false;
//		}
//		if (count > 25 && count < 32) {
//			ShowText ("Bem, ironicamente nossos\n" +
//				"funcionários, pra essa fase,\n" +
//				"já estão mortos, mas é só pra você… \n");
//		}
//		if (count > 32 && count < 34) {
//			apagaTexto = true;
//			finishedText = false;
//		}
//		if (count > 34 && count < 38) {
//			ShowText ("Pegar o “espírito” da coisa\n" +
//				"hehehe.");
//		}

		if (count > 39) {
//			apagaTexto = true;
//			finishedText = false;
			quest1 = true;
			startTutorialFinished = false;
//			playAudioOnce = false;

		}
	}
		
	private void Quest1(){

		if (!up && !down && !left && !right) {
//			ShowText ("Vai... ahn... Até ali pra eu começar\n" +
//				"a te avaliar.\n" +
//				"(Mova Joystick esquerdo pra cima \n" +
//				"pra andar pra frente)");
			//player.GetComponent<PlayerMovement> ().enabled = true;

			if (InputArcade.Eixo (0, EEixo.VERTICAL) > 0 /*&& finishedText == true*/) {
				up = true;
				Reset ();
			}
		}



		if (up) {
			PlayAudio (vampeta [1]);
			ShowText ("Nãnãnãnão!! Por aí não, você vai sujar todo o salão.\n" +
				"(Mova Joystick esquerdo pra esquerda para andar para a esquerda)");

			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) < 0 && finishedText == true) {
				up = false;
				left = true;
				Reset ();
			}
		}

		if (left) {
			PlayAudio (vampeta [2]);
			ShowText ("Ehrm… Se importa em dar a volta pela direita, eu… Sou supersticioso heheh… \n" +
				"(Mova o Joystick esquerdo pra direita para andar para a direita)");

			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) > 0 && finishedText == true) {
				left = false;
				right = true;
				Reset ();
			}
		}

		if (right) {
			PlayAudio (vampeta [4]);
			ShowText ("Certo. Agora vira pra cá pra eu registrar sua cara no sistema.\n" +
				"(Mova Joystick esquerdo pra baixo para andar para trás)");

			if (InputArcade.Eixo (0, EEixo.VERTICAL) < 0 && finishedText == true) {
				right = false;
				down = true;
				Reset ();
				count = 0;
			}
		}
		if (down) {
			count += Time.deltaTime;
			if (count < 2) {
				PlayAudio (vampeta [5]);
				ShowText ("Perfeito");
			}
			if (count > 2 && !doOnce) {
				player.GetComponent<PlayerMovement> ().enabled = false;
//				fazer algum efeito enquanto o ghost aparece
				ghost = Instantiate (Resources.Load ("Ghost")) as GameObject;
				ghost.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + 30);
				ghost.name = "Ghost";
				ghost.GetComponent<Ghost> ().enabled = false;
				ghostCreated = true;
				doOnce = true;
				Reset ();

			}if (count > 3 && count < 6) {
				PlayAudio (vampeta [6]);
					ShowText (" … Pegar o espírito da coisa… Entendeu? Heheheh. ");
			}
			if (count > 6) {
				apagaTexto = true;
				finishedText = false;
				quest1 = false;
				quest2 = true;
				count = 0;
				playAudioOnce = false;
				player.GetComponent<PlayerMovement> ().enabled = true;
				ghost.GetComponent<Ghost> ().enabled = true;

			}
		}  
	}
		
	private void Quest2(){
		count += Time.deltaTime;
		if (!basicSkill && !bulletSkill && !jumpSkill && !specialSkill) {
			PlayAudio (vampeta [7]);
			ShowText ("Ok, aqui diz que você tem… Experiência em decaptação com foice, é isso?\n" +
				"(Aperte ?? para atacar com a foice)");

			if (InputArcade.Eixo (1, EEixo.HORIZONTAL) > 0 && finishedText == true && isGrounded) {
				basicSkill = true;
				Reset ();
			}
		}

		if (basicSkill) {
			if (count < 2) {
				PlayAudio (vampeta [8]);
				ShowText ("… Muito original.");
			}
			if (count > 2 && count < 4) {
				Reset ();
			}
			if (count > 4) {
				PlayAudio (vampeta [8]);
				ShowText ("Facilidade com magia negra? \n" +
					"(Aperte ?? para jogar uma esfera de energia)");

				if (InputArcade.Eixo (1, EEixo.HORIZONTAL) < 0 && finishedText == true && isGrounded) {
					basicSkill = false;
					bulletSkill = true;
					Reset ();
					count = 0;
				}
			}
		}

		if (bulletSkill) {
			if (count < 2) {
				PlayAudio (vampeta [9]);
				ShowText ("Uuh…  Impressionante…");
			}
			if (count > 2 && count < 4) {
				Reset ();
			}
			if (count > 4) {
				PlayAudio (vampeta [10]);
				ShowText ("Pulo?\n" +
					"(Aperte ?? pra pular.");

				if (InputArcade.Eixo (1, EEixo.VERTICAL) > 0 && finishedText == true && isGrounded) {
					bulletSkill = false;
					jumpSkill = true;
					Reset ();
					count = 0;
				}
			}
		}

		if (jumpSkill) {
			if (count < 2) {
				ShowText ("Parece um coelho");
			}
			if (count > 2 && count < 4) {
				Reset ();
			}
			if (count > 4) {
				ShowText ("Paredao, estilo BBB\n" +
					"(Aperte ?? criar uma parede.");

				if (InputArcade.Eixo (1, EEixo.VERTICAL) < 0 && finishedText == true && isGrounded) {
					jumpSkill = false;
					quest2 = false;
					quest3 = true;
					Reset ();
					count = 0;
				}
			}
		}
	}
		
	private void Quest3(){
		count += Time.deltaTime;
		if (!airborneSkill && !slowedTimeSkill) {
			ShowText ("Vôo intermediário?\n" +
				"(Aperte ?? durante o pulo para planar. Você ganha habilidades diferentes enquanto está no ar.)\n");

			if (airborne) {
				airborneSkill = true;
				Reset ();
				count = 0;
			}
		}
		if (airborneSkill) {
			if (count < 2) {
				ShowText ("… Mais pra planar do que de fato voar… Todo mundo aumenta os pontos no currículo, de qualquer jeito… ");
			}
			if (count > 2 && count < 4) {
				Reset ();
			}
			if (count > 4) {
				ShowText ("Congelar o tempo…? \n" +
				"(Aperte ?? para desacelerar o tempo)");

				if (slowedTime) {
					airborneSkill = false;
					slowedTimeSkill = true;
					Reset ();
					count = 0;
				}
			}
		}
		if (slowedTimeSkill) {
			ShowText ("Eu devia ter percebido alguma coisa? Quer dizer, o tempo congelou pra você, não pra mim. Que seja, isso é o bastante. vamos para a segunda etapa.");
			if (count > 10) {
//				PlayerPrefs.SetInt ("2DIopen", 1);
//				PlayerPrefs.Save ();
				SceneManager.LoadScene ("2DI");
			}	
		}
		
	}

	private void GhostCreated(){
		float ghostCount;
		ghostCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if (ghostCount == 0) {
			ghost = Instantiate (Resources.Load ("Ghost")) as GameObject;
			ghost.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + 30);
			ghost.name = "Ghost";
		}
	}


	private void PlayAudio(AudioClip aud){
		if (!playAudioOnce) {
			speaker.clip = aud;
			speaker.Play ();
			playAudioOnce = true;
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

	private void Reset(){
		apagaTexto = true;
		finishedText = false;
		playAudioOnce = false;
	}
}


