using UnityEngine;
using UnityEngine.UI;

public class UI_btn_bldg : MonoBehaviour
{
    GameObject upgrade;
    Transform bldg;

    void Start()
    {
        upgrade = transform.Find("Upgrade").gameObject;
        bldg = transform.parent.Find("buildings");

        // init UI кнопок зданий
        Button[] bldg_btns = bldg.GetComponentsInChildren<Button>(true);
        foreach (Button btn in bldg_btns)
        {
            if(btn.transform.parent.name == "UI")
            {
                if (btn.name == "upgrade") 
                {
                    btn.onClick.AddListener(() => Open_upgrade(btn.transform.parent.parent.name));
                } 
                else
                if (btn.name == "first_BUY")
                {
                    btn.onClick.AddListener(() => First_buy(btn.transform.parent.parent.name));
                } 
                else
                if (btn.name == "lock")
                {
                    btn.onClick.AddListener(() => Lock(btn.transform.parent.parent.name));
                }
            }
        }
    }


    //---------------------------------------------------------------------------------------------------
    // UI кнопок зданий
    void Open_upgrade(string typ)
    {
        Debug.Log("Open_upgrade() > " + typ);

        foreach (Transform child in upgrade.transform)
        {
            child.gameObject.SetActive(false);
            if(child.name == "close") { child.gameObject.SetActive(true); }
        }

        upgrade.transform.Find(typ).gameObject.SetActive(true);
        upgrade.SetActive(true);
    }

    public void Close_upgrade()
    {
        upgrade.SetActive(false);
    }
    //---------------------------------------------------------------------------------------------------



    //---------------------------------------------------------------------------------------------------
    void First_buy(string typ)
    {
        Debug.Log("First_buy() > " + typ);
        Build_M.Build_First_BUY(typ);
    }
    //---------------------------------------------------------------------------------------------------



    //---------------------------------------------------------------------------------------------------
    void Lock(string typ)
    {
        Debug.Log("Lock() > " + typ);
    }
    //---------------------------------------------------------------------------------------------------
}
