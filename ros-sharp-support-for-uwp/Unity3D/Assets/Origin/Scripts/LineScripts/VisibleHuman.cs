using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit;

public class VisibleHuman : MonoBehaviour
{
    //メインカメラに付いているタグ名
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    //カメラに表示されているか
    private bool _isRendered = false;

    public static bool isObject1Render = false;
    public static bool isHuman1Render = false;
    public static bool isHuman2Render = false;
    public static bool isHuman3Render = false;
    public static bool isHuman4Render = false;
    public static bool isHuman5Render = false;


    private void Update()
    {

       
    }

    //カメラに映ってる間に呼ばれる
    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけ_isRenderedを有効に
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            if (this.gameObject.tag == "Object1")
            {
                isObject1Render = true;
            }
            if (this.gameObject.tag == "human1")
            {
                isHuman1Render = true;
            }
            if (this.gameObject.tag == "human2")
            {
                isHuman2Render = true;
            }
            /*if (this.gameObject.tag == "human3")
            {
                isHuman3Render = true;
            }
            if (this.gameObject.tag == "human4")
            {
                isHuman4Render = true;
            }
            if (this.gameObject.tag == "human5")
            {
                isHuman5Render = true;
            }*/

        }
    }

}