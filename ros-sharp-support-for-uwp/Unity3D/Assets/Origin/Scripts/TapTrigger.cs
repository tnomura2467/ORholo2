using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*タップされた回数のカウント*/

public class TapTrigger : MonoBehaviour
{
    public bool tap = false;
    private int cnt; //カウント数
    void Start()
    {
        cnt = 0;
    }

    public void OnClickFreeFall()
    {
        tap = true;
        cnt++; //カウント数を増やす
    }
}

