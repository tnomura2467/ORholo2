using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDia : MonoBehaviour
{
    public GameObject MRP;
    void Start()
    {
        MRP = GameObject.Find("Diagnostics");
    }

    // Update is called once per frame
    void Update()
    {
        MRP.SetActive(false);
    }
}