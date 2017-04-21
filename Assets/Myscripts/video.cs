using UnityEngine;
using System.Collections;
[RequireComponent (typeof(AudioSource))]
public class video : MonoBehaviour {
    // Use this for initialization
    public MovieTexture Movie;
	void Start () {

        MovieTexture Movie =gameObject.GetComponent<Renderer>().material.mainTexture as MovieTexture;
        Movie.Play();

        gameObject.GetComponent<AudioSource>().clip = Movie.audioClip;
        gameObject.GetComponent<AudioSource>().Play();

    }
	
}
