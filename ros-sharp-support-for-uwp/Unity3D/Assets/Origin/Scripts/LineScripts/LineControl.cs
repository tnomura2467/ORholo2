using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControl : MonoBehaviour
{
    public GameObject newLine;
    LineRenderer lRend;

    public GameObject bottle;
    public GameObject Head1;

    [SerializeField] private Gradient _gradient;
    private void Awake()
    {
        
    }

    void Start()
    {
        newLine = new GameObject("Line1");
        lRend = newLine.AddComponent<LineRenderer>();

        lRend.numCapVertices = 10;
        lRend.numCornerVertices = 10;
        lRend.startWidth = 0.02f;
        lRend.endWidth = 0.02f;

        lRend.material = new Material(Shader.Find("Sprites/Default"));
        lRend.colorGradient = _gradient;
    }

    // Update is called once per frame
    void Update()
    {
        lRend.SetPosition(0, Head1.gameObject.transform.position);
        lRend.SetPosition(1, bottle.gameObject.transform.position);
    }

}
