using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Land : MonoBehaviour
{
    static Dictionary<string, GameObject> lands;

    static Transform land_TR;
    static Transform buildings_TR;
    public static Dictionary<string, BLDG_cls> buildings;

    // ссылки для избежания постоянных переборов
    public static GAME_STATE.Land_cls state_land; // данные актуального острова
    public static Dictionary<string, GAME_STATE.Land_cls.BLDG_cls> state_buildings; // данные о зданиях актуального острова


    void Start()
    {
        lands = new Dictionary<string, GameObject>();
        lands["land_1_stones"] = GameObject.Find("/lands").transform.Find("land_1_stones").gameObject;
        lands["land_2_tree"]   = GameObject.Find("/lands").transform.Find("land_2_tree").gameObject;

        // init
        I(GL.state.active_land);
        Looking.I();
    }

    public static void I(string new_land_name)
    {
        //-----------------------------------------------------------------------------------------------------------------
        // init land
        foreach (KeyValuePair<string, GameObject> _land in lands) { _land.Value.SetActive(false); }

        if (new_land_name == "land_1_stones" ||
            new_land_name == "land_2_tree")
        {
            lands[new_land_name].SetActive(true);
        }
        else { Debug.LogError("Land.I(): wrong name!!! : " + new_land_name); }


        GL.state.active_land = new_land_name;

        foreach (GAME_STATE.Land_cls l in GL.state.lands)
        {
            if (l.name == GL.state.active_land)  // ищем актуальный остров
            {
                state_land = l;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------
        


        //-----------------------------------------------------------------------------------------------------------------
        // pth
        land_TR      = lands[GL.state.active_land].transform;
        buildings_TR = land_TR.Find("buildings");

        buildings = new Dictionary<string, BLDG_cls>();
        buildings["energy"]     = new BLDG_cls("energy", 3);
        buildings["cave"]       = new BLDG_cls("cave", 3);
        buildings["warehouse"]  = new BLDG_cls("warehouse", 3);
        buildings["road"]       = new BLDG_cls("road", 3);
        buildings["factory"]    = new BLDG_cls("factory", 3);
        buildings["port"]       = new BLDG_cls("port", 3);
        //-----------------------------------------------------------------------------------------------------------------



        //-----------------------------------------------------------------------------------------------------------------
        // init scene
        if (state_land == null) { Debug.LogError("scene init error, scene not found in json"); return; }

        foreach (KeyValuePair<string, BLDG_cls> bldg in buildings) // перебераем билдер сцены
        {
            foreach (GAME_STATE.Land_cls.BLDG_cls u in state_land.buildings) // перебераем здания из json
            {
                if (bldg.Key == u.name)
                {                    
                    if (u.lvl == -1) { bldg.Value.ui_lock.SetActive(true);         activ_lvl(0); }
                    if (u.lvl ==  0) { bldg.Value.ui_first_BUY.SetActive(true);    activ_lvl(u.lvl); }
                    if (u.lvl >   0) { bldg.Value.ui_upgrade.SetActive(true);      activ_lvl(u.lvl); }

                    void activ_lvl(int n) { bldg.Value.build_lvl[n].SetActive(true); }
                }
            }
        }


        state_buildings = new Dictionary<string, GAME_STATE.Land_cls.BLDG_cls>();
        foreach (GAME_STATE.Land_cls.BLDG_cls b in state_land.buildings) 
        {
            state_buildings.Add(b.name, b);
        }
        //-----------------------------------------------------------------------------------------------------------------
    }

    public class BLDG_cls
    {
        public GameObject GO;
        public Transform TR;
        public GameObject ui;
        public GameObject ui_upgrade;
        public GameObject ui_first_BUY;
        public TextMeshProUGUI ui_first_BUY_price;
        public GameObject ui_lock;
        public Dictionary<int, GameObject> build_lvl = new Dictionary<int, GameObject>();

        public BLDG_cls(string _name, int _lvl)
        {
            GO = buildings_TR.Find(_name).gameObject;                         GO.SetActive(true);
            TR = GO.transform;

            ui           = TR.Find("UI").gameObject;                        ui.SetActive(true);
            ui_upgrade   = ui.transform.Find("upgrade").gameObject;         ui_upgrade.SetActive(false);
            ui_first_BUY = ui.transform.Find("first_BUY").gameObject;       ui_first_BUY.SetActive(false);
            ui_lock      = ui.transform.Find("lock").gameObject;            ui_lock.SetActive(false);

            // цена первой покупки
            ui_first_BUY_price = ui_first_BUY.transform.Find("TXT").GetComponent<TextMeshProUGUI>();
            ui_first_BUY_price.text = Numbers_M.Get_Price_First_BUY(_name).ToString();

            for (int i = 0; i <= _lvl; i++)
            {
                build_lvl[i] = TR.Find(i.ToString()).gameObject; 
                build_lvl[i].SetActive(false);
            }
        }
    }
}
