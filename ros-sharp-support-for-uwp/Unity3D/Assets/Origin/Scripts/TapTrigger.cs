using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTrigger : MonoBehaviour
{
    public bool tap = false;
    //private Rigidbody rigidBody;
    private int cnt;
    void Start()
    {
        //rigidBody = GetComponent<Rigidbody>();
        cnt = 0;
    }

    public void OnClickFreeFall()
    {
        /*if(rigidBody != null)
        {
            rigidBody.useGravity = true;
           
        }*/
        tap = true;
        /*if (cnt % 2 == 0)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if(cnt%2 == 1)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }*/
        cnt++;
    }
}

