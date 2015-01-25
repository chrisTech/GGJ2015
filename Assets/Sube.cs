using UnityEngine;
using System.Collections;

public class Sube : MonoBehaviour {

	public GameObject score;

	// Use this for initialization
	void Start () {
		score.GetComponent<Score> ().Inc ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= -20) {
			Score s = score.GetComponent<Score> ();
			s.Dec();
			if (tag == "End")
				s.IncEnd();
			Destroy(gameObject);
		}
	}
}
