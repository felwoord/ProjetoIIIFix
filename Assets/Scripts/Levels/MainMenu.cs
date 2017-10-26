using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ArcadePUCCampinas;

public class MainMenu : MonoBehaviour {
	private GameObject mainCanvas;
	private GameObject levelCanvas;
	private GameObject optionCanvas;
	private GameObject resetCanvas;
	private GameObject startTutorialCanvas;

	private GameObject tutorial;
	private GameObject dI;
	private GameObject tP;
	private GameObject pT;
	private GameObject sF;
	private GameObject reset;
	private GameObject reset2;
	private GameObject sT;

	private Button tutorialButton;
	private Button dIButton;
	private Button tPButton;
	private Button pTButton;
	private Button sFButton;

	private Button sTButton;

	private Button resetButton;
	private Button reset2Button;

	private GameObject target;

	private float count = 0;

	// Use this for initialization
	void Start () {
		mainCanvas = GameObject.Find ("MainCanvas");
		levelCanvas = GameObject.Find ("LevelCanvas");
		optionCanvas = GameObject.Find ("OptionCanvas");
		resetCanvas = GameObject.Find ("ResetCanvas");
		startTutorialCanvas = GameObject.Find ("StartTutorialCanvas");
	

		reset = GameObject.Find ("ResetButton");
		reset2 = GameObject.Find ("ResetButton2");

		tutorial = GameObject.Find ("Tutorial");
		dI = GameObject.Find ("DI");
		tP = GameObject.Find ("TP");
		pT = GameObject.Find ("PT");
		sF = GameObject.Find ("SF");
		sT = GameObject.Find ("StartTutorialButton");

		resetButton = GameObject.Find ("ResetButton").GetComponent<Button> ();
		reset2Button = GameObject.Find ("ResetButton2").GetComponent<Button> ();

		tutorialButton = tutorial.GetComponent<Button> ();
		dIButton = dI.GetComponent<Button> ();
		tPButton = tP.GetComponent<Button> ();
		pTButton = pT.GetComponent<Button> ();
		sFButton = sF.GetComponent<Button> ();
		sTButton = sT.GetComponent<Button> ();



		CheckLevelOpen ();

		target = mainCanvas;
	}
	
	// Update is called once per frame
	void Update () {
		if (EventSystem.current.currentSelectedGameObject.tag == "MainButtons") {
			target = mainCanvas;
		} else if (EventSystem.current.currentSelectedGameObject.tag == "LevelButtons") {
			target = levelCanvas;
		} else if (EventSystem.current.currentSelectedGameObject.tag == "OptionButtons") {
			target = optionCanvas;
		} else if (EventSystem.current.currentSelectedGameObject.tag == "ResetButtons") {
			target = resetCanvas;
		} else if(EventSystem.current.currentSelectedGameObject.tag == "StartTutorialButton"){
			target = startTutorialCanvas;
			count += Time.deltaTime;
			if (count > 10) {
				SceneManager.LoadScene ("1Tutorial");
			}
		}else{
		}



		//transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);
		transform.position = new Vector3 (Mathf.Lerp(transform.position.x, target.transform.position.x, 0.25f),Mathf.Lerp(transform.position.y, target.transform.position.y, 0.5f), transform.position.z);
	
	}

	public void CheckLevelOpen(){

		int dIopen = PlayerPrefs.GetInt ("2DIopen");
		if (dIopen == 1) {
			dIButton.interactable = true;
		} else {
			dIButton.interactable = false;
		}

		int tPopen = PlayerPrefs.GetInt ("3TPopen");
		if (tPopen == 1) {
			tPButton.interactable = true;
		} else {
			tPButton.interactable = false;
		}

		int pTopen = PlayerPrefs.GetInt ("4PTopen");
		if (pTopen == 1) {
			pTButton.interactable = true;
		} else {
			pTButton.interactable = false;
		}

		int sFopen = PlayerPrefs.GetInt ("5SFopen");
		if (sFopen == 1) {
			sFButton.interactable = true;
		} else {
			sFButton.interactable = false;
		}
	}

	public void StartTutorial(){
		EventSystem.current.SetSelectedGameObject (sT);
	}
	public void StartTutorial2(){
		SceneManager.LoadScene ("1Tutorial");
	}
	public void StartLevel2(){
		SceneManager.LoadScene ("2DI");
	}
	public void StartLevel3(){
		SceneManager.LoadScene ("3TP");
	}
	public void StartLevel4(){
		SceneManager.LoadScene ("4PT");
	}
	public void StartLevel5(){
		SceneManager.LoadScene ("5SF");
	}


	public void DeleteAllSave(){
		PlayerPrefs.SetInt("2DIopen",1);
		PlayerPrefs.SetInt ("3TPopen", 0);
		PlayerPrefs.SetInt ("4PTopen", 0);
		PlayerPrefs.SetInt ("5SFopen", 0);
		PlayerPrefs.Save ();
		dIButton.interactable = true;;
		tPButton.interactable = false;
		pTButton.interactable = false;
		sFButton.interactable = false;


		EventSystem.current.GetComponent<EventSystem> ().SetSelectedGameObject (tutorial);
	}

	public void PlayButton(){
		EventSystem.current.GetComponent<EventSystem> ().SetSelectedGameObject (tutorial);
	}
	public void OptionsButton(){
		EventSystem.current.GetComponent<EventSystem> ().SetSelectedGameObject (reset);
	}

	public void ResetButton(){
		EventSystem.current.GetComponent<EventSystem> ().SetSelectedGameObject (reset2);
	}

}
