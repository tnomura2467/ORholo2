using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;

/*別ユーザの要求があればオブジェクトの位置を返す*/

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
    private TransformInfo transI;

    DBsubscriber dbsub;

    private float[] px; //オブジェクトの座標，角度，サイズのxyz
    private float[] py;
    private float[] pz;
    private float[] rx;
    private float[] ry;
    private float[] rz;
    private float[] sx;
    private float[] sy;
    private float[] sz;


    public GameObject newPlane0; //新しく物体の画像が貼られるPlane
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
        transI = new TransformInfo();
        newPlane = new GameObject[6];  //物体の画像が貼られるPlane
        newPlane[0] = newPlane0;
        newPlane[1] = newPlane1;
        newPlane[2] = newPlane2;
        newPlane[3] = newPlane3;
        newPlane[4] = newPlane4;
        newPlane[5] = newPlane5;

    

        dbsub = this.GetComponent<DBsubscriber>();
        px = new float[10];
        py = new float[10];
        pz = new float[10];
        rx = new float[10];
        ry = new float[10];
        rz = new float[10];
        sx = new float[10];
        sy = new float[10];
        sz = new float[10];

        Invoke("Init", 1.0f);

    }

    public void Init()
    {
        Mynumber = CreateUsersButton.MyNo;

        rosSocket = GetComponent<RosConnector>().RosSocket;

        //Topicを受け取ったら関数YurInfoが呼び出される
        rosSocket.Subscribe("/yourinfo"+Mynumber, "std_msgs/String", YourInfo, UpdateTime);
    }

    void YourInfo(Message message)
    {

        StandardString datas = (StandardString)message;
        please_data = datas.data;
        yourinfo_check_topic = true;
    }
    void Update()
    {
        if (yourinfo_check_topic)
        {
            YISubscribe();
        }
    }
    void YISubscribe()
    {

        for (int i = 0; i < dbsub.cnt; i++) //受け取った座標を更新
        { 
            px[i] = newPlane[i].gameObject.transform.localPosition.x;
            py[i] = newPlane[i].gameObject.transform.localPosition.y;
            pz[i] = newPlane[i].gameObject.transform.localPosition.z;
            rx[i] = newPlane[i].gameObject.transform.localEulerAngles.x;
            ry[i] = newPlane[i].gameObject.transform.localEulerAngles.y;
            rz[i] = newPlane[i].gameObject.transform.localEulerAngles.z;
            sx[i] = newPlane[i].gameObject.transform.localScale.x;
            sy[i] = newPlane[i].gameObject.transform.localScale.y;
            sz[i] = newPlane[i].gameObject.transform.localScale.z;
        }
        transI.image_num = dbsub.cnt;
        transI.position_x = px;
        transI.position_y = py;
        transI.position_z = pz;
        transI.rotation_x = rx;
        transI.rotation_y = ry;
        transI.rotation_z = rz;
        transI.scale_x = sx;
        transI.scale_y = sy;
        transI.scale_z = sz;
        transI.TimeB = dbsub.TimeB;
        transI.TimeT = dbsub.TimeT;


        Debug.Log(Mynumber);
        //advertise_id = rosSocket.Advertise("/myinfo" + Mynumber, "detect_object/DBinfo");
        advertise_id = rosSocket.Advertise("/myinfo" + Mynumber, "detect_object/TransformInfo");
        rosSocket.Publish(advertise_id, transI);

        yourinfo_check_topic = false;
    }
}
