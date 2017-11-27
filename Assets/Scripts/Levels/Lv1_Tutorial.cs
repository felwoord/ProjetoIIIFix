using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ArcadePUCCampinas;

public class Lv1_Tutorial : MonoBehaviour {
	private Text guideText;

	private GameObject player;

	//private float textSpeed = 0.16f;
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

	public Sprite frente, tras, esquerdo, direito, meio, botaoAzul;
	private Image joystick;
	private float countImg = 0;

	private bool doOnceGhost = false;


	void Start () {
		joystick = GameObject.Find ("Joystick").GetComponent<Image> ();
		joystick.enabled = false;

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
		//quest3 = true;
		count = 0;
		playerCreated = true;
	}
		
	private void Introduction(){
		if (count < 18) {
			PlayAudio (vampeta [0]);
			ShowText ("Gh...Ghrriiim? Gh… Que seja, seu nome não é importante! Você quer a vaga de Anjo da Morte?... " +
			"Eehhr vamos lá. Nessa seleção você vai ter que ahn… Matar. É óbvio. É pra isso que você quer o emprego, afinal", 0.16f);
		}
		if (count > 18 && count < 19) {
			apagaTexto = true;
			finishedText = false;
		}
		if (count > 19 && count < 30) {
			ShowText ("Ironicamente nossos funcionários pra essa fase já estão mortos, mas é só você… Pegar o “espírito” da coisa hehehehe. \n" +
			"Vai ahn… Até ali pra eu começar a te avaliar.\n" +
				"(     esquerdo: para andar pra frente)", 0.1f);
			if (count > 30f) {
				joystick.enabled = true;
				ChangeSprite (meio, frente);
			}
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

		if (count > 32) {
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
			ChangeSprite (meio, frente);

			if (InputArcade.Eixo (0, EEixo.VERTICAL) > 0 /*&& finishedText == true*/) {
				up = true;
				count = 0;
				Reset ();
			}
		}



		if (up) {
			count += Time.deltaTime;
			PlayAudio (vampeta [1]);
			ShowText ("Nãnãnãnão!! Por aí não, você vai sujar todo o salão.\n" +
				"(     esquerdo: para andar para a esquerda)", 0.07f);
			if (count < 3) {
				joystick.enabled = false;
			} else {
				joystick.enabled = true;
				joystick.transform.localPosition = new Vector3 (-585, -380, 0);
				ChangeSprite (meio, esquerdo);
			}

			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) < 0 && finishedText == true) {
				up = false;
				left = true;
				Reset ();
				count = 0;
			}
		}

		if (left) {
			count += Time.deltaTime;
			PlayAudio (vampeta [2]);
			ShowText ("Ehrm… Se importa em dar a volta pela direita, eu… Sou supersticioso heheh… \n" +
				"(     esquerdo: para andar para a direita)", 0.1f);
			if (count < 3.5f) {
				joystick.enabled = false;
			} else {
				joystick.enabled = true;
				joystick.transform.localPosition = new Vector3 (-585, -380, 0);
				ChangeSprite (meio, direito);
			}


			if (InputArcade.Eixo (0, EEixo.HORIZONTAL) > 0 && finishedText == true) {
				left = false;
				right = true;
				Reset ();
				count = 0;
			}
		}

		if (right) {
			count += Time.deltaTime;
			PlayAudio (vampeta [3]);
			ShowText ("Tá, certo. Agora vira pra cá pra eu registrar sua cara no sistema.\n" +
				"(     esquerdo: para andar para trás)", 0.1f);
			if (count < 3.5f) {
				joystick.enabled = false;
			} else {
				joystick.enabled = true;
				joystick.transform.localPosition = new Vector3 (-585, -380, 0);
				ChangeSprite (meio, tras);
			}

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
				joystick.enabled = false;
				PlayAudio (vampeta [4]);
				ShowText ("Perfeito", 0.1f);
			}
			if (count > 2 && !doOnce) {
				player.GetComponent<PlayerMovement> ().enabled = false;
//				fazer algum efeito enquanto o ghost aparece
				ghost = Instantiate (Resources.Load ("Ghost")) as GameObject;
				ghost.transform.position = new Vector3 (player.transform.position.x, 6.8f, player.transform.position.z + 30);
				ghost.name = "Ghost";
				ghost.GetComponent<Ghost> ().enabled = false;
				ghostCreated = true;
				doOnce = true;
				Reset ();

			}if (count > 3 && count < 6) {
				PlayAudio (vampeta [5]);
				ShowText (" … Pegar o espírito da coisa… Entendeu? Heheheh. ", 0.1f);
			}
			if (count > 9) {
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
			PlayAudio (vampeta [6]);
			ShowText ("Ok, aqui você tem… Experiência em decaptação com foice, é isso?\n" +
				"(     direito: para atacar com a foice)", 0.1f);
			if (count < 3) {
				joystick.enabled = false;
			} else {
				joystick.enabled = true;
				joystick.transform.localPosition = new Vector3 (-585, -380, 0);
				ChangeSprite (meio, direito);
			}

			if (InputArcade.Eixo (1, EEixo.HORIZONTAL) > 0 && finishedText == true && isGrounded) {
				basicSkill = true;
				Reset ();
				count = 0;
			}
		}

