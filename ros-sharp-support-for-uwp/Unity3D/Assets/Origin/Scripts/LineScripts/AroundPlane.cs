using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundPlane : MonoBehaviour
{
    public GameObject RedPlane;
    public GameObject NamePlane;
    public GameObject Icon1Plane;
    public GameObject Icon2Plane;

    private float transz;
    private float sizez;
    // Start is called before the first frame update
    void Start()
    {
        GameObject around_obj = Instantiate(RedPlane, new Vector3(0,0, 0), Quaternion.identity);
        around_obj.transform.SetParent(this.gameObject.transform, false);
        around_obj.transform.localPosition = new Vector3(0, -0.1f, 0);
        around_obj.transform.localScale = new Vector3(1.1f,1,1.1f);


        sizez = 0.015f / this.gameObject.transform.lossyScale.z;
        GameObject name_obj = Instantiate(NamePlane, new Vector3(0, 0, 0), Quaternion.identity);
        /*name_obj.transform.localScale = new Vector3(0.025f, 0.001f, 0.0028f);
        name_obj.transform.SetParent(this.gameObject.transform, true);
        name_obj.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 180.0f);
        name_obj.transform.localPosition = new Vector3(0f, -0.1f, -6.6f);*/
        name_obj.transform.SetParent(this.gameObject.transform, false);
        name_obj.transform.localPosition = new Vector3(0f, -0.1f, -6.85f);
        name_obj.transform.localScale = new Vector3(0.98f, -0.1f, sizez);

        transz=-5.0f-(0.08f / this.gameObject.transform.lossyScale.z);
        

        GameObject around_icon1 = Instantiate(Icon1Plane, new Vector3(0, 0, 0), Quaternion.identity);
        around_icon1.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        around_icon1.transform.SetParent(this.gameObject.transform, true);
        around_icon1.transform.localPosition = new Vector3(2.4f, -0.15f, transz);
        around_icon1.gameObject.name = this.gameObject.name+"_1";

        GameObject around_icon2 = Instantiate(Icon2Plane, new Vector3(0, 0, 0), Quaternion.identity);
        around_icon2.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        around_icon2.transform.SetParent(this.gameObject.transform, true);
        around_icon2.transform.localPosition = new Vector3(-2.4f, -0.15f, transz);
        around_icon2.gameObject.name = this.gameObject.name + "_2";

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.gameObject.transform.lossyScale.x);
    }
}
