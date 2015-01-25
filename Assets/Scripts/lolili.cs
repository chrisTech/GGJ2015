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
      //  mt.Stop();
      //  mt.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitLeBordel()
    {
        once = false;
        mt = renderer.material.mainTexture as MovieTexture;
        audio.clip = mt.audioClip;
        duration = mt.duration;
    }

    public void Play()
    {
        //if (mt.isReadyToPlay)
        //{
            mt.Play();
            
            audio.Play();
            once = true;
      //  }
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
