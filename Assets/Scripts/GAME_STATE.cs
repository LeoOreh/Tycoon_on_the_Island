using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GAME_STATE: MonoBehaviour
{
    // ����������
    public string active_land = "land_1_stones";

    // ���������


    // ������ ������
    public int money = 1000; // �� �������
    public int points;


    // �������
    public Resources resources = new Resources();
    [System.Serializable]
    public class Resources
    {
        public int stone;
        public int iron;

        public int silver;
        public int gold;
        public int diamond;
    }


    // �������
    public List<Land_> lands = new List<Land_>()
    {
        new Land_("land_1_stones", new string[] { "energy", "cave", "warehouse", "road" }) { S = 1 },
        new Land_("land_2_tree",   new string[] { "energy", "warehouse", "road" })         { },
    };

    


    [System.Serializable]
    public class Land_
    {
        public string name;
        public int S; // 0- �� ������ 1- ������
        public List<Upgrade> upgrades = new List<Upgrade> { };

        public Land_(string _name, string[] _upgrd)
        {
            name = _name;
            // ��������
            foreach(string s in _upgrd)
            {
                if (s == "energy")      { upgrades.Add(new Upgrade("energy", 0, 1,  1,  1)); };
                if (s == "cave")        { upgrades.Add(new Upgrade("energy", 0, 1,  1,  1)); };
                if (s == "warehouse")   { upgrades.Add(new Upgrade("energy", 0, 1, -1, -1)); };
                if (s == "road")        { upgrades.Add(new Upgrade("energy", 0, 1, -1, -1)); };
            }
        }
 

        [System.Serializable]
        public class Upgrade
        {
            public string name;
            public int lvl;
            public int points;

            public int books;
            public int engine;
            public int canister;

            public Upgrade(string _name, int _lvl, int _books, int _engine, int _canister)
            {
                this.name = _name;
                this.lvl = _lvl;
                this.books = _books;
                this.engine = _engine;
                this.canister = _canister;
            }
        }       
    }
}
