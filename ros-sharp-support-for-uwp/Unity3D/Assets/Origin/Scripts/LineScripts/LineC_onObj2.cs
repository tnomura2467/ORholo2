using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineC_onObj2 : MonoBehaviour
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
    //public GameObject MRcamera;


    private float alpha_Sin = 0;
    private Vector3 tmpIcon1;
    private Vector3 tmpIcon2;

    private GameObject Object1Icon1;
    private GameObject Object1Icon2;
    private GameObject Object2Icon1;
    private GameObject Object2Icon2;


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
        lRend.positionCount = 3;

        lRend2.numCapVertices = 10;
        lRend2.numCornerVertices = 10;
        lRend2.startWidth = 0.01f;
        lRend2.endWidth = 0.01f;
        lRend2.material = new Material(Shader.Find("Sprites/Default"));
        lRend2.colorGradient = _gradient2;


        tmpIcon1 = new Vector3(0.05f, 0.05f, 1f);
        tmpIcon2 = new Vector3(0.008f, 0.008f, 5f);

        Object1Icon1 = GameObject.Find("Green1_1");
        Object1Icon2 = GameObject.Find("Green1_2");
        Object2Icon1 = GameObject.Find("Blue2_1");
        Object2Icon2 = GameObject.Find("Blue2_2");

        Invoke("Init", 0.1f);
    }

    public void Init()
    {
        Object1Icon1 = GameObject.Find("Green1_1");
        Object1Icon2 = GameObject.Find("Green1_2");
        Object2Icon1 = GameObject.Find("Blue2_1");
        Object2Icon2 = GameObject.Find("Blue2_2");
    }

    // Update is called once per frame
    void Update()
    {
        //Xmark.SetActive(false);
        IconBlink1.SetActive(false);
        IconBlink2.SetActive(false);


        alpha_Sin = Mathf.Sin(Time.time * 4) / 2 + 0.5f;

        if (VisibleHuman.isObject1Render == true && VisibleHuman.isHuman1Render == true)  //人to物
        {
            Icon1.transform.position = Human1.transform.position;
            Icon1.transform.Translate(0, 0, -0.2f);
            Icon1.transform.localScale = tmpIcon1;
            lRend.positionCount = 3;
            lRend.SetPosition(0, Object1Icon1.gameObject.transform.position);
            lRend.SetPosition(1, Icon1.gameObject.transform.position);
            lRend.SetPosition(2, Object2Icon1.gameObject.transform.position);

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
        /*else if (VisibleHuman.isObject1Render == true)  //アイコンto物
        {
            Icon1.transform.localScale = tmpIcon2;
            Icon1.transform.position = IconBlink1.transform.position;
            lRend.positionCount = 3;
            lRend.SetPosition(0, Object1Icon1.gameObject.transform.position);
            lRend.SetPosition(1, Human1.gameObject.transform.position);
            lRend.SetPosition(2, Object2Icon1.gameObject.transform.position);
            lRend.startWidth = 0.005f;
            lRend.endWidth = 0.005f;

        }*/
        else  //アイコンto人
        {
            Icon1.transform.localScale = tmpIcon2;
            lRend.positionCount = 2;
            Icon1.transform.position = IconBlink1.transform.position;
            lRend.SetPosition(0, Icon1.gameObject.transform.position);
            lRend.SetPosition(1, Human1.gameObject.transform.position);
            lRend.startWidth = 0.005f;
            lRend.endWidth = 0.005f;
        }







        if (VisibleHuman.isObject1Render == true && VisibleHuman.isHuman2Render == true)
        {
            Icon2.transform.localScale = tmpIcon1;
            Icon2.transform.position = Human2.transform.position;
            Icon2.transform.Translate(0, 0, -0.2f);
            lRend2.positionCount = 3;
            lRend2.SetPosition(0, Object1Icon2.gameObject.transform.position);
            lRend2.SetPosition(1, Icon2.gameObject.transform.position);
            lRend2.SetPosition(2, Object2Icon2.gameObject.transform.position);
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
        /*else if (VisibleHuman.isObject1Render == true)
        {
            Icon2.transform.localScale = tmpIcon2;
            Icon2.transform.position = IconBlink2.transform.position;
            lRend2.positionCount = 3;
            lRend2.SetPosition(0, Object1Icon2.gameObject.transform.position);
            lRend2.SetPosition(1, Human2.gameObject.transform.position);
            lRend2.SetPosition(2, Object2Icon2.gameObject.transform.position);
            lRend2.startWidth = 0.005f;
            lRend2.endWidth = 0.005f;
        }*/
        else
        {
            Icon2.transform.localScale = tmpIcon2;
            Icon2.transform.position = IconBlink2.transform.position;
            lRend2.positionCount = 2;
            lRend2.SetPosition(0, Icon2.gameObject.transform.position);
            lRend2.SetPosition(1, Human2.gameObject.transform.position);
            lRend2.startWidth = 0.005f;
            lRend2.endWidth = 0.005f;
        }



        if (VisibleHuman.isObject1Render == true)
        {
            Vector3 center1 = (Object1.transform.position + Icon1.transform.position) * 0.5f;
            Vector3 center2 = (Object1.transform.position + Icon2.transform.position) * 0.5f;

            //Xmark.SetActive(true);
            //Xmark.transform.position = center1;
            //Xmark.transform.rotation = MRcamera.transform.rotation;

        }

        VisibleHuman.isObject1Render = false;
        VisibleHuman.isHuman1Render = false;
        VisibleHuman.isHuman2Render = false;


    }


}
