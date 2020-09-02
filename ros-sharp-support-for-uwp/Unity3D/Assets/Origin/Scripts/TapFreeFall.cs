using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapFreeFall : MonoBehaviour
{
    private Rigidbody rigidBody;
    public bool tap = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void OnClickFreeFall()
    {
        
        if (rigidBody != null)
        {
            rigidBody.useGravity = true;
            tap = true;
        }
    }
}

