using System.Collections;
using System.Collections.Generic;
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
                Build_M.Build_lvl_up("cave");
            }
        }
        else
        if (GL.state.active_land == "land_2_tree")
        {

        }
    }
}
