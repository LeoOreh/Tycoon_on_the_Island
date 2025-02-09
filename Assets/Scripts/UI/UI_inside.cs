using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_inside : MonoBehaviour
{
    public static GameObject ui_inside_GO;



    void Start()
    {
        ui_inside_GO = transform.Find("Inside").gameObject;
        ui_inside_GO.SetActive(false);
    }


    public void Open_inside(string typ)
    {
        ui_inside_GO.SetActive(true);
        foreach (Transform tr in ui_inside_GO.transform)
        {
            tr.gameObject.SetActive(false);
            if(tr.name == typ || tr.name == "close") { tr.gameObject.SetActive(true); }
        }
    }
    public void Close_inside()
    {
        ui_inside_GO.SetActive(false);
    }
}
