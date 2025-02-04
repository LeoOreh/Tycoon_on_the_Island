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
        Debug.Log(build + " > " + typ);

        float price = 20;
        float Q = 1;
        float Q2 = 1;
        float lvl = 1;

        if (build == "energy")    { Q = 1.3f; } else
        if (build == "cave")      { Q = 1.0f; } else
        if (build == "warehouse") { Q = 1.8f; } else
        if (build == "road")      { Q = 2.5f; } else
        if (build == "factory")   { Q = 3.4f; } else
        if (build == "port")      { Q = 4.1f; }

        if (typ == "books")     { Q2 = 1.15f; } else
        if (typ == "engine")    { Q2 = 5.83f; } else
        if (typ == "canister")  { Q2 = 9.25f; } else
        if (typ == "cccc")      { Q2 = 1.19f; } else
        if (typ == "ccccccc")   { Q2 = 1.21f; } else
        if (typ == "cccc")      { Q2 = 1.29f; } 

        foreach (GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls up in Land.state_buildings[build].upgrades)
        {
            if (up.name == typ) { lvl = up.lvl; }
        }

        price = ((price * Q) + (price * lvl / 4)) * Q2;


        return (int)price;
    }



    public static int Get_point_upgrade(string typ)
    {
        int p = 199;

        if (typ == "books")     { p = 1; } else
        if (typ == "engine")    { p = 5; } else
        if (typ == "canister")  { p = 10; } else
        if (typ == "cccc")      { p = 20; } else
        if (typ == "ccccccc")   { p = 35; } else
        if (typ == "cccc")      { p = 50; } 

        return p;
    }
}
