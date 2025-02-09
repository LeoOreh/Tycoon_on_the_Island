using System.Collections.Generic;
using UnityEngine;

public class Numbers_M : MonoBehaviour
{
    public static int Get_Price_First_BUY(string typ)
    {
        int price = 199;

        if (typ == "energy")    { price = 200; } else
        if (typ == "cave")      { price = 350; } else
        if (typ == "warehouse") { price = 450; } else
        if (typ == "road")      { price = 600; } else
        if (typ == "factory")   { price = 2000; } else
        if (typ == "port")      { price = 1000; } 

        return price;
    }




    public static int Get_Price_Upgrade(string build, string typ)
    {
        //Debug.Log(build + " > " + typ);

        float price = 1;
        float Q = 1;
        float Q2 = 1;
        float lvl = 1;
        float point = 5;

        if (build == "cave")      { Q = 1.0f; } else
        if (build == "energy")    { Q = 1.5f; } else
        if (build == "warehouse") { Q = 2.5f; } else
        if (build == "road")      { Q = 3.5f; } else
        if (build == "factory")   { Q = 5.5f; } else
        if (build == "port")      { Q = 8.0f; }

        // books, engine, canister, tractor, buldozer, turbo, forklift, spanner, winch, parts
        if (typ == "books")     { Q2 = 1.05f; } else
        if (typ == "engine")    { Q2 = 1.06f; } else
        if (typ == "canister")  { Q2 = 1.02f; } else
        if (typ == "tractor")   { Q2 = 1.04f; } else
        if (typ == "buldozer")  { Q2 = 1.08f; } else
        if (typ == "turbo")     { Q2 = 1.03f; } else
        if (typ == "forklift")  { Q2 = 1.05f; } else
        if (typ == "spanner")   { Q2 = 1.01f; } else
        if (typ == "winch")     { Q2 = 1.10f; } else
        if (typ == "parts")     { Q2 = 1.07f; } 

        foreach (GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls up in Land.state_buildings[build].upgrades)
        {
            if (up.name == typ) 
            {
                lvl = up.lvl ;
                point = up.points; 
                //Debug.Log(lvl);
            }
        }

        price = price + point + (point * 5 * ((lvl / 2) + 1) );
        price = price * Q * Q2;
        price = price * ((lvl / 6) + 1);

        if (price <= 0) Debug.Log(build + " " + typ + " " + lvl + " " + point + " " + Q + " " + Q2);

        return (int)price;
    }




    public static int summ_upgrade(string build)
    {
        int summ = 0;

        foreach (GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls up in Land.state_buildings[build].upgrades)
        {
            summ += (up.lvl - 1) * up.points;
        }
        //Debug.Log(summ);
        return summ;
    }




    public static float upgrade_fill(string bld, int summ)
    {
        float x;
        float a = 0;
        float b = 0;
        float Q = 1;
        int lvl = Land.state_buildings[bld].lvl;
        int i_ = 0;

        if (bld == "cave")      { Q = 1.0f; } else
        if (bld == "energy")    { Q = 1.5f; } else
        if (bld == "warehouse") { Q = 2.5f; } else
        if (bld == "road")      { Q = 3.5f; } else
        if (bld == "factory")   { Q = 5.5f; } else
        if (bld == "port")      { Q = 8.0f; }

        for (int i = 0; i < 100; i++)
        {
            x = i * Q * i * 5;
            if (summ >= x) { a = x; i_ = i + 1; }
            if (summ < x) { b = x; break; }
        }

        if(i_ < lvl) { i_ = lvl; }

        float result1 = (summ - a) / (b - a);
        float result = result1 + i_ - lvl;
        Debug.Log("bld: " + bld + " i_: " + i_.ToString() + ", lvl: " + lvl.ToString() + ", result: " + result.ToString() + ", result1: " + result1.ToString());

        return result;
    }



    // вероятность нового типа ордера. обратная прогрессия (х=/2)
    public static string Port_new_order_TYP()
    {
        int count_types_orders = Port_orders_I.name_types_orders.Count;
        float[] probability = new float[count_types_orders];

        float pr_value = 0.5f;
        float summ_pr = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            probability[i] = pr_value;
            summ_pr += pr_value;
            pr_value /= 2;
        }
        float remains = 1 - summ_pr;
        probability[0] += remains;

        float r = Random.Range(0, probability[0]);
        //Debug.Log(r);
        int r_n = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            if( r < probability[i]) { r_n = i; }
        }

        int foreach_n = 0;
        string nm = "_default_1";
        foreach (KeyValuePair<string, int> item in Port_orders_I.name_types_orders)
        {
            if (foreach_n == r_n) { nm = item.Key; }
            foreach_n++;
        }

        return nm;
    }
    // "stones", "gold", "coal", "granite", "quartz", "ruby", "silver", "diamond", "silicon"
    public static string Port_new_order_res_TYP()
    {
        /*
        int types_res = Warehouse_ADD.res.Count;
        float[] probability = new float[types_res];

        float pr_value = 0.5f;
        //float summ_pr = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            probability[i] = pr_value;
            //summ_pr += pr_value;
            pr_value /= 1.5f;
        }
        //float remains = 1 - summ_pr;
        //probability[0] += remains;

        float r = Random.Range(0, probability[0]);
        //Debug.Log(r);
        int r_n = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            if (r < probability[i]) { r_n = i; }
        }

        string nm = "ruby";
        int foreach_n = 0;
        foreach (Warehouse_ADD.RES_typ item in Warehouse_ADD.res)
        {
            if (foreach_n == r_n) { nm = item.name; }
            foreach_n++;
        }*/

        int rnd = Random.Range(0, Land.state_land.res_names.Length);
        string nm = Land.state_land.res_names[rnd];

        return nm;
    }
    public static int Port_new_order_res_count(string typ)
    {
        float Q = 1;
        foreach (Warehouse_ADD.RES_typ item in Warehouse_ADD.res)
        {
            if(item.name == typ) { Q = item.probability; } break;
        }
        Q *= summ_upgrade("port");

        float nm = Random.Range(Q/5, Q);
        if(nm < 1) { nm = 1; }

        return (int)nm;
    }
    public static int Port_new_order_PRICE(string[] types, int[] count)
    {
        float price = 0;

        for (int i = 0; i < types.Length; i++)
        {
            if (types[i] != null && types[i] != "")
            {
                foreach (Warehouse_ADD.RES_typ t in Warehouse_ADD.res)
                {
                    if (t.name == types[i])
                    {
                        price += t.price * count[i];
                    }
                }
            }
        }

        price *= Random.Range(0.9f, 1.1f);
        if(price < 1) { price = 1; }

        return (int)price;
    }
}
