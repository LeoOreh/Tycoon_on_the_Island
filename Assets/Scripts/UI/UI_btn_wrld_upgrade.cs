using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_btn_wrld_upgrade : MonoBehaviour
{
    GameObject upgrade;
    Transform upgrade_TR;
    GameObject energy;

    private void Start()
    {
        upgrade = transform.Find("Upgrade").gameObject;
        upgrade_TR = upgrade.transform;
        energy = upgrade.transform.Find("energy").gameObject;
    }


    public void Open(string typ)
    {
        foreach (Transform child in upgrade_TR)
        {
            child.gameObject.SetActive(false);
        }

        upgrade_TR.Find(typ).gameObject.SetActive(true);
        upgrade.SetActive(true);
    }

    public void Close()
    {
        upgrade.SetActive(false);
    }
}
