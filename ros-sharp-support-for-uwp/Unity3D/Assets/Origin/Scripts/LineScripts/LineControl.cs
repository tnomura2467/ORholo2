using System.Collections;
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
    public GameObject Icon;
    public GameObject Icon2;

    [SerializeField] private Gradient _gradient;
    [SerializeField] private Gradient _gradient2;
    private void Awake()
    {
        
    }

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
        lRend.colorGradient = _gradient2;

        lRend2.numCapVertices = 10;
        lRend2.numCornerVertices = 10;
        lRend2.startWidth = 0.01f;
        lRend2.endWidth = 0.01f;
        lRend2.material = new Material(Shader.Find("Sprites/Default"));
        lRend2.colorGradient = _gradient;



    }

    // Update is called once per frame
    void Update()
    {
        lRend.SetPosition(0, Icon.gameObject.transform.position);

        if (VisibleHuman.isObject1Render == true && VisibleHuman.isHuman1Render == true)
        {
            lRend.SetPosition(0, Human1.gameObject.transform.position);
            lRend.SetPosition(1, Object1.gameObject.transform.position);
            
        }else if (VisibleHuman.isObject1Render == true)
        {
            lRend.SetPosition(1, Object1.gameObject.transform.position);

        }else
        {
            lRend.SetPosition(1, Human1.gameObject.transform.position);
        }



        lRend2.SetPosition(0, Icon2.gameObject.transform.position);
        if (VisibleHuman.isObject1Render == true && VisibleHuman.isHuman2Render == true)
        {
            lRend2.SetPosition(0, Human2.gameObject.transform.position);
            lRend2.SetPosition(1, Object1.gameObject.transform.position);
        }
        else if (VisibleHuman.isObject1Render == true)
        {
            lRend2.SetPosition(1, Object1.gameObject.transform.position);
        }
        else
        {
            lRend2.SetPosition(1, Human2.gameObject.transform.position);
        }


        VisibleHuman.isObject1Render = false;
        VisibleHuman.isHuman1Render = false;
        VisibleHuman.isHuman2Render = false;
    }



}
