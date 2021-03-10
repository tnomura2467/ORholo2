using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*バーが動かされたことを知る。bool変数をtrueにする*/
public class MoveTimeBar : MonoBehaviour
{

    private float barY;
    private float newY;
    public bool movebar;
    void Start()
    {
        barY = this.transform.localPosition.y;
        movebar = false;
    }

    void Update()
    {
        newY = this.transform.localPosition.y;
        if (newY != barY && movebar == false)
        {
            movebar = true;
        }
        barY = this.transform.localPosition.y;
    }
}
