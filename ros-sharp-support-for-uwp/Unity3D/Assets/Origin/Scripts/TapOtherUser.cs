using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*タップしたボタンの色を変更*/

public class TapOtherUser : MonoBehaviour
{
    public int tapnumber = 0;
    GameObject UserText; //ユーザボタンの文字
    private string usertextname; 

    void Start()
    {
    }

    public void TapUserButton()
    {
        GameObject[] texts = GameObject.FindGameObjectsWithTag("usertext");
        foreach (GameObject text in texts)
        {
            text.GetComponent<TextMesh>().color= new Color32(255, 255, 255, 255);  //テキストの色を白に
        }

        UserText = GameObject.Find("User3DText" + gameObject.GetComponent<ButtonNo>().buttonNo);
        UserText.GetComponent<TextMesh>().color = new Color32(255, 0, 0, 255); //テキストの色を赤に

        tapnumber = this.gameObject.GetComponent<ButtonNo>().buttonNo;  //ボタンの番号を格納
        MyInfoPub.TUCtap = tapnumber;
        MyInfoPub.ButtonNo = tapnumber;
    }
}
