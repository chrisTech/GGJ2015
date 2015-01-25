using UnityEngine;
using System.Collections;

public class CubeGen : MonoBehaviour {

	public GameObject template;
	public GameObject score;
	public float size = 10f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("InstantiateCube", 1f, 1f);
	}

	void InstantiateCube () {
		Vector3 p = transform.position;
		p.x += Random.Range (0, size) - size / 2;
		p.y += Random.Range (0, size) - size / 2;
		GameObject o = Instantiate (template, p, Quaternion.identity) as GameObject;
		do {
			o.renderer.material.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 2f));
		} while (o.renderer.material.color == Color.black);
		o.GetComponent<Sube> ().score = score;
		if (Random.Range(0, 15) == 0) {
			o.tag = "End";
			o.renderer.material.color = Color.black;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
