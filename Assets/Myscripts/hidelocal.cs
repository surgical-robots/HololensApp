using UnityEngine;
using System.Collections;

public class hidelocal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // show
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            // hide
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
