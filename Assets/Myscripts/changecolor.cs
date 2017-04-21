using UnityEngine;
using System.Collections;

public class changecolor : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {

            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {

            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }

            //gameObject.transform.GetChild(2).gameObject.GetComponentInChildren<Renderer>().material.color = Color.blue;
            if (Input.GetKeyDown(KeyCode.R))
        {

            gameObject.GetComponent<Renderer>().material.color = Color.red;
            //gameObject.transform.GetChild(2).gameObject.GetComponentInChildren<Renderer>().material.color = Color.blue;
            //    //gameObject.GetComponentInParent<Renderer>().material.color = Color.green;
            //    this.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.green;
            //}
            //transform.Rotate(Vector3.left,5);

            //if (Input.GetKey(KeyCode.UpArrow))
            //{
            //    transform.Rotate(Vector3.left, 10);

            //}
        }
    }
}