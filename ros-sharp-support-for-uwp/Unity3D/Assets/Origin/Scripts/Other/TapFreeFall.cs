using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*タップしたオブジェクトを落下させる*/

public class TapFreeFall : MonoBehaviour
{
    private Rigidbody rigidBody;
    public bool tap = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void OnClickFreeFall() //タップされたら
    {
        
        if (rigidBody != null)
        {
            rigidBody.useGravity = true;  //重力付加
            tap = true;
        }
    }
}

