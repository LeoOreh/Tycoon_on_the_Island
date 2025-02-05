using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GAME_STATE: MonoBehaviour
{
    // ����������
    public string active_land = "land_1_stones";

    // ���������


    // ������ ������
    public int money = 10000; // �� �������
    public int points;


    // �������
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


    // �������
    public List<Land_cls> lands = new List<Land_cls>()
    {
        new Land_cls("land_1_stones", new string[] { "energy", "cave", "warehouse", "road", "factory", "port" }) { S = 1 },
        new Land_cls("land_2_tree",   new string[] { "energy", "warehouse", "port" })         { },
    };

    


    [System.Serializable]
    public class Land_cls
    {
        public string name;
        public int S; // 0- �� ������ 1- ������
        public List<BLDG_cls> buildings = new List<BLDG_cls> { };

        public Land_cls(string _name, string[] _upgrd)
        {
            this.name = _name;
            // ��������

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
        }
 

        [System.Serializable]
        public class BLDG_cls
        {
            public string name;
            public int lvl = -1;
            //public string[] list_upgrd;

            public List<Upgrade_cls> upgrades;

            public BLDG_cls(string _name, string[] _upgrades)
            {
                this.name = _name;
                //this.list_upgrd = _upgrades;

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
    }
}
