using System.Collections;
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
        int price = 199;
        float Q = 1;

        if (build == "energy")    { Q = 1.3f; } else
        if (build == "cave")      { Q = 1.0f; } else
        if (build == "warehouse") { Q = 1.8f; } else
        if (build == "road")      { Q = 2.5f; } else
        if (build == "factory")   { Q = 3.4f; } else
        if (build == "port")      { Q = 4.1f; }

       // Land.state_buildings[build].

        return price;
    }
}
