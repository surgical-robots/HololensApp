using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using System.Net;
using System.Net.Sockets;
#endif

#if !UNITY_EDITOR
using Windows.Networking.Sockets;
using Windows.Networking.Connectivity;
using Windows.Networking;
using Windows.Storage.Streams;
#endif
[System.Serializable]
public class VirtualRobotPosition
{
    public double RUpperBevel;
    public double RLowerBevel;
    public double RElbow;
    public double RTwist;
    public double ROpen;
    public double RClose;
    public double LUpperBevel;
    public double LLowerBevel;
    public double LElbow;
    public double LTwist;
    public double LOpen;
    public double LClose;
}

public class rotate2 : MonoBehaviour
{
#if !UNITY_EDITOR
    DatagramSocket socket;
    string message;
    VirtualRobotPosition pos = new VirtualRobotPosition();
    async void Start()
    {
        socket = new DatagramSocket();
        socket.MessageReceived += Socket_MessageReceived;


        try
        {
            await socket.ConnectAsync(new Windows.Networking.HostName("129.93.15.115"), "1236");
            DataWriter writer = new DataWriter(socket.OutputStream);
            writer.WriteString("Hello world");
            await writer.StoreAsync();
            message = "";
        }
        catch (Exception e)
        {
            message = e.Message;
        }

    }

    void Update()
    {
        GameObject newG = GameObject.Find("MyText");
        TextMesh newMa = newG.GetComponent<TextMesh>();
        newMa.text = message; 
        pos = JsonUtility.FromJson<VirtualRobotPosition>(message);
        updateRobot();
    }

    private async void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender,
        Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
    {
        //Read the message that was received from the UDP echo client.
        Stream streamIn = args.GetDataStream().AsStreamForRead();
        StreamReader reader = new StreamReader(streamIn);
        message = await reader.ReadLineAsync();
       
    }




    public void updateRobot()
    {
        GameObject newG1 = GameObject.Find("robot");
        newG1.transform.GetChild(0).transform.eulerAngles=new Vector3(1000*(Convert.ToSingle(pos.RUpperBevel)-Convert.ToSingle(Math.Floor(pos.RUpperBevel))),0,0);
        newG1.transform.GetChild(1).transform.eulerAngles = new Vector3(-1500.0f*(Convert.ToSingle(pos.LUpperBevel)-Convert.ToSingle(Math.Floor(pos.LUpperBevel))), 0, 0);
        newG1.transform.GetChild(0).transform.GetChild(2).transform.eulerAngles = new Vector3(0,-1500*(Convert.ToSingle(pos.RLowerBevel)-Convert.ToSingle(Math.Floor(pos.RLowerBevel))), 0);
        newG1.transform.GetChild(1).transform.GetChild(2).transform.eulerAngles = new Vector3(0,1000*(Convert.ToSingle(pos.LLowerBevel)-Convert.ToSingle(Math.Floor(pos.LLowerBevel))), 0);
        newG1.transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).transform.eulerAngles =  new Vector3(0,0,1000.0f*Convert.ToSingle(pos.RTwist));

    }



#endif

#if UNITY_EDITOR  //for editor
    //internet connection 
    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamReader reader;
    //private StreamWriter writer;
    private bool connected;

    private string jsstring;
    public float rot;





    void Start()
    {
        rot = 1.0F;
        socketReady = false;
        connected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!connected)//connect to server first time
            ConnectToServer();
        if (socketReady)//connected
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                    OnIncomingData(data);
            }
        }


    }

    private void OnIncomingData(string data)
    {
        //string jsstring = File.ReadAllText(Application.dataPath + "/strings.json");
        //////in debug, need copy jsonfile to data under app/data every build will erase this folder
        //Debug.Log(jsstring);
        //Debug.Log(data);

        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.GetChild(0).transform.Rotate(Vector3.left, -rot);

            gameObject.transform.GetChild(1).transform.Rotate(Vector3.left, -rot);

        }
        if (1 == 1)
        {

            gameObject.transform.GetChild(0).transform.GetChild(2).transform.Rotate(Vector3.up, rot);
            gameObject.transform.GetChild(1).transform.GetChild(2).transform.Rotate(Vector3.up, -rot);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {

            gameObject.transform.GetChild(0).transform.GetChild(2).transform.Rotate(Vector3.up, -rot);
            gameObject.transform.GetChild(1).transform.GetChild(2).transform.Rotate(Vector3.up, rot);
        }

        gameObject.transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).transform.Rotate(Vector3.back, rot * 5);


    }

    private void ConnectToServer()
    {
        if (socketReady)
        {
            return;
        }
        string host = "127.0.0.1";//ip
        int port = 1632;//port
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            //writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
            connected = true;
        }
        catch (Exception e)
        {
            Debug.Log("socket error" + e.Message);
        }


    }


#endif

}
