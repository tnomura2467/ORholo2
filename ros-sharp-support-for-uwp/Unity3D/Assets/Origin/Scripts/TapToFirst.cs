using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToFirst : MonoBehaviour
{
    public bool tap = false;
    //public GameObject rosconnect;

    void Start()
    {

    }

    public void UserCounter()
    {
        //rosconnect.GetComponent<UserNoSubscriber>().enabled = true;
        tap = true;
    }
}
