﻿using UnityEngine;
using System.Collections;

public class LOL : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RestartGame() {
		Application.LoadLevel ("retoto");
	}

	public void QuitGame() {
		Application.LoadLevel ("main");
	}
}
