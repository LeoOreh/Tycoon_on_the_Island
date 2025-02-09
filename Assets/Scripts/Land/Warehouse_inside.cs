using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Warehouse_inside : MonoBehaviour
{
    static Transform content_TR;
    Dictionary<string, RES_TYP> res = new Dictionary<string, RES_TYP>();

    void Start()
    {
        content_TR = transform.Find("Scroll/Viewport/Content");

        foreach (string _name in Land.state_land.res_names)
        {
            res[_name] = new RES_TYP(_name);
        }
        OnEnable();
    }

    void OnEnable()
    {
        foreach (KeyValuePair<string, RES_TYP> r in res)
        {
            r.Value.TXT_count.text = Land.state_res[r.Key].count.ToString();
        }
    }

    public class RES_TYP
    {
        public Transform TR;
        public TextMeshProUGUI TXT_count;

        public RES_TYP(string _name)
        {
            TR = content_TR.Find(_name);
            TXT_count = TR.Find("TXT").GetComponent<TextMeshProUGUI>();
        }
    }
}
