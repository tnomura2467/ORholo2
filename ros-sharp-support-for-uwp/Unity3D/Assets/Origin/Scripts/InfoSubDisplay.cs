using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;

public class InfoSubDisplay : MonoBehaviour
{
    private RosSocket rosSocket;
    public int UpdateTime = 0;
    public string UserNo;
    public bool no_check_topic = false;
    protected static int DeviceNo = 0;


    public int[] FrameB;  //モニタリングシステムが稼働してから物体が持ち込まれるまでのFrame数(使ってない)
    public int[] FrameT;  //MSが稼働してから物体が持ち去られるまでのFrame数(使ってない)
    public int[] TimeB;  //物体が持ち込まれた時刻
    public int[] TimeT;  //物体が持ち去られた時刻(持ち去られていない場合は0が返ってくる)
    public int[] id;  //持ち込まれた順に付与される番号(使ってない)
    public int[] Xmin;  //以下その物体の（画像の）座標とサイズ
    public int[] Ymin;
    public int[] Width;
    public int[] Height;
    public int[] Depth;
    public int[] Yobi; //トピックの内容(メッセージ)を書き換えるのは手間がかかるので緊急時の為の変数(何も格納されてない)
    public int[] YobiYobi;
    public int cnt;

    public GameObject newPlane0;
    public GameObject newPlane1;
    public GameObject newPlane2;
    public GameObject newPlane3;
    public GameObject newPlane4;
    public GameObject newPlane5;
    public GameObject[] newPlane;


    void Start()
    {


        newPlane = new GameObject[6];  //物体の画像が貼られるPlane
        newPlane[0] = newPlane0;
        newPlane[1] = newPlane1;
        newPlane[2] = newPlane2;
        newPlane[3] = newPlane3;
        newPlane[4] = newPlane4;
        newPlane[5] = newPlane5;

        UserNo = "0";
        Invoke("Init", 1.0f);

    }

    public void Init()
    {
        rosSocket = GetComponent<RosConnector>().RosSocket;

        //Topicを受け取ったら関数NuｍResが呼び出される

        Debug.Log("/myinfo"+ MyInfoPub.ButtonNo);
        

        rosSocket.Subscribe("/myinfo"+MyInfoPub.ButtonNo, "detect_object/DBinfo", Yourinfo, UpdateTime);
    }

    void Yourinfo(Message message)
    {
       // Debug.Log("Info Sub Display");

        DBinfo datas = (DBinfo)message;
        FrameB = datas.FrameB;
        FrameT = datas.FrameT;
        TimeB = datas.TimeB;
        TimeT = datas.TimeT;
        id = datas.id;
        Xmin = datas.Xmin;
        Ymin = datas.Ymin;
        Width = datas.Width;
        Height = datas.Height;
        Depth = datas.Depth;
        Yobi = datas.Yobi;
        YobiYobi = datas.YobiYobi;
        cnt =  datas.cnt;


        no_check_topic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (no_check_topic)
        {
            DecideUserNo();
        }
    }
    void DecideUserNo()
    {
        /*Debug.Log("cnt"+cnt);
        Debug.Log("Xmin"+Xmin[0]);
        Debug.Log("Ymin" + Ymin[0]);
        Debug.Log("Ymin" + Depth[0]);*/


        for (int i = 0; i < cnt; i++)  //i=0は一つ目の物体についての処理 i=1は二つ目の物体についての処理 i=2は三つ目の...
        {
            //物体の情報をPleneに反映 書き直し必須
            //newPlane[i].transform.localScale = new Vector3(Width[i] * 0.001f, 0.1f, Height[i] * 0.001f);
            newPlane[i].transform.localPosition = new Vector3(Xmin[i] * 0.001f, Ymin[i] * 0.001f,Depth[i] * 0.001f);

        }



        no_check_topic = false;
    }

}
