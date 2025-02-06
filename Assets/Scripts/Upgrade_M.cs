using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

                    price = Numbers_M.Get_Price_Upgrade(build, typ);
                    UI_upgrade.bldg[build].upgrades[typ].up_TXT_price.text = price.ToString();

                    int summ = Numbers_M.summ_upgrade(build);
                    UI_upgrade.bldg[build].TXT_summ.text = summ.ToString();

                    if (activeCoroutine != null) { GL.state.StopCoroutine(activeCoroutine); }

                    //UI_upgrade.bldg[build].fill.localScale = new Vector3(fill, 1, 1);
                    activeCoroutine = GL.state.StartCoroutine(fill_(build, summ));

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



    public static Coroutine activeCoroutine;
    static IEnumerator fill_(string build, int summ)
    {
        bool act = true;
        float TS = Time.time;
        float fill = Numbers_M.upgrade_fill(build, summ);
        Transform tr = UI_upgrade.bldg[build].fill;

        Debug.Log(fill);

        while (act)
        {
            //if (fill < tr.localScale.x) { fill += 1; }

            tr.localScale = Vector3.Lerp(tr.localScale, new Vector3(fill, 1, 1), 0.05f);

            if(tr.localScale.x >= 1) 
            { 
                fill -= 1; 
                tr.localScale = new Vector3(0, 1, 1);
                Build_M.LVL_up(build);
            }

            if (Time.time > TS + 2) { act = false; }

            yield return new WaitForFixedUpdate();
        }
    }

}
