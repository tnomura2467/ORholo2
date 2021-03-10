using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ハンドドラッグでバーをx方向z方向に動かないよう*/
public class TranceCube : MonoBehaviour
{
    public Vector3 nowpos; //元の座標
    public Vector3 nowlot; //元の角度
    private float nowy; //元のy座標
    
    void Start()
    {
        nowpos = new Vector3(0, 0, 0);
        nowlot = new Vector3(0, 0, -90);
    }

    void Update()
    {
        nowpos = this.transform.localPosition; //元の座標格納
        nowy = nowpos.y;
        if (nowpos.y < -1) //y座標を-1以下にしない
        {
            nowy = -1.0f;
        }
        if (nowpos.y > 1) //y座標を1以上にしない
        {
            nowy = 1.0f;
        }

        this.transform.localPosition = new Vector3(0f, nowy, 0f); //位置更新
        this.transform.localEulerAngles = nowlot;
    }
}
