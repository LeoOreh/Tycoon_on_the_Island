using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_btn_upgrade : MonoBehaviour
{
    GameObject upgrade;

    void Start()
    {
        upgrade = transform.Find("Upgrade").gameObject;

        // init UI ������ �������
        Button[] upgrd_btns = upgrade.GetComponentsInChildren<Button>(true);
        foreach (Button btn in upgrd_btns)
        {
            if (btn.name == "Button_Upgrade" && btn.transform.parent.parent.name == "Content")
            {
                btn.onClick.AddListener(() => Upgrade(btn.transform.parent.name));
            }
        }
    }




    //---------------------------------------------------------------------------------------------------
    // UI ������ �������
    void Upgrade(string typ)
    {
        Debug.Log("Upgrade() > " + typ);
    }
    //---------------------------------------------------------------------------------------------------
}
