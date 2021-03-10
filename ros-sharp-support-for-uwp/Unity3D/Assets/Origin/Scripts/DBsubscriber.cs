using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using Microsoft.MixedReality.Toolkit.UI;

/*ORderverから情報を受け取り，オブジェクト設置*/

public class DBsubscriber : MonoBehaviour
{
    private RosSocket rosSocket;
    public int UpdateTime = 0;
    //受け取ったトピックを格納する配列
    //[0]は初めに持ち込まれた物体の情報,[1]は次に持ち込まれた物体の情報,[2]はその次に...
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

    public int cnt;  //物体の個数
    //public int one;

    public int MinTime;  //最初にイベントが起こった時刻
    public int MaxTime;  //最後にイベントが起こった時刻
    public int MinSec;   //MS稼働から最初にイベントが起こるまでの時間(秒)
    public int MaxSec;   //MS稼働から最後にイベントが起こるまでの時間(秒)
    public int[] BSec;   //MS稼働から物体が持ち込まれるまでの時間(秒)
    public int[] TSec;   //MS稼働から物体が持ち去られるまでの時間(秒)

    public float nowtime;
    public struct NowTime //今の時刻
    {
        public int hour;
        public int min;
        public int sec;
    }

    public struct BeginTime //左端の時刻
    {
        public int hour;
        public int min;
        public int sec;
    }
    public struct EndTime //右端の時刻
    {
        public int hour;
        public int min;
        public int sec;
    }

    BeginTime bt = new BeginTime { hour = 0, min = 0, sec = 0 };
    EndTime et = new EndTime { hour = 0, min = 0, sec = 0 };
    public static NowTime nt = new NowTime { hour = 0, min = 0, sec = 0 };

    public GameObject Plane0; //各オブジェクトを貼るPlane
    public GameObject Plane1;
    public GameObject Plane2;
    public GameObject Plane3;
    public GameObject Plane4;
    public GameObject Plane5;
    public GameObject[] Plane;

    public GameObject texttimeobject;
    TextMesh texttime;
    public float Slidery;
    public GameObject PinchSliders;
    public float pinchslider;

    MoveTimeBar movetime;
    public bool movecomp;

    IDdecision iddecision;
    GameObject WorldEditorID;
    private int holoid;
    private string idstr;
    public bool num_check_topic = false;

    void Start()
    {
        BSec = new int[20];
        TSec = new int[20];

        Plane = new GameObject[6];  //物体の画像が貼られるPlane
        Plane[0] = Plane0;
        Plane[1] = Plane1;
        Plane[2] = Plane2;
        Plane[3] = Plane3;
        Plane[4] = Plane4;
        Plane[5] = Plane5;

        nowtime = 0;
        texttime = texttimeobject.GetComponent<TextMesh>();

        WorldEditorID = GameObject.Find("WorldEditor");
        iddecision = WorldEditorID.GetComponent<IDdecision>();

        holoid = iddecision.ids;
        idstr = holoid.ToString("0");

        Invoke("Init", 1.0f);
    }

    public void Init()
    {
        rosSocket = GetComponent<RosConnector>().RosSocket;

        //Topicを受け取ったら関数NuｍResが呼び出される
        rosSocket.Subscribe("/shelfDB_"+idstr, "detect_object/DBinfo", NumRes, UpdateTime);
    }

    void NumRes(Message message) //受け取った情報を格納
    {
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
        cnt = datas.cnt;

        MaxTime = 0;  
        MinTime = 0; 

        num_check_topic = true; 

    }

    //物体の持ち去り時間を０時０分０秒からの秒数に変換する
    //02時10分05秒は021005という数字で送られてきて管理しづらいので
    public int ChangeTime(int time)
    {
        int ctime;
        int hour;
        int min;
        int sec;
        hour = time / 10000;
        min = time / 100 - (hour * 100);
        sec = time - (hour * 10000) - (min * 100);

        ctime = hour * 3600 + min * 60 + sec;

        return ctime;
    }

 
    void Update()
    {
        //ORサーバからのtopicを受け取ったら
        if (num_check_topic)
        {
            PlaneChange();
        }
        //最初と最後のイベント時刻を秒数に変換。30+-はなくてもよい。見やすくするため
        MaxSec = ChangeTime(MaxTime) + 30;    //30秒+-
        MinSec = ChangeTime(MinTime) - 30;
        

        Slidery = pinchslider;

        nowtime = MinSec + (MaxSec - MinSec) * (Slidery / 2);
        Slidery = Slidery * (MaxSec - MinSec) / 2f;  //??

        //TimeTextにバーの位置が指し示す時刻を表示する
        nt.hour = (int)nowtime / 3600;
        nt.min = ((int)nowtime % 3600) / 60;
        nt.sec = ((int)nowtime % 3600) % 60;
        texttime.text = "Time|| " + nt.hour.ToString() + ":" + nt.min.ToString() + ":" + nt.sec.ToString();

        //バーの位置が指し示す時刻に存在しなかった物体(の画像を貼ったPlane)を非表示にする
        for (int i = 0; i < cnt; i++) //i=0は一つ目の物体についての処理 i=1は二つ目の物体についての処理 i=2は三つ目の...
        {
            BSec[i] = ChangeTime(TimeB[i]);  //bring時の時間
            TSec[i] = ChangeTime(TimeT[i]);  //take時の時間

            //takeされなかった物体のTsec(持ち去られた時刻)は0なのでMaxSecを格納
            if (TSec[i] == 0)
            {
                TSec[i] = MaxSec;
            }

            //持ち込まれた時刻より後に、バーの位置が指し示す時刻がある。&&持ち去られた時刻より前にバーの位置が指し示す時刻がある。場合に物体表示
            if ((BSec[i] - MinSec) <= Slidery && Slidery <= (TSec[i] - MinSec))
            {
                Plane[i].SetActive(true);
            }
            else
            {
                Plane[i].SetActive(false);
            }
        }

    }
    public void OnSliderUpdated(SliderEventData eventData)
    {
        pinchslider = 2*(eventData.NewValue);

    }
        void PlaneChange()
    {
        for (int i = 0; i < cnt; i++)  //i=0は一つ目の物体についての処理 i=1は二つ目の物体についての処理 i=2は三つ目の...
        {
            //物体の情報をPleneに反映 書き直し必須
            Plane[i].transform.localScale = new Vector3(Width[i] * 0.001f, 0.1f, Height[i] * 0.001f);
            Plane[i].transform.localPosition = new Vector3((Xmin[i] * 0.01f) - 2.46f + (Width[i] * 0.005f), (Ymin[i] * 0.01f * -1) + 1.35f - (Height[i] * 0.005f), 10f + Depth[i] * 0.00001f);

            //最後のイベント時刻を知る
            MinTime = TimeB[0]; //Beginning is bringed first objects 
            MaxTime = TimeB[i];
            if (MaxTime < TimeT[i])
            {
                MaxTime = TimeT[i];
            }
        }
        bt.hour = MinTime / 10000;
        bt.min = (MinTime / 100) - (bt.hour * 100);
        bt.sec = MinTime - (bt.hour * 10000) - (bt.min * 100);

        et.hour = MaxTime / 10000;
        et.min = (MaxTime / 100) - (et.hour * 100);
        et.sec = MaxTime - (et.hour * 10000) - (et.min * 100);
        num_check_topic = false;

    }

}
