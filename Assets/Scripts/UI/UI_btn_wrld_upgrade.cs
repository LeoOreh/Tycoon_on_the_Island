using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_btn_wrld_upgrade : MonoBehaviour
{
    GameObject upgrade;

    private void Start()
    {
        upgrade = transform.Find("Upgrade").gameObject;
    }


    public void Open(string typ)
    {
        foreach (Transform child in upgrade.transform)
        {
            child.gameObject.SetActive(false);
        }

        upgrade.transform.Find(typ).gameObject.SetActive(true);
        upgrade.SetActive(true);
    }

    public void Close()
    {
        upgrade.SetActive(false);
    }
}
