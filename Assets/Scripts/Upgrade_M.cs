using System.Collections.Generic;
using UnityEngine;

public class Upgrade_M : MonoBehaviour
{
    public static void LVL_up(string build, string typ)
    {
        int price = Numbers_M.Get_Price_Upgrade(build, typ);
        Debug.Log(price);

        if (GL_UI.coins_count >= price)
        {
            GL_UI.coins_count -= price;

            foreach (GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls up in Land.state_buildings[build].upgrades)
            {
                if(up.name == typ)
                {
                    up.lvl++;

                    foreach (KeyValuePair<string, UI_upgrade.Upgrde_cls> ui_up in UI_upgrade.upgrades)
                    {
                        if(ui_up.Key == build + typ)
                        {
                            ui_up.Value.TXT.text = Numbers_M.Get_Price_Upgrade(build, typ).ToString();
                        }
                    }

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
