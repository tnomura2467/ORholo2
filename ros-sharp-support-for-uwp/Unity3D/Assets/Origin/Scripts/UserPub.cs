using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPub : MonoBehaviour
{
    private RosSocket rosSocket;
    private string advertise_id;
    private StandardString message;
    public string counter;

    public GameObject UserCountButton;
    TapToFirst tapto;
    private bool TUCtap = false;

    //IDdecision iddecision;
    //GameObject WorldEditorID;

    void Start()
    {
        //WorldEditorID = GameObject.Find("WorldEditor");
        //iddecision = WorldEditorID.GetComponent<IDdecision>();

        UserCountButton = GameObject.Find("UserCountButton");
        tapto = UserCountButton.GetComponent<TapToFirst>();

        rosSocket = GetComponent<RosConnector>().RosSocket;
        advertise_id = rosSocket.Advertise("/usercount", "std_msgs/String");
        counter = "whatNo";
        message = new StandardString();
    }

    void Update()
    {
        TUCtap = tapto.tap;

        if (TUCtap == true)
        {
            counter = "whatNo";
            message.data = counter;
            rosSocket.Publish(advertise_id, message);
            //this.GetComponent<UserNoSubscriber>().enabled = true;
            //iddecision.ids = 1;
            //this.GetComponent<Subscriber2>().enabled = true;
            //this.GetComponent<Subscriber3>().enabled = true;
            this.GetComponent<CreateUsersButton>().enabled = true;
            tapto.tap = false;
            TUCtap = false;
        }
    }
}
