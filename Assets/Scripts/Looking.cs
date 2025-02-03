using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MonoBehaviour
{
    // вызывать после каждого важного события для проверки действий
    public static void I()
    {
        if (GL.state.active_land == "land_1_stones")
        {
            // если еще ничего нет
            if (Land.state_buildings["cave"].lvl == -1)
            {
                // открываем покупку пещеры
                Build_M.Build_lvl_up("cave");
            }
        }
        else
        if (GL.state.active_land == "land_2_tree")
        {

        }
    }
}
