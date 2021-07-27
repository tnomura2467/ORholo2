using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 FirstPos;
    private Vector3 transRota;

    void Start()
    {
        FirstPos = this.gameObject.transform.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        //transRota.z = this.gameObject.transform.rotation.z;
        transRota.x = 0.0f;
        transRota.y = 0.0f;
        this.gameObject.transform.localPosition = FirstPos;
        this.transform.rotation= Quaternion.Euler(transRota);

    }
}
