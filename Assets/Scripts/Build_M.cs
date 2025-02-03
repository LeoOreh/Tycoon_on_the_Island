using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_M : MonoBehaviour
{
    public static void Build_lvl_up(string typ)
    {
        Land.state_buildings[typ].lvl++;
        int new_lvl = Land.state_buildings[typ].lvl;


        // актуализация версии здания на сцене
        foreach (var bld in Land.buildings[typ].build_lvl) { bld.Value.SetActive(false); }
        Land.buildings[typ].build_lvl[new_lvl].SetActive(true);


        // актуализация UI здания на сцене
        Land.buildings[typ].ui_first_BUY.SetActive(false);
        Land.buildings[typ].ui_lock.SetActive(false);
        Land.buildings[typ].ui_upgrade.SetActive(false);

        if (new_lvl == 0) { Land.buildings[typ].ui_first_BUY.SetActive(true); } else
        if (new_lvl >  0) { Land.buildings[typ].ui_upgrade.SetActive(true); }


        JSON_M.Save();
    }


    public static void Build_First_BUY(string typ)
    {
        int price = Numbers_M.Get_Price_First_BUY(typ);

        if (GL_UI.coins_count >= price)
        {
            GL_UI.coins_count -= price;

            Build_lvl_up(typ);
        }
        else
        {
            Debug.Log("not enough money");
        }
    }
}
