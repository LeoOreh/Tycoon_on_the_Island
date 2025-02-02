using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Land : MonoBehaviour
{

    static Dictionary<string, GameObject> lands;

    static Transform land_TR;
    static Transform upgrade_TR;
    static Transform ui_TR;
    static Dictionary<string, UPGRD_cls> builds;


    void Start()
    {
        lands = new Dictionary<string, GameObject>();
        lands["land_1_stones"] = GameObject.Find("/lands").transform.Find("land_1_stones").gameObject;
        lands["land_2_tree"]   = GameObject.Find("/lands").transform.Find("land_2_tree").gameObject;

        I(GL.state.active_land);
    }

    public static void I(string new_land_name)
    {
        foreach (KeyValuePair<string, GameObject> land in lands) { land.Value.SetActive(false); }

        if (new_land_name == "land_1_stones" ||
            new_land_name == "land_2_tree")
        {
            lands[new_land_name].SetActive(true);
        }
        else { Debug.LogError("Land.I(): wrong name!!! : " + new_land_name); }

        GL.state.active_land = new_land_name;



        land_TR = lands[GL.state.active_land].transform;
        upgrade_TR = land_TR.Find("upgrade");
        ui_TR = land_TR.Find("ui");

        builds = new Dictionary<string, UPGRD_cls>();
        builds["energy"] = new UPGRD_cls("energy", 3);
        builds["cave"] = new UPGRD_cls("cave", 3);
        builds["warehouse"] = new UPGRD_cls("warehouse", 3);
        builds["road"] = new UPGRD_cls("road", 3);
        //builds["factory"]   = new UPGRD_cls("factory",   3);
        //builds["port"]      = new UPGRD_cls("port",      3);


        foreach (GAME_STATE.Land_ l in GL.state.lands) // перебераем острова из json
        {
            if (l.name == GL.state.active_land)  // ищем наш остров
            {
                foreach (KeyValuePair<string, UPGRD_cls> upgrd in builds) // перебераем билдер сцены
                {
                    foreach (GAME_STATE.Land_.Upgrade u in l.upgrades) // перебераем апргейды из json
                    {
                        if(upgrd.Key == u.name) // если компонент такой есть
                        {
                            upgrd.Value.lvl[u.lvl].SetActive(true); // то активируем
                        }
                    }
                }
            }
        }     
    }

    public class UPGRD_cls
    {
        public GameObject GO;
        public Transform TR;
        public GameObject ui;
        public Dictionary<int, GameObject> lvl = new Dictionary<int, GameObject>();

        public UPGRD_cls(string _name, int _lvl)
        {
            GO = upgrade_TR.Find(_name).gameObject;
            TR = GO.transform;
            ui = TR.Find("UI").gameObject;

            for (int i = 0; i <= _lvl; i++)
            {
                lvl[i] = TR.Find(i.ToString()).gameObject;
            }
        }
    }
}
