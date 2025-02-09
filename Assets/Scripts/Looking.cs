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

                Land.land_UI.GetComponent<UI_bld_btn>().Close_upgrade();
            }

            // если купили energy и сделали один апгрейд
            else
            if (Land.state_buildings["energy"].lvl > 0 && Land.state_buildings["port"].lvl == -1)
            {
                Build_M.LVL_up("port");
                Build_M.LVL_up("warehouse");

                Land.land_UI.GetComponent<UI_bld_btn>().Close_upgrade();
            }

            else
            if (Land.state_buildings["port"].lvl > 0 && Land.state_buildings["factory"].lvl < 0)
            {
                Build_M.LVL_up("factory");
            }



            if (GL.state.dialog.buy_energy_up == 1 && GL.state.dialog.last == 0)
            {
                GL.state.dialog.last = 1;
                GL.dlg.add_txt("last");
                Land.buildings["cave"].ui_inside.SetActive(true);
            }
        }
        else
        if (GL.state.active_land == "land_2_tree")
        {

        }
    }
}
