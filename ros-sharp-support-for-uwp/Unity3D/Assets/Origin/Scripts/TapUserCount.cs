using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ユーザカウントボタンのタップ管理*/

public class TapUserCount : MonoBehaviour
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
