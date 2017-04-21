using UnityEngine;
using System.Collections;

public class ManifyModel : MonoBehaviour
{
    public float biggerV, biggerR, biggerB;//did not use array is stupid, but it has index problem
    public bool hide;
    public int currentModel;
    public float rotateFx,rotateFy,rotateFz;

    // Use this for initialization
    void Start()
    {
        hide = false;
        currentModel = 1;//0: video, 1 :human 2: robot
        biggerR = 0.0F;
        biggerV = 0.0F;
        biggerB = 0.0F;
        rotateFx = 0.0F;
        rotateFy = 0.0F;
        rotateFz = 0.0F;

    }

    // Update is called once per frame
    void Update()
    {

        if (currentModel == 0)//tested
        {
            GameObject newG = GameObject.Find("video");
            newG.transform.localScale += new Vector3(biggerV, biggerV, 0);
            biggerV = 0.0F;

            if (hide == true)
                newG.GetComponent<Renderer>().enabled = false;
            if (hide == false)
                newG.GetComponent<Renderer>().enabled = true;

            newG.transform.Rotate(new Vector3(0, 0, rotateFy/10.0F));//rotate video about z
            rotateFy = 0;
            rotateFx = 0;
            rotateFz = 0;

        }
        if (currentModel == 1)
        {
            GameObject newG = GameObject.Find("body");
            newG.transform.localScale += new Vector3(biggerB, biggerB, biggerB);
            biggerB = 0.0F;



            newG.transform.Rotate(new Vector3(0, 0, -1 * rotateFy));//rotate body about z

            rotateFy = 0;
            rotateFx = 0;
            rotateFz = 0;
        }
        if (currentModel == 2)
        {
            GameObject newG = GameObject.Find("robot");
            newG.transform.localScale += new Vector3(biggerR, biggerR, biggerR);
            biggerR = 0.0F;
            newG.transform.Rotate(new Vector3(0, rotateFx/2.0F, 0));// y axis of robot
            rotateFy = 0;
            rotateFx = 0;
            rotateFz = 0;
        }

    }
}
