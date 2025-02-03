using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GAME_STATE: MonoBehaviour
{
    // глобальные
    public string active_land = "land_1_stones";

    // настройки


    // данные игрока
    public int money = 1000; // по дефолту
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
        new Land_cls("land_2_tree",   new string[] { "energy", "warehouse", "road" })         { },
    };

    


    [System.Serializable]
    public class Land_cls
    {
        public string name;
        public int S; // 0- не открыт 1- открыт
        public List<BLDG_cls> buildings = new List<BLDG_cls> { };

        public Land_cls(string _name, string[] _upgrd)
        {
            this.name = _name;
            // апгрейды
            foreach(string nm in _upgrd)
            {
                if (nm == "energy")      { buildings.Add(new BLDG_cls(nm, 1,  1,  1)); };
                if (nm == "cave")        { buildings.Add(new BLDG_cls(nm, 1,  1,  1)); };
                if (nm == "warehouse")   { buildings.Add(new BLDG_cls(nm, 1, -1, -1)); };
                if (nm == "road")        { buildings.Add(new BLDG_cls(nm, 1, -1, -1)); };
                if (nm == "factory")     { buildings.Add(new BLDG_cls(nm, 1, -1, -1)); };
                if (nm == "port")        { buildings.Add(new BLDG_cls(nm, 1, -1, -1)); };
            }
        }
 

        [System.Serializable]
        public class BLDG_cls
        {
            public string name;
            public int lvl = -1;
            public int points;

            //public List<Upgrade_cls> upgrades;

            public int books;
            public int engine;
            public int canister;

            public BLDG_cls(string _name, int _books, int _engine, int _canister)
            {
                this.name = _name;
                this.books = _books;
                this.engine = _engine;
                this.canister = _canister;
            }

            public class Upgrade_cls
            {
                public string name;
                public int lvl;
            }
        }       
    }



    /*
    public List<Dialog_cls> dialogs = new List<Dialog_cls>()
    {
        { new Dialog_cls("welcome") },
    };
    [System.Serializable]
    public class Dialog_cls
    {
        public string name;
        public int S;
        public Dialog_cls(string _name)
        {
            this.name = _name;
        }
    }*/
}
