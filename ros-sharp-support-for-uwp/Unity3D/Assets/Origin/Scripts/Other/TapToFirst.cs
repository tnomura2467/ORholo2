using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*初めのタップを管理*/

public class TapToFirst : MonoBehaviour
{
    public bool tap = false;
    void Start()
    {
    }

    public void UserCounter()
    {
        tap = true;
    }
}
