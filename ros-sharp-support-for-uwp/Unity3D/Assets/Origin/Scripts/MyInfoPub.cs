using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*自分の情報を送信*/

public class MyInfoPub : MonoBehaviour
{
    private RosSocket rosSocket;
    private string advertise_id;
    private StandardString message;
    public string yourinfo;

    GameObject UserButtons;
    TapOtherUser tapOU;
    public static int TUCtap = 0;
    public static int ButtonNo = 0;
    private int Mynumber;
    private int cnt = 0;

    public static int DisplayNo;

    void Start()
    {

        Mynumber = CreateUsersButton.MyNo;
        TUCtap = 0;
        ButtonNo = 0;
        UserButtons = GameObject.Find("UserButton"+Mynumber);
        tapOU = UserButtons.GetComponent<TapOtherUser>();

        cnt = 0;
        rosSocket = GetComponent<RosConnector>().RosSocket;
        yourinfo = "testdd";
        message = new StandardString();
    }

    void Update()
    {
        if (TUCtap !=0 && TUCtap!=Mynumber )
        {
            Debug.Log("OK");

            this.GetComponent<InfoSubDisplay>().enabled = true;
            DisplayNo = TUCtap;
            yourinfo = "please"+Mynumber;
            message.data = yourinfo;
            advertise_id = rosSocket.Advertise("/yourinfo" + TUCtap, "std_msgs/String");
            rosSocket.Publish(advertise_id, message);
            tapOU.tapnumber = 0;
            TUCtap = 0;
        }
    }
}
