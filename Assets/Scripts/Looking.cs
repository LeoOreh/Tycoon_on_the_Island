using UnityEngine;

public class Looking : MonoBehaviour
{
    // �������� ����� ������� ������� ������� ��� �������� ��������
    public static void I()
    {
        if (GL.state.active_land == "land_1_stones")
        {
            // ���� ��� ������ ���
            if (Land.state_buildings["cave"].lvl == -1)
            {
                // ��������� ������� ������
                Build_M.LVL_up("cave");
            }

            // ����� ������� �������� ������ ��������� �������
            else
            if (Land.state_buildings["energy"].lvl == -1)
            {
                Build_M.LVL_up("energy");
            }
        }
        else
        if (GL.state.active_land == "land_2_tree")
        {

        }
    }
}
