using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Land : MonoBehaviour
{
    static Dictionary<string, GameObject> lands;

    static Transform land_TR;
    public static Transform land_UI;
    static Transform buildings_TR;
    public static Dictionary<string, BLDG_cls> buildings;
    static Transform envr, envr_bld;

    // ссылки для избежания постоянных переборов
    public static GAME_STATE.Land_cls state_land; // данные актуального острова
    public static Dictionary<string, GAME_STATE.Land_cls.BLDG_cls> state_buildings; // данные о зданиях актуального острова
    public static Dictionary<string, GAME_STATE.Land_cls.RES_CLS> state_res; // данные о зданиях актуального острова


    void Start()
    {
        lands = new Dictionary<string, GameObject>();
        lands["land_1_stones"] = GameObject.Find("/lands").transform.Find("land_1_stones").gameObject;
        lands["land_2_tree"]   = GameObject.Find("/lands").transform.Find("land_2_tree").gameObject;

        // init
        I(GL.state.active_land);
        land_UI = land_TR.Find("UI");
        land_UI.GetComponent<UI_bld_btn>().I();
        land_UI.GetComponent<UI_upgrade_I>().I();

        Warehouse_ADD.I();
        Port_orders_I.I();

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
            lands[new_land_name].transform.Find("UI").gameObject.SetActive(true);
        }
        else { Debug.LogError("Land.I(): wrong name!!! : " + new_land_name); }

        GL.state.active_land = new_land_name;
        //-----------------------------------------------------------------------------------------------------------------



        //-----------------------------------------------------------------------------------------------------------------
        // ссылки для избежания постоянных переборов
        foreach (GAME_STATE.Land_cls l in GL.state.lands)
        {
            if (l.name == GL.state.active_land)  
            {
                state_land = l;  // актуальный остров
            }
        }

        state_buildings = new Dictionary<string, GAME_STATE.Land_cls.BLDG_cls>();
        foreach (GAME_STATE.Land_cls.BLDG_cls b in state_land.buildings)
        {
            state_buildings.Add(b.name, b);
        }

        state_res = new Dictionary<string, GAME_STATE.Land_cls.RES_CLS>();
        foreach (GAME_STATE.Land_cls.RES_CLS r in state_land.resources)
        {
            state_res.Add(r.res_name, r);
        }
        //-----------------------------------------------------------------------------------------------------------------



        //-----------------------------------------------------------------------------------------------------------------
        // pth
        land_TR      = lands[GL.state.active_land].transform; 
        envr         = land_TR.Find("envr");
        envr_bld     = envr.Find("bld");
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
        // init bld scene
        if (state_land == null) { Debug.LogError("scene init error, scene not found in json"); return; }

        foreach (KeyValuePair<string, BLDG_cls> bldg in buildings) // перебераем билдер сцены
        {
            foreach (GAME_STATE.Land_cls.BLDG_cls u in state_land.buildings) // перебераем здания из json
            {
                if (bldg.Key == u.name)
                {                    
                    if (u.lvl == -1) { bldg.Value.ui_lock.SetActive(true);         activ_lvl(0); }
                    if (u.lvl ==  0) { bldg.Value.ui_first_BUY.SetActive(true);    activ_lvl(u.lvl); }
                    if (u.lvl >   0) 
                    { 
                        bldg.Value.ui_upgrade.SetActive(true);                     activ_lvl(u.lvl); 
                        if (u.all_res > 0) { bldg.Value.ui_inside.SetActive(true); }
                        if (u.name == "port" && u.lvl > 0) { bldg.Value.ui_inside.SetActive(true); }
                    }

                    void activ_lvl(int n) { if (n < 2) { bldg.Value.build_lvl[n].SetActive(true); } else { bldg.Value.build_lvl[1].SetActive(true); } }
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------------------
    }

    public class BLDG_cls
    {


        public string typ;
        public GameObject GO;
        public Transform TR;
        public Transform POS;

        public GameObject ui;
        public GameObject ui_upgrade;
        public GameObject ui_first_BUY;
        public TextMeshProUGUI ui_first_BUY_price;
        public GameObject ui_lock;
        public GameObject ui_inside { get; set; }

        public Dictionary<int, GameObject> build_lvl = new Dictionary<int, GameObject>();
        public Dictionary<string, GameObject> envr_add = new Dictionary<string, GameObject>();


        public BLDG_cls(string _typ, int _lvl)
        {
            typ = _typ;

            GO = buildings_TR.Find(typ).gameObject;                         GO.SetActive(true);
            TR = GO.transform;
            POS = TR.Find("POS"); POS.gameObject.SetActive(false);

            ui           = TR.Find("UI").gameObject;                        ui.SetActive(true);
            ui_upgrade   = ui.transform.Find("upgrade").gameObject;         ui_upgrade.SetActive(false);
            ui_first_BUY = ui.transform.Find("first_BUY").gameObject;       ui_first_BUY.SetActive(false);
            ui_lock      = ui.transform.Find("lock").gameObject;            ui_lock.SetActive(false);
            ui_inside    = ui.transform.Find("inside").gameObject;          ui_inside.SetActive(false);

            // цена первой покупки
            ui_first_BUY_price = ui_first_BUY.transform.Find("TXT").GetComponent<TextMeshProUGUI>();
            ui_first_BUY_price.text = Numbers_M.Get_Price_First_BUY(typ).ToString();

            for (int i = 0; i <= _lvl; i++)
            {
                build_lvl[i] = TR.Find(i.ToString()).gameObject; 
                build_lvl[i].SetActive(false);
            }


            

            if(typ == "energy")
            {
                string nm_add = "electro";
                envr_add[nm_add] = envr_bld.Find(typ + "/" + nm_add).gameObject; 
                if (state_buildings[typ].lvl > 0) 
                { envr_add[nm_add].SetActive(true); } 
                else { envr_add[nm_add].SetActive(false); }
            }
            else
            if(typ == "cave")
            {
                string nm_add = "smoke";
                envr_add[nm_add] = envr_bld.Find(typ + "/" + nm_add).gameObject;
                if (state_buildings[typ].lvl > 0) { envr_add[nm_add].SetActive(true); } else { envr_add[nm_add].SetActive(false); }
            }
        }
    }
}