		if (basicSkill) {
			if (count < 5.5f) {
				joystick.enabled = false;
			} else {
				joystick.enabled = true;
				joystick.transform.localPosition = new Vector3 (-585, -380, 0);
				ChangeSprite (meio, esquerdo);
			}

			if (count < 2) {
				PlayAudio (vampeta [7]);
				ShowText ("… Muito original.", 0.1f); //corrigir, nao saiu audio (talvez muito rapido);
			}
			if (count > 2 && count < 4) {
				Reset ();
			}
			if (count > 4) {
				PlayAudio (vampeta [8]);
				ShowText ("Facilidade com magia negra? \n\n" +
					"(     direito: para jogar uma esfera de energia)", 0.06f);


				if (InputArcade.Eixo (1, EEixo.HORIZONTAL) < 0 && finishedText == true && isGrounded) {
					basicSkill = false;
					bulletSkill = true;
					Reset ();
					count = 0;
				}
			}
		}

		if (bulletSkill) {
			if (count < 7) {
				joystick.enabled = false;
			} else {
				joystick.enabled = true;
				joystick.transform.localPosition = new Vector3 (-585, -380, 0);
				ChangeSprite (meio, frente);
			}

			if (count < 2) {
				PlayAudio (vampeta [9]);
				ShowText ("Uuh…  Impressionante…", 0.1f);
			}
			if (count > 2 && count < 4) {
				Reset ();
			}
			if (count > 4) {
				PlayAudio (vampeta [10]);
				ShowText ("Pu...Pulo? Hãã, quem coloca no currículo que sabe pular?!\n" +
					"(     direito: pra pular.)", 0.1f);

				if (InputArcade.Eixo (1, EEixo.VERTICAL) > 0 && finishedText == true) {
					bulletSkill = false;
					jumpSkill = true;
					Reset ();
					count = 0;
				}
			}
		}

		if (jumpSkill) {
			if (count < 8) {
				joystick.enabled = false;
			} else {
				joystick.enabled = true;
				joystick.transform.localPosition = new Vector3 (-585, -450, 0);
				ChangeSprite (meio, tras);
			}
			if (count < 1) {
				Reset ();
			}
			if (count > 1) {
				PlayAudio (vampeta [14]);
				ShowText ("“Conjuração de auto entidade profana ascendente do plano“...? Isso é um tipo formal de falar “Tipo um lance do mal que sai do chão”?\n" +
					"(     direito: para criar a barreira.)"
					, 0.1f);

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
			if (count < 1f) {
				joystick.enabled = false;
			} else {
				joystick.enabled = true;
				joystick.transform.localPosition = new Vector3 (-585, -380, 0);
				ChangeSprite (meio, frente);
			}
			PlayAudio (vampeta [11]);
			ShowText ("Vôo intermediário?\n\n" +
				"(     direito: durante o pulo para planar. Você ganha habilidades diferentes enquanto está no ar.)", 0.06f);

			if (airborne) {
				airborneSkill = true;
				Reset ();
				count = 0;
			}
		}
		if (airborneSkill) {
			if (count < 7.5f) {
				joystick.enabled = false;
			} else {
				joystick.enabled = true;
				joystick.transform.localPosition = new Vector3 (-575, -320, 0);
				joystick.rectTransform.localScale = new Vector2 (0.4f, 0.4f);
				ChangeSprite (botaoAzul, botaoAzul);
			}
			if (count < 6) {
				PlayAudio (vampeta [12]);
				ShowText ("… Mais pra planar do que de fato voar… Todo mundo aumenta os pontos no currículo, de qualquer jeito… ", 0.1f);
			}
			if (count > 6 && count < 6.5f) {
				Reset ();
			}
			if (count > 6.5f) {
				//PlayAudio (vampeta [12]);
				ShowText ("Congelar o tempo…? \n" +
					"(     para desacelerar o tempo)", 0.1f);

				if (slowedTime) {
					airborneSkill = false;
					slowedTimeSkill = true;
					Reset ();
					count = 0;
				}
			}
		}
		if (slowedTimeSkill) {
			joystick.enabled = false;
			if (count < 9) {
				PlayAudio (vampeta [13]);
				ShowText ("Eu devia ter percebido alguma coisa? Quer dizer, o tempo congelou pra você, não pra mim. ", 0.1f);
			}
			if (count > 9 && count < 10) {
				Reset ();
			}
			if (count > 10 && count < 13 ) {
				PlayAudio (vampeta [15]);
				ShowText ("Teletransporte.\n" +
					"(Aperte 2x rapidamente para um mesmo lado)", 0.1f);
			}
			if (count > 13 && count < 14) {
				Reset ();
			}
			if (count > 18) {
				PlayAudio (vampeta [16]);
				ShowText ("Parece que alguém não vai ter desculpa pra chegar atrasado. \nVamos para próxima etapa", 0.1f);
			}
			if (count > 25) {
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
			if (!doOnceGhost) {
				Invoke ("CreateGhost", 2.0f);
				doOnceGhost = true;
			}
		}
	}

	private void CreateGhost(){
		ghost = Instantiate (Resources.Load ("Ghost")) as GameObject;
		ghost.transform.position = new Vector3 (player.transform.position.x + Random.Range (-15,15), 6.8f, player.transform.position.z + 30);
		ghost.name = "Ghost";
		doOnceGhost = false;
	}


	private void PlayAudio(AudioClip aud){
		if (!playAudioOnce) {
			speaker.clip = aud;
			speaker.Play ();
			playAudioOnce = true;
		}
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

	private void Reset(){
		apagaTexto = true;
		finishedText = false;
		playAudioOnce = false;
	}

	private void ChangeSprite (Sprite um, Sprite dois){
		countImg += Time.deltaTime;

		if (countImg < 0.5f) {
			joystick.sprite = um;
		}
		if (countImg > 0.5f && countImg < 1) {
			joystick.sprite = dois;
		}
		if (countImg > 1){
			countImg = 0;
		}
	}
}


