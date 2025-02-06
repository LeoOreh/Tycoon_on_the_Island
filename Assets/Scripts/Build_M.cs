using Unity.VisualScripting;
using UnityEngine;

public class Build_M : MonoBehaviour
{
    public static void LVL_up(string typ)
    {
        Debug.Log("LVL_up");

        Land.state_buildings[typ].lvl++;
        int new_lvl = Land.state_buildings[typ].lvl;
        JSON_M.Save();


        // актуализация версии здания на сцене
        foreach (var bld in Land.buildings[typ].build_lvl) { bld.Value.SetActive(false); }
        if (new_lvl > 0) { Land.buildings[typ].build_lvl[1].SetActive(true); }


        // актуализация UI здания на сцене
        Land.buildings[typ].ui_first_BUY.SetActive(false);
        Land.buildings[typ].ui_lock.SetActive(false);
        Land.buildings[typ].ui_upgrade.SetActive(false);

        if (new_lvl == 0) { Land.buildings[typ].ui_first_BUY.SetActive(true); } else
        if (new_lvl >  0) { Land.buildings[typ].ui_upgrade.SetActive(true); }


        int summ = Numbers_M.summ_upgrade(typ);
        if (UI_upgrade.bldg[typ].tr.gameObject.activeInHierarchy)
        {
            //UI_upgrade.bldg[typ].fill.localScale = new Vector3(Numbers_M.upgrade_fill(typ, Numbers_M.summ_upgrade(typ)), 1, 1);
            if (Upgrade_M.activeCoroutine != null) { GL.state.StopCoroutine(Upgrade_M.activeCoroutine); }
            Upgrade_M.activeCoroutine = GL.state.StartCoroutine(Upgrade_M.fill_(typ, summ));
        }
        else
        {
            UI_upgrade.bldg[typ].fill.localScale = new Vector3(Numbers_M.upgrade_fill(typ, Numbers_M.summ_upgrade(typ)), 1, 1);
        }

        UI_upgrade.bldg[typ].TXT_lvl.text = Land.state_buildings[typ].lvl.ToString();
        
        JSON_M.Save();
    }


    public static void First_BUY(string typ)
    {
        int price = Numbers_M.Get_Price_First_BUY(typ);

        if (GL_UI.coins_count >= price)
        {
            GL_UI.coins_count -= price;

            LVL_up(typ);
        }
        else
        {
            Debug.Log("not enough money");
        }
    }
}
