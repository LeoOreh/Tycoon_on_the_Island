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




    public static string Port_new_order_TYP()
    {
        string nm = "_default_1";

        return nm;
    }
    // "stones", "gold", "coal", "granite", "quartz", "ruby", "silver", "diamond", "silicon"
    public static string Port_new_order_res_TYP()
    {
        string nm = "ruby";

        return nm;
    }
    public static int Port_new_order_res_count(string typ)
    {
        int nm = Random.Range(1, 20);

        return nm;
    }
    public static int Port_new_order_PRICE(string[] typ)
    {
        int nm = Random.Range(20, 50);

        return nm;
    }
}
