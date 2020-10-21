using RosSharp.RosBridgeClient;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Runtime.InteropServices;
//using HoloToolkit.Unity.InputModule;


public class StrPub : MonoBehaviour
{

    private RosSocket rosSocket;
    private string advertise_id;
    private StandardString message;
    public string moji;

    public GameObject sphere;
    TapTrigger taptrigger;
    private bool tapk = false;

    void Start()
    {

        sphere = GameObject.Find("PressableButtonHoloLens2");
        taptrigger = sphere.GetComponent<TapTrigger>();

        rosSocket = GetComponent<RosConnector>().RosSocket;
        advertise_id = rosSocket.Advertise("/chatter", "std_msgs/String");
        moji = "true";
        message = new StandardString();
    }

    void Update()
    {
        tapk = taptrigger.tap;


        if (tapk == true)
        {
            moji = "true";
            message.data = moji;
            rosSocket.Publish(advertise_id, message);
            taptrigger.tap = false;
            tapk = false;
        }
    }
}
