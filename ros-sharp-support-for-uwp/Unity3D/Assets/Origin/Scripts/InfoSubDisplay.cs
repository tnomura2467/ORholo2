using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;

/*別ユーザのオブジェクト位置を受け取り表示*/

public class InfoSubDisplay : MonoBehaviour
{
    private RosSocket rosSocket;
    public int UpdateTime = 0;
    public string UserNo;
    public bool no_check_topic = false;
    protected static int DeviceNo = 0;

    public int cnt;

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
        Debug.Log("/myinfo"+ MyInfoPub.ButtonNo);
        //Topicを受け取ったら関数YourInfoが呼び出される
        rosSocket.Subscribe("/myinfo"+MyInfoPub.ButtonNo, "detect_object/TransformInfo", Yourinfo, UpdateTime);
    }

    void Yourinfo(Message message) //受け取った情報を格納
    {
        TransformInfo datas = (TransformInfo)message;
        cnt = datas.image_num;
        px = datas.position_x;
        py = datas.position_y;
        pz = datas.position_z;
        rx = datas.rotation_x;
        ry = datas.rotation_y;
        rz = datas.rotation_z;
        
        no_check_topic = true;
    }
    
    void Update()
    {
        if (no_check_topic)
        {
            DecideUserNo();
        }
    }
    void DecideUserNo()
    {
        for (int i = 0; i < cnt; i++)  //i=0は一つ目の物体についての処理 i=1は二つ目の物体についての処理 i=2は三つ目の...
        {
            //物体の情報をPleneに反映 書き直し必須
            //newPlane[i].transform.localScale = new Vector3(Width[i] * 0.001f, 0.1f, Height[i] * 0.001f);
            newPlane[i].transform.localPosition = new Vector3(px[i], py[i],pz[i]);
            newPlane[i].transform.rotation = Quaternion.Euler(rx[i], ry[i], rz[i]);
        }
        no_check_topic = false;
    }

}
