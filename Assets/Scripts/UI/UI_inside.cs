using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_inside : MonoBehaviour
{
    public static GameObject ui_inside_GO;
    public static GameObject ui_inside_port_err;
    public static GameObject ui_inside_factory_err;



    void Start()
    {
        ui_inside_GO = transform.Find("Inside").gameObject;
        ui_inside_GO.SetActive(false);

        ui_inside_port_err = transform.Find("Inside/port/err").gameObject;
        ui_inside_port_err.SetActive(false);

        ui_inside_factory_err = transform.Find("Inside/factory/err").gameObject;
        ui_inside_factory_err.SetActive(false);
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



    public void Open_port_err()
    {
        ui_inside_port_err.SetActive(true);
    }
    public void Close_port_err()
    {
        ui_inside_port_err.SetActive(false);
    }


    public void Open_factory_err()
    {
        ui_inside_factory_err.SetActive(true);
    }
    public void Close_factory_err()
    {
        ui_inside_factory_err.SetActive(false);
    }
}
