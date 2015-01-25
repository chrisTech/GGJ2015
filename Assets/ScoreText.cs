using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	public GameObject score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Score s = score.GetComponent<Score> ();
		if (s.Over())
			GetComponent<Text> ().text = "Game over... Game ov... Game... GAME... LE JEU\n"
				+ "Score: " + s.Value().ToString ();
		else
			GetComponent<Text> ().text = "Score: " + s.Value().ToString () + "\n"
				+ "Lives: " + s.ValueEnd().ToString();
	}
}
