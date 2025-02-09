using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GAME_STATE: MonoBehaviour
{
    // глобальные
    public string active_land = "land_1_stones";

    // настройки


    // данные игрока
    public int money = 10000; // по дефолту
    public int points;


    // ресурсы
    public Resources_cls resources = new Resources_cls();
    [System.Serializable]
    public class Resources_cls
    {
        public int stone;
        public int iron;

        public int silver;
        public int gold;
        public int diamond;
    }


    // острова
    public List<Land_cls> lands = new List<Land_cls>()
    {
        new Land_cls("land_1_stones", new string[] { "energy", "cave", "warehouse", "road", "factory", "port" }) { S = 1 },
        new Land_cls("land_2_tree",   new string[] { "energy", "warehouse", "port" })         { },
    };

    


    [System.Serializable]
    public class Land_cls
    {
        public string name;
        public int S; // 0- остров не открыт 1- открыт

        public List<BLDG_cls> buildings = new List<BLDG_cls> { };

        // ресурсы острова
        public string[] res_names = new string[] { "stones", "gold", "coal", "granite", "quartz", "ruby", "silver", "diamond", "silicon", 
                                                   "axe", "shovel", "bucket", "wheel", "door", "bike" };
        public List<RES_CLS> resources = new List<RES_CLS> { } ;

        // сделки порта
        public int count_orders;
        public List<PORT_order_cls> port_orders = new List<PORT_order_cls>();


        public Land_cls(string _name, string[] _upgrd)
        {
            this.name = _name;
            // апгрейды

            if (name == "land_1_stones") 
            { 
                foreach(string nm in _upgrd)
                {
                    // books, engine, canister, tractor, buldozer, turbo, forklift, spanner, winch, parts
                    if (nm == "energy")      { buildings.Add(new BLDG_cls(nm, new string[] { "engine", "turbo", "spanner", "parts", "winch" })); };
                    if (nm == "cave")        { buildings.Add(new BLDG_cls(nm, new string[] { "spanner", "tractor", "parts", "buldozer", "canister" })); };
                    if (nm == "warehouse")   { buildings.Add(new BLDG_cls(nm, new string[] { "spanner", "forklift", "tractor", "turbo", "books" })); };
                    if (nm == "road")        { buildings.Add(new BLDG_cls(nm, new string[] { "books", "buldozer", "tractor", "parts" })); };
                    if (nm == "factory")     { buildings.Add(new BLDG_cls(nm, new string[] { "spanner", "books", "tractor", "canister" })); };
                    if (nm == "port")        { buildings.Add(new BLDG_cls(nm, new string[] { "canister", "books", "winch", "spanner" })); };
                }
            }
            else
            if (name == "land_2_tree") 
            { 
                foreach(string nm in _upgrd)
                {
                    if (nm == "energy")      { buildings.Add(new BLDG_cls(nm, new string[] { "books", "engine", "canister" })); };
                    if (nm == "warehouse")   { buildings.Add(new BLDG_cls(nm, new string[] { "books", "engine", })); };
                    if (nm == "port")        { buildings.Add(new BLDG_cls(nm, new string[] { "books", "engine", "canister" })); };
                }
            }


            // ресурсы острова
            foreach (string res_name in res_names)
            {
                resources.Add(new RES_CLS(res_name));
            }
        }
 

        [System.Serializable]
        public class BLDG_cls
        {
            public string name;
            public int lvl = -1;
            public int all_res;

            public List<Upgrade_cls> upgrades;

            public BLDG_cls(string _name, string[] _upgrades)
            {
                this.name = _name;

                upgrades = new List<Upgrade_cls>();
                int point = 1;
                foreach (var nm in _upgrades) 
                { 
                    upgrades.Add(new Upgrade_cls(nm,  point)); 
                    point *= 5;
                }
            }



            [System.Serializable]
            public class Upgrade_cls
            {
                public string name;
                public int lvl = 1;
                public int points;
                public Upgrade_cls(string name, int _point)
                {
                    this.name = name;
                    this.points = _point;
                }
            }
        }


        [System.Serializable]
        public class RES_CLS
        {
            public string res_name;
            public int count;
            public int count_all;

            public RES_CLS (string res_name)
            {
                this.res_name = res_name;
            }
        }



        // предлагаемая сделка
        // port_orders.Add(new PORT_order());
        [System.Serializable]
        public class PORT_order_cls
        {
            public string typ;
            public string name;
            public List<PORT_order_res> resources = new List<PORT_order_res>();
            public int total_price;

            public PORT_order_cls() { }

            // условия сделки
            [System.Serializable]
            public class PORT_order_res
            {
                public int count_res;
                public string res_name;

                public PORT_order_res(string _res_name, int _count_res)
                {
                    count_res = _count_res;
                    res_name = _res_name;
                }
            }
        }
    }
}
