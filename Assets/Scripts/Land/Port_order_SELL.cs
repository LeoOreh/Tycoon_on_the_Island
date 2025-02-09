using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port_order_SELL : MonoBehaviour
{
    string name_order;
    GAME_STATE.Land_cls.PORT_order_cls state_oredr;

    void Start()
    {
        name_order = transform.parent.name;

        foreach (GAME_STATE.Land_cls.PORT_order_cls o in Land.state_land.port_orders)
        {
            Debug.Log(o.typ);
            if (o.name == name_order)
            {
                state_oredr = o;
                Debug.Log(o.typ + "-------");
                break;
            }
        }
    }

    public void SELL()
    {
        bool sell = true;
        Debug.Log(name_order);

        foreach (GAME_STATE.Land_cls.PORT_order_cls.PORT_order_res res_ord in state_oredr.resources)
        {
            foreach (GAME_STATE.Land_cls.RES_CLS state_res in Land.state_land.resources)
            {
                if(state_res.res_name == res_ord.res_name)
                {
                    if(state_res.count < res_ord.count_res) { sell = false; break; } 
                }
            }
        }

        if (sell == false) { return; }

        GL_UI.coins_count(state_oredr.total_price);

        Land.state_land.port_orders.Remove(state_oredr);

        Destroy(Port_orders_I.orders[name_order].order_TR.gameObject);
        Port_orders_I.orders.Remove(name_order);
    }
}
