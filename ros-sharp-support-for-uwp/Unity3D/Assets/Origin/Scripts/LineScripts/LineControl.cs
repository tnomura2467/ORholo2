﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControl : MonoBehaviour
{
    public GameObject newLine;
    public GameObject newLine2;
    LineRenderer lRend;
    LineRenderer lRend2;
    public GameObject Object1;
    public GameObject Human1;
    public GameObject Human2;
    public GameObject Icon1;
    public GameObject IconBlink1;
    public GameObject Icon2;
    public GameObject IconBlink2;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Gradient _gradient2;
    public GameObject Xmark;
    public GameObject MRcamera;


    private float alpha_Sin=0;
    public GameObject Frame1;
    public GameObject Frame2;


    private Vector2 confirmation2;
    private Vector3 confirmation3;
    //public GameObject con3;
    Vector3 hitPos;
    Vector3 tmpPos;
    float lazerDistance = 10f;
    float lazerStartPointDistance = 0.15f;
    float lineWidth = 0.01f;

    /*public GameObject newLine3;
    public GameObject newLine4;
    public GameObject newLine5;
    LineRenderer lRend3;
    LineRenderer lRend4;
    LineRenderer lRend5;
    public GameObject Human3;
    public GameObject Human4;
    public GameObject Human5;
    public GameObject Icon3;
    public GameObject Icon4;
    public GameObject Icon5;
    [SerializeField] private Gradient _gradient3;
    [SerializeField] private Gradient _gradient4;*/
    [SerializeField] private Gradient _gradient5;

    
    void Start()
    {
        newLine = new GameObject("Line1");
        newLine2 = new GameObject("Line2");
        lRend = newLine.AddComponent<LineRenderer>();
        lRend2 = newLine2.AddComponent<LineRenderer>();

        lRend.numCapVertices = 10;
        lRend.numCornerVertices = 10;
        lRend.startWidth = 0.01f;
        lRend.endWidth = 0.01f;
        lRend.material = new Material(Shader.Find("Sprites/Default"));
        lRend.colorGradient = _gradient;

        lRend2.numCapVertices = 10;
        lRend2.numCornerVertices = 10;
        lRend2.startWidth = 0.01f;
        lRend2.endWidth = 0.01f;
        lRend2.material = new Material(Shader.Find("Sprites/Default"));
        lRend2.colorGradient = _gradient2;

        //Camera.main.orthographic = true;
        

        /*
        newLine3 = new GameObject("Line3");
        newLine4 = new GameObject("Line4");
        newLine5 = new GameObject("Line5");
        lRend3 = newLine3.AddComponent<LineRenderer>();
        lRend4 = newLine4.AddComponent<LineRenderer>();
        lRend5 = newLine5.AddComponent<LineRenderer>();
        
        lRend3.numCapVertices = 10;
        lRend3.numCornerVertices = 10;
        lRend3.startWidth = 0.01f;
        lRend3.endWidth = 0.01f;
        lRend3.material = new Material(Shader.Find("Sprites/Default"));
        lRend3.colorGradient = _gradient3;

        lRend4.numCapVertices = 10;
        lRend4.numCornerVertices = 10;
        lRend4.startWidth = 0.01f;
        lRend4.endWidth = 0.01f;
        lRend4.material = new Material(Shader.Find("Sprites/Default"));
        lRend4.colorGradient = _gradient4;

        lRend5.numCapVertices = 10;
        lRend5.numCornerVertices = 10;
        lRend5.startWidth = 0.01f;
        lRend5.endWidth = 0.01f;
        lRend5.material = new Material(Shader.Find("Sprites/Default"));
        lRend5.colorGradient = _gradient5;*/


    }

    // Update is called once per frame
    void Update()
    {
        Xmark.SetActive(false);
        IconBlink1.SetActive(false);
        alpha_Sin = Mathf.Sin(Time.time*4) / 2 + 0.5f;
        //confirmation.transform.position = Change2DPos(Human1);

       
        

        if (VisibleHuman.isObject1Render == true && VisibleHuman.isHuman1Render == true)  //人to物
        {
            lRend.positionCount = 2;
            lRend.SetPosition(0, Human1.gameObject.transform.position);
            lRend.SetPosition(1, Object1.gameObject.transform.position);
            lRend.startWidth = 0.01f;
            lRend.endWidth = 0.01f;
            if (alpha_Sin >= 0.5f)
            {
                IconBlink1.SetActive(true);
            }
            else
            {
                IconBlink1.SetActive(false);
            }

        }
        else if (VisibleHuman.isObject1Render == true)  //アイコンto物
        {
            lRend.positionCount = 3;
            lRend.SetPosition(0, Icon1.gameObject.transform.position);
            lRend.SetPosition(1, Frame1.gameObject.transform.position);
            lRend.SetPosition(2, Object1.gameObject.transform.position);
            lRend.startWidth = 0.005f;
            lRend.endWidth = 0.005f;

        }
        else  //アイコンto人
        {
            lRend.positionCount = 3;
            lRend.SetPosition(0, Icon1.gameObject.transform.position);
            lRend.SetPosition(1, Frame1.gameObject.transform.position);
            lRend.SetPosition(2, Human1.gameObject.transform.position);
            lRend.startWidth = 0.005f;
            lRend.endWidth = 0.005f;
        }



        


        
        if (VisibleHuman.isObject1Render == true && VisibleHuman.isHuman2Render == true)
        {
            lRend2.positionCount = 2;
            lRend2.SetPosition(0, Human2.gameObject.transform.position);
            lRend2.SetPosition(1, Object1.gameObject.transform.position);
            lRend2.startWidth = 0.01f;
            lRend2.endWidth = 0.01f;
            if (alpha_Sin >= 0.5f)
            {
                IconBlink2.SetActive(true);
            }
            else
            {
                IconBlink2.SetActive(false);
            }
        }
        else if (VisibleHuman.isObject1Render == true)
        {
            lRend2.positionCount = 3;
            lRend2.SetPosition(0, Icon2.gameObject.transform.position);
            lRend2.SetPosition(1, Frame2.gameObject.transform.position);
            lRend2.SetPosition(2, Object1.gameObject.transform.position);
            lRend2.startWidth = 0.005f;
            lRend2.endWidth = 0.005f;
        }
        else
        {
            lRend2.positionCount = 3;
            lRend2.SetPosition(0, Icon2.gameObject.transform.position);
            lRend2.SetPosition(1, Frame2.gameObject.transform.position);
            lRend2.SetPosition(2, Human2.gameObject.transform.position);
            lRend2.startWidth = 0.005f;
            lRend2.endWidth = 0.005f;
        }



        if (VisibleHuman.isObject1Render == true)
        {
            Vector3 center1 = (Object1.transform.position + Icon1.transform.position) * 0.5f;
            Vector3 center2 = (Object1.transform.position + Icon2.transform.position) * 0.5f;


            Xmark.transform.position = center1;
            Xmark.transform.rotation = MRcamera.transform.rotation;
            
        }














        
       /*
        lRend3.SetPosition(0, Icon3.gameObject.transform.position);
        if (VisibleHuman.isObject1Render == true && VisibleHuman.isHuman3Render == true)
        {
            lRend3.SetPosition(0, Human3.gameObject.transform.position);
            lRend3.SetPosition(1, Object1.gameObject.transform.position);
        }
        else if (VisibleHuman.isObject1Render == true)
        {
            lRend3.SetPosition(1, Object1.gameObject.transform.position);
        }
        else
        {
            lRend3.SetPosition(1, Human3.gameObject.transform.position);
        }


        lRend4.SetPosition(0, Icon4.gameObject.transform.position);
        if (VisibleHuman.isObject1Render == true && VisibleHuman.isHuman4Render == true)
        {
            lRend4.SetPosition(0, Human4.gameObject.transform.position);
            lRend4.SetPosition(1, Object1.gameObject.transform.position);
        }
        else if (VisibleHuman.isObject1Render == true)
        {
            lRend4.SetPosition(1, Object1.gameObject.transform.position);
        }
        else
        {
            lRend4.SetPosition(1, Human4.gameObject.transform.position);
        }



        lRend5.SetPosition(0, Icon5.gameObject.transform.position);
        if (VisibleHuman.isObject1Render == true && VisibleHuman.isHuman5Render == true)
        {
            lRend5.SetPosition(0, Human5.gameObject.transform.position);
            lRend5.SetPosition(1, Object1.gameObject.transform.position);
        }
        else if (VisibleHuman.isObject1Render == true)
        {
            lRend5.SetPosition(1, Object1.gameObject.transform.position);
        }
        else
        {
            lRend5.SetPosition(1, Human5.gameObject.transform.position);
        }*/





        VisibleHuman.isObject1Render = false;
        VisibleHuman.isHuman1Render = false;
        VisibleHuman.isHuman2Render = false;
        /*VisibleHuman.isHuman3Render = false;
        VisibleHuman.isHuman4Render = false;
        VisibleHuman.isHuman5Render = false;*/

    }

    /*Vector2 Change2DPos(GameObject obj3D)
    {
        //Camera.mainになっていますが設定したいCameraがあれば変更してくさい。
        Vector2 pos2D = Camera.main.ViewportToWorldPoint(Camera.main.WorldToViewportPoint(obj3D.transform.position));
        return pos2D;
    }
    void OnRay()
    {
        Vector3 direction = Icon2.transform.forward * lazerDistance;
        Vector3 rayStartPosition = Icon2.transform.forward * lazerStartPointDistance;
        Vector3 pos = Icon2.transform.position;
        RaycastHit hit;
        Ray ray = new Ray(Icon2.transform.position, Human2.transform.position);

        lRend2.SetPosition(0,Icon2.transform.position);

        if (Physics.Raycast(ray, out hit, lazerDistance))
        {
            hitPos = hit.point;
            lRend2.SetPosition(1, hitPos);
            lRend2.SetPosition(2, Human2.transform.position);
            Debug.Log("elseno");
        }
        else
        {
            lRend2.colorGradient = _gradient2;
            //lRend2.SetPosition(1,hitPos);
            lRend2.SetPosition(1, Human2.transform.position);
            Debug.Log("枠に当たってない．あかん");
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.1f);

    }*/

}