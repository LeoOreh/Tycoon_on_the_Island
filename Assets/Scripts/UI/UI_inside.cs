using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_inside : MonoBehaviour
{
    GameObject ui_inside;



    void Start()
    {
        ui_inside = transform.Find("Inside").gameObject;
        ui_inside.SetActive(false);
    }


    public void Open_inside(string typ)
    {
        ui_inside.SetActive(true);
        foreach (Transform tr in ui_inside.transform)
        {
            tr.gameObject.SetActive(false);
            if(tr.name == typ || tr.name == "close") { tr.gameObject.SetActive(true); }
        }
    }
    public void Close_inside()
    {
        ui_inside.SetActive(false);
    }
}
