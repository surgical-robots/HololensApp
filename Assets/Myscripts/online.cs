using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class online: MonoBehaviour
{
    string url = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.ogv";
    MovieTexture movieTexture;
void Start()
    {
        
        WWW www = new WWW(url);
        movieTexture =www.movie;
        gameObject.GetComponent<Renderer>().material.mainTexture = movieTexture;
        gameObject.GetComponent<AudioSource>().clip = movieTexture.audioClip;
       
       
    }

void Update()
    {
        if (movieTexture.isReadyToPlay &&!movieTexture.isPlaying)
        {
        movieTexture.Play();
        gameObject.GetComponent<AudioSource>().Play();
        }
        
    }

}