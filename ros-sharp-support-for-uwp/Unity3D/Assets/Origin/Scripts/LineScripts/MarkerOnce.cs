using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerOnce : MonoBehaviour
{

    private bool i = false;
    DefaultTrackableEventHandler DTE;
    public bool cameraoff = false;
    public GameObject imagetarget;
    void Start()
    {
        DTE = imagetarget.GetComponent<DefaultTrackableEventHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DTE.once == true)
        {
            this.gameObject.transform.parent = null;
            //i = true;
        }
    }
    
}
