using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;

public class InfoSubPub : MonoBehaviour
{
    private RosSocket rosSocket;
    private string advertise_id;
    public int UpdateTime = 0;
    public string please_data;
    public bool yourinfo_check_topic = false;
    protected static int DeviceNo = 0;
    private int Mynumber;

    private DBinfo message; 

    DBsubscriber dbsub;


    private int[] FrameB;  //モニタリングシステムが稼働してから物体が持ち込まれるまでのFrame数(使ってない)
    private int[] FrameT;  //MSが稼働してから物体が持ち去られるまでのFrame数(使ってない)
    private int[] TimeB;  //物体が持ち込まれた時刻
    private int[] TimeT;  //物体が持ち去られた時刻(持ち去られていない場合は0が返ってくる)
    private int[] id;  //持ち込まれた順に付与される番号(使ってない)
    private int[] Xmin;  //以下その物体の（画像の）座標とサイズ
    private int[] Ymin;
    private int[] Width;
    private int[] Height;
    private int[] Depth;
    private int[] Yobi; //トピックの内容(メッセージ)を書き換えるのは手間がかかるので緊急時の為の変数(何も格納されてない)
    private int[] YobiYobi;

    public GameObject newPlane0;
    public GameObject newPlane1;
    public GameObject newPlane2;
    public GameObject newPlane3;
    public GameObject newPlane4;
    public GameObject newPlane5;
    public GameObject[] newPlane;


    void Start()
    {
        please_data = "0";
        message = new DBinfo();
        newPlane = new GameObject[6];  //物体の画像が貼られるPlane
        newPlane[0] = newPlane0;
        newPlane[1] = newPlane1;
        newPlane[2] = newPlane2;
        newPlane[3] = newPlane3;
        newPlane[4] = newPlane4;
        newPlane[5] = newPlane5;


        dbsub = this.GetComponent<DBsubscriber>();


        Invoke("Init", 1.0f);

    }

    public void Init()
    {
        Mynumber = CreateUsersButton.MyNo;

        rosSocket = GetComponent<RosConnector>().RosSocket;

        //Topicを受け取ったら関数NuｍResが呼び出される
        rosSocket.Subscribe("/yourinfo"+Mynumber, "std_msgs/String", YourInfo, UpdateTime);
    }

    void YourInfo(Message message)
    {

        StandardString datas = (StandardString)message;

        please_data = datas.data;

        yourinfo_check_topic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (yourinfo_check_topic)
        {
            YISubscribe();
        }
    }
    void YISubscribe()
    {
        Xmin = dbsub.Xmin;
        Ymin = dbsub.Ymin;
        Depth = dbsub.Depth;
        Yobi = dbsub.Yobi;



        for (int i = 0; i < dbsub.cnt; i++) {
            //Debug.Log(Xmin[i]);
            //Debug.Log(newPlane[i].gameObject.transform.position.x);
            Xmin[i] = (int)(newPlane[i].gameObject.transform.localPosition.x *1000);
            Ymin[i] = (int)(newPlane[i].gameObject.transform.localPosition.y * 1000);
            Depth[i] = (int)(newPlane[i].gameObject.transform.localPosition.z * 1000);
            /*Debug.Log("x" + newPlane[i].gameObject.transform.localPosition.x);
            Debug.Log("y" + newPlane[i].gameObject.transform.localPosition.y);
            Debug.Log("z" + newPlane[i].gameObject.transform.localPosition.z);

            Debug.Log("Xmin" + Xmin[i]);
            Debug.Log("Ymin" + Ymin[i]);
            Debug.Log("Zmin" + Depth[i]);*/

        }
        Yobi[0] = DBsubscriber.nt.hour;
        Yobi[1] = DBsubscriber.nt.min;
        Yobi[2] = DBsubscriber.nt.sec;


        message.FrameB = dbsub.FrameB;
        message.FrameT = dbsub.FrameT;
        message.TimeB = dbsub.TimeB;
        message.TimeT = dbsub.TimeT;
        message.id = dbsub.id;
        message.Xmin = Xmin;
        message.Ymin = Ymin;
        message.Width = dbsub.Width;
        message.Height = dbsub.Height;
        message.Depth = Depth;
        message.Yobi = Yobi;
        message.YobiYobi = dbsub.YobiYobi;
        message.cnt = dbsub.cnt;


        Debug.Log(Mynumber);
        advertise_id = rosSocket.Advertise("/myinfo" + Mynumber, "detect_object/DBinfo");
        rosSocket.Publish(advertise_id, message);

        yourinfo_check_topic = false;
    }
}
