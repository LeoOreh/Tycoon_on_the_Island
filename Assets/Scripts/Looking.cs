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
                Build_M.LVL_up("cave");
            }

            // полсе первого апгрейда пещеры открываем станцию
            else
            if (Land.state_buildings["energy"].lvl == -1)
            {
                Build_M.LVL_up("energy");
            }

            else
            if (Land.state_buildings["port"].lvl == -1)
            {
                Build_M.LVL_up("port");
                Land.buildings["cave"].ui_inside.SetActive(true); 
            }
        }
        else
        if (GL.state.active_land == "land_2_tree")
        {

        }
    }
}
