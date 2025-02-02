using UnityEngine;

[System.Serializable]
public class GAME_STATE: MonoBehaviour
{
    public int money;
    public int points;
    public Resources resources = new Resources();
    public Land_1_stones land_1_stones = new Land_1_stones();
    public Land_2_tree land_2_tree = new Land_2_tree();

    [System.Serializable]
    public class Resources
    {
        public int stone;
        public int iron;

        public int silver;
        public int gold;
        public int diamond;
    }

    [System.Serializable]
    public class Land_1_stones
    {
        public Upgrade upgrade = new Upgrade();


        [System.Serializable]
        public class Upgrade
        {
            public Upgrade_energy energy = new Upgrade_energy();
            public Upgrade_cave cave = new Upgrade_cave();
            public Upgrade_warehouse warehouse = new Upgrade_warehouse();

            [System.Serializable]
            public class Upgrade_energy
            {
                public int lvl;
                public int points;
                public int val3;
            }
            [System.Serializable]
            public class Upgrade_cave
            {
                public int val1;
                public int val2;
                public int val3;
            }
            [System.Serializable]
            public class Upgrade_warehouse
            {
                public int val1;
                public int val2;
                public int val3;
            }
        }
    }


    [System.Serializable]
    public class Land_2_tree
    {
        public Upgrade upgrade = new Upgrade();


        [System.Serializable]
        public class Upgrade
        {
            public Upgrade_energy energy = new Upgrade_energy();

            [System.Serializable]
            public class Upgrade_energy
            {
                public int val1;
                public int val2;
                public int val3;
            }

        }
    }
}
