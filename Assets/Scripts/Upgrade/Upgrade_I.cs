using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrade_I : MonoBehaviour
{
    static Transform TR;
    static int lvl;

    public static void I()
    {
        string scene_name = SceneManager.GetActiveScene().name;


        TR = GameObject.Find("UPGRADE/energy").transform;

        if (scene_name == "Land_1_stones")
        {
            lvl = GL.state.land_1_stones.upgrade.energy.lvl;
        }

        for (int i = 1; i <= 10; i++)
        {
            TR.Find(i.ToString()).gameObject.SetActive(false);
        }
        TR.Find(lvl.ToString()).gameObject.SetActive(true);
    }
}
