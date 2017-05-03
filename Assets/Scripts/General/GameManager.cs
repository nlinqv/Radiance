﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameMode { laserMode, mirrorMode, none };	// gameModes
	public enum GameState {tutorial,gameRunning,endScreen};	// gameStates available

    public static GameMode gameMode = GameMode.none;		// init gameMode to none
    private static GameState prevGameState;					// save prev gameState
	public GameObject laserRay;								
	public ViewController UI;								// UI containing all panels etc.
	public TargetMaster targetMaster;

	public static GameState gameState;
	private string levelName;
	private int tutorialIndex = 1;

	// Use this for initialization
	void Start ()
	{
		/*
			"Prata med minne"


		levelName = loadName();
		loadScore();
		loadTutorialIndex();

		*/

		// Start game with tutorial window #1
		gameState = GameState.tutorial;
        prevGameState = GameState.tutorial;
        gameMode = GameMode.none;
       
		// Access UIs components and children
		UI = GameObject.Find ("UI").GetComponent<ViewController> ();
		// Access targetMaster
		targetMaster = GameObject.Find ("TargetMaster").GetComponent<TargetMaster> ();
		// Hide mellanmeny
		UI.transform.Find ("MellanMeny").gameObject.SetActive (false);

		// If totrial index = -1 dont show anything, otherwise load tutorial with index tutorialIndex
		if (tutorialIndex >= 0) {
			LoadTutorial (tutorialIndex);
		}
	}


	void FixedUpdate ()
	{
		// When entering gameRunning from tutorial, show gameModebutton
        if(prevGameState == GameState.tutorial && gameState == GameState.gameRunning)
        {
            gameMode = GameMode.laserMode;
            UI.ShowGameModeButton();
        }
		// Decide which code is running according to gameState
		switch (gameState) {
		// Case tutorial, do something...
		case GameState.tutorial:
            break;
			// Case gameRunning, show gamemode-button and check if level is completed
            case GameState.gameRunning:
            UI.ShowGameModeButton();
			CheckLevelCompleted ();
			break;
			// Case endScreen, check what next state is
		case GameState.endScreen:
			CheckNextState ();
			break;

		default:
			break;
		}
        prevGameState = gameState; 
	}

	private void LoadTutorial (int index)
	{
		// Load info about tutorial
		string title = "title";
		string text = "sample text";
		Image icon = null;
		UI.NewTutorial (title, text, icon);
	}

	private void CheckLevelCompleted ()
	{
		// If all targets are hit, load endScreen and set gameMode = none
		if (targetMaster.CheckLevelCompleted ()) {
			LoadLevelEndScreen ();
			gameState = GameState.endScreen;
            gameMode = GameMode.none;
		}
	}

	private void LoadLevelEndScreen ()
	{
		// Load info about which level got completed
		string level = "Level";
		UI.ShowEndScreen (level, targetMaster.GetCollectables ());
	}

	private void CheckNextState ()
	{
		// Check select to decide what next state is
		switch (UI.select) {
		case ViewController.Select.menu:
			MainMenu ();
			break;
		case ViewController.Select.replay:
			Replay ();
			break;
		case ViewController.Select.next:
			NextScene ();
			break;
		default:
			break;
		}
	}

	private void NewScene (int n)
	{ // where n is offset from current scene
		Scene activeScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (activeScene.buildIndex + n);
		Debug.Log ("Entering next scene");
	}
    	

	private void NextScene ()
	{
		// Load next level
		NewScene (1);
		gameState = GameState.gameRunning;
	}

	private void Replay ()
	{
		// Load same level again
		NewScene (0);
		gameState = GameState.gameRunning;
	}

	private void MainMenu ()
	{
        SceneManager.LoadScene("StartScene");
	}

}