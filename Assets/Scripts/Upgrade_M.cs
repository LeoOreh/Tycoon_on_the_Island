using System.Collections.Generic;
using UnityEngine;

public class Upgrade_M : MonoBehaviour
{
    public static void LVL_up(string build, string typ)
    {
        int price = Numbers_M.Get_Price_Upgrade(build, typ);

        if (GL_UI.coins_count >= price)
        {
            GL_UI.coins_count -= price;

            foreach (GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls up in Land.state_buildings[build].upgrades)
            {
                if(up.name == typ)
                {
                    up.lvl++; 

                    UI_upgrade.bldg[build].upgrades[typ].up_TXT_price.text = Numbers_M.Get_Price_Upgrade(build, typ).ToString();
                    UI_upgrade.bldg[build].TXT_summ.text = Numbers_M.summ_upgrade(build) + "/sec";


                    JSON_M.Save();

                    Looking.I();
                }
            }
        }
        else
        {
            Debug.Log("not enough money");
        }
    }
}
