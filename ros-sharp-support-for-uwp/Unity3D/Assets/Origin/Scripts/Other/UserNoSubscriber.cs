using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;

/*ユーザ番号を受け取る*/

public class UserNoSubscriber : MonoBehaviour
{
    private RosSocket rosSocket;
    public int UpdateTime = 0;
    public string UserNo;
    public bool no_check_topic = false;
    public GameObject PBH2_3296;
    protected static int DeviceNo = 0;


    private IMixedRealitySceneSystem sceneSystem;

    void Start()
    {
        sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();

        sceneSystem.LoadContent(sceneSystem.ContentSceneNames[0], LoadSceneMode.Single);

        UserNo = "0";
        Invoke("Init", 1.0f);
    }

    public void Init()
    {
        rosSocket = GetComponent<RosConnector>().RosSocket;

        //Topicを受け取ったら関数NoResが呼び出される
        rosSocket.Subscribe("/countNo", "std_msgs/String", NoRes, UpdateTime);
    }

    void NoRes(Message message)
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
        DeviceNo = int.Parse(UserNo);
        no_check_topic = false;

        this.GetComponent<RosConnector>().Disconnect();

        sceneSystem.LoadNextContent(true, LoadSceneMode.Single);
    }

    public static int GetNo()
    {
        return DeviceNo;
    }
}
