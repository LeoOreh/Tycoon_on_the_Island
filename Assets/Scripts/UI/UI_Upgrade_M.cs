using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Upgrade_M : MonoBehaviour
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
                    UI_upgrade_I.bldg[build].upgrades[typ].up_TXT_price.text = price.ToString();

                    int summ = Numbers_M.summ_upgrade(build);
                    UI_upgrade_I.bldg[build].TXT_summ.text = summ.ToString();


                    //UI_upgrade.bldg[build].fill.localScale = new Vector3(fill, 1, 1);
                    //if (activeCoroutine != null) { GL.state.StopCoroutine(activeCoroutine); }
                    //activeCoroutine = GL.state.StartCoroutine(fill_(build, summ));
                    GL.state.StartCoroutine(fx_star_to_fill(UI_upgrade_I.bldg[build].upgrades[typ].up_fx_star, build, summ));

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

    //---
    static IEnumerator fx_star_to_fill(GameObject star, string build, int summ)
    {
        RectTransform tr = Instantiate(star, UI_upgrade_I.upgrade_TR).GetComponent<RectTransform>();
        tr.anchoredPosition = star.GetComponent<RectTransform>().anchoredPosition;

        Vector3 startWorldPosition = star.GetComponent<RectTransform>().position;
        Vector3 endWorldPosition = UI_upgrade_I.bldg[build].TXT_lvl.GetComponent<RectTransform>().position;

        tr.position = startWorldPosition;

        float totalDistance = Vector3.Distance(startWorldPosition, endWorldPosition);
        float distanceTravelled = 0f;

        while (distanceTravelled / totalDistance < 0.97f)
        {
            tr.position = Vector3.Lerp(tr.position, endWorldPosition, 0.02f);

            distanceTravelled = Vector3.Distance(startWorldPosition, tr.position);

            yield return null; 
        }

        tr.position = endWorldPosition;

        if (activeCoroutine != null)
        {
            GL.state.StopCoroutine(activeCoroutine);
        }

        activeCoroutine = GL.state.StartCoroutine(fill_(build, summ));

        Destroy(tr.gameObject);
    }



    public static Coroutine activeCoroutine;
    public static IEnumerator fill_(string build, int summ)
    {
        bool act = true;
        float TS = Time.time;
        float fill = Numbers_M.upgrade_fill(build, summ);
        Transform tr = UI_upgrade_I.bldg[build].fill;

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
