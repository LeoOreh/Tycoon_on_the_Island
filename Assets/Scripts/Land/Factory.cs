using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public Dictionary<string, int> res = new Dictionary<string, int>();
    int limit_res = 3;
    public string out_product;

    void Start()
    {
        for (int i = 1; i <= limit_res; i++)
        {
            if(transform.Find(i.ToString()) != null) 
            {
                int count = Int32.Parse(transform.Find(i.ToString() + "/TXT_count").GetComponent<TextMeshProUGUI>().text);

                string name_res = "";
                foreach (Transform tr in transform.Find(i.ToString() + "/icon_res"))
                {
                    if (tr.gameObject.activeInHierarchy) { name_res = tr.name; }
                }

                res[name_res] = count;
                Debug.Log(name_res + " " + count);
            }
        }

        foreach (Transform tr in transform.Find("icon_out"))
        {
            if (tr.gameObject.activeInHierarchy) { out_product = tr.name; break; }
        }
    }

    public void DO()
    {
        Debug.Log(out_product);

        bool DO = true;

        foreach (KeyValuePair<string, int> r in res)
        {
            if( Land.state_res[r.Key].count < r.Value) { DO = false; }
        }

        if (DO == false) { return; }

        Land.state_res[out_product].count++;
        Land.state_res[out_product].count_all++;

        foreach (KeyValuePair<string, int> r in res)
        {
            Land.state_res[r.Key].count -= r.Value;
        }
    }
}
