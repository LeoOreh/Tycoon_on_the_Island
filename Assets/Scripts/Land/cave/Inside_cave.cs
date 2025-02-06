using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inside_cave : MonoBehaviour
{
    Transform UI_cave;
    TextMeshProUGUI TXT_summ;

    float TS_save;
    float TS_res;
    int new_count;

    string TYP = "cave";

    void Start()
    {
        UI_cave = transform.Find("land_1_stones/UI/Inside/cave");
        TXT_summ = UI_cave.Find("state/summ").GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (Time.time > TS_res + 1) 
        {
            TS_res = Time.time;
            Land.state_buildings[TYP].all_res += Numbers_M.summ_upgrade(TYP);
            TXT_summ.text = Land.state_buildings[TYP].all_res.ToString();
        }


        //TXT_summ.text = new_count


        if (Time.time > TS_save + 5)
        {
            TS_save = Time.time;
            JSON_M.Save();
        }
    }
}
