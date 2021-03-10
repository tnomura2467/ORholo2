using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*自分のユーザ番号を要求*/

public class UserPub : MonoBehaviour
{
    private RosSocket rosSocket;
    private string advertise_id;
    private StandardString message;
    public string counter;

    public GameObject UserCountButton; //ユーザカウントボタン
    TapToFirst tapto;
    private bool TUCtap = false;
    
    void Start()
    {
        UserCountButton = GameObject.Find("UserCountButton");
        tapto = UserCountButton.GetComponent<TapToFirst>();
        rosSocket = GetComponent<RosConnector>().RosSocket;
        //トピック名はusercount，型はString
        advertise_id = rosSocket.Advertise("/usercount", "std_msgs/String");
        counter = "whatNo";
        message = new StandardString();
    }

    void Update()
    {
        TUCtap = tapto.tap;

        if (TUCtap == true) //ユーザカウントボタンが押されたら要求
        {
            counter = "whatNo";
            message.data = counter;
            rosSocket.Publish(advertise_id, message);
            this.GetComponent<CreateUsersButton>().enabled = true;
            tapto.tap = false;
            TUCtap = false;
        }
    }
}
