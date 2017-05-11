﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Use with Targets, A master object with Targets as children
public class TargetMaster : MonoBehaviour {	
	private Target[] Targets;
	private ICollectable[] Collectables;

	private bool levelCompleted;
	void Start () {
		//Get all Target-scripts children-targets
		Targets = GetComponentsInChildren<Target> ();
		levelCompleted = false;
		Collectables = GetComponentsInChildren<ICollectable> ();
		if (Collectables == null) Collectables = new ICollectable[0];
	}
	

	// Update is called late in every frame to make sure all target hits are registered correctly
	void LateUpdate () {

		//Check if all are hit
		bool nextLevel = false;

		//Get all current hits for targets on level
		for(int i = 0; i < Targets.Length; i++){

			//Debug.Log ("Target " + i + " hit: " + Targets[i].hit);

			if (Targets[i].hit) {
				nextLevel = true;
			} else {
				nextLevel = false;
				break;
			}
		}

		//if all are hit go to next scene
		if (nextLevel) {
			levelCompleted = true;
		}

	}
	public bool CheckLevelCompleted(){
		return levelCompleted;
	}
	public int GetCollectables(){
		if (Collectables.Length == 0)
			return 3;

		int collectedCollectables = 0;
		for (int i = 0; i < Collectables.Length; i++) {
			if (Collectables [i].Collected())
				collectedCollectables++;
		}
		float score = Mathf.Floor (((float)collectedCollectables / Collectables.Length) * 2 + 1);
		return (int) score;
	}
}

