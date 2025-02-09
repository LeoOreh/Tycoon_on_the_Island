using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_bld_btn : MonoBehaviour
{
    Transform lands;
    Transform bldg;
    GameObject upgrade;

    public static string active_build_UI;

    public void I()
    {
        lands = transform.parent.parent;
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
                else
                if (btn.name == "inside")
                {
                    btn.onClick.AddListener(() => Inside(btn.transform.parent.parent.name));
                }
            }
        }
    }


    //---------------------------------------------------------------------------------------------------
    // UI кнопок зданий
    void Open_upgrade(string typ)
    {
        Debug.Log("Open_upgrade() > " + typ);

        active_build_UI = typ;

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

        if (GL.state.dialog.buy_energy == 0) { GL.state.dialog.buy_energy = 1; GL.dlg.add_txt("buy_energy"); }
    }
    //---------------------------------------------------------------------------------------------------



    //---------------------------------------------------------------------------------------------------
    void First_buy(string typ)
    {
        Debug.Log("First_buy() > " + typ);

        Transform pos = Land.buildings[typ].POS;
        Transform fx = Instantiate(Resources.Load<GameObject>(Resources_PTH.FX_build), pos.position, Quaternion.identity).transform;
        Destroy(fx.gameObject, 5);
        
        Land.buildings[typ].ui_first_BUY.SetActive(false);

        StartCoroutine(IE_First_buy(typ));
    }

    IEnumerator IE_First_buy(string typ)
    {
        yield return new WaitForSeconds(2);

        Debug.Log("Invoke_First_buy() > " + typ);

        Build_M.First_BUY(typ);

        if (typ == "energy") { Land.buildings[typ].envr_add["electro"].SetActive(true); }
        if (typ == "cave") { Land.buildings[typ].envr_add["smoke"].SetActive(true); }
    }
    //---------------------------------------------------------------------------------------------------



    //---------------------------------------------------------------------------------------------------
    void Lock(string typ)
    {
        Debug.Log("Lock() > " + typ);
    }
    //---------------------------------------------------------------------------------------------------



    //---------------------------------------------------------------------------------------------------
    void Inside(string typ)
    {
         GetComponent<UI_inside>().Open_inside(typ);
    }
    //---------------------------------------------------------------------------------------------------
}
