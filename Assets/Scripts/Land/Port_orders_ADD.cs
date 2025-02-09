using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port_orders_ADD : Port_orders_I
{
    static int N_ordes_name;

    public static void ADD_from_jsn()
    {
        Debug.Log("ADD_from_jsn");
        foreach (GAME_STATE.Land_cls.PORT_order_cls order_jsn in Land.state_land.port_orders)
        {
            Transform tr = Instantiate(default_order_types[order_jsn.typ].order_TR, content_TR);
            tr.gameObject.SetActive(true);

            string _name = order_jsn.name;
            tr.name = _name;
            Debug.Log(tr.name);

            orders.Add(_name, new RES_TYP_CLS(_name));

            orders[_name].TXT_price.text = order_jsn.total_price.ToString();

            int N = 1;
            foreach (GAME_STATE.Land_cls.PORT_order_cls.PORT_order_res order_res_jsn in order_jsn.resources)
            {
                orders[_name].resources[N].TXT_count.text = order_res_jsn.count_res.ToString();
                orders[_name].resources[N].res_name = order_res_jsn.res_name;

                foreach (Transform icon in orders[_name].resources[N].res_icon)
                {
                    icon.gameObject.SetActive(false);
                }
                orders[_name].resources[N].res_icon.Find(order_res_jsn.res_name).gameObject.SetActive(true);
                N++;
            }
        }


        if (orders.Count < 5) { ADD_new_order(); }
    }


    public static void ADD_new_order()
    {
        Debug.Log("ADD_new_order");
        string new_name_def = Numbers_M.Port_new_order_TYP();
        Transform tr = Instantiate(default_order_types[new_name_def].order_TR, content_TR);
        tr.gameObject.SetActive(true);

        string new_name = new_name_def +  "__" + N_ordes_name;
        tr.name = new_name;
        N_ordes_name++;

        orders.Add(new_name, new RES_TYP_CLS(new_name));

        GAME_STATE.Land_cls.PORT_order_cls new_order_jsn = new GAME_STATE.Land_cls.PORT_order_cls();
        Land.state_land.port_orders.Add(new_order_jsn);
        new_order_jsn.typ = new_name_def;
        new_order_jsn.name = new_name;

        string[] types_for_price = new string[limit_res_in_order];
        int[] count_res_for_price = new int[limit_res_in_order];

        for (int i = 0; i <= limit_res_in_order; i++)
        {
            if (orders[new_name].order_TR.Find(i.ToString()) != null) 
            {
                //orders[name].resources[i] = new RES_TYP_BUY(orders[name].res_TR.Find(i.ToString()));

                string name_res = Numbers_M.Port_new_order_res_TYP();
                orders[new_name].resources[i].res_name = name_res;
                types_for_price[i] = name_res;

                int count_res = Numbers_M.Port_new_order_res_count(name_res);
                orders[new_name].resources[i].TXT_count.text = count_res.ToString();
                count_res_for_price[i] = count_res;

                foreach (Transform icon in orders[new_name].resources[i].res_icon)
                {
                    icon.gameObject.SetActive(false);
                }
                orders[new_name].resources[i].res_icon.Find(name_res).gameObject.SetActive(true);

                new_order_jsn.resources.Add(new GAME_STATE.Land_cls.PORT_order_cls.PORT_order_res(name_res, count_res));
            }
        }

        int total_price = Numbers_M.Port_new_order_PRICE(types_for_price, count_res_for_price);
        orders[new_name].TXT_price.text = total_price.ToString();
        new_order_jsn.total_price = total_price;


        if (orders.Count < 5) { ADD_new_order(); }
    }
}
