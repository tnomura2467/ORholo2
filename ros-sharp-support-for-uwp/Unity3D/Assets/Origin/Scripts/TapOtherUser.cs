using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapOtherUser : MonoBehaviour
{
    public int tapnumber = 0;
    GameObject UserText;
    private string usertextname;

    void Start()
    {


        
    }

    public void TapUserButton()
    {
        //Debug.Log(this.gameObject.GetComponent<ButtonNo>().buttonNo);


        GameObject[] texts = GameObject.FindGameObjectsWithTag("usertext");
        foreach (GameObject text in texts)
        {
            text.GetComponent<TextMesh>().color= new Color32(255, 255, 255, 255);

        }

        UserText = GameObject.Find("User3DText" + gameObject.GetComponent<ButtonNo>().buttonNo);
        UserText.GetComponent<TextMesh>().color = new Color32(255, 0, 0, 255);


        tapnumber = this.gameObject.GetComponent<ButtonNo>().buttonNo;
        MyInfoPub.TUCtap = tapnumber;
        MyInfoPub.ButtonNo = tapnumber;
    }
}
