using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inside_cave : MonoBehaviour
{
    Transform UI_cave;
    TextMeshProUGUI TXT_summ;
    Image icon_limit;

    int limit_count;
    float percent;

    float TS_one_sec;

    GameObject lock_sell_port;
    GameObject lock_warehouse;

    string TYP = "cave";

    void Start()
    {
        UI_cave   = transform.Find("land_1_stones/UI/Inside/cave");
        TXT_summ  = UI_cave.Find("state/summ").GetComponent<TextMeshProUGUI>();
        icon_limit = UI_cave.Find("state/limit/icon_limit").GetComponent<Image>();

        lock_sell_port = UI_cave.Find("state/sell_port/lock").gameObject;
        lock_warehouse = UI_cave.Find("state/warehouse/lock").gameObject;
    }


    void Update()
    {      
        if (Time.time > TS_one_sec + 1)
        {
            TS_one_sec = Time.time;

            limit_count = Numbers_M.summ_upgrade(TYP) * 50;

            if (Land.state_buildings[TYP].all_res < limit_count)
            {
                Land.state_buildings[TYP].all_res += Numbers_M.summ_upgrade(TYP);

                if (Land.state_buildings[TYP].all_res > limit_count) { Land.state_buildings[TYP].all_res = limit_count; }

                TXT_summ.text = Land.state_buildings[TYP].all_res.ToString();
            }
            else
            {
                Land.state_buildings[TYP].all_res = limit_count;
                TXT_summ.text = Land.state_buildings[TYP].all_res.ToString();
            }


            percent = (float)Land.state_buildings[TYP].all_res / (float)limit_count;
            icon_limit.fillAmount = percent;


            if (percent > 0.05f)
            {
                if (Land.state_buildings["port"].lvl > 0) { lock_sell_port.SetActive(false); }
                if (Land.state_buildings["warehouse"].lvl > 0) { lock_warehouse.SetActive(false); }
            }
            else
            {
                lock_sell_port.SetActive(true);
                lock_warehouse.SetActive(true);
            }

            JSON_M.Save();
        }    
    }


    public void cave_minus(string typ)
    {
        if (typ == "port"      && lock_sell_port.activeInHierarchy == true) { return; }
        if (typ == "warehouse" && lock_warehouse.activeInHierarchy == true) { return; }

        int res = Land.state_buildings[TYP].all_res;
        Land.state_buildings[TYP].all_res = 0;

        Debug.Log(res);
        Warehouse_ADD.ADD_res(res);

        TS_one_sec = 0;
    }
}
