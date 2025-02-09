using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Port_orders_I : MonoBehaviour
{
    static protected Transform content_TR;

    // здесь добавлять типы сделок (префабы)
    public static Dictionary<string, int> name_types_orders = new Dictionary<string, int>();
    static protected Dictionary<string, RES_TYP_CLS> default_order_types = new Dictionary<string, RES_TYP_CLS>();

    public static Dictionary<string, RES_TYP_CLS> orders = new Dictionary<string, RES_TYP_CLS>();

    static protected int limit_res_in_order = 3;

    public static void I()
    {
        Debug.Log("Port_orders_I.I");
        content_TR = Land.land_UI.Find("Inside/port/Scroll/Viewport/Content");

        name_types_orders.Add("_default_1", 1);
        name_types_orders.Add("_default_2", 2);
        name_types_orders.Add("_default_3", 3);
        foreach (KeyValuePair<string, int> type in name_types_orders)
        {
            default_order_types[type.Key] = new RES_TYP_CLS(type.Key);
        }

        foreach (KeyValuePair<string,RES_TYP_CLS> order in default_order_types) { order.Value.order_TR.gameObject.SetActive(false); }


        Port_orders_ADD.ADD_from_jsn();
    }

    public class RES_TYP_CLS
    {
        public Transform order_TR;
        public Dictionary<int, RES_TYP_BUY> resources = new Dictionary<int, RES_TYP_BUY>();
        public TextMeshProUGUI TXT_price;


        public RES_TYP_CLS(string _name)
        {
            order_TR = content_TR.Find(_name);
            Debug.Log(order_TR.name);

            // до трех условий в сделке
            for (int i = 1; i <= limit_res_in_order; i++)
            {
                if (order_TR.Find(i.ToString()) != null) { resources[i] = new RES_TYP_BUY(order_TR.Find(i.ToString()));}
            }

            TXT_price = order_TR.Find("buy/TXT_price").GetComponent<TextMeshProUGUI>();
        }
    }

    public class RES_TYP_BUY
    {
        public string res_name;
        public Transform res_TR;
        public Transform res_icon;
        public TextMeshProUGUI TXT_count;

        public RES_TYP_BUY(Transform _tr)
        {
            res_TR = _tr;
            res_icon = res_TR.Find("icon");
            TXT_count = res_TR.Find("TXT_count").GetComponent<TextMeshProUGUI>();
        }
    }
}
