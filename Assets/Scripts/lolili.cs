using UnityEngine;
using System.Collections;

public class lolili : MonoBehaviour {

    MovieTexture mt = null;
    public float duration = 0f;
    public bool once = false;

	// Use this for initialization
	void Start () {
        once = false;
        mt = renderer.material.mainTexture as MovieTexture;
        audio.clip = mt.audioClip;
        duration = mt.duration;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        if (mt.isReadyToPlay)
        {

            mt.Play();
            
            audio.Play();
            once = true;
        }
    }

    public void Stop()
    {
        if (mt != null)
        {
            mt.Stop();
            audio.Stop();
        }
    }
     
}
