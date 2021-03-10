using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using Microsoft.MixedReality.Toolkit.UI;

/*ユーザボタンの作成*/

public class CreateUsersButton : MonoBehaviour
{
    private RosSocket rosSocket;
    public int UpdateTime = 0;
    public string UserNo;
    public bool no_check_topic = false;

    IDdecision iddecision;
    GameObject WorldEditorID;
    private int holoid;
    public GameObject UsersButton; //ユーザナンバー表示ボタン
    public GameObject UsersText; //ユーザ表示文字
    public GameObject UserCountButton; //ユーザ数をカウントするボタン
    private GameObject UT;
    private GameObject UB;
    public static int userno; //自身のユーザナンバー
    private int oriuserno;
    private int cnt;
    public static int MyNo;

    void Start()
    {
        userno = 0;
        oriuserno = 0;
        cnt = 0;
        MyNo = 0;

        WorldEditorID = GameObject.Find("WorldEditor"); //WorldEditor探索
        iddecision = WorldEditorID.GetComponent<IDdecision>();
        holoid = iddecision.ids;
        Invoke("Init", 1.0f);

    }
    public void Init()
    {
        rosSocket = GetComponent<RosConnector>().RosSocket;
        //Topicを受け取ったら関数NuｍResが呼び出される
        rosSocket.Subscribe("/countNo", "std_msgs/String", NoRes, UpdateTime);
    }

    void NoRes(Message message) //受け取ったメッセージを格納
    {
        StandardString datas = (StandardString)message;
        UserNo = datas.data;
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
        userno = int.Parse(UserNo);

        if (cnt == 0)
        {
            MyNo = userno; ;
        }

        if (oriuserno != userno)
        {
            oriuserno = userno;
            iddecision.ids = userno;
            if (cnt == 0) //各スクリプト起動
            {
                this.GetComponent<StrPub>().enabled = true;
                this.GetComponent<Subscriber2>().enabled = true;
                this.GetComponent<Subscriber3>().enabled = true;
                this.GetComponent<DBsubscriber>().enabled = true;
                this.GetComponent<MyInfoPub>().enabled = true;
                this.GetComponent<InfoSubPub>().enabled = true;
            }
            for (int i = cnt; i < userno; i++) //ボタンの表示位置調整
            {
                //Debug.Log("i:" + i + ",cnt:" + cnt + ",user:" + userno);

                UB = Instantiate(UsersButton, new Vector3(0.3f, 0.1f - (i * 0.15f), 0.8f), new Quaternion(0, 0, 0, 0));
                UT = Instantiate(UsersText, new Vector3(0.3f, 0.1f - (i * 0.15f), 0.8f), new Quaternion(0, 0, 0, 0));
                UB.gameObject.name = "UserButton" + (i + 1);
                UB.gameObject.GetComponent<ButtonNo>().buttonNo = i + 1;
                UT.gameObject.name = "User3DText" + (i + 1);


                if (userno == (i + 1) && cnt == 0) //自分の番号にMeを付ける
                {
                    UT.GetComponent<TextMesh>().text = "User" + (i + 1) + " (Me)";
                    UT.GetComponent<TextMesh>().color = new Color32(255, 0, 0, 255); //色を赤にする
                }
                else
                {
                    UT.GetComponent<TextMesh>().text = "User " + (i + 1);
                }
            }
            UserCountButton.SetActive(false);
            cnt = cnt + 1;
        }
        no_check_topic = false;
    }
}
