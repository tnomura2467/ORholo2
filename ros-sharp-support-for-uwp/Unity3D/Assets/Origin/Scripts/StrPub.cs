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
    //public int id;
    public string moji;

    public GameObject sphere;
    TapTrigger taptrigger;
    private bool tapk = false;

    IDdecision iddecision;
    GameObject WorldEditorID;
    private int holoid;

    void Start()
    {
        WorldEditorID = GameObject.Find("WorldEditor");
        iddecision = WorldEditorID.GetComponent<IDdecision>();
        holoid = iddecision.ids;

        sphere = GameObject.Find("PressableButtonHoloLens2");
        taptrigger = sphere.GetComponent<TapTrigger>();

        rosSocket = GetComponent<RosConnector>().RosSocket;
        advertise_id = rosSocket.Advertise("/chatter", "std_msgs/String");
        moji = "trues";
        message = new StandardString();
    }

    void Update()
    {
        tapk = taptrigger.tap;


        if (tapk == true)
        {
            if (holoid == 1)
            {
                moji = "true1";
            }else if (holoid == 2)
            {
                moji = "true2";
            }
            message.data = moji;
            rosSocket.Publish(advertise_id, message);
            taptrigger.tap = false;
            tapk = false;
        }
    }
}
