using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public GameObject replayButton, quitButton;
	private float n = 0, m = 0, max = 0, hs = 10, e = 0;
	private bool over = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Inc() {
		if (!Over ())
			n++;
		if (n > max)
			max = n;
	}

	public void Dec() {
		if (!Over ()) {
			n--;
			m++;
		}
	}

	public float Value() {
		return max;
	}

	public bool Over () {
		if (over) {
			replayButton.SetActive(true);
			quitButton.SetActive(true);
		}
		return over;
	}

	public void IncEnd () {
		if (++e >= hs)
			over = true;
		Over ();
	}

	public float ValueEnd () {
		return hs - e;
	}
}
